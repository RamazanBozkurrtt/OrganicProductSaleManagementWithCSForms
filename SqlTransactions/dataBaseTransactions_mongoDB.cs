using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using OrganicProductSaleManagementV2._0.Items;
using OrganicProductSaleManagementV2._0.SqlTransactions.userLoginTransactions;
using OrganicProductSaleManagementV3.items;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrganicProductSaleManagementV3.SqlTransactions
{
    internal class dataBaseTransactions_mongoDB : IorProSaleDal
    {
        private IMongoDatabase database;
        private IMongoClient client;

        public dataBaseTransactions_mongoDB()
        {
            setConnection();
        }

        public void setConnection()
        {
            string connectionString = "mongodb://localhost:27017";
            client = new MongoClient(connectionString);

            database = client.GetDatabase("OrganicProductSaleManagement");
        }

        HashSet<DataRow> IorProSaleDal.getUserDatasToList()
        {
            throw new NotImplementedException();
        }

        public userInfo userLogin(string userName, string password)
        {

            var userCollection = database.GetCollection<BsonDocument>("userLoginInformations");
            var filter = Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Eq("userName", userName),
                Builders<BsonDocument>.Filter.Eq("userPassword", password)
            );

            var user = userCollection.Find(filter).FirstOrDefault();

            if (user != null)
            {
                ObjectId id = (ObjectId)user["_id"];
                string phoneNumber = user["userPhoneNumber"].AsString;
                string authorizationType = user["authorizationType"].AsString;
                byte[] imageData = user["userImage"].AsByteArray;
                string userID = user["userIDNumber"].AsString;
                DateTime birthDate = user["userBirthDate"].ToUniversalTime();

                string idd = id.ToString();

                return new userInfo(idd, userName, password, phoneNumber, authorizationType, imageData, userID, birthDate);
            }

            return null;
        }

        public List<productInfo> getProducts()
        {

            var productCollection = database.GetCollection<BsonDocument>("products");
            var productTypeCollection = database.GetCollection<BsonDocument>("productType");


            var products = productCollection.Find(new BsonDocument()).ToList();
            var productInfos = new List<productInfo>();

            foreach (var product in products)
            {

                ObjectId id = product["_id"].AsObjectId;
                int amountOfProducts = product.GetValue("amountOfProducts", BsonValue.Create(0)).AsInt32;


                string productTypeId = product.GetValue("productType", BsonValue.Create(0)).AsString;


                var productTypeFilter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(productTypeId));
                var productType = productTypeCollection.Find(productTypeFilter).FirstOrDefault();
                if (productType != null)
                {

                    string productName = productType.GetValue("productName", BsonValue.Create(string.Empty)).AsString;
                    int shelfLife = productType.GetValue("productAverageShelfLife", BsonValue.Create(0)).AsInt32;
                    int price = productType.GetValue("productSellingPrice", BsonValue.Create(0)).AsInt32;
                    string stockCode = productType.GetValue("productStockCode", BsonValue.Create(string.Empty)).AsString;
                    byte[] imageData = productType.GetValue("productImage", BsonValue.Create(new byte[0])).AsByteArray;


                    var productTypeInfo = new productTypeInfo(
                        productTypeId.ToString(),
                        productName,
                        stockCode,
                        shelfLife,
                        price,
                        imageData
                    );

                    var productInfo = new productInfo(
                        id.ToString(),
                        productTypeId.ToString(),
                        amountOfProducts,
                        productTypeInfo
                    );

                    productInfos.Add(productInfo);
                }
                else
                {
                    Console.WriteLine($"ProductType not found for Product ID: {id} with ProductType ID: {productTypeId}");
                }
            }

            return productInfos;
        }



        public int getProductAmountSum(string _productTypeId)
        {
            ObjectId productTypeId = ObjectId.Parse(_productTypeId);

            var productCollection = database.GetCollection<BsonDocument>("products");
            var filter = Builders<BsonDocument>.Filter.Eq("productType", productTypeId);
            var aggregate = productCollection.Aggregate()
                .Match(filter)
                .Group(new BsonDocument
                {
                { "_id", BsonNull.Value },
                { "totalAmount", new BsonDocument("$sum", "$amountOfProducts") }
                })
                .FirstOrDefault();

            return aggregate == null ? 0 : aggregate["totalAmount"].AsInt32;
        }

        public List<productInfo> getProductsWithParameter(string searchKeyWord)
        {
            var productCollection = database.GetCollection<BsonDocument>("products");
            var productTypeCollection = database.GetCollection<BsonDocument>("productType");

            var lookup = new BsonDocument
            {
                { "$lookup", new BsonDocument
                    {
                        { "from", "productType" },
                        { "localField", "productType" },
                        { "foreignField", "_id" },
                        { "as", "productDetails" }
                    }
                }
            };

            var match = new BsonDocument
            {
                { "$match", new BsonDocument("productDetails.productName", new BsonDocument("$regex", searchKeyWord)) }
            };

            var documents = productCollection.Aggregate().AppendStage<BsonDocument>(lookup).AppendStage<BsonDocument>(match).ToList();

            List<productInfo> productInfos = new List<productInfo>();

            foreach (var doc in documents)
            {
                ObjectId id = (ObjectId)doc["_id"];
                int amountOfProducts = doc["amountOfProducts"].AsInt32;

                var productDetails = doc["productDetails"].AsBsonArray.FirstOrDefault();
                if (productDetails != null)
                {
                    ObjectId id2 = (ObjectId)doc["_id"]; ;
                    string productName = productDetails["productName"].AsString;
                    int shelfLife = productDetails["productAverageShelfLife"].AsInt32;
                    int price = productDetails["productSellingPrice"].AsInt32;
                    string stockCode = productDetails["productStockCode"].AsString;
                    byte[] productImage = productDetails["productImage"].AsByteArray;

                    string _productTypeId = id2.ToString();
                    string idd = id.ToString();

                    productTypeInfo tempPInfoType = new productTypeInfo(_productTypeId, productName, stockCode, shelfLife, price, productImage);

                    productInfos.Add(new productInfo(idd, _productTypeId, amountOfProducts, tempPInfoType));
                }
            }

            return productInfos;
        }

        public void InsertProductWithType(string productName, string productStockCode, int productAverageShelfLife, int productSellingPrice, byte[] productImage, int amountOfProducts)
        {
            var productTypeCollection = database.GetCollection<BsonDocument>("productType");
            var productCollection = database.GetCollection<BsonDocument>("products");

            using (var session = client.StartSession())
            {

                try
                {
                    var newProductType = new BsonDocument
                    {
                        { "productName", productName },
                        { "productStockCode", productStockCode },
                        { "productAverageShelfLife", productAverageShelfLife },
                        { "productSellingPrice", productSellingPrice },
                        { "productImage", new BsonBinaryData(productImage) },
                        { "productSellCount", 0 }
                    };

                    productTypeCollection.InsertOne( newProductType);
                    var productTypeId = newProductType["_id"].AsObjectId;

                    var newProduct = new BsonDocument
                    {
                        { "productType", productTypeId },
                        { "amountOfProducts", amountOfProducts }
                    };

                    productCollection.InsertOne( newProduct);

                }
                catch (Exception ex)
                {
                    session.AbortTransaction();
                    Console.WriteLine($"Transaction failed: {ex.Message}");
                }
            }
        }

        public void updateProductStockAmount(string _productId, int count, string _productTypeID)
        {
            var productCollection = database.GetCollection<BsonDocument>("products");
            var productTypeCollection = database.GetCollection<BsonDocument>("productType");

            ObjectId productId = ObjectId.Parse(_productId);
            ObjectId productTypeID = ObjectId.Parse(_productTypeID);

            /////////////////////////

            var productFilter = Builders<BsonDocument>.Filter.Eq("_id", productId);
            var productUpdate = Builders<BsonDocument>.Update.Inc("amountOfProducts", -count);

            var productTypeFilter = Builders<BsonDocument>.Filter.Eq("id", productTypeID);
            var productTypeUpdate = Builders<BsonDocument>.Update.Inc("productSellCount", count);

            using (var session = client.StartSession())
            {
                session.StartTransaction();
                try
                {
                    productCollection.UpdateOne(session, productFilter, productUpdate);
                    productTypeCollection.UpdateOne(session, productTypeFilter, productTypeUpdate);

                    session.CommitTransaction();
                }
                catch (Exception ex)
                {
                    session.AbortTransaction();
                    Console.WriteLine($"Transaction failed: {ex.Message}");
                }
            }
        }

        public void setProductStockAmount(string productId, int count)
        {
            ObjectId productIDString = ObjectId.Parse(productId);

            var collection = database.GetCollection<BsonDocument>("products");

            var filter = Builders<BsonDocument>.Filter.Eq("_id",productIDString);
            var update = Builders<BsonDocument>.Update.Set("amountOfProducts", count);

            var result = collection.UpdateOne(filter, update);

            if (result.ModifiedCount > 0)
            {
                Console.WriteLine("Güncelleme başarılı.");
            }
            else
            {
                Console.WriteLine("Hiçbir satır güncellenmedi. Sistemsel bir hata olabilir yetkiliye başvurun.");
            }
        }

        public void deleteProduct(string _productId, string _productTypeId)
        {

            ObjectId productIDStr = ObjectId.Parse(_productId);
            ObjectId productTypeIdStr = ObjectId.Parse(_productTypeId);

            try
            {
                var productCollection = database.GetCollection<BsonDocument>("products");
                var productTypeCollection = database.GetCollection<BsonDocument>("productType");

                var productFilter = Builders<BsonDocument>.Filter.Eq("_id", productIDStr);
                var productTypeFilter = Builders<BsonDocument>.Filter.Eq("_id",productTypeIdStr);

                productCollection.DeleteOne(productFilter);
                productTypeCollection.DeleteOne(productTypeFilter);

                Console.WriteLine("Ürün ve ürün türü başarıyla silindi.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata oluştu: {ex.Message}");
            }
        }

        public List<userInfo> getUsersInfo(string name)
        {

            var collection = this.database.GetCollection<BsonDocument>("userLoginInformations");

            var filter = Builders<BsonDocument>.Filter.Regex("userName", new BsonRegularExpression(name, "i"));
            var projection = Builders<BsonDocument>.Projection.Include("id")
                                                              .Include("userName")
                                                              .Include("userPassword")
                                                              .Include("userPhoneNumber")
                                                              .Include("authorizationType")
                                                              .Include("userImage")
                                                              .Include("userIDNumber")
                                                              .Include("userBirthDate");

            var usersData = collection.Find(filter).Project(projection).ToList();

            List<userInfo> users = new List<userInfo>();

            foreach (var userDoc in usersData)
            {
                ObjectId id = (ObjectId)userDoc["_id"];
                string userName = userDoc["userName"].AsString;
                string password = userDoc["userPassword"].AsString;
                string phoneNumber = userDoc["userPhoneNumber"].AsString;
                string authorizationType = userDoc["authorizationType"].AsString;
                byte[] imageData = userDoc["userImage"].AsByteArray;
                string userID = userDoc["userIDNumber"].AsString;
                DateTime dateTime = userDoc["userBirthDate"].ToUniversalTime();

                string idd = id.ToString();

                users.Add(new userInfo(idd, userName, password, phoneNumber, authorizationType, imageData, userID, dateTime));
            }

            return users;
        }

        public void updateUser(string _id, string userName, string password, string phoneNumber, string authorizationType, byte[] imageData, string userID, DateTime userBirthDate)
        {
            ObjectId id = ObjectId.Parse(_id);

            var collection = database.GetCollection<BsonDocument>("userLoginInformations");

            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            var update = Builders<BsonDocument>.Update
                .Set("userName", userName)
                .Set("userPassword", password)
                .Set("userPhoneNumber", phoneNumber)
                .Set("authorizationType", authorizationType)
                .Set("userImage", imageData)
                .Set("userIDNumber", userID)
                .Set("userBirthDate", userBirthDate);

            collection.UpdateOne(filter, update);
        }

        public void deleteUserProfile(string _id)
        {
            ObjectId id = ObjectId.Parse(_id);

            var collection = database.GetCollection<BsonDocument>("userLoginInformations");

            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            collection.DeleteOne(filter);
        }

        public void createUser(string userName, string password, string phoneNumber, string authorizationType, byte[] imageData, string userID, DateTime userBirthDate)
        {
            var collection = database.GetCollection<BsonDocument>("userLoginInformations");

            var document = new BsonDocument
            {
                { "userName", userName },
                { "userPassword", password },
                { "userPhoneNumber", phoneNumber },
                { "authorizationType", authorizationType },
                { "userImage", new BsonBinaryData(imageData) },
                { "userIDNumber", userID },
                { "userBirthDate", userBirthDate }
            };

            collection.InsertOne(document);
        }

        public List<topProducts> getTopThreeProduct()
        {

            var collection = database.GetCollection<BsonDocument>("productType");

            var filter = new BsonDocument();
            var sort = Builders<BsonDocument>.Sort.Descending("productSellCount");
            var productsData = collection.Find(filter)
                                         .Sort(sort)
                                         .Limit(3)
                                         .ToList();

            List<topProducts> tP = new List<topProducts>();

            foreach (var productDoc in productsData)
            {
                string name = productDoc["productName"].AsString;
                int price = productDoc["productSellingPrice"].AsInt32;
                int sellCount = productDoc["productSellCount"].AsInt32;
                byte[] imageData = productDoc["productImage"].AsByteArray;

                tP.Add(new topProducts(name, price, imageData, sellCount));
            }

            return tP;
        }

        public List<authLevels> getAuthLevels()
        {
            IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("authorizationLevels");

            List<BsonDocument> authLevelsData = collection.Find(new BsonDocument()).ToList();

            List<authLevels> authLevels = new List<authLevels>();
            Console.WriteLine("ass");
            foreach (var authLevelDoc in authLevelsData)
            {

                ObjectId id = (ObjectId)authLevelDoc["_id"];
                string auth = authLevelDoc["authorizationLevel"].AsString;

                String _id = id.ToString();

                authLevels aL = new authLevels(_id, auth);
                authLevels.Add(aL);
            }

            return authLevels;
        }

        public DataTable getProductsForDataTable()
        {
            DataTable dt = new DataTable();

            try
            {
                // MongoDB koleksiyonlarına erişim
                var productCollection = database.GetCollection<BsonDocument>("products");
                var productTypeCollection = database.GetCollection<BsonDocument>("productType");

                // productType koleksiyonundan tüm verileri al
                var productTypes = productTypeCollection.Find(new BsonDocument()).ToList();

                // DataTable kolonlarını tanımla
                dt.Columns.Add("id", typeof(string)); // ObjectId string olarak saklanır
                dt.Columns.Add("Tür_id", typeof(string)); // ObjectId string olarak saklanır
                dt.Columns.Add("Isim", typeof(string));
                dt.Columns.Add("Stok_Adedi", typeof(int));
                dt.Columns.Add("Raf_Omru", typeof(int));
                dt.Columns.Add("Fiyat", typeof(int));
                dt.Columns.Add("Stok_No", typeof(string));
                dt.Columns.Add("resim", typeof(byte[]));


                foreach (var productType in productTypes)
                {
                    ObjectId productTypeId = productType["_id"].AsObjectId;


                    var filter = Builders<BsonDocument>.Filter.Eq("productType", productTypeId);
                    var products = productCollection.Find(filter).ToList();

                    foreach (var product in products)
                    {
                        DataRow row = dt.NewRow();

                        row["id"] = product["_id"].ToString(); 
                        row["Tür_id"] = productTypeId.ToString(); 
                        row["Isim"] = productType.GetValue("productName", BsonValue.Create(string.Empty)).AsString;
                        row["Stok_Adedi"] = product.GetValue("amountOfProducts", BsonValue.Create(0)).AsInt32;
                        row["Raf_Omru"] = productType.GetValue("productAverageShelfLife", BsonValue.Create(0)).AsInt32;
                        row["Fiyat"] = productType.GetValue("productSellingPrice", BsonValue.Create(0)).AsInt32;
                        row["Stok_No"] = productType.GetValue("productStockCode", BsonValue.Create(string.Empty)).AsString;
                        row["resim"] = productType.GetValue("productImage", BsonValue.Create(new byte[0])).AsByteArray;

                        dt.Rows.Add(row);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri yüklenirken bir hata oluştu: " + ex.Message);
            }

            return dt;
        }


        public void updateProduct(string _id, string _id2, string name, string stockAmount, int shelfLifeDay, int price, string stockNo, byte[] imageData)
        {

            ObjectId id = ObjectId.Parse(_id);
            ObjectId id2 = ObjectId.Parse(_id2);

            var productTypeCollection = database.GetCollection<BsonDocument>("productType");
            var productsCollection = database.GetCollection<BsonDocument>("products");

            var filter1 = Builders<BsonDocument>.Filter.Eq("id", id2);
            var update1 = Builders<BsonDocument>.Update
                .Set("productName", name)
                .Set("productAverageShelfLife_Day", shelfLifeDay)
                .Set("productSellingPrice", price)
                .Set("productStockCode", stockNo)
                .Set("productImage", imageData);

            productTypeCollection.UpdateOne(filter1, update1);

            var filter2 = Builders<BsonDocument>.Filter.Eq("id", id);
            var update2 = Builders<BsonDocument>.Update.Set("amountOfProducts", stockAmount);

            productsCollection.UpdateOne(filter2, update2);
        }



    }
}

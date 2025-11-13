using Oracle.ManagedDataAccess.Client;  // Oracle namespace
using Oracle.ManagedDataAccess.Types;
using OrganicProductSaleManagementV2._0.Items;
using OrganicProductSaleManagementV3.items;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace OrganicProductSaleManagementV2._0.SqlTransactions.userLoginTransactions
{
    internal class dataBaseTransactions_Oracle : IorProSaleDal
    {
        OracleConnection connection;
        OracleCommand command;
        OracleDataReader reader;

        String serverName = "localhost";
        String port = "1521";
        String serviceName = "xe";
        String userId = "system";
        String password = "123qwe"; 

        public dataBaseTransactions_Oracle()
        {
            string connectionString = "Data Source=" + serverName + ":" + port + "/" + serviceName + ";User Id=" + userId + ";Password=" + password + ";";
            this.connection = new OracleConnection(connectionString);
        }

        HashSet<DataRow> IorProSaleDal.getUserDatasToList()
        {
            throw new NotImplementedException();
        }

        userInfo IorProSaleDal.userLogin(string _userName, string _password)
        {
            this.connection.Open();
            String query = "SELECT userLoginInformationsThree.id, userName, userPassword, userPhoneNumber, " +
                           "authorizationLevelsThree.authorizationTypes AS Authority, userImage, userIDNumber, userBirthDate " +
                           "FROM userLoginInformationsThree " +
                           "JOIN authorizationLevelsThree ON userLoginInformationsThree.authorizationType_ID = authorizationLevelsThree.id";
            this.command = new OracleCommand(query, this.connection);
            this.reader = command.ExecuteReader();
            userInfo uInfo = null;
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string userName = reader.GetString(1);
                string password = reader.GetString(2);
                string phoneNumber = reader.GetString(3);
                string authorizationType = reader.GetString(4);
                byte[] imageData = (byte[])reader.GetOracleBinary(5);
                string userID = reader.GetString(6);
                DateTime dateTime = reader.GetDateTime(7);
                
                if (userName.Trim().Equals(_userName.Trim()) && password.Trim().Equals(_password.Trim()))
                {
                    string idd = id.ToString();

                    uInfo = new userInfo(idd, userName, password, phoneNumber, authorizationType, imageData, userID, dateTime);
                }
            }
            this.connection.Close();
            return uInfo;
        }


        List<productInfo> IorProSaleDal.getProducts()
        {
            this.connection.Open();
            string query = "SELECT " +
                           "products.id, " +
                           "products.amountOfProducts, " +
                           "products.productType AS id2, " +
                           "productType.productName AS name, " +
                           "productType.productAverageShelfLife_Day AS shelfLife, " +
                           "productType.productSellingPrice AS price, " +
                           "productType.productStockCode AS stockC, " +
                           "productType.productImage AS ımage " +
                           "FROM products " +
                           "JOIN productType ON products.productType = productType.id";
            command = new OracleCommand(query, this.connection);
            reader = command.ExecuteReader();
            List<productInfo> productInfos = new List<productInfo>();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                int amountOfProducts = reader.GetInt32(1);
                int id2 = reader.GetInt32(2);
                string productName = reader.GetString(3);
                int ShelLife = reader.GetInt32(4);
                int price = reader.GetInt32(5);
                string stockC = reader.GetString(6);
                byte[] ımageData = (byte[])reader.GetOracleBinary(7);
                string idd = id.ToString();
                string idd2 = id2.ToString();

                productTypeInfo productTypeInfo = new productTypeInfo(idd2, productName, stockC, ShelLife, price, ımageData);

                productInfos.Add(new productInfo(idd, idd2, amountOfProducts, productTypeInfo));
            }
            this.connection.Close();
            return productInfos;
        }

        public int getProductAmountSum(string _productType)
        {
            int productType = Convert.ToInt32(_productType);

            this.connection.Open();
            string query = "SELECT SUM(amountOfProducts) FROM products WHERE productType = :type";
            command = new OracleCommand(query, connection);

            command.Parameters.Add(new OracleParameter(":type", productType));

            int sum = 0;
            object result = command.ExecuteScalar();
            if (result != null && result != DBNull.Value)
            {
                sum = Convert.ToInt32(result);
            }
            connection.Close();
            return sum;
        }

        public List<productInfo> getProductsWithParameter(string searchKeyWord)
        {
            this.connection.Open();
            List<productInfo> productInfos = new List<productInfo>();
            string query = "SELECT " +
                           "products.id, " +
                           "products.amountOfProducts, " +
                           "products.productType AS id2, " +
                           "productType.productName AS name, " +
                           "productType.productAverageShelfLife_Day AS shelfLife, " +
                           "productType.productSellingPrice AS price, " +
                           "productType.productStockCode AS stockC, " +
                           "productType.productImage AS ımage " +
                           "FROM products " +
                           "JOIN productType ON products.productType = productType.id " +
                           "WHERE productType.productName LIKE :keyWord";

            this.command = new OracleCommand(query, this.connection);
            command.Parameters.Add(new OracleParameter(":keyWord", "%" + searchKeyWord + "%"));
            OracleDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    int amountOfProducts = reader.GetInt32(1);

                    int id2 = reader.GetInt32(2);
                    string productName = reader.GetString(3);
                    int ShelLife = reader.GetInt32(4);
                    int price = reader.GetInt32(5);
                    string stockC = reader.GetString(6);
                    byte[] ımageData = (byte[])reader.GetOracleBinary(7);

                    string idd = id.ToString();
                    string idd2 = id2.ToString();

                    productTypeInfo tempPInfoType = new productTypeInfo(idd2, productName, stockC, ShelLife, price, ımageData);

                    productInfos.Add(new productInfo(idd, idd2, amountOfProducts, tempPInfoType));
                }
                this.connection.Close();
                reader.Close();

                return productInfos;
            }
            else
            {
                return null;
            }
        }

        public DataTable getProductsForDataTable()
        {
            this.connection.Open();
            DataTable dt = new DataTable();
            try
            {
                string query = "SELECT " +
                               "products.id, " +
                               "products.productType AS Tür_id, " +
                               "productType.productName AS Isim, " +
                               "products.amountOfProducts AS Stok_Adedi, " +
                               "productType.productAverageShelfLife_Day AS Raf_Omru, " +
                               "productType.productSellingPrice AS Fiyat, " +
                               "productType.productStockCode AS Stok_No, " +
                               "productType.productImage AS resim " +
                               "FROM products " +
                               "JOIN productType ON products.productType = productType.id";
                command = new OracleCommand(query, connection);
                using (OracleDataAdapter adapter = new OracleDataAdapter(command))
                {
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri yüklenirken bir hata oluştu: " + ex.Message);
                connection.Close();
            }
            connection.Close();
            return dt;
        }

        public void updateProduct(string _id, string _id2, string name, string stockAmount, int shelfLifeDay, int price, string stockNo, byte[] imageData)
        {

            int id = Convert.ToInt32(_id);
            int id2 = Convert.ToInt32(_id2);

            this.connection.Open();
            string query1 = "UPDATE productType " +
                           "SET productName = :name, " +
                           "productAverageShelfLife_Day = :shelfDay, " +
                           "productSellingPrice = :price, " +
                           "productStockCode = :stockCode, " +
                           "productImage = :image " +
                           "WHERE productType.id = :productId";

            command = new OracleCommand(query1, connection);

            command.Parameters.Add(new OracleParameter(":name", name));
            command.Parameters.Add(new OracleParameter(":shelfDay", shelfLifeDay));
            command.Parameters.Add(new OracleParameter(":price", price));
            command.Parameters.Add(new OracleParameter(":stockCode", stockNo));
            command.Parameters.Add(new OracleParameter(":image", imageData));
            command.Parameters.Add(new OracleParameter(":productId", id2));

            command.ExecuteNonQuery();

            string query2 = "UPDATE products " +
                            "SET amountOfProducts = :aOfP " +
                            "WHERE id = :id";
            command = new OracleCommand(query2, connection);
            command.Parameters.Add(new OracleParameter(":aOfP", stockAmount));
            command.Parameters.Add(new OracleParameter(":id", id));

            command.ExecuteNonQuery();
            connection.Close();
        }


        public void InsertProductWithType(string productName, string productStockCode, int productAverageShelfLife, int productSellingPrice, byte[] productImage, int amountOfProducts)
        {
            OracleConnection connection = null;
            OracleTransaction transaction = null;

            try
            {
                this.connection.Open();
                connection = this.connection;
                transaction = connection.BeginTransaction();


                string query1 = "INSERT INTO productType (productName, productStockCode, productAverageShelfLife_Day, productSellingPrice, productImage, productSellCount) " +
                                "VALUES (:productName, :productStockCode, :productAverageShelfLife, :productSellingPrice, :productImage, :sellCount) " +
                                "RETURNING id INTO :productTypeId";

                using (OracleCommand command1 = new OracleCommand(query1, connection))
                {
                    command1.Transaction = transaction; 

                    command1.Parameters.Add(":productName", OracleDbType.Varchar2).Value = productName;
                    command1.Parameters.Add(":productStockCode", OracleDbType.Varchar2).Value = productStockCode;
                    command1.Parameters.Add(":productAverageShelfLife", OracleDbType.Int32).Value = productAverageShelfLife;
                    command1.Parameters.Add(":productSellingPrice", OracleDbType.Int32).Value = productSellingPrice;
                    command1.Parameters.Add(":productImage", OracleDbType.Blob).Value = productImage;
                    command1.Parameters.Add(":sellCount", OracleDbType.Int32).Value = 0;
                    OracleParameter productTypeIdParam = new OracleParameter(":productTypeId", OracleDbType.Int32);
                    productTypeIdParam.Direction = ParameterDirection.Output;
                    command1.Parameters.Add(productTypeIdParam);
                    command1.ExecuteNonQuery();
                    int productTypeId = Convert.ToInt32(((OracleDecimal)productTypeIdParam.Value).ToInt32());

                    string query2 = "INSERT INTO products (productType, amountOfProducts) VALUES (:productTypeId, :amountOfProducts)";

                    using (OracleCommand command2 = new OracleCommand(query2, connection))
                    {
                        command2.Transaction = transaction;

                        command2.Parameters.Add(":productTypeId", OracleDbType.Int32).Value = productTypeId;
                        command2.Parameters.Add(":amountOfProducts", OracleDbType.Int32).Value = amountOfProducts;
                        command2.ExecuteNonQuery();
                    }
                }


                transaction.Commit();
                MessageBox.Show("Ürün başarıyla eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {

                if (transaction != null)
                {
                    transaction.Rollback();
                }
                MessageBox.Show($"Hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }


        public void updateProductStockAmount(string _productId, int count, string _productTypeID)
        {
            this.connection.Open();


            string query = "UPDATE products SET amountOfProducts = amountOfProducts - :count WHERE id = :id";

            int productId = Convert.ToInt32(_productId);
            int productTypeID = Convert.ToInt32(_productTypeID);

            try
            {
                command = new OracleCommand(query, connection);

                command.Parameters.Add(":id", OracleDbType.Int32).Value = productId;
                command.Parameters.Add(":count", OracleDbType.Int32).Value = count;

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    connection.Close();
                }
                else
                {
                    Console.WriteLine("Hiçbir satır güncellenmedi. Sistemsel bir hata olabilir yetkiliye başvurun.");
                }

                this.connection.Open();
                string query1 = "UPDATE productType SET productSellCount = productSellCount + :countt WHERE id = :id2";
                command = new OracleCommand(query1, connection);

                command.Parameters.Add(":id2", OracleDbType.Int32).Value = productTypeID;
                command.Parameters.Add(":countt", OracleDbType.Int32).Value = count;

                int rowsAffected2 = command.ExecuteNonQuery();

                if (rowsAffected2 > 0)
                {
                    connection.Close();
                }
                else
                {
                    Console.WriteLine("Hiçbir satır güncellenmedi. Sistemsel bir hata olabilir yetkiliye başvurun.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
            }
        }

        public void setProductStockAmount(string _productId, int count)
        {
            this.connection.Open();

            string query = "UPDATE products SET amountOfProducts = :count WHERE id = :id";

            int productId = Convert.ToInt32(_productId);

            try
            {
                command = new OracleCommand(query, connection);

                command.Parameters.Add(":id", OracleDbType.Int32).Value = productId;
                command.Parameters.Add(":count", OracleDbType.Int32).Value = count;

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Güncelleme başarılı.");
                    connection.Close();
                }
                else
                {
                    Console.WriteLine("Hiçbir satır güncellenmedi. Sistemsel bir hata olabilir yetkiliye başvurun.");
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
                connection.Close();
            }
        }

        public void deleteProduct(string _productID, string _productTypeID)
        {
            try
            {
                this.connection.Open();

                int productID = Convert.ToInt32(_productID);
                int productTypeID = Convert.ToInt32(_productTypeID);

                try
                {
                    string query1 = "DELETE FROM products WHERE id = :productID";

                    OracleCommand command1 = new OracleCommand(query1, connection);
                    command1.Parameters.Add(":productID", OracleDbType.Int32).Value = productID;

                    command1.ExecuteNonQuery();

                    string query2 = "DELETE FROM productType WHERE id = :productTypeID";

                    OracleCommand command2 = new OracleCommand(query2, connection);
                    command2.Parameters.Add(":productTypeID", OracleDbType.Int32).Value = productTypeID;

                    command2.ExecuteNonQuery();

                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bağlantı hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public List<userInfo> getUsersInfo(string name)
        {
            this.connection.Open();
            string query = "SELECT userLoginInformationsThree.id, userName, userPassword, userPhoneNumber, authorizationLevelsThree.authorizationTypes AS Authority, " +
                           "userImage, userIDNumber, userBirthDate " +
                           "FROM userLoginInformationsThree " +
                           "JOIN authorizationLevelsThree ON userLoginInformationsThree.authorizationType_ID = authorizationLevelsThree.id " +
                           "WHERE userLoginInformationsThree.userName LIKE :keyWord";

            command = new OracleCommand(query, connection);
            command.Parameters.Add(":keyWord", OracleDbType.Varchar2).Value = "%" + name + "%";


            reader = command.ExecuteReader();
            List<userInfo> users = new List<userInfo>();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string userName = reader.GetString(1);
                string password = reader.GetString(2);
                string phoneNumber = reader.GetString(3);
                string authorizationType = reader.GetString(4);
                byte[] imageData = (byte[])reader.GetOracleBlob(5).Value;
                string userID = reader.GetString(6);
                DateTime dateTime = reader.GetDateTime(7);

                string idd = id.ToString();

                users.Add(new userInfo(idd, userName, password, phoneNumber, authorizationType, imageData, userID, dateTime));
            }

            connection.Close();
            return users;
        }

        public void updateUser(string _id, string userName, string password, string phoneNumber, string authorizationType, byte[] imageData, string userID, DateTime userBirthDate)
        {
            List<authLevels> aL = getAuthLevels();
            int id = Convert.ToInt32(_id);
            int authID = 101;
            foreach (authLevels all in aL)
            {
                if (all.getAuthLevel().Trim().Equals(authorizationType))
                {
                    authID = Convert.ToInt32(all.getId());
                    Console.WriteLine(authID);
                }
            }

            this.connection.Open();
            string query1 = "UPDATE userLoginInformationsThree " +
                           "SET userName = :name, " +
                           "userPassword = :password, " +
                           "userPhoneNumber = :phone, " +
                           "authorizationType_ID = :authID," +
                           "userIDNumber = :userID," +
                           "userBirthDate = :date," +
                           "userImage = :image " +
                           "WHERE userLoginInformationsThree.id = :id";

            command = new OracleCommand(query1, connection);

            command.Parameters.Add(":name", OracleDbType.Varchar2).Value = userName;
            command.Parameters.Add(":password", OracleDbType.Varchar2).Value = password;
            command.Parameters.Add(":phone", OracleDbType.Varchar2).Value = phoneNumber;
            command.Parameters.Add(":authID", OracleDbType.Int32).Value = authID;
            command.Parameters.Add(":userID", OracleDbType.Varchar2).Value = userID;
            command.Parameters.Add(":date", OracleDbType.Date).Value = userBirthDate;
            command.Parameters.Add(":image", OracleDbType.Blob).Value = imageData;
            command.Parameters.Add(":id", OracleDbType.Int32).Value = id;

            command.ExecuteNonQuery();

            connection.Close();
        }

        public List<authLevels> getAuthLevels()
        {
            this.connection.Open();
            string query = "SELECT * FROM authorizationLevelsThree";
            command = new OracleCommand(query, connection);
            reader = command.ExecuteReader();
            List<authLevels> authLevels = new List<authLevels>();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string auth = reader.GetString(1);
                string idd = id.ToString();
                authLevels aL = new authLevels(idd, auth);
                authLevels.Add(aL);
            }
            reader.Close();
            connection.Close();
            return authLevels;
        }

        public void deleteUserProfile(string _id)
        {
            this.connection.Open();
            int id = Convert.ToInt32(_id);
            string query = "DELETE FROM userLoginInformationsThree WHERE id = :productID";
            command = new OracleCommand(query, connection);
            command.Parameters.Add(":productID", OracleDbType.Int32).Value = id;

            command.ExecuteNonQuery();
            connection.Close();
        }

        public void createUser(string userName, string password, string phoneNumber, string authorizationType, byte[] imageData, string userID, DateTime userBirthDate)
        {

            List<authLevels> aL = getAuthLevels();
            int authID = 101;
            foreach (authLevels all in aL)
            {
                if (all.getAuthLevel().Trim().Equals(authorizationType))
                {
                    authID = Convert.ToInt32(all.getId());
                }
            }
            this.connection.Open();
            string query = "INSERT INTO userLoginInformationsThree (userName, userPassword, userPhoneNumber, authorizationType_ID, userImage, userIDNumber, userBirthDate) " +
                           "VALUES (:name, :password, :phoneN, :authID, :FileData, :userID, :userBD)";

            using (OracleCommand command = new OracleCommand(query, connection))
            {
                command.Parameters.Add(":name", OracleDbType.Varchar2).Value = userName;
                command.Parameters.Add(":password", OracleDbType.Varchar2).Value = password;
                command.Parameters.Add(":phoneN", OracleDbType.Varchar2).Value = phoneNumber;
                command.Parameters.Add(":authID", OracleDbType.Int32).Value = authID;
                command.Parameters.Add(":FileData", OracleDbType.Blob).Value = imageData;
                command.Parameters.Add(":userID", OracleDbType.Varchar2).Value = userID;
                command.Parameters.Add(":userBD", OracleDbType.Date).Value = userBirthDate;

                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        public List<topProducts> getTopThreeProduct()
        {
            this.connection.Open();
            string query = "SELECT productName, productSellingPrice, productSellCount, productImage FROM productType ORDER BY productSellCount DESC FETCH FIRST 3 ROWS ONLY";
            command = new OracleCommand(query, connection);
            reader = command.ExecuteReader();
            List<topProducts> tP = new List<topProducts>();

            while (reader.Read())
            {
                string name = reader.GetString(0);
                int price = reader.GetInt32(1);
                int sellCount = reader.GetInt32(2);
                byte[] imageData = (byte[])reader.GetOracleBlob(3).Value;

                tP.Add(new topProducts(name, price, imageData, sellCount));
            }

            reader.Close();
            connection.Close();
            return tP;
        }

        public void createUsesr(string userName, string password, string phoneNumber, int authorizationType, byte[] imageData, string userID, DateTime userBirthDate)
        {

            this.connection.Open();
            string query = "INSERT INTO userLoginInformationsThree (userName, userPassword, userPhoneNumber, authorizationType_ID, userImage, userIDNumber, userBirthDate) " +
                           "VALUES (:name, :password, :phoneN, :authID, :FileData, :userID, :userBD)";

            using (OracleCommand command = new OracleCommand(query, connection))
            {
                command.Parameters.Add(":name", OracleDbType.Varchar2).Value = userName;
                command.Parameters.Add(":password", OracleDbType.Varchar2).Value = password;
                command.Parameters.Add(":phoneN", OracleDbType.Varchar2).Value = phoneNumber;
                command.Parameters.Add(":authID", OracleDbType.Int32).Value = authorizationType;
                command.Parameters.Add(":FileData", OracleDbType.Blob).Value = imageData;
                command.Parameters.Add(":userID", OracleDbType.Varchar2).Value = userID;
                command.Parameters.Add(":userBD", OracleDbType.Date).Value = userBirthDate;

                command.ExecuteNonQuery();
            }
            connection.Close();
        }

    }
}

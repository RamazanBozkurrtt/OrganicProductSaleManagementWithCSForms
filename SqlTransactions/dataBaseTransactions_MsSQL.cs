using Microsoft.Win32;
using OrganicProductSaleManagementV2._0.Items;
using OrganicProductSaleManagementV3.items;
using System;

using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;

using System.Windows.Forms;
using System.Xml.Linq;


namespace OrganicProductSaleManagementV2._0.SqlTransactions.userLoginTransactions
{
    internal class dataBaseTransactions_MsSQL : IorProSaleDal
    {

        SqlConnection connection;
        SqlCommand command;
        SqlDataReader reader;

        String serverName = "DESKTOP-7DO0PJK\\SQLEXPRESS";
        String dBName = "OrganicProductSaleManagement";
        
        
        public dataBaseTransactions_MsSQL()
        {
            this.connection = new SqlConnection("Data Source=" + serverName + ";" +
                                                "Initial Catalog=" + dBName + ";" +
                                                "Integrated Security=True;");

        }

        HashSet<DataRow> IorProSaleDal.getUserDatasToList()
        {


            return null;
        }


        userInfo IorProSaleDal.userLogin(string _userName, string _password)
        {

            this.connection.Open();
            String query = "SELECT dbo.userLoginInformations.id,userName,userPassword,userPhoneNumber,dbo.authorizationLevels.authorizationTypes AS Authority ," +
                            "userImage, userIDNumber, userBirthDate " +
                            "FROM userLoginInformations JOIN dbo.authorizationLevels ON dbo.userLoginInformations.authorizationType_ID = dbo.authorizationLevels.id";
            this.command = new SqlCommand(query, this.connection);
            this.reader = command.ExecuteReader();
            userInfo uInfo = null;
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string userName = reader.GetString(1);
                string password = reader.GetString(2);
                string phoneNumber = reader.GetString(3);
                string authorizationType = reader.GetString(4);
                byte[] imageData = (byte[])reader.GetSqlBinary(5);
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
                           "dbo.products.id," +
                           "dbo.products.amountOfProducts," +
                           "dbo.products.productType AS id2," +
                           "dbo.productType.productName AS name," +
                           "dbo.productType.productAverageShelfLife_Day AS shelfLife," +
                           "dbo.productType.productSellingPrice AS price," +
                           "dbo.productType.productStockCode AS stockC," +
                           "dbo.productType.productImage AS ımage " +
                           "FROM dbo.products " +
                           "JOIN dbo.productType " +
                           "ON dbo.products.productType = dbo.productType.id";
            command = new SqlCommand(query, this.connection);
            reader = command.ExecuteReader();
            List<productInfo> productInfos = new List<productInfo>();
            while(reader.Read()) 
            { 
                int id = reader.GetInt32(0);
                int amountOfProducts = reader.GetInt32(1);
                
                int id2 = reader.GetInt32(2);
                string productName = reader.GetString(3);
                int ShelLife = reader.GetInt32(4);
                int price = reader.GetInt32(5);
                string stockC = reader.GetString(6);
                byte[] ımageData = (byte[])reader.GetSqlBinary(7);

                string idd = id.ToString();
                string idd2 = id2.ToString();

                productTypeInfo productTypeInfo = new productTypeInfo(idd2,productName,stockC,ShelLife,price,ımageData);

                productInfos.Add(new productInfo(idd,idd2,amountOfProducts,productTypeInfo));
            
            
            }


            this.connection.Close();
            return productInfos;
        }

        public int getProductAmountSum(String _productType)
        {
            this.connection.Open();

            int productType = Convert.ToInt32(_productType);

            string query = "SELECT SUM(amountOfProducts) FROM dbo.products WHERE productType = @type";
            command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@type", productType);

            int sum = 0;
            object result = command.ExecuteScalar();
            if (result != null && result != DBNull.Value)
            {
                sum = Convert.ToInt32(result);
            }

            return sum;
        }

        public List<productInfo> getProductsWithParameter(string searchKeyWord)
        {
            this.connection.Open();
            List<productInfo> productInfos = new List<productInfo>();
            string query = "SELECT " +
                           "dbo.products.id," +
                           "dbo.products.amountOfProducts," +
                           "dbo.products.productType AS id2," +
                           "dbo.productType.productName AS name," +
                           "dbo.productType.productAverageShelfLife_Day AS shelfLife," +
                           "dbo.productType.productSellingPrice AS price," +
                           "dbo.productType.productStockCode AS stockC," +
                           "dbo.productType.productImage AS ımage " +
                           "FROM dbo.products " +
                           "JOIN dbo.productType " +
                           "ON dbo.products.productType = dbo.productType.id "+
                           "WHERE dbo.productType.productName LIKE @keyWord COLLATE SQL_Latin1_General_CP1_CI_AI";

            this.command = new SqlCommand(query,this.connection);
            
            command.Parameters.AddWithValue("@keyWord", "%" + searchKeyWord + "%");
            SqlDataReader reader = command.ExecuteReader();
            if(reader.HasRows)
            {
                while(reader.Read())
                {
                    int id = reader.GetInt32(0);
                    int amountOfProducts = reader.GetInt32(1);

                    int id2 = reader.GetInt32(2);
                    string productName = reader.GetString(3);
                    int ShelLife = reader.GetInt32(4);
                    int price = reader.GetInt32(5);
                    string stockC = reader.GetString(6);
                    byte[] ımageData = (byte[])reader.GetSqlBinary(7);


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
                               "dbo.products.id," +
                               "dbo.products.productType AS Tür_id," +
                               "dbo.productType.productName AS Isim," +
                               "dbo.products.amountOfProducts AS Stok_Adedi," +
                               "dbo.productType.productAverageShelfLife_Day AS Raf_Omru," +
                               "dbo.productType.productSellingPrice AS Fiyat," +
                               "dbo.productType.productStockCode AS Stok_No ," +
                               "dbo.productType.productImage AS resim " +
                               "FROM dbo.products " +
                               "JOIN dbo.productType " +
                               "ON dbo.products.productType = dbo.productType.id";
                command = new SqlCommand(query, connection);
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
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

        public void updateProduct(String _id, String _id2, string name, string stockAmount, int shelfLifeDay, int price, string stockNo, byte[] imageData)
        {
            int id = Convert.ToInt32(_id);
            int id2 = Convert.ToInt32(_id2);

            this.connection.Open();
            string query1 = "UPDATE dbo.productType " +
                           "SET productName = @name, " +
                           "productAverageShelfLife_Day = @shelfDay, " +
                           "productSellingPrice = @price, " +
                           "productStockCode = @stockCode," +
                           "productImage = @image " +
                           "WHERE dbo.productType.id = @productId;";

            command = new SqlCommand(query1, connection);

            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@shelfDay", shelfLifeDay);
            command.Parameters.AddWithValue("@price", price);
            command.Parameters.AddWithValue("@stockCode", stockNo);
            command.Parameters.AddWithValue("@image", imageData);
            command.Parameters.AddWithValue("@productId", id2);

            command.ExecuteNonQuery();

            string query2 = "UPDATE p " +
                    "SET p.amountOfProducts = @aOfP " +
                    "FROM dbo.products AS p " +
                    "WHERE p.id = @id;";
            command = new SqlCommand(query2, connection);
            command.Parameters.AddWithValue("@aOfP", stockAmount);
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();
            connection.Close();
        }

        public void InsertProductWithType(string productName, string productStockCode, int productAverageShelfLife, int productSellingPrice, byte[] productImage, int amountOfProducts)
        {
            try
            {
                this.connection.Open();

                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    string query1 = "INSERT INTO dbo.productType (productName, productStockCode, productAverageShelfLife_Day, productSellingPrice, productImage, productSellCount) " +
                                    "VALUES (@productName, @productStockCode, @productAverageShelfLife, @productSellingPrice, @productImage, @sellCount); " +
                                    "SELECT SCOPE_IDENTITY();";

                    SqlCommand command1 = new SqlCommand(query1, connection, transaction);
                    command1.Parameters.AddWithValue("@productName", productName);
                    command1.Parameters.AddWithValue("@productStockCode", productStockCode);
                    command1.Parameters.AddWithValue("@productAverageShelfLife", productAverageShelfLife);
                    command1.Parameters.AddWithValue("@productSellingPrice", productSellingPrice);
                    command1.Parameters.AddWithValue("@productImage", productImage);
                    command1.Parameters.AddWithValue("@sellCount",0);

                    int productTypeId = Convert.ToInt32(command1.ExecuteScalar());

                    string query2 = "INSERT INTO dbo.products (productType, amountOfProducts) " +
                                    "VALUES (@productTypeId, @amountOfProducts)";

                    SqlCommand command2 = new SqlCommand(query2, connection, transaction);
                    command2.Parameters.AddWithValue("@productTypeId", productTypeId);
                    command2.Parameters.AddWithValue("@amountOfProducts", amountOfProducts);

                    command2.ExecuteNonQuery();

                    transaction.Commit();
                    connection.Close();
                    MessageBox.Show("Ürün başarıyla eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bağlantı hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connection.Close();
            }
        }


        public void updateProductStockAmount(string _productId, int count, string _productTypeID)
        {
            this.connection.Open();
            string query = "UPDATE products SET amountOfProducts = amountOfProducts - @count WHERE id = @id";

            int productId = Convert.ToInt32(_productId);
            int productTypeID = Convert.ToInt32(_productId);

            try
            {
                command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", productId);
                command.Parameters.AddWithValue("@count", count);

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
                string query1 = "UPDATE productType SET productSellCount = productSellCount + @countt WHERE id = @id2";
                Console.WriteLine(productTypeID);
                command = new SqlCommand(query1, connection);

                command.Parameters.AddWithValue("@id2", productTypeID);
                command.Parameters.AddWithValue("@countt", count);

                int rowsAffected2 = command.ExecuteNonQuery();

                if (rowsAffected2 > 0)
                {
                    connection.Close();
                }
                else
                {
                    Console.WriteLine("Hiçbir satır güncellenmedi. Sistemsel bir hata olabilir yetkiliye başvurun.");
                    connection.Close();
                }
            }
            catch(Exception ex) {

                Console.WriteLine($"Hata: {ex.Message}");
                connection.Close();
            }
        }

        public void setProductStockAmount(string _productId, int count)
        {
            this.connection.Open();
            string query = "UPDATE products SET amountOfProducts = @count WHERE id = @id";

            int productId = Convert.ToInt32(_productId);

            try
            {
                command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", productId);
                command.Parameters.AddWithValue("@count", count);

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
                    string query1 = "DELETE FROM products WHERE id = @productID";

                    SqlCommand command1 = new SqlCommand(query1, connection);
                    command1.Parameters.AddWithValue("@productID", productID);

                    command1.ExecuteNonQuery();

                    string query2 = "DELETE FROM productType WHERE id = @productTypeID";

                    SqlCommand command2 = new SqlCommand(query2, connection);
                    command2.Parameters.AddWithValue("@productTypeID", productTypeID);

                    command2.ExecuteNonQuery();

                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bağlantı hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connection.Close();
            }
        }

        public List<userInfo> getUsersInfo(string name)
        {
            this.connection.Open();
            string query = "SELECT dbo.userLoginInformations.id, userName, userPassword, userPhoneNumber, dbo.authorizationLevels.authorizationTypes AS Authority, " +
                           "userImage, userIDNumber, userBirthDate " +
                           "FROM userLoginInformations " +
                           "JOIN dbo.authorizationLevels ON dbo.userLoginInformations.authorizationType_ID = dbo.authorizationLevels.id " +
                           "WHERE dbo.userLoginInformations.userName LIKE @keyWord COLLATE SQL_Latin1_General_CP1_CI_AI"; 

            this.command = new SqlCommand(query, this.connection);
            command.Parameters.AddWithValue("@keyWord", "%" + name + "%");

            this.reader = command.ExecuteReader();
            List<userInfo> users = new List<userInfo>();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string userName = reader.GetString(1);
                string password = reader.GetString(2);
                string phoneNumber = reader.GetString(3);
                string authorizationType = reader.GetString(4);
                byte[] imageData = (byte[])reader.GetSqlBinary(5);
                string userID = reader.GetString(6);
                DateTime dateTime = reader.GetDateTime(7);

                string idd = id.ToString();

                users.Add(new userInfo(idd, userName, password, phoneNumber, authorizationType, imageData, userID, dateTime));
            }

            this.connection.Close();
            return users;
        }

        public void updateUser(string _id, string userName, string password, string phoneNumber, string authorizationType, byte[] imageData, string userID, DateTime userBirthDate)
        {
            int id = Convert.ToInt32(_id);

            List<authLevels> aL = getAuthLevels();
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
            string query1 = "UPDATE userLoginInformations " +
                           "SET userName = @name, " +
                           "userPassword = @password, " +
                           "userPhoneNumber = @phone, " +
                           "authorizationType_ID = @authID," +
                           "userIDNumber = @userID," +
                           "userBirthDate = @date," +
                           "userImage = @image " +
                           "WHERE userLoginInformations.id = @id;";

            command = new SqlCommand(query1, connection);

            command.Parameters.AddWithValue("@name", userName);
            command.Parameters.AddWithValue("@password", password);
            command.Parameters.AddWithValue("@phone", phoneNumber);
            command.Parameters.AddWithValue("@authID", authID);
            command.Parameters.AddWithValue("@userID", userID);
            command.Parameters.AddWithValue("@date", userBirthDate);
            command.Parameters.AddWithValue("@image", imageData);
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();

            connection.Close();
        }
        //2. buradasın
        public List<authLevels> getAuthLevels()
        {
            this.connection.Open();
            string query = "SELECT * FROM dbo.authorizationLevels";
            command = new SqlCommand(query, this.connection);
            reader = command.ExecuteReader();
            List<authLevels> authLevels = new List<authLevels>();
            while (reader.Read())
            {

                int id = reader.GetInt32(0);
                string auth = reader.GetString(1);

                string idd = id.ToString();

                authLevels aL = new authLevels(idd,auth);
                authLevels.Add(aL);

            }
            reader.Close();
            connection.Close();
            return authLevels;
        }

        public void deleteUserProfile(string _id)
        {

            int id = Convert.ToInt32 (_id);

            this.connection.Open();
            string query = "DELETE FROM userLoginInformations WHERE id = @productID";

            command = new SqlCommand (query, this.connection);
            command.Parameters.AddWithValue("@productID", id);

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

            string query = "INSERT INTO dbo.userLoginInformations (userName, userPassword, userPhoneNumber, authorizationType_ID, userImage, userIDNumber,userBirthDate)" +
                " VALUES (@name,@password,@phoneN,@authID,@FileData,@userID,@userBD)";

            using (SqlCommand command = new SqlCommand(query, this.connection))
            {

                command.Parameters.AddWithValue("@name", userName);
                command.Parameters.AddWithValue("@password", password);
                command.Parameters.AddWithValue("@phoneN", phoneNumber);
                command.Parameters.AddWithValue("@authID", authID);
                command.Parameters.AddWithValue("@FileData", imageData);
                command.Parameters.AddWithValue("@userID", userID);
                command.Parameters.AddWithValue("@userBD", userBirthDate);

                command.ExecuteNonQuery();
            }
            this.connection.Close();
        }

        public List<topProducts> getTopThreeProduct()
        {
            this.connection.Open();
            string query = "SELECT TOP 3 productName, productSellingPrice, productSellCount, productImage FROM productType ORDER BY productSellCount DESC";
            command = new SqlCommand(query,connection);
            this.reader = command.ExecuteReader();
            List<topProducts> tP = new List<topProducts>();
            while (this.reader.Read())
            {
                string name = this.reader.GetString(0);
                int price = this.reader.GetInt32(1);
                int sellCount = this.reader.GetInt32(2);
                byte[] imageData = (byte[])reader.GetSqlBinary(3);

                tP.Add(new topProducts(name,price, imageData,sellCount));

            }
            connection.Close();
            return tP;
        }

    }
}


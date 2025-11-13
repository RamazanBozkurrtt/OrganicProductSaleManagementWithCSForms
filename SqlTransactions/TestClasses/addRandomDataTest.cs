using MongoDB.Bson;
using MongoDB.Driver;
using Oracle.ManagedDataAccess.Client;
using OrganicProductSaleManagementV2._0.SqlTransactions.userLoginTransactions;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrganicProductSaleManagementV3.SqlTransactions.TestClasses
{
    internal class addRandomDataTest : Form
    {
        const int testQueryCount = 1000000;


        //_______________________________mongoDB Connection variables_____________________
        private IMongoDatabase database;
        private IMongoClient client;
        IMongoCollection<BsonDocument> collection;
        //___________________________________________________________________________________



        //_______________________________MsSQL Connection variables_____________________
        SqlConnection connection;

        string serverName = "DESKTOP-7DO0PJK\\SQLEXPRESS";
        string dBName = "DBPTest";
        //___________________________________________________________________________________


        //_______________________________Oracle Connection variables_____________________
        OracleConnection connectionOracle;

        string serverNameORacle = "localhost";
        string port = "1521";
        string serviceName = "xe";
        string userId = "system";
        string password = "123qwe";
        //___________________________________________________________________________________

        int[] authTypesInt = { 101
                             , 102
                             , 103
        };

        string[] userNameArr = new string[testQueryCount];
        string[] passwordArr = new string[testQueryCount];
        string[] phoneNumberArr = new string[testQueryCount];
        int[] authorizationTypeArr = new int[testQueryCount];
        byte[][] imageDataArr = new byte[testQueryCount][];
        string[] userIDArr = new string[testQueryCount];
        DateTime[] userBirthDateArr = new DateTime[testQueryCount];

        private Random random = new Random();


        public addRandomDataTest()
        {
            insertDataToDataBases();
            Dispose();
        }

        private void insertDataToDataBases()
        {
            setConnection_mongoDB();
            setConnection_MsSql();
            setConnection_Oracle();

            Stopwatch mongoTimer = new Stopwatch();
            Stopwatch oracleTimer = new Stopwatch();
            Stopwatch mssqlTimer = new Stopwatch();

            for (int a = 0; a < testQueryCount; a++)
            {
                userNameArr[a] = GenerateRandomUserInfo("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz");
                passwordArr[a] = GenerateRandomUserInfo("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789");
                phoneNumberArr[a] = GenerateRandomPhoneNumber();
                authorizationTypeArr[a] = authTypesInt[random.Next(2)];
                byte[] imageData = new byte[20];
                random.NextBytes(new byte[20]);
                imageDataArr[a] = imageData;
                userIDArr[a] = GenerateRandomUserID();
                userBirthDateArr[a] = DateTime.UtcNow.AddYears(-random.Next(18, 50)).Date;
            }

            var mongoTask = Task.Run(() =>
            {
                mongoTimer.Start();
                for (int i = 0; i < testQueryCount; i++)
                {
                    CreateUserMongoDB(userNameArr[i], passwordArr[i], phoneNumberArr[i], authorizationTypeArr[i], imageDataArr[i], userIDArr[i], userBirthDateArr[i]);
                }
                mongoTimer.Stop();
            });

            var oracleTask = Task.Run(() =>
            {
                oracleTimer.Start();
                for (int i = 0; i < testQueryCount; i++)
                {
                    CreateUserOracle(userNameArr[i], passwordArr[i], phoneNumberArr[i], authorizationTypeArr[i], imageDataArr[i], userIDArr[i], userBirthDateArr[i]);
                }
                oracleTimer.Stop();
            });

            var mssqlTask = Task.Run(() =>
            {
                mssqlTimer.Start();
                for (int i = 0; i < testQueryCount; i++)
                {
                    CreateUserMsSQL(userNameArr[i], passwordArr[i], phoneNumberArr[i], authorizationTypeArr[i], imageDataArr[i], userIDArr[i], userBirthDateArr[i]);
                }
                mssqlTimer.Stop();
            });

            Task.WaitAll(mongoTask,oracleTask,mssqlTask);

            string fastestDB = " ";
            if (mongoTimer.ElapsedMilliseconds < oracleTimer.ElapsedMilliseconds)
            {
                if (mongoTimer.ElapsedMilliseconds < mssqlTimer.ElapsedMilliseconds)
                {
                    fastestDB = "MongoDB";
                }
                else
                {
                    fastestDB = "MsSql";
                }
            }
            else if (oracleTimer.ElapsedMilliseconds < mssqlTimer.ElapsedMilliseconds)
            {
                fastestDB = "Oracle";
            }
            else
            {
                fastestDB = "MsSql";
            }

            using (StreamWriter writer = new StreamWriter("C:\\Users\\Lenovo\\Desktop\\DatabaseTesLog\\result.txt"))
            {
                writer.WriteLine($"[{DateTime.Now}] \n[Planlanan Satır Sayısı]: [{testQueryCount}] \n[Test Yapılan Veritabanları]: [MongoDB],[Oracle],[MsSql]" +
                                 $"\n[Veri Ekleme Hızları(Second)]: [MongoDB]=[{mongoTimer.ElapsedMilliseconds / 1000}]" +
                                 $"\n                               [Oracle]=[{oracleTimer.ElapsedMilliseconds / 1000}]" +
                                 $"\n                               [MsSql]=[{mssqlTimer.ElapsedMilliseconds / 1000}]" +
                                 $"\n\n[En Hızlı Veri Ekleme Yapan Veritabanı]: [{fastestDB}]\n \n________________________________________________________________\n\n");
            }

            this.DialogResult = DialogResult.OK;
        }

        private void setConnection_mongoDB()
        {
            string connectionString = "mongodb://localhost:27017";
            client = new MongoClient(connectionString);
            database = client.GetDatabase("DBPTest");
            collection = database.GetCollection<BsonDocument>("userLoginInformations");
        }

        private void CreateUserMongoDB(string userName, string password, string phoneNumber, int authorizationType, byte[] imageData, string userID, DateTime userBirthDate)
        {
            

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

            this.collection.InsertOne(document);
        }

        private void setConnection_MsSql()
        {
            this.connection = new SqlConnection("Data Source=" + serverName + ";" +
                                                "Initial Catalog=" + dBName + ";" +
                                                "Integrated Security=True;");
            connection.Open();
        }

        private void CreateUserMsSQL(string userName, string password, string phoneNumber, int authorizationType, byte[] imageData, string userID, DateTime userBirthDate)
        {
            string query = "INSERT INTO dbo.userLoginInformations (userName, userPassword, userPhoneNumber, authorizationType_ID, userImage, userIDNumber, userBirthDate) " +
                           "VALUES (@name, @password, @phoneN, @authID, @FileData, @userID, @userBD)";

            using (SqlCommand command = new SqlCommand(query, this.connection))
            {
                command.Parameters.AddWithValue("@name", userName);
                command.Parameters.AddWithValue("@password", password);
                command.Parameters.AddWithValue("@phoneN", phoneNumber);
                command.Parameters.AddWithValue("@authID", authorizationType);
                command.Parameters.AddWithValue("@FileData", imageData);
                command.Parameters.AddWithValue("@userID", userID);
                command.Parameters.AddWithValue("@userBD", userBirthDate);

                command.ExecuteNonQuery();
            }
        }

        private void setConnection_Oracle()
        {
            string connectionString = "Data Source=" + serverNameORacle + ":" + port + "/" + serviceName + ";User Id=" + userId + ";Password=" + password + ";";
            this.connectionOracle = new OracleConnection(connectionString);
            connectionOracle.Open();
        }

        public void CreateUserOracle(string userName, string password, string phoneNumber, int authorizationType, byte[] imageData, string userID, DateTime userBirthDate)
        {

            string query = "INSERT INTO userLoginInformationss (userName, userPassword, userPhoneNumber, authorizationType_ID, userImage, userIDNumber, userBirthDate) " +
                           "VALUES (:name, :password, :phoneN, :authID, :FileData, :userID, :userBD)";

            using (OracleCommand command = new OracleCommand(query, connectionOracle))
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

        }

        public  string GenerateRandomUserInfo(string charSet)
        {
            int length = random.Next(8, 11);
            string chars = charSet;
            char[] stringChars = new char[length];

            for (int i = 0; i < length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new string(stringChars);
        }

        public string GenerateRandomUserID()
        {
            int length = 11;
            string chars = "0123456789";
            string charsWOutZero = "123456789";
            char[] stringChars = new char[length];

            for (int i = 0; i < length; i++)
            {
                if(i != 0)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }else
                {
                    stringChars[i] = charsWOutZero[random.Next(charsWOutZero.Length)];
                }
                
            }

            return new string(stringChars);
        }

        public string GenerateRandomPhoneNumber()
        {
            int length = 10;
            string chars = "0123456789";
            char[] stringChars = new char[length];

            for (int i = 0; i < length; i++)
            {
                if (i != 0)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }
                else
                {
                    stringChars[i] = chars[5];
                }

            }

            return new string(stringChars);
        }



    }
}

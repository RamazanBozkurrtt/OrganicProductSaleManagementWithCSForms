using Oracle.ManagedDataAccess.Types;
using OrganicProductSaleManagementV2._0.Items;
using OrganicProductSaleManagementV2._0.SqlTransactions.userLoginTransactions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MongoDB.Driver.WriteConcern;

namespace OrganicProductSaleManagementV3.SqlTransactions.TestClasses
{
    internal class testİsDatasEqual : Form
    {
        private orProSaleManager mongoManager = new orProSaleManager(new dataBaseTransactions_mongoDB());
        private orProSaleManager oracleManager = new orProSaleManager(new dataBaseTransactions_Oracle());
        private orProSaleManager msSqlManager = new orProSaleManager(new dataBaseTransactions_MsSQL());

        List<userInfo> userInfos_Oracel;
        List<userInfo> userInfos_MsSql;
        List<userInfo> userInfos_MongoDB;

        public testİsDatasEqual()
        {


            var oracleTaskEq = Task.Run(() =>
            {
                userInfos_Oracel = oracleManager.getUsersInfo("");
            });

            var msSqlTaskEq = Task.Run(() =>
            {
                userInfos_MsSql = msSqlManager.getUsersInfo("");
            });

            var mongoTaskEq = Task.Run(() =>
            {
                userInfos_MongoDB = mongoManager.getUsersInfo("");
            });

            Task.WaitAll(oracleTaskEq, msSqlTaskEq, mongoTaskEq);

            bool areEqual = true;

            List<int> mismatchedIndices = new List<int>();

            for (int i = 0; i < 1000000; i++)
            {
                bool areOracleAndMsSqlEqual =
                    userInfos_Oracel[i].getUserName().Equals(userInfos_MsSql[i].getUserName()) &&
                    userInfos_Oracel[i].getUserId().Equals(userInfos_MsSql[i].getUserId()) && 
                    userInfos_Oracel[i].getUserBirthDate().Date == userInfos_MsSql[i].getUserBirthDate().ToLocalTime().Date &&
                   userInfos_Oracel[i].getPhoneNumber().Equals(userInfos_MsSql[i].getPhoneNumber()) &&
                    userInfos_Oracel[i].getPassword().Equals(userInfos_MsSql[i].getPassword()) &&
                    userInfos_Oracel[i].getAuthorityLevel().Trim().Equals(userInfos_MsSql[i].getAuthorityLevel().Trim()) &&
                     userInfos_Oracel[i].getImageData().SequenceEqual(userInfos_MsSql[i].getImageData());

                bool areMsSqlAndMongoDbEqual =
                    userInfos_MsSql[i].getUserName().Equals(userInfos_MongoDB[i].getUserName()) &&
                    userInfos_MsSql[i].getUserId().Equals(userInfos_MongoDB[i].getUserId()) &&
                    userInfos_MsSql[i].getUserBirthDate().Date == userInfos_MongoDB[i].getUserBirthDate().ToLocalTime().Date &&
                    userInfos_MsSql[i].getPhoneNumber().Equals(userInfos_MongoDB[i].getPhoneNumber()) &&
                    userInfos_MsSql[i].getPassword().Equals(userInfos_MongoDB[i].getPassword()) &&
                    userInfos_MsSql[i].getAuthorityLevel().Trim().Equals(userInfos_MongoDB[i].getAuthorityLevel().Trim()) &&
                    userInfos_MsSql[i].getImageData().SequenceEqual(userInfos_MongoDB[i].getImageData());

                if (!(areOracleAndMsSqlEqual && areMsSqlAndMongoDbEqual))
                {
                    mismatchedIndices.Add(i);
                    areEqual = false;
                }
            }

            using (StreamWriter writer = new StreamWriter("C:\\Users\\Lenovo\\Desktop\\DatabaseTesLog\\Test.txt"))
            {
                
                writer.WriteLine($"[Sorgu Yapılan Veritabanları]: [MongoDB],[Oracle],[MsSql]\n" +
                                 $"[Veritabanlarından Elde Edilen Veri Satırı Sayıları]: [MongoDB]=[{userInfos_Oracel.Count()}]" +
                                 $"\n                                                      [Oracle]=[{userInfos_MsSql.Count()}]" +
                                 $"\n                                                      [MsSql]=[{userInfos_MongoDB.Count()}]" +
                                 $"\n[Veri Birbirine Eşit Mi]: [{areEqual}]");
                foreach(var val in mismatchedIndices)
                {
                    writer.WriteLine(val);
                }
            }
            this.DialogResult = DialogResult.OK;
            Dispose();
        }


    }
}

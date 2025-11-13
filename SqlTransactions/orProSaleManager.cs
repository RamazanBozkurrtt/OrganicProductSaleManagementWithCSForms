using OrganicProductSaleManagementV2._0.Items;
using OrganicProductSaleManagementV3.items;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicProductSaleManagementV2._0.SqlTransactions.userLoginTransactions
{
    internal class orProSaleManager
    {

        IorProSaleDal IloginControlDal;

        public orProSaleManager(IorProSaleDal _IloginControlDal)
        {
            this.IloginControlDal = _IloginControlDal;
        }

        public userInfo userLogin(String userName, String password)
        {
            return this.IloginControlDal.userLogin(userName, password);
        }

        public void updateUser(String id, String userName, String password, String phoneNumber, string authorizationType, byte[] imageData, string userID, DateTime userBirthDate)
        {
            IloginControlDal.updateUser(id,userName,password,phoneNumber,authorizationType,imageData,userID,userBirthDate);
        }

        public void createUser(String userName, String password, String phoneNumber, string authorizationType, byte[] imageData,string userID,DateTime userBirthDate)
        {
            this.IloginControlDal.createUser(userName, password, phoneNumber, authorizationType, imageData, userID, userBirthDate);
        }

        public List<authLevels> getAuthLevels()
        {
            return IloginControlDal.getAuthLevels();
        }


        public List<productInfo> getProducts()
        {
            return this.IloginControlDal.getProducts();
        }

        public int getProductAmountSum(String _productType)
        {
            return this.IloginControlDal.getProductAmountSum(_productType);
        }

        public List<productInfo> getProductWithParameter(string searchKeyWord)
        {
            return this.IloginControlDal.getProductsWithParameter(searchKeyWord);
        }

        public DataTable getProductsForDataTable()
        {
            return this.IloginControlDal.getProductsForDataTable();
        }

        public void updateProduct(String id, String id2, string name, string stockAmount, int shelfLifeDay, int price, string stockNo, byte[] imageData)
        {
            IloginControlDal.updateProduct(id, id2, name, stockAmount, shelfLifeDay, price, stockNo, imageData);
        }

        public void InsertProductWithType(string productName, string productStockCode, int productAverageShelfLife, int productSellingPrice, byte[] productImage, int amountOfProducts)
        {
            IloginControlDal.InsertProductWithType(productName,productStockCode,productAverageShelfLife,productSellingPrice,productImage,amountOfProducts);
        }

        public void updateProductStockAmount(String productId,int count, String productTypeID)
        {
            IloginControlDal.updateProductStockAmount(productId, count, productTypeID);
        }

        public void setProductStokcAmount(String productId, int count)
        {
            IloginControlDal.setProductStockAmount(productId, count);
        }

        public void deletProduct(String productID, String productTypeID)
        {
            IloginControlDal.deleteProduct(productID,productTypeID);
        }

        public List<userInfo> getUsersInfo(string name)
        {
            return IloginControlDal.getUsersInfo(name);
        }

        public void deleteUserProfile(String id)
        {
            IloginControlDal.deleteUserProfile(id);
        }

        public List<topProducts> getTopThreeProducts() 
        { 
            return IloginControlDal.getTopThreeProduct();
        
        }

        public HashSet<DataRow> getUserDatasToList()
        {
            return IloginControlDal.getUserDatasToList();
        }
    }
}

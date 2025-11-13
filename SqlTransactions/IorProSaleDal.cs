using OrganicProductSaleManagementV2._0.Items;
using OrganicProductSaleManagementV3.items;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrganicProductSaleManagementV2._0.SqlTransactions.userLoginTransactions
{
    interface IorProSaleDal
    {
        userInfo userLogin(String userName, String password);

        HashSet<DataRow> getUserDatasToList();

        void createUser(String userName, String password, String phoneNumber, string authorizationType , byte[] imageData,string userID,DateTime userBirthDate);

        void updateUser(String id,String userName, String password, String phoneNumber, string authorizationType, byte[] imageData, string userID, DateTime userBirthDate);


        List<authLevels> getAuthLevels();

        List<productInfo> getProducts();


        int getProductAmountSum(String _productType);

        List<productInfo> getProductsWithParameter(string searchKeyWord);

        DataTable getProductsForDataTable();

        void updateProduct(String id,String id2,string name,string stockAmount,int shelfLifeDay,int price,string stockNo, byte[] imageData);

        void InsertProductWithType(string productName, string productStockCode, int productAverageShelfLife, int productSellingPrice, byte[] productImage, int amountOfProducts);

        void updateProductStockAmount(String productId, int count, String productTypeID);

        void setProductStockAmount(String productId, int count);
        void deleteProduct(String productID, String productTypeID);

        List<userInfo> getUsersInfo(string name);

        void deleteUserProfile(String id);

        List<topProducts> getTopThreeProduct();

    }
}

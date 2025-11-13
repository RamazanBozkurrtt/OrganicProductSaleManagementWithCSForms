using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicProductSaleManagementV3.items
{
    public class productInfo
    {
        private string id;
        private string productType;
        private int numberOfProducts;
        private productTypeInfo productTypeClass;

        public productInfo(string _id, string _productType, int _numberOfProducts, productTypeInfo productTypeClass = null)
        {
            this.id = _id;
            this.productType = _productType; ;
            this.numberOfProducts = _numberOfProducts;
            this.productTypeClass = productTypeClass;


        }

        public string getId() { return id; }
        public string getProductType() { return productType;}
        public int getNumberOfProducts() {  return numberOfProducts; }
        public productTypeInfo getProductTypeClass() { return this.productTypeClass; }



    }
}

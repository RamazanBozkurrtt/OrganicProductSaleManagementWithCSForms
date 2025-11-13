using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicProductSaleManagementV3.items
{

    

    public class productTypeInfo
    {
        private string id;
        private string productName;
        private string productStockCode;
        private int productAverageShelfLife;
        private int price;
        private byte[] imageData;


        public productTypeInfo(string id,string _productName, string _productStockCode, int _productAverageShelfLife, int _price, byte[] _imageData) {
            this.id = id;
            this.productName = _productName;
            this.productStockCode = _productStockCode;
            this.productAverageShelfLife = _productAverageShelfLife;
            this.price = _price;
            this.imageData = _imageData;
        }

        public string getId() { return id; }
        public string getProductName() { return productName; }
        public string getProductStockCode() { return productStockCode; }
        public int getProductAverageShelfLife() { return productAverageShelfLife; }
        public int getPrice() { return price; }
        public byte[] getImageData() { return imageData; }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicProductSaleManagementV3.items
{
    internal class topProducts 
    {
        private string name;
        private int price;
        private byte[] imageData;
        private int sellCount;

        public topProducts(string _name, int _price, byte[] _imageData, int sellCount)
        {
            this.name = _name;
            this.price = _price;
            this.imageData = _imageData;
            this.sellCount = sellCount;
        }

        public string getName() { return this.name; }
        
        public int getPrice() { return this.price;}

        public byte[] getPicture() { return this.imageData; }

        public int getSellCount() {return this.sellCount;}
    }

    
}

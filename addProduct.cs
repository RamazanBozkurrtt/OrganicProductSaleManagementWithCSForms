using OrganicProductSaleManagementV2._0.SqlTransactions.userLoginTransactions;
using OrganicProductSaleManagementV3.SqlTransactions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrganicProductSaleManagementV3
{
    public partial class addProduct : Form
    {


        private Action<object, KeyEventArgs> method;
        private orProSaleManager sqlManager = new currentDatabase().getCurrentDatabaseForTransaction();
        public addProduct(Action<object, KeyEventArgs> _method)
        {
            this.method = _method;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            InitializeComponent();
        }

        private void callMethodFromMainFormClass(object sender, KeyEventArgs e)
        {
            method(sender, e);
        }

        private void addProductbttn_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(productNameTextField.Text) ||
                string.IsNullOrWhiteSpace(productStockCodeTextField.Text) ||
                string.IsNullOrWhiteSpace(productShelfLifeTextField.Text) ||
                string.IsNullOrWhiteSpace(productPriceTextField.Text) ||
                string.IsNullOrWhiteSpace(productStockAmountTextField.Text))
            {
                MessageBox.Show("Tüm alanlar doldurulmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (productStockCodeTextField.Text.Length != 10)
            {
                MessageBox.Show("Stok kodu 10 karakter olmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
            string stockCodeSuffix = productStockCodeTextField.Text.Substring(productStockCodeTextField.Text.Length - 4);
            if (stockCodeSuffix != "1600" && stockCodeSuffix != "1100")
            {
                MessageBox.Show("Stok kodunun son 4 hanesi '1600' veya '1100' olmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
            if (productNameTextField.Text.Length > 15)
            {
                MessageBox.Show("Ürün adı en fazla 15 karakter olmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
            if (imageBox.Image == null)
            {
                MessageBox.Show("Lütfen bir resim seçiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
            string productName = productNameTextField.Text.Trim();
            string productStockCode = productStockCodeTextField.Text.Trim();

            

            if (!int.TryParse(productShelfLifeTextField.Text, out int productAverageShelfLife))
            {
                MessageBox.Show("Raf ömrü sayısal bir değer olmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(productPriceTextField.Text, out int productSellingPrice))
            {
                MessageBox.Show("Fiyat sayısal bir değer olmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
            if (!int.TryParse(productStockAmountTextField.Text, out int amountOfProducts))
            {
                MessageBox.Show("Stok miktarı sayısal bir değer olmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
            byte[] productImage;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                imageBox.Image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                productImage = memoryStream.ToArray();
            }

            
            sqlManager.InsertProductWithType(productName, productStockCode, productAverageShelfLife, productSellingPrice, productImage, amountOfProducts);
            callMethodFromMainFormClass(null, null);
            Dispose();
        }

        private void addProduct_Load(object sender, EventArgs e)
        {

        }

        //resim seçmeyi sağlar
        private void setImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                imageBox.Image = Image.FromFile(openFileDialog.FileName);
            }
        }


        private void uploadImageButton_Click(object sender, EventArgs e)
        {
            setImage();
        }
    }
}

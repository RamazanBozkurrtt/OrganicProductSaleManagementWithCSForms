using OrganicProductSaleManagementV2._0.SqlTransactions.userLoginTransactions;
using OrganicProductSaleManagementV3.items;
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
    public partial class setAmountOfProduct : Form
    {
        productInfo pInfo;
        private Action<object, KeyEventArgs> method;
        private orProSaleManager sqlManager = new currentDatabase().getCurrentDatabaseForTransaction();


        public setAmountOfProduct(productInfo _pINfo, Action<object, KeyEventArgs> _method)
        {
            this.method = _method;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.pInfo = _pINfo;
            InitializeComponent();
        }

        private void callMethodFromMainFormClass(object sender, KeyEventArgs e)
        {
            method(sender, e);
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(newStockTextBox.Text, out int stokcAmount))
                {
                    MessageBox.Show("Sayı değeri girilmemiş yada boş bırakılmış.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (stokcAmount >= 0)
                {
                    sqlManager.setProductStokcAmount(this.pInfo.getId(), stokcAmount);
                    MessageBox.Show("Stok Başarıyla Güncellendir !");
                    callMethodFromMainFormClass(null,null);
                    Dispose();
                }
                else
                {
                    MessageBox.Show("0 veya 0'dan büyük bir değer giriniz!");
                }
            }catch(Exception ex) {
                MessageBox.Show("Bir hata oluştu "+ex.Message);
            
            }
        }

        private void setAmountOfProduct_Load(object sender, EventArgs e)
        {
            setTextBoxTexts();
            setImage();
        }

        private void setTextBoxTexts()
        {
            productNameTextBox.Text = this.pInfo.getProductTypeClass().getProductName().Trim();
            productShelfLifeTextBox.Text = this.pInfo.getProductTypeClass().getProductAverageShelfLife().ToString().Trim();
            productStockNoTextBox.Text = this.pInfo.getProductTypeClass().getProductStockCode().Trim();
            currentStockTextBox.Text = this.pInfo.getNumberOfProducts().ToString().Trim();
        }

        private void setImage()
        {
            using (MemoryStream ms = new MemoryStream(this.pInfo.getProductTypeClass().getImageData()))
            {
                this.productImage.Image = Image.FromStream(ms);
            }
        }
    }
}

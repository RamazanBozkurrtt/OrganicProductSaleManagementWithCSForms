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
using System.Xml.Linq;

namespace OrganicProductSaleManagementV3
{
    public partial class productTransactionsPage : Form
    {
        orProSaleManager sqlTransactions = new currentDatabase().getCurrentDatabaseForTransaction();
        DataGridViewRow selectedRow;
        string productIDInput;
        bool isAdmin;
        public productTransactionsPage(bool _isAdmin,string productID = "-1")
        {
            this.isAdmin = _isAdmin;
            productIDInput = productID;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            InitializeComponent();
        }

        private void productTransactionsPage_Load(object sender, EventArgs e)
        {
            DataTable datasTable = sqlTransactions.getProductsForDataTable();

            dataTable.DataSource = datasTable;
            setClicked();
        }


        private void imageBox_Resize(object sender, EventArgs e)
        {

        }

        private void setImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Image originalImage = Image.FromFile(openFileDialog.FileName);
                int width, height;

                if (int.TryParse(imageWidthTextField.Text, out width) && int.TryParse(imageHeightTextField.Text, out height))
                {
                    if (width > 0 && height > 0) 
                    {
                        Image resizedImage = ResizeImage(originalImage, width, height);
                        imageBox.Image = resizedImage;
                    }
                    else
                    {
                        MessageBox.Show("Genişlik ve yükseklik 0'dan büyük olmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Image resizedImage = ResizeImage(originalImage, 200, 200);
                        imageBox.Image = resizedImage;
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen geçerli bir sayı giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void setClicked()
        {
            if (!productIDInput.Equals("-1"))
            {
                foreach (DataGridViewRow row in dataTable.Rows)
                {

                    if (row.Cells[0].Value != null)
                    {
                        if (row.Cells[0].Value.ToString().Equals(productIDInput.ToString()))
                        {
                            selectedRow = row;
                            printDatasToTextFields();
                            button1_Click(null, null);
                        }
                    }
                    
                    

                }
            }

            
        }

        private Image ResizeImage(Image originalImage, int width, int height)
        {
            // Yeni boyutlarda Bitmap oluştur
            Bitmap resizedBitmap = new Bitmap(width, height);

            // Graphics nesnesi oluştur ve resmi yeni boyutlara göre çiz
            using (Graphics g = Graphics.FromImage(resizedBitmap))
            {
                // Interpolasyon modu ile yüksek kaliteli resim boyutlandırma
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(originalImage, 0, 0, width, height);
            }

            return resizedBitmap;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (selectedRow == null)
            {
                MessageBox.Show("Seçili satır bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            productNameTextField.Enabled = true;
            productStockCodeTextField.Enabled = true;
            productShelfLifeTextField.Enabled = true;
            productPriceTextField.Enabled = true;
            imageHeightTextField.Enabled = true;
            imageWidthTextField.Enabled = true;
            productStockAmountTextField.Enabled = true;
            uploadImageButton.Enabled = true;
            confirmUpdateButton.Enabled = true;
            deleteButton.Enabled = true;
            updateButton.Click += new System.EventHandler(button1_Click2);
            updateButton.Text = "İptal Et";
        }

        private void button1_Click2(object sender, EventArgs e)
        {
            productNameTextField.Enabled = false;
            uploadImageButton.Enabled= false;
            confirmUpdateButton.Enabled= false;
            productStockCodeTextField.Enabled = false;
            productShelfLifeTextField.Enabled = false;
            productPriceTextField.Enabled = false;
            productStockAmountTextField.Enabled = false;
            imageHeightTextField.Enabled = false;
            imageWidthTextField.Enabled = false;
            deleteButton.Enabled = false;
            imageBox.Image = null;
            updateButton.Click += new System.EventHandler(button1_Click);
            updateButton.Text = "Ürünü Güncelle";

            printDatasToTextFields();

        }

        private void printDatasToTextFields()
        {
            try
            {

                // Hücre değerlerini kontrol ederek textboxlara yazma
                productNameTextField.Text = selectedRow.Cells[2]?.Value?.ToString() ?? "";
                productStockAmountTextField.Text = selectedRow.Cells[3]?.Value?.ToString() ?? "";
                productShelfLifeTextField.Text = selectedRow.Cells[4]?.Value?.ToString() ?? "";
                productPriceTextField.Text = selectedRow.Cells[5]?.Value?.ToString() ?? "";
                productStockCodeTextField.Text = selectedRow.Cells[6]?.Value?.ToString() ?? "";

                // resim kontrolü yapma
                if (selectedRow.Cells[7].Value is byte[] imageBytes)
                {
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        imageBox.Image = Image.FromStream(ms);
                    }
                }
                else
                {
                    imageBox.Image = null;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Bir hata oluştu. Program devam ediyor..."+ex.Message;
            }

        }

        private void dataTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedRow = dataTable.Rows[e.RowIndex];

                printDatasToTextFields();
            }
        }

        private void confirmUpdateButton_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show(
            "Ürünü Güncellemek İstediğiniz Emin Misiniz?",
            "Ürünü Güncelleme Onayı",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        );


            if (result == DialogResult.No)
            {
                MessageBox.Show("Ürün Güncelleme İptal Edildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
  

                // TextBox değerlerinin boş olup olmadığını kontrol et
                if (string.IsNullOrWhiteSpace(productNameTextField.Text) ||
                    string.IsNullOrWhiteSpace(productStockAmountTextField.Text) ||
                    string.IsNullOrWhiteSpace(productShelfLifeTextField.Text) ||
                    string.IsNullOrWhiteSpace(productPriceTextField.Text) ||
                    string.IsNullOrWhiteSpace(productStockCodeTextField.Text))
                {
                    MessageBox.Show("Değerler boş olamaz. Lütfen tüm alanları doldurduğunuzdan emin olun.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // resim kontrolü
                if (imageBox.Image == null)
                {
                    MessageBox.Show("Resim alanı boş bırakılamaz. Lütfen bir resim seçiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // resmi byte[] formatına çevirme
                byte[] imageData;
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    // resmin kopyasını alma
                    using (Image tempImage = new Bitmap(imageBox.Image))
                    {
                        tempImage.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                        imageData = memoryStream.ToArray();
                    }
                }

                // verileri parse etme ve doğrulama
                string name = productNameTextField.Text.Trim();
                string stockAmount = productStockAmountTextField.Text.Trim();
                if (!int.TryParse(productShelfLifeTextField.Text, out int shelfLifeDay))
                {
                    MessageBox.Show("Raf ömrü değeri sayı olmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(productPriceTextField.Text, out int price))
                {
                    MessageBox.Show("Fiyat değeri sayı olmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string stockCode = productStockCodeTextField.Text.Trim();

                // SQL işlemi
                try
                {
                    sqlTransactions.updateProduct(
                        (selectedRow.Cells[0].Value).ToString(),
                        (selectedRow.Cells[1].Value).ToString(),// ID
                        name,
                        stockAmount,
                        shelfLifeDay,
                        price,
                        stockCode,
                        imageData
                    );

                    MessageBox.Show("Ürün başarıyla güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Veritabanı güncelleme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception generalEx)
            {
                MessageBox.Show($"Beklenmeyen bir hata oluştu: {generalEx.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void uploadImageButton_Click(object sender, EventArgs e)
        {
            setImage();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show(
            "Ürününü Silmek İstediğinize Emin Misiniz?",
            "Ürün İptal Onayı",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        );

            if ( result == DialogResult.Yes ) {
                string productId = selectedRow.Cells[0].Value.ToString();
                string productTypeId = selectedRow.Cells[1].Value.ToString();

                sqlTransactions.deletProduct(productId.ToString(), productTypeId.ToString());
                MessageBox.Show("Ürün Başarıyla Silindi");
                productTransactionsPage_Load(null, null);
                productNameTextField.Text = "";
                productStockCodeTextField.Text = "";
                productShelfLifeTextField.Text = "";
                productPriceTextField.Text = "";
                productStockAmountTextField.Text = "";
                imageWidthTextField.Text = "";
                imageHeightTextField.Text = "";

                button1_Click2(null,null);

            }
            else if( result == DialogResult.No ) {
                MessageBox.Show("Silme İşlemi İptal Edildi");
                return;
            }

            
        }
    }
}

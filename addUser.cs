using OrganicProductSaleManagementV2._0.SqlTransactions.userLoginTransactions;
using OrganicProductSaleManagementV3.items;
using OrganicProductSaleManagementV3.SqlTransactions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace OrganicProductSaleManagementV3
{
    public partial class addUser : Form
    {
        
        private List<string> ints = new List<string>();
        private Action<object, KeyEventArgs> method;
        private orProSaleManager sqlManager = new currentDatabase().getCurrentDatabaseForTransaction();
        public addUser(Action<object, KeyEventArgs> _method)
        {
            sqlManager = new currentDatabase().getCurrentDatabaseForTransaction();
            this.method = _method;
            InitializeComponent();
            setComboBoxSettings();
        }

        private void addCustomer_Load(object sender, EventArgs e)
        {
            
        
        }

        private void callMethodFromMainFormClass(object sender, KeyEventArgs e)
        {
            method(sender, e);
        }


        private void setComboBoxSettings()
        {

            List<authLevels> authLevels = sqlManager.getAuthLevels();

            foreach (authLevels aLevel in authLevels)
            {
                Console.WriteLine(aLevel.getAuthLevel());
                this.comboBoxAuth.Items.Add(aLevel.getAuthLevel());
                this.ints.Add(aLevel.getId());
            }
            
        }

        private void uploadImageButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Resim Seç",
                Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp|Tüm Dosyalar|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };


            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {

                    imageBox.Image = Image.FromFile(openFileDialog.FileName);
                    imageBox.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void addProductbttn_Click(object sender, EventArgs e)
        {
            if (userIdTextBox.Text.Length != 11 || !long.TryParse(userIdTextBox.Text, out _))
            {
                MessageBox.Show("User ID 11 haneli olmalıdır ve yalnızca sayılardan oluşmalıdır.", "Geçersiz Girdi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (phoneTextBox.Text.Length != 10 || !long.TryParse(phoneTextBox.Text, out _))
            {
                MessageBox.Show("Telefon numarası 10 haneli olmalıdır ve yalnızca sayılardan oluşmalıdır.", "Geçersiz Girdi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (nameTextBox.Text.Length > 15)
            {
                MessageBox.Show("Ad en fazla 15 karakter uzunluğunda olmalıdır.", "Geçersiz Girdi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {

                byte[] imageData;
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (Image tempImage = new Bitmap(imageBox.Image))
                    {
                        tempImage.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                        imageData = memoryStream.ToArray();
                    }
                }

                sqlManager.createUser(nameTextBox.Text.Trim(), passwordTextBox.Text.Trim(), phoneTextBox.Text.Trim(), this.comboBoxAuth.SelectedItem.ToString().Trim(), imageData, userIdTextBox.Text.Trim(), dateTimePicker1.Value);




                MessageBox.Show("Kullanıcı başarıyla güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                callMethodFromMainFormClass(null,null);
                Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

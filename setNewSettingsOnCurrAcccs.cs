using OrganicProductSaleManagementV2._0.Items;
using OrganicProductSaleManagementV2._0.SqlTransactions.userLoginTransactions;
using OrganicProductSaleManagementV3.SqlTransactions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrganicProductSaleManagementV3.items
{
    public partial class setNewSettingsOnCurrAcccs : Form
    {
        private orProSaleManager sqlManager = new currentDatabase().getCurrentDatabaseForTransaction();
        private Action<object, KeyEventArgs> method;
        private userInfo uInfo;
        public setNewSettingsOnCurrAcccs(userInfo _uInfo, Action<object, KeyEventArgs> _method)
        {
            this.method = _method;
            this.uInfo = _uInfo;
            InitializeComponent();
        }

        private void setNewSettingsOnCurrAcccs_Load(object sender, EventArgs e)
        {
            Text = "Profil Güncelleme";
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            setDatas();
        }

        private void callMethodFromMainFormClass(object sender, KeyEventArgs e)
        {
            method(sender, e);
        }

        private void setDatas()
        {
            this.nameTextBox.Text = uInfo.getUserName().Trim();
            this.passwordTextBox.Text = uInfo.getPassword().Trim();
            this.phoneTextBox.Text = uInfo.getPhoneNumber().Trim();
            this.authTextBox.Text = uInfo.getAuthorityLevel().Trim();
            this.userIdTextBox.Text = uInfo.getUserId().Trim();
            this.dateTimePicker1.Value = uInfo.getUserBirthDate();


            using (MemoryStream ms = new MemoryStream(uInfo.getImageData()))
            {
                profilPicture.Image = Image.FromStream(ms);
            }

        }

        private void selectPictureButton_Click(object sender, EventArgs e)
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
                    
                    profilPicture.Image = Image.FromFile(openFileDialog.FileName);
                    profilPicture.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void updateInfos_Click(object sender, EventArgs e)
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
                    using (Image tempImage = new Bitmap(profilPicture.Image))
                    {
                        tempImage.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                        imageData = memoryStream.ToArray();
                    }
                }

                
                sqlManager.updateUser(
                    this.uInfo.getId(),
                    nameTextBox.Text.Trim(),
                    passwordTextBox.Text.Trim(),
                    phoneTextBox.Text.Trim(),
                    this.authTextBox.Text.Trim(),
                    imageData,
                    userIdTextBox.Text.Trim(),
                    dateTimePicker1.Value
                );

                MessageBox.Show("Kullanıcı başarıyla güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                callMethodFromMainFormClass(null,null);
                Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}

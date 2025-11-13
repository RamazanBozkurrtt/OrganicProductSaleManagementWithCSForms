using OrganicProductSaleManagementV2._0.Items;
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
    public partial class showProfilDetail : Form
    {
        private orProSaleManager sqlManager = new currentDatabase().getCurrentDatabaseForTransaction();
        private Action<object, KeyEventArgs> method;
        private bool isAdmin;
        private userInfo uInfo;
        private List<string> ints = new List<string>();
         
        public showProfilDetail(bool isAdmin,userInfo _uInfo, Action<object, KeyEventArgs> _method)
        {
            this.method = _method;
            this.uInfo = _uInfo;
            this.isAdmin = isAdmin;
            InitializeComponent();
        }
        private void callMethodFromMainFormClass(object sender, KeyEventArgs e)
        {
            method(sender, e);
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

        private void showProfilDetail_Load(object sender, EventArgs e)
        {
            Text = "Profil Detayı";
            setAdminSettings();
            setComboBoxSettings();
            setDatas();


        }

        private void setDatas()
        {
            this.nameTextBox.Text = uInfo.getUserName().Trim();
            this.passwordTextBox.Text = uInfo.getPassword().Trim();
            this.phoneTextBox.Text = uInfo.getPhoneNumber().Trim();
            this.userIdTextBox.Text = uInfo.getUserId().Trim();
            this.dateTimePicker1.Value = uInfo.getUserBirthDate();

            using (MemoryStream ms = new MemoryStream(uInfo.getImageData()))
            {
                profilPicture.Image = Image.FromStream(ms);
            }
        }

        

        private void setAdminSettings()
        {
            if(isAdmin)
            {
                nameTextBox.Enabled = true;
                passwordTextBox.Enabled = true;
                userIdTextBox.Enabled = true;
                phoneTextBox.Enabled = true;
                userAuthChoose.Enabled = true;
                dateTimePicker1.Enabled = true;
                updateButton.Enabled = true;
                deleteProfile.Enabled = true;
                selectPictureButton.Enabled = true;
            }
            else
            {
                nameTextBox.Enabled = false;
                passwordTextBox.Enabled = false;
                userIdTextBox.Enabled = false;
                phoneTextBox.Enabled = false;
                userAuthChoose.Enabled = false;
                dateTimePicker1.Enabled = false;
                updateButton.Enabled = false;
                deleteProfile.Enabled = false;
                selectPictureButton.Enabled = false;

                passwordTextBox.PasswordChar = '*';

            }
        }

        private void setComboBoxSettings()
        {
            List<authLevels> authLevels = sqlManager.getAuthLevels();

            foreach(authLevels aLevel in authLevels)
            {
                this.userAuthChoose.Items.Add(aLevel.getAuthLevel());
                this.ints.Add(aLevel.getId());
            }
            userAuthChoose.Text = uInfo.getAuthorityLevel();
        }

        private void updateButton_Click(object sender, EventArgs e)
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
                    this.userAuthChoose.SelectedItem.ToString().Trim(),
                    imageData,
                    userIdTextBox.Text.Trim(),
                    dateTimePicker1.Value
                );
                MessageBox.Show("Kullanıcı başarıyla güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteProfile_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
            "Profil Silme İşlemini İptal Etmek İstiyor Musunuz ?",
            "Profil Silme Onayı",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        );

            if (result == DialogResult.No)
            {
                MessageBox.Show("Silme İşlemi İptal Edildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sqlManager.deleteUserProfile(this.uInfo.getId());
            MessageBox.Show("Profil Başarıyla Silindi !", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            callMethodFromMainFormClass(null,null);
            Dispose();

        }

        private void userAuthChoose_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

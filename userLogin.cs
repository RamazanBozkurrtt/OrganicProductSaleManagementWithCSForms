using OrganicProductSaleManagementV2._0.Items;
using OrganicProductSaleManagementV2._0.SqlTransactions.userLoginTransactions;
using OrganicProductSaleManagementV3.SqlTransactions;
using System;

using System.Drawing;
using System.Windows.Forms;

namespace OrganicProductSaleManagementV3
{
    public partial class userLogin : Form
    {
        private orProSaleManager sqlManager = new currentDatabase().getCurrentDatabaseForTransaction();
        userInfo uInfo;
        Boolean isHidden = true;
        
        public userInfo getUser()
        {
            return this.uInfo;
        }

        public userLogin()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {

            this.uInfo = null;

            if (string.IsNullOrWhiteSpace(userNameField.Text) || string.IsNullOrWhiteSpace(passwordField.Text))
            {
                MessageBox.Show("Kullanıcı adı ve şifre boş olamaz.", "Hata!");
            }
            else
            {
                this.uInfo = this.sqlManager.userLogin(userNameField.Text, passwordField.Text);

                if (this.uInfo == null)
                {
                    MessageBox.Show("Kullanıcı Adı veya Şifre Yanlış", "Hata!");
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
        }

        private void userLogin_Load(object sender, EventArgs e)
        {
            hideAndShow.Image = global::OrganicProductSaleManagementV3.Properties.Resources.newHide30;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
        }

        private void hideAndShow_Click_1(object sender, EventArgs e)
        {
            if (isHidden == true)
            {
                isHidden = false;
                hideAndShow.Image = global::OrganicProductSaleManagementV3.Properties.Resources.newHide30;
                passwordField.UseSystemPasswordChar = false;
                this.passwordField.BackColor = Color.White;

            }
            else if (isHidden == false)
            {
                isHidden = true;
                hideAndShow.Image = global::OrganicProductSaleManagementV3.Properties.Resources.newShow30;
                passwordField.UseSystemPasswordChar = true;
                this.passwordField.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(255)))), ((int)(((byte)(204)))));
            }
        }
    }
}

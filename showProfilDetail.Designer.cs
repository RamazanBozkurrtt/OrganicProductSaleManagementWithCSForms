namespace OrganicProductSaleManagementV3
{
    partial class showProfilDetail
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.userAuthChoose = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.userBirthDate = new System.Windows.Forms.Label();
            this.userIdTextBox = new System.Windows.Forms.TextBox();
            this.selectPictureButton = new System.Windows.Forms.Button();
            this.phoneLabel = new System.Windows.Forms.Label();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.phoneTextBox = new System.Windows.Forms.TextBox();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.userID = new System.Windows.Forms.Label();
            this.authLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.updateButton = new System.Windows.Forms.Button();
            this.deleteProfile = new System.Windows.Forms.Button();
            this.profilPicture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.profilPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // userAuthChoose
            // 
            this.userAuthChoose.FormattingEnabled = true;
            this.userAuthChoose.Location = new System.Drawing.Point(413, 107);
            this.userAuthChoose.Name = "userAuthChoose";
            this.userAuthChoose.Size = new System.Drawing.Size(113, 24);
            this.userAuthChoose.TabIndex = 0;
            this.userAuthChoose.SelectedIndexChanged += new System.EventHandler(this.userAuthChoose_SelectedIndexChanged);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Enabled = false;
            this.dateTimePicker1.Location = new System.Drawing.Point(413, 183);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 22);
            this.dateTimePicker1.TabIndex = 29;
            // 
            // userBirthDate
            // 
            this.userBirthDate.AutoSize = true;
            this.userBirthDate.Location = new System.Drawing.Point(303, 183);
            this.userBirthDate.Name = "userBirthDate";
            this.userBirthDate.Size = new System.Drawing.Size(91, 16);
            this.userBirthDate.TabIndex = 28;
            this.userBirthDate.Text = "Doğum Günü :";
            // 
            // userIdTextBox
            // 
            this.userIdTextBox.Enabled = false;
            this.userIdTextBox.Location = new System.Drawing.Point(145, 180);
            this.userIdTextBox.Name = "userIdTextBox";
            this.userIdTextBox.Size = new System.Drawing.Size(113, 22);
            this.userIdTextBox.TabIndex = 27;
            // 
            // selectPictureButton
            // 
            this.selectPictureButton.Enabled = false;
            this.selectPictureButton.Location = new System.Drawing.Point(664, 232);
            this.selectPictureButton.Name = "selectPictureButton";
            this.selectPictureButton.Size = new System.Drawing.Size(140, 34);
            this.selectPictureButton.TabIndex = 26;
            this.selectPictureButton.Text = "Resim Seçin";
            this.selectPictureButton.UseVisualStyleBackColor = true;
            this.selectPictureButton.Click += new System.EventHandler(this.selectPictureButton_Click);
            // 
            // phoneLabel
            // 
            this.phoneLabel.AutoSize = true;
            this.phoneLabel.Location = new System.Drawing.Point(54, 104);
            this.phoneLabel.Name = "phoneLabel";
            this.phoneLabel.Size = new System.Drawing.Size(83, 16);
            this.phoneLabel.TabIndex = 24;
            this.phoneLabel.Text = "Telefon No : ";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Enabled = false;
            this.passwordTextBox.Location = new System.Drawing.Point(413, 29);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(113, 22);
            this.passwordTextBox.TabIndex = 23;
            // 
            // phoneTextBox
            // 
            this.phoneTextBox.Enabled = false;
            this.phoneTextBox.Location = new System.Drawing.Point(145, 104);
            this.phoneTextBox.Name = "phoneTextBox";
            this.phoneTextBox.Size = new System.Drawing.Size(113, 22);
            this.phoneTextBox.TabIndex = 21;
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(354, 32);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(40, 16);
            this.passwordLabel.TabIndex = 20;
            this.passwordLabel.Text = "Şifre :";
            // 
            // userID
            // 
            this.userID.AutoSize = true;
            this.userID.Location = new System.Drawing.Point(47, 183);
            this.userID.Name = "userID";
            this.userID.Size = new System.Drawing.Size(90, 16);
            this.userID.TabIndex = 19;
            this.userID.Text = "TC Kimlik No :";
            // 
            // authLabel
            // 
            this.authLabel.AutoSize = true;
            this.authLabel.Location = new System.Drawing.Point(303, 110);
            this.authLabel.Name = "authLabel";
            this.authLabel.Size = new System.Drawing.Size(90, 16);
            this.authLabel.TabIndex = 18;
            this.authLabel.Text = "Yetki Düzeyi : ";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Enabled = false;
            this.nameTextBox.Location = new System.Drawing.Point(145, 29);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(113, 22);
            this.nameTextBox.TabIndex = 17;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(97, 32);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(40, 16);
            this.nameLabel.TabIndex = 16;
            this.nameLabel.Text = "İsim : ";
            // 
            // updateButton
            // 
            this.updateButton.Enabled = false;
            this.updateButton.Location = new System.Drawing.Point(34, 289);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(159, 34);
            this.updateButton.TabIndex = 30;
            this.updateButton.Text = "Profili Güncelle";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // deleteProfile
            // 
            this.deleteProfile.Enabled = false;
            this.deleteProfile.Location = new System.Drawing.Point(235, 289);
            this.deleteProfile.Name = "deleteProfile";
            this.deleteProfile.Size = new System.Drawing.Size(159, 34);
            this.deleteProfile.TabIndex = 31;
            this.deleteProfile.Text = "Profili Sil !";
            this.deleteProfile.UseVisualStyleBackColor = true;
            this.deleteProfile.Click += new System.EventHandler(this.deleteProfile_Click);
            // 
            // profilPicture
            // 
            this.profilPicture.Location = new System.Drawing.Point(651, 29);
            this.profilPicture.Name = "profilPicture";
            this.profilPicture.Size = new System.Drawing.Size(173, 180);
            this.profilPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.profilPicture.TabIndex = 25;
            this.profilPicture.TabStop = false;
            // 
            // showProfilDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 347);
            this.Controls.Add(this.deleteProfile);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.userBirthDate);
            this.Controls.Add(this.userIdTextBox);
            this.Controls.Add(this.selectPictureButton);
            this.Controls.Add(this.profilPicture);
            this.Controls.Add(this.phoneLabel);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.phoneTextBox);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.userID);
            this.Controls.Add(this.authLabel);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.userAuthChoose);
            this.Name = "showProfilDetail";
            this.Text = "Profil Detayı";
            this.Load += new System.EventHandler(this.showProfilDetail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.profilPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox userAuthChoose;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label userBirthDate;
        private System.Windows.Forms.TextBox userIdTextBox;
        private System.Windows.Forms.Button selectPictureButton;
        private System.Windows.Forms.PictureBox profilPicture;
        private System.Windows.Forms.Label phoneLabel;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.TextBox phoneTextBox;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Label userID;
        private System.Windows.Forms.Label authLabel;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Button deleteProfile;
    }
}
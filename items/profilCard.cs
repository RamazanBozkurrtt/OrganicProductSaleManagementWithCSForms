using OrganicProductSaleManagementV2._0.Items;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace OrganicProductSaleManagementV3.items
{
    public class profilCard : Panel
    {
        private userInfo userInfo;

        private Panel innerPanel;
        private Panel decorationPanelRightDown;

        private PictureBox profilPicture;

        private Button profilDetailButton;

        private Label nameLabel;

        private Label authorityLabel;

        private bool isAdmin;

        private Action<object, KeyEventArgs> method;

        private showProfilDetail sD;

        public profilCard(userInfo _uInfo, int xPositiyon, int yPosition,bool _isAdmin, Action<object, KeyEventArgs> _method)
        {
            this.isAdmin = _isAdmin;
            this.userInfo = _uInfo;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(223)))), ((int)(((byte)(215)))));
            this.Location = new System.Drawing.Point(xPositiyon, yPosition);
            this.Name = "panel1";
            this.Size = new System.Drawing.Size(560, 150);
            this.TabIndex = 0;
            this.method = _method;
            setSettings();
            setLabelSettings();
            setPictureSettings();
            setButtonSettings();
            addObjects();
        }

        private void setSettings()
        {

            this.innerPanel = new Panel();
            this.innerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.innerPanel.Location = new System.Drawing.Point(4, 4);
            this.innerPanel.Name = "panel5";
            this.innerPanel.Size = new System.Drawing.Size(547, 137);
            this.innerPanel.TabIndex = 0;
            this.Controls.Add(this.innerPanel);

            this.decorationPanelRightDown = new Panel();
            this.decorationPanelRightDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(223)))), ((int)(((byte)(215)))));
            this.decorationPanelRightDown.Location = new System.Drawing.Point(370, 50);
            this.decorationPanelRightDown.Name = "panel3";
            this.decorationPanelRightDown.Size = new System.Drawing.Size(136, 56);
            this.decorationPanelRightDown.TabIndex = 22;

        }
        private void setPictureSettings()
        {
            Image ımg;
            using (MemoryStream ms = new MemoryStream(this.userInfo.getImageData()))
            {
                ımg = Image.FromStream(ms);
            }

            this.profilPicture = new PictureBox();
            this.profilPicture.Location = new System.Drawing.Point(4, 4);
            this.profilPicture.Name = "pictureBox2";
            this.profilPicture.Size = new System.Drawing.Size(106, 128);
            this.profilPicture.TabIndex = 7;
            this.profilPicture.TabStop = false;
            this.profilPicture.Image = ımg;
            this.profilPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
        }

        private void setLabelSettings()
        {
            this.nameLabel = new Label();
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.nameLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.nameLabel.Location = new System.Drawing.Point(140, 20);
            this.nameLabel.Name = "label1";
            this.nameLabel.Size = new System.Drawing.Size(50, 15);
            this.nameLabel.TabIndex = 19;
            this.nameLabel.Text = "Kullanıcı Adı : " + this.userInfo.getUserName();
            this.nameLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.authorityLabel = new Label();
            this.authorityLabel.AutoSize = true;
            this.authorityLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.authorityLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.authorityLabel.Location = new System.Drawing.Point(140, 86);
            this.authorityLabel.Name = "label4";
            this.authorityLabel.Size = new System.Drawing.Size(50, 15);
            this.authorityLabel.TabIndex = 19;
            this.authorityLabel.Text = "Yetki Düzeyi : " +this.userInfo.getAuthorityLevel();
            this.authorityLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

        }

        private void setButtonSettings()
        {
            this.profilDetailButton = new Button();
            this.profilDetailButton.BackColor = System.Drawing.Color.White;
            this.profilDetailButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(223)))), ((int)(((byte)(215)))));
            this.profilDetailButton.FlatAppearance.BorderSize = 0;
            this.profilDetailButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(223)))), ((int)(((byte)(215)))));
            this.profilDetailButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(223)))), ((int)(((byte)(215)))));
            this.profilDetailButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.profilDetailButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(223)))), ((int)(((byte)(215)))));
            this.profilDetailButton.Location = new System.Drawing.Point(8, 8);
            this.profilDetailButton.Name = "button2";
            this.profilDetailButton.Size = new System.Drawing.Size(120, 40);
            this.profilDetailButton.TabIndex = 19;
            this.profilDetailButton.Text = "Profil Detayı";
            this.profilDetailButton.UseVisualStyleBackColor = false;
            this.profilDetailButton.MouseLeave += new System.EventHandler(this.profilDetailButton_MouseLeave);
            this.profilDetailButton.MouseEnter += new System.EventHandler(this.profilDetailButton_MouseEnter);
            this.profilDetailButton.Click += new System.EventHandler(this.profilDetailButton_Click);

        }

        private void addObjects()
        {
            innerPanel.Controls.Add(decorationPanelRightDown);
            innerPanel.Controls.Add(profilPicture);
            innerPanel.Controls.Add(nameLabel);
            innerPanel.Controls.Add(authorityLabel);

            decorationPanelRightDown.Controls.Add(profilDetailButton);
        }

        private void profilDetailButton_Click(object sender, EventArgs e)
        {

            if(this.sD == null)
            {
                sD = new showProfilDetail(isAdmin, userInfo, this.method);
                sD.Show();
            }
            else
            {
                this.sD.Dispose();
                sD = new showProfilDetail(isAdmin, userInfo, this.method);
                sD.Show();
            }

        }

        private void profilDetailButton_MouseEnter(object sender, EventArgs e)
        {

        }

        private void profilDetailButton_MouseLeave(object sender, EventArgs e)
        {

        }

    }
}

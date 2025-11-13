using ElipseToolDemo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrganicProductSaleManagementV3.items
{
    public class itemCard : Panel
    {
        productTransactionsPage pPage;
        private setAmountOfProduct setAmountOfProduct;
        private productInfo pInfo;
        private int itemSum;
        private bool isAdmin;

        private System.Windows.Forms.Label pNameLabel;
        private System.Windows.Forms.Label pStockCodeLabel;
        private System.Windows.Forms.Label pPrice;
        private System.Windows.Forms.Label pShelfLifeLabel;
        private System.Windows.Forms.Label pTotalAmountOfStockLabel;

        private System.Windows.Forms.Button changeStockAmountButton;
        private System.Windows.Forms.Button deleteProductButton;
        private System.Windows.Forms.Button updateProductButton;

        private System.Windows.Forms.PictureBox profilePicture;

        private ButonKavis updateButtonKavis;
        private ButonKavis deleteButtonKavis;
        private ButonKavis changeStockAmountButtonKavis;

        private System.Windows.Forms.Panel designPanel;

        private Action<object, KeyEventArgs> method;


        public itemCard(int positionX, int positionY, productInfo _pInfo, int _itemSum, bool _isAdmin, Action<object, KeyEventArgs> _method) 
        {
            this.method = _method;
            this.isAdmin = _isAdmin;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
            this.Location = new System.Drawing.Point(positionX, positionY);
            this.Size = new System.Drawing.Size(950,122);
            this.pInfo = _pInfo;
            this.itemSum = _itemSum;
            setObjects();
            updateKavisDesigns();
            updateProfilePicture();
            updateButtonsSettings();
            updateLabelsSettings();
            updateDesignPanel();
            setUserAuthorityAccesses();


        }


        private void setUserAuthorityAccesses()
        {
            if (isAdmin == false)
            {
                this.deleteProductButton.ForeColor = Color.Red;
                this.updateProductButton.ForeColor = Color.Red;
                changeStockAmountButton.Enabled = true;
                deleteProductButton.Enabled = false;
                updateProductButton.Enabled = false;
                updateProductButton.Cursor = Cursors.No;
                deleteProductButton.Cursor = Cursors.No;

            }
        }

        private void setObjects()
        {
            this.designPanel = new System.Windows.Forms.Panel();

            this.Controls.Add(this.designPanel);



            pNameLabel = new System.Windows.Forms.Label();
            pStockCodeLabel = new System.Windows.Forms.Label();
            pPrice = new System.Windows.Forms.Label();
            pShelfLifeLabel = new System.Windows.Forms.Label();
            pTotalAmountOfStockLabel = new System.Windows.Forms.Label();

            designPanel.Controls.Add(pNameLabel);
            designPanel.Controls.Add(pStockCodeLabel);
            designPanel.Controls.Add(pPrice);
            designPanel.Controls.Add(pShelfLifeLabel);
            designPanel.Controls.Add(pTotalAmountOfStockLabel);


            profilePicture = new System.Windows.Forms.PictureBox();

            designPanel.Controls.Add(profilePicture);

            updateButtonKavis = new ButonKavis();
            deleteButtonKavis = new ButonKavis();
            changeStockAmountButtonKavis = new ButonKavis();

            changeStockAmountButton = new System.Windows.Forms.Button();
            updateProductButton = new System.Windows.Forms.Button();
            deleteProductButton = new System.Windows.Forms.Button();

            designPanel.Controls.Add(changeStockAmountButton);
            designPanel.Controls.Add(updateProductButton);
            designPanel.Controls.Add(deleteProductButton);



        }

        private void updateKavisDesigns()
        {
            updateButtonKavis.TargetControl = updateProductButton;
            updateButtonKavis.CornerRadius = 45;

            deleteButtonKavis.TargetControl = deleteProductButton;
            deleteButtonKavis.CornerRadius = 45;

            changeStockAmountButtonKavis.TargetControl = changeStockAmountButton;
            changeStockAmountButtonKavis.CornerRadius = 45;
        }

        private void updateDesignPanel()
        {
            this.designPanel.Location = new System.Drawing.Point(3, 3);
            this.designPanel.Size = new System.Drawing.Size(944,116);
            this.designPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
        }

        private void updateProfilePicture()
        {
            Image img;
            using (MemoryStream ms = new MemoryStream(this.pInfo.getProductTypeClass().getImageData()))
            {
                img = Image.FromStream(ms);
            }

            this.profilePicture.Image = img;
            this.profilePicture.Name = "profilePicture";
            this.profilePicture.Location = new System.Drawing.Point(3,3);
            this.profilePicture.Size = new System.Drawing.Size(120,112);
            this.profilePicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;

        }

        private void updateButtonsSettings()
        {

            this.updateProductButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
            this.updateProductButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
            this.updateProductButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
            this.updateProductButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
            this.updateProductButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.updateProductButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.updateProductButton.ForeColor = System.Drawing.Color.White;
            this.updateProductButton.Location = new System.Drawing.Point(555, 8);
            this.updateProductButton.Name = "updateProduct";
            this.updateProductButton.Size = new System.Drawing.Size(170, 35);
            this.updateProductButton.TabIndex = 5;
            this.updateProductButton.Text = "Ürün Güncelle";
            this.updateProductButton.UseVisualStyleBackColor = false;
            this.updateProductButton.Click += new System.EventHandler(this.updateProduct_Click);
            this.updateProductButton.MouseEnter += new System.EventHandler(this.updateProduct_MouseEnter);
            this.updateProductButton.MouseLeave += new System.EventHandler(this.updateProduct_MouseLeave);


            this.deleteProductButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
            this.deleteProductButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
            this.deleteProductButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(181)))), ((int)(((byte)(44)))));
            this.deleteProductButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(181)))), ((int)(((byte)(44)))));
            this.deleteProductButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteProductButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.deleteProductButton.ForeColor = System.Drawing.Color.White;
            this.deleteProductButton.Location = new System.Drawing.Point(750, 74);
            this.deleteProductButton.Name = "deleteProduct";
            this.deleteProductButton.Size = new System.Drawing.Size(170, 35);
            this.deleteProductButton.TabIndex = 4;
            this.deleteProductButton.Text = "Ürün Sil";
            this.deleteProductButton.UseVisualStyleBackColor = false;
            this.deleteProductButton.Click += new System.EventHandler(this.deleteProduct_Click);
            this.deleteProductButton.MouseEnter += new System.EventHandler(this.deleteProduct_MouseEnter);
            this.deleteProductButton.MouseLeave += new System.EventHandler(this.deleteProduct_MouseLeave);


            this.changeStockAmountButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
            this.changeStockAmountButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
            this.changeStockAmountButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(181)))), ((int)(((byte)(44)))));
            this.changeStockAmountButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(181)))), ((int)(((byte)(44)))));
            this.changeStockAmountButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.changeStockAmountButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.changeStockAmountButton.ForeColor = System.Drawing.Color.White;
            this.changeStockAmountButton.Location = new System.Drawing.Point(555, 74);
            this.changeStockAmountButton.Name = "deleteProduct";
            this.changeStockAmountButton.Size = new System.Drawing.Size(170, 35);
            this.changeStockAmountButton.TabIndex = 4;
            this.changeStockAmountButton.Text = "Stok Güncelle";
            this.changeStockAmountButton.UseVisualStyleBackColor = false;
            this.changeStockAmountButton.Click += new System.EventHandler(this.changeAmountOfItemButton_Click);
            this.changeStockAmountButton.MouseEnter += new System.EventHandler(this.changeAmountOfItemButton_MouseEnter);
            this.changeStockAmountButton.MouseLeave += new System.EventHandler(this.changeAmountOfItemButton_MouseLeave);




        }

        private void updateLabelsSettings()
        {
            pNameLabel.Text = "Ürün İsmi : " + pInfo.getProductTypeClass().getProductName().ToString().Trim();
            pNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            pNameLabel.Location = new System.Drawing.Point(170, 16);
            pNameLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pNameLabel.AutoSize = true;

            string maddeKodu = pInfo.getProductTypeClass().getProductStockCode().ToString().Trim();
            pStockCodeLabel.Text = "Madde Kodu : " + maddeKodu;
            pStockCodeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            pStockCodeLabel.ForeColor = System.Drawing.Color.Black;
            pStockCodeLabel.Location = new System.Drawing.Point(170, 49);
            pStockCodeLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pStockCodeLabel.AutoSize = true;

            pPrice.Text = "Ürün Fiyatı : " + this.pInfo.getProductTypeClass().getPrice().ToString().Trim();
            pPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            pPrice.ForeColor = System.Drawing.Color.Black;
            pPrice.Location = new System.Drawing.Point(170, 83);
            pPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pPrice.AutoSize = true;

            pShelfLifeLabel.Text = "Ortalam Raf Ömrü : " + this.pInfo.getProductTypeClass().getProductAverageShelfLife().ToString();
            pShelfLifeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            pShelfLifeLabel.ForeColor = System.Drawing.Color.Black;
            pShelfLifeLabel.Location = new System.Drawing.Point(375, 16);
            pShelfLifeLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pShelfLifeLabel.AutoSize = true;

            pTotalAmountOfStockLabel.Text = "Toplam Stok : " + itemSum.ToString();
            pTotalAmountOfStockLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            pTotalAmountOfStockLabel.ForeColor = System.Drawing.Color.Black;
            pTotalAmountOfStockLabel.Location = new System.Drawing.Point(375, 49);
            pTotalAmountOfStockLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pTotalAmountOfStockLabel.AutoSize = true;

            Console.WriteLine(pInfo.getProductTypeClass().getProductName());

        }





        private void updateProduct_Click(object sender, EventArgs e)
        {
            if(this.pPage == null)
            {
                this.pPage = new productTransactionsPage(this.isAdmin, pInfo.getId());
                this.pPage.Show();
            }
            else
            {
                this.pPage.Dispose();
                this.pPage = new productTransactionsPage(this.isAdmin, pInfo.getId());
                this.pPage.Show();
            }
            
        }

        private void updateProduct_MouseEnter(object sender, EventArgs e)
        {
            this.updateProductButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(227)))), ((int)(((byte)(140)))));
            this.updateProductButton.ForeColor = System.Drawing.Color.White;
            this.updateProductButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.updateProductButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(181)))), ((int)(((byte)(44)))));
            this.updateProductButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(227)))), ((int)(((byte)(140)))));
        }

        private void updateProduct_MouseLeave(object sender, EventArgs e)
        {
            this.updateProductButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
            this.updateProductButton.ForeColor = Color.White;
            this.updateProductButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.updateProductButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
            this.updateProductButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
        }

        private void deleteProduct_Click(object sender, EventArgs e)
        {
            if (this.pPage == null)
            {
                this.pPage = new productTransactionsPage(this.isAdmin, pInfo.getId());
                this.pPage.Show();
            }
            else
            {
                this.pPage.Dispose();
                this.pPage = new productTransactionsPage(this.isAdmin, pInfo.getId());
                this.pPage.Show();
            }
            
        }

        private void deleteProduct_MouseEnter(object sender, EventArgs e)
        {
            this.deleteProductButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(227)))), ((int)(((byte)(140)))));
            this.deleteProductButton.ForeColor = System.Drawing.Color.White;
            this.deleteProductButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.deleteProductButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(181)))), ((int)(((byte)(44)))));
            this.deleteProductButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(227)))), ((int)(((byte)(140)))));
        }

        private void deleteProduct_MouseLeave(object sender, EventArgs e)
        {
            this.deleteProductButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
            this.deleteProductButton.ForeColor = Color.White;
            this.deleteProductButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.deleteProductButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
            this.deleteProductButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
        }

        private void changeAmountOfItemButton_Click(object sender, EventArgs e)
        {

            if(setAmountOfProduct == null)
            {
                setAmountOfProduct = new setAmountOfProduct(this.pInfo,method);
                setAmountOfProduct.Show();
            }
            else
            {
                setAmountOfProduct.Dispose();
                setAmountOfProduct = new setAmountOfProduct(this.pInfo, method);
                setAmountOfProduct.Show();
            }
            
        }

        private void changeAmountOfItemButton_MouseEnter(object sender, EventArgs e)
        {
            this.changeStockAmountButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(227)))), ((int)(((byte)(140)))));
            this.changeStockAmountButton.ForeColor = System.Drawing.Color.White;
            this.changeStockAmountButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.changeStockAmountButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(181)))), ((int)(((byte)(44)))));
            this.changeStockAmountButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(227)))), ((int)(((byte)(140)))));
        }

        private void changeAmountOfItemButton_MouseLeave(object sender, EventArgs e)
        {
            this.changeStockAmountButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
            this.changeStockAmountButton.ForeColor = Color.White;
            this.changeStockAmountButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.changeStockAmountButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
            this.changeStockAmountButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
        }

    }
}

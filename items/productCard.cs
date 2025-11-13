using ElipseToolDemo;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace OrganicProductSaleManagementV3.items
{
    public class productCard : Panel
    {
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button decreaseButton;
        private System.Windows.Forms.Button increaseButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Label countLabel;
        private ButonKavis mainKavis;
        private ButonKavis innerKavis;
        private ButonKavis pictureKavis;
        private System.Windows.Forms.Panel innerPanel;

        

        private productInfo pTypeInfo;
        private int stockAmount;
        private DataGridView dataTableForProducts;

        private EventHandler buttonFirst;
        private EventHandler buttonSecond;

        private Action<int> method;

        public productCard(int positionX, int positionY, productInfo _pTypeInfo,int _stockAmount,DataGridView _dataTableForProducts, Action<int> _method)
        {
            method = _method;
            dataTableForProducts = _dataTableForProducts;
            buttonFirst = addButtonClick;
            buttonSecond = addButtonClickTwo;
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.decreaseButton = new System.Windows.Forms.Button();
            this.increaseButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.countLabel = new System.Windows.Forms.Label();
            this.pTypeInfo = _pTypeInfo;
            this.stockAmount = _stockAmount;

            this.innerPanel = new System.Windows.Forms.Panel();
            this.innerKavis = new ButonKavis();
            this.innerKavis.CornerRadius = 45;
            this.innerKavis.TargetControl = this.innerPanel;

            this.mainKavis = new ButonKavis();
            this.mainKavis.CornerRadius = 45;
            this.mainKavis.TargetControl = this;

            this.pictureKavis = new ButonKavis();
            this.pictureKavis.CornerRadius = 45;
            this.pictureKavis.TargetControl = pictureBox1;


            this.innerPanel.Size = new System.Drawing.Size(190, 250);
            this.innerPanel.Location = new System.Drawing.Point(5, 5);
            this.innerPanel.Parent = this;
            this.innerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));


            setButtonsOptions();
            setPictureBoxOptions();
            setLabelOptions();

            this.Size = new System.Drawing.Size(200, 260);
            this.Location = new System.Drawing.Point(positionX, positionY);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
            this.innerPanel.Controls.Add(this.decreaseButton);
            this.innerPanel.Controls.Add(this.increaseButton);
            this.innerPanel.Controls.Add(this.addButton);
            this.innerPanel.Controls.Add(this.countLabel);
            this.innerPanel.Controls.Add(this.label3);
            this.innerPanel.Controls.Add(this.label2);
            this.innerPanel.Controls.Add(this.label1);
            this.innerPanel.Controls.Add(this.pictureBox1);

            this.Controls.Add(innerPanel);
            
            

            this.Click += new System.EventHandler(this.click);

            this.MouseEnter += new System.EventHandler(this.mouseEnter);
            this.MouseLeave += new System.EventHandler(this.mouseLeave);

            this.innerPanel.MouseEnter += new System.EventHandler(this.innerMouseEnter);
            this.innerPanel.MouseLeave += new System.EventHandler(this.innerMouseLeave);

            this.label1.MouseEnter += new System.EventHandler(this.mouseEnter);
            this.label1.MouseLeave += new System.EventHandler(this.innerMouseLeave);

            this.label2.MouseEnter += new System.EventHandler(this.mouseEnter);
            this.label2.MouseLeave += new System.EventHandler(this.innerMouseLeave);

            this.label3.MouseEnter += new System.EventHandler(this.mouseEnter);
            this.label3.MouseLeave += new System.EventHandler(this.innerMouseLeave);

            this.pictureBox1.MouseEnter += new System.EventHandler(this.mouseEnter);
            this.pictureBox1.MouseLeave += new System.EventHandler(this.innerMouseLeave);



            this.addButton.MouseEnter += new System.EventHandler(this.mouseEnter);
            this.addButton.MouseLeave += new System.EventHandler(this.innerMouseLeave);

            this.decreaseButton.MouseEnter += new System.EventHandler(this.mouseEnter);
            this.decreaseButton.MouseLeave += new System.EventHandler(this.innerMouseLeave);

            this.increaseButton.MouseEnter += new System.EventHandler(this.mouseEnter);
            this.increaseButton.MouseLeave += new System.EventHandler(this.innerMouseLeave);

            setPreviousSetting();

        }

        private void callMethodFromMainFormClass(int value)
        {
            method(value);
        }

        private void setPreviousSetting()
        {
            foreach (DataGridViewRow row in dataTableForProducts.Rows)
            {
                if (row.Cells[0].Value.ToString().Trim().Equals(pTypeInfo.getProductTypeClass().getProductName().Trim()))
                {
                    label3.Text = (Convert.ToInt32(stockAmount.ToString()) - Convert.ToInt32(row.Cells[2].Value.ToString().Trim())).ToString();
                    increaseButton.Enabled = false;
                    decreaseButton.Enabled = false;
                    addButton.Click -= addButtonClick;
                    addButton.Click += addButtonClickTwo;
                    addButton.Text = "İptal Et";
                    countLabel.Text = row.Cells[2].Value.ToString().Trim();
                }
            }
        }

        private void click(object sender, EventArgs e)
        {

        }

        private void mouseLeave(object sender, EventArgs e)
        {
            this.innerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
        }

        private void mouseEnter(object sender, EventArgs e)
        {
            this.innerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
        }

        private void innerMouseEnter(object sender, EventArgs e)
        {
            this.innerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
        }
        private void innerMouseLeave(object sender, EventArgs e) 
        {
            this.innerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
        }


        private void setButtonsOptions()
        {


            this.addButton.Location = new System.Drawing.Point(118, 217);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(69, 23);
            this.addButton.TabIndex = 3;
            this.addButton.Text = "Ekle";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButtonClick);

            this.increaseButton.Location = new System.Drawing.Point(67, 217);
            this.increaseButton.Name = "increaseButton";
            this.increaseButton.Size = new System.Drawing.Size(37, 23);
            this.increaseButton.TabIndex = 1;
            this.increaseButton.Text = "+";
            this.increaseButton.UseVisualStyleBackColor = true;
            this.increaseButton.Click += new System.EventHandler(this.increaseButtonClick);

            this.decreaseButton.Location = new System.Drawing.Point(6, 217);
            this.decreaseButton.Name = "decreaseButton";
            this.decreaseButton.Size = new System.Drawing.Size(37, 23);
            this.decreaseButton.TabIndex = 0;
            this.decreaseButton.Text = "-";
            this.decreaseButton.UseVisualStyleBackColor = true;
            this.decreaseButton.Click += new System.EventHandler(this.decreaseButtonClick);

        }

        private void addButtonClick(object sender, EventArgs e)
        {
            if (Convert.ToInt32(countLabel.Text) == 0)
            {
                MessageBox.Show("Lütfen Önce Ürün Adedi Belirleyiniz !");
                return;
            }
            this.label3.Text = "Stok Durumu :" + (this.stockAmount - Convert.ToInt32(countLabel.Text)).ToString();
            this.addButton.Text = "İptal Et";
            this.addButton.Click -= addButtonClick;
            this.addButton.Click += addButtonClickTwo;
            this.increaseButton.Enabled = false;
            this.decreaseButton.Enabled = false;
            dataTableForProducts.Rows.Add(pTypeInfo.getProductTypeClass().getProductName().ToString().Trim(),
                                          pTypeInfo.getProductTypeClass().getPrice().ToString(),
                                          Convert.ToInt32(countLabel.Text),
                                          Convert.ToInt32(countLabel.Text) * pTypeInfo.getProductTypeClass().getPrice(),
                                          pTypeInfo.getId(),
                                          pTypeInfo.getProductTypeClass().getId());

            callMethodFromMainFormClass(Convert.ToInt32(countLabel.Text) * pTypeInfo.getProductTypeClass().getPrice());

        }

        private void addButtonClickTwo(object sender, EventArgs e)
        {
            callMethodFromMainFormClass(-1 * Convert.ToInt32(countLabel.Text.Trim()) * pTypeInfo.getProductTypeClass().getPrice());
            this.addButton.Text = "Ekle";
            this.countLabel.Text = "0";
            this.increaseButton.Enabled = true;
            this.decreaseButton.Enabled = true;
            this.label3.Text = "Stok Durumu :" + stockAmount.ToString();
            this.addButton.Click -= addButtonClickTwo;
            this.addButton.Click += addButtonClick;
            

            foreach (DataGridViewRow row in dataTableForProducts.Rows)
            {
                if (row.Cells[0].Value.ToString().Trim() == pTypeInfo.getProductTypeClass().getProductName().ToString().Trim())
                {
                    dataTableForProducts.Rows.Remove(row);
                }
            }
        }

        private void increaseButtonClick(object sender, EventArgs e)
        {

            if (Convert.ToInt32(countLabel.Text) < this.stockAmount)
            {
                countLabel.Text = (Convert.ToInt32(countLabel.Text) + 1).ToString();
            }
            
            
        }

        private void decreaseButtonClick(object sender, EventArgs e)
        {
            if (Convert.ToInt32(countLabel.Text) > 0)
            {
                countLabel.Text = (Convert.ToInt32(countLabel.Text) - 1).ToString();
            }
            
        }

        private void setPictureBoxOptions()
        {
            Image ımg;
            using (MemoryStream ms = new MemoryStream(this.pTypeInfo.getProductTypeClass().getImageData()))
            {
                ımg = Image.FromStream(ms);
            }

            this.pictureBox1.Location = new System.Drawing.Point(12, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(165, 130);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Image = ımg;
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
        }   

        private void setLabelOptions()
        {
            countLabel.AutoSize = true;
            countLabel.Location = new System.Drawing.Point(46, 217);
            countLabel.Name = "countLabel";
            countLabel.Size = new System.Drawing.Size(14, 16);
            countLabel.TabIndex = 2;
            countLabel.Text = "0";
            countLabel.AutoSize = true;

            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(10, 144);
            label1.Name = "pName";
            label1.Size = new System.Drawing.Size(44, 16);
            label1.TabIndex = 1;
            label1.Text = "Ürün :" + pTypeInfo.getProductTypeClass().getProductName().ToString().Trim();
            label1.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            label1.ForeColor = System.Drawing.Color.Black;
            label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            label1.AutoSize = true;

            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(10, 169);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(44, 16);
            label2.TabIndex = 2;
            label2.Text = "Fiyatı : " + pTypeInfo.getProductTypeClass().getPrice().ToString() +" TL";
            label2.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            label2.ForeColor = System.Drawing.Color.Black;
            label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            label2.AutoSize = true;

            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(10, 194);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(44, 16);
            label3.TabIndex = 3;
            label3.Text = "Stok Durumu :" + this.stockAmount.ToString();
            label3.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            label3.ForeColor = System.Drawing.Color.Black;
            label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            label3.AutoSize = true;
        }




    }
}

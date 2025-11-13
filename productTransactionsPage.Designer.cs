namespace OrganicProductSaleManagementV3
{
    partial class productTransactionsPage
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
            this.dataTable = new System.Windows.Forms.DataGridView();
            this.deleteButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.uploadImageButton = new System.Windows.Forms.Button();
            this.confirmUpdateButton = new System.Windows.Forms.Button();
            this.productStockAmountLabel = new System.Windows.Forms.Label();
            this.productStockAmountTextField = new System.Windows.Forms.TextBox();
            this.imageHeightTextField = new System.Windows.Forms.TextBox();
            this.imageWidthTextField = new System.Windows.Forms.TextBox();
            this.imageHeight = new System.Windows.Forms.Label();
            this.imageWidth = new System.Windows.Forms.Label();
            this.productImage = new System.Windows.Forms.Label();
            this.imageBox = new System.Windows.Forms.PictureBox();
            this.productPriceTextField = new System.Windows.Forms.TextBox();
            this.productShelfLifeTextField = new System.Windows.Forms.TextBox();
            this.productStockCodeTextField = new System.Windows.Forms.TextBox();
            this.productPriceLable = new System.Windows.Forms.Label();
            this.productAverageShelfLifeLabel = new System.Windows.Forms.Label();
            this.productStockCodeLabel = new System.Windows.Forms.Label();
            this.productNameLabel = new System.Windows.Forms.Label();
            this.productNameTextField = new System.Windows.Forms.TextBox();
            this.updateButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.lblError = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // dataTable
            // 
            this.dataTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataTable.Location = new System.Drawing.Point(12, 240);
            this.dataTable.Name = "dataTable";
            this.dataTable.RowHeadersWidth = 51;
            this.dataTable.RowTemplate.Height = 24;
            this.dataTable.Size = new System.Drawing.Size(1149, 228);
            this.dataTable.TabIndex = 0;
            this.dataTable.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataTable_CellClick);
            // 
            // deleteButton
            // 
            this.deleteButton.Enabled = false;
            this.deleteButton.Location = new System.Drawing.Point(796, 145);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(172, 29);
            this.deleteButton.TabIndex = 1;
            this.deleteButton.Text = "Ürünü Sil";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.Controls.Add(this.uploadImageButton);
            this.panel1.Controls.Add(this.confirmUpdateButton);
            this.panel1.Controls.Add(this.productStockAmountLabel);
            this.panel1.Controls.Add(this.productStockAmountTextField);
            this.panel1.Controls.Add(this.deleteButton);
            this.panel1.Controls.Add(this.imageHeightTextField);
            this.panel1.Controls.Add(this.imageWidthTextField);
            this.panel1.Controls.Add(this.imageHeight);
            this.panel1.Controls.Add(this.imageWidth);
            this.panel1.Controls.Add(this.productImage);
            this.panel1.Controls.Add(this.imageBox);
            this.panel1.Controls.Add(this.productPriceTextField);
            this.panel1.Controls.Add(this.productShelfLifeTextField);
            this.panel1.Controls.Add(this.productStockCodeTextField);
            this.panel1.Controls.Add(this.productPriceLable);
            this.panel1.Controls.Add(this.productAverageShelfLifeLabel);
            this.panel1.Controls.Add(this.productStockCodeLabel);
            this.panel1.Controls.Add(this.productNameLabel);
            this.panel1.Controls.Add(this.productNameTextField);
            this.panel1.Location = new System.Drawing.Point(174, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(987, 221);
            this.panel1.TabIndex = 2;
            // 
            // uploadImageButton
            // 
            this.uploadImageButton.Enabled = false;
            this.uploadImageButton.Location = new System.Drawing.Point(482, 180);
            this.uploadImageButton.Name = "uploadImageButton";
            this.uploadImageButton.Size = new System.Drawing.Size(172, 29);
            this.uploadImageButton.TabIndex = 18;
            this.uploadImageButton.Text = "Resim Yükle";
            this.uploadImageButton.UseVisualStyleBackColor = true;
            this.uploadImageButton.Click += new System.EventHandler(this.uploadImageButton_Click);
            // 
            // confirmUpdateButton
            // 
            this.confirmUpdateButton.Enabled = false;
            this.confirmUpdateButton.Location = new System.Drawing.Point(796, 180);
            this.confirmUpdateButton.Name = "confirmUpdateButton";
            this.confirmUpdateButton.Size = new System.Drawing.Size(172, 29);
            this.confirmUpdateButton.TabIndex = 17;
            this.confirmUpdateButton.Text = "Güncelle !";
            this.confirmUpdateButton.UseVisualStyleBackColor = true;
            this.confirmUpdateButton.Click += new System.EventHandler(this.confirmUpdateButton_Click);
            // 
            // productStockAmountLabel
            // 
            this.productStockAmountLabel.AutoSize = true;
            this.productStockAmountLabel.Location = new System.Drawing.Point(45, 180);
            this.productStockAmountLabel.Name = "productStockAmountLabel";
            this.productStockAmountLabel.Size = new System.Drawing.Size(80, 16);
            this.productStockAmountLabel.TabIndex = 16;
            this.productStockAmountLabel.Text = "Ürün Adedi :";
            // 
            // productStockAmountTextField
            // 
            this.productStockAmountTextField.Enabled = false;
            this.productStockAmountTextField.Location = new System.Drawing.Point(143, 180);
            this.productStockAmountTextField.Name = "productStockAmountTextField";
            this.productStockAmountTextField.Size = new System.Drawing.Size(213, 22);
            this.productStockAmountTextField.TabIndex = 15;
            // 
            // imageHeightTextField
            // 
            this.imageHeightTextField.Enabled = false;
            this.imageHeightTextField.Location = new System.Drawing.Point(771, 50);
            this.imageHeightTextField.Name = "imageHeightTextField";
            this.imageHeightTextField.Size = new System.Drawing.Size(95, 22);
            this.imageHeightTextField.TabIndex = 14;
            // 
            // imageWidthTextField
            // 
            this.imageWidthTextField.Enabled = false;
            this.imageWidthTextField.Location = new System.Drawing.Point(771, 13);
            this.imageWidthTextField.Name = "imageWidthTextField";
            this.imageWidthTextField.Size = new System.Drawing.Size(95, 22);
            this.imageWidthTextField.TabIndex = 13;
            // 
            // imageHeight
            // 
            this.imageHeight.AutoSize = true;
            this.imageHeight.Location = new System.Drawing.Point(648, 53);
            this.imageHeight.Name = "imageHeight";
            this.imageHeight.Size = new System.Drawing.Size(117, 16);
            this.imageHeight.TabIndex = 12;
            this.imageHeight.Text = "Resim Yüksekliği :";
            // 
            // imageWidth
            // 
            this.imageWidth.AutoSize = true;
            this.imageWidth.Location = new System.Drawing.Point(658, 13);
            this.imageWidth.Name = "imageWidth";
            this.imageWidth.Size = new System.Drawing.Size(107, 16);
            this.imageWidth.TabIndex = 11;
            this.imageWidth.Text = "Resim Genişliği :";
            // 
            // productImage
            // 
            this.productImage.AutoSize = true;
            this.productImage.Location = new System.Drawing.Point(393, 16);
            this.productImage.Name = "productImage";
            this.productImage.Size = new System.Drawing.Size(83, 16);
            this.productImage.TabIndex = 10;
            this.productImage.Text = "Ürün Resmi :";
            // 
            // imageBox
            // 
            this.imageBox.Location = new System.Drawing.Point(482, 13);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(150, 132);
            this.imageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imageBox.TabIndex = 9;
            this.imageBox.TabStop = false;
            this.imageBox.Resize += new System.EventHandler(this.imageBox_Resize);
            // 
            // productPriceTextField
            // 
            this.productPriceTextField.Enabled = false;
            this.productPriceTextField.Location = new System.Drawing.Point(143, 135);
            this.productPriceTextField.Name = "productPriceTextField";
            this.productPriceTextField.Size = new System.Drawing.Size(213, 22);
            this.productPriceTextField.TabIndex = 8;
            // 
            // productShelfLifeTextField
            // 
            this.productShelfLifeTextField.Enabled = false;
            this.productShelfLifeTextField.Location = new System.Drawing.Point(143, 90);
            this.productShelfLifeTextField.Name = "productShelfLifeTextField";
            this.productShelfLifeTextField.Size = new System.Drawing.Size(213, 22);
            this.productShelfLifeTextField.TabIndex = 7;
            // 
            // productStockCodeTextField
            // 
            this.productStockCodeTextField.Enabled = false;
            this.productStockCodeTextField.Location = new System.Drawing.Point(143, 53);
            this.productStockCodeTextField.Name = "productStockCodeTextField";
            this.productStockCodeTextField.Size = new System.Drawing.Size(213, 22);
            this.productStockCodeTextField.TabIndex = 6;
            // 
            // productPriceLable
            // 
            this.productPriceLable.AutoSize = true;
            this.productPriceLable.Location = new System.Drawing.Point(49, 138);
            this.productPriceLable.Name = "productPriceLable";
            this.productPriceLable.Size = new System.Drawing.Size(76, 16);
            this.productPriceLable.TabIndex = 4;
            this.productPriceLable.Text = "Ürün Fiyatı :";
            // 
            // productAverageShelfLifeLabel
            // 
            this.productAverageShelfLifeLabel.AutoSize = true;
            this.productAverageShelfLifeLabel.Location = new System.Drawing.Point(25, 90);
            this.productAverageShelfLifeLabel.Name = "productAverageShelfLifeLabel";
            this.productAverageShelfLifeLabel.Size = new System.Drawing.Size(100, 16);
            this.productAverageShelfLifeLabel.TabIndex = 3;
            this.productAverageShelfLifeLabel.Text = "Ürün Raf Ömrü :";
            // 
            // productStockCodeLabel
            // 
            this.productStockCodeLabel.AutoSize = true;
            this.productStockCodeLabel.Location = new System.Drawing.Point(20, 53);
            this.productStockCodeLabel.Name = "productStockCodeLabel";
            this.productStockCodeLabel.Size = new System.Drawing.Size(105, 16);
            this.productStockCodeLabel.TabIndex = 2;
            this.productStockCodeLabel.Text = "Ürün Stok Kodu: ";
            // 
            // productNameLabel
            // 
            this.productNameLabel.AutoSize = true;
            this.productNameLabel.Location = new System.Drawing.Point(57, 19);
            this.productNameLabel.Name = "productNameLabel";
            this.productNameLabel.Size = new System.Drawing.Size(68, 16);
            this.productNameLabel.TabIndex = 1;
            this.productNameLabel.Text = "Ürün İsmi :";
            // 
            // productNameTextField
            // 
            this.productNameTextField.Enabled = false;
            this.productNameTextField.Location = new System.Drawing.Point(143, 13);
            this.productNameTextField.Name = "productNameTextField";
            this.productNameTextField.Size = new System.Drawing.Size(213, 22);
            this.productNameTextField.TabIndex = 0;
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(12, 13);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(144, 36);
            this.updateButton.TabIndex = 3;
            this.updateButton.Text = "Ürünü Güncelle";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(12, 186);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(144, 36);
            this.exitButton.TabIndex = 4;
            this.exitButton.Text = "Çıkış Yap";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Location = new System.Drawing.Point(540, 474);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(85, 16);
            this.lblError.TabIndex = 5;
            this.lblError.Text = "Sistem Stabil";
            // 
            // productTransactionsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1173, 499);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataTable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Location = new System.Drawing.Point(350, 250);
            this.Name = "productTransactionsPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "productTransactionsPage";
            this.Load += new System.EventHandler(this.productTransactionsPage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataTable)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataTable;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label productNameLabel;
        private System.Windows.Forms.TextBox productNameTextField;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.TextBox productPriceTextField;
        private System.Windows.Forms.TextBox productShelfLifeTextField;
        private System.Windows.Forms.TextBox productStockCodeTextField;
        private System.Windows.Forms.Label productPriceLable;
        private System.Windows.Forms.Label productAverageShelfLifeLabel;
        private System.Windows.Forms.Label productStockCodeLabel;
        private System.Windows.Forms.Label productImage;
        private System.Windows.Forms.PictureBox imageBox;
        private System.Windows.Forms.TextBox imageHeightTextField;
        private System.Windows.Forms.TextBox imageWidthTextField;
        private System.Windows.Forms.Label imageHeight;
        private System.Windows.Forms.Label imageWidth;
        private System.Windows.Forms.Label productStockAmountLabel;
        private System.Windows.Forms.TextBox productStockAmountTextField;
        private System.Windows.Forms.Button confirmUpdateButton;
        private System.Windows.Forms.Button uploadImageButton;
        private System.Windows.Forms.Label lblError;
    }
}
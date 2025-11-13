using OrganicProductSaleManagementV2._0.Items;
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
    public partial class mainFrame : Form
    {
        productTransactionsPage ptPage;
        addProduct addP;
        Boolean isUserAdmin = false;
        int pageNumber = 0;
        int pageNumberFSalM = 0;
        int pageNumberFPmp = 0;
        List<newSaleMProductsPanel> pPanels;
        List<productCard> pCards;
        List<productInfo> itemsForProducts;


        List<productInfo> items;
        List<itemCard> openedTabs;
        List<itemsPanel> itemsPanels;

        List<userInfo> profileItems;
        List<profilCard> profileCards;
        List<profilePanel> profilePanels;

        addUser aU;
        setNewSettingsOnCurrAcccs sett;

        private int animationStep = 15;
        private Boolean isMenuOpened = false;
        private Boolean isMenuOpenedForTimer = false;
        private double opacityValue = 0.0;
        private const double opacityDecrement = 0.01;
        private userInfo userInfo = null;
        private int[] productCardPositions = { 40, 315, 580};
        private int[] itemCardPositions = { 18, 158, 298 };
        private int[] profileCardPositions = { 10, 170, 330 };

        private orProSaleManager sqlManager = new currentDatabase().getCurrentDatabaseForTransaction();


        public mainFrame(userInfo _userInfo)
        {
            this.userInfo = _userInfo;
            InitializeComponent();
            pPanels = new List<newSaleMProductsPanel>();
            pCards = new List<productCard>();
            openedTabs = new List<itemCard>();
            itemsPanels = new List<itemsPanel>();
            profilePanels = new List<profilePanel>();
            profileCards = new List<profilCard>();
            profileItems = new List<userInfo>();
            
            
            timerExpand.Interval = 15;
            timerExpand.Tick += TimerExpand_Tick;
            setUserInformationOptions();
            productSaleCardsSearchBar_KeyUp(null, null);
            BankCard.Checked = true;
            //setTopThreeProduct();
        }


        //Veritabanından bilgi çekerek profil kartına ekliyoruz
        private void setProfileInformations()
        {
            this.profileUserNameLabel.Text = userInfo.getUserName();
            this.profileUserPhoneNumber.Text = userInfo.getPhoneNumber();
            this.profileUserAuthLevel.Text = userInfo.getAuthorityLevel();
            this.profileUserBirthDate.Text = userInfo.getUserBirthDate().ToString();
            this.profileUserIdentityNumber.Text = userInfo.getUserId();
            byte[] imageBytes = userInfo.getImageData();
            this.userProfilePicture.Image = convertByteToImage(imageBytes);
        }

        //Burada veri tablosu oluşturuyoruz
        private void setupDataGridView()
        {
            DataGridViewColumn hiddenColumn = new DataGridViewTextBoxColumn();
            hiddenColumn.Name = "productId";
            hiddenColumn.HeaderText = "id";
            hiddenColumn.Visible = false; 

            DataGridViewColumn hiddenColumn2 = new DataGridViewTextBoxColumn();
            hiddenColumn.Name = "productTypeId";
            hiddenColumn.HeaderText = "id2";
            hiddenColumn.Visible = false;


            dataTableForProducts.Columns.Add("productName", "Ürün Adı");
            dataTableForProducts.Columns.Add("unitPrice", "Birim Fiyat");
            dataTableForProducts.Columns.Add("count", "Adet");
            dataTableForProducts.Columns.Add("sumPrice", "Toplam Fiyat");
            dataTableForProducts.Columns.Add(hiddenColumn);
            dataTableForProducts.Columns.Add(hiddenColumn2);
            dataTableForProducts.Columns[0].Width = 158;
            dataTableForProducts.Columns[1].Width = 82;
            dataTableForProducts.Columns[2].Width = 82;
            dataTableForProducts.Columns[3].Width = 84;
            dataTableForProducts.DefaultCellStyle.BackColor = Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(227)))), ((int)(((byte)(140)))));
            dataTableForProducts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(191)))), ((int)(((byte)(47))))); ;
            dataTableForProducts.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

        }

        //Bur metod bütün panelleri arkaya atar
        private void closeAll()
        {
            saleManagementPanel.Enabled = false;
            //saleManagementPanel.SendToBack();

            inventoryMPanel.Enabled = false;
            //inventoryMPanel.SendToBack();

            saleManagementPanel.Enabled = false;
            //saleManagementPanel.SendToBack();

            dataTableForProducts.Rows.Clear();
        }

        //Toplam fiyatı günceller
        private void setSumPriceTextBoxValue(int value)
        {
            sumPriceTextField.Text = (Convert.ToInt32(sumPriceTextField.Text) + value).ToString();
        }
        
        //Bu metod satış ekranındaki arama textboxının içine yazılan değeri veritabanında arayarak eşleşen ürünleri getirir
        private void productSaleCardsSearchBar_KeyUp(object sender, KeyEventArgs e)
        {
            if (pPanels != null)
            {
                foreach (newSaleMProductsPanel productPanel in pPanels)
                {
                    saleManagementPanel.Controls.Remove(productPanel);
                    productPanel.Dispose();
                }
                pPanels.Clear();
            }
            else
            {
                pPanels = new List<newSaleMProductsPanel>();
            }

            if (pCards != null)
            {
                pCards.Clear();
            }
            else
            {
                pCards = new List<productCard>();
            }

            itemsForProducts = sqlManager.getProductWithParameter(productSaleCardsSearchBar.Text.Trim());


            if (itemsForProducts != null && itemsForProducts.Count > 0)
            {
                int j = 0;
                foreach (productInfo pInfo in itemsForProducts)
                {
                    //int stockAmount = sqlManager.getProductAmountSum(pInfo.getProductTypeClass().getId());

                    productCard pCard = new productCard(productCardPositions[j], 5, pInfo, pInfo.getNumberOfProducts(), dataTableForProducts, this.setSumPriceTextBoxValue);
                    pCards.Add(pCard);
                    j++;
                    if (j == 3)
                    {
                        j = 0;
                    }
                }
            }

            if (pCards.Count > 0)
            {
                for (int a = 0; a < pCards.Count; a += 3)
                {

                    List<productCard> tempItems = new List<productCard>();
                    if (pCards.Count - a >= 3)
                    {
                        tempItems.Add(pCards[a]);
                        tempItems.Add(pCards[a + 1]);
                        tempItems.Add(pCards[a + 2]);
                        tempItems.Add(pCards[a + 3]);
                    }
                    else if (pCards.Count - a >= 3)
                    {
                        tempItems.Add(pCards[a]);
                        tempItems.Add(pCards[a + 1]);
                        tempItems.Add(pCards[a + 2]);
                    }
                    else if (pCards.Count - a == 2)
                    {
                        tempItems.Add(pCards[a]);
                        tempItems.Add(pCards[a + 1]);
                    }
                    else if (pCards.Count - a == 1)
                    {
                        tempItems.Add(pCards[a]);
                    }

                    if (tempItems.Count > 0)
                    {
                        pPanels.Add(new newSaleMProductsPanel(tempItems));
                    }
                }

            }

            if (pPanels.Count > 0)
            {

                saleManagementPanel.Controls.Add(pPanels[0]);
                pPanels[0].Visible = true;
                pPanels[0].Enabled = true;



                for (int i = 1; i < pPanels.Count; i++)
                {
                    saleManagementPanel.Controls.Add(pPanels[i]);
                    pPanels[i].Visible = false;
                    pPanels[i].Enabled = true;
                }
            }

            pageNumberFSalM = 0;
            previousPageForSaleM.Enabled = false;
            pageNumberLabelSaleMa.Text = "0";

            nextPageForSaleM.Enabled = pPanels.Count > 1;
            saleManagementPanel.Refresh();
        }


        //satış ekranında ürünlerin bulunduğu paneller arasınd geçiş yaoar
        private void previousPageForSaleM_Click(object sender, EventArgs e)
        {
            pPanels[pageNumberFSalM].Visible = false;
            pPanels[pageNumberFSalM - 1].Visible = true;

            if (pageNumberFSalM - 1 == 0)
            {
                previousPageForSaleM.Enabled = false;
            }
            if (pageNumberFSalM > 0)
            {
                nextPageForSaleM.Enabled = true;
            }
            pageNumberFSalM--;
            pageNumberLabelSaleMa.Text = (Convert.ToInt32(pageNumberLabelSaleMa.Text.Trim()) - 1).ToString();
        }

        //satış ekranında ürünlerin bulunduğu paneller arasınd geçiş yaoar
        private void nextPageForSaleM_Click(object sender, EventArgs e)
        {
            pPanels[pageNumberFSalM].Visible = false;
            pPanels[pageNumberFSalM + 1].Visible = true;

            if (pPanels.Count - 1 == pageNumberFSalM + 1)
            {
                nextPageForSaleM.Enabled = false;
            }
            if (pageNumberFSalM + 1 > 0)
            {
                previousPageForSaleM.Enabled = true;
            }
            pageNumberFSalM++;
            pageNumberLabelSaleMa.Text = (Convert.ToInt32(pageNumberLabelSaleMa.Text.Trim()) + 1).ToString();
        }

        //Arama barında aksiyon gerçekleşince sqle sorgu gönderir ve bir panel oluşturur. Bu panelin içine veritabanından ürün bilgilerini
        //alarak ürün kartları yerleştirir. eğer 3 ten fazla ürün varsa yeni sayfalar üretir ve sayfalar arasında geçiş yapılabilir.
        //Yeni sayfalar ve yeni ürün kartları sınıflardan new'leyere oluşturulur
        private void searchBar_KeyUp(object sender, KeyEventArgs e)
        {
            if (itemsPanels != null)
            {
                foreach (itemsPanel openedPanel in itemsPanels)
                {
                    this.inventoryMPanel.Controls.Remove(openedPanel);
                    openedPanel.Dispose();
                }
                itemsPanels.Clear();
            }

            if (openedTabs == null)
            {
                openedTabs = new List<itemCard>();
            }
            else
            {
                openedTabs.Clear();
            }

            itemsPanels = new List<itemsPanel>();


            items = this.sqlManager.getProductWithParameter(this.searchBar.Text.Trim());

            if (items != null)
            {
                int j = 0;
                foreach (productInfo pInfo in items)
                {
                    //int stockAmount = sqlManager.getProductAmountSum(pInfo.getProductTypeClass().getId());
                    itemCard iCard = new itemCard(12, itemCardPositions[j], pInfo, pInfo.getNumberOfProducts(), isUserAdmin, searchBar_KeyUp);
                    openedTabs.Add(iCard);
                    j++;
                    if (j == 3)
                    {
                        j = 0;
                    }
                }
            }

            if (openedTabs != null && openedTabs.Count > 0)
            {
                for (int a = 0; a < openedTabs.Count; a += 3)
                {
                    List<itemCard> tempİtem = new List<itemCard>();
                    if (openedTabs.Count - a >= 3)
                    {
                        tempİtem.Add(openedTabs[a]);
                        tempİtem.Add(openedTabs[a + 1]);
                        tempİtem.Add(openedTabs[a + 2]);
                    }
                    else if (openedTabs.Count - a == 2)
                    {
                        tempİtem.Add(openedTabs[a]);
                        tempİtem.Add(openedTabs[a + 1]);
                    }
                    else if (openedTabs.Count - a == 1)
                    {
                        tempİtem.Add(openedTabs[a]);
                    }
                    if (tempİtem.Count > 0)
                    {
                        itemsPanels.Add(new itemsPanel(tempİtem));
                    }
                }
            }

            if (itemsPanels.Count > 0)
            {
                inventoryMPanel.Controls.Add(itemsPanels[0]);
                itemsPanels[0].Visible = true;

                for (int i = 1; i < itemsPanels.Count; i++)
                {
                    inventoryMPanel.Controls.Add(itemsPanels[i]);
                    itemsPanels[i].Visible = false;
                }
            }
            pageNumber = 0;
            previousPageButton.Enabled = false;
            pageLabel.Text = "0";
            if (itemsPanels.Count > 1)
            {
                nextPageButton.Enabled = true;
            }
            else
            {
                nextPageButton.Enabled = false;
            }

        }


        //Ürün yönetim ekranında oluşturulan sayfalar arasında geçiş yapmak için. Geriye gitmek için
        private void previousPageButton_Click(object sender, EventArgs e)
        {
            itemsPanels[pageNumber].Visible = false;
            itemsPanels[pageNumber - 1].Visible = true;

            if (pageNumber - 1 == 0)
            {
                previousPageButton.Enabled = false;
            }
            if (pageNumber > 0)
            {
                nextPageButton.Enabled = true;
            }
            pageNumber--;
            pageLabel.Text = (Convert.ToInt32(pageLabel.Text.Trim()) - 1).ToString();

        }

        //Ürün yönetim ekranında oluşturulan sayfalar arasında geçiş yapmak için. İleriye gitmek için
        private void nextPageButton_Click(object sender, EventArgs e)
        {

            itemsPanels[pageNumber].Visible = false;
            itemsPanels[pageNumber + 1].Visible = true;

            if (itemsPanels.Count - 1 == pageNumber + 1)
            {
                nextPageButton.Enabled = false;
            }
            if (pageNumber + 1 > 0)
            {
                previousPageButton.Enabled = true;
            }
            pageNumber++;
            pageLabel.Text = (Convert.ToInt32(pageLabel.Text.Trim()) + 1).ToString();
        }


        //Menünün kayma efektini yapmka için
        private void TimerExpand_Tick(object sender, EventArgs e)
        {

            if (isMenuOpenedForTimer)
            {

                leftMenu.Width -= animationStep;
                this.leftMenu.Location = new System.Drawing.Point(0, 0);
                if (leftMenu.Width <= 50)
                {
                    leftMenu.Width = 50;
                    isMenuOpenedForTimer = false;
                    timerExpand.Stop();
                }
            }
            else
            {

                leftMenu.Width += animationStep;
                this.leftMenu.Location = new System.Drawing.Point(0, 0);
                if (leftMenu.Width >= 190)
                {
                    leftMenu.Width = 190;
                    isMenuOpenedForTimer = true;
                    timerExpand.Stop();
                }

            }

        }


        //Menü kapandığında gerçekleşecek olan düğme ve yazı stillerini ayarlar
        private void setClosedOptionsOfMenu()
        {

            this.isMenuOpened = false;

            this.menuButton.Text = "";
            this.menuButton.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.menuButton.Size = new System.Drawing.Size(51, 50);
            this.menuButton.Location = new System.Drawing.Point(0, 12);
            this.for_menuButton.CornerRadius = 0;


            this.saleMButton.Text = "";
            this.saleMButton.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.saleMButton.Size = new System.Drawing.Size(51, 50);
            this.saleMButton.Location = new System.Drawing.Point(0, 100);
            this.for_SaleMButton.CornerRadius = 0;


            this.InventOnHandListButton.Text = "";
            this.InventOnHandListButton.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.InventOnHandListButton.Size = new System.Drawing.Size(51, 50);
            this.InventOnHandListButton.Location = new System.Drawing.Point(0, 180);
            this.for_InventOnHandListBtn.CornerRadius = 0;


            this.EmployeeManagementButton.Text = "";
            this.EmployeeManagementButton.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.EmployeeManagementButton.Size = new System.Drawing.Size(51, 50);
            this.EmployeeManagementButton.Location = new System.Drawing.Point(0, 260);
            this.for_EmployeeManagementBtn.CornerRadius = 0;



        }

        //Menü açıldığında gerçekleşecek olan düğme ve yazı stillerini ayarlar
        private void setOpenedOptionsOfMenu()
        {

            this.isMenuOpened = true;

            this.menuButton.Text = "      Menu";
            this.menuButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menuButton.Size = new System.Drawing.Size(175, 50);
            this.menuButton.Location = new System.Drawing.Point(5, 12);
            this.for_menuButton.CornerRadius = 45;


            this.saleMButton.Text = "Satış Yönetimi";
            this.saleMButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.saleMButton.Size = new System.Drawing.Size(215, 50);
            this.saleMButton.Location = new System.Drawing.Point(5, 100);
            this.for_SaleMButton.CornerRadius = 45;


            this.InventOnHandListButton.Text = "Stok Yönetimi";
            this.InventOnHandListButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.InventOnHandListButton.Size = new System.Drawing.Size(215, 50);
            this.InventOnHandListButton.Location = new System.Drawing.Point(5, 180);
            this.for_InventOnHandListBtn.CornerRadius = 45;


            this.EmployeeManagementButton.Text = "   Çalışan Yönetimi";
            this.EmployeeManagementButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.EmployeeManagementButton.Size = new System.Drawing.Size(215, 50);
            this.EmployeeManagementButton.Location = new System.Drawing.Point(5, 260);
            this.for_EmployeeManagementBtn.CornerRadius = 45;

        }


        //SAyfanın saydamlıktan opaklığa dopğru açılmasını sağlayan timer ın bağlı olduğu metod.YAvaş yavaş opaklığı arttırırın
        private void opacityTimer_Tick(object sender, EventArgs e)
        {

            if (opacityValue < 1)
            {
                opacityValue += opacityDecrement;
                this.Opacity = opacityValue;
            }
            else
            {
                opacityTimer.Stop();
                this.DialogResult = DialogResult.OK;
            }
        }

        //resimleri byte koda çeviren metod
        public Image convertByteToImage(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                return Image.FromStream(ms);
            }
        }

        //Kullanıcının yetki düzeyine göre erişlebilirliği ayarlıyor
        private void setUserAuthorityAcces()
        {
            if (userInfo.getAuthorityLevel().Trim().Equals("admin") || userInfo.getAuthorityLevel().Trim().Equals("CİO"))
            {
                addNewProductType.Enabled = true;
                productEventOptionsButton.Enabled = true;
                isUserAdmin = true;
                addNewProfileButton.Enabled = true;
            }
            else if (userInfo.getAuthorityLevel().Trim().Equals("employee"))
            {

                addNewProductType.Enabled = false;
                productEventOptionsButton.Enabled = false;
                isUserAdmin = false;
                addNewProfileButton.Enabled = false;
            }
        }

        //Sağ üstteki profil kartını ayarlıyor
        private void setUserInformationOptions()
        {
            byte[] imageBytes = userInfo.getImageData();
            userPicture.Image = convertByteToImage(imageBytes);

            this.userNameLabel.Text = "Kullanıcı Adı : " + userInfo.getUserName();
            this.userAuthorityLabel.Text = "Yetki Düzeyi : " + userInfo.getAuthorityLevel();

            setProfileInformations();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            setUserAuthorityAcces();

            this.leftMenu.Location = new System.Drawing.Point(0, 0);
            this.leftMenu.Size = new System.Drawing.Size(50, 900);
            setClosedOptionsOfMenu();
            opacityTimer.Interval = 10;
            opacityTimer.Start();
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            setupDataGridView();
            sumPriceTextField.Text = "0";
            searchTextBox2_KeyUp(null, null);

        }

        //Satış ekranı butonu. Basınca satış ekranını açar
        private void saleMButton_Click(object sender, EventArgs e)
        {
            if (this.isMenuOpened)
            {
                timerExpand.Start();
                setClosedOptionsOfMenu();
            }
            closeAll();
            saleManagementPanel.Enabled = true;
            saleManagementPanel.BringToFront();
            leftMenu.BringToFront();

        }

        //MouseEnter ve MouseLeave metodları görsel dizayn içindir
        private void saleMButton_MouseEnter(object sender, EventArgs e)
        {
            this.saleMButton.BackColor = Color.White;
            this.saleMButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
            this.saleMButton.Image = global::OrganicProductSaleManagementV3.Properties.Resources.basketGreen;
            this.saleMButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonFace; ;
            this.saleMButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ButtonFace; ;
            this.saleMButton.FlatAppearance.MouseOverBackColor = Color.White;
        }
        //MouseEnter ve MouseLeave metodları görsel dizayn içindir
        private void saleMButton_MouseLeave(object sender, EventArgs e)
        {
            this.saleMButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
            this.saleMButton.ForeColor = Color.White;
            this.saleMButton.Image = global::OrganicProductSaleManagementV3.Properties.Resources.basketWhite;
            this.saleMButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
            this.saleMButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
            this.saleMButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
        }
        //Eldekiler(Envanter) listesi açma butonudur
        private void InventOnHandListButton_Click(object sender, EventArgs e)
        {
            if (this.isMenuOpened)
            {
                timerExpand.Start();
                setClosedOptionsOfMenu();

            }
            closeAll();
            inventoryMPanel.Enabled = true;
            inventoryMPanel.BringToFront();
            leftMenu.BringToFront();
            searchBar_KeyUp(null, null);
        }

        //MouseEnter ve MouseLeave metodları görsel dizayn içindir
        private void InventOnHandListButton_MouseEnter(object sender, EventArgs e)
        {
            this.InventOnHandListButton.BackColor = Color.White;
            this.InventOnHandListButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
            this.InventOnHandListButton.Image = global::OrganicProductSaleManagementV3.Properties.Resources.inventoryGreen;
            this.InventOnHandListButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonFace; ;
            this.InventOnHandListButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ButtonFace; ;
            this.InventOnHandListButton.FlatAppearance.MouseOverBackColor = Color.White;
        }
        //MouseEnter ve MouseLeave metodları görsel dizayn içindir
        private void InventOnHandListButton_MouseLeave(object sender, EventArgs e)
        {
            this.InventOnHandListButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
            this.InventOnHandListButton.ForeColor = Color.White;
            this.InventOnHandListButton.Image = global::OrganicProductSaleManagementV3.Properties.Resources.inventoryWhite;
            this.InventOnHandListButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
            this.InventOnHandListButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
            this.InventOnHandListButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
        }
        //Profil yönetim ekranı açma butonu
        private void EmployeeManagementButton_Click(object sender, EventArgs e)
        {
            if (this.isMenuOpened)
            {
                timerExpand.Start();
                setClosedOptionsOfMenu();

            }
            setProfileInformations();
            closeAll();
            profilesManagementPanel.Enabled = true;
            profilesManagementPanel.BringToFront();
            leftMenu.BringToFront();
            searchTextBox2_KeyUp(null, null);

        }
        //MouseEnter ve MouseLeave metodları görsel dizayn içindir
        private void EmployeeManagementButton_MouseEnter(object sender, EventArgs e)
        {
            this.EmployeeManagementButton.BackColor = Color.White;
            this.EmployeeManagementButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
            this.EmployeeManagementButton.Image = global::OrganicProductSaleManagementV3.Properties.Resources.employeeGreen;
            this.EmployeeManagementButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonFace; ;
            this.EmployeeManagementButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ButtonFace; ;
            this.EmployeeManagementButton.FlatAppearance.MouseOverBackColor = Color.White;
        }
        //MouseEnter ve MouseLeave metodları görsel dizayn içindir
        private void EmployeeManagementButton_MouseLeave(object sender, EventArgs e)
        {
            this.EmployeeManagementButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
            this.EmployeeManagementButton.ForeColor = Color.White;
            this.EmployeeManagementButton.Image = global::OrganicProductSaleManagementV3.Properties.Resources.employeeWhite;
            this.EmployeeManagementButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
            this.EmployeeManagementButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
            this.EmployeeManagementButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
        }
        //Menüyu açma butonu
        //Basıldığında timer ile birlikte animasyonlu bir şekilde ekranı açar
        private void menuButton_Click(object sender, EventArgs e)
        {

            if (this.isMenuOpened)
            {
                timerExpand.Start();
                setClosedOptionsOfMenu();
                productSaleCardsSearchBar.Text = "";
                productSaleCardsSearchBar_KeyUp(null, null);


            }
            else
            {
                timerExpand.Start();
                setOpenedOptionsOfMenu();


            }


        }

        //MouseEnter ve MouseLeave metodları görsel dizayn içindir
        private void menuButton_MouseEnter(object sender, EventArgs e)
        {
            this.menuButton.BackColor = Color.White;
            this.menuButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
            this.menuButton.Image = global::OrganicProductSaleManagementV3.Properties.Resources.icons8_menu_30;
            this.menuButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonFace; ;
            this.menuButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ButtonFace; ;
            this.menuButton.FlatAppearance.MouseOverBackColor = Color.White;
        }
        //MouseEnter ve MouseLeave metodları görsel dizayn içindir
        private void menuButton_MouseLeave(object sender, EventArgs e)
        {
            this.menuButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
            this.menuButton.ForeColor = Color.White;
            this.menuButton.Image = global::OrganicProductSaleManagementV3.Properties.Resources.icons8_menu_30__1_;
            this.menuButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
            this.menuButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
            this.menuButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
        }
        //Yeni ürün ekleme Frame'ini açar
        private void addNewProductType_Click(object sender, EventArgs e)
        {

            if (this.addP == null)
            {
                this.addP = new addProduct(searchBar_KeyUp);
                this.addP.Show();
            }
            else
            {
                this.addP.Dispose();
                this.addP = new addProduct(searchBar_KeyUp);
                this.addP.Show();
            }
        }
        //MouseEnter ve MouseLeave metodları görsel dizayn içindir
        private void addNewProductType_MouseEnter(object sender, EventArgs e)
        {
            this.addNewProductType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(227)))), ((int)(((byte)(140)))));
            this.addNewProductType.ForeColor = System.Drawing.Color.White;
            this.addNewProductType.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonFace; ;
            this.addNewProductType.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(181)))), ((int)(((byte)(44)))));
            this.addNewProductType.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(227)))), ((int)(((byte)(140)))));
        }
        //MouseEnter ve MouseLeave metodları görsel dizayn içindir
        private void addNewProductType_MouseLeave(object sender, EventArgs e)
        {
            this.addNewProductType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
            this.addNewProductType.ForeColor = Color.White;
            this.addNewProductType.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
            this.addNewProductType.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
            this.addNewProductType.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
        }
        //ürün yönetim ekranını açar
        private void productEventOptionsButton_Click(object sender, EventArgs e)
        { 

            if (this.ptPage == null)
            {
                this.ptPage = new productTransactionsPage(isUserAdmin);
                this.ptPage.Show();
            }
            else
            {
                this.ptPage.Dispose();
                this.ptPage = new productTransactionsPage(isUserAdmin);
                this.ptPage.Show();
            }
        }
        //Reklam kısmındaki veriyi düzenler
        private void setTopThreeProduct()
        {
            List<topProducts> tP = sqlManager.getTopThreeProducts();

            if(tP != null)
            {
                pictureBox1.Image = convertByteToImage(tP[0].getPicture());
                label4.Text = tP[0].getName().Trim();
                label6.Text = "Ücret: " + tP[0].getPrice().ToString();

                pictureBox2.Image = convertByteToImage(tP[1].getPicture());
                label8.Text = tP[1].getName().Trim();
                label7.Text = "Ücret: " + tP[1].getPrice().ToString();

                pictureBox3.Image = convertByteToImage(tP[2].getPicture());
                label11.Text = tP[2].getName().Trim();
                label10.Text = "Ücret: " + tP[2].getPrice().ToString();
            }

           
        }
        //MouseEnter ve MouseLeave metodları görsel dizayn içindir
        private void productEventOptionsButton_MouseEnter(object sender, EventArgs e)
        {
            this.productEventOptionsButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(227)))), ((int)(((byte)(140)))));
            this.productEventOptionsButton.ForeColor = System.Drawing.Color.White;
            this.productEventOptionsButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonFace; ;
            this.productEventOptionsButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(181)))), ((int)(((byte)(44)))));
            this.productEventOptionsButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(227)))), ((int)(((byte)(140)))));
        }
        //MouseEnter ve MouseLeave metodları görsel dizayn içindir
        private void productEventOptionsButton_MouseLeave(object sender, EventArgs e)
        {
            this.productEventOptionsButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
            this.productEventOptionsButton.ForeColor = Color.White;
            this.productEventOptionsButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
            this.productEventOptionsButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
            this.productEventOptionsButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(215)))), ((int)(((byte)(91)))));
        }
        //Veri tablosunun sütunlarına tıklayınca verinin arama textbox ına gitmesini sağlar
        private void dataTableForProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataTableForProducts.Rows[e.RowIndex];
                productSaleCardsSearchBar.Text = selectedRow.Cells[0].Value.ToString();
                productSaleCardsSearchBar_KeyUp(null, null);
                searchBar.Text = "";

            }
        }

        //RadioButton eventini denetler
        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (BankCard.Checked)
            {

            }
            else if (CashRadioButton.Checked)
            {

            }
        }

        //Satışı bitirmek butonudur. Veritabanına stok eskiltmek için sorgu gönderir ve satış sayılarıbnı arttırır
        private void endShopping_Click(object sender, EventArgs e)
        {
            try
            {
                //burada ok ve no yazan dialog penceresi açıyorız
                DialogResult result = MessageBox.Show(
                    "Satışı Bitirmek İstiyor Musunuz?",
                    "Satış Bitirme Onayı",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );
                //Evet ise nolucak
                if (result == DialogResult.Yes)
                {

                    foreach (DataGridViewRow row in dataTableForProducts.Rows)
                    {
                        try
                        {
                            //hata var mı diye kontorl etmek için verileri değerlere atıyoruz
                            int productId = Convert.ToInt32(row.Cells[4].Value);
                            int productTypeId = Convert.ToInt32(row.Cells[5].Value);
                            int quantity = Convert.ToInt32(row.Cells[2].Value);

                            //sql update metodunu çalıştırır
                            sqlManager.updateProductStockAmount(productId.ToString(), quantity, productTypeId.ToString());
                            setTopThreeProduct();
                        }
                        catch (FormatException ex)
                        {
                            MessageBox.Show($"Veri format hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    //satış biterse mesaj verir
                    MessageBox.Show("Satış Bitirildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //Kodun bu kısmında tablo verileri silinir ve kartlar güncellenir

                    dataTableForProducts.Rows.Clear();
                    productSaleCardsSearchBar_KeyUp(null, null);
                    sumPriceTextField.Text = "0";

                }
                else if (result == DialogResult.No)
                {
                    //hayrı derse bu mesajı verir
                    MessageBox.Show("Satış bitirme iptal edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                //Genel bir hata olduğunda bu blok çalışıyor
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Çıkış butonudur. Basınca onay ekranı gelir ve sonra çıkış yapılır
        private void exitButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                    "Çıkış Yapmak İstiyor Musunuz?",
                    "Çıkış Yap",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

            if (result == DialogResult.Yes)
            {
                Dispose();
            }
            else if (result == DialogResult.No)
            {

            }

        }
        //Kullanıcı değiştirme ekranıdır
        private void ChangeUserButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                    "Kullanıcı Değiştirmek İstiyor Musunuz ?",
                    "Kullanıcı Değiştir",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

            if (result == DialogResult.Yes)
            {
                Application.Restart();
            }
            else if (result == DialogResult.No)
            {

            }

        }

        //profil güncelleme ekranını açar
        private void profileUpdateUserInformations_Click(object sender, EventArgs e)
        {
            if (this.sett == null)
            {
                this.sett = new setNewSettingsOnCurrAcccs(this.userInfo, searchTextBox2_KeyUp);
                this.sett.Show();
            }
            else
            {
                this.sett.Dispose();
                this.sett = new setNewSettingsOnCurrAcccs(this.userInfo, searchTextBox2_KeyUp);
                this.sett.Show();
            }
        }
        //MouseEnter ve MouseLeave metodları görsel dizayn içindir
        private void profileUpdateUserInformations_MouseEnter(object sender, EventArgs e)
        {
            this.productEventOptionsButton.BackColor = Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(235)))), ((int)(((byte)(230)))));
            this.productEventOptionsButton.ForeColor = Color.White;
            this.productEventOptionsButton.FlatAppearance.BorderColor = SystemColors.ButtonFace; ;
            this.productEventOptionsButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(235)))), ((int)(((byte)(230)))));
            this.productEventOptionsButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(235)))), ((int)(((byte)(230)))));
        }
        //MouseEnter ve MouseLeave metodları görsel dizayn içindir
        private void profileUpdateUserInformations_MouseLeave(object sender, EventArgs e)
        {
            this.productEventOptionsButton.BackColor = Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(223)))), ((int)(((byte)(215)))));
            this.productEventOptionsButton.ForeColor = Color.White;
            this.productEventOptionsButton.FlatAppearance.BorderColor = Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(223)))), ((int)(((byte)(215)))));
            this.productEventOptionsButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(223)))), ((int)(((byte)(215)))));
            this.productEventOptionsButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(223)))), ((int)(((byte)(215)))));
        }
        //Kullanıcı değiştirme butonu
        private void changeUser_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                    "Kullanıcı Değiştirmek İstiyor Musunuz ?",
                    "Kullanıcı Değiştir",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

            if (result == DialogResult.Yes)
            {
                Application.Restart();
            }
            else if (result == DialogResult.No)
            {

            }
        }
        //MouseEnter ve MouseLeave metodları görsel dizayn içindir
        private void changeUser_MouseEnter(object sender, EventArgs e)
        {
            this.productEventOptionsButton.BackColor = Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(235)))), ((int)(((byte)(230)))));
            this.productEventOptionsButton.ForeColor = Color.White;
            this.productEventOptionsButton.FlatAppearance.BorderColor = Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(235)))), ((int)(((byte)(230))))); ;
            this.productEventOptionsButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(235)))), ((int)(((byte)(230)))));
            this.productEventOptionsButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(235)))), ((int)(((byte)(230)))));
        }
        //MouseEnter ve MouseLeave metodları görsel dizayn içindir
        private void changeUser_MouseLeave(object sender, EventArgs e)
        {
            this.productEventOptionsButton.BackColor = Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(223)))), ((int)(((byte)(215)))));
            this.productEventOptionsButton.ForeColor = Color.White;
            this.productEventOptionsButton.FlatAppearance.BorderColor = Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(223)))), ((int)(((byte)(215)))));
            this.productEventOptionsButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(223)))), ((int)(((byte)(215)))));
            this.productEventOptionsButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(223)))), ((int)(((byte)(215)))));
        }

        //MouseEnter ve MouseLeave metodları görsel dizayn içindir
        private void button2_MouseEnter(object sender, EventArgs e)
        {
            this.productEventOptionsButton.BackColor = Color.White;
            this.productEventOptionsButton.ForeColor = Color.White;
            this.productEventOptionsButton.FlatAppearance.BorderColor = Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(223)))), ((int)(((byte)(215)))));
            this.productEventOptionsButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(223)))), ((int)(((byte)(215)))));
            this.productEventOptionsButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(223)))), ((int)(((byte)(215)))));

        }
        //MouseEnter ve MouseLeave metodları görsel dizayn içindir
        private void button2_MouseLeave(object sender, EventArgs e)
        {
            this.productEventOptionsButton.BackColor = Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(223)))), ((int)(((byte)(215)))));
            this.productEventOptionsButton.ForeColor = Color.White;
            this.productEventOptionsButton.FlatAppearance.BorderColor = Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(223)))), ((int)(((byte)(215)))));
            this.productEventOptionsButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(223)))), ((int)(((byte)(215)))));
            this.productEventOptionsButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(223)))), ((int)(((byte)(215)))));
        }
        //Profil ekranındaki textbox içine yazılan değeri veritabanında arar ve eşleşen profilleri getirir
        private void searchTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (profilePanels != null)
            {
                foreach (profilePanel profilePanel in profilePanels)
                {
                    saleManagementPanel.Controls.Remove(profilePanel);
                    profilePanel.Dispose();
                }
                profilePanels.Clear();
            }
            else
            {
                profilePanels = new List<profilePanel>();
            }

            if (profileCards != null)
            {
                profileCards.Clear();
            }
            else
            {
                profileCards = new List<profilCard>();
            }

            profileItems = sqlManager.getUsersInfo(searchTextBox2.Text.Trim());

            if (profileItems != null && profileItems.Count > 0)
            {
                int j = 0;
                foreach (userInfo pInfo in profileItems)
                {
                    profilCard profilCardd = new profilCard(pInfo, 16, profileCardPositions[j], isUserAdmin, searchTextBox2_KeyUp);
                    profileCards.Add(profilCardd);
                    j++;
                    if (j == 3)
                    {
                        j = 0;
                    }
                }
            }

            if (profileCards.Count > 0)
            {
                for (int a = 0; a < profileCards.Count; a += 3)
                {

                    List<profilCard> tempItems = new List<profilCard>();

                    if (profileCards.Count - a >= 3)
                    {
                        tempItems.Add(profileCards[a]);
                        tempItems.Add(profileCards[a + 1]);
                        tempItems.Add(profileCards[a + 2]);
                    }
                    else if (profileCards.Count - a == 2)
                    {
                        tempItems.Add(profileCards[a]);
                        tempItems.Add(profileCards[a + 1]);
                    }
                    else if (profileCards.Count - a == 1)
                    {
                        tempItems.Add(profileCards[a]);
                    }

                    if (tempItems.Count > 0)
                    {
                        profilePanels.Add(new profilePanel(tempItems));
                    }
                }
            }

            if (profilePanels.Count > 0)
            {
                profilesManagementPanel.Controls.Add(profilePanels[0]);
                profilePanels[0].Visible = true;

                for (int i = 1; i < profilePanels.Count; i++)
                {
                    profilesManagementPanel.Controls.Add(profilePanels[i]);
                    profilePanels[i].Visible = false;
                }

                profilPagesPreviousPageButton.Enabled = false;
                pageNumberFPmp = 0;
                profilPagesLabel.Text = "0";
                if (profilePanels.Count > 1)
                {
                    profilPagesNextPageButton.Enabled = true;
                }
                else
                {
                    profilPagesNextPageButton.Enabled = false;
                }
            }
        }
        //Profil sayfaları arasında geçiş yapar
        private void profilPagesPreviousPageButton_Click(object sender, EventArgs e)
        {
            profilePanels[pageNumberFPmp].Visible = false;
            profilePanels[pageNumberFPmp - 1].Visible = true;

            if (pageNumberFPmp - 1 == 0)
            {
                profilPagesPreviousPageButton.Enabled = false;
            }
            if (pageNumberFPmp > 0)
            {
                profilPagesNextPageButton.Enabled = true;
            }
            pageNumberFPmp--;
            profilPagesLabel.Text = (Convert.ToInt32(profilPagesLabel.Text.Trim()) - 1).ToString();
        }
        //Profile sayfaları arasında geçiş yapar
        private void profilPagesNextPageButton_Click(object sender, EventArgs e)
        {
            profilePanels[pageNumberFPmp].Visible = false;
            profilePanels[pageNumberFPmp + 1].Visible = true;

            if (profilePanels.Count - 1 == pageNumberFPmp + 1)
            {
                profilPagesNextPageButton.Enabled = false;
            }
            if (pageNumberFPmp + 1 > 0)
            {
                profilPagesPreviousPageButton.Enabled = true;
            }
            pageNumberFPmp++;
            profilPagesLabel.Text = (Convert.ToInt32(profilPagesLabel.Text.Trim()) + 1).ToString();
        }
        //Yeni profil ekleme butonu. Yeni profil ekler sadece admin kullanabilir
        private void addNewProfileButton_Click(object sender, EventArgs e)
        {
            if (this.aU == null)
            {
                this.aU = new addUser(searchTextBox2_KeyUp);
                this.aU.Show();
            }
            else
            {
                this.aU.Dispose();
                this.aU = new addUser(searchTextBox2_KeyUp);
                this.aU.Show();
            }

        }

        private void dataTableForProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }


    }

    


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrganicProductSaleManagementV3.items
{
    public class itemsPanel : Panel
    {
        private List<itemCard> items;

        public itemsPanel(List<itemCard> iCards)
        {
            
            this.items = iCards;
            this.Location = new System.Drawing.Point(15, 93);
            this.Size = new System.Drawing.Size(1293, 430);
            this.TabIndex = 2;
            this.Name = "inventoryMPanel";
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            setCards();
        }

        private void setCards()
        {
            if (items != null)
            {
                foreach (itemCard pInfo in items)
                {
                    Controls.Add(pInfo);
                }
            }
            

        }




    }
}

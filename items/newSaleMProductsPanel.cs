using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrganicProductSaleManagementV3.items
{
    public class newSaleMProductsPanel : Panel
    {
        private List<productCard> pCards;


        public newSaleMProductsPanel(List<productCard> _pCards)
        {
            this.pCards = _pCards;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Location = new System.Drawing.Point(12, 85);
            this.Name = "newSalMPanel";
            this.Size = new System.Drawing.Size(828, 270);
            this.TabIndex = 9;
            setCards();

        }

        private void setCards()
        {
            if (pCards != null)
            {
                foreach (productCard pcards in pCards)
                {
                    this.Controls.Add(pcards);
                }
            }


        }
    }
}

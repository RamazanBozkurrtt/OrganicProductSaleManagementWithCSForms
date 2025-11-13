using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrganicProductSaleManagementV3.items
{
    public class productPanel : Panel
    {
        private List<productCard> pCards;

        public productPanel(List<productCard> _pCards)
        {
            Console.WriteLine("PANEL OLUŞTURULDU");
            this.pCards = _pCards;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Location = new System.Drawing.Point(1288, 351);
            this.TabIndex = 2;
            this.Size = new System.Drawing.Size(12, 69);
            this.Name = "saleManagementPanel";
            this.Visible = true;
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

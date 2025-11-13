using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrganicProductSaleManagementV3.items
{
    public class profilePanel : Panel
    {
        private Panel innerPanel;
        private List<profilCard> profileCards;

        public profilePanel(List<profilCard> _profileCards)
        {
            this.profileCards = _profileCards;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(223)))), ((int)(((byte)(215)))));
            this.Location = new System.Drawing.Point(370,75);
            this.Name = "profilesPanel";
            this.Size = new System.Drawing.Size(610, 500);
            this.TabIndex = 1;
            setPanelSettings();
            setCards();

        }

        private void setPanelSettings()
        {
            this.innerPanel = new Panel();
            this.innerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.innerPanel.Location = new System.Drawing.Point(6, 6);
            this.innerPanel.Name = "profilesInnerPanel";
            this.innerPanel.Size = new System.Drawing.Size(594, 485);
            this.innerPanel.TabIndex = 0;
            Controls.Add(this.innerPanel);
        }

        private void setCards()
        {
            foreach (profilCard pCard in this.profileCards)
            {
                this.innerPanel.Controls.Add(pCard);
            }
        }
    }
}

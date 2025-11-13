using OrganicProductSaleManagementV2._0.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrganicProductSaleManagementV3.SqlTransactions.TestClasses
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label2.Text = "İŞLEM DEVAM EDİYOR...";
            button1.Enabled = false;
            using (addRandomDataTest test = new addRandomDataTest())
            {
                if(test.ShowDialog() == DialogResult.OK)
                {
                    label2.Text = "İŞLEM BİTTİ !";
                    button2.Enabled = true;
                }
            }



            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (testİsDatasEqual test = new testİsDatasEqual())
            {
                if (test.ShowDialog() == DialogResult.OK)
                {
                    label1.Text = "İŞLEM BİTTİ !";
                    button1.Enabled = true;
                }
            }
            
            button2.Enabled = false;
        }
    }
}

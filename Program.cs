using OrganicProductSaleManagementV2._0.Items;
using OrganicProductSaleManagementV3.SqlTransactions.TestClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrganicProductSaleManagementV3
{
    internal static class Program
    {
        
        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            
            using (userLogin loginScreen = new userLogin())
            {
                
                if (loginScreen.ShowDialog()==DialogResult.OK) 
                {
                    using(loadingScreen lScreen = new loadingScreen())
                    {
                      if (lScreen.ShowDialog() == DialogResult.OK)
                        {
                            System.Threading.Thread.Sleep(200);
                            userInfo uInfo = loginScreen.getUser();
                            Console.WriteLine(uInfo.getUserName());
                            Application.Run(new mainFrame(uInfo)); 
                        }
                        

                    }
                    
                }
            }
            










        }
    }
}

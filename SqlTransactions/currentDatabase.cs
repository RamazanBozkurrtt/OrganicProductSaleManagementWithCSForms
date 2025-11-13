using OrganicProductSaleManagementV2._0.SqlTransactions.userLoginTransactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicProductSaleManagementV3.SqlTransactions
{
    internal class currentDatabase
    {
        private orProSaleManager orProSaleManager;
        public currentDatabase(){
            this.orProSaleManager = new orProSaleManager(new dataBaseTransactions_MsSQL());
        
        }

        public orProSaleManager getCurrentDatabaseForTransaction()
        {
            return this.orProSaleManager;
        }

        
    }
}

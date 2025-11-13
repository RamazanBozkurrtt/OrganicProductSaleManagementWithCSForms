using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicProductSaleManagementV3.items
{
    public class authLevels
    {
        private string id;
        private string authLevel;
        public authLevels(string _id,string _authLevel) { 
        
            this.id = _id;
            this.authLevel = _authLevel;

        }

        public string getId() { return id; }
        public string getAuthLevel() {  return authLevel; }
    }
}

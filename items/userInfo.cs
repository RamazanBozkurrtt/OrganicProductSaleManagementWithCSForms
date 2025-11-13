using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicProductSaleManagementV2._0.Items
{
    public class userInfo
    {
        private String id;
        private String userName;
        private String password;
        private String phoneNumber;
        private String authorityLevel;
        private byte[] ımageData;
        private string userId;
        private DateTime userBirthDate;

        public userInfo(String _id, String _userName, String _password, String phoneNumber, String _authorityLevel, byte[] ımageData,string _userID,DateTime _userBirthDate)
        {
            this.id = _id;
            this.userName = _userName;
            this.authorityLevel = _authorityLevel;
            this.password = _password;
            this.phoneNumber = phoneNumber;
            this.ımageData = ımageData;
            this.userId = _userID;
            this.userBirthDate = _userBirthDate;
        }

        public String getId() { return id; }
        public String getUserName() { return userName; }
        public String getPassword() { return password; }
        public String getAuthorityLevel() { return authorityLevel; }
        public String getPhoneNumber() { return phoneNumber; }
        public byte[] getImageData() { return ımageData; }
        public String getUserId() { return userId; }
        public DateTime getUserBirthDate() {  return userBirthDate; }



    }
}

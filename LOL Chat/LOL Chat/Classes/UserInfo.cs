using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainFunc.Classes
{
    class UserInfo : IDisposable
    {


        public string userId;
        public string userName;
        public bool isActive;
        public DateTime lastSeen;
        public int lastMessageReceived;
        public UserInfo(string userId, string userName)
        {
            this.userId = userId;
            this.userName = userName;
            this.isActive = false;
            this.lastSeen = DateTime.MinValue;
            this.lastMessageReceived = 0;
        }
        public void Dispose()
        {
            this.userId = ""; 
            this.userName = "";
            this.isActive = false;
            this.lastSeen = DateTime.MinValue;
            this.lastMessageReceived = 0;
        }
    }
}

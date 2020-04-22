using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainFunc.Classes
{ 
    class SendMess 
    {
        public string user;
        public string msg;
        public MsgType msgType;
        public string sendTo;
        public SendMess(string _user,string _msg,MsgType _msgType,string _sendTo) 
        {
            this.user = _user;
            this.msg = _msg;
            this.msgType = _msgType;
            this.sendTo = _sendTo;
        }
        //string SendFrom() 
        //{
        
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainFunc.Classes
{
    class Message
    {
        public string user;
        public string msg; // message
        public MsgType type;
        public Message(string _user, string _msg, MsgType _type)
        {
            this.user = _user;
            this.msg = _msg;
            this.type = _type;
        }

        public override string ToString()
        {
            switch (this.type)
            {
                case MsgType.Msg:
                    return this.user + ": " + this.msg;
                case MsgType.Join:
                    return this.user + "has joined the room.";
                case MsgType.Left:
                    return this.user + "has left the room.";
            }
            return "";
        }
        public Message(string _user, MsgType _type) : this(_user, "", _type) { }
        public Message(MsgType _type) : this("", "", _type) { }
        
    }
    public enum MsgType { Msg, Start, Join, Left }
}

using System;

namespace Qarth
{
    public class Message
    {
        public MessageType type;
        public string route;
        public uint mid;
        public uint flag;
        public uint mainid;
        public uint subid;
        public byte[] data;

        public Message(MessageType type, uint mid, uint flag, uint mainid, uint subid, string route, byte[] data)
        {
            this.type = type;
            this.mid = mid;
            this.flag = flag;
            this.mainid = mainid;
            this.subid = subid;
            this.route = route;
            this.data = data;
        }
    }
}
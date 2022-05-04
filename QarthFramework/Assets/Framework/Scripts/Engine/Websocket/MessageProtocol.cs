using System;
using System.Text;
using System.Collections.Generic;

namespace Qarth
{
    public class MessageProtocol
    {
        private Dictionary<string, ushort> dict = new Dictionary<string, ushort>();
        private Dictionary<ushort, string> abbrs = new Dictionary<ushort, string>();
        private Dictionary<uint, string> reqMap;

        public const int MSG_Route_Limit = 255;
        public const int MSG_Route_Mask = 0x01;
        public const int MSG_Type_Mask = 0x0f;

        // public MessageProtocol(JsonObject dict)
        // {
        //     ICollection<string> keys = dict.Keys;

        //     foreach (string key in keys)
        //     {
        //         ushort value = Convert.ToUInt16(dict[key]);
        //         this.dict[key] = value;
        //         this.abbrs[value] = key;
        //     }

        //     this.reqMap = new Dictionary<uint, string>();
        // }

        // public static byte[] encode(string route, byte[] body)
        // {
        //     return encode(route, 0, body);
        // }
        //public byte[] encode(uint id, MessageType type, Int16 flag, )
        public static byte[] encode(uint mid, Int16 flag, Int16 mainid, Int16 subid, byte[] body)
        {
            int length = 5;

            if (body != null) length += body.Length;

            byte[] buf = new byte[length];

            int index = 0;
            buf[index++] = Convert.ToByte(mid);
            //buf[index++] = Convert.ToByte(0);
            //buf[index++] = ((byte)MessageType.MSG_REQUEST) << 4;
            buf[index++] = Convert.ToByte((int)MessageType.MSG_REQUEST << 4 | flag & 0x0F);
            buf[index++] = Convert.ToByte(mainid);
            buf[index++] = Convert.ToByte(subid >> 8 & 0xFF);
            buf[index++] = Convert.ToByte(subid & 0xFF);
            while (index < length)
            {
                buf[index] = body[index - 5];
                index++;
            }

            return buf;
        }
        // public static byte[] encode(string route, uint id, byte[] body)
        // {
        //     int routeLength = byteLength(route);
        //     if (routeLength > MSG_Route_Limit)
        //     {
        //         throw new Exception("Route is too long!");
        //     }

        //     //Encode head
        //     //The maximus length of head is 1 byte flag + 4 bytes message id + route string length + 1byte
        //     byte[] head = new byte[6];
        //     int offset = 1;
        //     byte flag = 0;

        //     if (id > 0)
        //     {
        //         byte[] bytes = Encoder.encodeUInt32(id);

        //         writeBytes(bytes, offset, head);
        //         flag |= ((byte)MessageType.MSG_REQUEST) << 1;
        //         offset += bytes.Length;
        //     }
        //     else
        //     {
        //         flag |= ((byte)MessageType.MSG_NOTIFY) << 1;
        //     }

        //     //Compress head
        //     if (dict.ContainsKey(route))
        //     {
        //         ushort cmpRoute = dict[route];
        //         writeShort(offset, cmpRoute, head);
        //         flag |= MSG_Route_Mask;
        //         offset += 2;
        //     }
        //     else
        //     {
        //         //Write route length
        //         head[offset++] = (byte)routeLength;

        //         //Write route
        //         writeBytes(Encoding.UTF8.GetBytes(route), offset, head);
        //         offset += routeLength;
        //     }

        //     head[0] = flag;

        //     //Construct the result
        //     byte[] result = new byte[offset + body.Length];
        //     for (int i = 0; i < offset; i++)
        //     {
        //         result[i] = head[i];
        //     }

        //     for (int i = 0; i < body.Length; i++)
        //     {
        //         result[offset + i] = body[i];
        //     }

        //     //Add id to route map
        //     //if (id > 0) reqMap.Add(id, route);

        //     return result;
        // }

        public static Message decode(byte[] buffer)
        {
            //Decode head
            byte mid = buffer[0];
            //Get flag
            byte flag = buffer[1];
            //Set offset to 1, for the 1st byte will always be the flag
            int offset = 5;

            //Get type from flag;
            MessageType type = (MessageType)((flag >> 4) & MSG_Type_Mask);
            flag = (byte)(flag & MSG_Type_Mask);
            uint mainid = buffer[2];
            uint subid = ((uint)buffer[3] << 8) + buffer[4];
            string[] typeString = new string[] { "F", "S", "C", "N", "P" };//C 2 N 3 S 1 P 4
            string route = string.Format("{0}{1:D1}{2:X2}{3:X4}", typeString[(uint)type], flag, mainid, subid);
            //Decode body
            byte[] body = new byte[buffer.Length - offset];
            for (int i = 0; i < body.Length; i++)
            {
                body[i] = buffer[i + offset];
            }
            //Construct the message
            return new Message(type, mid, flag, mainid, subid, route, body);
        }
        // public Message decode(byte[] buffer)
        // {
        //     //Decode head
        //     //Get flag
        //     byte flag = buffer[0];
        //     //Set offset to 1, for the 1st byte will always be the flag
        //     int offset = 1;

        //     //Get type from flag;
        //     MessageType type = (MessageType)((flag >> 1) & MSG_Type_Mask);
        //     uint id = 0;
        //     string route;

        //     if (type == MessageType.MSG_RESPONSE)
        //     {
        //         int length;
        //         id = (uint)Decoder.decodeUInt32(offset, buffer, out length);
        //         // if (id <= 0 || !reqMap.ContainsKey(id))
        //         // {
        //         //     return null;
        //         // }
        //         // else
        //         // {
        //         //     route = reqMap[id];
        //         //     reqMap.Remove(id);
        //         // }
        //         route = "";

        //         offset += length;
        //     }
        //     else if (type == MessageType.MSG_PUSH)
        //     {
        //         //Get route
        //         if ((flag & 0x01) == 1)
        //         {
        //             ushort routeId = readShort(offset, buffer);
        //             route = abbrs[routeId];

        //             offset += 2;
        //         }
        //         else
        //         {
        //             byte length = buffer[offset];
        //             offset += 1;

        //             route = Encoding.UTF8.GetString(buffer, offset, length);
        //             offset += length;
        //         }
        //     }
        //     else
        //     {
        //         return null;
        //     }

        //     //Decode body
        //     byte[] body = new byte[buffer.Length - offset];
        //     for (int i = 0; i < body.Length; i++)
        //     {
        //         body[i] = buffer[i + offset];
        //     }

        //     //Construct the message
        //     return new Message(type, id, route, body);
        // }

        private void writeInt(int offset, uint value, byte[] bytes)
        {
            bytes[offset] = (byte)(value >> 24 & 0xff);
            bytes[offset + 1] = (byte)(value >> 16 & 0xff);
            bytes[offset + 2] = (byte)(value >> 8 & 0xff);
            bytes[offset + 3] = (byte)(value & 0xff);
        }

        private void writeShort(int offset, ushort value, byte[] bytes)
        {
            bytes[offset] = (byte)(value >> 8 & 0xff);
            bytes[offset + 1] = (byte)(value & 0xff);
        }

        private ushort readShort(int offset, byte[] bytes)
        {
            ushort result = 0;

            result += (ushort)(bytes[offset] << 8);
            result += (ushort)(bytes[offset + 1]);

            return result;
        }

        private int byteLength(string msg)
        {
            return Encoding.UTF8.GetBytes(msg).Length;
        }

        private void writeBytes(byte[] source, int offset, byte[] target)
        {
            for (int i = 0; i < source.Length; i++)
            {
                target[offset + i] = source[i];
            }
        }
    }
}
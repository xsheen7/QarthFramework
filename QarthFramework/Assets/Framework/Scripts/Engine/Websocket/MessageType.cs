using System;

namespace Qarth
{
    public enum MessageType
    {
        MSG_REQUEST = 0x02,
        MSG_NOTIFY = 0x03,
        MSG_RESPONSE = 0x01,
        MSG_PUSH = 0x04
    }
}
using System;

namespace Qarth
{
    public enum PackageType
    {
        PKG_DATA = 0,
        PKG_HANDSHAKE = 1,
        PKG_HANDSHAKE_ACK = 2,
        PKG_HEARTBEAT = 3,
        PKG_DATA_CLOSE = 4,
        PKG_KICK = 5
    }
}
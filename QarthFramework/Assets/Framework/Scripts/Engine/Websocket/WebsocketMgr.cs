using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BestHTTP.WebSocket;
using BestHTTP.WebSocket.Frames;


namespace Qarth
{
    public class WebsocketMgr : TMonoSingleton<WebsocketMgr>
    {
        WebSocket m_Websocket;
        private uint m_Mid = 0;
        private int m_ConcentCount = 0;
        public override void OnSingletonInit()
        {
            m_Mid = 0;
            m_ConcentCount = 0;
        }
        public void Connect()
        {
            if (m_Websocket == null)
            {
                Log.e("WebSocket connect");
                m_Websocket = new WebSocket(new Uri(AppConfig.S.websocketUri));
                //webSocket = new WebSocket(uri: new Uri("wss://echo.websocket.org/"), origin: "echo.websocket.org", protocol: "Echo", extensions: null);

#if !UNITY_WEBGL || UNITY_EDITOR
                m_Websocket.StartPingThread = true;
#endif

                // Subscribe to the WS events
                m_Websocket.OnOpen += OnWebSocketOpen;
                m_Websocket.OnMessage += OnMessageRecv;
                m_Websocket.OnBinary += OnBinaryRecv;
                m_Websocket.OnClosed += OnClosed;
                m_Websocket.OnError += OnError;

                // Start connecting to the server
                m_Websocket.Open();
            }
        }
        public void DestroySocket()
        {
            m_Websocket.Close();
            m_Websocket = null;
        }

        public void Destroy()
        {
            if (m_Websocket != null)
            {
                m_Websocket.Close();
                m_Websocket = null;
            }
        }
        public void SendMsg(string msg)
        {
            if (m_Websocket != null)
                m_Websocket.Send(msg);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="flag">0 系统 1 通用 3 游戏</param>
        /// <param name="mainid">游戏kind id</param>
        /// <param name="subid">协议子id</param>
        /// <param name="body">proto数据</param>
        public void SendMsg(Int16 flag, Int16 mainid, Int16 subid, byte[] body)
        {
            if (m_Websocket != null)
            {
                uint id = (m_Mid++) % 253 + 1;
                var msg = MessageProtocol.encode(id, flag, mainid, subid, body);
                var pkg = PackageProtocol.encode(PackageType.PKG_DATA, msg);
                //Log.e(BitConverter.ToString(msg));
                //Log.e(msg.Length);
                //Log.e(BitConverter.ToString(pkg));
                m_Websocket.Send(pkg);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="protoal">协议id （proto类名）</param>
        /// <param name="body">proto数据</param>
        public void SendMsg(string protoal, byte[] body)
        {
            if (m_Websocket != null)
            {
                var flag = Convert.ToInt16(protoal.Substring(1, 1), 16);
                var mainid = Convert.ToInt16(protoal.Substring(2, 2), 16); ;
                var subid = Convert.ToInt16(protoal.Substring(4, 4), 16); ;
                uint id = (m_Mid++) % 253 + 1;
                var msg = MessageProtocol.encode(id, flag, mainid, subid, body);
                var pkg = PackageProtocol.encode(PackageType.PKG_DATA, msg);
                //Log.e(BitConverter.ToString(msg));
                //Log.e(msg.Length);
                //Log.e(BitConverter.ToString(pkg));
                m_Websocket.Send(pkg);
            }
        }

        void OnWebSocketOpen(WebSocket webSocket)
        {
            Log.e("WebSocket is now Open!");
            m_ConcentCount++;
            EventSystem.S.Send(EngineEventID.OnWebSocketOpen, m_ConcentCount);
        }

        void OnMessageRecv(WebSocket webSocket, string message)
        {
            Log.e("OnMessageRecv: msg={0}", message);
            EventSystem.S.Send(EngineEventID.OnWebsocketStringMsg, message);
        }

        void OnBinaryRecv(WebSocket webSocket, byte[] data)
        {
            Log.i("OnBinaryRecv: len={0}", data.Length);
            var pkg = PackageProtocol.decode(data);
            if (pkg.type == PackageType.PKG_HEARTBEAT)
            {
                SendHeartPkg();
            }
            else
            {
                EventSystem.S.Send(EngineEventID.OnWebsocketBinaryRecv, pkg.body);
            }
        }

        void OnClosed(WebSocket webSocket, UInt16 code, string message)
        {
            Log.e("OnClosed: code={0}, msg={1}", code, message);
            webSocket = null;
            m_Websocket = null;
        }

        void OnError(WebSocket webSocket, string reason)
        {
            string errorMsg = string.Empty;
#if !UNITY_WEBGL || UNITY_EDITOR
            if (webSocket.InternalRequest.Response != null)
            {
                errorMsg = string.Format("Status Code from Server: {0} and Message: {1}", webSocket.InternalRequest.Response.StatusCode, webSocket.InternalRequest.Response.Message);
            }
#endif
            Log.e("OnError: error occured: {0}\n", (reason != null ? reason : "Unknown Error " + errorMsg));

            webSocket = null;
            DestroySocket();
            //Connect();
            EventSystem.S.Send(EngineEventID.OnWebsocketError);
            Timer.S.CallWithDelay(() =>
            {
                WebsocketMgr.S.Connect();
            }, 1f);
        }

        void SendHeartPkg()
        {
            var heart = PackageProtocol.encode(PackageType.PKG_HEARTBEAT, new byte[] { });
            m_Websocket.Send(heart);
        }
    }

}
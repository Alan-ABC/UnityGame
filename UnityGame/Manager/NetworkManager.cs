using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityGameToolkit
{
    public class NetworkManager : SingleManagerBase
    {
        private SocketClient socket;
        static readonly object m_lockObject = new object();
        static Queue<KeyValuePair<int, ByteBuffer>> mEvents = new Queue<KeyValuePair<int, ByteBuffer>>();

        SocketClient SocketClient
        {
            get
            { 
                if (socket == null)
                    socket = new SocketClient();
                return socket;                    
            }
        }

        void Awake()
        {
            Init();
        }

        void Init()
        {
            SocketClient.OnRegister();
        }

        ///------------------------------------------------------------------------------------
        public static void AddEvent(int _event, ByteBuffer data)
        {
            lock (m_lockObject)
            {
                mEvents.Enqueue(new KeyValuePair<int, ByteBuffer>(_event, data));
            }
        }

        /// <summary>
        /// ����Command�����ﲻ����ķ���˭��
        /// </summary>
        void Update()
        {
            if (mEvents.Count > 0)
            {
                while (mEvents.Count > 0)
                {
                    KeyValuePair<int, ByteBuffer> _event = mEvents.Dequeue();
                    //facade.SendMessageCommand(NotiConst.DISPATCH_MESSAGE, _event);
                }
            }
        }

        /// <summary>
        /// ������������
        /// </summary>
        public void SendConnect()
        {
            SocketClient.SendConnect();
        }

        /// <summary>
        /// ����SOCKET��Ϣ
        /// </summary>
        public void SendMessage(ByteBuffer buffer)
        {
            SocketClient.SendMessage(buffer);
        }

        /// <summary>
        /// ��������
        /// </summary>
        public override void Destroy()
        {
            base.Destroy();
            SocketClient.OnRemove();
            Debug.Log("~NetworkManager was destroy");
        }
    }
}
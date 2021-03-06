﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XSocketsClient.Protocol.Readers
{
    public abstract class XFrameHandler : IXFrameHandler
    {
        private readonly List<byte> _data = new List<byte>();

        public Action<List<byte>> ReceiveData = delegate { };

        public void Receive(IEnumerable<byte> data)
        {
            _data.AddRange(data);
            ReceiveData(_data);
        }
    }

    public class Rfc6455FrameHandler : XFrameHandler
    {
    }
}
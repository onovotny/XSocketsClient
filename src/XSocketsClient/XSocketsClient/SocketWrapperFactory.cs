﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
//using Org.BouncyCastle.Pkcs;
using XSocketsClient.Common.Interfaces;

namespace XSocketsClient
{
    internal static class SocketWrapperFactory
    {
        private const string PlatformAssembly = "XSocketsClient.Platform";
        private static readonly Lazy<Type> _socketWrapperType = new Lazy<Type>(() =>
        {
            var an = typeof(SocketWrapperFactory).GetTypeInfo().Assembly.GetName();
            an.Name = PlatformAssembly;

            var assm = Assembly.Load(an);

            var type = assm.GetType("XSocketsClient.Wrapper.SocketWrapper");

            return type;
        });

        public static async Task<ISocketWrapper> ConnectToSocketAsync(Uri host, string origin, string protocol)
        {

//            object[] args = certificate == null ? null : new[] {certificate};
            object[] args = null;

            var obj = Activator.CreateInstance(_socketWrapperType.Value, args) as ISocketWrapper;
            if (obj == null)
                throw new Exception("Platform assembly not found. Ensure that XSocketsClient.Platform is present");

            await obj.ConnectAsync(host, origin, protocol).ConfigureAwait(false);

            return obj;
        }
    }
}

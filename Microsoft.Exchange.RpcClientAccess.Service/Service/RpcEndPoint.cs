using System;
using System.Collections.Generic;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.RpcClientAccess.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Service
{
	// Token: 0x02000004 RID: 4
	internal static class RpcEndPoint
	{
		// Token: 0x06000004 RID: 4 RVA: 0x0000214B File Offset: 0x0000034B
		internal static void AddTcpPort(string port)
		{
			RpcEndPoint.AddProtocolAndPort("ncacn_ip_tcp", port);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002158 File Offset: 0x00000358
		internal static void AddHttpPort(string port)
		{
			RpcEndPoint.AddProtocolAndPort("ncacn_http", port);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002165 File Offset: 0x00000365
		internal static void EnableLrpc()
		{
			RpcEndPoint.AddProtocolAndPort("ncalrpc", null);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002174 File Offset: 0x00000374
		internal static void AddServer(Action starter, Action stopper)
		{
			Util.ThrowOnNullArgument(starter, "starter");
			Util.ThrowOnNullArgument(stopper, "stopper");
			lock (RpcEndPoint.initializeLock)
			{
				RpcEndPoint.servers.Add(new RpcEndPoint.ServerActions(starter, stopper));
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021DC File Offset: 0x000003DC
		internal static void Start()
		{
			bool flag = false;
			lock (RpcEndPoint.initializeLock)
			{
				if (!RpcEndPoint.endpointRegistered)
				{
					try
					{
						Feature.TraceContextGetter = (() => Activity.TraceId);
						RpcServerBase.RegisterProtocols(RpcEndPoint.protocolSequences.ToArray(), RpcEndPoint.protocolPorts.ToArray());
						RpcEndPoint.endpointRegistered = true;
						foreach (RpcEndPoint.ServerActions serverActions in RpcEndPoint.servers)
						{
							serverActions.Starter();
						}
						flag = true;
					}
					finally
					{
						if (!flag)
						{
							RpcEndPoint.Stop();
						}
					}
				}
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000022C4 File Offset: 0x000004C4
		internal static void Stop()
		{
			if (RpcEndPoint.endpointRegistered)
			{
				lock (RpcEndPoint.initializeLock)
				{
					if (RpcEndPoint.endpointRegistered)
					{
						foreach (RpcEndPoint.ServerActions serverActions in RpcEndPoint.servers)
						{
							serverActions.Stopper();
						}
						RpcEndPoint.endpointRegistered = false;
					}
				}
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002358 File Offset: 0x00000558
		internal static Exception HandleRpcServiceException(RpcServiceException rpcServiceException)
		{
			RpcServerBase.ThrowRpcException(rpcServiceException.Message, rpcServiceException.RpcStatus);
			return new InvalidOperationException("This code should not have been executed after RaiseRpcException at runtime.");
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002378 File Offset: 0x00000578
		private static void AddProtocolAndPort(string protocol, string port)
		{
			Util.ThrowOnNullArgument(protocol, "protocol");
			lock (RpcEndPoint.initializeLock)
			{
				bool flag2 = false;
				int num = 0;
				while (num < RpcEndPoint.protocolSequences.Count && !flag2)
				{
					flag2 = (string.Compare(protocol, RpcEndPoint.protocolSequences[num], true) == 0 && string.Compare(port, RpcEndPoint.protocolPorts[num], true) == 0);
					num++;
				}
				if (!flag2)
				{
					RpcEndPoint.protocolSequences.Add(protocol);
					RpcEndPoint.protocolPorts.Add(port);
				}
			}
		}

		// Token: 0x04000001 RID: 1
		private const string ProtocolTCP = "ncacn_ip_tcp";

		// Token: 0x04000002 RID: 2
		private const string ProtocolHTTP = "ncacn_http";

		// Token: 0x04000003 RID: 3
		private const string ProtocolLRPC = "ncalrpc";

		// Token: 0x04000004 RID: 4
		private static readonly object initializeLock = new object();

		// Token: 0x04000005 RID: 5
		private static bool endpointRegistered = false;

		// Token: 0x04000006 RID: 6
		private static List<string> protocolSequences = new List<string>(2);

		// Token: 0x04000007 RID: 7
		private static List<string> protocolPorts = new List<string>(2);

		// Token: 0x04000008 RID: 8
		private static List<RpcEndPoint.ServerActions> servers = new List<RpcEndPoint.ServerActions>(2);

		// Token: 0x02000005 RID: 5
		private struct ServerActions
		{
			// Token: 0x0600000E RID: 14 RVA: 0x0000244F File Offset: 0x0000064F
			internal ServerActions(Action starter, Action stopper)
			{
				this.Starter = starter;
				this.Stopper = stopper;
			}

			// Token: 0x0400000A RID: 10
			internal Action Starter;

			// Token: 0x0400000B RID: 11
			internal Action Stopper;
		}
	}
}

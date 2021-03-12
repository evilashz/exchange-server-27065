using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.ExchangeServer;
using Microsoft.Exchange.RpcClientAccess.Messages;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000046 RID: 70
	internal sealed class RpcServer : ExchangeAsyncRpcServer_EMSMDB
	{
		// Token: 0x0600025E RID: 606 RVA: 0x0000D089 File Offset: 0x0000B289
		internal static void Initialize(IExchangeAsyncDispatch exchangeAsyncDispatch, int maximumConcurrentCalls, ExEventLog eventLog)
		{
			Util.ThrowOnNullArgument(exchangeAsyncDispatch, "exchangeAsyncDispatch");
			Util.ThrowOnNullArgument(eventLog, "eventLog");
			RpcServer.exchangeAsyncDispatch = exchangeAsyncDispatch;
			RpcServer.eventLog = eventLog;
			RpcServer.maximumConcurrentCalls = maximumConcurrentCalls;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000D0B4 File Offset: 0x0000B2B4
		internal static void Start()
		{
			bool flag = false;
			if (RpcServer.server == null)
			{
				try
				{
					RpcServer.server = (RpcServer)RpcServerBase.RegisterAutoListenInterfaceSupportingAnonymous(typeof(RpcServer), RpcServer.maximumConcurrentCalls, null, true);
					RpcServer.server.StartRundownQueue();
					flag = true;
				}
				catch (DuplicateRpcEndpointException ex)
				{
					RpcServer.eventLog.LogEvent(RpcClientAccessServiceEventLogConstants.Tuple_DuplicateRpcEndpoint, string.Empty, new object[]
					{
						ex.Message
					});
					throw new RpcServiceAbortException("RpcServer is being aborted the service due to DuplicateRpcEndpointException", ex);
				}
				finally
				{
					if (!flag)
					{
						RpcServer.Stop();
						RpcServer.exchangeAsyncDispatch = null;
					}
				}
			}
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000D158 File Offset: 0x0000B358
		internal static void Stop()
		{
			if (RpcServer.server != null)
			{
				RpcServerBase.UnregisterInterface(ExchangeAsyncRpcServer_EMSMDB.RpcIntfHandle, true);
				RpcServer.server.StopRundownQueue();
				RpcServer.server = null;
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000D17C File Offset: 0x0000B37C
		public override IExchangeAsyncDispatch GetAsyncDispatch()
		{
			if (RpcClientAccessService.IsShuttingDown)
			{
				return null;
			}
			return RpcServer.exchangeAsyncDispatch;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000D18C File Offset: 0x0000B38C
		public override ushort GetVersionDelta()
		{
			return 4000;
		}

		// Token: 0x0400013B RID: 315
		private static RpcServer server = null;

		// Token: 0x0400013C RID: 316
		private static IExchangeAsyncDispatch exchangeAsyncDispatch = null;

		// Token: 0x0400013D RID: 317
		private static int maximumConcurrentCalls;

		// Token: 0x0400013E RID: 318
		private static ExEventLog eventLog;
	}
}

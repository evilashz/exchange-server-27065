using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.ExchangeServer;
using Microsoft.Exchange.RpcClientAccess.Messages;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000037 RID: 55
	internal sealed class RpcAsynchronousServer : ExchangeAsyncRpcServer_AsyncEMSMDB
	{
		// Token: 0x0600019A RID: 410 RVA: 0x00007EAE File Offset: 0x000060AE
		internal static void Initialize(IExchangeAsyncDispatch exchangeAsyncDispatch, int maximumConcurrentCalls, ExEventLog eventLog)
		{
			Util.ThrowOnNullArgument(exchangeAsyncDispatch, "exchangeAsyncDispatch");
			Util.ThrowOnNullArgument(eventLog, "eventLog");
			RpcAsynchronousServer.exchangeAsyncDispatch = exchangeAsyncDispatch;
			RpcAsynchronousServer.eventLog = eventLog;
			RpcAsynchronousServer.maximumConcurrentCalls = maximumConcurrentCalls;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00007ED8 File Offset: 0x000060D8
		internal static void Start()
		{
			bool flag = false;
			if (RpcAsynchronousServer.server == null)
			{
				try
				{
					RpcAsynchronousServer.server = (RpcAsynchronousServer)RpcServerBase.RegisterAutoListenInterfaceSupportingAnonymous(typeof(RpcAsynchronousServer), RpcAsynchronousServer.maximumConcurrentCalls, null, true);
					RpcAsynchronousServer.server.StartDroppedConnectionNotificationThread();
					RpcAsynchronousServer.server.StartRundownQueue();
					flag = true;
				}
				catch (DuplicateRpcEndpointException ex)
				{
					RpcAsynchronousServer.eventLog.LogEvent(RpcClientAccessServiceEventLogConstants.Tuple_DuplicateRpcEndpoint, string.Empty, new object[]
					{
						ex.Message
					});
					throw new RpcServiceAbortException("RpcAsynchronousServer is being aborted the service due to DuplicateRpcEndpointException", ex);
				}
				finally
				{
					if (!flag)
					{
						RpcAsynchronousServer.Stop();
						RpcAsynchronousServer.exchangeAsyncDispatch = null;
					}
				}
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00007F88 File Offset: 0x00006188
		internal static void Stop()
		{
			if (RpcAsynchronousServer.server != null)
			{
				RpcAsynchronousServer.server.StopDroppedConnectionNotificationThread();
				RpcServerBase.UnregisterInterface(ExchangeAsyncRpcServer_AsyncEMSMDB.RpcIntfHandle);
				RpcAsynchronousServer.server.StopRundownQueue();
				RpcAsynchronousServer.server = null;
			}
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00007FB5 File Offset: 0x000061B5
		public override IExchangeAsyncDispatch GetAsyncDispatch()
		{
			if (RpcClientAccessService.IsShuttingDown)
			{
				return null;
			}
			return RpcAsynchronousServer.exchangeAsyncDispatch;
		}

		// Token: 0x040000CF RID: 207
		private static RpcAsynchronousServer server = null;

		// Token: 0x040000D0 RID: 208
		private static IExchangeAsyncDispatch exchangeAsyncDispatch = null;

		// Token: 0x040000D1 RID: 209
		private static int maximumConcurrentCalls;

		// Token: 0x040000D2 RID: 210
		private static ExEventLog eventLog;
	}
}

using System;
using System.ServiceModel;
using Microsoft.Exchange.Data.Storage.Authentication;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.RpcClientAccess;
using Microsoft.Exchange.Net.XropService;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.RpcClientAccess.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Messages;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000052 RID: 82
	internal static class WebServiceEndPoint
	{
		// Token: 0x060002F6 RID: 758 RVA: 0x0000FE3C File Offset: 0x0000E03C
		internal static void Start(IExchangeAsyncDispatch exchangeAsyncDispatch, string endpoint, ExEventLog eventLog)
		{
			Util.ThrowOnNullArgument(exchangeAsyncDispatch, "exchangeAsyncDispatch");
			Util.ThrowOnNullArgument(endpoint, "endpoint");
			Util.ThrowOnNullArgument(eventLog, "eventLog");
			lock (WebServiceEndPoint.initializeLock)
			{
				if (!WebServiceEndPoint.endpointRegistered)
				{
					try
					{
						WebServiceEndPoint.exchangeAsyncDispatch = exchangeAsyncDispatch;
						ExternalAuthentication current = ExternalAuthentication.GetCurrent();
						if (current != null && current.Enabled)
						{
							Uri endpoint2 = new Uri(endpoint);
							try
							{
								Server.InitializeGlobalErrorHandlers(new WebServiceEndPoint.WebServiceDiagnosticsInfo());
								WebServiceEndPoint.server = new Server(endpoint2, current.TokenValidator, new WebServiceAuthorizationManager(), new WebServiceServerSessionProvider(), new WebServiceEndPoint.WebServiceDiagnosticsInfo());
								eventLog.LogEvent(RpcClientAccessServiceEventLogConstants.Tuple_WebServiceEndPointRegistered, string.Empty, new object[]
								{
									endpoint
								});
								goto IL_D8;
							}
							catch (AddressAccessDeniedException ex)
							{
								eventLog.LogEvent(RpcClientAccessServiceEventLogConstants.Tuple_CannotRegisterEndPointAccessDenied, ex.ToString(), new object[0]);
								goto IL_D8;
							}
						}
						eventLog.LogEvent(RpcClientAccessServiceEventLogConstants.Tuple_FederatedAuthentication, string.Empty, new object[0]);
						IL_D8:
						WebServiceEndPoint.endpointRegistered = true;
					}
					finally
					{
						if (!WebServiceEndPoint.endpointRegistered)
						{
							WebServiceEndPoint.Stop();
						}
					}
				}
			}
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000FF6C File Offset: 0x0000E16C
		internal static void Stop()
		{
			lock (WebServiceEndPoint.initializeLock)
			{
				Server.TerminateGlobalErrorHandlers();
				if (WebServiceEndPoint.endpointRegistered)
				{
					if (WebServiceEndPoint.server != null)
					{
						WebServiceEndPoint.server.Dispose();
						WebServiceEndPoint.server = null;
					}
					WebServiceEndPoint.exchangeAsyncDispatch = null;
					WebServiceEndPoint.endpointRegistered = false;
				}
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000FFD4 File Offset: 0x0000E1D4
		internal static IExchangeAsyncDispatch Dispatch
		{
			get
			{
				return WebServiceEndPoint.exchangeAsyncDispatch;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0000FFDB File Offset: 0x0000E1DB
		// (set) Token: 0x060002FA RID: 762 RVA: 0x0000FFE2 File Offset: 0x0000E1E2
		internal static bool IsShuttingDown
		{
			get
			{
				return WebServiceEndPoint.isShuttingDown;
			}
			set
			{
				WebServiceEndPoint.isShuttingDown = value;
			}
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000FFEC File Offset: 0x0000E1EC
		internal static void LogFailure(string message, Exception exception, string emailAddress, string domain, string organization, Trace trace)
		{
			string protocolSequence;
			if (string.IsNullOrEmpty(emailAddress))
			{
				if (string.IsNullOrEmpty(domain))
				{
					protocolSequence = RpcDispatch.WebServiceProtocolSequencePrefix;
				}
				else
				{
					protocolSequence = RpcDispatch.WebServiceProtocolSequencePrefix + domain;
				}
			}
			else
			{
				protocolSequence = string.Format("{0}{1}[{0}{2}]", RpcDispatch.WebServiceProtocolSequencePrefix, domain, emailAddress);
			}
			ProtocolLog.LogWebServiceFailure("xrop: failure", message ?? string.Empty, exception, emailAddress ?? string.Empty, organization ?? string.Empty, protocolSequence, trace);
		}

		// Token: 0x0400017E RID: 382
		private static readonly object initializeLock = new object();

		// Token: 0x0400017F RID: 383
		private static bool endpointRegistered = false;

		// Token: 0x04000180 RID: 384
		private static bool isShuttingDown;

		// Token: 0x04000181 RID: 385
		private static Server server = null;

		// Token: 0x04000182 RID: 386
		private static IExchangeAsyncDispatch exchangeAsyncDispatch = null;

		// Token: 0x02000053 RID: 83
		internal sealed class WebServiceDiagnosticsInfo : IServerDiagnosticsHandler
		{
			// Token: 0x060002FD RID: 765 RVA: 0x0001007C File Offset: 0x0000E27C
			public void AnalyseException(ref Exception exception)
			{
			}

			// Token: 0x060002FE RID: 766 RVA: 0x0001007E File Offset: 0x0000E27E
			public void LogException(Exception exception)
			{
				ProtocolLog.LogWebServiceFailure("xrop: WCF exception", null, exception, null, null, RpcDispatch.WebServiceProtocolSequencePrefix, ExTraceGlobals.ConnectXropTracer);
			}

			// Token: 0x060002FF RID: 767 RVA: 0x00010098 File Offset: 0x0000E298
			public void LogMessage(string message)
			{
				ProtocolLog.LogWebServiceFailure("xrop: WCF message", message, null, null, null, RpcDispatch.WebServiceProtocolSequencePrefix, ExTraceGlobals.ConnectXropTracer);
			}
		}
	}
}

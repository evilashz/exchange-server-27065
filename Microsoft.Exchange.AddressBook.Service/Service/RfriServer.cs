using System;
using Microsoft.Exchange.AddressBook.EventLog;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.AddressBook.Service;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.RfriServer;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x02000049 RID: 73
	internal sealed class RfriServer : RfriAsyncRpcServer
	{
		// Token: 0x06000319 RID: 793 RVA: 0x00013888 File Offset: 0x00011A88
		internal static void Initialize(IRpcServiceManager serviceManager, ExEventLog eventLog)
		{
			Util.ThrowOnNullArgument(serviceManager, "serviceManager");
			Util.ThrowOnNullArgument(eventLog, "eventLog");
			RfriServer.rfriAsyncDispatch = new RfriAsyncDispatch();
			RfriServer.eventLog = eventLog;
			serviceManager.AddServer(new Action(RfriServer.Start), new Action(RfriServer.Stop));
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600031A RID: 794 RVA: 0x000138D9 File Offset: 0x00011AD9
		internal static RfriServer Instance
		{
			get
			{
				return RfriServer.instance;
			}
		}

		// Token: 0x0600031B RID: 795 RVA: 0x000138E0 File Offset: 0x00011AE0
		internal static void Start()
		{
			ServerFqdnCache.InitializeCache();
			if (RfriServer.instance == null)
			{
				bool flag = false;
				try
				{
					RfriServer.instance = (RfriServer)RpcServerBase.RegisterAutoListenInterfaceSupportingAnonymous(typeof(RfriServer), RpcServerBase.DefaultMaxRpcCalls, "Microsoft Exchange RFR Interface", true);
					flag = true;
				}
				catch (RpcException ex)
				{
					RfriServer.ReferralTracer.TraceError<string>(0L, "Error registering the RFR RPC interface: {0}", ex.Message);
					RfriServer.eventLog.LogEvent(AddressBookEventLogConstants.Tuple_RpcRegisterInterfaceFailure, string.Empty, new object[]
					{
						"RFR",
						ServiceHelper.FormatWin32ErrorString(ex.ErrorCode)
					});
				}
				finally
				{
					if (!flag)
					{
						RfriServer.rfriAsyncDispatch = null;
						RfriServer.Stop();
						RfriServer.instance = null;
					}
				}
			}
		}

		// Token: 0x0600031C RID: 796 RVA: 0x000139A8 File Offset: 0x00011BA8
		internal static void Stop()
		{
			if (RfriServer.instance != null)
			{
				RpcServerBase.UnregisterInterface(RfriAsyncRpcServer.RpcIntfHandle, true);
				RfriServer.instance = null;
			}
			ServerFqdnCache.TerminateCache();
		}

		// Token: 0x0600031D RID: 797 RVA: 0x000139C7 File Offset: 0x00011BC7
		internal static void ShuttingDown()
		{
			if (RfriServer.instance != null && RfriServer.rfriAsyncDispatch != null)
			{
				RfriServer.rfriAsyncDispatch.ShuttingDown();
			}
		}

		// Token: 0x0600031E RID: 798 RVA: 0x000139E1 File Offset: 0x00011BE1
		public override IRfriAsyncDispatch GetAsyncDispatch()
		{
			return RfriServer.rfriAsyncDispatch;
		}

		// Token: 0x040001B8 RID: 440
		private static readonly Trace ReferralTracer = ExTraceGlobals.ReferralTracer;

		// Token: 0x040001B9 RID: 441
		private static RfriServer instance = null;

		// Token: 0x040001BA RID: 442
		private static RfriAsyncDispatch rfriAsyncDispatch = null;

		// Token: 0x040001BB RID: 443
		private static ExEventLog eventLog;
	}
}

using System;
using Microsoft.Exchange.AddressBook.EventLog;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.AddressBook.Service;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.NspiServer;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x02000039 RID: 57
	internal sealed class NspiServer : NspiAsyncRpcServer
	{
		// Token: 0x0600026C RID: 620 RVA: 0x000113BC File Offset: 0x0000F5BC
		internal static void Initialize(IRpcServiceManager serviceManager, ExEventLog eventLog)
		{
			Util.ThrowOnNullArgument(serviceManager, "serviceManager");
			Util.ThrowOnNullArgument(eventLog, "eventLog");
			NspiServer.nspiAsyncDispatch = new NspiAsyncDispatch();
			NspiServer.eventLog = eventLog;
			serviceManager.AddServer(new Action(NspiServer.Start), new Action(NspiServer.Stop));
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0001140D File Offset: 0x0000F60D
		internal static NspiServer Instance
		{
			get
			{
				return NspiServer.instance;
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00011414 File Offset: 0x0000F614
		internal static void Start()
		{
			if (NspiServer.instance == null)
			{
				bool flag = false;
				try
				{
					NspiServer.instance = (NspiServer)RpcServerBase.RegisterAutoListenInterfaceSupportingAnonymous(typeof(NspiServer), RpcServerBase.DefaultMaxRpcCalls, "Microsoft Exchange NSPI Interface", false);
					NspiServer.instance.StartRundownQueue();
					flag = true;
				}
				catch (RpcException ex)
				{
					NspiServer.NspiTracer.TraceError<string>(0L, "Error registering the NSPI RPC interface: {0}", ex.Message);
					NspiServer.eventLog.LogEvent(AddressBookEventLogConstants.Tuple_RpcRegisterInterfaceFailure, string.Empty, new object[]
					{
						"NSPI",
						ServiceHelper.FormatWin32ErrorString(ex.ErrorCode)
					});
				}
				finally
				{
					if (!flag)
					{
						NspiServer.nspiAsyncDispatch = null;
						NspiServer.Stop();
						NspiServer.instance = null;
					}
				}
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x000114E0 File Offset: 0x0000F6E0
		internal static void Stop()
		{
			if (NspiServer.instance != null)
			{
				RpcServerBase.UnregisterInterface(NspiAsyncRpcServer.RpcIntfHandle);
				NspiServer.instance.StopRundownQueue();
				NspiServer.instance = null;
			}
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00011503 File Offset: 0x0000F703
		internal static void ShuttingDown()
		{
			if (NspiServer.instance != null && NspiServer.nspiAsyncDispatch != null)
			{
				NspiServer.nspiAsyncDispatch.ShuttingDown();
			}
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0001151D File Offset: 0x0000F71D
		public override INspiAsyncDispatch GetAsyncDispatch()
		{
			return NspiServer.nspiAsyncDispatch;
		}

		// Token: 0x0400017C RID: 380
		private static readonly Trace NspiTracer = ExTraceGlobals.NspiTracer;

		// Token: 0x0400017D RID: 381
		private static NspiServer instance = null;

		// Token: 0x0400017E RID: 382
		private static NspiAsyncDispatch nspiAsyncDispatch = null;

		// Token: 0x0400017F RID: 383
		private static ExEventLog eventLog;
	}
}

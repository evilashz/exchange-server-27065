using System;
using Microsoft.Exchange.AddressBook.EventLog;
using Microsoft.Exchange.AddressBook.Nspi;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.AddressBook.Service;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x02000002 RID: 2
	internal sealed class AddressBookService : BaseObject, IRpcService, IDisposable
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public AddressBookService(IRpcServiceManager serviceManager)
		{
			this.eventLog = new ExEventLog(AddressBookService.ComponentGuid, "MSExchangeAB");
			this.serviceManager = serviceManager;
			string userName = Environment.UserName;
			AddressBookService.GeneralTracer.TraceDebug<string>(0L, "Running as {0}", userName);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002117 File Offset: 0x00000317
		public string Name
		{
			get
			{
				return "MSExchangeAB";
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x0000211E File Offset: 0x0000031E
		internal static MovingAveragePerfCounter NspiRpcRequestsAverageLatency
		{
			get
			{
				return AddressBookService.nspiRpcRequestsAverageLatency;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002125 File Offset: 0x00000325
		internal static MovingAveragePerfCounter NspiRpcBrowseRequestsAverageLatency
		{
			get
			{
				return AddressBookService.nspiRpcBrowseRequestsAverageLatency;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000212C File Offset: 0x0000032C
		internal static MovingAveragePerfCounter RfrRpcRequestsAverageLatency
		{
			get
			{
				return AddressBookService.rfrRpcRequestsAverageLatency;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002133 File Offset: 0x00000333
		internal ExEventLog ExEventLog
		{
			get
			{
				return this.eventLog;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000007 RID: 7 RVA: 0x0000213B File Offset: 0x0000033B
		internal bool IsStarted
		{
			get
			{
				return this.isStarted;
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002143 File Offset: 0x00000343
		public bool IsEnabled()
		{
			return NspiServer.Instance != null || RfriServer.Instance != null;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000215C File Offset: 0x0000035C
		public void OnStartBegin()
		{
			this.isStarted = true;
			Configuration.Initialize(this.eventLog, new Action(this.serviceManager.StopService));
			if (!Configuration.ServiceEnabled)
			{
				AddressBookService.GeneralTracer.TraceDebug(0L, "The service is not enabled.");
				return;
			}
			AddressBookService.InitializePerfCounters(new AddressBookPerformanceCounters());
			NspiPropMapper.Initialize();
			this.RegisterServicePrincipalNames();
			ADSession.DisableAdminTopologyMode();
			this.serviceManager.AddHttpPort(6001.ToString());
			NspiServer.Initialize(this.serviceManager, this.eventLog);
			RfriServer.Initialize(this.serviceManager, this.eventLog);
			if (Configuration.ProtocolLoggingEnabled)
			{
				if (string.IsNullOrEmpty(Configuration.LogFilePath))
				{
					this.eventLog.LogEvent(AddressBookEventLogConstants.Tuple_BadConfigParameter, "LogFilePath", new object[]
					{
						Configuration.LogFilePath
					});
					return;
				}
				ProtocolLog.Initialize(ExDateTime.UtcNow, Configuration.LogFilePath, TimeSpan.FromHours((double)Configuration.MaxRetentionPeriod), Configuration.MaxDirectorySize, Configuration.PerFileMaxSize, Configuration.ApplyHourPrecision);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002260 File Offset: 0x00000460
		internal static void InitializePerfCounters(IAddressBookPerformanceCounters addressBookPerformanceCounters)
		{
			Util.ThrowOnNullArgument(addressBookPerformanceCounters, "addressBookPerformanceCounters");
			AddressBookPerformanceCountersWrapper.Initialize(addressBookPerformanceCounters);
			AddressBookPerformanceCountersWrapper.AddressBookPerformanceCounters.PID.RawValue = (long)Globals.ProcessId;
			AddressBookService.nspiRpcRequestsAverageLatency = new MovingAveragePerfCounter(AddressBookPerformanceCountersWrapper.AddressBookPerformanceCounters.NspiRequestsAverageLatency, Configuration.AverageLatencySamples);
			AddressBookService.nspiRpcBrowseRequestsAverageLatency = new MovingAveragePerfCounter(AddressBookPerformanceCountersWrapper.AddressBookPerformanceCounters.NspiBrowseRequestsAverageLatency, Configuration.AverageLatencySamples);
			AddressBookService.rfrRpcRequestsAverageLatency = new MovingAveragePerfCounter(AddressBookPerformanceCountersWrapper.AddressBookPerformanceCounters.RfrRequestsAverageLatency, Configuration.AverageLatencySamples);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000022E0 File Offset: 0x000004E0
		public void OnStartEnd()
		{
			if (NspiServer.Instance == null && RfriServer.Instance == null)
			{
				this.eventLog.LogEvent(AddressBookEventLogConstants.Tuple_NoEndpointsConfigured, string.Empty, new object[0]);
				return;
			}
			this.eventLog.LogEvent(AddressBookEventLogConstants.Tuple_AddressBookServiceStartSuccess, string.Empty, new object[0]);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002334 File Offset: 0x00000534
		public void OnStopBegin()
		{
			NspiServer.ShuttingDown();
			RfriServer.ShuttingDown();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002340 File Offset: 0x00000540
		public void OnStopEnd()
		{
			if (this.isStarted)
			{
				if (Configuration.ProtocolLoggingEnabled)
				{
					ProtocolLog.Shutdown();
				}
				Configuration.Terminate();
				this.eventLog.LogEvent(AddressBookEventLogConstants.Tuple_AddressBookServiceStopSuccess, string.Empty, new object[0]);
			}
			this.isStarted = false;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002380 File Offset: 0x00000580
		public void HandleUnexpectedExceptionOnStart(Exception ex)
		{
			if (ex is DuplicateRpcEndpointException)
			{
				DuplicateRpcEndpointException ex2 = (DuplicateRpcEndpointException)ex;
				AddressBookService.GeneralTracer.TraceError<int, string>(0L, "Error {0} starting the RPC server: {1}", ex2.ErrorCode, ex2.Message);
				this.eventLog.LogEvent(AddressBookEventLogConstants.Tuple_RpcRegisterInterfaceFailure, string.Empty, new object[]
				{
					"MSExchangeAB",
					ServiceHelper.FormatWin32ErrorString(ex2.ErrorCode)
				});
				return;
			}
			this.eventLog.LogEvent(AddressBookEventLogConstants.Tuple_UnexpectedExceptionOnStart, string.Empty, new object[]
			{
				ex.Message
			});
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002418 File Offset: 0x00000618
		public void HandleUnexpectedExceptionOnStop(Exception ex)
		{
			AddressBookService.GeneralTracer.TraceError<Exception>(0L, "Unexpected exception while stopping: {0}", ex);
			this.eventLog.LogEvent(AddressBookEventLogConstants.Tuple_UnexpectedExceptionOnStop, string.Empty, new object[]
			{
				ex
			});
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002459 File Offset: 0x00000659
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<AddressBookService>(this);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002461 File Offset: 0x00000661
		private void RegisterServicePrincipalNames()
		{
			ServiceHelper.RegisterSPN("exchangeAB", this.eventLog, AddressBookEventLogConstants.Tuple_SpnRegisterFailure);
			ServiceHelper.RegisterSPN("exchangeRFR", this.eventLog, AddressBookEventLogConstants.Tuple_SpnRegisterFailure);
		}

		// Token: 0x04000001 RID: 1
		private const string AddressBookServiceName = "MSExchangeAB";

		// Token: 0x04000002 RID: 2
		private const string NspiServiceClass = "exchangeAB";

		// Token: 0x04000003 RID: 3
		private const string RfrServiceClass = "exchangeRFR";

		// Token: 0x04000004 RID: 4
		internal static readonly Trace GeneralTracer = ExTraceGlobals.GeneralTracer;

		// Token: 0x04000005 RID: 5
		private static readonly Guid ComponentGuid = new Guid("10193997-6273-4e05-b423-2ffb1d96e1aa");

		// Token: 0x04000006 RID: 6
		private static MovingAveragePerfCounter nspiRpcRequestsAverageLatency;

		// Token: 0x04000007 RID: 7
		private static MovingAveragePerfCounter nspiRpcBrowseRequestsAverageLatency;

		// Token: 0x04000008 RID: 8
		private static MovingAveragePerfCounter rfrRpcRequestsAverageLatency;

		// Token: 0x04000009 RID: 9
		private readonly ExEventLog eventLog;

		// Token: 0x0400000A RID: 10
		private readonly IRpcServiceManager serviceManager;

		// Token: 0x0400000B RID: 11
		private bool isStarted;
	}
}

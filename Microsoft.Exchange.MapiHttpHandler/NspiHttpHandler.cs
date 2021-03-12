using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Web;
using Microsoft.Exchange.AddressBook.EventLog;
using Microsoft.Exchange.AddressBook.Nspi;
using Microsoft.Exchange.AddressBook.Service;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net.MapiHttp;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000028 RID: 40
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiHttpHandler : MapiHttpHandler
	{
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000189 RID: 393 RVA: 0x000096A2 File Offset: 0x000078A2
		internal static IRfriAsyncDispatch RfriAsyncDispatch
		{
			get
			{
				return NspiHttpHandler.rfriAsyncDispatch;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600018A RID: 394 RVA: 0x000096A9 File Offset: 0x000078A9
		internal static INspiAsyncDispatch NspiAsyncDispatch
		{
			get
			{
				return NspiHttpHandler.nspiAsyncDispatch;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600018B RID: 395 RVA: 0x000096B0 File Offset: 0x000078B0
		internal override string EndpointVdirPath
		{
			get
			{
				return MapiHttpEndpoints.VdirPathNspi;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600018C RID: 396 RVA: 0x000096B7 File Offset: 0x000078B7
		internal override IAsyncOperationFactory OperationFactory
		{
			get
			{
				return NspiHttpHandler.operationFactory;
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000096C0 File Offset: 0x000078C0
		internal override bool TryEnsureHandlerIsInitialized()
		{
			if (NspiHttpHandler.shutdownTime != null)
			{
				return false;
			}
			if (!NspiHttpHandler.initialized)
			{
				lock (NspiHttpHandler.initializeLock)
				{
					if (NspiHttpHandler.shutdownTime != null)
					{
						return false;
					}
					if (!NspiHttpHandler.initialized)
					{
						MapiHttpHandler.IsValidContextHandleDelegate = new Func<object, bool>(NspiHttpHandler.InternalIsValidContextHandle);
						MapiHttpHandler.TryContextHandleRundownDelegate = new Func<object, bool>(NspiHttpHandler.InternalTryContextHandleRundown);
						MapiHttpHandler.ShutdownHandlerDelegate = new Action(NspiHttpHandler.InternalShutdownHandler);
						MapiHttpHandler.NeedTokenRehydrationDelegate = new Func<string, bool>(NspiHttpHandler.InternalNeedTokenRehydration);
						NspiHttpHandler.InitializeAddressBookService();
						NspiHttpHandler.initialized = true;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000977C File Offset: 0x0000797C
		internal override void LogFailure(IList<string> requestIds, IList<string> cookies, string message, string userName, string protocolSequence, string clientAddress, string organization, Exception exception, Microsoft.Exchange.Diagnostics.Trace trace)
		{
			ProtocolLog.LogProtocolFailure("MapiHttp: failure", requestIds, cookies, message, userName, protocolSequence, clientAddress, organization, exception);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000097A0 File Offset: 0x000079A0
		private static void InternalShutdownHandler()
		{
			try
			{
				if (NspiHttpHandler.shutdownTime == null)
				{
					NspiHttpHandler.shutdownTime = new ExDateTime?(ExDateTime.Now);
					lock (NspiHttpHandler.initializeLock)
					{
						if (NspiHttpHandler.initialized)
						{
							NspiHttpHandler.ShutdownAddressBookService();
							NspiHttpHandler.initialized = false;
						}
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000981C File Offset: 0x00007A1C
		private static bool InternalIsValidContextHandle(object contextHandle)
		{
			if (contextHandle == null)
			{
				return false;
			}
			IntPtr? intPtr = contextHandle as IntPtr?;
			return intPtr != null && !(intPtr.Value == IntPtr.Zero);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000987C File Offset: 0x00007A7C
		private static bool InternalTryContextHandleRundown(object contextHandle)
		{
			IntPtr? localContextHandle = contextHandle as IntPtr?;
			if (localContextHandle == null)
			{
				return true;
			}
			if (localContextHandle.Value == IntPtr.Zero)
			{
				return true;
			}
			MapiHttpHandler.DispatchCallSync(delegate
			{
				NspiHttpHandler.NspiAsyncDispatch.ContextHandleRundown(localContextHandle.Value);
			});
			return true;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000098DC File Offset: 0x00007ADC
		private static bool InternalNeedTokenRehydration(string requestType)
		{
			return !string.IsNullOrWhiteSpace(requestType) && (string.Compare(requestType, "Bind", true) == 0 || string.Compare(requestType, "GetMailboxUrl", true) == 0 || string.Compare(requestType, "GetAddressBookUrl", true) == 0 || string.Compare(requestType, "GetNspiUrl", true) == 0);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000992C File Offset: 0x00007B2C
		private static void InitializeAddressBookService()
		{
			Configuration.UseDefaultAppConfig = true;
			ProtocolLog.SetDefaults(string.Format("{0}Logging\\MAPI AddressBook Service\\", ExchangeSetupContext.InstallPath), "MAPI AddressBook Protocol Logs", "MAPIAB_", "MAPIAddressBookProtocolLogs");
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				NspiHttpHandler.processId = currentProcess.Id;
			}
			NspiHttpHandler.eventLogger = new ExEventLog(NspiHttpHandler.ComponentGuid, "MSExchangeMapiAddressBookAppPool");
			NspiHttpHandler.eventLogger.LogEvent(AddressBookEventLogConstants.Tuple_StartingMSExchangeMapiAddressBookAppPool, string.Empty, new object[]
			{
				NspiHttpHandler.processId,
				"Microsoft Exchange",
				"15.00.1497.010"
			});
			int num = Privileges.RemoveAllExcept(new string[]
			{
				"SeAuditPrivilege",
				"SeChangeNotifyPrivilege",
				"SeCreateGlobalPrivilege",
				"SeTcbPrivilege"
			}, "MSExchangeMapiAddressBookAppPool");
			if (num != 0)
			{
				NspiHttpHandler.eventLogger.LogEvent(AddressBookEventLogConstants.Tuple_MapiAddressBookRemovingPrivilegeErrorOnStart, string.Empty, new object[0]);
				string failureDescription = string.Format("Failed to remove privileges from {0}, error code = {1}", "MSExchangeMapiAddressBookAppPool", num);
				throw ProtocolException.FromResponseCode((LID)51100, failureDescription, ResponseCode.EndpointDisabled, null);
			}
			Configuration.Initialize(NspiHttpHandler.eventLogger, null);
			NspiPropMapper.Initialize();
			AddressBookService.InitializePerfCounters(new NspiPerformanceCountersWrapper());
			NspiHttpHandler.isProtocolLogEnabled = Configuration.ProtocolLoggingEnabled;
			if (NspiHttpHandler.isProtocolLogEnabled)
			{
				ProtocolLog.Initialize(ExDateTime.UtcNow, Configuration.LogFilePath, TimeSpan.FromHours((double)Configuration.MaxRetentionPeriod), Configuration.MaxDirectorySize, Configuration.PerFileMaxSize, Configuration.ApplyHourPrecision);
			}
			UserWorkloadManager.Initialize(100, 100, 100, TimeSpan.FromHours(1.0), null);
			ServerFqdnCache.InitializeCache();
			NspiHttpHandler.eventLogger.LogEvent(AddressBookEventLogConstants.Tuple_MSExchangeMapiAddressBookAppPoolStartSuccess, string.Empty, new object[]
			{
				NspiHttpHandler.processId,
				"Microsoft Exchange",
				"15.00.1497.010"
			});
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00009B14 File Offset: 0x00007D14
		private static void ShutdownAddressBookService()
		{
			NspiHttpHandler.eventLogger.LogEvent(AddressBookEventLogConstants.Tuple_StoppingMSExchangeMapiAddressBookAppPool, string.Empty, new object[]
			{
				NspiHttpHandler.processId,
				"Microsoft Exchange",
				"15.00.1497.010"
			});
			while (ExDateTime.Now - NspiHttpHandler.shutdownTime.Value < NspiHttpHandler.waitDrainOnShutdown)
			{
				Thread.Sleep(500);
				if (UserWorkloadManager.Singleton.TotalTasks == 0)
				{
					break;
				}
			}
			UserWorkloadManager.Singleton.Dispose();
			Thread.Sleep(500);
			if (NspiHttpHandler.isProtocolLogEnabled)
			{
				ProtocolLog.Shutdown();
			}
			Configuration.Terminate();
			NspiHttpHandler.eventLogger.LogEvent(AddressBookEventLogConstants.Tuple_MSExchangeMapiAddressBookAppPoolStopSuccess, string.Empty, new object[]
			{
				NspiHttpHandler.processId,
				"Microsoft Exchange",
				"15.00.1497.010"
			});
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00009CA4 File Offset: 0x00007EA4
		// Note: this type is marked as 'beforefieldinit'.
		static NspiHttpHandler()
		{
			Dictionary<string, Func<HttpContextBase, AsyncOperation>> dictionary = new Dictionary<string, Func<HttpContextBase, AsyncOperation>>();
			dictionary.Add("Bind", (HttpContextBase context) => new NspiBindAsyncOperation(context));
			dictionary.Add("Unbind", (HttpContextBase context) => new NspiUnbindAsyncOperation(context));
			dictionary.Add("GetMatches", (HttpContextBase context) => new NspiGetMatchesAsyncOperation(context));
			dictionary.Add("GetPropList", (HttpContextBase context) => new NspiGetPropListAsyncOperation(context));
			dictionary.Add("GetProps", (HttpContextBase context) => new NspiGetPropsAsyncOperation(context));
			dictionary.Add("ModProps", (HttpContextBase context) => new NspiModPropsAsyncOperation(context));
			dictionary.Add("DNToMId", (HttpContextBase context) => new NspiDNToEphAsyncOperation(context));
			dictionary.Add("CompareMIds", (HttpContextBase context) => new NspiCompareDNTsAsyncOperation(context));
			dictionary.Add("CompareDNTs", (HttpContextBase context) => new NspiCompareDNTsAsyncOperation(context));
			dictionary.Add("CompareMinIds", (HttpContextBase context) => new NspiCompareDNTsAsyncOperation(context));
			dictionary.Add("GetSpecialTable", (HttpContextBase context) => new NspiGetSpecialTableAsyncOperation(context));
			dictionary.Add("GetTemplateInfo", (HttpContextBase context) => new NspiGetTemplateInfoAsyncOperation(context));
			dictionary.Add("ModLinkAtt", (HttpContextBase context) => new NspiModLinkAttAsyncOperation(context));
			dictionary.Add("QueryColumns", (HttpContextBase context) => new NspiQueryColumnsAsyncOperation(context));
			dictionary.Add("QueryRows", (HttpContextBase context) => new NspiQueryRowsAsyncOperation(context));
			dictionary.Add("ResolveNames", (HttpContextBase context) => new NspiResolveNamesAsyncOperation(context));
			dictionary.Add("ResortRestriction", (HttpContextBase context) => new NspiResortRestrictionAsyncOperation(context));
			dictionary.Add("SeekEntries", (HttpContextBase context) => new NspiSeekEntriesAsyncOperation(context));
			dictionary.Add("UpdateStat", (HttpContextBase context) => new NspiUpdateStatAsyncOperation(context));
			dictionary.Add("GetMailboxUrl", (HttpContextBase context) => new RfriGetMailboxUrlAsyncOperation(context));
			dictionary.Add("GetAddressBookUrl", (HttpContextBase context) => new RfriGetAddressBookUrlAsyncOperation(context));
			dictionary.Add("GetNspiUrl", (HttpContextBase context) => new RfriGetAddressBookUrlAsyncOperation(context));
			NspiHttpHandler.operationFactory = new DictionaryBasedOperationFactory(dictionary);
			NspiHttpHandler.waitDrainOnShutdown = TimeSpan.FromSeconds(30.0);
			NspiHttpHandler.rfriAsyncDispatch = new RfriAsyncDispatch();
			NspiHttpHandler.nspiAsyncDispatch = new NspiAsyncDispatch();
			NspiHttpHandler.initializeLock = new object();
			NspiHttpHandler.initialized = false;
			NspiHttpHandler.shutdownTime = null;
			NspiHttpHandler.isProtocolLogEnabled = false;
		}

		// Token: 0x040000AF RID: 175
		public const string ApplicationPoolName = "MSExchangeMapiAddressBookAppPool";

		// Token: 0x040000B0 RID: 176
		private const string LogTypeName = "MAPI AddressBook Protocol Logs";

		// Token: 0x040000B1 RID: 177
		private const string LogFilePrefix = "MAPIAB_";

		// Token: 0x040000B2 RID: 178
		private const string LogComponent = "MAPIAddressBookProtocolLogs";

		// Token: 0x040000B3 RID: 179
		private static readonly Guid ComponentGuid = new Guid("ef013c5d-8aa2-402d-9c7d-227c8c6f0ad6");

		// Token: 0x040000B4 RID: 180
		private static readonly IAsyncOperationFactory operationFactory;

		// Token: 0x040000B5 RID: 181
		private static readonly TimeSpan waitDrainOnShutdown;

		// Token: 0x040000B6 RID: 182
		private static readonly IRfriAsyncDispatch rfriAsyncDispatch;

		// Token: 0x040000B7 RID: 183
		private static readonly INspiAsyncDispatch nspiAsyncDispatch;

		// Token: 0x040000B8 RID: 184
		private static readonly object initializeLock;

		// Token: 0x040000B9 RID: 185
		private static ExEventLog eventLogger;

		// Token: 0x040000BA RID: 186
		private static bool initialized;

		// Token: 0x040000BB RID: 187
		private static ExDateTime? shutdownTime;

		// Token: 0x040000BC RID: 188
		private static bool isProtocolLogEnabled;

		// Token: 0x040000BD RID: 189
		private static int processId;
	}
}

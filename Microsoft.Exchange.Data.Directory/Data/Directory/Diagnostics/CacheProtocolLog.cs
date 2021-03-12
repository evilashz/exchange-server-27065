using System;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.Directory.Diagnostics
{
	// Token: 0x020000BA RID: 186
	internal sealed class CacheProtocolLog : BaseDirectoryProtocolLog
	{
		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x0002BE10 File Offset: 0x0002A010
		protected override LogSchema Schema
		{
			get
			{
				return CacheProtocolLog.schema;
			}
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0002BE18 File Offset: 0x0002A018
		internal static void BeginAppend(string operation, string dn, DateTime whenReadUTC, long totalProcessingTime, long wcfGetProcessingTime, long wcfRemoveProcessingTime, long wcfPutProcessingTime, long adProcessingTime, bool isNewProxyObject, int retryCount, string objectType, string cachePerformanceTracker, Guid activityId, string callerInfo, string error = null)
		{
			if (CacheProtocolLog.instance == null)
			{
				CacheProtocolLog value = new CacheProtocolLog();
				Interlocked.CompareExchange<CacheProtocolLog>(ref CacheProtocolLog.instance, value, null);
			}
			CacheProtocolLog.AppendDelegate appendDelegate = new CacheProtocolLog.AppendDelegate(CacheProtocolLog.instance.AppendInstance);
			appendDelegate.BeginInvoke(operation, dn, whenReadUTC, totalProcessingTime, wcfGetProcessingTime, wcfRemoveProcessingTime, wcfPutProcessingTime, adProcessingTime, isNewProxyObject, retryCount, objectType, cachePerformanceTracker, activityId, callerInfo, error, null, null);
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0002BE74 File Offset: 0x0002A074
		private void AppendInstance(string operation, string dn, DateTime whenReadUTC, long totalProcessingTime, long wcfGetProcessingTime, long wcfRemoveProcessingTime, long wcfPutProcessingTime, long adProcessingTime, bool isNewProxy, int retryCount, string objectType, string cachePerformanceTracker, Guid activityId, string callerInfo, string error)
		{
			if (!base.Initialized)
			{
				this.Initialize();
			}
			if (BaseDirectoryProtocolLog.LoggingEnabled && !this.protocolLogDisabled)
			{
				LogRowFormatter logRowFormatter = new LogRowFormatter(CacheProtocolLog.schema);
				logRowFormatter[1] = CacheProtocolLog.instance.GetNextSequenceNumber();
				logRowFormatter[2] = Globals.ProcessName;
				logRowFormatter[4] = Globals.ProcessAppName;
				logRowFormatter[3] = Globals.ProcessId;
				logRowFormatter[5] = operation;
				logRowFormatter[6] = totalProcessingTime;
				logRowFormatter[7] = wcfGetProcessingTime;
				logRowFormatter[8] = wcfPutProcessingTime;
				logRowFormatter[9] = wcfRemoveProcessingTime;
				logRowFormatter[10] = adProcessingTime;
				logRowFormatter[11] = isNewProxy;
				logRowFormatter[12] = retryCount;
				logRowFormatter[13] = objectType;
				logRowFormatter[15] = whenReadUTC;
				logRowFormatter[14] = dn;
				logRowFormatter[16] = cachePerformanceTracker;
				logRowFormatter[17] = activityId;
				logRowFormatter[18] = callerInfo;
				logRowFormatter[19] = error;
				base.Logger.Append(logRowFormatter, 0);
			}
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0002BFB8 File Offset: 0x0002A1B8
		private void Initialize()
		{
			lock (this.logLock)
			{
				if (!base.Initialized)
				{
					this.ReadConfigData();
					base.Initialize(ExDateTime.UtcNow, Path.Combine(BaseDirectoryProtocolLog.GetExchangeInstallPath(), "Logging\\DirCache\\"), BaseDirectoryProtocolLog.DefaultMaxRetentionPeriod, BaseDirectoryProtocolLog.DefaultDirectorySizeQuota, BaseDirectoryProtocolLog.DefaultPerFileSizeQuota, true, "DirCacheLogs");
				}
			}
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0002C030 File Offset: 0x0002A230
		protected override void UpdateConfigIfChanged(object state)
		{
			base.UpdateConfigIfChanged(state);
			this.ReadConfigData();
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0002C040 File Offset: 0x0002A240
		private void ReadConfigData()
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\MSExchange ADAccess\\Parameters"))
			{
				this.protocolLogDisabled = BaseDirectoryProtocolLog.GetRegistryBool(registryKey, "CacheProtocolLogDisabled", false);
			}
		}

		// Token: 0x0400037B RID: 891
		private const string CacheProtocolLogDisabled = "CacheProtocolLogDisabled";

		// Token: 0x0400037C RID: 892
		private const string LogTypeName = "DirCache Logs";

		// Token: 0x0400037D RID: 893
		private const string LogComponent = "DirCacheLogs";

		// Token: 0x0400037E RID: 894
		private const string LoggingDirectoryUnderExchangeInstallPath = "Logging\\DirCache\\";

		// Token: 0x0400037F RID: 895
		private static CacheProtocolLog instance = null;

		// Token: 0x04000380 RID: 896
		internal static readonly BaseDirectoryProtocolLog.FieldInfo[] Fields = new BaseDirectoryProtocolLog.FieldInfo[]
		{
			new BaseDirectoryProtocolLog.FieldInfo(0, "date-time"),
			new BaseDirectoryProtocolLog.FieldInfo(1, "seq-number"),
			new BaseDirectoryProtocolLog.FieldInfo(2, "process-name"),
			new BaseDirectoryProtocolLog.FieldInfo(3, "process-id"),
			new BaseDirectoryProtocolLog.FieldInfo(4, "application-name"),
			new BaseDirectoryProtocolLog.FieldInfo(5, "operation"),
			new BaseDirectoryProtocolLog.FieldInfo(6, "processing-time"),
			new BaseDirectoryProtocolLog.FieldInfo(7, "wcfgetprocessing-time"),
			new BaseDirectoryProtocolLog.FieldInfo(8, "wcfputprocessing-time"),
			new BaseDirectoryProtocolLog.FieldInfo(9, "wcfremoveprocessing-time"),
			new BaseDirectoryProtocolLog.FieldInfo(10, "adprocessing-time"),
			new BaseDirectoryProtocolLog.FieldInfo(11, "is-new-proxy-object"),
			new BaseDirectoryProtocolLog.FieldInfo(12, "retry-count"),
			new BaseDirectoryProtocolLog.FieldInfo(13, "objecttype"),
			new BaseDirectoryProtocolLog.FieldInfo(14, "distinguished-name"),
			new BaseDirectoryProtocolLog.FieldInfo(15, "whenread-utc"),
			new BaseDirectoryProtocolLog.FieldInfo(16, "cacheperf-details"),
			new BaseDirectoryProtocolLog.FieldInfo(17, "activity-id"),
			new BaseDirectoryProtocolLog.FieldInfo(18, "caller-info"),
			new BaseDirectoryProtocolLog.FieldInfo(19, "error")
		};

		// Token: 0x04000381 RID: 897
		private static readonly LogSchema schema = new LogSchema("Microsoft Exchange", "15.00.1497.010", "DirCache Logs", BaseDirectoryProtocolLog.GetColumnArray(CacheProtocolLog.Fields));

		// Token: 0x04000382 RID: 898
		private object logLock = new object();

		// Token: 0x04000383 RID: 899
		private bool protocolLogDisabled;

		// Token: 0x020000BB RID: 187
		private enum Field : byte
		{
			// Token: 0x04000385 RID: 901
			DateTime,
			// Token: 0x04000386 RID: 902
			SequenceNumber,
			// Token: 0x04000387 RID: 903
			ClientName,
			// Token: 0x04000388 RID: 904
			Pid,
			// Token: 0x04000389 RID: 905
			AppName,
			// Token: 0x0400038A RID: 906
			Operation,
			// Token: 0x0400038B RID: 907
			TotalProcessingTime,
			// Token: 0x0400038C RID: 908
			WCFGetProcessingTime,
			// Token: 0x0400038D RID: 909
			WCFPutProcessingTime,
			// Token: 0x0400038E RID: 910
			WCFRemoveProcessingTime,
			// Token: 0x0400038F RID: 911
			ADProcessingTime,
			// Token: 0x04000390 RID: 912
			IsNewProxyObject,
			// Token: 0x04000391 RID: 913
			RetryCount,
			// Token: 0x04000392 RID: 914
			ObjectType,
			// Token: 0x04000393 RID: 915
			DN,
			// Token: 0x04000394 RID: 916
			WhenReadUTC,
			// Token: 0x04000395 RID: 917
			CachePerformanceDetails,
			// Token: 0x04000396 RID: 918
			ActivityId,
			// Token: 0x04000397 RID: 919
			CallerInfo,
			// Token: 0x04000398 RID: 920
			Error
		}

		// Token: 0x020000BC RID: 188
		// (Invoke) Token: 0x060009CE RID: 2510
		internal delegate void AppendDelegate(string operation, string dn, DateTime whenReadUTC, long totalProcessingTime, long wcfGetProcessingTime, long wcfRemoveProcessingTime, long wcfPutProcessingTime, long adProcessingTime, bool isNewProxyObject, int retryCount, string objectType, string cachePerformanceTracker, Guid requestId, string callerInfo, string error);
	}
}

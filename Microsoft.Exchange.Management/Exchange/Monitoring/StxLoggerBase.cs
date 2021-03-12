using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200002B RID: 43
	internal abstract class StxLoggerBase
	{
		// Token: 0x06000124 RID: 292 RVA: 0x0000603B File Offset: 0x0000423B
		internal StxLoggerBase()
		{
			this.extendedFields = new List<FieldInfo>();
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00006050 File Offset: 0x00004250
		internal static StxLoggerBase GetLoggerInstance(StxLogType type)
		{
			StxLoggerBase stxLoggerBase;
			if (!StxLoggerBase.LogDictionary.TryGetValue(type, out stxLoggerBase))
			{
				switch (type)
				{
				case StxLogType.TestLiveIdAuthentication:
					stxLoggerBase = new LiveIdAuthenticationStxLogger();
					break;
				case StxLogType.TestNtlmConnectivity:
					stxLoggerBase = new NtlmConnectivityStxLogger();
					break;
				case StxLogType.TestActiveDirectoryConnectivity:
					stxLoggerBase = new ActiveDirectoryConnectivityStxLogger();
					break;
				case StxLogType.TestTopologyService:
					stxLoggerBase = new TopologyServiceStxLogger();
					break;
				case StxLogType.TestGlobalLocatorService:
					stxLoggerBase = new GlobalLocatorServiceStxLogger();
					break;
				case StxLogType.TestForwardFullSync:
					stxLoggerBase = new ForwardFullSyncStxLogger();
					break;
				case StxLogType.TestForwardSyncCookie:
					stxLoggerBase = new ForwardSyncCookieStxLogger();
					break;
				case StxLogType.TestForwardSyncCookieResponder:
					stxLoggerBase = new ForwardSyncCookieResponderStxLogger();
					break;
				case StxLogType.TestForwardSyncCompanyProbe:
					stxLoggerBase = new ForwardSyncCompanyProbeStxLogger();
					break;
				case StxLogType.TestForwardSyncCompanyResponder:
					stxLoggerBase = new ForwardSyncCompanyResponderStxLogger();
					break;
				case StxLogType.DatabaseAvailability:
					stxLoggerBase = new DatabaseAvailabilityStxLogger();
					break;
				case StxLogType.TestRidMonitor:
					stxLoggerBase = new RidMonitorLogger();
					break;
				case StxLogType.TestRidSetMonitor:
					stxLoggerBase = new RidSetMonitorLogger();
					break;
				case StxLogType.TestActiveDirectorySelfCheck:
					stxLoggerBase = new ActiveDirectorySelfCheckStxLogger();
					break;
				case StxLogType.TenantRelocationErrorMonitor:
					stxLoggerBase = new TenantRelocationErrorLogger();
					break;
				case StxLogType.SharedConfigurationTenantMonitor:
					stxLoggerBase = new SharedConfigurationTenantMonitorLogger();
					break;
				case StxLogType.TestActivedirectoryConnectivityForConfigDC:
					stxLoggerBase = new ActiveDirectoryConnectivityConfigDCStxLogger();
					break;
				case StxLogType.SyntheticReplicationTransaction:
					stxLoggerBase = new SyntheticReplicationTransactionLogger();
					break;
				case StxLogType.SyntheticReplicationMonitor:
					stxLoggerBase = new SyntheticReplicationMonitorLogger();
					break;
				case StxLogType.PassiveReplicationMonitor:
					stxLoggerBase = new PassiveReplicationMonitorLogger();
					break;
				case StxLogType.PassiveADReplicationMonitor:
					stxLoggerBase = new PassiveADReplicationMonitorLogger();
					break;
				case StxLogType.PassiveReplicationPerfCounterProbe:
					stxLoggerBase = new PassiveReplicationPerfCounterProbeLogger();
					break;
				case StxLogType.RemoteDomainControllerStateProbe:
					stxLoggerBase = new RemoteDomainControllerStateProbeLogger();
					break;
				case StxLogType.TrustMonitorProbe:
					stxLoggerBase = new TrustMonitorProbeLogger();
					break;
				case StxLogType.TestKDCService:
					stxLoggerBase = new TestKDCServiceStxLogger();
					break;
				case StxLogType.TestDoMTConnectivity:
					stxLoggerBase = new DoMTConnectivityStxLogger();
					break;
				case StxLogType.TestOfflineGLS:
					stxLoggerBase = new OfflineGLSStxLogger();
					break;
				}
				stxLoggerBase = StxLoggerBase.LogDictionary.GetOrAdd(type, stxLoggerBase);
			}
			return stxLoggerBase;
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000126 RID: 294 RVA: 0x000061EA File Offset: 0x000043EA
		// (set) Token: 0x06000127 RID: 295 RVA: 0x00006222 File Offset: 0x00004422
		internal Log Log
		{
			get
			{
				if (this.log == null)
				{
					this.log = new Log(this.LogFilePrefix, new LogHeaderFormatter(this.Schema, this.HeaderCsvOption), this.LogComponent);
				}
				return this.log;
			}
			private set
			{
				this.log = value;
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000622B File Offset: 0x0000442B
		internal void BeginAppend(LogRowFormatter row)
		{
			StxLogger.BeginAppend(this, row);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00006234 File Offset: 0x00004434
		internal void BeginAppend(string target, bool status, TimeSpan latency, int error, string errorString)
		{
			this.BeginAppend(target, status, latency, error, errorString, null, null, null, null);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00006254 File Offset: 0x00004454
		internal void BeginAppend(string target, bool status, TimeSpan latency, int error, string errorString, string stateAttribute1, string stateAttribute2, string stateAttribute3, string stateAttribute4)
		{
			LogRowFormatter logRowFormatter = new LogRowFormatter(this.Schema);
			logRowFormatter[1] = target;
			logRowFormatter[2] = (status ? 0 : 1);
			logRowFormatter[3] = latency.TotalSeconds;
			logRowFormatter[4] = error.ToString();
			logRowFormatter[5] = ((errorString != null) ? errorString : string.Empty);
			logRowFormatter[6] = ((stateAttribute1 != null) ? stateAttribute1 : string.Empty);
			logRowFormatter[7] = ((stateAttribute2 != null) ? stateAttribute2 : string.Empty);
			logRowFormatter[8] = ((stateAttribute3 != null) ? stateAttribute3 : string.Empty);
			logRowFormatter[9] = ((stateAttribute4 != null) ? stateAttribute4 : string.Empty);
			this.BeginAppend(logRowFormatter);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00006318 File Offset: 0x00004518
		internal void BeginAppend(string target, bool status, TimeSpan latency, int error, string errorString, string stateAttribute1, string stateAttribute2, string stateAttribute3, string stateAttribute4, List<string> extendedAttributes)
		{
			LogRowFormatter logRowFormatter = new LogRowFormatter(this.Schema);
			logRowFormatter[1] = target;
			logRowFormatter[2] = (status ? 0 : 1);
			logRowFormatter[3] = latency.TotalSeconds;
			logRowFormatter[4] = error.ToString();
			logRowFormatter[5] = ((errorString != null) ? errorString : string.Empty);
			logRowFormatter[6] = ((stateAttribute1 != null) ? stateAttribute1 : string.Empty);
			logRowFormatter[7] = ((stateAttribute2 != null) ? stateAttribute2 : string.Empty);
			logRowFormatter[8] = ((stateAttribute3 != null) ? stateAttribute3 : string.Empty);
			logRowFormatter[9] = ((stateAttribute4 != null) ? stateAttribute4 : string.Empty);
			if (this.ExtendedFields.Count != extendedAttributes.Count)
			{
				throw new Exception(string.Format("the count of extended attribute values does not match with extended fields count. expected {0}, actual {1}", this.ExtendedFields.Count, extendedAttributes.Count));
			}
			for (int i = 0; i < this.ExtendedFields.Count; i++)
			{
				logRowFormatter[StxLoggerBase.MandatoryFields.Count + i] = ((extendedAttributes[i] != null) ? extendedAttributes[i] : string.Empty);
			}
			this.BeginAppend(logRowFormatter);
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600012C RID: 300 RVA: 0x0000645A File Offset: 0x0000465A
		internal virtual LogSchema Schema
		{
			get
			{
				if (this.schema == null)
				{
					this.schema = new LogSchema("Microsoft Exchange", "15.00.1497.010", this.LogTypeName, this.GetColumnArray());
				}
				return this.schema;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600012D RID: 301 RVA: 0x0000648B File Offset: 0x0000468B
		internal virtual int DateTimeField
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600012E RID: 302 RVA: 0x0000648E File Offset: 0x0000468E
		internal List<FieldInfo> ExtendedFields
		{
			get
			{
				return this.extendedFields;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00006496 File Offset: 0x00004696
		internal virtual LogHeaderCsvOption HeaderCsvOption
		{
			get
			{
				return LogHeaderCsvOption.CsvCompatible;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00006499 File Offset: 0x00004699
		// (set) Token: 0x06000131 RID: 305 RVA: 0x000064A1 File Offset: 0x000046A1
		internal bool Initialized { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000132 RID: 306
		internal abstract string LogTypeName { get; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000133 RID: 307
		internal abstract string LogComponent { get; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000134 RID: 308
		internal abstract string LogFilePrefix { get; }

		// Token: 0x06000135 RID: 309 RVA: 0x000064AC File Offset: 0x000046AC
		private string[] GetColumnArray()
		{
			string[] array = new string[StxLoggerBase.MandatoryFields.Count + this.ExtendedFields.Count];
			for (int i = 0; i < StxLoggerBase.MandatoryFields.Count; i++)
			{
				array[i] = StxLoggerBase.MandatoryFields[i].ColumnName;
			}
			int num = 0;
			for (int j = StxLoggerBase.MandatoryFields.Count; j < this.ExtendedFields.Count + StxLoggerBase.MandatoryFields.Count; j++)
			{
				array[j] = this.ExtendedFields[num].ColumnName;
				num++;
			}
			return array;
		}

		// Token: 0x040000F1 RID: 241
		internal static readonly List<FieldInfo> MandatoryFields = new List<FieldInfo>
		{
			new FieldInfo(0, "Timestamp"),
			new FieldInfo(1, "Target Entity"),
			new FieldInfo(2, "Status"),
			new FieldInfo(3, "Latency"),
			new FieldInfo(4, "Error"),
			new FieldInfo(5, "Exception"),
			new FieldInfo(6, "StateAttribute1"),
			new FieldInfo(7, "StateAttribute2"),
			new FieldInfo(8, "StateAttribute3"),
			new FieldInfo(9, "StateAttribute4")
		};

		// Token: 0x040000F2 RID: 242
		private Log log;

		// Token: 0x040000F3 RID: 243
		private LogSchema schema;

		// Token: 0x040000F4 RID: 244
		private List<FieldInfo> extendedFields;

		// Token: 0x040000F5 RID: 245
		internal static readonly ConcurrentDictionary<StxLogType, StxLoggerBase> LogDictionary = new ConcurrentDictionary<StxLogType, StxLoggerBase>();

		// Token: 0x0200002C RID: 44
		internal enum MandatoryField
		{
			// Token: 0x040000F8 RID: 248
			DateTime,
			// Token: 0x040000F9 RID: 249
			Target,
			// Token: 0x040000FA RID: 250
			Status,
			// Token: 0x040000FB RID: 251
			Latency,
			// Token: 0x040000FC RID: 252
			Error,
			// Token: 0x040000FD RID: 253
			Exception,
			// Token: 0x040000FE RID: 254
			StateAttribute1,
			// Token: 0x040000FF RID: 255
			StateAttribute2,
			// Token: 0x04000100 RID: 256
			StateAttribute3,
			// Token: 0x04000101 RID: 257
			StateAttribute4
		}
	}
}

using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.EventLog
{
	// Token: 0x02000A3D RID: 2621
	public static class DirectoryEventLogConstants
	{
		// Token: 0x04004D8F RID: 19855
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_SYNC_FAILED = new ExEventLog.EventTuple(3221489677U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004D90 RID: 19856
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_DC_DOWN = new ExEventLog.EventTuple(1074006038U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004D91 RID: 19857
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_FIND_LOCAL_SERVER_FAILED = new ExEventLog.EventTuple(3221489691U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004D92 RID: 19858
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_DISCOVERED_SERVERS = new ExEventLog.EventTuple(1074006048U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004D93 RID: 19859
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_GOING_IN_SITE_DC = new ExEventLog.EventTuple(1074006050U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004D94 RID: 19860
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_GOING_IN_SITE_GC = new ExEventLog.EventTuple(1074006051U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004D95 RID: 19861
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_REG_BAD_DWORD = new ExEventLog.EventTuple(1074006056U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004D96 RID: 19862
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_REG_CDC_BAD = new ExEventLog.EventTuple(2147747881U, 4, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004D97 RID: 19863
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_REG_CDC_DOWN = new ExEventLog.EventTuple(2147747882U, 4, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004D98 RID: 19864
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_REG_SERVER_BAD = new ExEventLog.EventTuple(2147747883U, 4, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004D99 RID: 19865
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_REG_DC = new ExEventLog.EventTuple(1074006060U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004D9A RID: 19866
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_REG_GC = new ExEventLog.EventTuple(1074006061U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004D9B RID: 19867
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_REG_CDC = new ExEventLog.EventTuple(1074006064U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004D9C RID: 19868
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_ALL_DC_DOWN = new ExEventLog.EventTuple(3221489718U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004D9D RID: 19869
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_ALL_GC_DOWN = new ExEventLog.EventTuple(3221489719U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004D9E RID: 19870
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_GETHOSTBYNAME_FAILED = new ExEventLog.EventTuple(2147747899U, 3, EventLogEntryType.Warning, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004D9F RID: 19871
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_BIND_FAILED = new ExEventLog.EventTuple(3221489726U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DA0 RID: 19872
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_SUITABILITY_CHECK_FAILED = new ExEventLog.EventTuple(3221489727U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004DA1 RID: 19873
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_NO_SACL = new ExEventLog.EventTuple(2147747904U, 3, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004DA2 RID: 19874
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_GOT_SACL = new ExEventLog.EventTuple(1074006081U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004DA3 RID: 19875
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_FATAL_ERROR = new ExEventLog.EventTuple(2147747907U, 5, EventLogEntryType.Warning, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004DA4 RID: 19876
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_BAD_OS_VERSION = new ExEventLog.EventTuple(2147747908U, 3, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DA5 RID: 19877
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_IMPERSONATED_CALLER = new ExEventLog.EventTuple(2147747909U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004DA6 RID: 19878
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_DNS_DIAG_SERVER_FAILURE = new ExEventLog.EventTuple(3221489734U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DA7 RID: 19879
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_DNS_NAME_ERROR = new ExEventLog.EventTuple(3221489735U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DA8 RID: 19880
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_DNS_TIMEOUT = new ExEventLog.EventTuple(3221489736U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DA9 RID: 19881
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_DNS_NO_ERROR = new ExEventLog.EventTuple(2147747913U, 3, EventLogEntryType.Warning, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DAA RID: 19882
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_DNS_OTHER = new ExEventLog.EventTuple(3221489738U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DAB RID: 19883
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_DNS_NO_ERROR_DC_FOUND = new ExEventLog.EventTuple(2147747915U, 3, EventLogEntryType.Warning, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DAC RID: 19884
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_DNS_NO_ERROR_DC_NOT_FOUND = new ExEventLog.EventTuple(2147747916U, 3, EventLogEntryType.Warning, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DAD RID: 19885
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_NO_CONNECTION = new ExEventLog.EventTuple(3221489742U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004DAE RID: 19886
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_DC_UP = new ExEventLog.EventTuple(1074006095U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DAF RID: 19887
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_BASE_CONFIG_SEARCH_FAILED = new ExEventLog.EventTuple(2147747920U, 4, EventLogEntryType.Warning, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DB0 RID: 19888
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_GET_DC_FROM_DOMAIN = new ExEventLog.EventTuple(1074006097U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004DB1 RID: 19889
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_GET_DC_FROM_DOMAIN_FAILED = new ExEventLog.EventTuple(3221489746U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DB2 RID: 19890
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_NEW_CONNECTION = new ExEventLog.EventTuple(1074006099U, 5, EventLogEntryType.Information, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004DB3 RID: 19891
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_CONNECTION_CLOSED = new ExEventLog.EventTuple(1074006100U, 5, EventLogEntryType.Information, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004DB4 RID: 19892
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_LONG_RUNNING_OPERATION = new ExEventLog.EventTuple(2147747925U, 5, EventLogEntryType.Warning, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004DB5 RID: 19893
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_NON_UNIQUE_RECIPIENT = new ExEventLog.EventTuple(2147747928U, 4, EventLogEntryType.Warning, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DB6 RID: 19894
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_ROOTDSE_READ_FAILED = new ExEventLog.EventTuple(3221489753U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DB7 RID: 19895
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_NO_CONNECTION_TO_SERVER = new ExEventLog.EventTuple(3221489754U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DB8 RID: 19896
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_TOPO_INITIALIZATION_TIMEOUT = new ExEventLog.EventTuple(3221489755U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004DB9 RID: 19897
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_PREFERRED_TOPOLOGY = new ExEventLog.EventTuple(1074006109U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004DBA RID: 19898
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_ADAM_GET_SERVER_FROM_DOMAIN_DN = new ExEventLog.EventTuple(3221489759U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004DBB RID: 19899
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_AD_DRIVER_PERF_INIT_FAILED = new ExEventLog.EventTuple(2147747941U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004DBC RID: 19900
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_TOPOLOGY_UPDATE = new ExEventLog.EventTuple(1074006118U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004DBD RID: 19901
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_AD_DRIVER_INIT = new ExEventLog.EventTuple(1074006119U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004DBE RID: 19902
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_WRITE_FAILED = new ExEventLog.EventTuple(3221489769U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004DBF RID: 19903
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_RANGED_READ = new ExEventLog.EventTuple(1074006122U, 5, EventLogEntryType.Information, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DC0 RID: 19904
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_VALIDATION_FAILED_FCO_MODE_CONFIG = new ExEventLog.EventTuple(3221489771U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DC1 RID: 19905
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_VALIDATION_FAILED_FCO_MODE_RECIPIENT = new ExEventLog.EventTuple(3221489772U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DC2 RID: 19906
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_VALIDATION_FAILED_PCO_MODE_CONFIG = new ExEventLog.EventTuple(2147747949U, 6, EventLogEntryType.Warning, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DC3 RID: 19907
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_VALIDATION_FAILED_PCO_MODE_RECIPIENT = new ExEventLog.EventTuple(2147747950U, 6, EventLogEntryType.Warning, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DC4 RID: 19908
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_VALIDATION_FAILED_IGNORE_MODE_CONFIG = new ExEventLog.EventTuple(2147747951U, 6, EventLogEntryType.Warning, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DC5 RID: 19909
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_VALIDATION_FAILED_IGNORE_MODE_RECIPIENT = new ExEventLog.EventTuple(2147747952U, 6, EventLogEntryType.Warning, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DC6 RID: 19910
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_VALIDATION_FAILED_ATTRIBUTE = new ExEventLog.EventTuple(2147747953U, 6, EventLogEntryType.Warning, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DC7 RID: 19911
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_CONSTRAINT_READ_FAILED = new ExEventLog.EventTuple(2147747954U, 6, EventLogEntryType.Warning, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DC8 RID: 19912
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_DC_DOWN_FAULT = new ExEventLog.EventTuple(1074006132U, 3, EventLogEntryType.Warning, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DC9 RID: 19913
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_DUPLICATED_SERVER = new ExEventLog.EventTuple(3221489807U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004DCA RID: 19914
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_MULTIPLE_DEFAULT_ACCEPTED_DOMAIN = new ExEventLog.EventTuple(2147747984U, 9, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004DCB RID: 19915
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_INTERNAL_SUITABILITY_CHECK_FAILED = new ExEventLog.EventTuple(2147747985U, 3, EventLogEntryType.Warning, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004DCC RID: 19916
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_LDAP_SIZELIMIT_EXCEEDED = new ExEventLog.EventTuple(2147748100U, 5, EventLogEntryType.Warning, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DCD RID: 19917
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_LDAP_TIMEOUT = new ExEventLog.EventTuple(2147748181U, 5, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DCE RID: 19918
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_SERVER_DOES_NOT_HAVE_SITE = new ExEventLog.EventTuple(3221490018U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004DCF RID: 19919
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_RUS_SERVER_LOOKUP_FAILED = new ExEventLog.EventTuple(2147748243U, 7, EventLogEntryType.Warning, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004DD0 RID: 19920
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_ALLOW_IMPERSONATION = new ExEventLog.EventTuple(1074006420U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004DD1 RID: 19921
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_RPC_SERVER_TOO_BUSY = new ExEventLog.EventTuple(2147748245U, 9, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DD2 RID: 19922
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_ISSUE_NOTIFICATION_FAILURE = new ExEventLog.EventTuple(3221490070U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DD3 RID: 19923
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SITEMON_EVENT_CHECK_FAILED = new ExEventLog.EventTuple(3221490117U, 8, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DD4 RID: 19924
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SITEMON_EVENT_SITE_UPDATED = new ExEventLog.EventTuple(1074006471U, 8, EventLogEntryType.Information, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004DD5 RID: 19925
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ADTOPO_RPC_RESOLVE_SID_FAILED = new ExEventLog.EventTuple(2147748393U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DD6 RID: 19926
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ADTOPO_RPC_FLUSH_LOCALSYSTEM_TICKET_FAILED = new ExEventLog.EventTuple(3221490218U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DD7 RID: 19927
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_READ_ROOTDSE_FAILED = new ExEventLog.EventTuple(3221490219U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DD8 RID: 19928
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ADTopologyServiceStartSuccess = new ExEventLog.EventTuple(264944U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004DD9 RID: 19929
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ADTopologyServiceStopSuccess = new ExEventLog.EventTuple(264945U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004DDA RID: 19930
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_RODC_FOUND = new ExEventLog.EventTuple(2147748594U, 3, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DDB RID: 19931
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_AD_NOTIFICATION_CALLBACK_TIMED_OUT = new ExEventLog.EventTuple(3221490419U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DDC RID: 19932
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MoreThanOneOrganizationThrottlingPolicy = new ExEventLog.EventTuple(3221228374U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DDD RID: 19933
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InitializePerformanceCountersFailed = new ExEventLog.EventTuple(3221228375U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004DDE RID: 19934
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToReadThrottlingPolicy = new ExEventLog.EventTuple(3221228376U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004DDF RID: 19935
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DynamicDistributionGroupFilterError = new ExEventLog.EventTuple(3221228377U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DE0 RID: 19936
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GlobalThrottlingPolicyMissing = new ExEventLog.EventTuple(3221228378U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004DE1 RID: 19937
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MoreThanOneGlobalThrottlingPolicy = new ExEventLog.EventTuple(3221228379U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004DE2 RID: 19938
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UnableToLoadABProvider = new ExEventLog.EventTuple(3221228382U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DE3 RID: 19939
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ADTOPO_RPC_FLUSH_NETWORKSERVICE_TICKET_FAILED = new ExEventLog.EventTuple(3221490528U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DE4 RID: 19940
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UnableToFindGALForUser = new ExEventLog.EventTuple(2147486561U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DE5 RID: 19941
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DeletedThrottlingPolicyReferenced = new ExEventLog.EventTuple(3221228386U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004DE6 RID: 19942
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ExcessiveMassUserThrottling = new ExEventLog.EventTuple(3221228388U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DE7 RID: 19943
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InitializeResourceHealthPerformanceCountersFailed = new ExEventLog.EventTuple(3221228392U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004DE8 RID: 19944
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CompanyMainStreamCookiePersisted = new ExEventLog.EventTuple(1073744745U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004DE9 RID: 19945
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RecipientMainStreamCookiePersisted = new ExEventLog.EventTuple(1073744746U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004DEA RID: 19946
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TenantFullSyncCompanyPageTokenPersisted = new ExEventLog.EventTuple(1073744747U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004DEB RID: 19947
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TenantFullSyncRecipientPageTokenPersisted = new ExEventLog.EventTuple(1073744748U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004DEC RID: 19948
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TenantFullSyncCompanyPageTokenCleared = new ExEventLog.EventTuple(1073744749U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004DED RID: 19949
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ConcurrencyOverflowQueueTimeoutDetected = new ExEventLog.EventTuple(3221228398U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DEE RID: 19950
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ConcurrencyLongWaitInOverflowQueueDetected = new ExEventLog.EventTuple(2147486575U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DEF RID: 19951
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ConcurrencyOverflowSizeLimitReached = new ExEventLog.EventTuple(3221228400U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DF0 RID: 19952
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ConcurrencyOverflowSizeWarningLimitReached = new ExEventLog.EventTuple(2147486577U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DF1 RID: 19953
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ConcurrencyStartConcurrencyDoesNotMatchEnd = new ExEventLog.EventTuple(3221228402U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DF2 RID: 19954
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ConcurrencyCorruptedState = new ExEventLog.EventTuple(3221228403U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DF3 RID: 19955
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ConcurrencyResourceBackToHealthy = new ExEventLog.EventTuple(1073744756U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DF4 RID: 19956
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SyncObjectInvalidProxyAddressStripped = new ExEventLog.EventTuple(2147486581U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004DF5 RID: 19957
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TenantFullSyncRecipientPageTokenCleared = new ExEventLog.EventTuple(1073744758U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004DF6 RID: 19958
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ResourceHealthRemoteCounterReadTimedOut = new ExEventLog.EventTuple(3221228407U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004DF7 RID: 19959
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ResourceHealthRemoteCounterFailed = new ExEventLog.EventTuple(3221228408U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DF8 RID: 19960
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_DeletedObjectIdLinked = new ExEventLog.EventTuple(2147486585U, 6, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04004DF9 RID: 19961
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MaxResourceConcurrencyReached = new ExEventLog.EventTuple(3221228410U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DFA RID: 19962
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UMCountryListNotFound = new ExEventLog.EventTuple(3221228411U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004DFB RID: 19963
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidNotificationRequest = new ExEventLog.EventTuple(3221228412U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004DFC RID: 19964
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidNotificationRequestForDeletedObjects = new ExEventLog.EventTuple(3221228413U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DFD RID: 19965
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ADNotificationsMaxNumberOfNotificationsPerConnection = new ExEventLog.EventTuple(1073744766U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004DFE RID: 19966
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TransportDeletedADNotificationReceived = new ExEventLog.EventTuple(1073744768U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004DFF RID: 19967
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MaximumNumberOrNotificationsForDeletedObjects = new ExEventLog.EventTuple(3221228415U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004E00 RID: 19968
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToCleanupCookies = new ExEventLog.EventTuple(2147486593U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004E01 RID: 19969
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ADHealthReport = new ExEventLog.EventTuple(1073744824U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004E02 RID: 19970
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ADHealthFailed = new ExEventLog.EventTuple(3221228473U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004E03 RID: 19971
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PolicyRefresh = new ExEventLog.EventTuple(1073744834U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004E04 RID: 19972
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PCEnterReadLockFailed = new ExEventLog.EventTuple(2147487649U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004E05 RID: 19973
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PCEnterWriteLockFailed = new ExEventLog.EventTuple(2147487650U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004E06 RID: 19974
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PCEnterReadLockForOrgRemovalFailed = new ExEventLog.EventTuple(2147487651U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004E07 RID: 19975
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PCEnterWriteLockForOrgDataRemovalFailed = new ExEventLog.EventTuple(2147487652U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004E08 RID: 19976
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PCProvisioningCacheEnabled = new ExEventLog.EventTuple(1073745829U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004E09 RID: 19977
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PCResettingWholeProvisioningCache = new ExEventLog.EventTuple(1073745830U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004E0A RID: 19978
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PCClearingExpiredOrganizations = new ExEventLog.EventTuple(1073745831U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004E0B RID: 19979
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PCClearingExpiredOrganizationsFinished = new ExEventLog.EventTuple(1073745832U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004E0C RID: 19980
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PCResettingOrganizationData = new ExEventLog.EventTuple(1073745833U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004E0D RID: 19981
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PCResettingOrganizationDataFinished = new ExEventLog.EventTuple(1073745834U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004E0E RID: 19982
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PCResettingGlobalData = new ExEventLog.EventTuple(1073745835U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004E0F RID: 19983
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PCResettingGlobalDataFinished = new ExEventLog.EventTuple(1073745836U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004E10 RID: 19984
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PCOrganizationDataInvalidated = new ExEventLog.EventTuple(1073745837U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004E11 RID: 19985
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PCGlobalDataInvalidated = new ExEventLog.EventTuple(1073745838U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004E12 RID: 19986
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PCInvalidationMessageFailedBroadcast = new ExEventLog.EventTuple(3221229488U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004E13 RID: 19987
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PCStartingToReceiveInvalidationMessage = new ExEventLog.EventTuple(1073745841U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004E14 RID: 19988
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PCInvalidInvalidationMessageReceived = new ExEventLog.EventTuple(3221229490U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004E15 RID: 19989
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PCFailedToReceiveInvalidationMessage = new ExEventLog.EventTuple(3221229491U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004E16 RID: 19990
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PCUnhandledExceptionInActivity = new ExEventLog.EventTuple(3221229492U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004E17 RID: 19991
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PCBadGlobalCacheKeyReceived = new ExEventLog.EventTuple(2147487669U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004E18 RID: 19992
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LowResourceHealthMeasureAverage = new ExEventLog.EventTuple(2147487670U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004E19 RID: 19993
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UserLockedOutThrottling = new ExEventLog.EventTuple(3221229495U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004E1A RID: 19994
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CannotResolveExternalDirectoryOrganizationId = new ExEventLog.EventTuple(3221229496U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004E1B RID: 19995
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UserNoLongerLockedOutThrottling = new ExEventLog.EventTuple(3221229500U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004E1C RID: 19996
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_CANNOT_CONTACT_AD_TOPOLOGY_SERVICE = new ExEventLog.EventTuple(3221491643U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004E1D RID: 19997
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SyncPropertySetStartingUpgrade = new ExEventLog.EventTuple(1073745852U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004E1E RID: 19998
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SyncPropertySetFinishedUpgrade = new ExEventLog.EventTuple(1073745854U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004E1F RID: 19999
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RelocationServiceTransientException = new ExEventLog.EventTuple(3221229503U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004E20 RID: 20000
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RelocationServicePermanentException = new ExEventLog.EventTuple(3221229504U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004E21 RID: 20001
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RelocationServiceRemoveUserExperienceMonitoringAccountError = new ExEventLog.EventTuple(3221229505U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004E22 RID: 20002
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PCActivityExit = new ExEventLog.EventTuple(1073745857U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004E23 RID: 20003
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PCFailedToExitActivity = new ExEventLog.EventTuple(3221229506U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004E24 RID: 20004
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PCStartToExitActivity = new ExEventLog.EventTuple(1073745859U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004E25 RID: 20005
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PCStartingToReceiveDiagnosticCommand = new ExEventLog.EventTuple(1073745860U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004E26 RID: 20006
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PCFailedToReceiveDiagnosticCommand = new ExEventLog.EventTuple(3221229509U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004E27 RID: 20007
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PCInvalidDiagnosticCommandReceived = new ExEventLog.EventTuple(3221229510U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004E28 RID: 20008
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PCFailedToReceiveClientDiagnosticDommand = new ExEventLog.EventTuple(3221229511U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004E29 RID: 20009
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UpgradeServiceTransientException = new ExEventLog.EventTuple(3221229512U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004E2A RID: 20010
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UpgradeServicePermanentException = new ExEventLog.EventTuple(3221229513U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004E2B RID: 20011
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CannotResolveMSAUserNetID = new ExEventLog.EventTuple(3221229514U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004E2C RID: 20012
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CannotDeleteMServEntry = new ExEventLog.EventTuple(3221491716U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004E2D RID: 20013
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_BudgetActionExceededExpectedTime = new ExEventLog.EventTuple(3221229573U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004E2E RID: 20014
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_WcfClientConfigError = new ExEventLog.EventTuple(3221229575U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004E2F RID: 20015
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DnsThroubleshooterError = new ExEventLog.EventTuple(3221491720U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004E30 RID: 20016
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SessionIsScopedToRetiredTenantError = new ExEventLog.EventTuple(3221491721U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004E31 RID: 20017
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CannotContactGLS = new ExEventLog.EventTuple(3221491723U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004E32 RID: 20018
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PCProvisioningCacheInitializationFailed = new ExEventLog.EventTuple(3221229580U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004E33 RID: 20019
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ServerComponentStateSetOffline = new ExEventLog.EventTuple(3221229581U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004E34 RID: 20020
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ServerComponentStateSetOnline = new ExEventLog.EventTuple(1073745934U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004E35 RID: 20021
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CannotContactADCacheService = new ExEventLog.EventTuple(3221491727U, 12, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004E36 RID: 20022
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UpdateServerSettingsAfterSuitabilityError = new ExEventLog.EventTuple(3221491728U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004E37 RID: 20023
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ConfigurationSettingsLoadError = new ExEventLog.EventTuple(3221229585U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004E38 RID: 20024
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_GetActivityContextFailed = new ExEventLog.EventTuple(3221491729U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004E39 RID: 20025
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PCSocketExceptionDisabledProvisioningCache = new ExEventLog.EventTuple(2147487762U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004E3A RID: 20026
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ReadADCacheConfigurationFailed = new ExEventLog.EventTuple(3221491731U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004E3B RID: 20027
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CallADCacheServiceFailed = new ExEventLog.EventTuple(3221491732U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004E3C RID: 20028
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ADCacheServiceUnexpectedException = new ExEventLog.EventTuple(3221491733U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004E3D RID: 20029
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_WrongObjectReturned = new ExEventLog.EventTuple(3221491734U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004E3E RID: 20030
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidDatabasesCacheOnAllSites = new ExEventLog.EventTuple(1073745943U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004E3F RID: 20031
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DirectoryTaskTransientException = new ExEventLog.EventTuple(3221229592U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004E40 RID: 20032
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DirectoryTaskPermanentException = new ExEventLog.EventTuple(3221229593U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004E41 RID: 20033
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SettingOverrideValidationError = new ExEventLog.EventTuple(3221229594U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04004E42 RID: 20034
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ApiNotSupported = new ExEventLog.EventTuple(3221229595U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04004E43 RID: 20035
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ApiInputNotSupported = new ExEventLog.EventTuple(3221229596U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x02000A3E RID: 2622
		private enum Category : short
		{
			// Token: 0x04004E45 RID: 20037
			General = 1,
			// Token: 0x04004E46 RID: 20038
			Cache,
			// Token: 0x04004E47 RID: 20039
			Topology,
			// Token: 0x04004E48 RID: 20040
			Configuration,
			// Token: 0x04004E49 RID: 20041
			LDAP,
			// Token: 0x04004E4A RID: 20042
			Validation,
			// Token: 0x04004E4B RID: 20043
			Recipient_Update_Service,
			// Token: 0x04004E4C RID: 20044
			Site_Update,
			// Token: 0x04004E4D RID: 20045
			Exchange_Topology,
			// Token: 0x04004E4E RID: 20046
			MSERV,
			// Token: 0x04004E4F RID: 20047
			GLS,
			// Token: 0x04004E50 RID: 20048
			Directory_Cache
		}

		// Token: 0x02000A3F RID: 2623
		internal enum Message : uint
		{
			// Token: 0x04004E52 RID: 20050
			DSC_EVENT_SYNC_FAILED = 3221489677U,
			// Token: 0x04004E53 RID: 20051
			DSC_EVENT_DC_DOWN = 1074006038U,
			// Token: 0x04004E54 RID: 20052
			DSC_EVENT_FIND_LOCAL_SERVER_FAILED = 3221489691U,
			// Token: 0x04004E55 RID: 20053
			DSC_EVENT_DISCOVERED_SERVERS = 1074006048U,
			// Token: 0x04004E56 RID: 20054
			DSC_EVENT_GOING_IN_SITE_DC = 1074006050U,
			// Token: 0x04004E57 RID: 20055
			DSC_EVENT_GOING_IN_SITE_GC,
			// Token: 0x04004E58 RID: 20056
			DSC_EVENT_REG_BAD_DWORD = 1074006056U,
			// Token: 0x04004E59 RID: 20057
			DSC_EVENT_REG_CDC_BAD = 2147747881U,
			// Token: 0x04004E5A RID: 20058
			DSC_EVENT_REG_CDC_DOWN,
			// Token: 0x04004E5B RID: 20059
			DSC_EVENT_REG_SERVER_BAD,
			// Token: 0x04004E5C RID: 20060
			DSC_EVENT_REG_DC = 1074006060U,
			// Token: 0x04004E5D RID: 20061
			DSC_EVENT_REG_GC,
			// Token: 0x04004E5E RID: 20062
			DSC_EVENT_REG_CDC = 1074006064U,
			// Token: 0x04004E5F RID: 20063
			DSC_EVENT_ALL_DC_DOWN = 3221489718U,
			// Token: 0x04004E60 RID: 20064
			DSC_EVENT_ALL_GC_DOWN,
			// Token: 0x04004E61 RID: 20065
			DSC_EVENT_GETHOSTBYNAME_FAILED = 2147747899U,
			// Token: 0x04004E62 RID: 20066
			DSC_EVENT_BIND_FAILED = 3221489726U,
			// Token: 0x04004E63 RID: 20067
			DSC_EVENT_SUITABILITY_CHECK_FAILED,
			// Token: 0x04004E64 RID: 20068
			DSC_EVENT_NO_SACL = 2147747904U,
			// Token: 0x04004E65 RID: 20069
			DSC_EVENT_GOT_SACL = 1074006081U,
			// Token: 0x04004E66 RID: 20070
			DSC_EVENT_FATAL_ERROR = 2147747907U,
			// Token: 0x04004E67 RID: 20071
			DSC_EVENT_BAD_OS_VERSION,
			// Token: 0x04004E68 RID: 20072
			DSC_EVENT_IMPERSONATED_CALLER,
			// Token: 0x04004E69 RID: 20073
			DSC_EVENT_DNS_DIAG_SERVER_FAILURE = 3221489734U,
			// Token: 0x04004E6A RID: 20074
			DSC_EVENT_DNS_NAME_ERROR,
			// Token: 0x04004E6B RID: 20075
			DSC_EVENT_DNS_TIMEOUT,
			// Token: 0x04004E6C RID: 20076
			DSC_EVENT_DNS_NO_ERROR = 2147747913U,
			// Token: 0x04004E6D RID: 20077
			DSC_EVENT_DNS_OTHER = 3221489738U,
			// Token: 0x04004E6E RID: 20078
			DSC_EVENT_DNS_NO_ERROR_DC_FOUND = 2147747915U,
			// Token: 0x04004E6F RID: 20079
			DSC_EVENT_DNS_NO_ERROR_DC_NOT_FOUND,
			// Token: 0x04004E70 RID: 20080
			DSC_EVENT_NO_CONNECTION = 3221489742U,
			// Token: 0x04004E71 RID: 20081
			DSC_EVENT_DC_UP = 1074006095U,
			// Token: 0x04004E72 RID: 20082
			DSC_EVENT_BASE_CONFIG_SEARCH_FAILED = 2147747920U,
			// Token: 0x04004E73 RID: 20083
			DSC_EVENT_GET_DC_FROM_DOMAIN = 1074006097U,
			// Token: 0x04004E74 RID: 20084
			DSC_EVENT_GET_DC_FROM_DOMAIN_FAILED = 3221489746U,
			// Token: 0x04004E75 RID: 20085
			DSC_EVENT_NEW_CONNECTION = 1074006099U,
			// Token: 0x04004E76 RID: 20086
			DSC_EVENT_CONNECTION_CLOSED,
			// Token: 0x04004E77 RID: 20087
			DSC_EVENT_LONG_RUNNING_OPERATION = 2147747925U,
			// Token: 0x04004E78 RID: 20088
			DSC_EVENT_NON_UNIQUE_RECIPIENT = 2147747928U,
			// Token: 0x04004E79 RID: 20089
			DSC_EVENT_ROOTDSE_READ_FAILED = 3221489753U,
			// Token: 0x04004E7A RID: 20090
			DSC_EVENT_NO_CONNECTION_TO_SERVER,
			// Token: 0x04004E7B RID: 20091
			DSC_EVENT_TOPO_INITIALIZATION_TIMEOUT,
			// Token: 0x04004E7C RID: 20092
			DSC_EVENT_PREFERRED_TOPOLOGY = 1074006109U,
			// Token: 0x04004E7D RID: 20093
			DSC_EVENT_ADAM_GET_SERVER_FROM_DOMAIN_DN = 3221489759U,
			// Token: 0x04004E7E RID: 20094
			DSC_EVENT_AD_DRIVER_PERF_INIT_FAILED = 2147747941U,
			// Token: 0x04004E7F RID: 20095
			DSC_EVENT_TOPOLOGY_UPDATE = 1074006118U,
			// Token: 0x04004E80 RID: 20096
			DSC_EVENT_AD_DRIVER_INIT,
			// Token: 0x04004E81 RID: 20097
			DSC_EVENT_WRITE_FAILED = 3221489769U,
			// Token: 0x04004E82 RID: 20098
			DSC_EVENT_RANGED_READ = 1074006122U,
			// Token: 0x04004E83 RID: 20099
			DSC_EVENT_VALIDATION_FAILED_FCO_MODE_CONFIG = 3221489771U,
			// Token: 0x04004E84 RID: 20100
			DSC_EVENT_VALIDATION_FAILED_FCO_MODE_RECIPIENT,
			// Token: 0x04004E85 RID: 20101
			DSC_EVENT_VALIDATION_FAILED_PCO_MODE_CONFIG = 2147747949U,
			// Token: 0x04004E86 RID: 20102
			DSC_EVENT_VALIDATION_FAILED_PCO_MODE_RECIPIENT,
			// Token: 0x04004E87 RID: 20103
			DSC_EVENT_VALIDATION_FAILED_IGNORE_MODE_CONFIG,
			// Token: 0x04004E88 RID: 20104
			DSC_EVENT_VALIDATION_FAILED_IGNORE_MODE_RECIPIENT,
			// Token: 0x04004E89 RID: 20105
			DSC_EVENT_VALIDATION_FAILED_ATTRIBUTE,
			// Token: 0x04004E8A RID: 20106
			DSC_EVENT_CONSTRAINT_READ_FAILED,
			// Token: 0x04004E8B RID: 20107
			DSC_EVENT_DC_DOWN_FAULT = 1074006132U,
			// Token: 0x04004E8C RID: 20108
			DSC_EVENT_DUPLICATED_SERVER = 3221489807U,
			// Token: 0x04004E8D RID: 20109
			DSC_EVENT_MULTIPLE_DEFAULT_ACCEPTED_DOMAIN = 2147747984U,
			// Token: 0x04004E8E RID: 20110
			DSC_EVENT_INTERNAL_SUITABILITY_CHECK_FAILED,
			// Token: 0x04004E8F RID: 20111
			DSC_EVENT_LDAP_SIZELIMIT_EXCEEDED = 2147748100U,
			// Token: 0x04004E90 RID: 20112
			DSC_EVENT_LDAP_TIMEOUT = 2147748181U,
			// Token: 0x04004E91 RID: 20113
			DSC_EVENT_SERVER_DOES_NOT_HAVE_SITE = 3221490018U,
			// Token: 0x04004E92 RID: 20114
			DSC_EVENT_RUS_SERVER_LOOKUP_FAILED = 2147748243U,
			// Token: 0x04004E93 RID: 20115
			DSC_EVENT_ALLOW_IMPERSONATION = 1074006420U,
			// Token: 0x04004E94 RID: 20116
			DSC_EVENT_RPC_SERVER_TOO_BUSY = 2147748245U,
			// Token: 0x04004E95 RID: 20117
			DSC_EVENT_ISSUE_NOTIFICATION_FAILURE = 3221490070U,
			// Token: 0x04004E96 RID: 20118
			SITEMON_EVENT_CHECK_FAILED = 3221490117U,
			// Token: 0x04004E97 RID: 20119
			SITEMON_EVENT_SITE_UPDATED = 1074006471U,
			// Token: 0x04004E98 RID: 20120
			ADTOPO_RPC_RESOLVE_SID_FAILED = 2147748393U,
			// Token: 0x04004E99 RID: 20121
			ADTOPO_RPC_FLUSH_LOCALSYSTEM_TICKET_FAILED = 3221490218U,
			// Token: 0x04004E9A RID: 20122
			DSC_EVENT_READ_ROOTDSE_FAILED,
			// Token: 0x04004E9B RID: 20123
			ADTopologyServiceStartSuccess = 264944U,
			// Token: 0x04004E9C RID: 20124
			ADTopologyServiceStopSuccess,
			// Token: 0x04004E9D RID: 20125
			DSC_EVENT_RODC_FOUND = 2147748594U,
			// Token: 0x04004E9E RID: 20126
			DSC_EVENT_AD_NOTIFICATION_CALLBACK_TIMED_OUT = 3221490419U,
			// Token: 0x04004E9F RID: 20127
			MoreThanOneOrganizationThrottlingPolicy = 3221228374U,
			// Token: 0x04004EA0 RID: 20128
			InitializePerformanceCountersFailed,
			// Token: 0x04004EA1 RID: 20129
			FailedToReadThrottlingPolicy,
			// Token: 0x04004EA2 RID: 20130
			DynamicDistributionGroupFilterError,
			// Token: 0x04004EA3 RID: 20131
			GlobalThrottlingPolicyMissing,
			// Token: 0x04004EA4 RID: 20132
			MoreThanOneGlobalThrottlingPolicy,
			// Token: 0x04004EA5 RID: 20133
			UnableToLoadABProvider = 3221228382U,
			// Token: 0x04004EA6 RID: 20134
			ADTOPO_RPC_FLUSH_NETWORKSERVICE_TICKET_FAILED = 3221490528U,
			// Token: 0x04004EA7 RID: 20135
			UnableToFindGALForUser = 2147486561U,
			// Token: 0x04004EA8 RID: 20136
			DeletedThrottlingPolicyReferenced = 3221228386U,
			// Token: 0x04004EA9 RID: 20137
			ExcessiveMassUserThrottling = 3221228388U,
			// Token: 0x04004EAA RID: 20138
			InitializeResourceHealthPerformanceCountersFailed = 3221228392U,
			// Token: 0x04004EAB RID: 20139
			CompanyMainStreamCookiePersisted = 1073744745U,
			// Token: 0x04004EAC RID: 20140
			RecipientMainStreamCookiePersisted,
			// Token: 0x04004EAD RID: 20141
			TenantFullSyncCompanyPageTokenPersisted,
			// Token: 0x04004EAE RID: 20142
			TenantFullSyncRecipientPageTokenPersisted,
			// Token: 0x04004EAF RID: 20143
			TenantFullSyncCompanyPageTokenCleared,
			// Token: 0x04004EB0 RID: 20144
			ConcurrencyOverflowQueueTimeoutDetected = 3221228398U,
			// Token: 0x04004EB1 RID: 20145
			ConcurrencyLongWaitInOverflowQueueDetected = 2147486575U,
			// Token: 0x04004EB2 RID: 20146
			ConcurrencyOverflowSizeLimitReached = 3221228400U,
			// Token: 0x04004EB3 RID: 20147
			ConcurrencyOverflowSizeWarningLimitReached = 2147486577U,
			// Token: 0x04004EB4 RID: 20148
			ConcurrencyStartConcurrencyDoesNotMatchEnd = 3221228402U,
			// Token: 0x04004EB5 RID: 20149
			ConcurrencyCorruptedState,
			// Token: 0x04004EB6 RID: 20150
			ConcurrencyResourceBackToHealthy = 1073744756U,
			// Token: 0x04004EB7 RID: 20151
			SyncObjectInvalidProxyAddressStripped = 2147486581U,
			// Token: 0x04004EB8 RID: 20152
			TenantFullSyncRecipientPageTokenCleared = 1073744758U,
			// Token: 0x04004EB9 RID: 20153
			ResourceHealthRemoteCounterReadTimedOut = 3221228407U,
			// Token: 0x04004EBA RID: 20154
			ResourceHealthRemoteCounterFailed,
			// Token: 0x04004EBB RID: 20155
			DeletedObjectIdLinked = 2147486585U,
			// Token: 0x04004EBC RID: 20156
			MaxResourceConcurrencyReached = 3221228410U,
			// Token: 0x04004EBD RID: 20157
			UMCountryListNotFound,
			// Token: 0x04004EBE RID: 20158
			InvalidNotificationRequest,
			// Token: 0x04004EBF RID: 20159
			InvalidNotificationRequestForDeletedObjects,
			// Token: 0x04004EC0 RID: 20160
			ADNotificationsMaxNumberOfNotificationsPerConnection = 1073744766U,
			// Token: 0x04004EC1 RID: 20161
			TransportDeletedADNotificationReceived = 1073744768U,
			// Token: 0x04004EC2 RID: 20162
			MaximumNumberOrNotificationsForDeletedObjects = 3221228415U,
			// Token: 0x04004EC3 RID: 20163
			FailedToCleanupCookies = 2147486593U,
			// Token: 0x04004EC4 RID: 20164
			ADHealthReport = 1073744824U,
			// Token: 0x04004EC5 RID: 20165
			ADHealthFailed = 3221228473U,
			// Token: 0x04004EC6 RID: 20166
			PolicyRefresh = 1073744834U,
			// Token: 0x04004EC7 RID: 20167
			PCEnterReadLockFailed = 2147487649U,
			// Token: 0x04004EC8 RID: 20168
			PCEnterWriteLockFailed,
			// Token: 0x04004EC9 RID: 20169
			PCEnterReadLockForOrgRemovalFailed,
			// Token: 0x04004ECA RID: 20170
			PCEnterWriteLockForOrgDataRemovalFailed,
			// Token: 0x04004ECB RID: 20171
			PCProvisioningCacheEnabled = 1073745829U,
			// Token: 0x04004ECC RID: 20172
			PCResettingWholeProvisioningCache,
			// Token: 0x04004ECD RID: 20173
			PCClearingExpiredOrganizations,
			// Token: 0x04004ECE RID: 20174
			PCClearingExpiredOrganizationsFinished,
			// Token: 0x04004ECF RID: 20175
			PCResettingOrganizationData,
			// Token: 0x04004ED0 RID: 20176
			PCResettingOrganizationDataFinished,
			// Token: 0x04004ED1 RID: 20177
			PCResettingGlobalData,
			// Token: 0x04004ED2 RID: 20178
			PCResettingGlobalDataFinished,
			// Token: 0x04004ED3 RID: 20179
			PCOrganizationDataInvalidated,
			// Token: 0x04004ED4 RID: 20180
			PCGlobalDataInvalidated,
			// Token: 0x04004ED5 RID: 20181
			PCInvalidationMessageFailedBroadcast = 3221229488U,
			// Token: 0x04004ED6 RID: 20182
			PCStartingToReceiveInvalidationMessage = 1073745841U,
			// Token: 0x04004ED7 RID: 20183
			PCInvalidInvalidationMessageReceived = 3221229490U,
			// Token: 0x04004ED8 RID: 20184
			PCFailedToReceiveInvalidationMessage,
			// Token: 0x04004ED9 RID: 20185
			PCUnhandledExceptionInActivity,
			// Token: 0x04004EDA RID: 20186
			PCBadGlobalCacheKeyReceived = 2147487669U,
			// Token: 0x04004EDB RID: 20187
			LowResourceHealthMeasureAverage,
			// Token: 0x04004EDC RID: 20188
			UserLockedOutThrottling = 3221229495U,
			// Token: 0x04004EDD RID: 20189
			CannotResolveExternalDirectoryOrganizationId,
			// Token: 0x04004EDE RID: 20190
			UserNoLongerLockedOutThrottling = 3221229500U,
			// Token: 0x04004EDF RID: 20191
			DSC_EVENT_CANNOT_CONTACT_AD_TOPOLOGY_SERVICE = 3221491643U,
			// Token: 0x04004EE0 RID: 20192
			SyncPropertySetStartingUpgrade = 1073745852U,
			// Token: 0x04004EE1 RID: 20193
			SyncPropertySetFinishedUpgrade = 1073745854U,
			// Token: 0x04004EE2 RID: 20194
			RelocationServiceTransientException = 3221229503U,
			// Token: 0x04004EE3 RID: 20195
			RelocationServicePermanentException,
			// Token: 0x04004EE4 RID: 20196
			RelocationServiceRemoveUserExperienceMonitoringAccountError,
			// Token: 0x04004EE5 RID: 20197
			PCActivityExit = 1073745857U,
			// Token: 0x04004EE6 RID: 20198
			PCFailedToExitActivity = 3221229506U,
			// Token: 0x04004EE7 RID: 20199
			PCStartToExitActivity = 1073745859U,
			// Token: 0x04004EE8 RID: 20200
			PCStartingToReceiveDiagnosticCommand,
			// Token: 0x04004EE9 RID: 20201
			PCFailedToReceiveDiagnosticCommand = 3221229509U,
			// Token: 0x04004EEA RID: 20202
			PCInvalidDiagnosticCommandReceived,
			// Token: 0x04004EEB RID: 20203
			PCFailedToReceiveClientDiagnosticDommand,
			// Token: 0x04004EEC RID: 20204
			UpgradeServiceTransientException,
			// Token: 0x04004EED RID: 20205
			UpgradeServicePermanentException,
			// Token: 0x04004EEE RID: 20206
			CannotResolveMSAUserNetID,
			// Token: 0x04004EEF RID: 20207
			CannotDeleteMServEntry = 3221491716U,
			// Token: 0x04004EF0 RID: 20208
			BudgetActionExceededExpectedTime = 3221229573U,
			// Token: 0x04004EF1 RID: 20209
			WcfClientConfigError = 3221229575U,
			// Token: 0x04004EF2 RID: 20210
			DnsThroubleshooterError = 3221491720U,
			// Token: 0x04004EF3 RID: 20211
			SessionIsScopedToRetiredTenantError,
			// Token: 0x04004EF4 RID: 20212
			CannotContactGLS = 3221491723U,
			// Token: 0x04004EF5 RID: 20213
			PCProvisioningCacheInitializationFailed = 3221229580U,
			// Token: 0x04004EF6 RID: 20214
			ServerComponentStateSetOffline,
			// Token: 0x04004EF7 RID: 20215
			ServerComponentStateSetOnline = 1073745934U,
			// Token: 0x04004EF8 RID: 20216
			CannotContactADCacheService = 3221491727U,
			// Token: 0x04004EF9 RID: 20217
			UpdateServerSettingsAfterSuitabilityError,
			// Token: 0x04004EFA RID: 20218
			ConfigurationSettingsLoadError = 3221229585U,
			// Token: 0x04004EFB RID: 20219
			GetActivityContextFailed = 3221491729U,
			// Token: 0x04004EFC RID: 20220
			PCSocketExceptionDisabledProvisioningCache = 2147487762U,
			// Token: 0x04004EFD RID: 20221
			ReadADCacheConfigurationFailed = 3221491731U,
			// Token: 0x04004EFE RID: 20222
			CallADCacheServiceFailed,
			// Token: 0x04004EFF RID: 20223
			ADCacheServiceUnexpectedException,
			// Token: 0x04004F00 RID: 20224
			WrongObjectReturned,
			// Token: 0x04004F01 RID: 20225
			InvalidDatabasesCacheOnAllSites = 1073745943U,
			// Token: 0x04004F02 RID: 20226
			DirectoryTaskTransientException = 3221229592U,
			// Token: 0x04004F03 RID: 20227
			DirectoryTaskPermanentException,
			// Token: 0x04004F04 RID: 20228
			SettingOverrideValidationError,
			// Token: 0x04004F05 RID: 20229
			ApiNotSupported,
			// Token: 0x04004F06 RID: 20230
			ApiInputNotSupported
		}
	}
}

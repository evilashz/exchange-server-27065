using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Configuration.RedirectionModule.EventLog
{
	// Token: 0x02000012 RID: 18
	public static class TaskEventLogConstants
	{
		// Token: 0x04000046 RID: 70
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LiveIdRedirection_ServerError = new ExEventLog.EventTuple(3221225622U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000047 RID: 71
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LiveIdRedirection_FailedSidMapping = new ExEventLog.EventTuple(3221225623U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000048 RID: 72
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LiveIdRedirection_FailedWindowsIdMapping = new ExEventLog.EventTuple(3221225624U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000049 RID: 73
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LiveIdRedirection_FailedPUIDMapping = new ExEventLog.EventTuple(3221225625U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400004A RID: 74
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LiveIdRedirection_FailedExtractMemberName = new ExEventLog.EventTuple(3221225626U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400004B RID: 75
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LiveIdRedirection_FailedToResolveForestRedirection = new ExEventLog.EventTuple(3221225627U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400004C RID: 76
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LiveIdRedirection_UsingManagementSiteLink = new ExEventLog.EventTuple(3221225629U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400004D RID: 77
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LiveIdRedirection_TargetSitePresentOnResponsibleForSite = new ExEventLog.EventTuple(3221225630U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400004E RID: 78
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TenantRedirection_ServerError = new ExEventLog.EventTuple(3221225722U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400004F RID: 79
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TenantRedirection_FailedToResolveForestRedirection = new ExEventLog.EventTuple(3221225723U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000050 RID: 80
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReaderWriterLock_Timeout = new ExEventLog.EventTuple(3221225724U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x02000013 RID: 19
		private enum Category : short
		{
			// Token: 0x04000052 RID: 82
			General = 1
		}

		// Token: 0x02000014 RID: 20
		internal enum Message : uint
		{
			// Token: 0x04000054 RID: 84
			LiveIdRedirection_ServerError = 3221225622U,
			// Token: 0x04000055 RID: 85
			LiveIdRedirection_FailedSidMapping,
			// Token: 0x04000056 RID: 86
			LiveIdRedirection_FailedWindowsIdMapping,
			// Token: 0x04000057 RID: 87
			LiveIdRedirection_FailedPUIDMapping,
			// Token: 0x04000058 RID: 88
			LiveIdRedirection_FailedExtractMemberName,
			// Token: 0x04000059 RID: 89
			LiveIdRedirection_FailedToResolveForestRedirection,
			// Token: 0x0400005A RID: 90
			LiveIdRedirection_UsingManagementSiteLink = 3221225629U,
			// Token: 0x0400005B RID: 91
			LiveIdRedirection_TargetSitePresentOnResponsibleForSite,
			// Token: 0x0400005C RID: 92
			TenantRedirection_ServerError = 3221225722U,
			// Token: 0x0400005D RID: 93
			TenantRedirection_FailedToResolveForestRedirection,
			// Token: 0x0400005E RID: 94
			ReaderWriterLock_Timeout
		}
	}
}

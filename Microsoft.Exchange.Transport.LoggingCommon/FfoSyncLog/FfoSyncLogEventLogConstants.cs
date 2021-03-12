using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.FfoSyncLog
{
	// Token: 0x0200001F RID: 31
	public static class FfoSyncLogEventLogConstants
	{
		// Token: 0x0400011D RID: 285
		public const string EventSource = "FfoSyncLog";

		// Token: 0x0400011E RID: 286
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FfoSyncLogConfigured = new ExEventLog.EventTuple(1073742825U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400011F RID: 287
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FfoSyncLogLogPathNotConfigured = new ExEventLog.EventTuple(1073742826U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000120 RID: 288
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FfoSyncLogConfigRegistryReadAccessException = new ExEventLog.EventTuple(3221226475U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000121 RID: 289
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FfoSyncLogFormatException = new ExEventLog.EventTuple(2147484652U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000122 RID: 290
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FfoSyncLogADOperationException = new ExEventLog.EventTuple(3221226477U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x02000020 RID: 32
		private enum Category : short
		{
			// Token: 0x04000124 RID: 292
			General = 1
		}

		// Token: 0x02000021 RID: 33
		internal enum Message : uint
		{
			// Token: 0x04000126 RID: 294
			FfoSyncLogConfigured = 1073742825U,
			// Token: 0x04000127 RID: 295
			FfoSyncLogLogPathNotConfigured,
			// Token: 0x04000128 RID: 296
			FfoSyncLogConfigRegistryReadAccessException = 3221226475U,
			// Token: 0x04000129 RID: 297
			FfoSyncLogFormatException = 2147484652U,
			// Token: 0x0400012A RID: 298
			FfoSyncLogADOperationException = 3221226477U
		}
	}
}

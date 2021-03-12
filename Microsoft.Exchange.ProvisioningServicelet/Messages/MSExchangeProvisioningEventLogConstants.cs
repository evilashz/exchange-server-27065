using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.Provisioning.Messages
{
	// Token: 0x02000017 RID: 23
	public static class MSExchangeProvisioningEventLogConstants
	{
		// Token: 0x040000AE RID: 174
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TransientException = new ExEventLog.EventTuple(3221227473U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000AF RID: 175
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PermanentException = new ExEventLog.EventTuple(3221227474U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000B0 RID: 176
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TenantAdminNotFound = new ExEventLog.EventTuple(3221227475U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000B1 RID: 177
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OneProvisioningRoundCompleted = new ExEventLog.EventTuple(1073743831U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000B2 RID: 178
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ProvisioningRoundCompleted = new ExEventLog.EventTuple(1073743832U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000B3 RID: 179
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_NonFatalProcessingError = new ExEventLog.EventTuple(3221227482U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000B4 RID: 180
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ItemWillBeRetried = new ExEventLog.EventTuple(1073743835U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x02000018 RID: 24
		private enum Category : short
		{
			// Token: 0x040000B6 RID: 182
			General = 1
		}

		// Token: 0x02000019 RID: 25
		internal enum Message : uint
		{
			// Token: 0x040000B8 RID: 184
			TransientException = 3221227473U,
			// Token: 0x040000B9 RID: 185
			PermanentException,
			// Token: 0x040000BA RID: 186
			TenantAdminNotFound,
			// Token: 0x040000BB RID: 187
			OneProvisioningRoundCompleted = 1073743831U,
			// Token: 0x040000BC RID: 188
			ProvisioningRoundCompleted,
			// Token: 0x040000BD RID: 189
			NonFatalProcessingError = 3221227482U,
			// Token: 0x040000BE RID: 190
			ItemWillBeRetried = 1073743835U
		}
	}
}

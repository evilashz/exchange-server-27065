using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200018E RID: 398
	public static class MSExchangeSyncMigrationEventLogConstants
	{
		// Token: 0x0400066E RID: 1646
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CriticalError = new ExEventLog.EventTuple(3221227473U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400066F RID: 1647
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PermanentException = new ExEventLog.EventTuple(3221227474U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000670 RID: 1648
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CorruptMigrationJob = new ExEventLog.EventTuple(3221227475U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000671 RID: 1649
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CorruptMigrationJobItem = new ExEventLog.EventTuple(3221227476U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000672 RID: 1650
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_IgnoredException = new ExEventLog.EventTuple(3221227477U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0200018F RID: 399
		private enum Category : short
		{
			// Token: 0x04000674 RID: 1652
			General = 1
		}

		// Token: 0x02000190 RID: 400
		internal enum Message : uint
		{
			// Token: 0x04000676 RID: 1654
			CriticalError = 3221227473U,
			// Token: 0x04000677 RID: 1655
			PermanentException,
			// Token: 0x04000678 RID: 1656
			CorruptMigrationJob,
			// Token: 0x04000679 RID: 1657
			CorruptMigrationJobItem,
			// Token: 0x0400067A RID: 1658
			IgnoredException
		}
	}
}

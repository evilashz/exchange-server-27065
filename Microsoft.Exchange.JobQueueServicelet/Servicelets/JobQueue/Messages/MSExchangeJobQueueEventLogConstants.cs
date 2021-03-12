using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.JobQueue.Messages
{
	// Token: 0x0200001B RID: 27
	public static class MSExchangeJobQueueEventLogConstants
	{
		// Token: 0x040000B3 RID: 179
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToRegisterRpc = new ExEventLog.EventTuple(3221226473U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000B4 RID: 180
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToLoadAppConfig = new ExEventLog.EventTuple(3221226474U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0200001C RID: 28
		private enum Category : short
		{
			// Token: 0x040000B6 RID: 182
			General = 1
		}

		// Token: 0x0200001D RID: 29
		internal enum Message : uint
		{
			// Token: 0x040000B8 RID: 184
			FailedToRegisterRpc = 3221226473U,
			// Token: 0x040000B9 RID: 185
			FailedToLoadAppConfig
		}
	}
}

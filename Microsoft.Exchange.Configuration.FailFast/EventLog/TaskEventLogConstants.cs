using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Configuration.FailFast.EventLog
{
	// Token: 0x02000012 RID: 18
	public static class TaskEventLogConstants
	{
		// Token: 0x04000039 RID: 57
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LogFailFastEnabledFlag = new ExEventLog.EventTuple(1073741825U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400003A RID: 58
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LogUserAddedToFailFastUserCached = new ExEventLog.EventTuple(3221225474U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400003B RID: 59
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LogIncorrectUserInfoReceivedOnServerStream = new ExEventLog.EventTuple(3221225475U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400003C RID: 60
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LogUserRequestIsFailed = new ExEventLog.EventTuple(3221225476U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x02000013 RID: 19
		private enum Category : short
		{
			// Token: 0x0400003E RID: 62
			General = 1
		}

		// Token: 0x02000014 RID: 20
		internal enum Message : uint
		{
			// Token: 0x04000040 RID: 64
			LogFailFastEnabledFlag = 1073741825U,
			// Token: 0x04000041 RID: 65
			LogUserAddedToFailFastUserCached = 3221225474U,
			// Token: 0x04000042 RID: 66
			LogIncorrectUserInfoReceivedOnServerStream,
			// Token: 0x04000043 RID: 67
			LogUserRequestIsFailed
		}
	}
}

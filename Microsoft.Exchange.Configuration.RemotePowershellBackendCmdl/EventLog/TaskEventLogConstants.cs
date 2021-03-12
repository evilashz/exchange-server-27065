using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Configuration.RemotePowershellBackendCmdletProxy.EventLog
{
	// Token: 0x02000004 RID: 4
	public static class TaskEventLogConstants
	{
		// Token: 0x04000007 RID: 7
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LogInvalidCommonAccessTokenReceived = new ExEventLog.EventTuple(3221225473U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x02000005 RID: 5
		private enum Category : short
		{
			// Token: 0x04000009 RID: 9
			General = 1
		}

		// Token: 0x02000006 RID: 6
		internal enum Message : uint
		{
			// Token: 0x0400000B RID: 11
			LogInvalidCommonAccessTokenReceived = 3221225473U
		}
	}
}

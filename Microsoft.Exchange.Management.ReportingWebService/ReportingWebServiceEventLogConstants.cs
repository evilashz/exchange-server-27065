using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.ReportingWebService
{
	// Token: 0x02000040 RID: 64
	public static class ReportingWebServiceEventLogConstants
	{
		// Token: 0x040000C7 RID: 199
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LoadReportingschemaFailed = new ExEventLog.EventTuple(3221225473U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000C8 RID: 200
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RequestFailed = new ExEventLog.EventTuple(3221225474U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000C9 RID: 201
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InvokeCmdletFailed = new ExEventLog.EventTuple(3221225475U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x02000041 RID: 65
		private enum Category : short
		{
			// Token: 0x040000CB RID: 203
			General = 1
		}

		// Token: 0x02000042 RID: 66
		internal enum Message : uint
		{
			// Token: 0x040000CD RID: 205
			LoadReportingschemaFailed = 3221225473U,
			// Token: 0x040000CE RID: 206
			RequestFailed,
			// Token: 0x040000CF RID: 207
			InvokeCmdletFailed
		}
	}
}

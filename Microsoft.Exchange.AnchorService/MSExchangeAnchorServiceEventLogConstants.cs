using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AnchorService
{
	// Token: 0x02000043 RID: 67
	public static class MSExchangeAnchorServiceEventLogConstants
	{
		// Token: 0x040000B0 RID: 176
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CriticalError = new ExEventLog.EventTuple(3221227473U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000B1 RID: 177
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_Information = new ExEventLog.EventTuple(1073743826U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000B2 RID: 178
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PermanentException = new ExEventLog.EventTuple(3221227475U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x02000044 RID: 68
		private enum Category : short
		{
			// Token: 0x040000B4 RID: 180
			General = 1
		}

		// Token: 0x02000045 RID: 69
		internal enum Message : uint
		{
			// Token: 0x040000B6 RID: 182
			CriticalError = 3221227473U,
			// Token: 0x040000B7 RID: 183
			Information = 1073743826U,
			// Token: 0x040000B8 RID: 184
			PermanentException = 3221227475U
		}
	}
}

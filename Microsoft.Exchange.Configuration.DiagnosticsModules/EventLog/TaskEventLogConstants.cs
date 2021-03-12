using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Configuration.DiagnosticsModules.EventLog
{
	// Token: 0x02000008 RID: 8
	public static class TaskEventLogConstants
	{
		// Token: 0x0400000F RID: 15
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorLogging_UnhandledException = new ExEventLog.EventTuple(3221225473U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000010 RID: 16
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ClientDiagnostics_RedirectWithDiagnosticsInformation = new ExEventLog.EventTuple(1073741826U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x02000009 RID: 9
		private enum Category : short
		{
			// Token: 0x04000012 RID: 18
			General = 1
		}

		// Token: 0x0200000A RID: 10
		internal enum Message : uint
		{
			// Token: 0x04000014 RID: 20
			ErrorLogging_UnhandledException = 3221225473U,
			// Token: 0x04000015 RID: 21
			ClientDiagnostics_RedirectWithDiagnosticsInformation = 1073741826U
		}
	}
}

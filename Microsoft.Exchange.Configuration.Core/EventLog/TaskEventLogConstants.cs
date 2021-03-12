using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Configuration.Core.EventLog
{
	// Token: 0x0200002A RID: 42
	public static class TaskEventLogConstants
	{
		// Token: 0x04000099 RID: 153
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnhandledException = new ExEventLog.EventTuple(3221225473U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400009A RID: 154
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NonCrashingException = new ExEventLog.EventTuple(3221225474U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400009B RID: 155
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LogConnectToNamedPipeServerTimeout = new ExEventLog.EventTuple(3221225477U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400009C RID: 156
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LogNamedPipeReceivedUnexpectedPackage = new ExEventLog.EventTuple(3221225478U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400009D RID: 157
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LogNamePipeStarted = new ExEventLog.EventTuple(1073741831U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400009E RID: 158
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LogNamePipeStopped = new ExEventLog.EventTuple(1073741832U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400009F RID: 159
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LogNamedPipeServerInInvalidState = new ExEventLog.EventTuple(3221225481U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000A0 RID: 160
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CrossAppDomainPrimaryObjectBehaviorException = new ExEventLog.EventTuple(3221225482U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000A1 RID: 161
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WinRMDataReceiverHandledExceptionFromCache = new ExEventLog.EventTuple(3221225483U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0200002B RID: 43
		private enum Category : short
		{
			// Token: 0x040000A3 RID: 163
			General = 1
		}

		// Token: 0x0200002C RID: 44
		internal enum Message : uint
		{
			// Token: 0x040000A5 RID: 165
			UnhandledException = 3221225473U,
			// Token: 0x040000A6 RID: 166
			NonCrashingException,
			// Token: 0x040000A7 RID: 167
			LogConnectToNamedPipeServerTimeout = 3221225477U,
			// Token: 0x040000A8 RID: 168
			LogNamedPipeReceivedUnexpectedPackage,
			// Token: 0x040000A9 RID: 169
			LogNamePipeStarted = 1073741831U,
			// Token: 0x040000AA RID: 170
			LogNamePipeStopped,
			// Token: 0x040000AB RID: 171
			LogNamedPipeServerInInvalidState = 3221225481U,
			// Token: 0x040000AC RID: 172
			CrossAppDomainPrimaryObjectBehaviorException,
			// Token: 0x040000AD RID: 173
			WinRMDataReceiverHandledExceptionFromCache
		}
	}
}

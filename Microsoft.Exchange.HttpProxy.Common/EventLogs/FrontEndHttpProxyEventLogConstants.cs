using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HttpProxy.EventLogs
{
	// Token: 0x0200001D RID: 29
	internal static class FrontEndHttpProxyEventLogConstants
	{
		// Token: 0x040000D8 RID: 216
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ApplicationStart = new ExEventLog.EventTuple(1073742825U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000D9 RID: 217
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ApplicationShutdown = new ExEventLog.EventTuple(1073742826U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000DA RID: 218
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InternalServerError = new ExEventLog.EventTuple(3221226475U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000DB RID: 219
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorLoadingSslCert = new ExEventLog.EventTuple(3221227473U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000DC RID: 220
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TooManyOutstandingRequests = new ExEventLog.EventTuple(2147485650U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000DD RID: 221
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RefreshingDownLevelServerMap = new ExEventLog.EventTuple(1073744825U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000DE RID: 222
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorRefreshDownLevelServerMap = new ExEventLog.EventTuple(3221228474U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000DF RID: 223
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RefreshingDownLevelServerStatus = new ExEventLog.EventTuple(1073744827U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000E0 RID: 224
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PingingDownLevelServer = new ExEventLog.EventTuple(1073744828U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000E1 RID: 225
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MarkingDownLevelServerUnhealthy = new ExEventLog.EventTuple(2147486653U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000E2 RID: 226
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RefreshingBackEndServerCache = new ExEventLog.EventTuple(1073744830U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000E3 RID: 227
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RefreshingDatabaseBackEndServer = new ExEventLog.EventTuple(1073744831U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000E4 RID: 228
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorRefreshingDatabaseBackEndServer = new ExEventLog.EventTuple(2147486656U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0200001E RID: 30
		private enum Category : short
		{
			// Token: 0x040000E6 RID: 230
			Core = 1
		}

		// Token: 0x0200001F RID: 31
		internal enum Message : uint
		{
			// Token: 0x040000E8 RID: 232
			ApplicationStart = 1073742825U,
			// Token: 0x040000E9 RID: 233
			ApplicationShutdown,
			// Token: 0x040000EA RID: 234
			InternalServerError = 3221226475U,
			// Token: 0x040000EB RID: 235
			ErrorLoadingSslCert = 3221227473U,
			// Token: 0x040000EC RID: 236
			TooManyOutstandingRequests = 2147485650U,
			// Token: 0x040000ED RID: 237
			RefreshingDownLevelServerMap = 1073744825U,
			// Token: 0x040000EE RID: 238
			ErrorRefreshDownLevelServerMap = 3221228474U,
			// Token: 0x040000EF RID: 239
			RefreshingDownLevelServerStatus = 1073744827U,
			// Token: 0x040000F0 RID: 240
			PingingDownLevelServer,
			// Token: 0x040000F1 RID: 241
			MarkingDownLevelServerUnhealthy = 2147486653U,
			// Token: 0x040000F2 RID: 242
			RefreshingBackEndServerCache = 1073744830U,
			// Token: 0x040000F3 RID: 243
			RefreshingDatabaseBackEndServer,
			// Token: 0x040000F4 RID: 244
			ErrorRefreshingDatabaseBackEndServer = 2147486656U
		}
	}
}

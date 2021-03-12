using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x0200003B RID: 59
	public static class MSExchangeTransportLogSearchEventLogConstants
	{
		// Token: 0x040000E9 RID: 233
		public const string EventSource = "MSExchangeTransportLogSearch";

		// Token: 0x040000EA RID: 234
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LogSearchServiceStartSuccess = new ExEventLog.EventTuple(269145U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000EB RID: 235
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LogSearchServiceStopSuccess = new ExEventLog.EventTuple(269146U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000EC RID: 236
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LogSearchServiceStopFailure = new ExEventLog.EventTuple(3221494620U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000ED RID: 237
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LogSearchReadConfigFailed = new ExEventLog.EventTuple(3221494621U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000EE RID: 238
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LogSearchUnAuthorizedFileAccess = new ExEventLog.EventTuple(2147752801U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000EF RID: 239
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_LogSearchIOException = new ExEventLog.EventTuple(2147752802U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x040000F0 RID: 240
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LogSearchNullOrEmptyLogPath = new ExEventLog.EventTuple(3221494627U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000F1 RID: 241
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LogSearchLogFileCorrupted = new ExEventLog.EventTuple(2147752804U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000F2 RID: 242
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LogSearchServiceStartFailureInit = new ExEventLog.EventTuple(3221494629U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000F3 RID: 243
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LogSearchServiceStartFailureSessionManager = new ExEventLog.EventTuple(3221494630U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000F4 RID: 244
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LogSearchServiceStartFailureMTGLog = new ExEventLog.EventTuple(3221494631U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000F5 RID: 245
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LogSearchServiceCreateDirectoryFailed = new ExEventLog.EventTuple(3221494632U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000F6 RID: 246
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LogSearchClientQuery = new ExEventLog.EventTuple(269164U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000F7 RID: 247
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LogSearchServiceIndexingComplete = new ExEventLog.EventTuple(269147U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000F8 RID: 248
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LogSearchServiceIndexFileCorrupt = new ExEventLog.EventTuple(3221494633U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000F9 RID: 249
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LogSearchServiceLogFileTooLarge = new ExEventLog.EventTuple(3221494634U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000FA RID: 250
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ActiveUserStatisticsLogPathIsNull = new ExEventLog.EventTuple(2147752811U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000FB RID: 251
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServerStatisticsLogPathIsNull = new ExEventLog.EventTuple(2147752812U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000FC RID: 252
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TransportSyncLogEntryReadingFailure = new ExEventLog.EventTuple(3221494637U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000FD RID: 253
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TransportSyncLogEntryProcessingFailure = new ExEventLog.EventTuple(3221494639U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000FE RID: 254
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RegistryAccessDenied = new ExEventLog.EventTuple(3221494648U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000FF RID: 255
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RegistryExchangeInstallPathNotFound = new ExEventLog.EventTuple(3221494649U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000100 RID: 256
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorReadingAppConfig = new ExEventLog.EventTuple(3221494650U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0200003C RID: 60
		private enum Category : short
		{
			// Token: 0x04000102 RID: 258
			General = 1,
			// Token: 0x04000103 RID: 259
			Client_Monitoring,
			// Token: 0x04000104 RID: 260
			Transport_Sync
		}

		// Token: 0x0200003D RID: 61
		internal enum Message : uint
		{
			// Token: 0x04000106 RID: 262
			LogSearchServiceStartSuccess = 269145U,
			// Token: 0x04000107 RID: 263
			LogSearchServiceStopSuccess,
			// Token: 0x04000108 RID: 264
			LogSearchServiceStopFailure = 3221494620U,
			// Token: 0x04000109 RID: 265
			LogSearchReadConfigFailed,
			// Token: 0x0400010A RID: 266
			LogSearchUnAuthorizedFileAccess = 2147752801U,
			// Token: 0x0400010B RID: 267
			LogSearchIOException,
			// Token: 0x0400010C RID: 268
			LogSearchNullOrEmptyLogPath = 3221494627U,
			// Token: 0x0400010D RID: 269
			LogSearchLogFileCorrupted = 2147752804U,
			// Token: 0x0400010E RID: 270
			LogSearchServiceStartFailureInit = 3221494629U,
			// Token: 0x0400010F RID: 271
			LogSearchServiceStartFailureSessionManager,
			// Token: 0x04000110 RID: 272
			LogSearchServiceStartFailureMTGLog,
			// Token: 0x04000111 RID: 273
			LogSearchServiceCreateDirectoryFailed,
			// Token: 0x04000112 RID: 274
			LogSearchClientQuery = 269164U,
			// Token: 0x04000113 RID: 275
			LogSearchServiceIndexingComplete = 269147U,
			// Token: 0x04000114 RID: 276
			LogSearchServiceIndexFileCorrupt = 3221494633U,
			// Token: 0x04000115 RID: 277
			LogSearchServiceLogFileTooLarge,
			// Token: 0x04000116 RID: 278
			ActiveUserStatisticsLogPathIsNull = 2147752811U,
			// Token: 0x04000117 RID: 279
			ServerStatisticsLogPathIsNull,
			// Token: 0x04000118 RID: 280
			TransportSyncLogEntryReadingFailure = 3221494637U,
			// Token: 0x04000119 RID: 281
			TransportSyncLogEntryProcessingFailure = 3221494639U,
			// Token: 0x0400011A RID: 282
			RegistryAccessDenied = 3221494648U,
			// Token: 0x0400011B RID: 283
			RegistryExchangeInstallPathNotFound,
			// Token: 0x0400011C RID: 284
			ErrorReadingAppConfig
		}
	}
}

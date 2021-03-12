using System;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.FullTextIndex
{
	// Token: 0x0200000D RID: 13
	public class FullTextIndexLogger
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00004DF7 File Offset: 0x00002FF7
		public static bool IsLoggingEnabled
		{
			get
			{
				return FullTextIndexLogger.binaryLogger != null && FullTextIndexLogger.binaryLogger.IsLoggingEnabled;
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004E0C File Offset: 0x0000300C
		public static void Initialize()
		{
			FullTextIndexLogger.binaryLogger = LoggerManager.GetLogger(LoggerType.FullTextIndex);
			FullTextIndexLogger.minStringLogBufferSize = 6 + Guid.Empty.ToString().Length + 4 + 4 + 4 + 3;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004E4C File Offset: 0x0000304C
		public static void LogViewOperation(Guid databaseGuid, int mailboxNumber, int clientType, Guid correlationId, FullTextIndexLogger.ViewOperationType viewOperation, TimeSpan operationTime, int lazyIndexSeek, int lazyIndexRead, int messageSeek, int messageRead, bool isOptimizedInstantSearch)
		{
			if (!FullTextIndexLogger.IsLoggingEnabled)
			{
				return;
			}
			long num = (long)operationTime.TotalMilliseconds;
			string logString = string.Format("{0}:{1}:{2}:{3}:{4}:{5}:{6}:{7}", new object[]
			{
				DateTime.UtcNow.ToString("MM/dd/yyyy HH-mm-ss.fff"),
				(int)viewOperation,
				num,
				lazyIndexSeek,
				lazyIndexRead,
				messageSeek,
				messageRead,
				isOptimizedInstantSearch ? 1 : 0
			});
			FullTextIndexLogger.Log(FullTextIndexLogger.LogOperationType.ViewOperation, databaseGuid, mailboxNumber, clientType, correlationId, logString);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004EE8 File Offset: 0x000030E8
		public static void LogOtherSuboperation(Guid databaseGuid, int mailboxNumber, int clientType, Guid correlationId, int suboperationType)
		{
			if (!FullTextIndexLogger.IsLoggingEnabled)
			{
				return;
			}
			FullTextIndexLogger.Log(FullTextIndexLogger.LogOperationType.OtherOperation, databaseGuid, mailboxNumber, clientType, correlationId, suboperationType.ToString());
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004F04 File Offset: 0x00003104
		public static void LogViewSetSearchCriteria(Guid databaseGuid, int mailboxNumber, int clientType, Guid correlationId)
		{
			if (!FullTextIndexLogger.IsLoggingEnabled)
			{
				return;
			}
			FullTextIndexLogger.Log(FullTextIndexLogger.LogOperationType.ViewSetSearchCriteria, databaseGuid, mailboxNumber, clientType, correlationId, DateTime.UtcNow.ToString("MM/dd/yyyy HH-mm-ss.fff"));
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004F38 File Offset: 0x00003138
		public static void LogSingleLineQuery(Guid correlationId, DateTime queryStartTime, DateTime queryEndTime, Guid databaseGuid, Guid mailboxGuid, int mailboxNumber, int clientType, string queryString, bool failed, string errorMessage, bool maxLinkCountReached, string storeResidual, int numberFastTrips, bool isPulsing, string firstScopeFolder, int maxCount, int scopeFolderCount, int initialSearchState, int finalSearchState, int searchCriteriaFlags, bool isNestedSearchFolder, int first1000FastResults, int firstNotificationFastResults, int fastResults, int totalResults, TimeSpan searchRestrictionTime, TimeSpan searchPlanTime, int first1000FastTime, int firstResultsFastTime, string fastTimes, int firstResultsTime, int fastTime, int expandedScopeFolderCount, string friendlyFolderName, uint totalRowsProcessed, string clientActionString, string scrubbedQuery, string replacements)
		{
			if (!FullTextIndexLogger.IsLoggingEnabled)
			{
				return;
			}
			using (TraceBuffer traceBuffer = TraceRecord.Create(LoggerManager.TraceGuids.FullTextIndexSingleLine, true, true, 6, correlationId.ToString(), databaseGuid.ToString(), mailboxGuid.ToString(), queryStartTime.ToString("o"), queryEndTime.ToString("o"), mailboxNumber, clientType, queryString, failed ? 1 : 0, errorMessage, maxLinkCountReached ? 1 : 0, storeResidual, numberFastTrips, isPulsing ? 1 : 0, firstScopeFolder, maxCount, scopeFolderCount, initialSearchState, finalSearchState, searchCriteriaFlags, isNestedSearchFolder ? 1 : 0, first1000FastResults, firstNotificationFastResults, fastResults, totalResults, (int)searchRestrictionTime.TotalMilliseconds, (int)searchPlanTime.TotalMilliseconds, first1000FastTime, firstResultsFastTime, fastTimes, firstResultsTime, fastTime, expandedScopeFolderCount, friendlyFolderName, totalRowsProcessed, clientActionString, scrubbedQuery, replacements))
			{
				FullTextIndexLogger.binaryLogger.TryWrite(traceBuffer);
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00005028 File Offset: 0x00003228
		private static void Log(FullTextIndexLogger.LogOperationType operationType, Guid databaseGuid, int mailboxNumber, int clientType, Guid correlationId, string logString)
		{
			string strValue = logString;
			if (logString.Length > 524288)
			{
				StringBuilder stringBuilder = new StringBuilder(logString);
				stringBuilder.Length = 524288 - "</Truncated>".Length;
				stringBuilder.Append("</Truncated>");
				strValue = stringBuilder.ToString();
			}
			using (TraceBuffer traceBuffer = TraceRecord.Create(LoggerManager.TraceGuids.FullTextIndexQuery, true, false, 6, correlationId.ToString(), databaseGuid.ToString(), mailboxNumber, (int)operationType, clientType, strValue))
			{
				FullTextIndexLogger.binaryLogger.TryWrite(traceBuffer);
			}
		}

		// Token: 0x0400003F RID: 63
		private const int MaxLogStringLength = 524288;

		// Token: 0x04000040 RID: 64
		private const short TraceVersion = 6;

		// Token: 0x04000041 RID: 65
		private const string TruncatedLogSuffix = "</Truncated>";

		// Token: 0x04000042 RID: 66
		private const string DateTimeFormat = "MM/dd/yyyy HH-mm-ss.fff";

		// Token: 0x04000043 RID: 67
		private static IBinaryLogger binaryLogger;

		// Token: 0x04000044 RID: 68
		private static int minStringLogBufferSize;

		// Token: 0x0200000E RID: 14
		public enum LogOperationType
		{
			// Token: 0x04000046 RID: 70
			SearchFolderPopulationStart = 1,
			// Token: 0x04000047 RID: 71
			RequestSentToFAST,
			// Token: 0x04000048 RID: 72
			ResponseReceivedFromFAST,
			// Token: 0x04000049 RID: 73
			ReceviedErrorFromFAST,
			// Token: 0x0400004A RID: 74
			FirstResultsLinked,
			// Token: 0x0400004B RID: 75
			RequestCompleted,
			// Token: 0x0400004C RID: 76
			ViewOperation,
			// Token: 0x0400004D RID: 77
			ViewSetSearchCriteria,
			// Token: 0x0400004E RID: 78
			OtherOperation
		}

		// Token: 0x0200000F RID: 15
		public enum ViewOperationType
		{
			// Token: 0x04000050 RID: 80
			GetContentsTable = 1,
			// Token: 0x04000051 RID: 81
			GetContentsTableEx,
			// Token: 0x04000052 RID: 82
			QueryRows,
			// Token: 0x04000053 RID: 83
			QueryPosition,
			// Token: 0x04000054 RID: 84
			FindRow,
			// Token: 0x04000055 RID: 85
			SeekRow
		}
	}
}

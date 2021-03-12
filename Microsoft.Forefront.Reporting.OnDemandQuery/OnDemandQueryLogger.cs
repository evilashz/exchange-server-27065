using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Hygiene.Data.Directory;

namespace Microsoft.Forefront.Reporting.OnDemandQuery
{
	// Token: 0x02000005 RID: 5
	internal class OnDemandQueryLogger : DisposeTrackableBase
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002850 File Offset: 0x00000A50
		static OnDemandQueryLogger()
		{
			string[] names = Enum.GetNames(typeof(OnDemandQueryLogFields));
			OnDemandQueryLogger.OnDemandQueryLogSchema = new LogSchema("Microsoft.Forefront.Reporting.Common", "15.00.1497.010", "OnDemandQueryLogs", names);
			string path = "D:\\OnDemandQueryLogs";
			TimeSpan maxAge = TimeSpan.FromDays(5.0);
			long maxDirectorySize = 50000000L;
			long maxLogFileSize = 5000000L;
			OnDemandQueryLogger.onDemandQueryLog = new Log("OnDemandQueryLogs_", new LogHeaderFormatter(OnDemandQueryLogger.OnDemandQueryLogSchema), "OnDemandQueryLogs");
			OnDemandQueryLogger.onDemandQueryLog.Configure(path, maxAge, maxDirectorySize, maxLogFileSize);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000028D8 File Offset: 0x00000AD8
		public static void Log(OnDemandQueryRequest queryRequest, OnDemandQueryLogEvent eventType, string exceptionStr = null)
		{
			LogRowFormatter logRowFormatter = new LogRowFormatter(OnDemandQueryLogger.OnDemandQueryLogSchema);
			logRowFormatter[0] = DateTime.UtcNow;
			logRowFormatter[1] = eventType.ToString();
			logRowFormatter[2] = queryRequest.RequestId;
			logRowFormatter[3] = queryRequest.TenantId;
			logRowFormatter[4] = queryRequest.Region;
			logRowFormatter[5] = queryRequest.SubmissionTime;
			logRowFormatter[6] = queryRequest.QueryType;
			logRowFormatter[7] = queryRequest.QueryPriority;
			logRowFormatter[8] = queryRequest.CallerType;
			logRowFormatter[9] = queryRequest.QueryDefinition;
			logRowFormatter[10] = queryRequest.BatchId;
			logRowFormatter[11] = queryRequest.InBatchQueryId;
			logRowFormatter[12] = queryRequest.CosmosJobId;
			logRowFormatter[13] = queryRequest.MatchRowCounts;
			logRowFormatter[14] = queryRequest.ResultRowCounts;
			logRowFormatter[15] = queryRequest.ResultSize;
			logRowFormatter[16] = queryRequest.ViewCounts;
			logRowFormatter[17] = queryRequest.RetryCount;
			logRowFormatter[18] = queryRequest.ResultLocale;
			logRowFormatter[19] = exceptionStr;
			OnDemandQueryLogger.onDemandQueryLog.Append(logRowFormatter, 0);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002A58 File Offset: 0x00000C58
		public static void Log(IEnumerable<OnDemandQueryRequest> requests, OnDemandQueryLogEvent eventType, string exceptionStr = null)
		{
			foreach (OnDemandQueryRequest queryRequest in requests)
			{
				OnDemandQueryLogger.Log(queryRequest, eventType, exceptionStr);
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002AA4 File Offset: 0x00000CA4
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<OnDemandQueryLogger>(this);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002AAC File Offset: 0x00000CAC
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && OnDemandQueryLogger.onDemandQueryLog != null)
			{
				OnDemandQueryLogger.onDemandQueryLog.Close();
			}
		}

		// Token: 0x04000031 RID: 49
		private const string LogType = "OnDemandQueryLogs";

		// Token: 0x04000032 RID: 50
		private const string LogComponentName = "Microsoft.Forefront.Reporting.Common";

		// Token: 0x04000033 RID: 51
		private static readonly LogSchema OnDemandQueryLogSchema;

		// Token: 0x04000034 RID: 52
		private static readonly Log onDemandQueryLog;
	}
}

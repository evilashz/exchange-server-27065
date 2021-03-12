using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000C2 RID: 194
	internal sealed class RequestLogger
	{
		// Token: 0x060004E0 RID: 1248 RVA: 0x00015E18 File Offset: 0x00014018
		public RequestLogger()
		{
			this.items = new Dictionary<RequestStatisticsType, RequestStatistics>();
			this.capturedTime = DateTime.UtcNow;
			this.capturedTimeList = new List<RequestLogger.TagTimePair>();
			this.logData = new StringBuilder(1024);
			this.exceptionStatistics = new Dictionary<string, int>();
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x00015E67 File Offset: 0x00014067
		public StringBuilder LogData
		{
			get
			{
				return this.logData;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x00015E6F File Offset: 0x0001406F
		public List<string> ErrorData
		{
			get
			{
				return this.errorData;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x00015E77 File Offset: 0x00014077
		public Dictionary<string, int> ExceptionData
		{
			get
			{
				return this.exceptionStatistics;
			}
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00015E80 File Offset: 0x00014080
		public void Add(RequestStatistics requestStatistics)
		{
			lock (this.items)
			{
				RequestStatistics requestStatistics2;
				if (this.items.TryGetValue(requestStatistics.Tag, out requestStatistics2))
				{
					if (requestStatistics.TimeTaken > requestStatistics2.TimeTaken)
					{
						this.items[requestStatistics.Tag] = requestStatistics;
					}
				}
				else
				{
					this.items[requestStatistics.Tag] = requestStatistics;
				}
			}
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00015F04 File Offset: 0x00014104
		public void CaptureRequestStage(string tag)
		{
			DateTime utcNow = DateTime.UtcNow;
			this.capturedTimeList.Add(new RequestLogger.TagTimePair
			{
				Tag = tag,
				TimeTaken = (long)(utcNow - this.capturedTime).TotalMilliseconds
			});
			this.capturedTime = utcNow;
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00015F54 File Offset: 0x00014154
		public void Log()
		{
			this.CaptureRequestStage("PostQuery");
			lock (this.items)
			{
				foreach (RequestStatistics requestStatistics in this.items.Values)
				{
					requestStatistics.Log(this);
				}
			}
			foreach (RequestLogger.TagTimePair tagTimePair in this.capturedTimeList)
			{
				this.AppendToLog<long>(tagTimePair.Tag, tagTimePair.TimeTaken);
			}
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00016030 File Offset: 0x00014230
		public void AppendToLog<T>(string key, T value)
		{
			this.logData.AppendFormat("{0}={1};", key, value);
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0001604A File Offset: 0x0001424A
		public void StartLog()
		{
			this.logData.Append("[AS-Start-");
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0001605D File Offset: 0x0001425D
		public void EndLog()
		{
			this.logData.Append("-AS-END]");
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00016070 File Offset: 0x00014270
		public void LogToResponse(HttpResponse httpResponse)
		{
			if (httpResponse != null)
			{
				int num = 0;
				int i = 80;
				int length = this.logData.Length;
				while (i < length)
				{
					httpResponse.AppendToLog(this.logData.ToString(num, 80));
					num = i;
					i += 80;
				}
				if (num < length)
				{
					httpResponse.AppendToLog(this.logData.ToString(num, length - num));
				}
			}
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x000160CC File Offset: 0x000142CC
		public void CalculateQueryStatistics(FreeBusyQuery[] queries)
		{
			if (queries == null)
			{
				return;
			}
			QueryLogData queryLogData = new QueryLogData();
			for (int i = 0; i < queries.Length; i++)
			{
				FreeBusyQueryResult result = queries[i].Result;
				if (result != null)
				{
					if (result.ExceptionInfo != null)
					{
						this.UpdateExceptionStatictics(result.ExceptionInfo);
						queryLogData.Add(queries[i], true);
					}
					else
					{
						queryLogData.Add(queries[i], false);
					}
				}
				else
				{
					queryLogData.Add(queries[i], false);
				}
			}
			this.logData.Append(queryLogData.GetSucceededQueryLogData());
			this.errorData = queryLogData.GetFailedQueryLogData();
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00016154 File Offset: 0x00014354
		private void UpdateExceptionStatictics(Exception ex)
		{
			while (ex.InnerException != null)
			{
				ex = ex.InnerException;
			}
			string name = ex.GetType().Name;
			if (!this.exceptionStatistics.ContainsKey(name))
			{
				this.exceptionStatistics.Add(name, 1);
				return;
			}
			Dictionary<string, int> dictionary;
			string key;
			(dictionary = this.exceptionStatistics)[key = name] = dictionary[key] + 1;
		}

		// Token: 0x040002D0 RID: 720
		private const string START = "[AS-Start-";

		// Token: 0x040002D1 RID: 721
		private const string END = "-AS-END]";

		// Token: 0x040002D2 RID: 722
		private const int DefaultCapacity = 1024;

		// Token: 0x040002D3 RID: 723
		private const int MaxLogAppendStringLength = 80;

		// Token: 0x040002D4 RID: 724
		private Dictionary<RequestStatisticsType, RequestStatistics> items;

		// Token: 0x040002D5 RID: 725
		private DateTime capturedTime;

		// Token: 0x040002D6 RID: 726
		private List<RequestLogger.TagTimePair> capturedTimeList;

		// Token: 0x040002D7 RID: 727
		private StringBuilder logData;

		// Token: 0x040002D8 RID: 728
		private List<string> errorData;

		// Token: 0x040002D9 RID: 729
		private Dictionary<string, int> exceptionStatistics;

		// Token: 0x020000C3 RID: 195
		private class TagTimePair
		{
			// Token: 0x040002DA RID: 730
			public string Tag;

			// Token: 0x040002DB RID: 731
			public long TimeTaken;
		}
	}
}

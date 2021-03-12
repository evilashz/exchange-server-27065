using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.RequestDispatch;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000B9 RID: 185
	internal abstract class Query<ReturnType>
	{
		// Token: 0x0600042B RID: 1067 RVA: 0x00011A62 File Offset: 0x0000FC62
		public Query(ClientContext clientContext, HttpResponse httpResponse, CasTraceEventType casTraceEventType, ThreadCounter threadCounter, ExPerformanceCounter currentRequestsCounter) : this(casTraceEventType, threadCounter, currentRequestsCounter)
		{
			this.ClientContext = clientContext;
			this.HttpResponse = httpResponse;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00011A80 File Offset: 0x0000FC80
		public Query(CasTraceEventType casTraceEventType, ThreadCounter threadCounter, ExPerformanceCounter currentRequestsCounter)
		{
			this.casTraceEventType = casTraceEventType;
			this.threadCounter = threadCounter;
			this.currentRequestsCounter = currentRequestsCounter;
			this.Timeout = Configuration.WebRequestTimeoutInSeconds;
			this.requestProcessingDeadline = DateTime.UtcNow + this.Timeout;
			this.queryPrepareDeadline = this.requestProcessingDeadline;
			this.RequestLogger = new RequestLogger();
			ConfigurationReader.Start(this.RequestLogger);
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x00011AEB File Offset: 0x0000FCEB
		// (set) Token: 0x0600042E RID: 1070 RVA: 0x00011AF3 File Offset: 0x0000FCF3
		public ClientContext ClientContext { get; set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x00011AFC File Offset: 0x0000FCFC
		// (set) Token: 0x06000430 RID: 1072 RVA: 0x00011B04 File Offset: 0x0000FD04
		public HttpResponse HttpResponse { get; set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x00011B0D File Offset: 0x0000FD0D
		// (set) Token: 0x06000432 RID: 1074 RVA: 0x00011B15 File Offset: 0x0000FD15
		public RequestLogger RequestLogger { get; protected set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x00011B1E File Offset: 0x0000FD1E
		// (set) Token: 0x06000434 RID: 1076 RVA: 0x00011B26 File Offset: 0x0000FD26
		public string ServerName { get; set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x00011B2F File Offset: 0x0000FD2F
		// (set) Token: 0x06000436 RID: 1078 RVA: 0x00011B37 File Offset: 0x0000FD37
		public TimeSpan Timeout { get; set; }

		// Token: 0x06000437 RID: 1079 RVA: 0x00011B40 File Offset: 0x0000FD40
		public ReturnType Execute()
		{
			this.ValidateInputData();
			return ThreadContext.Set<ReturnType>(string.Format(CultureInfo.InvariantCulture, "{0}.Execute", new object[]
			{
				base.GetType().Name
			}), this.threadCounter, this.ClientContext, this.RequestLogger, new ThreadContext.ExecuteDelegate<ReturnType>(this.ExecuteWithPerformanceMeasurement));
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00011B9B File Offset: 0x0000FD9B
		internal static string GetCurrentHttpRequestServerName()
		{
			if (HttpContext.Current != null && HttpContext.Current.Server != null)
			{
				return HttpContext.Current.Server.MachineName;
			}
			return string.Empty;
		}

		// Token: 0x06000439 RID: 1081
		protected abstract void ValidateSpecificInputData();

		// Token: 0x0600043A RID: 1082
		protected abstract ReturnType ExecuteInternal();

		// Token: 0x0600043B RID: 1083
		protected abstract void UpdateCountersAtExecuteEnd(Stopwatch responseTimer);

		// Token: 0x0600043C RID: 1084
		protected abstract void AppendSpecificSpExecuteOperationData(StringBuilder spOperationData);

		// Token: 0x0600043D RID: 1085 RVA: 0x00011BC5 File Offset: 0x0000FDC5
		protected virtual void LogExpensiveRequests(RequestStatistics threadStatistics, RequestStatistics mapiStatistics, RequestStatistics adStatistics)
		{
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00011BC8 File Offset: 0x0000FDC8
		protected int LogFailures(ReturnType result, IDictionary<string, int> exceptionStatistics)
		{
			int num = 0;
			if (result != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("<");
				foreach (string text in exceptionStatistics.Keys)
				{
					num += exceptionStatistics[text];
					stringBuilder.AppendFormat("{0}={1}|", text, exceptionStatistics[text]);
				}
				stringBuilder.Append(">");
				this.RequestLogger.AppendToLog<int>("Failures", num);
				this.RequestLogger.AppendToLog<string>("EXP", stringBuilder.ToString());
			}
			else
			{
				this.RequestLogger.AppendToLog<string>("RequestFailed", "true");
			}
			return num;
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00011C9C File Offset: 0x0000FE9C
		protected StringBuilder GetSpExecuteOperationData()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Mailboxes Processed: {0}", this.individualMailboxesProcessed);
			this.AppendSpecificSpExecuteOperationData(stringBuilder);
			return stringBuilder;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00011CD0 File Offset: 0x0000FED0
		private ReturnType ExecuteWithPerformanceMeasurement()
		{
			Query<ReturnType>.Tracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: {1}.Execute: enter", TraceContext.Get(), base.GetType().Name);
			RequestStatisticsForThread requestStatisticsForThread = RequestStatisticsForThread.Begin();
			RequestStatisticsForMapi requestStatisticsForMapi = RequestStatisticsForMapi.Begin();
			RequestStatisticsForAD requestStatisticsForAD = RequestStatisticsForAD.Begin();
			Stopwatch stopwatch = Stopwatch.StartNew();
			Guid serverRequestId = Microsoft.Exchange.Diagnostics.Trace.TraceCasStart(this.casTraceEventType);
			this.currentRequestsCounter.Increment();
			ReturnType result;
			try
			{
				result = this.ExecuteInternal();
			}
			finally
			{
				stopwatch.Stop();
				this.TraceExecuteInternalStop(serverRequestId);
				this.UpdateCountersAtExecuteEnd(stopwatch);
				this.currentRequestsCounter.Decrement();
				Query<ReturnType>.Tracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: {1}.Execute: exit", TraceContext.Get(), base.GetType().Name);
				RequestStatistics requestStatistics = requestStatisticsForMapi.End(RequestStatisticsType.MailboxRPC);
				requestStatistics.Log(this.RequestLogger);
				RequestStatistics requestStatistics2 = requestStatisticsForAD.End(RequestStatisticsType.AD);
				requestStatistics2.Log(this.RequestLogger);
				RequestStatistics requestStatistics3 = requestStatisticsForThread.End(RequestStatisticsType.RequestCPUMain);
				if (requestStatistics3 != null)
				{
					requestStatistics3.Log(this.RequestLogger);
				}
				this.RequestLogger.Log();
				this.LogExpensiveRequests(requestStatistics3, requestStatistics, requestStatistics2);
			}
			return result;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00011E04 File Offset: 0x00010004
		private void ValidateInputData()
		{
			if (this.ClientContext == null)
			{
				throw new MissingArgumentException(Strings.descMissingArgument("ClientContext"));
			}
			this.ClientContext.ValidateContext();
			this.ValidateSpecificInputData();
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00011E30 File Offset: 0x00010030
		private void TraceExecuteInternalStop(Guid serverRequestId)
		{
			if (ETWTrace.ShouldTraceCasStop(serverRequestId))
			{
				StringBuilder spExecuteOperationData = this.GetSpExecuteOperationData();
				if (string.IsNullOrEmpty(this.ServerName))
				{
					this.ServerName = Query<ReturnType>.GetCurrentHttpRequestServerName();
				}
				Microsoft.Exchange.Diagnostics.Trace.TraceCasStop(this.casTraceEventType, serverRequestId, 0, 0, this.ServerName, TraceContext.Get(), string.Format(CultureInfo.InvariantCulture, "{0}::ExecuteInternal", new object[]
				{
					base.GetType().Name
				}), spExecuteOperationData, string.Empty);
			}
		}

		// Token: 0x04000297 RID: 663
		private static readonly Microsoft.Exchange.Diagnostics.Trace Tracer = ExTraceGlobals.RequestRoutingTracer;

		// Token: 0x04000298 RID: 664
		protected DateTime requestProcessingDeadline;

		// Token: 0x04000299 RID: 665
		protected DateTime queryPrepareDeadline;

		// Token: 0x0400029A RID: 666
		protected CasTraceEventType casTraceEventType;

		// Token: 0x0400029B RID: 667
		protected int individualMailboxesProcessed;

		// Token: 0x0400029C RID: 668
		private ThreadCounter threadCounter;

		// Token: 0x0400029D RID: 669
		private ExPerformanceCounter currentRequestsCounter;
	}
}

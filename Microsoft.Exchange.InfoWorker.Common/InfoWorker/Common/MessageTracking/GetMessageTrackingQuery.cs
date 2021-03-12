using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.SoapWebClient.AutoDiscover;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002AB RID: 683
	internal sealed class GetMessageTrackingQuery : Query<GetMessageTrackingQueryResult>
	{
		// Token: 0x06001310 RID: 4880 RVA: 0x00057224 File Offset: 0x00055424
		public GetMessageTrackingQuery(SmtpAddress proxyRecipient, DirectoryContext directoryContext, GetMessageTrackingReportRequestTypeWrapper request, ExchangeVersion minVersionRequested, TimeSpan timeout) : base(directoryContext.ClientContext, null, CasTraceEventType.MessageTracking, GetMessageTrackingApplication.MessageTrackingIOCompletion, InfoWorkerMessageTrackingPerformanceCounters.CurrentRequestDispatcherRequests)
		{
			MessageTrackingReportId messageTrackingReportId = null;
			if (!MessageTrackingReportId.TryParse(request.WrappedRequest.MessageTrackingReportId, out messageTrackingReportId))
			{
				throw new ArgumentException("MessageTrackingReportId invalid");
			}
			this.directoryContext = directoryContext;
			string address = ServerCache.Instance.GetOrgMailboxForDomain(messageTrackingReportId.Domain).ToString();
			this.fakeRecipientQueryResults = MessageTrackingApplication.CreateFakeRecipientQueryResult(address);
			this.proxyRecipient = proxyRecipient;
			this.request = request;
			this.minVersionRequested = minVersionRequested;
			base.Timeout = timeout;
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x000572BB File Offset: 0x000554BB
		protected override void ValidateSpecificInputData()
		{
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x000572C0 File Offset: 0x000554C0
		protected override GetMessageTrackingQueryResult ExecuteInternal()
		{
			base.RequestLogger.AppendToLog<string>("MessageTrackingRequest", "Start");
			GetMessageTrackingApplication getMessageTrackingApplication = new GetMessageTrackingApplication(this.request, this.minVersionRequested);
			GetMessageTrackingQueryResult result;
			using (RequestDispatcher requestDispatcher = new RequestDispatcher(base.RequestLogger))
			{
				try
				{
					IList<RecipientData> recipientQueryResults;
					if (this.proxyRecipient != SmtpAddress.Empty)
					{
						recipientQueryResults = MessageTrackingApplication.CreateRecipientQueryResult(this.directoryContext, this.queryPrepareDeadline, this.proxyRecipient.ToString());
					}
					else
					{
						recipientQueryResults = this.fakeRecipientQueryResults;
					}
					QueryGenerator queryGenerator = new QueryGenerator(getMessageTrackingApplication, base.ClientContext, base.RequestLogger, requestDispatcher, this.queryPrepareDeadline, this.requestProcessingDeadline, recipientQueryResults);
					BaseQuery[] queries = queryGenerator.GetQueries();
					requestDispatcher.Execute(this.requestProcessingDeadline, base.HttpResponse);
					GetMessageTrackingBaseQuery getMessageTrackingBaseQuery = (GetMessageTrackingBaseQuery)queries[0];
					if (getMessageTrackingBaseQuery.Result == null)
					{
						result = null;
					}
					else
					{
						if (getMessageTrackingBaseQuery.Result.ExceptionInfo != null)
						{
							throw getMessageTrackingBaseQuery.Result.ExceptionInfo;
						}
						GetMessageTrackingQueryResult getMessageTrackingQueryResult = new GetMessageTrackingQueryResult();
						getMessageTrackingQueryResult.Response = getMessageTrackingBaseQuery.Result.Response;
						base.RequestLogger.AppendToLog<string>("MessageTrackingRequest", "Exit");
						result = getMessageTrackingQueryResult;
					}
				}
				finally
				{
					requestDispatcher.LogStatistics(base.RequestLogger);
					getMessageTrackingApplication.LogThreadsUsage(base.RequestLogger);
				}
			}
			return result;
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x00057424 File Offset: 0x00055624
		protected override void UpdateCountersAtExecuteEnd(Stopwatch responseTimer)
		{
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x00057426 File Offset: 0x00055626
		protected override void AppendSpecificSpExecuteOperationData(StringBuilder spOperationData)
		{
			spOperationData.AppendFormat("Recipient Processed: {0}", this.proxyRecipient.ToString());
		}

		// Token: 0x04000CC7 RID: 3271
		private DirectoryContext directoryContext;

		// Token: 0x04000CC8 RID: 3272
		private SmtpAddress proxyRecipient;

		// Token: 0x04000CC9 RID: 3273
		private IList<RecipientData> fakeRecipientQueryResults;

		// Token: 0x04000CCA RID: 3274
		private GetMessageTrackingReportRequestTypeWrapper request;

		// Token: 0x04000CCB RID: 3275
		private ExchangeVersion minVersionRequested;
	}
}

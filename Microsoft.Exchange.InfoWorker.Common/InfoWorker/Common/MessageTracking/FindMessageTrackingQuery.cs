using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.SoapWebClient.AutoDiscover;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002A6 RID: 678
	internal sealed class FindMessageTrackingQuery : Query<FindMessageTrackingQueryResult>
	{
		// Token: 0x060012F0 RID: 4848 RVA: 0x00056CF8 File Offset: 0x00054EF8
		public FindMessageTrackingQuery(SmtpAddress proxyRecipient, string domain, DirectoryContext directoryContext, FindMessageTrackingReportRequestTypeWrapper request, ExchangeVersion minVersionRequested, TimeSpan timeout) : base(directoryContext.ClientContext, null, CasTraceEventType.MessageTracking, FindMessageTrackingApplication.MessageTrackingIOCompletion, InfoWorkerMessageTrackingPerformanceCounters.CurrentRequestDispatcherRequests)
		{
			if (SmtpAddress.Empty.Equals(proxyRecipient))
			{
				string address = ServerCache.Instance.GetOrgMailboxForDomain(domain).ToString();
				this.fakeRecipientQueryResults = MessageTrackingApplication.CreateFakeRecipientQueryResult(address);
			}
			else
			{
				this.proxyRecipient = proxyRecipient;
			}
			this.directoryContext = directoryContext;
			this.request = request;
			this.minVersionRequested = minVersionRequested;
			base.Timeout = timeout;
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x00056D7C File Offset: 0x00054F7C
		protected override void ValidateSpecificInputData()
		{
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x00056D80 File Offset: 0x00054F80
		protected override FindMessageTrackingQueryResult ExecuteInternal()
		{
			base.RequestLogger.AppendToLog<string>("MessageTrackingRequest", "Start");
			FindMessageTrackingApplication findMessageTrackingApplication = new FindMessageTrackingApplication(this.request, this.minVersionRequested);
			FindMessageTrackingQueryResult result;
			using (RequestDispatcher requestDispatcher = new RequestDispatcher(base.RequestLogger))
			{
				IList<RecipientData> recipientQueryResults;
				if (this.fakeRecipientQueryResults != null)
				{
					recipientQueryResults = this.fakeRecipientQueryResults;
				}
				else
				{
					recipientQueryResults = MessageTrackingApplication.CreateRecipientQueryResult(this.directoryContext, this.queryPrepareDeadline, this.proxyRecipient.ToString());
				}
				QueryGenerator queryGenerator = new QueryGenerator(findMessageTrackingApplication, base.ClientContext, base.RequestLogger, requestDispatcher, this.queryPrepareDeadline, this.requestProcessingDeadline, recipientQueryResults);
				try
				{
					BaseQuery[] queries = queryGenerator.GetQueries();
					requestDispatcher.Execute(this.requestProcessingDeadline, base.HttpResponse);
					FindMessageTrackingBaseQuery findMessageTrackingBaseQuery = (FindMessageTrackingBaseQuery)queries[0];
					if (findMessageTrackingBaseQuery.Result == null)
					{
						result = null;
					}
					else
					{
						if (findMessageTrackingBaseQuery.Result.ExceptionInfo != null)
						{
							throw findMessageTrackingBaseQuery.Result.ExceptionInfo;
						}
						FindMessageTrackingQueryResult findMessageTrackingQueryResult = new FindMessageTrackingQueryResult();
						findMessageTrackingQueryResult.Response = findMessageTrackingBaseQuery.Result.Response;
						base.RequestLogger.AppendToLog<string>("MessageTrackingRequest", "Exit");
						result = findMessageTrackingQueryResult;
					}
				}
				finally
				{
					requestDispatcher.LogStatistics(base.RequestLogger);
					findMessageTrackingApplication.LogThreadsUsage(base.RequestLogger);
				}
			}
			return result;
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x00056EDC File Offset: 0x000550DC
		protected override void UpdateCountersAtExecuteEnd(Stopwatch responseTimer)
		{
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x00056EDE File Offset: 0x000550DE
		protected override void AppendSpecificSpExecuteOperationData(StringBuilder spOperationData)
		{
			spOperationData.AppendFormat("Recipient Processed: {0}", this.proxyRecipient.ToString());
		}

		// Token: 0x04000CBA RID: 3258
		internal static readonly PropertyDefinition[] RecipientProperties = new PropertyDefinition[]
		{
			ADObjectSchema.OrganizationId,
			ADRecipientSchema.RecipientType,
			ADRecipientSchema.RecipientDisplayType,
			ADRecipientSchema.ExternalEmailAddress,
			ADRecipientSchema.PrimarySmtpAddress,
			ADRecipientSchema.DisplayName,
			ADRecipientSchema.LegacyExchangeDN,
			ADMailboxRecipientSchema.Database,
			ADMailboxRecipientSchema.ServerLegacyDN,
			ADMailboxRecipientSchema.ExchangeGuid,
			ADMailboxRecipientSchema.Database,
			ADObjectSchema.ExchangeVersion,
			ADObjectSchema.Id
		};

		// Token: 0x04000CBB RID: 3259
		private DirectoryContext directoryContext;

		// Token: 0x04000CBC RID: 3260
		private SmtpAddress proxyRecipient;

		// Token: 0x04000CBD RID: 3261
		private IList<RecipientData> fakeRecipientQueryResults;

		// Token: 0x04000CBE RID: 3262
		private FindMessageTrackingReportRequestTypeWrapper request;

		// Token: 0x04000CBF RID: 3263
		private ExchangeVersion minVersionRequested;
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Hygiene.Data.Directory;

namespace Microsoft.Forefront.Reporting.OnDemandQuery
{
	// Token: 0x02000006 RID: 6
	internal class RequestBatch
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00002AE4 File Offset: 0x00000CE4
		internal RequestBatch(TenantReportingSession dalSession, OnDemandQueryType[] queryTypes, Guid batchId, IEnumerable<OnDemandQueryRequest> allRequests)
		{
			this.dalSession = dalSession;
			this.BatchID = batchId;
			this.Requests = new Dictionary<OnDemandQueryType, IEnumerable<OnDemandQueryRequest>>();
			for (int i = 0; i < queryTypes.Length; i++)
			{
				OnDemandQueryType type = queryTypes[i];
				IEnumerable<OnDemandQueryRequest> enumerable = from r in allRequests
				where r.QueryType == type
				select r;
				if (enumerable.Any<OnDemandQueryRequest>())
				{
					int num = 1000000 * (int)type + 1;
					int num2 = num;
					foreach (OnDemandQueryRequest onDemandQueryRequest in enumerable)
					{
						onDemandQueryRequest.BatchId = new Guid?(this.BatchID);
						onDemandQueryRequest.InBatchQueryId = num2;
						num2++;
					}
				}
				this.Requests.Add(type, enumerable);
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002BEC File Offset: 0x00000DEC
		internal RequestBatch(TenantReportingSession dalSession, List<RequestTracker> requestTrackers, Guid batchId)
		{
			this.dalSession = dalSession;
			this.Requests = new Dictionary<OnDemandQueryType, IEnumerable<OnDemandQueryRequest>>();
			foreach (IGrouping<OnDemandQueryType, RequestTracker> grouping in from tracker in requestTrackers
			group tracker by tracker.QueryType)
			{
				List<OnDemandQueryRequest> list = new List<OnDemandQueryRequest>();
				if (grouping.Count<RequestTracker>() > 0)
				{
					foreach (RequestTracker requestTracker in grouping)
					{
						OnDemandQueryRequest onDemandQueryRequest = this.dalSession.FindOnDemandReportRequests(requestTracker.TenantId, new Guid?(requestTracker.RequestId), null, null, 100).First<OnDemandQueryRequest>();
						onDemandQueryRequest.InBatchQueryId = requestTracker.InBatchQueryId;
						onDemandQueryRequest.BatchId = new Guid?(batchId);
						list.Add(onDemandQueryRequest);
					}
				}
				this.Requests.Add(grouping.Key, list);
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002D18 File Offset: 0x00000F18
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002D20 File Offset: 0x00000F20
		internal Dictionary<OnDemandQueryType, IEnumerable<OnDemandQueryRequest>> Requests { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002D29 File Offset: 0x00000F29
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00002D31 File Offset: 0x00000F31
		internal Guid BatchID { get; private set; }

		// Token: 0x06000028 RID: 40 RVA: 0x00002D3C File Offset: 0x00000F3C
		internal void UpdateCosmosJobAndStatus(Guid cosmosJobId)
		{
			foreach (OnDemandQueryRequest onDemandQueryRequest in this.GetAllRequests())
			{
				onDemandQueryRequest.RequestStatus = OnDemandQueryRequestStatus.InProgress;
				onDemandQueryRequest.CosmosJobId = new Guid?(cosmosJobId);
				this.dalSession.Save(onDemandQueryRequest);
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002DA4 File Offset: 0x00000FA4
		internal void UpdateStatus(OnDemandQueryRequestStatus newStatus)
		{
			foreach (OnDemandQueryRequest onDemandQueryRequest in this.GetAllRequests())
			{
				onDemandQueryRequest.RequestStatus = newStatus;
				this.dalSession.Save(onDemandQueryRequest);
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002E00 File Offset: 0x00001000
		internal void UpdateStatus(OnDemandQueryType queryType, OnDemandQueryRequestStatus newStatus)
		{
			foreach (OnDemandQueryRequest onDemandQueryRequest in this.Requests[queryType])
			{
				onDemandQueryRequest.RequestStatus = newStatus;
				this.dalSession.Save(onDemandQueryRequest);
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003084 File Offset: 0x00001284
		internal IEnumerable<OnDemandQueryRequest> GetAllRequests()
		{
			foreach (OnDemandQueryType queryType in this.Requests.Keys)
			{
				foreach (OnDemandQueryRequest request in this.Requests[queryType])
				{
					yield return request;
				}
			}
			yield break;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000030C0 File Offset: 0x000012C0
		internal List<RequestTracker> GetRequestTrackers()
		{
			return (from r in this.GetAllRequests()
			select new RequestTracker(r.TenantId, r.RequestId, r.InBatchQueryId, r.QueryType)).ToList<RequestTracker>();
		}

		// Token: 0x04000035 RID: 53
		private TenantReportingSession dalSession;
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Hygiene.Data.Directory;
using Microsoft.Forefront.Reporting.Common;

namespace Microsoft.Forefront.Reporting.OnDemandQuery
{
	// Token: 0x02000007 RID: 7
	internal class RequestBatchCompiler
	{
		// Token: 0x0600002F RID: 47 RVA: 0x000030EF File Offset: 0x000012EF
		public RequestBatchCompiler(RequestBatch batch) : this(batch, null, null)
		{
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003108 File Offset: 0x00001308
		public RequestBatchCompiler(RequestBatch batch, PIIHashingDelegate piiHashingDelegate, LSHDelegate lshDelegate)
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			HashSet<DateTime> hashSet = new HashSet<DateTime>();
			foreach (IGrouping<Guid, OnDemandQueryRequest> grouping in from request in batch.GetAllRequests()
			group request by request.TenantId)
			{
				stringBuilder.AppendFormat("case \"{0}\": ", grouping.Key.ToString());
				stringBuilder2.AppendFormat("case \"{0}\": ", grouping.Key.ToString());
				foreach (OnDemandQueryRequest onDemandQueryRequest in grouping)
				{
					OnDemandQueryType queryType = onDemandQueryRequest.QueryType;
					QueryCompiler queryCompiler = new QueryCompiler(queryType, onDemandQueryRequest.QueryDefinition, piiHashingDelegate, lshDelegate);
					foreach (Tuple<DateTime, DateTime> tuple in queryCompiler.QueryTimeRange)
					{
						DateTime t = tuple.Item1;
						while (t <= tuple.Item2)
						{
							hashSet.Add(t.Date);
							t = t.AddDays(1.0);
						}
					}
					stringBuilder.Append(queryCompiler.CompiledCode);
					stringBuilder.AppendFormat(" results.Add({0});", onDemandQueryRequest.InBatchQueryId);
					stringBuilder2.Append(queryCompiler.PreFilteringCode);
					stringBuilder2.Append(" return true;");
				}
				stringBuilder.AppendLine(" break;");
				stringBuilder2.AppendLine(" break;");
			}
			this.CompiledCode = stringBuilder.ToString();
			this.PrefilteringCode = stringBuilder2.ToString();
			this.QueryDates = (from dt in hashSet
			orderby dt
			select dt).ToList<DateTime>();
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00003368 File Offset: 0x00001568
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00003370 File Offset: 0x00001570
		internal string CompiledCode { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00003379 File Offset: 0x00001579
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00003381 File Offset: 0x00001581
		internal string PrefilteringCode { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000035 RID: 53 RVA: 0x0000338A File Offset: 0x0000158A
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00003392 File Offset: 0x00001592
		internal List<DateTime> QueryDates { get; private set; }
	}
}

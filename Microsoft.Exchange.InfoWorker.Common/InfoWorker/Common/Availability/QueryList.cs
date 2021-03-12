using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.RequestDispatch;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000BC RID: 188
	internal sealed class QueryList : IEnumerable<BaseQuery>, IEnumerable
	{
		// Token: 0x0600046E RID: 1134 RVA: 0x0001456D File Offset: 0x0001276D
		public QueryList() : this(new List<BaseQuery>())
		{
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0001457A File Offset: 0x0001277A
		public QueryList(int count) : this(new List<BaseQuery>(count))
		{
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00014588 File Offset: 0x00012788
		public QueryList(QueryList existing) : this(new List<BaseQuery>(existing))
		{
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00014596 File Offset: 0x00012796
		public QueryList(params BaseQuery[] baseQueries) : this(new List<BaseQuery>(baseQueries))
		{
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x000145A4 File Offset: 0x000127A4
		IEnumerator<BaseQuery> IEnumerable<BaseQuery>.GetEnumerator()
		{
			return this.queryList.GetEnumerator();
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x000145B6 File Offset: 0x000127B6
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.queryList.GetEnumerator();
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x000145C8 File Offset: 0x000127C8
		public void Add(BaseQuery baseQuery)
		{
			this.queryList.Add(baseQuery);
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x000145D6 File Offset: 0x000127D6
		public int Count
		{
			get
			{
				return this.queryList.Count;
			}
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x000145E3 File Offset: 0x000127E3
		public BaseQuery[] ToArray()
		{
			return this.queryList.ToArray();
		}

		// Token: 0x170000E3 RID: 227
		public BaseQuery this[int i]
		{
			get
			{
				return this.queryList[i];
			}
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00014600 File Offset: 0x00012800
		public void SetResultInAllQueries(BaseQueryResult result)
		{
			foreach (BaseQuery baseQuery in this.queryList)
			{
				if (baseQuery.SetResultOnFirstCall(result))
				{
					QueryList.RequestRoutingTracer.TraceError<object, EmailAddress, BaseQueryResult>((long)this.GetHashCode(), "{0}: the following result was set for query {1}: {2}", TraceContext.Get(), baseQuery.Email, result);
				}
			}
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00014678 File Offset: 0x00012878
		public void LogLatency(string key, long value)
		{
			foreach (BaseQuery baseQuery in this.queryList)
			{
				baseQuery.LogLatency(key, value);
			}
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x000146F4 File Offset: 0x000128F4
		public BaseQuery[] FindByEmailAddress(string email)
		{
			List<BaseQuery> list = this.queryList.FindAll((BaseQuery query) => StringComparer.OrdinalIgnoreCase.Equals(query.Email.Address, email));
			if (list.Count > 0)
			{
				return list.ToArray();
			}
			throw new ArgumentException(string.Format("The recipient with email address {0} was not found in the request", email), "email");
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00014750 File Offset: 0x00012950
		private QueryList(List<BaseQuery> queryList)
		{
			this.queryList = queryList;
		}

		// Token: 0x040002B6 RID: 694
		private List<BaseQuery> queryList;

		// Token: 0x040002B7 RID: 695
		private static readonly Trace RequestRoutingTracer = ExTraceGlobals.RequestRoutingTracer;
	}
}

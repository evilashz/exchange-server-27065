using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.InfoWorker.Common
{
	// Token: 0x02000019 RID: 25
	internal class QueryResultEnumerator<T, U, V, W> : QueryResultEnumeratorBase, IEnumerable<Quad<T, U, V, W>>, IEnumerable
	{
		// Token: 0x0600005F RID: 95 RVA: 0x00003483 File Offset: 0x00001683
		private QueryResultEnumerator(QueryResult queryResult) : base(queryResult)
		{
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000348C File Offset: 0x0000168C
		private QueryResultEnumerator(QueryResult queryResult, int fetchCount) : base(queryResult, fetchCount)
		{
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003496 File Offset: 0x00001696
		public static QueryResultEnumerator<T, U, V, W> From(QueryResult queryResult)
		{
			return new QueryResultEnumerator<T, U, V, W>(queryResult);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x0000349E File Offset: 0x0000169E
		public static QueryResultEnumerator<T, U, V, W> From(QueryResult queryResult, int fetchCount)
		{
			return new QueryResultEnumerator<T, U, V, W>(queryResult, fetchCount);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000036D4 File Offset: 0x000018D4
		public IEnumerator<Quad<T, U, V, W>> GetEnumerator()
		{
			for (;;)
			{
				object[][] rowResults = base.QueryResult.GetRows(base.FetchCount);
				if (rowResults == null || rowResults.Length <= 0)
				{
					break;
				}
				for (int i = 0; i < rowResults.Length; i++)
				{
					T first = default(T);
					U second = default(U);
					V third = default(V);
					W fourth = default(W);
					if (rowResults[i][0] != null && !(rowResults[i][0] is PropertyError))
					{
						first = (T)((object)rowResults[i][0]);
					}
					if (rowResults[i][1] != null && !(rowResults[i][1] is PropertyError))
					{
						second = (U)((object)rowResults[i][1]);
					}
					if (rowResults[i][2] != null && !(rowResults[i][2] is PropertyError))
					{
						third = (V)((object)rowResults[i][2]);
					}
					if (rowResults[i][3] != null && !(rowResults[i][3] is PropertyError))
					{
						fourth = (W)((object)rowResults[i][3]);
					}
					yield return new Quad<T, U, V, W>(first, second, third, fourth);
				}
			}
			yield break;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000036F0 File Offset: 0x000018F0
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}
}

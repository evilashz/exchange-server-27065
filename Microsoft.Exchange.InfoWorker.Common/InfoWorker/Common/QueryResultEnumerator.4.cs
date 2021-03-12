using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.InfoWorker.Common
{
	// Token: 0x02000018 RID: 24
	internal class QueryResultEnumerator<T, U, V> : QueryResultEnumeratorBase, IEnumerable<Triplet<T, U, V>>, IEnumerable
	{
		// Token: 0x06000059 RID: 89 RVA: 0x00003263 File Offset: 0x00001463
		private QueryResultEnumerator(QueryResult queryResult) : base(queryResult)
		{
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000326C File Offset: 0x0000146C
		private QueryResultEnumerator(QueryResult queryResult, int fetchCount) : base(queryResult, fetchCount)
		{
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003276 File Offset: 0x00001476
		public static QueryResultEnumerator<T, U, V> From(QueryResult queryResult)
		{
			return new QueryResultEnumerator<T, U, V>(queryResult);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000327E File Offset: 0x0000147E
		public static QueryResultEnumerator<T, U, V> From(QueryResult queryResult, int fetchCount)
		{
			return new QueryResultEnumerator<T, U, V>(queryResult, fetchCount);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003460 File Offset: 0x00001660
		public IEnumerator<Triplet<T, U, V>> GetEnumerator()
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
					yield return new Triplet<T, U, V>(first, second, third);
				}
			}
			yield break;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0000347C File Offset: 0x0000167C
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}
}

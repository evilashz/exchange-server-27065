using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.InfoWorker.Common
{
	// Token: 0x02000017 RID: 23
	internal class QueryResultEnumerator<T, U> : QueryResultEnumeratorBase, IEnumerable<Pair<T, U>>, IEnumerable
	{
		// Token: 0x06000053 RID: 83 RVA: 0x00003097 File Offset: 0x00001297
		private QueryResultEnumerator(QueryResult queryResult) : base(queryResult)
		{
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000030A0 File Offset: 0x000012A0
		private QueryResultEnumerator(QueryResult queryResult, int fetchCount) : base(queryResult, fetchCount)
		{
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000030AA File Offset: 0x000012AA
		public static QueryResultEnumerator<T, U> From(QueryResult queryResult)
		{
			return new QueryResultEnumerator<T, U>(queryResult);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000030B2 File Offset: 0x000012B2
		public static QueryResultEnumerator<T, U> From(QueryResult queryResult, int fetchCount)
		{
			return new QueryResultEnumerator<T, U>(queryResult, fetchCount);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003240 File Offset: 0x00001440
		public IEnumerator<Pair<T, U>> GetEnumerator()
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
					if (rowResults[i][0] != null && !(rowResults[i][0] is PropertyError))
					{
						first = (T)((object)rowResults[i][0]);
					}
					if (rowResults[i][1] != null && !(rowResults[i][1] is PropertyError))
					{
						second = (U)((object)rowResults[i][1]);
					}
					yield return new Pair<T, U>(first, second);
				}
			}
			yield break;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000325C File Offset: 0x0000145C
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}
}

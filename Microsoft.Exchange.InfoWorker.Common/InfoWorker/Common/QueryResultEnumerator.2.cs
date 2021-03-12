using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.InfoWorker.Common
{
	// Token: 0x02000016 RID: 22
	internal class QueryResultEnumerator<T> : QueryResultEnumeratorBase, IEnumerable<T>, IEnumerable
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00002F0F File Offset: 0x0000110F
		private QueryResultEnumerator(QueryResult queryResult) : base(queryResult)
		{
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002F18 File Offset: 0x00001118
		private QueryResultEnumerator(QueryResult queryResult, int fetchCount) : base(queryResult, fetchCount)
		{
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002F22 File Offset: 0x00001122
		public static QueryResultEnumerator<T> From(QueryResult queryResult)
		{
			return new QueryResultEnumerator<T>(queryResult);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002F2A File Offset: 0x0000112A
		public static QueryResultEnumerator<T> From(QueryResult queryResult, int fetchCount)
		{
			return new QueryResultEnumerator<T>(queryResult, fetchCount);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003074 File Offset: 0x00001274
		public IEnumerator<T> GetEnumerator()
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
					if (rowResults[i][0] != null && !(rowResults[i][0] is PropertyError))
					{
						yield return (T)((object)rowResults[i][0]);
					}
					else
					{
						yield return default(T);
					}
				}
			}
			yield break;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003090 File Offset: 0x00001290
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}
}

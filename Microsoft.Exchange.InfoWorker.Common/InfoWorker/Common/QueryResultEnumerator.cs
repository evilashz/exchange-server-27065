using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.InfoWorker.Common
{
	// Token: 0x02000015 RID: 21
	internal class QueryResultEnumerator : QueryResultEnumeratorBase, IEnumerable<object[]>, IEnumerable
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00002DEE File Offset: 0x00000FEE
		private QueryResultEnumerator(QueryResult queryResult) : base(queryResult)
		{
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002DF7 File Offset: 0x00000FF7
		private QueryResultEnumerator(QueryResult queryResult, int fetchCount) : base(queryResult, fetchCount)
		{
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002E01 File Offset: 0x00001001
		public static QueryResultEnumerator From(QueryResult queryResult)
		{
			return new QueryResultEnumerator(queryResult);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002E09 File Offset: 0x00001009
		public static QueryResultEnumerator From(QueryResult queryResult, int fetchCount)
		{
			return new QueryResultEnumerator(queryResult, fetchCount);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002EEC File Offset: 0x000010EC
		public IEnumerator<object[]> GetEnumerator()
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
					yield return rowResults[i];
				}
			}
			yield break;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002F08 File Offset: 0x00001108
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}
}

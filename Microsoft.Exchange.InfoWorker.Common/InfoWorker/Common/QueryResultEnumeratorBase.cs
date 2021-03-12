using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.InfoWorker.Common
{
	// Token: 0x02000014 RID: 20
	internal class QueryResultEnumeratorBase
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00002D91 File Offset: 0x00000F91
		protected QueryResultEnumeratorBase(QueryResult queryResult) : this(queryResult, QueryResultEnumeratorBase.DefaultFetchCount)
		{
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002D9F File Offset: 0x00000F9F
		protected QueryResultEnumeratorBase(QueryResult queryResult, int fetchCount)
		{
			if (queryResult == null)
			{
				throw new ArgumentNullException("queryResult");
			}
			if (fetchCount <= 0)
			{
				throw new ArgumentException("fetchCount");
			}
			this.queryResult = queryResult;
			this.fetchCount = fetchCount;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002DD2 File Offset: 0x00000FD2
		internal QueryResult QueryResult
		{
			get
			{
				return this.queryResult;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002DDA File Offset: 0x00000FDA
		internal int FetchCount
		{
			get
			{
				return this.fetchCount;
			}
		}

		// Token: 0x04000030 RID: 48
		public static readonly int DefaultFetchCount = 512;

		// Token: 0x04000031 RID: 49
		private QueryResult queryResult;

		// Token: 0x04000032 RID: 50
		private int fetchCount;
	}
}

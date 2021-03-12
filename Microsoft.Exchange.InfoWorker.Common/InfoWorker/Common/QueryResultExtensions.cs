using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.InfoWorker.Common
{
	// Token: 0x02000013 RID: 19
	internal static class QueryResultExtensions
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00002D3C File Offset: 0x00000F3C
		public static QueryResultEnumerator Enumerator(this QueryResult queryResult)
		{
			return QueryResultEnumerator.From(queryResult);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002D44 File Offset: 0x00000F44
		public static QueryResultEnumerator Enumerator(this QueryResult queryResult, int fetchCount)
		{
			return QueryResultEnumerator.From(queryResult, fetchCount);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002D4D File Offset: 0x00000F4D
		public static QueryResultEnumerator<T> Enumerator<T>(this QueryResult queryResult)
		{
			return QueryResultEnumerator<T>.From(queryResult);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002D55 File Offset: 0x00000F55
		public static QueryResultEnumerator<T> Enumerator<T>(this QueryResult queryResult, int fetchCount)
		{
			return QueryResultEnumerator<T>.From(queryResult, fetchCount);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002D5E File Offset: 0x00000F5E
		public static QueryResultEnumerator<T, U> Enumerator<T, U>(this QueryResult queryResult)
		{
			return QueryResultEnumerator<T, U>.From(queryResult);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002D66 File Offset: 0x00000F66
		public static QueryResultEnumerator<T, U> Enumerator<T, U>(this QueryResult queryResult, int fetchCount)
		{
			return QueryResultEnumerator<T, U>.From(queryResult, fetchCount);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002D6F File Offset: 0x00000F6F
		public static QueryResultEnumerator<T, U, V> Enumerator<T, U, V>(this QueryResult queryResult)
		{
			return QueryResultEnumerator<T, U, V>.From(queryResult);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002D77 File Offset: 0x00000F77
		public static QueryResultEnumerator<T, U, V> Enumerator<T, U, V>(this QueryResult queryResult, int fetchCount)
		{
			return QueryResultEnumerator<T, U, V>.From(queryResult, fetchCount);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002D80 File Offset: 0x00000F80
		public static QueryResultEnumerator<T, U, V, W> Enumerator<T, U, V, W>(this QueryResult queryResult)
		{
			return QueryResultEnumerator<T, U, V, W>.From(queryResult);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002D88 File Offset: 0x00000F88
		public static QueryResultEnumerator<T, U, V, W> Enumerator<T, U, V, W>(this QueryResult queryResult, int fetchCount)
		{
			return QueryResultEnumerator<T, U, V, W>.From(queryResult, fetchCount);
		}
	}
}

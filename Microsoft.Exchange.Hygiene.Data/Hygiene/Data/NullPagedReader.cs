using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000095 RID: 149
	internal class NullPagedReader<TResult> : IPagedReader<TResult>, IEnumerable<TResult>, IEnumerable where TResult : IConfigurable, new()
	{
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x00010DB8 File Offset: 0x0000EFB8
		public bool RetrievedAllData
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x00010DBB File Offset: 0x0000EFBB
		public int PagesReturned
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x00010DBE File Offset: 0x0000EFBE
		public int PageSize
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00010DC1 File Offset: 0x0000EFC1
		public TResult[] ReadAllPages()
		{
			return new TResult[0];
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00010E1C File Offset: 0x0000F01C
		public IEnumerator<TResult> GetEnumerator()
		{
			yield break;
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00010E38 File Offset: 0x0000F038
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00010E40 File Offset: 0x0000F040
		public TResult[] GetNextPage()
		{
			return new TResult[0];
		}
	}
}

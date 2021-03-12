using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Ceres.InteractionEngine.Services.Exchange;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.FullTextIndex
{
	// Token: 0x02000014 RID: 20
	public class PagedQueryResults : DisposableBase
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00005513 File Offset: 0x00003713
		public IEnumerator PagedResults
		{
			get
			{
				return this.pagedResults;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x0000551B File Offset: 0x0000371B
		public bool IsInitialized
		{
			get
			{
				return this.pagedResults != null;
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00005529 File Offset: 0x00003729
		public void Initialize(IEnumerator<SearchResultItem[]> pagedResults)
		{
			this.pagedResults = (pagedResults ?? new List<SearchResultItem[]>(0).GetEnumerator());
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00005546 File Offset: 0x00003746
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PagedQueryResults>(this);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000554E File Offset: 0x0000374E
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.pagedResults != null)
			{
				this.pagedResults.Dispose();
				this.pagedResults = null;
			}
		}

		// Token: 0x04000061 RID: 97
		private IEnumerator<SearchResultItem[]> pagedResults;
	}
}

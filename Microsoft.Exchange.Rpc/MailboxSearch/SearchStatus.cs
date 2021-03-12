using System;

namespace Microsoft.Exchange.Rpc.MailboxSearch
{
	// Token: 0x0200027F RID: 639
	internal class SearchStatus
	{
		// Token: 0x06000BF6 RID: 3062 RVA: 0x0002A494 File Offset: 0x00029894
		public SearchStatus(string searchId, string owner, int status, int percentCompleted, long estimateResultItems, ulong estimateResultSize, long resultItems, ulong resultSize, string resultLink)
		{
			this.m_searchId = searchId;
			this.m_owner = owner;
			this.m_status = status;
			this.m_percentCompleted = percentCompleted;
			this.m_estimateResultItems = estimateResultItems;
			this.m_estimateResultSize = estimateResultSize;
			this.m_resultItems = resultItems;
			this.m_resultSize = resultSize;
			this.m_resultLink = resultLink;
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x0002A4EC File Offset: 0x000298EC
		// (set) Token: 0x06000BF8 RID: 3064 RVA: 0x0002A500 File Offset: 0x00029900
		public string SearchId
		{
			get
			{
				return this.m_searchId;
			}
			set
			{
				this.m_searchId = value;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x0002A514 File Offset: 0x00029914
		// (set) Token: 0x06000BFA RID: 3066 RVA: 0x0002A528 File Offset: 0x00029928
		public string Owner
		{
			get
			{
				return this.m_owner;
			}
			set
			{
				this.m_owner = value;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x0002A53C File Offset: 0x0002993C
		// (set) Token: 0x06000BFC RID: 3068 RVA: 0x0002A550 File Offset: 0x00029950
		public int Status
		{
			get
			{
				return this.m_status;
			}
			set
			{
				this.m_status = value;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x0002A564 File Offset: 0x00029964
		// (set) Token: 0x06000BFE RID: 3070 RVA: 0x0002A578 File Offset: 0x00029978
		public int PercentCompleted
		{
			get
			{
				return this.m_percentCompleted;
			}
			set
			{
				this.m_percentCompleted = value;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000BFF RID: 3071 RVA: 0x0002A58C File Offset: 0x0002998C
		// (set) Token: 0x06000C00 RID: 3072 RVA: 0x0002A5A0 File Offset: 0x000299A0
		public long EstimateResultItems
		{
			get
			{
				return this.m_estimateResultItems;
			}
			set
			{
				this.m_estimateResultItems = value;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000C01 RID: 3073 RVA: 0x0002A5B4 File Offset: 0x000299B4
		// (set) Token: 0x06000C02 RID: 3074 RVA: 0x0002A5C8 File Offset: 0x000299C8
		public ulong EstimateResultSize
		{
			get
			{
				return this.m_estimateResultSize;
			}
			set
			{
				this.m_estimateResultSize = value;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000C03 RID: 3075 RVA: 0x0002A5DC File Offset: 0x000299DC
		// (set) Token: 0x06000C04 RID: 3076 RVA: 0x0002A5F0 File Offset: 0x000299F0
		public long ResultItems
		{
			get
			{
				return this.m_resultItems;
			}
			set
			{
				this.m_resultItems = value;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x0002A604 File Offset: 0x00029A04
		// (set) Token: 0x06000C06 RID: 3078 RVA: 0x0002A618 File Offset: 0x00029A18
		public ulong ResultSize
		{
			get
			{
				return this.m_resultSize;
			}
			set
			{
				this.m_resultSize = value;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x0002A62C File Offset: 0x00029A2C
		// (set) Token: 0x06000C08 RID: 3080 RVA: 0x0002A640 File Offset: 0x00029A40
		public string ResultLink
		{
			get
			{
				return this.m_resultLink;
			}
			set
			{
				this.m_resultLink = value;
			}
		}

		// Token: 0x04000D22 RID: 3362
		private string m_searchId;

		// Token: 0x04000D23 RID: 3363
		private string m_owner;

		// Token: 0x04000D24 RID: 3364
		private int m_status;

		// Token: 0x04000D25 RID: 3365
		private int m_percentCompleted;

		// Token: 0x04000D26 RID: 3366
		private long m_estimateResultItems;

		// Token: 0x04000D27 RID: 3367
		private ulong m_estimateResultSize;

		// Token: 0x04000D28 RID: 3368
		private long m_resultItems;

		// Token: 0x04000D29 RID: 3369
		private ulong m_resultSize;

		// Token: 0x04000D2A RID: 3370
		private string m_resultLink;
	}
}

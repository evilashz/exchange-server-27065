using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;

namespace Microsoft.Exchange.InfoWorker.Common.Search
{
	// Token: 0x02000231 RID: 561
	public class SearchMailboxResult
	{
		// Token: 0x06000F69 RID: 3945 RVA: 0x00044754 File Offset: 0x00042954
		public SearchMailboxResult(ADObjectId identity)
		{
			this.identity = identity;
			this.ResultItemsCount = 0;
			this.Success = false;
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000F6A RID: 3946 RVA: 0x00044789 File Offset: 0x00042989
		public ADObjectId Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000F6B RID: 3947 RVA: 0x00044791 File Offset: 0x00042991
		// (set) Token: 0x06000F6C RID: 3948 RVA: 0x00044799 File Offset: 0x00042999
		public ADObjectId TargetMailbox
		{
			get
			{
				return this.targetMailbox;
			}
			set
			{
				this.targetMailbox = value;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000F6D RID: 3949 RVA: 0x000447A2 File Offset: 0x000429A2
		// (set) Token: 0x06000F6E RID: 3950 RVA: 0x000447AA File Offset: 0x000429AA
		public bool Success
		{
			get
			{
				return this.success;
			}
			set
			{
				this.success = value;
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000F6F RID: 3951 RVA: 0x000447B3 File Offset: 0x000429B3
		// (set) Token: 0x06000F70 RID: 3952 RVA: 0x000447BB File Offset: 0x000429BB
		public string TargetFolder
		{
			get
			{
				return this.targetFolder;
			}
			set
			{
				this.targetFolder = value;
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000F71 RID: 3953 RVA: 0x000447C4 File Offset: 0x000429C4
		// (set) Token: 0x06000F72 RID: 3954 RVA: 0x000447CC File Offset: 0x000429CC
		public int ResultItemsCount
		{
			get
			{
				return this.resultItemsCount;
			}
			set
			{
				this.resultItemsCount = value;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000F73 RID: 3955 RVA: 0x000447D5 File Offset: 0x000429D5
		// (set) Token: 0x06000F74 RID: 3956 RVA: 0x000447DD File Offset: 0x000429DD
		public ByteQuantifiedSize ResultItemsSize
		{
			get
			{
				return this.resultItemsSize;
			}
			internal set
			{
				this.resultItemsSize = value;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000F75 RID: 3957 RVA: 0x000447E6 File Offset: 0x000429E6
		internal IDictionary<string, KeywordHit> SubQueryResults
		{
			get
			{
				return this.subQueryResults;
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000F76 RID: 3958 RVA: 0x000447EE File Offset: 0x000429EE
		// (set) Token: 0x06000F77 RID: 3959 RVA: 0x000447F6 File Offset: 0x000429F6
		internal Exception LastException
		{
			get
			{
				return this.lastException;
			}
			set
			{
				this.lastException = value;
			}
		}

		// Token: 0x04000A8C RID: 2700
		private ADObjectId identity;

		// Token: 0x04000A8D RID: 2701
		private string targetFolder;

		// Token: 0x04000A8E RID: 2702
		private ADObjectId targetMailbox;

		// Token: 0x04000A8F RID: 2703
		private int resultItemsCount;

		// Token: 0x04000A90 RID: 2704
		private bool success;

		// Token: 0x04000A91 RID: 2705
		private ByteQuantifiedSize resultItemsSize = ByteQuantifiedSize.FromBytes(0UL);

		// Token: 0x04000A92 RID: 2706
		private Exception lastException;

		// Token: 0x04000A93 RID: 2707
		private IDictionary<string, KeywordHit> subQueryResults = new Dictionary<string, KeywordHit>();
	}
}

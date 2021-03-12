using System;

namespace Microsoft.Exchange.Rpc.MultiMailboxSearch
{
	// Token: 0x0200017A RID: 378
	[Serializable]
	internal sealed class PageReference : MultiMailboxSearchBase
	{
		// Token: 0x0600092F RID: 2351 RVA: 0x0000A338 File Offset: 0x00009738
		internal PageReference(int version) : base(version)
		{
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0000A320 File Offset: 0x00009720
		internal PageReference() : base(MultiMailboxSearchBase.CurrentVersion)
		{
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x0000A34C File Offset: 0x0000974C
		// (set) Token: 0x06000932 RID: 2354 RVA: 0x0000A360 File Offset: 0x00009760
		internal MultiMailboxSearchResult PreviousPageReference
		{
			get
			{
				return this.previousPageReferenceItem;
			}
			set
			{
				this.previousPageReferenceItem = value;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x0000A374 File Offset: 0x00009774
		// (set) Token: 0x06000934 RID: 2356 RVA: 0x0000A388 File Offset: 0x00009788
		internal MultiMailboxSearchResult NextPageReference
		{
			get
			{
				return this.nextPageReferenceItem;
			}
			set
			{
				this.nextPageReferenceItem = value;
			}
		}

		// Token: 0x04000B1E RID: 2846
		private MultiMailboxSearchResult previousPageReferenceItem;

		// Token: 0x04000B1F RID: 2847
		private MultiMailboxSearchResult nextPageReferenceItem;
	}
}

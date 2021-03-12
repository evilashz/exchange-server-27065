using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006F2 RID: 1778
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SharepointWeb
	{
		// Token: 0x06004685 RID: 18053 RVA: 0x0012C839 File Offset: 0x0012AA39
		internal SharepointWeb(string title, SharepointSiteId siteId)
		{
			this.title = title;
			this.siteId = siteId;
		}

		// Token: 0x17001482 RID: 5250
		// (get) Token: 0x06004686 RID: 18054 RVA: 0x0012C84F File Offset: 0x0012AA4F
		public string Title
		{
			get
			{
				return this.title;
			}
		}

		// Token: 0x17001483 RID: 5251
		// (get) Token: 0x06004687 RID: 18055 RVA: 0x0012C857 File Offset: 0x0012AA57
		public Uri Uri
		{
			get
			{
				return this.siteId.SiteUri;
			}
		}

		// Token: 0x17001484 RID: 5252
		// (get) Token: 0x06004688 RID: 18056 RVA: 0x0012C864 File Offset: 0x0012AA64
		public ObjectId Id
		{
			get
			{
				return this.siteId;
			}
		}

		// Token: 0x0400269B RID: 9883
		private readonly string title;

		// Token: 0x0400269C RID: 9884
		private readonly SharepointSiteId siteId;
	}
}

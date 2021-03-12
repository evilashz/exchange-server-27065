using System;
using Microsoft.Exchange.Common.Cache;

namespace Microsoft.Exchange.Data.Directory.IsMemberOfProvider
{
	// Token: 0x020001CB RID: 459
	internal class ResolvedGroup : CachableItem
	{
		// Token: 0x060012A4 RID: 4772 RVA: 0x00059B28 File Offset: 0x00057D28
		public ResolvedGroup() : this(Guid.Empty)
		{
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x00059B35 File Offset: 0x00057D35
		public ResolvedGroup(Guid groupGuid)
		{
			this.groupGuid = groupGuid;
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x060012A6 RID: 4774 RVA: 0x00059B44 File Offset: 0x00057D44
		public Guid GroupGuid
		{
			get
			{
				return this.groupGuid;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x060012A7 RID: 4775 RVA: 0x00059B4C File Offset: 0x00057D4C
		public override long ItemSize
		{
			get
			{
				return 16L;
			}
		}

		// Token: 0x04000AB8 RID: 2744
		private const int GuidLength = 16;

		// Token: 0x04000AB9 RID: 2745
		private Guid groupGuid;
	}
}

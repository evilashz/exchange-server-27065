using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x0200009C RID: 156
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct CacheEntry<TValue> : ILifetimeTrackable
	{
		// Token: 0x060006B8 RID: 1720 RVA: 0x00018656 File Offset: 0x00016856
		public CacheEntry(TValue value)
		{
			this.Value = value;
			this.createTime = DateTime.UtcNow;
			this.lastAccessTime = this.createTime;
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x00018676 File Offset: 0x00016876
		public DateTime CreateTime
		{
			get
			{
				return this.createTime;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x0001867E File Offset: 0x0001687E
		// (set) Token: 0x060006BB RID: 1723 RVA: 0x00018686 File Offset: 0x00016886
		public DateTime LastAccessTime
		{
			get
			{
				return this.lastAccessTime;
			}
			set
			{
				this.lastAccessTime = value;
			}
		}

		// Token: 0x040002FB RID: 763
		public readonly TValue Value;

		// Token: 0x040002FC RID: 764
		private readonly DateTime createTime;

		// Token: 0x040002FD RID: 765
		private DateTime lastAccessTime;
	}
}

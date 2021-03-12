using System;

namespace Microsoft.Exchange.Clients.Common
{
	// Token: 0x02000026 RID: 38
	public class LiveHeaderMenu : ILiveHeaderElement
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600010D RID: 269 RVA: 0x000081BB File Offset: 0x000063BB
		// (set) Token: 0x0600010E RID: 270 RVA: 0x000081D6 File Offset: 0x000063D6
		public LiveHeaderLinkCollection List
		{
			get
			{
				if (this.list == null)
				{
					this.list = new LiveHeaderLinkCollection();
				}
				return this.list;
			}
			set
			{
				this.list = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600010F RID: 271 RVA: 0x000081DF File Offset: 0x000063DF
		// (set) Token: 0x06000110 RID: 272 RVA: 0x000081FA File Offset: 0x000063FA
		public LiveHeaderLink Link
		{
			get
			{
				if (this.link == null)
				{
					this.link = new LiveHeaderLink();
				}
				return this.link;
			}
			set
			{
				this.link = value;
			}
		}

		// Token: 0x0400026D RID: 621
		private LiveHeaderLinkCollection list;

		// Token: 0x0400026E RID: 622
		private LiveHeaderLink link;
	}
}

using System;

namespace Microsoft.Exchange.Data.Directory.DirSync
{
	// Token: 0x020001B2 RID: 434
	[Serializable]
	internal class ADDirSyncLink
	{
		// Token: 0x06001210 RID: 4624 RVA: 0x00057B05 File Offset: 0x00055D05
		public ADDirSyncLink(ADObjectId link, LinkState state)
		{
			this.Link = link;
			this.State = state;
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06001211 RID: 4625 RVA: 0x00057B1B File Offset: 0x00055D1B
		// (set) Token: 0x06001212 RID: 4626 RVA: 0x00057B23 File Offset: 0x00055D23
		public ADObjectId Link { get; private set; }

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06001213 RID: 4627 RVA: 0x00057B2C File Offset: 0x00055D2C
		// (set) Token: 0x06001214 RID: 4628 RVA: 0x00057B34 File Offset: 0x00055D34
		public LinkState State { get; private set; }
	}
}

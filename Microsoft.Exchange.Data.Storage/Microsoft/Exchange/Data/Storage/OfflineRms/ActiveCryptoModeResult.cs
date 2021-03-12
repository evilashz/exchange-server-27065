using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Data.Storage.OfflineRms
{
	// Token: 0x02000AC4 RID: 2756
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ActiveCryptoModeResult
	{
		// Token: 0x0600646A RID: 25706 RVA: 0x001A9B76 File Offset: 0x001A7D76
		internal ActiveCryptoModeResult(int cryptoMode, RightsManagementServerException e)
		{
			this.ActiveCryptoMode = cryptoMode;
			this.Error = e;
		}

		// Token: 0x17001BBD RID: 7101
		// (get) Token: 0x0600646B RID: 25707 RVA: 0x001A9B8C File Offset: 0x001A7D8C
		// (set) Token: 0x0600646C RID: 25708 RVA: 0x001A9B94 File Offset: 0x001A7D94
		public int ActiveCryptoMode { get; private set; }

		// Token: 0x17001BBE RID: 7102
		// (get) Token: 0x0600646D RID: 25709 RVA: 0x001A9B9D File Offset: 0x001A7D9D
		// (set) Token: 0x0600646E RID: 25710 RVA: 0x001A9BA5 File Offset: 0x001A7DA5
		public RightsManagementServerException Error { get; private set; }
	}
}

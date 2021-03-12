using System;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000093 RID: 147
	public enum HygieneAgent
	{
		// Token: 0x04000198 RID: 408
		[LocDescription(Strings.IDs.ContentFilteringFeatureName)]
		ContentFilter,
		// Token: 0x04000199 RID: 409
		[LocDescription(Strings.IDs.IPBlockProvidersFeatureName)]
		IPBlockListProviders,
		// Token: 0x0400019A RID: 410
		[LocDescription(Strings.IDs.IPBlockListFeatureName)]
		IPBlockList,
		// Token: 0x0400019B RID: 411
		[LocDescription(Strings.IDs.IPAllowProvidersFeatureName)]
		IPAllowListProviders,
		// Token: 0x0400019C RID: 412
		[LocDescription(Strings.IDs.IPAllowListFeatureName)]
		IPAllowList,
		// Token: 0x0400019D RID: 413
		[LocDescription(Strings.IDs.RecipientFilteringFeatureName)]
		RecipientFilter,
		// Token: 0x0400019E RID: 414
		[LocDescription(Strings.IDs.SenderFilteringFeatureName)]
		SenderFilter,
		// Token: 0x0400019F RID: 415
		[LocDescription(Strings.IDs.SenderIdFeatureName)]
		SenderId,
		// Token: 0x040001A0 RID: 416
		[LocDescription(Strings.IDs.SenderReputationFeatureName)]
		SenderReputation
	}
}

using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000143 RID: 323
	public enum ExchangeErrorCategory
	{
		// Token: 0x040006A8 RID: 1704
		Client = 1000,
		// Token: 0x040006A9 RID: 1705
		ServerOperation,
		// Token: 0x040006AA RID: 1706
		ServerTransient,
		// Token: 0x040006AB RID: 1707
		Context,
		// Token: 0x040006AC RID: 1708
		Authorization,
		// Token: 0x040006AD RID: 1709
		LiveIdAlreadyExists,
		// Token: 0x040006AE RID: 1710
		WindowsLiveIdAlreadyUsed,
		// Token: 0x040006AF RID: 1711
		WLCDPasswordInvalid,
		// Token: 0x040006B0 RID: 1712
		EasiIdAlreadyExists,
		// Token: 0x040006B1 RID: 1713
		MailboxMissingServerLegacyDN,
		// Token: 0x040006B2 RID: 1714
		UpdateLegacyMailboxNotSupported,
		// Token: 0x040006B3 RID: 1715
		GlobalCatalogNotAvailable,
		// Token: 0x040006B4 RID: 1716
		ProvisioningLayerNotAvailable
	}
}

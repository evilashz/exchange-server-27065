using System;

namespace Microsoft.Exchange.Data.Directory.Cache
{
	// Token: 0x020000A3 RID: 163
	[Flags]
	internal enum ObjectType
	{
		// Token: 0x04000300 RID: 768
		Unknown = 0,
		// Token: 0x04000301 RID: 769
		ExchangeConfigurationUnit = 1,
		// Token: 0x04000302 RID: 770
		Recipient = 2,
		// Token: 0x04000303 RID: 771
		AcceptedDomain = 4,
		// Token: 0x04000304 RID: 772
		FederatedOrganizationId = 8,
		// Token: 0x04000305 RID: 773
		MiniRecipient = 16,
		// Token: 0x04000306 RID: 774
		TransportMiniRecipient = 32,
		// Token: 0x04000307 RID: 775
		OWAMiniRecipient = 64,
		// Token: 0x04000308 RID: 776
		ActiveSyncMiniRecipient = 128,
		// Token: 0x04000309 RID: 777
		ADRawEntry = 256,
		// Token: 0x0400030A RID: 778
		StorageMiniRecipient = 512,
		// Token: 0x0400030B RID: 779
		LoadBalancingMiniRecipient = 1024,
		// Token: 0x0400030C RID: 780
		MiniRecipientWithTokenGroups = 2048,
		// Token: 0x0400030D RID: 781
		FrontEndMiniRecipient = 4096
	}
}

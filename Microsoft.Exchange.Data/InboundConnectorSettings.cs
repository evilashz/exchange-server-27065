using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000B0 RID: 176
	[Flags]
	internal enum InboundConnectorSettings
	{
		// Token: 0x040002AF RID: 687
		Default = 0,
		// Token: 0x040002B0 RID: 688
		ConnectionFilteringEnabled = 1,
		// Token: 0x040002B1 RID: 689
		AntiSpamFilteringEnabled = 2,
		// Token: 0x040002B2 RID: 690
		AntiMalwareFilteringEnabled = 4,
		// Token: 0x040002B3 RID: 691
		PolicyFilteringEnabled = 8,
		// Token: 0x040002B4 RID: 692
		RejectAnonymousConnection = 16,
		// Token: 0x040002B5 RID: 693
		RequireTls = 32,
		// Token: 0x040002B6 RID: 694
		RejectUntrustedConnection = 64,
		// Token: 0x040002B7 RID: 695
		CloudServicesMailEnabled = 128
	}
}

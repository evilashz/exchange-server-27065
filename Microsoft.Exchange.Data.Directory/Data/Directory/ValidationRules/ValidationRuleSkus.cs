using System;

namespace Microsoft.Exchange.Data.Directory.ValidationRules
{
	// Token: 0x02000A30 RID: 2608
	[Flags]
	internal enum ValidationRuleSkus : byte
	{
		// Token: 0x04004CC4 RID: 19652
		None = 0,
		// Token: 0x04004CC5 RID: 19653
		Enterprise = 1,
		// Token: 0x04004CC6 RID: 19654
		Datacenter = 2,
		// Token: 0x04004CC7 RID: 19655
		DatacenterTenant = 4,
		// Token: 0x04004CC8 RID: 19656
		Hosted = 8,
		// Token: 0x04004CC9 RID: 19657
		HostedTenant = 16,
		// Token: 0x04004CCA RID: 19658
		All = 255
	}
}

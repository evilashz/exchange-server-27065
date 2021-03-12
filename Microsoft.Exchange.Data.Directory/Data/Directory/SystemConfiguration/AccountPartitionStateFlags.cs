using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002A2 RID: 674
	[Flags]
	internal enum AccountPartitionStateFlags
	{
		// Token: 0x040012AF RID: 4783
		None = 0,
		// Token: 0x040012B0 RID: 4784
		IsLocalForest = 1,
		// Token: 0x040012B1 RID: 4785
		EnabledForProvisioning = 2,
		// Token: 0x040012B2 RID: 4786
		SecondaryAccountPartition = 4
	}
}

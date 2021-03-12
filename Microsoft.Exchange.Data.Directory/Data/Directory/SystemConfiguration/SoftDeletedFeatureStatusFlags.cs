using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002CB RID: 715
	[Flags]
	public enum SoftDeletedFeatureStatusFlags
	{
		// Token: 0x040013C7 RID: 5063
		Disabled = 0,
		// Token: 0x040013C8 RID: 5064
		EDUEnabled = 1,
		// Token: 0x040013C9 RID: 5065
		MSOEnabled = 2,
		// Token: 0x040013CA RID: 5066
		ForwardSyncEnabled = 4
	}
}

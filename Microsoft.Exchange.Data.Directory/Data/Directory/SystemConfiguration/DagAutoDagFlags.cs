using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003DD RID: 989
	[Flags]
	internal enum DagAutoDagFlags
	{
		// Token: 0x04001E8D RID: 7821
		None = 0,
		// Token: 0x04001E8E RID: 7822
		AutoDagEnabled = 1,
		// Token: 0x04001E8F RID: 7823
		DatabaseCopyLocationAgilityDisabled = 2,
		// Token: 0x04001E90 RID: 7824
		AutoReseedDisabled = 4,
		// Token: 0x04001E91 RID: 7825
		AllServersInstalled = 8,
		// Token: 0x04001E92 RID: 7826
		ReplayLagManagerEnabled = 16,
		// Token: 0x04001E93 RID: 7827
		DiskReclaimerDisabled = 32,
		// Token: 0x04001E94 RID: 7828
		BitlockerEnabled = 64,
		// Token: 0x04001E95 RID: 7829
		FIPSCompliant = 128
	}
}

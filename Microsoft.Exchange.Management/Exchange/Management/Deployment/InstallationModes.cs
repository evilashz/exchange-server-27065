using System;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000231 RID: 561
	public enum InstallationModes
	{
		// Token: 0x040007FB RID: 2043
		Unknown,
		// Token: 0x040007FC RID: 2044
		Install,
		// Token: 0x040007FD RID: 2045
		BuildToBuildUpgrade,
		// Token: 0x040007FE RID: 2046
		DisasterRecovery,
		// Token: 0x040007FF RID: 2047
		Uninstall,
		// Token: 0x04000800 RID: 2048
		PostSetupOnly
	}
}

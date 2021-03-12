using System;

namespace Microsoft.Exchange.Management.Deployment.PrereqAnalysisSample
{
	// Token: 0x02000078 RID: 120
	[Flags]
	public enum SetupMode
	{
		// Token: 0x040005C9 RID: 1481
		None = 0,
		// Token: 0x040005CA RID: 1482
		Install = 1,
		// Token: 0x040005CB RID: 1483
		Upgrade = 2,
		// Token: 0x040005CC RID: 1484
		Uninstall = 4,
		// Token: 0x040005CD RID: 1485
		DisasterRecovery = 8,
		// Token: 0x040005CE RID: 1486
		All = 15
	}
}

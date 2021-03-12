using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002CE RID: 718
	public enum UpgradeStage
	{
		// Token: 0x040013DC RID: 5084
		None,
		// Token: 0x040013DD RID: 5085
		SyncedWorkItem,
		// Token: 0x040013DE RID: 5086
		StartPilotUpgrade,
		// Token: 0x040013DF RID: 5087
		StartOrgUpgrade,
		// Token: 0x040013E0 RID: 5088
		MoveArbitration,
		// Token: 0x040013E1 RID: 5089
		MoveRegularUser,
		// Token: 0x040013E2 RID: 5090
		MoveCloudOnlyArchive,
		// Token: 0x040013E3 RID: 5091
		MoveRegularPilot,
		// Token: 0x040013E4 RID: 5092
		MoveCloudOnlyArchivePilot,
		// Token: 0x040013E5 RID: 5093
		CompleteOrgUpgrade
	}
}

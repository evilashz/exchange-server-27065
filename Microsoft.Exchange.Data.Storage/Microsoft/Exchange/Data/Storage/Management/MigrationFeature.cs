using System;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A29 RID: 2601
	[Flags]
	[Serializable]
	public enum MigrationFeature
	{
		// Token: 0x0400361C RID: 13852
		[LocDescription(ServerStrings.IDs.MigrationFeatureNone)]
		None = 0,
		// Token: 0x0400361D RID: 13853
		[LocDescription(ServerStrings.IDs.MigrationFeatureMultiBatch)]
		MultiBatch = 1,
		// Token: 0x0400361E RID: 13854
		[LocDescription(ServerStrings.IDs.MigrationFeatureEndpoints)]
		Endpoints = 2,
		// Token: 0x0400361F RID: 13855
		[LocDescription(ServerStrings.IDs.MigrationFeatureUpgradeBlock)]
		UpgradeBlock = 4,
		// Token: 0x04003620 RID: 13856
		[LocDescription(ServerStrings.IDs.MigrationFeaturePAW)]
		PAW = 8
	}
}

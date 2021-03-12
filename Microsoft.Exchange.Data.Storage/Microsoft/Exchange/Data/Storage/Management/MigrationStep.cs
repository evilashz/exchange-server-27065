using System;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A3C RID: 2620
	[Serializable]
	public enum MigrationStep
	{
		// Token: 0x0400366F RID: 13935
		[LocDescription(ServerStrings.IDs.MigrationStepInitialization)]
		Initialization = 2,
		// Token: 0x04003670 RID: 13936
		[LocDescription(ServerStrings.IDs.MigrationStepProvisioning)]
		Provisioning = 7,
		// Token: 0x04003671 RID: 13937
		[LocDescription(ServerStrings.IDs.MigrationStepProvisioningUpdate)]
		ProvisioningUpdate = 12,
		// Token: 0x04003672 RID: 13938
		[LocDescription(ServerStrings.IDs.MigrationStepDataMigration)]
		DataMigration = 17
	}
}

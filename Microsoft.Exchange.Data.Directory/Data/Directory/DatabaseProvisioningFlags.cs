using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000103 RID: 259
	internal enum DatabaseProvisioningFlags
	{
		// Token: 0x04000597 RID: 1431
		None,
		// Token: 0x04000598 RID: 1432
		ReservedFlag,
		// Token: 0x04000599 RID: 1433
		IsExcludedFromProvisioning,
		// Token: 0x0400059A RID: 1434
		IsSuspendedFromProvisioning = 4,
		// Token: 0x0400059B RID: 1435
		IsOutOfService = 8,
		// Token: 0x0400059C RID: 1436
		IsExcludedFromInitialProvisioning = 16,
		// Token: 0x0400059D RID: 1437
		IsExcludedFromProvisioningBySpaceMonitoring = 32,
		// Token: 0x0400059E RID: 1438
		IsExcludedFromProvisioningBySchemaVersionMonitoring = 64
	}
}

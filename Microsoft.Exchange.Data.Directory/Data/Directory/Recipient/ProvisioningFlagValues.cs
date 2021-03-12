using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x0200023B RID: 571
	[Flags]
	internal enum ProvisioningFlagValues
	{
		// Token: 0x04000D3D RID: 3389
		None = 0,
		// Token: 0x04000D3E RID: 3390
		UMProvisioningFlag = 2,
		// Token: 0x04000D3F RID: 3391
		IsDefault_R3 = 4,
		// Token: 0x04000D40 RID: 3392
		IsDefault = 8,
		// Token: 0x04000D41 RID: 3393
		CurrentRelease = 16,
		// Token: 0x04000D42 RID: 3394
		NonCurrentRelease = 32,
		// Token: 0x04000D43 RID: 3395
		SKUAssigned = 64,
		// Token: 0x04000D44 RID: 3396
		SKURemoved = 128,
		// Token: 0x04000D45 RID: 3397
		UCSImListMigrationCompletedFlag = 256,
		// Token: 0x04000D46 RID: 3398
		IsPilotMailboxPlan = 512,
		// Token: 0x04000D47 RID: 3399
		IsExcludedFromServingHierarchy = 1024,
		// Token: 0x04000D48 RID: 3400
		LEOEnabled = 2048,
		// Token: 0x04000D49 RID: 3401
		ModernGroupTypeLowerFlag = 4096,
		// Token: 0x04000D4A RID: 3402
		ModernGroupTypeUpperFlag = 8192,
		// Token: 0x04000D4B RID: 3403
		IsGroupMailboxConfigured = 16384,
		// Token: 0x04000D4C RID: 3404
		GroupMailboxExternalResourcesSet = 32768,
		// Token: 0x04000D4D RID: 3405
		IsUCCPolicyUsedForAudit = 65536,
		// Token: 0x04000D4E RID: 3406
		AuxMailbox = 131072,
		// Token: 0x04000D4F RID: 3407
		AutoSubscribeNewGroupMembers = 262144,
		// Token: 0x04000D50 RID: 3408
		IsHierarchyReady = 524288
	}
}

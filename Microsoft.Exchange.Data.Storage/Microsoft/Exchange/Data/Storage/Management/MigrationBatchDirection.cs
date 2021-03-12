using System;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A1F RID: 2591
	[Serializable]
	public enum MigrationBatchDirection
	{
		// Token: 0x040035B0 RID: 13744
		[LocDescription(ServerStrings.IDs.MigrationBatchDirectionLocal)]
		Local = 1,
		// Token: 0x040035B1 RID: 13745
		[LocDescription(ServerStrings.IDs.MigrationBatchDirectionOnboarding)]
		Onboarding,
		// Token: 0x040035B2 RID: 13746
		[LocDescription(ServerStrings.IDs.MigrationBatchDirectionOffboarding)]
		Offboarding = 4
	}
}

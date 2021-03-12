using System;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A4B RID: 2635
	[Serializable]
	public enum TestMigrationServerAvailabilityResult
	{
		// Token: 0x040036DC RID: 14044
		[LocDescription(ServerStrings.IDs.MigrationTestMSASuccess)]
		Success,
		// Token: 0x040036DD RID: 14045
		[LocDescription(ServerStrings.IDs.MigrationTestMSAFailed)]
		Failed,
		// Token: 0x040036DE RID: 14046
		[LocDescription(ServerStrings.IDs.MigrationTestMSAWarning)]
		Warning
	}
}

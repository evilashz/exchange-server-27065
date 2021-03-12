using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002C5 RID: 709
	[Flags]
	public enum OrganizationMigrationFlags
	{
		// Token: 0x0400139A RID: 5018
		None = 0,
		// Token: 0x0400139B RID: 5019
		IsExcludedFromOnboardMigration = 1,
		// Token: 0x0400139C RID: 5020
		IsExcludedFromOffboardMigration = 2,
		// Token: 0x0400139D RID: 5021
		IsFfoMigrationInProgress = 4
	}
}

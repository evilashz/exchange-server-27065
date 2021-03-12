using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002D8 RID: 728
	public enum OrganizationUpgradeStage
	{
		// Token: 0x04001550 RID: 5456
		StartUpgrade,
		// Token: 0x04001551 RID: 5457
		UpgradeOrganizationMailboxes,
		// Token: 0x04001552 RID: 5458
		UpgradeUserMailboxes,
		// Token: 0x04001553 RID: 5459
		CompleteUpgrade
	}
}

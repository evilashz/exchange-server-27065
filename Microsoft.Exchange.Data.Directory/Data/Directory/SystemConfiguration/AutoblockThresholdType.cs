using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000301 RID: 769
	public enum AutoblockThresholdType
	{
		// Token: 0x04001630 RID: 5680
		[LocDescription(DirectoryStrings.IDs.UserAgentsChanges)]
		UserAgentsChanges,
		// Token: 0x04001631 RID: 5681
		[LocDescription(DirectoryStrings.IDs.RecentCommands)]
		RecentCommands,
		// Token: 0x04001632 RID: 5682
		[LocDescription(DirectoryStrings.IDs.Watsons)]
		Watsons,
		// Token: 0x04001633 RID: 5683
		[LocDescription(DirectoryStrings.IDs.OutOfBudgets)]
		OutOfBudgets,
		// Token: 0x04001634 RID: 5684
		[LocDescription(DirectoryStrings.IDs.SyncCommands)]
		SyncCommands,
		// Token: 0x04001635 RID: 5685
		[LocDescription(DirectoryStrings.IDs.EnableNotificationEmail)]
		EnableNotificationEmail,
		// Token: 0x04001636 RID: 5686
		[LocDescription(DirectoryStrings.IDs.EnableNotificationEmail)]
		CommandFrequency
	}
}

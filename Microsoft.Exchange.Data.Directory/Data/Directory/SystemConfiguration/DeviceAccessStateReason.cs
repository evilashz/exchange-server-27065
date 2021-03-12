using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004EE RID: 1262
	public enum DeviceAccessStateReason
	{
		// Token: 0x04002621 RID: 9761
		[LocDescription(DirectoryStrings.IDs.Unknown)]
		Unknown,
		// Token: 0x04002622 RID: 9762
		[LocDescription(DirectoryStrings.IDs.Global)]
		Global,
		// Token: 0x04002623 RID: 9763
		[LocDescription(DirectoryStrings.IDs.Individual)]
		Individual,
		// Token: 0x04002624 RID: 9764
		[LocDescription(DirectoryStrings.IDs.DeviceRule)]
		DeviceRule,
		// Token: 0x04002625 RID: 9765
		[LocDescription(DirectoryStrings.IDs.Upgrade)]
		Upgrade,
		// Token: 0x04002626 RID: 9766
		[LocDescription(DirectoryStrings.IDs.Policy)]
		Policy,
		// Token: 0x04002627 RID: 9767
		[LocDescription(DirectoryStrings.IDs.UserAgentsChanges)]
		UserAgentsChanges,
		// Token: 0x04002628 RID: 9768
		[LocDescription(DirectoryStrings.IDs.RecentCommands)]
		RecentCommands,
		// Token: 0x04002629 RID: 9769
		[LocDescription(DirectoryStrings.IDs.Watsons)]
		Watsons,
		// Token: 0x0400262A RID: 9770
		[LocDescription(DirectoryStrings.IDs.OutOfBudgets)]
		OutOfBudgets,
		// Token: 0x0400262B RID: 9771
		[LocDescription(DirectoryStrings.IDs.SyncCommands)]
		SyncCommands,
		// Token: 0x0400262C RID: 9772
		[LocDescription(DirectoryStrings.IDs.EnableNotificationEmail)]
		EnableNotificationEmail,
		// Token: 0x0400262D RID: 9773
		[LocDescription(DirectoryStrings.IDs.EnableNotificationEmail)]
		CommandFrequency,
		// Token: 0x0400262E RID: 9774
		[LocDescription(DirectoryStrings.IDs.ExternalMdm)]
		ExternallyManaged = 51,
		// Token: 0x0400262F RID: 9775
		[LocDescription(DirectoryStrings.IDs.ExternalCompliance)]
		ExternalCompliance,
		// Token: 0x04002630 RID: 9776
		[LocDescription(DirectoryStrings.IDs.ExternalEnrollment)]
		ExternalEnrollment
	}
}

using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005EE RID: 1518
	[Flags]
	internal enum AutoAttendantFlagBits
	{
		// Token: 0x04003196 RID: 12694
		[LocDescription(DirectoryStrings.IDs.None)]
		None = 0,
		// Token: 0x04003197 RID: 12695
		[LocDescription(DirectoryStrings.IDs.Enabled)]
		Enabled = 1,
		// Token: 0x04003198 RID: 12696
		[LocDescription(DirectoryStrings.IDs.NameLookupEnabled)]
		NameLookupEnabled = 2,
		// Token: 0x04003199 RID: 12697
		[LocDescription(DirectoryStrings.IDs.InfoAnnouncementEnabled)]
		InfoAnnouncementEnabled = 4,
		// Token: 0x0400319A RID: 12698
		[LocDescription(DirectoryStrings.IDs.ForwardCallsToDefaultMailbox)]
		ForwardCallsToDefaultMailbox = 8,
		// Token: 0x0400319B RID: 12699
		[LocDescription(DirectoryStrings.IDs.StarOutToDialPlanEnabled)]
		StarOutToDialPlanEnabled = 16
	}
}

using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200052E RID: 1326
	public enum FreeBusyAccessLevel
	{
		// Token: 0x04002809 RID: 10249
		[LocDescription(DirectoryStrings.IDs.CalendarSharingFreeBusyNone)]
		None,
		// Token: 0x0400280A RID: 10250
		[LocDescription(DirectoryStrings.IDs.CalendarSharingFreeBusyAvailabilityOnly)]
		AvailabilityOnly,
		// Token: 0x0400280B RID: 10251
		[LocDescription(DirectoryStrings.IDs.CalendarSharingFreeBusyLimitedDetails)]
		LimitedDetails
	}
}

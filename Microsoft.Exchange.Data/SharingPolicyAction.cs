using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001A6 RID: 422
	[Flags]
	public enum SharingPolicyAction
	{
		// Token: 0x04000880 RID: 2176
		[LocDescription(DataStrings.IDs.CalendarSharingFreeBusySimple)]
		CalendarSharingFreeBusySimple = 1,
		// Token: 0x04000881 RID: 2177
		[LocDescription(DataStrings.IDs.CalendarSharingFreeBusyDetail)]
		CalendarSharingFreeBusyDetail = 2,
		// Token: 0x04000882 RID: 2178
		[LocDescription(DataStrings.IDs.CalendarSharingFreeBusyReviewer)]
		CalendarSharingFreeBusyReviewer = 4,
		// Token: 0x04000883 RID: 2179
		[LocDescription(DataStrings.IDs.ContactsSharing)]
		ContactsSharing = 8,
		// Token: 0x04000884 RID: 2180
		[LocDescription(DataStrings.IDs.MeetingLimitedDetails)]
		MeetingLimitedDetails = 16,
		// Token: 0x04000885 RID: 2181
		[LocDescription(DataStrings.IDs.MeetingFullDetails)]
		MeetingFullDetails = 32,
		// Token: 0x04000886 RID: 2182
		[LocDescription(DataStrings.IDs.MeetingFullDetailsWithAttendees)]
		MeetingFullDetailsWithAttendees = 64
	}
}

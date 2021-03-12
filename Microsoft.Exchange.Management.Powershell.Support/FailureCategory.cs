using System;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x02000003 RID: 3
	[Flags]
	public enum FailureCategory
	{
		// Token: 0x04000004 RID: 4
		None = 0,
		// Token: 0x04000005 RID: 5
		DuplicateMeetings = 1,
		// Token: 0x04000006 RID: 6
		WrongTime = 2,
		// Token: 0x04000007 RID: 7
		WrongLocation = 4,
		// Token: 0x04000008 RID: 8
		WrongTrackingStatus = 8,
		// Token: 0x04000009 RID: 9
		CorruptMeetings = 16,
		// Token: 0x0400000A RID: 10
		MissingMeetings = 32,
		// Token: 0x0400000B RID: 11
		RecurrenceProblems = 64,
		// Token: 0x0400000C RID: 12
		MailboxUnavailable = 128,
		// Token: 0x0400000D RID: 13
		All = 255
	}
}

using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200020B RID: 523
	internal enum MeetingMessageType
	{
		// Token: 0x04000EF1 RID: 3825
		None,
		// Token: 0x04000EF2 RID: 3826
		NewMeetingRequest,
		// Token: 0x04000EF3 RID: 3827
		FullUpdate = 65536,
		// Token: 0x04000EF4 RID: 3828
		InformationalUpdate = 131072,
		// Token: 0x04000EF5 RID: 3829
		SilentUpdate = 262144,
		// Token: 0x04000EF6 RID: 3830
		Outdated = 524288,
		// Token: 0x04000EF7 RID: 3831
		PrincipalWantsCopy = 1048576
	}
}

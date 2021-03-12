using System;

// Token: 0x020002B6 RID: 694
[Flags]
internal enum HybridFeatureFlags
{
	// Token: 0x0400130F RID: 4879
	None = 0,
	// Token: 0x04001310 RID: 4880
	FreeBusy = 1,
	// Token: 0x04001311 RID: 4881
	MoveMailbox = 2,
	// Token: 0x04001312 RID: 4882
	Mailtips = 4,
	// Token: 0x04001313 RID: 4883
	MessageTracking = 8,
	// Token: 0x04001314 RID: 4884
	OwaRedirection = 16,
	// Token: 0x04001315 RID: 4885
	OnlineArchive = 32,
	// Token: 0x04001316 RID: 4886
	SecureMail = 64,
	// Token: 0x04001317 RID: 4887
	CentralizedTransportOnPrem = 128,
	// Token: 0x04001318 RID: 4888
	CentralizedTransportInCloud = 256,
	// Token: 0x04001319 RID: 4889
	Photos = 512,
	// Token: 0x0400131A RID: 4890
	All = 1023
}

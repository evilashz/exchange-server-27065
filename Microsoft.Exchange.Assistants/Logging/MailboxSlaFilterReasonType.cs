using System;

namespace Microsoft.Exchange.Assistants.Logging
{
	// Token: 0x020000C3 RID: 195
	internal enum MailboxSlaFilterReasonType
	{
		// Token: 0x04000382 RID: 898
		None,
		// Token: 0x04000383 RID: 899
		NoGuid,
		// Token: 0x04000384 RID: 900
		NotInDirectory,
		// Token: 0x04000385 RID: 901
		MoveDestination,
		// Token: 0x04000386 RID: 902
		Inaccessible,
		// Token: 0x04000387 RID: 903
		Archive,
		// Token: 0x04000388 RID: 904
		NotUser,
		// Token: 0x04000389 RID: 905
		PublicFolder,
		// Token: 0x0400038A RID: 906
		InDemandJob
	}
}

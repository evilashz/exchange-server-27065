using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200023C RID: 572
	[Flags]
	internal enum ForwardCreationFlags
	{
		// Token: 0x0400110C RID: 4364
		None = 0,
		// Token: 0x0400110D RID: 4365
		PreserveSender = 1,
		// Token: 0x0400110E RID: 4366
		PreserveSubject = 2,
		// Token: 0x0400110F RID: 4367
		TreatAsMeetingMessage = 4,
		// Token: 0x04001110 RID: 4368
		ResourceDelegationMessage = 8
	}
}

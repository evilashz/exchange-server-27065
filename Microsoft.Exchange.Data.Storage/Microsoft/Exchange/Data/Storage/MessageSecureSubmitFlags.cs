using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200007E RID: 126
	[Flags]
	internal enum MessageSecureSubmitFlags
	{
		// Token: 0x0400025D RID: 605
		None = 0,
		// Token: 0x0400025E RID: 606
		ClientSubmittedSecurely = 1,
		// Token: 0x0400025F RID: 607
		ServerSubmittedSecurely = 2
	}
}

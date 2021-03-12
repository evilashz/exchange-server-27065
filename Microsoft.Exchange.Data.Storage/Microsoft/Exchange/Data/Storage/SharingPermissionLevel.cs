using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000262 RID: 610
	internal enum SharingPermissionLevel
	{
		// Token: 0x0400122E RID: 4654
		FreeBusy,
		// Token: 0x0400122F RID: 4655
		LimitedDetails = 10,
		// Token: 0x04001230 RID: 4656
		Read = 20,
		// Token: 0x04001231 RID: 4657
		ReadWrite = 30,
		// Token: 0x04001232 RID: 4658
		CoOwner = 40,
		// Token: 0x04001233 RID: 4659
		Owner = 50
	}
}

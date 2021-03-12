using System;

namespace Microsoft.Exchange.Data.Mapi
{
	// Token: 0x02000012 RID: 18
	[Flags]
	public enum ObjectClass
	{
		// Token: 0x04000036 RID: 54
		Unknown = 0,
		// Token: 0x04000037 RID: 55
		Mailbox = 1,
		// Token: 0x04000038 RID: 56
		Gateway = 2,
		// Token: 0x04000039 RID: 57
		DistributionGroup = 4,
		// Token: 0x0400003A RID: 58
		PublicFolder = 8,
		// Token: 0x0400003B RID: 59
		UserDisabled = 16,
		// Token: 0x0400003C RID: 60
		ExOleDbSystemMailbox = 32,
		// Token: 0x0400003D RID: 61
		SystemAttendantMailbox = 64
	}
}

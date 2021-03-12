using System;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000034 RID: 52
	[Flags]
	public enum RecipientType
	{
		// Token: 0x040002F7 RID: 759
		Orig = 0,
		// Token: 0x040002F8 RID: 760
		To = 1,
		// Token: 0x040002F9 RID: 761
		Cc = 2,
		// Token: 0x040002FA RID: 762
		Bcc = 3,
		// Token: 0x040002FB RID: 763
		P1 = 268435456,
		// Token: 0x040002FC RID: 764
		Submitted = -2147483648,
		// Token: 0x040002FD RID: 765
		SubmittedTo = -2147483647,
		// Token: 0x040002FE RID: 766
		SubmittedCc = -2147483646,
		// Token: 0x040002FF RID: 767
		SubmittedBcc = -2147483645,
		// Token: 0x04000300 RID: 768
		Unknown = -1
	}
}

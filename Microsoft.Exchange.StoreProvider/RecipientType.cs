using System;

namespace Microsoft.Mapi
{
	// Token: 0x02000211 RID: 529
	internal enum RecipientType
	{
		// Token: 0x04000F76 RID: 3958
		Orig,
		// Token: 0x04000F77 RID: 3959
		To,
		// Token: 0x04000F78 RID: 3960
		Cc,
		// Token: 0x04000F79 RID: 3961
		Bcc,
		// Token: 0x04000F7A RID: 3962
		P1 = 268435456,
		// Token: 0x04000F7B RID: 3963
		Submitted = -2147483648,
		// Token: 0x04000F7C RID: 3964
		Unknown = -1
	}
}

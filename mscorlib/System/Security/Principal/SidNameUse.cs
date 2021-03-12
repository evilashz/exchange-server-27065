using System;

namespace System.Security.Principal
{
	// Token: 0x0200030C RID: 780
	internal enum SidNameUse
	{
		// Token: 0x04000FE7 RID: 4071
		User = 1,
		// Token: 0x04000FE8 RID: 4072
		Group,
		// Token: 0x04000FE9 RID: 4073
		Domain,
		// Token: 0x04000FEA RID: 4074
		Alias,
		// Token: 0x04000FEB RID: 4075
		WellKnownGroup,
		// Token: 0x04000FEC RID: 4076
		DeletedAccount,
		// Token: 0x04000FED RID: 4077
		Invalid,
		// Token: 0x04000FEE RID: 4078
		Unknown,
		// Token: 0x04000FEF RID: 4079
		Computer
	}
}

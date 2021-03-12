using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000162 RID: 354
	[Flags]
	internal enum RestrictedHeaderSet
	{
		// Token: 0x040007CA RID: 1994
		None = 0,
		// Token: 0x040007CB RID: 1995
		Organization = 1,
		// Token: 0x040007CC RID: 1996
		Forest = 2,
		// Token: 0x040007CD RID: 1997
		MTA = 4,
		// Token: 0x040007CE RID: 1998
		All = -1
	}
}

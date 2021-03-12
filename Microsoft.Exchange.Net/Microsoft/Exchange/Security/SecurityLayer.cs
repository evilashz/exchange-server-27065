using System;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000C77 RID: 3191
	[Flags]
	internal enum SecurityLayer : byte
	{
		// Token: 0x04003B34 RID: 15156
		None = 0,
		// Token: 0x04003B35 RID: 15157
		NoSecurity = 1,
		// Token: 0x04003B36 RID: 15158
		Integrity = 2,
		// Token: 0x04003B37 RID: 15159
		Privacy = 4
	}
}

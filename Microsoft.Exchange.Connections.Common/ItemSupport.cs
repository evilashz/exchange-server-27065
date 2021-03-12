using System;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x0200000F RID: 15
	[Flags]
	internal enum ItemSupport
	{
		// Token: 0x04000035 RID: 53
		None = 0,
		// Token: 0x04000036 RID: 54
		Email = 1,
		// Token: 0x04000037 RID: 55
		Contacts = 2,
		// Token: 0x04000038 RID: 56
		Generic = 32
	}
}

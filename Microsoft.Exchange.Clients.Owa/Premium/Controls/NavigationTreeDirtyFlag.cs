using System;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003D0 RID: 976
	[Flags]
	internal enum NavigationTreeDirtyFlag
	{
		// Token: 0x0400192B RID: 6443
		None = 0,
		// Token: 0x0400192C RID: 6444
		Favorites = 1,
		// Token: 0x0400192D RID: 6445
		Calendar = 2,
		// Token: 0x0400192E RID: 6446
		Contact = 4,
		// Token: 0x0400192F RID: 6447
		Task = 8
	}
}

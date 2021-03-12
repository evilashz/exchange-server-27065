using System;

namespace Microsoft.Exchange.Search.Mdb
{
	// Token: 0x0200002A RID: 42
	internal enum NotificationType
	{
		// Token: 0x040000DC RID: 220
		Uninteresting,
		// Token: 0x040000DD RID: 221
		Insert,
		// Token: 0x040000DE RID: 222
		Update,
		// Token: 0x040000DF RID: 223
		Delete,
		// Token: 0x040000E0 RID: 224
		Move,
		// Token: 0x040000E1 RID: 225
		ReadFlagChange,
		// Token: 0x040000E2 RID: 226
		DeleteMailbox
	}
}

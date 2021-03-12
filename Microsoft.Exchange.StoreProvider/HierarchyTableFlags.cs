using System;

namespace Microsoft.Mapi
{
	// Token: 0x02000081 RID: 129
	[Flags]
	internal enum HierarchyTableFlags
	{
		// Token: 0x040004E1 RID: 1249
		None = 0,
		// Token: 0x040004E2 RID: 1250
		ConvenientDepth = 1,
		// Token: 0x040004E3 RID: 1251
		ShowSoftDeletes = 2,
		// Token: 0x040004E4 RID: 1252
		NoNotifications = 32,
		// Token: 0x040004E5 RID: 1253
		SuppressNotificationsOnMyActions = 4096,
		// Token: 0x040004E6 RID: 1254
		DeferredErrors = 8,
		// Token: 0x040004E7 RID: 1255
		Unicode = -2147483648
	}
}

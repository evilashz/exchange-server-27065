using System;

namespace Microsoft.Mapi
{
	// Token: 0x02000082 RID: 130
	[Flags]
	internal enum ContentsTableFlags
	{
		// Token: 0x040004E9 RID: 1257
		None = 0,
		// Token: 0x040004EA RID: 1258
		ShowSoftDeletes = 2,
		// Token: 0x040004EB RID: 1259
		ShowConversations = 256,
		// Token: 0x040004EC RID: 1260
		ShowConversationMembers = 512,
		// Token: 0x040004ED RID: 1261
		RetrieveFromIndex = 1024,
		// Token: 0x040004EE RID: 1262
		NoNotifications = 32,
		// Token: 0x040004EF RID: 1263
		Associated = 64,
		// Token: 0x040004F0 RID: 1264
		DeferredErrors = 8,
		// Token: 0x040004F1 RID: 1265
		Unicode = -2147483648,
		// Token: 0x040004F2 RID: 1266
		DocumentIdView = 2048,
		// Token: 0x040004F3 RID: 1267
		ExpandedConversationView = 8192,
		// Token: 0x040004F4 RID: 1268
		PrereadExtendedProperties = 16384
	}
}

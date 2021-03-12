using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000214 RID: 532
	[Flags]
	internal enum ItemQueryType
	{
		// Token: 0x04000F5C RID: 3932
		None = 0,
		// Token: 0x04000F5D RID: 3933
		Associated = 1,
		// Token: 0x04000F5E RID: 3934
		SoftDeleted = 2,
		// Token: 0x04000F5F RID: 3935
		RetrieveFromIndex = 4,
		// Token: 0x04000F60 RID: 3936
		ConversationViewMembers = 8,
		// Token: 0x04000F61 RID: 3937
		ConversationView = 16,
		// Token: 0x04000F62 RID: 3938
		DocumentIdView = 32,
		// Token: 0x04000F63 RID: 3939
		PrereadExtendedProperties = 64,
		// Token: 0x04000F64 RID: 3940
		NoNotifications = 128
	}
}

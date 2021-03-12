using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000026 RID: 38
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class Constants
	{
		// Token: 0x04000105 RID: 261
		internal const int Unicode = -2147483648;

		// Token: 0x04000106 RID: 262
		internal const int Associated = 64;

		// Token: 0x04000107 RID: 263
		internal const int Create = 2;

		// Token: 0x04000108 RID: 264
		internal const int ShowSoftDeletes = 2;

		// Token: 0x04000109 RID: 265
		internal const int NoNotifications = 32;

		// Token: 0x0400010A RID: 266
		internal const int ShowConversations = 256;

		// Token: 0x0400010B RID: 267
		internal const int ShowConversationMembers = 512;

		// Token: 0x0400010C RID: 268
		internal const int RetrieveFromIndex = 1024;

		// Token: 0x0400010D RID: 269
		internal const int DocumentIdView = 2048;

		// Token: 0x0400010E RID: 270
		internal const int ExpandedConversationView = 8192;

		// Token: 0x0400010F RID: 271
		internal const int PrereadExtendedProperties = 16384;

		// Token: 0x04000110 RID: 272
		internal const int Modify = 1;

		// Token: 0x04000111 RID: 273
		internal const int DeferredErrors = 8;

		// Token: 0x04000112 RID: 274
		internal const int ReadOnly = 16;

		// Token: 0x04000113 RID: 275
		internal const int BestAccess = 16;

		// Token: 0x04000114 RID: 276
		internal const int ForceUnicode = 1024;

		// Token: 0x04000115 RID: 277
		internal const int BestBody = 8192;

		// Token: 0x04000116 RID: 278
		internal const int ContentAggregation = 1;

		// Token: 0x04000117 RID: 279
		internal const int StandardChainedRpcBufferSize = 98304;

		// Token: 0x04000118 RID: 280
		internal const int MaximumChainedRpcBufferSize = 262144;
	}
}

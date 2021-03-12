using System;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x02000008 RID: 8
	internal interface ISplitter
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000027 RID: 39
		bool OnePass { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000028 RID: 40
		PartType PartType { get; }

		// Token: 0x06000029 RID: 41
		BookmarkRetriever Split(string text);
	}
}

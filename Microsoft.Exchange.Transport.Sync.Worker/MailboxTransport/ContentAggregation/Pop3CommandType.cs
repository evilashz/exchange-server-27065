using System;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x020001F1 RID: 497
	internal enum Pop3CommandType
	{
		// Token: 0x04000937 RID: 2359
		Quit,
		// Token: 0x04000938 RID: 2360
		Stat,
		// Token: 0x04000939 RID: 2361
		List,
		// Token: 0x0400093A RID: 2362
		Retr,
		// Token: 0x0400093B RID: 2363
		Dele,
		// Token: 0x0400093C RID: 2364
		Noop,
		// Token: 0x0400093D RID: 2365
		Rset,
		// Token: 0x0400093E RID: 2366
		Top,
		// Token: 0x0400093F RID: 2367
		Uidl,
		// Token: 0x04000940 RID: 2368
		User,
		// Token: 0x04000941 RID: 2369
		Pass,
		// Token: 0x04000942 RID: 2370
		Auth,
		// Token: 0x04000943 RID: 2371
		Blob,
		// Token: 0x04000944 RID: 2372
		Capa,
		// Token: 0x04000945 RID: 2373
		Stls
	}
}

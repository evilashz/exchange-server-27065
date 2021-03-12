using System;

namespace Microsoft.Exchange.Management.Analysis.Features
{
	// Token: 0x02000066 RID: 102
	[Flags]
	public enum SetupRole
	{
		// Token: 0x0400015A RID: 346
		None = 0,
		// Token: 0x0400015B RID: 347
		AdminTools = 1,
		// Token: 0x0400015C RID: 348
		Mailbox = 2,
		// Token: 0x0400015D RID: 349
		Bridgehead = 4,
		// Token: 0x0400015E RID: 350
		ClientAccess = 8,
		// Token: 0x0400015F RID: 351
		UnifiedMessaging = 16,
		// Token: 0x04000160 RID: 352
		Gateway = 32,
		// Token: 0x04000161 RID: 353
		Cafe = 64,
		// Token: 0x04000162 RID: 354
		Global = 128,
		// Token: 0x04000163 RID: 355
		LanguagePacks = 256,
		// Token: 0x04000164 RID: 356
		UmLanguagePack = 512,
		// Token: 0x04000165 RID: 357
		FrontendTransport = 1024,
		// Token: 0x04000166 RID: 358
		All = 2047
	}
}

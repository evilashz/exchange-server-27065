using System;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x0200005E RID: 94
	internal enum RegSAM : uint
	{
		// Token: 0x04000159 RID: 345
		None,
		// Token: 0x0400015A RID: 346
		QueryValue,
		// Token: 0x0400015B RID: 347
		SetValue,
		// Token: 0x0400015C RID: 348
		CreateSubKey = 4U,
		// Token: 0x0400015D RID: 349
		EnumerateSubKeys = 8U,
		// Token: 0x0400015E RID: 350
		Notify = 16U,
		// Token: 0x0400015F RID: 351
		CreateLink = 32U,
		// Token: 0x04000160 RID: 352
		WOW64_32Key = 512U,
		// Token: 0x04000161 RID: 353
		WOW64_64Key = 256U,
		// Token: 0x04000162 RID: 354
		WOW64_Res = 768U,
		// Token: 0x04000163 RID: 355
		Read = 131097U,
		// Token: 0x04000164 RID: 356
		Write = 131078U,
		// Token: 0x04000165 RID: 357
		Execute = 131097U,
		// Token: 0x04000166 RID: 358
		AllAccess = 983103U
	}
}

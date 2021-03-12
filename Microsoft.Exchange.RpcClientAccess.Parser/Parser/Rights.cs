using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000088 RID: 136
	[Flags]
	internal enum Rights : uint
	{
		// Token: 0x040001CF RID: 463
		None = 0U,
		// Token: 0x040001D0 RID: 464
		ReadAny = 1U,
		// Token: 0x040001D1 RID: 465
		Create = 2U,
		// Token: 0x040001D2 RID: 466
		EditOwned = 8U,
		// Token: 0x040001D3 RID: 467
		DeleteOwned = 16U,
		// Token: 0x040001D4 RID: 468
		EditAny = 32U,
		// Token: 0x040001D5 RID: 469
		DeleteAny = 64U,
		// Token: 0x040001D6 RID: 470
		CreateSubfolder = 128U,
		// Token: 0x040001D7 RID: 471
		Owner = 256U,
		// Token: 0x040001D8 RID: 472
		Contact = 512U,
		// Token: 0x040001D9 RID: 473
		Visible = 1024U,
		// Token: 0x040001DA RID: 474
		FreeBusySimple = 2048U,
		// Token: 0x040001DB RID: 475
		FreeBusyDetailed = 4096U
	}
}

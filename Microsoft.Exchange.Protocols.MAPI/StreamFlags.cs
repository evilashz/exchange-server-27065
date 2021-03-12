using System;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x0200004C RID: 76
	[Flags]
	public enum StreamFlags
	{
		// Token: 0x04000128 RID: 296
		AllowCreate = 1,
		// Token: 0x04000129 RID: 297
		AllowAppend = 2,
		// Token: 0x0400012A RID: 298
		AllowRead = 4,
		// Token: 0x0400012B RID: 299
		AllowWrite = 8
	}
}

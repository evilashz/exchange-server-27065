using System;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000049 RID: 73
	[Flags]
	public enum MapiCopyToFlags
	{
		// Token: 0x04000115 RID: 277
		Move = 1,
		// Token: 0x04000116 RID: 278
		NoReplace = 2,
		// Token: 0x04000117 RID: 279
		DeclineOk = 4,
		// Token: 0x04000118 RID: 280
		Dialog = 8
	}
}

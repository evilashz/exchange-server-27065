using System;

namespace Microsoft.Exchange.Transport.Sync.Common.Interop
{
	// Token: 0x02000079 RID: 121
	[Flags]
	internal enum MLDETECTCP
	{
		// Token: 0x0400019E RID: 414
		MLDETECTCP_NONE = 0,
		// Token: 0x0400019F RID: 415
		MLDETECTCP_7BIT = 1,
		// Token: 0x040001A0 RID: 416
		MLDETECTCP_8BIT = 2,
		// Token: 0x040001A1 RID: 417
		MLDETECTCP_DBCS = 4,
		// Token: 0x040001A2 RID: 418
		MLDETECTCP_HTML = 8,
		// Token: 0x040001A3 RID: 419
		MLDETECTCP_NUMBER = 16
	}
}

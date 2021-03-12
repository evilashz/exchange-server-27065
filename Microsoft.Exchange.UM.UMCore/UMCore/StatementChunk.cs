using System;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001E5 RID: 485
	internal class StatementChunk
	{
		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000E3E RID: 3646 RVA: 0x00040507 File Offset: 0x0003E707
		// (set) Token: 0x06000E3F RID: 3647 RVA: 0x0004050F File Offset: 0x0003E70F
		public ChunkType Type { get; set; }

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000E40 RID: 3648 RVA: 0x00040518 File Offset: 0x0003E718
		// (set) Token: 0x06000E41 RID: 3649 RVA: 0x00040520 File Offset: 0x0003E720
		public object Value { get; set; }
	}
}

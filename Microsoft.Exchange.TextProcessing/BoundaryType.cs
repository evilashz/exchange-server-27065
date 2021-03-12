using System;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x02000006 RID: 6
	public enum BoundaryType : byte
	{
		// Token: 0x0400001E RID: 30
		None,
		// Token: 0x0400001F RID: 31
		Normal,
		// Token: 0x04000020 RID: 32
		NormalLeftOnly,
		// Token: 0x04000021 RID: 33
		NormalRightOnly,
		// Token: 0x04000022 RID: 34
		Url,
		// Token: 0x04000023 RID: 35
		FullUrl
	}
}

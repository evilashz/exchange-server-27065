using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000F6A RID: 3946
	public enum TokenKind
	{
		// Token: 0x0400352B RID: 13611
		StartTag,
		// Token: 0x0400352C RID: 13612
		EndTag,
		// Token: 0x0400352D RID: 13613
		Text,
		// Token: 0x0400352E RID: 13614
		EmptyTag,
		// Token: 0x0400352F RID: 13615
		OverlappedClose,
		// Token: 0x04003530 RID: 13616
		OverlappedReopen,
		// Token: 0x04003531 RID: 13617
		IgnorableTag
	}
}

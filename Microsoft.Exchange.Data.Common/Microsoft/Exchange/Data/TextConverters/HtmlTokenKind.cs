using System;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x02000225 RID: 549
	public enum HtmlTokenKind
	{
		// Token: 0x0400193E RID: 6462
		Text,
		// Token: 0x0400193F RID: 6463
		StartTag,
		// Token: 0x04001940 RID: 6464
		EndTag,
		// Token: 0x04001941 RID: 6465
		EmptyElementTag,
		// Token: 0x04001942 RID: 6466
		SpecialTag,
		// Token: 0x04001943 RID: 6467
		OverlappedClose,
		// Token: 0x04001944 RID: 6468
		OverlappedReopen
	}
}

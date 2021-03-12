using System;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Html
{
	// Token: 0x0200021F RID: 543
	internal enum HtmlTokenId : byte
	{
		// Token: 0x04001909 RID: 6409
		None,
		// Token: 0x0400190A RID: 6410
		EndOfFile,
		// Token: 0x0400190B RID: 6411
		Text,
		// Token: 0x0400190C RID: 6412
		EncodingChange,
		// Token: 0x0400190D RID: 6413
		Tag,
		// Token: 0x0400190E RID: 6414
		Restart,
		// Token: 0x0400190F RID: 6415
		OverlappedClose,
		// Token: 0x04001910 RID: 6416
		OverlappedReopen,
		// Token: 0x04001911 RID: 6417
		InjectionBegin,
		// Token: 0x04001912 RID: 6418
		InjectionEnd
	}
}

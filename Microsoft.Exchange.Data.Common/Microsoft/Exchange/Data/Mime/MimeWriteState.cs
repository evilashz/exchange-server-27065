using System;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x02000079 RID: 121
	internal enum MimeWriteState
	{
		// Token: 0x04000385 RID: 901
		Initial,
		// Token: 0x04000386 RID: 902
		Complete,
		// Token: 0x04000387 RID: 903
		StartPart,
		// Token: 0x04000388 RID: 904
		Headers,
		// Token: 0x04000389 RID: 905
		Parameters,
		// Token: 0x0400038A RID: 906
		Recipients,
		// Token: 0x0400038B RID: 907
		GroupRecipients,
		// Token: 0x0400038C RID: 908
		PartContent,
		// Token: 0x0400038D RID: 909
		EndPart
	}
}

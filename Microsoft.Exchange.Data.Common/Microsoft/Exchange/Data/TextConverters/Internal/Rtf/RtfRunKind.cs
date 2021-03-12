using System;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Rtf
{
	// Token: 0x0200025C RID: 604
	internal enum RtfRunKind : ushort
	{
		// Token: 0x04001DE7 RID: 7655
		Ignore,
		// Token: 0x04001DE8 RID: 7656
		EndOfFile = 4096,
		// Token: 0x04001DE9 RID: 7657
		Begin = 8192,
		// Token: 0x04001DEA RID: 7658
		End = 12288,
		// Token: 0x04001DEB RID: 7659
		Binary = 16384,
		// Token: 0x04001DEC RID: 7660
		SingleRunLast = 16384,
		// Token: 0x04001DED RID: 7661
		Keyword = 20480,
		// Token: 0x04001DEE RID: 7662
		Text = 28672,
		// Token: 0x04001DEF RID: 7663
		Escape = 32768,
		// Token: 0x04001DF0 RID: 7664
		Unicode = 36864,
		// Token: 0x04001DF1 RID: 7665
		Zero = 40960,
		// Token: 0x04001DF2 RID: 7666
		Mask = 61440
	}
}

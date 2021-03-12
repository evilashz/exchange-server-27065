using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005C4 RID: 1476
	internal enum ByteEncoderTypeFor7BitCharsets
	{
		// Token: 0x04002042 RID: 8258
		Use7Bit,
		// Token: 0x04002043 RID: 8259
		UseQP,
		// Token: 0x04002044 RID: 8260
		UseBase64,
		// Token: 0x04002045 RID: 8261
		UseQPHtmlDetectTextPlain = 5,
		// Token: 0x04002046 RID: 8262
		UseBase64HtmlDetectTextPlain,
		// Token: 0x04002047 RID: 8263
		UseQPHtml7BitTextPlain = 13,
		// Token: 0x04002048 RID: 8264
		UseBase64Html7BitTextPlain
	}
}

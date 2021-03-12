using System;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000282 RID: 642
	[Flags]
	internal enum EncodingFlags
	{
		// Token: 0x04000D11 RID: 3345
		Preference = 131072,
		// Token: 0x04000D12 RID: 3346
		Mime = 262144,
		// Token: 0x04000D13 RID: 3347
		MimeText = 393216,
		// Token: 0x04000D14 RID: 3348
		MimeHtml = 917504,
		// Token: 0x04000D15 RID: 3349
		MimeHtmlText = 1441792,
		// Token: 0x04000D16 RID: 3350
		UUEncode = 2228224,
		// Token: 0x04000D17 RID: 3351
		UUEncodeBinHex = 131072
	}
}

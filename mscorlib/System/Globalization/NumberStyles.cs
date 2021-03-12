using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	// Token: 0x020003AD RID: 941
	[Flags]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum NumberStyles
	{
		// Token: 0x040014B7 RID: 5303
		[__DynamicallyInvokable]
		None = 0,
		// Token: 0x040014B8 RID: 5304
		[__DynamicallyInvokable]
		AllowLeadingWhite = 1,
		// Token: 0x040014B9 RID: 5305
		[__DynamicallyInvokable]
		AllowTrailingWhite = 2,
		// Token: 0x040014BA RID: 5306
		[__DynamicallyInvokable]
		AllowLeadingSign = 4,
		// Token: 0x040014BB RID: 5307
		[__DynamicallyInvokable]
		AllowTrailingSign = 8,
		// Token: 0x040014BC RID: 5308
		[__DynamicallyInvokable]
		AllowParentheses = 16,
		// Token: 0x040014BD RID: 5309
		[__DynamicallyInvokable]
		AllowDecimalPoint = 32,
		// Token: 0x040014BE RID: 5310
		[__DynamicallyInvokable]
		AllowThousands = 64,
		// Token: 0x040014BF RID: 5311
		[__DynamicallyInvokable]
		AllowExponent = 128,
		// Token: 0x040014C0 RID: 5312
		[__DynamicallyInvokable]
		AllowCurrencySymbol = 256,
		// Token: 0x040014C1 RID: 5313
		[__DynamicallyInvokable]
		AllowHexSpecifier = 512,
		// Token: 0x040014C2 RID: 5314
		[__DynamicallyInvokable]
		Integer = 7,
		// Token: 0x040014C3 RID: 5315
		[__DynamicallyInvokable]
		HexNumber = 515,
		// Token: 0x040014C4 RID: 5316
		[__DynamicallyInvokable]
		Number = 111,
		// Token: 0x040014C5 RID: 5317
		[__DynamicallyInvokable]
		Float = 167,
		// Token: 0x040014C6 RID: 5318
		[__DynamicallyInvokable]
		Currency = 383,
		// Token: 0x040014C7 RID: 5319
		[__DynamicallyInvokable]
		Any = 511
	}
}

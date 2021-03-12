using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	// Token: 0x0200037F RID: 895
	[Flags]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum DateTimeStyles
	{
		// Token: 0x040012A7 RID: 4775
		[__DynamicallyInvokable]
		None = 0,
		// Token: 0x040012A8 RID: 4776
		[__DynamicallyInvokable]
		AllowLeadingWhite = 1,
		// Token: 0x040012A9 RID: 4777
		[__DynamicallyInvokable]
		AllowTrailingWhite = 2,
		// Token: 0x040012AA RID: 4778
		[__DynamicallyInvokable]
		AllowInnerWhite = 4,
		// Token: 0x040012AB RID: 4779
		[__DynamicallyInvokable]
		AllowWhiteSpaces = 7,
		// Token: 0x040012AC RID: 4780
		[__DynamicallyInvokable]
		NoCurrentDateDefault = 8,
		// Token: 0x040012AD RID: 4781
		[__DynamicallyInvokable]
		AdjustToUniversal = 16,
		// Token: 0x040012AE RID: 4782
		[__DynamicallyInvokable]
		AssumeLocal = 32,
		// Token: 0x040012AF RID: 4783
		[__DynamicallyInvokable]
		AssumeUniversal = 64,
		// Token: 0x040012B0 RID: 4784
		[__DynamicallyInvokable]
		RoundtripKind = 128
	}
}

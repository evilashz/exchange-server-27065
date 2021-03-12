using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001F5 RID: 501
	[Flags]
	internal enum BodyCharsetFlags
	{
		// Token: 0x04000DF5 RID: 3573
		None = 0,
		// Token: 0x04000DF6 RID: 3574
		DetectCharset = 0,
		// Token: 0x04000DF7 RID: 3575
		DisableCharsetDetection = 1,
		// Token: 0x04000DF8 RID: 3576
		CharsetDetectionMask = 65535,
		// Token: 0x04000DF9 RID: 3577
		PreferGB18030 = 65536,
		// Token: 0x04000DFA RID: 3578
		PreferIso885915 = 131072,
		// Token: 0x04000DFB RID: 3579
		PreserveUnicode = 262144,
		// Token: 0x04000DFC RID: 3580
		DoNotPreferIso2022jp = 1048576
	}
}

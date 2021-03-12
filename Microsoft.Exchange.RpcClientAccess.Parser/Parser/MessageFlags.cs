using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000359 RID: 857
	[Flags]
	internal enum MessageFlags
	{
		// Token: 0x04000AF1 RID: 2801
		None = 0,
		// Token: 0x04000AF2 RID: 2802
		Read = 1,
		// Token: 0x04000AF3 RID: 2803
		Unmodified = 2,
		// Token: 0x04000AF4 RID: 2804
		Submit = 4,
		// Token: 0x04000AF5 RID: 2805
		Unsent = 8,
		// Token: 0x04000AF6 RID: 2806
		HasAttach = 16,
		// Token: 0x04000AF7 RID: 2807
		FromMe = 32,
		// Token: 0x04000AF8 RID: 2808
		Associated = 64,
		// Token: 0x04000AF9 RID: 2809
		Resend = 128,
		// Token: 0x04000AFA RID: 2810
		RnPending = 256,
		// Token: 0x04000AFB RID: 2811
		NrnPending = 512,
		// Token: 0x04000AFC RID: 2812
		EverRead = 1024,
		// Token: 0x04000AFD RID: 2813
		Restricted = 2048,
		// Token: 0x04000AFE RID: 2814
		OriginX400 = 4096,
		// Token: 0x04000AFF RID: 2815
		OriginInternet = 8192,
		// Token: 0x04000B00 RID: 2816
		OriginMiscExt = 32768,
		// Token: 0x04000B01 RID: 2817
		OutlookNonEmsTransport = 65536
	}
}

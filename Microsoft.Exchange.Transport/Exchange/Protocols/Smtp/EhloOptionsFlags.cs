using System;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020003FE RID: 1022
	[Flags]
	internal enum EhloOptionsFlags
	{
		// Token: 0x04001703 RID: 5891
		None = 0,
		// Token: 0x04001704 RID: 5892
		AnonymousTls = 1,
		// Token: 0x04001705 RID: 5893
		UnusedCanBeRepurposed = 2,
		// Token: 0x04001706 RID: 5894
		BinaryMime = 4,
		// Token: 0x04001707 RID: 5895
		Chunking = 8,
		// Token: 0x04001708 RID: 5896
		Dsn = 16,
		// Token: 0x04001709 RID: 5897
		EightBitMime = 32,
		// Token: 0x0400170A RID: 5898
		EnhancedStatusCodes = 64,
		// Token: 0x0400170B RID: 5899
		Pipelining = 128,
		// Token: 0x0400170C RID: 5900
		StartTls = 256,
		// Token: 0x0400170D RID: 5901
		Xexch50 = 512,
		// Token: 0x0400170E RID: 5902
		XlongAddr = 1024,
		// Token: 0x0400170F RID: 5903
		Xoorg = 2048,
		// Token: 0x04001710 RID: 5904
		Xorar = 4096,
		// Token: 0x04001711 RID: 5905
		Xproxy = 8192,
		// Token: 0x04001712 RID: 5906
		Xrdst = 16384,
		// Token: 0x04001713 RID: 5907
		Xshadow = 32768,
		// Token: 0x04001714 RID: 5908
		XproxyFrom = 65536,
		// Token: 0x04001715 RID: 5909
		XshadowRequest = 131072,
		// Token: 0x04001716 RID: 5910
		XAdrc = 262144,
		// Token: 0x04001717 RID: 5911
		XExProps = 524288,
		// Token: 0x04001718 RID: 5912
		XproxyTo = 1048576,
		// Token: 0x04001719 RID: 5913
		XSessionMdbGuid = 2097152,
		// Token: 0x0400171A RID: 5914
		XAttr = 4194304,
		// Token: 0x0400171B RID: 5915
		XFastIndex = 8388608,
		// Token: 0x0400171C RID: 5916
		XSysProbe = 16777216,
		// Token: 0x0400171D RID: 5917
		XMsgId = 33554432,
		// Token: 0x0400171E RID: 5918
		XrsetProxyTo = 67108864,
		// Token: 0x0400171F RID: 5919
		SmtpUtf8 = 134217728,
		// Token: 0x04001720 RID: 5920
		XOrigFrom = 268435456,
		// Token: 0x04001721 RID: 5921
		XSessionType = 536870912,
		// Token: 0x04001722 RID: 5922
		AllFlags = 268435455
	}
}

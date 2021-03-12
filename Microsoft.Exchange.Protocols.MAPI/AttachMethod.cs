using System;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x0200000C RID: 12
	public enum AttachMethod
	{
		// Token: 0x04000046 RID: 70
		NoAttachment,
		// Token: 0x04000047 RID: 71
		AttachByValue,
		// Token: 0x04000048 RID: 72
		AttachByReference,
		// Token: 0x04000049 RID: 73
		AttachByRefResolve,
		// Token: 0x0400004A RID: 74
		AttachByRefOnly,
		// Token: 0x0400004B RID: 75
		AttachEmbeddedMsg,
		// Token: 0x0400004C RID: 76
		AttachOle
	}
}

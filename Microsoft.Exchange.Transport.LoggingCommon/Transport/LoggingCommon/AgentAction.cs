using System;

namespace Microsoft.Exchange.Transport.LoggingCommon
{
	// Token: 0x02000007 RID: 7
	internal enum AgentAction
	{
		// Token: 0x04000036 RID: 54
		RejectConnection = 1,
		// Token: 0x04000037 RID: 55
		Disconnect,
		// Token: 0x04000038 RID: 56
		RejectAuthentication,
		// Token: 0x04000039 RID: 57
		RejectCommand,
		// Token: 0x0400003A RID: 58
		RejectRecipients,
		// Token: 0x0400003B RID: 59
		RejectMessage,
		// Token: 0x0400003C RID: 60
		AcceptMessage,
		// Token: 0x0400003D RID: 61
		QuarantineRecipients,
		// Token: 0x0400003E RID: 62
		QuarantineMessage,
		// Token: 0x0400003F RID: 63
		DeleteRecipients,
		// Token: 0x04000040 RID: 64
		DeleteMessage,
		// Token: 0x04000041 RID: 65
		ModifyHeaders,
		// Token: 0x04000042 RID: 66
		StampScl,
		// Token: 0x04000043 RID: 67
		AttributionResult,
		// Token: 0x04000044 RID: 68
		OnPremiseInboundConnectorInfo,
		// Token: 0x04000045 RID: 69
		InvalidCertificate,
		// Token: 0x04000046 RID: 70
		AutoNukeRecipient
	}
}

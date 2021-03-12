using System;

namespace Microsoft.Exchange.Transport.LoggingCommon
{
	// Token: 0x02000008 RID: 8
	internal enum AgentLogField
	{
		// Token: 0x04000048 RID: 72
		Timestamp,
		// Token: 0x04000049 RID: 73
		SessionId,
		// Token: 0x0400004A RID: 74
		LocalEndpoint,
		// Token: 0x0400004B RID: 75
		RemoteEndpoint,
		// Token: 0x0400004C RID: 76
		EnteredOrgFromIP,
		// Token: 0x0400004D RID: 77
		MessageId,
		// Token: 0x0400004E RID: 78
		P1FromAddress,
		// Token: 0x0400004F RID: 79
		P2FromAddresses,
		// Token: 0x04000050 RID: 80
		Recipient,
		// Token: 0x04000051 RID: 81
		NumRecipients,
		// Token: 0x04000052 RID: 82
		Agent,
		// Token: 0x04000053 RID: 83
		Event,
		// Token: 0x04000054 RID: 84
		Action,
		// Token: 0x04000055 RID: 85
		SmtpResponse,
		// Token: 0x04000056 RID: 86
		Reason,
		// Token: 0x04000057 RID: 87
		ReasonData,
		// Token: 0x04000058 RID: 88
		Diagnostics,
		// Token: 0x04000059 RID: 89
		NetworkMsgID,
		// Token: 0x0400005A RID: 90
		TenantID,
		// Token: 0x0400005B RID: 91
		Directionality,
		// Token: 0x0400005C RID: 92
		NumFields
	}
}

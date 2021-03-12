using System;

namespace Microsoft.Exchange.Transport.Agent.InterceptorAgent
{
	// Token: 0x0200000D RID: 13
	public enum InterceptorAgentConditionType
	{
		// Token: 0x04000036 RID: 54
		Invalid,
		// Token: 0x04000037 RID: 55
		MessageSubject,
		// Token: 0x04000038 RID: 56
		EnvelopeFrom,
		// Token: 0x04000039 RID: 57
		EnvelopeTo,
		// Token: 0x0400003A RID: 58
		MessageId,
		// Token: 0x0400003B RID: 59
		HeaderName,
		// Token: 0x0400003C RID: 60
		HeaderValue,
		// Token: 0x0400003D RID: 61
		SmtpClientHostName,
		// Token: 0x0400003E RID: 62
		ProcessRole,
		// Token: 0x0400003F RID: 63
		ServerVersion,
		// Token: 0x04000040 RID: 64
		TenantId,
		// Token: 0x04000041 RID: 65
		Directionality,
		// Token: 0x04000042 RID: 66
		AccountForest
	}
}

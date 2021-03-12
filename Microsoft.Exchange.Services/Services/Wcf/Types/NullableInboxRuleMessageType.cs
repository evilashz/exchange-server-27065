using System;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AF0 RID: 2800
	public enum NullableInboxRuleMessageType
	{
		// Token: 0x04002C94 RID: 11412
		NullInboxRuleMessageType = -1,
		// Token: 0x04002C95 RID: 11413
		AutomaticReply,
		// Token: 0x04002C96 RID: 11414
		AutomaticForward,
		// Token: 0x04002C97 RID: 11415
		Encrypted,
		// Token: 0x04002C98 RID: 11416
		Calendaring,
		// Token: 0x04002C99 RID: 11417
		CalendaringResponse,
		// Token: 0x04002C9A RID: 11418
		PermissionControlled,
		// Token: 0x04002C9B RID: 11419
		Voicemail,
		// Token: 0x04002C9C RID: 11420
		Signed,
		// Token: 0x04002C9D RID: 11421
		ApprovalRequest,
		// Token: 0x04002C9E RID: 11422
		ReadReceipt,
		// Token: 0x04002C9F RID: 11423
		NonDeliveryReport
	}
}

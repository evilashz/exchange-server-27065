using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000251 RID: 593
	public enum InboxRuleMessageType
	{
		// Token: 0x040011D0 RID: 4560
		[LocDescription(ServerStrings.IDs.InboxRuleMessageTypeAutomaticReply)]
		AutomaticReply,
		// Token: 0x040011D1 RID: 4561
		[LocDescription(ServerStrings.IDs.InboxRuleMessageTypeAutomaticForward)]
		AutomaticForward,
		// Token: 0x040011D2 RID: 4562
		[LocDescription(ServerStrings.IDs.InboxRuleMessageTypeEncrypted)]
		Encrypted,
		// Token: 0x040011D3 RID: 4563
		[LocDescription(ServerStrings.IDs.InboxRuleMessageTypeCalendaring)]
		Calendaring,
		// Token: 0x040011D4 RID: 4564
		[LocDescription(ServerStrings.IDs.InboxRuleMessageTypeCalendaringResponse)]
		CalendaringResponse,
		// Token: 0x040011D5 RID: 4565
		[LocDescription(ServerStrings.IDs.InboxRuleMessageTypePermissionControlled)]
		PermissionControlled,
		// Token: 0x040011D6 RID: 4566
		[LocDescription(ServerStrings.IDs.InboxRuleMessageTypeVoicemail)]
		Voicemail,
		// Token: 0x040011D7 RID: 4567
		[LocDescription(ServerStrings.IDs.InboxRuleMessageTypeSigned)]
		Signed,
		// Token: 0x040011D8 RID: 4568
		[LocDescription(ServerStrings.IDs.InboxRuleMessageTypeApprovalRequest)]
		ApprovalRequest,
		// Token: 0x040011D9 RID: 4569
		[LocDescription(ServerStrings.IDs.InboxRuleMessageTypeReadReceipt)]
		ReadReceipt,
		// Token: 0x040011DA RID: 4570
		[LocDescription(ServerStrings.IDs.InboxRuleMessageTypeNonDeliveryReport)]
		NonDeliveryReport
	}
}

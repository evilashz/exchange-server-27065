using System;
using Microsoft.Exchange.Core.RuleTasks;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BDD RID: 3037
	public enum MessageType
	{
		// Token: 0x04003A70 RID: 14960
		[LocDescription(RulesTasksStrings.IDs.MessageTypeOof)]
		OOF,
		// Token: 0x04003A71 RID: 14961
		[LocDescription(RulesTasksStrings.IDs.MessageTypeAutoForward)]
		AutoForward,
		// Token: 0x04003A72 RID: 14962
		[LocDescription(RulesTasksStrings.IDs.MessageTypeEncrypted)]
		Encrypted,
		// Token: 0x04003A73 RID: 14963
		[LocDescription(RulesTasksStrings.IDs.MessageTypeCalendaring)]
		Calendaring,
		// Token: 0x04003A74 RID: 14964
		[LocDescription(RulesTasksStrings.IDs.MessageTypePermissionControlled)]
		PermissionControlled,
		// Token: 0x04003A75 RID: 14965
		[LocDescription(RulesTasksStrings.IDs.MessageTypeVoicemail)]
		Voicemail,
		// Token: 0x04003A76 RID: 14966
		[LocDescription(RulesTasksStrings.IDs.MessageTypeSigned)]
		Signed,
		// Token: 0x04003A77 RID: 14967
		[LocDescription(RulesTasksStrings.IDs.MessageTypeApprovalRequest)]
		ApprovalRequest,
		// Token: 0x04003A78 RID: 14968
		[LocDescription(RulesTasksStrings.IDs.MessageTypeReadReceipt)]
		ReadReceipt
	}
}

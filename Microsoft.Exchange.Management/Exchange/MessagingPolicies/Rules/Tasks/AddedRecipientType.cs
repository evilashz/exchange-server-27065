using System;
using Microsoft.Exchange.Core.RuleTasks;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B7A RID: 2938
	public enum AddedRecipientType
	{
		// Token: 0x04003999 RID: 14745
		[LocDescription(RulesTasksStrings.IDs.ToRecipientType)]
		To,
		// Token: 0x0400399A RID: 14746
		[LocDescription(RulesTasksStrings.IDs.CcRecipientType)]
		Cc,
		// Token: 0x0400399B RID: 14747
		[LocDescription(RulesTasksStrings.IDs.BccRecipientType)]
		Bcc,
		// Token: 0x0400399C RID: 14748
		[LocDescription(RulesTasksStrings.IDs.RedirectRecipientType)]
		Redirect
	}
}

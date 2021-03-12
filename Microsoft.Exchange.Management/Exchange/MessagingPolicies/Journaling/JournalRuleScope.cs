using System;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.MessagingPolicies.Journaling
{
	// Token: 0x02000A24 RID: 2596
	public enum JournalRuleScope
	{
		// Token: 0x04003483 RID: 13443
		[LocDescription(Strings.IDs.InternalJournalRuleScope)]
		Internal,
		// Token: 0x04003484 RID: 13444
		[LocDescription(Strings.IDs.ExternalJournalRuleScope)]
		External,
		// Token: 0x04003485 RID: 13445
		[LocDescription(Strings.IDs.GlobalJournalRuleScope)]
		Global
	}
}

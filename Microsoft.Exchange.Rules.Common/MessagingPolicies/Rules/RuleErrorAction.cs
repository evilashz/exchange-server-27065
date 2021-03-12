using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000027 RID: 39
	public enum RuleErrorAction
	{
		// Token: 0x0400003D RID: 61
		[LocDescription(RulesStrings.IDs.RuleErrorActionIgnore)]
		Ignore,
		// Token: 0x0400003E RID: 62
		[LocDescription(RulesStrings.IDs.RuleErrorActionDefer)]
		Defer
	}
}

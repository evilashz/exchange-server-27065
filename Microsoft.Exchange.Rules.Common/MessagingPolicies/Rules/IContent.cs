using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200000D RID: 13
	internal interface IContent
	{
		// Token: 0x0600004E RID: 78
		bool Matches(MultiMatcher matcher, RulesEvaluationContext context);
	}
}

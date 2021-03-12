using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A61 RID: 2657
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class InboxRuleTaskHelper
	{
		// Token: 0x0600610E RID: 24846 RVA: 0x00199532 File Offset: 0x00197732
		public static ulong GetRuleIdentity(RuleId ruleId)
		{
			if (ruleId == null)
			{
				throw new ArgumentNullException("ruleId");
			}
			return (ulong)ruleId.StoreRuleId;
		}

		// Token: 0x0600610F RID: 24847 RVA: 0x00199548 File Offset: 0x00197748
		public static void SetRuleId(Rule rule, RuleId ruleId)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("rule");
			}
			if (ruleId == null)
			{
				throw new ArgumentNullException("ruleId");
			}
			rule.Id = ruleId;
		}

		// Token: 0x06006110 RID: 24848 RVA: 0x0019956D File Offset: 0x0019776D
		public static void SetRuleSequence(Rule rule, int newSequence)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("rule");
			}
			rule.Sequence = newSequence;
		}
	}
}

using System;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Data.ClientAccessRules
{
	// Token: 0x02000109 RID: 265
	internal class ClientAccessRuleCollection : RuleCollection
	{
		// Token: 0x0600095A RID: 2394 RVA: 0x0001DA32 File Offset: 0x0001BC32
		internal ClientAccessRuleCollection(string name) : base(name)
		{
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x0001DA3C File Offset: 0x0001BC3C
		public ClientAccessRulesExecutionStatus Run(ClientAccessRulesEvaluationContext context)
		{
			RulesEvaluator rulesEvaluator = new RulesEvaluator(context);
			rulesEvaluator.Run();
			return ClientAccessRulesExecutionStatus.Success;
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x0001DA58 File Offset: 0x0001BC58
		internal void AddClientAccessRuleCollection(ClientAccessRuleCollection rules)
		{
			foreach (Rule rule in rules)
			{
				ClientAccessRule rule2 = (ClientAccessRule)rule;
				base.AddWithoutNameCheck(rule2);
			}
		}
	}
}

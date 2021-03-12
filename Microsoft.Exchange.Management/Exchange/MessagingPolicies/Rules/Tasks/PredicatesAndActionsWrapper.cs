using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B65 RID: 2917
	internal class PredicatesAndActionsWrapper
	{
		// Token: 0x1700213B RID: 8507
		// (get) Token: 0x06006B15 RID: 27413 RVA: 0x001B73D9 File Offset: 0x001B55D9
		// (set) Token: 0x06006B14 RID: 27412 RVA: 0x001B73D0 File Offset: 0x001B55D0
		internal TransportRulePredicate[] Conditions { get; set; }

		// Token: 0x1700213C RID: 8508
		// (get) Token: 0x06006B17 RID: 27415 RVA: 0x001B73EA File Offset: 0x001B55EA
		// (set) Token: 0x06006B16 RID: 27414 RVA: 0x001B73E1 File Offset: 0x001B55E1
		internal TransportRulePredicate[] Exceptions { get; set; }

		// Token: 0x1700213D RID: 8509
		// (get) Token: 0x06006B19 RID: 27417 RVA: 0x001B73FB File Offset: 0x001B55FB
		// (set) Token: 0x06006B18 RID: 27416 RVA: 0x001B73F2 File Offset: 0x001B55F2
		internal TransportRuleAction[] Actions { get; set; }

		// Token: 0x06006B1A RID: 27418 RVA: 0x001B7403 File Offset: 0x001B5603
		public PredicatesAndActionsWrapper(TransportRulePredicate[] conditions, TransportRulePredicate[] exceptions, TransportRuleAction[] actions)
		{
			this.Conditions = conditions;
			this.Exceptions = exceptions;
			this.Actions = actions;
		}
	}
}

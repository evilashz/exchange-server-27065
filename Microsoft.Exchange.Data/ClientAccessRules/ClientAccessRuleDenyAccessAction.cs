using System;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Data.ClientAccessRules
{
	// Token: 0x0200010A RID: 266
	internal class ClientAccessRuleDenyAccessAction : ClientAccessRuleAction
	{
		// Token: 0x0600095D RID: 2397 RVA: 0x0001DAA8 File Offset: 0x0001BCA8
		public ClientAccessRuleDenyAccessAction(ShortList<Argument> arguments) : base(arguments)
		{
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x0600095E RID: 2398 RVA: 0x0001DAB1 File Offset: 0x0001BCB1
		public override string Name
		{
			get
			{
				return "DenyAccess";
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x0001DAB8 File Offset: 0x0001BCB8
		public override Version MinimumVersion
		{
			get
			{
				return ClientAccessRuleDenyAccessAction.PredicateBaseVersion;
			}
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x0001DAC0 File Offset: 0x0001BCC0
		protected override ExecutionControl OnExecute(RulesEvaluationContext baseContext)
		{
			ClientAccessRulesEvaluationContext clientAccessRulesEvaluationContext = (ClientAccessRulesEvaluationContext)baseContext;
			clientAccessRulesEvaluationContext.DenyAccessDelegate(clientAccessRulesEvaluationContext);
			if (clientAccessRulesEvaluationContext.WhatIf)
			{
				clientAccessRulesEvaluationContext.WhatIfActionDelegate(clientAccessRulesEvaluationContext.CurrentRule, ClientAccessRulesAction.DenyAccess);
				return ExecutionControl.Execute;
			}
			return ExecutionControl.SkipAll;
		}

		// Token: 0x040005EF RID: 1519
		public const string ActionName = "DenyAccess";

		// Token: 0x040005F0 RID: 1520
		public const ClientAccessRulesAction ActionIdentifier = ClientAccessRulesAction.DenyAccess;

		// Token: 0x040005F1 RID: 1521
		private static readonly Version PredicateBaseVersion = new Version("15.00.0008.00");
	}
}

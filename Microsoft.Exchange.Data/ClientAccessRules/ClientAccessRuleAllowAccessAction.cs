using System;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Data.ClientAccessRules
{
	// Token: 0x02000107 RID: 263
	internal class ClientAccessRuleAllowAccessAction : ClientAccessRuleAction
	{
		// Token: 0x06000954 RID: 2388 RVA: 0x0001D9B6 File Offset: 0x0001BBB6
		public ClientAccessRuleAllowAccessAction(ShortList<Argument> arguments) : base(arguments)
		{
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000955 RID: 2389 RVA: 0x0001D9BF File Offset: 0x0001BBBF
		public override string Name
		{
			get
			{
				return "AllowAccess";
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x0001D9C6 File Offset: 0x0001BBC6
		public override Version MinimumVersion
		{
			get
			{
				return ClientAccessRuleAllowAccessAction.PredicateBaseVersion;
			}
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x0001D9D0 File Offset: 0x0001BBD0
		protected override ExecutionControl OnExecute(RulesEvaluationContext baseContext)
		{
			ClientAccessRulesEvaluationContext clientAccessRulesEvaluationContext = (ClientAccessRulesEvaluationContext)baseContext;
			if (clientAccessRulesEvaluationContext.WhatIf)
			{
				clientAccessRulesEvaluationContext.WhatIfActionDelegate(clientAccessRulesEvaluationContext.CurrentRule, ClientAccessRulesAction.AllowAccess);
				return ExecutionControl.Execute;
			}
			return ExecutionControl.SkipAll;
		}

		// Token: 0x040005E9 RID: 1513
		public const string ActionName = "AllowAccess";

		// Token: 0x040005EA RID: 1514
		public const ClientAccessRulesAction ActionIdentifier = ClientAccessRulesAction.AllowAccess;

		// Token: 0x040005EB RID: 1515
		private static readonly Version PredicateBaseVersion = new Version("15.00.0008.00");
	}
}

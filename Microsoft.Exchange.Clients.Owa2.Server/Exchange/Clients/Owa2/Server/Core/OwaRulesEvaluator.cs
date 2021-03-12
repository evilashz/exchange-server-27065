using System;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200022A RID: 554
	internal class OwaRulesEvaluator : RulesEvaluator
	{
		// Token: 0x06001513 RID: 5395 RVA: 0x0004B037 File Offset: 0x00049237
		public OwaRulesEvaluator(OwaRulesEvaluationContext context) : base(context)
		{
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x0004B040 File Offset: 0x00049240
		protected override ExecutionControl EnterRule()
		{
			OwaRulesEvaluationContext owaRulesEvaluationContext = (OwaRulesEvaluationContext)base.Context;
			PolicyTipRule policyTipRule = (PolicyTipRule)owaRulesEvaluationContext.CurrentRule;
			if (owaRulesEvaluationContext.RuleExecutionMonitor != null)
			{
				owaRulesEvaluationContext.RuleExecutionMonitor.RuleId = policyTipRule.ImmutableId.ToString("D");
				owaRulesEvaluationContext.RuleExecutionMonitor.Restart();
			}
			owaRulesEvaluationContext.ResetPerRuleData();
			return base.EnterRule();
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x0004B0A4 File Offset: 0x000492A4
		protected override ExecutionControl ExitRule()
		{
			OwaRulesEvaluationContext owaRulesEvaluationContext = (OwaRulesEvaluationContext)base.Context;
			if (owaRulesEvaluationContext.RuleExecutionMonitor != null)
			{
				owaRulesEvaluationContext.RuleExecutionMonitor.Stop(true);
			}
			((OwaRulesEvaluationContext)base.Context).CapturePerRuleData();
			return base.ExitRule();
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x0004B0E8 File Offset: 0x000492E8
		protected override ExecutionControl ExecuteAction(Microsoft.Exchange.MessagingPolicies.Rules.Action action, RulesEvaluationContext context)
		{
			ExecutionControl executionControl = base.ExecuteAction(action, context);
			if (executionControl == ExecutionControl.Execute && action is SenderNotify)
			{
				((OwaRulesEvaluationContext)base.Context).CapturePerRuleMatchData();
			}
			return executionControl;
		}
	}
}

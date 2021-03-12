using System;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000222 RID: 546
	internal class SenderNotify : Microsoft.Exchange.MessagingPolicies.Rules.Action
	{
		// Token: 0x060014D4 RID: 5332 RVA: 0x00049F19 File Offset: 0x00048119
		public SenderNotify(ShortList<Argument> arguments) : base(arguments)
		{
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x060014D5 RID: 5333 RVA: 0x00049F22 File Offset: 0x00048122
		public override Type[] ArgumentsType
		{
			get
			{
				return SenderNotify.ArgumentTypes;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x060014D6 RID: 5334 RVA: 0x00049F29 File Offset: 0x00048129
		public override string Name
		{
			get
			{
				return "SenderNotify";
			}
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x00049F30 File Offset: 0x00048130
		public override bool ShouldExecute(RuleMode mode, RulesEvaluationContext context)
		{
			return base.ShouldExecute(mode, context) || RuleMode.AuditAndNotify == mode;
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x00049F44 File Offset: 0x00048144
		protected override ExecutionControl OnExecute(RulesEvaluationContext baseContext)
		{
			BaseTransportRulesEvaluationContext baseTransportRulesEvaluationContext = (BaseTransportRulesEvaluationContext)baseContext;
			if (baseTransportRulesEvaluationContext.CurrentRule != null && baseTransportRulesEvaluationContext.CurrentRule.Mode == RuleMode.AuditAndNotify)
			{
				baseTransportRulesEvaluationContext.ActionName = DlpPolicyTipAction.NotifyOnly.ToString();
			}
			else
			{
				baseTransportRulesEvaluationContext.ActionName = (string)base.Arguments[0].GetValue(baseTransportRulesEvaluationContext);
			}
			return ExecutionControl.Execute;
		}

		// Token: 0x04000B4C RID: 2892
		private static readonly Type[] ArgumentTypes = new Type[]
		{
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(string)
		};
	}
}

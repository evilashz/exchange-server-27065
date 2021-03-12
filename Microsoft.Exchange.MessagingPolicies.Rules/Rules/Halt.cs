using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200006A RID: 106
	internal class Halt : TransportAction
	{
		// Token: 0x0600037F RID: 895 RVA: 0x00013DF2 File Offset: 0x00011FF2
		public Halt(ShortList<Argument> arguments) : base(arguments)
		{
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000380 RID: 896 RVA: 0x00013DFB File Offset: 0x00011FFB
		public override string Name
		{
			get
			{
				return "Halt";
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000381 RID: 897 RVA: 0x00013E02 File Offset: 0x00012002
		public override TransportActionType Type
		{
			get
			{
				return TransportActionType.BifurcationNeeded;
			}
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00013E08 File Offset: 0x00012008
		protected override ExecutionControl OnExecute(RulesEvaluationContext baseContext)
		{
			TransportRulesEvaluationContext transportRulesEvaluationContext = baseContext as TransportRulesEvaluationContext;
			if (transportRulesEvaluationContext != null && transportRulesEvaluationContext.Server != null)
			{
				TransportUtils.AddRuleCollectionStamp(transportRulesEvaluationContext.MailItem.Message, transportRulesEvaluationContext.Server.Name);
			}
			return ExecutionControl.SkipAll;
		}
	}
}

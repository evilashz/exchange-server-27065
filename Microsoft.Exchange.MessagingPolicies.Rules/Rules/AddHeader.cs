using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000054 RID: 84
	internal class AddHeader : HeaderValueAction
	{
		// Token: 0x06000305 RID: 773 RVA: 0x000110C0 File Offset: 0x0000F2C0
		public AddHeader(ShortList<Argument> arguments) : base(arguments)
		{
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000306 RID: 774 RVA: 0x000110C9 File Offset: 0x0000F2C9
		public override string Name
		{
			get
			{
				return "AddHeader";
			}
		}

		// Token: 0x06000307 RID: 775 RVA: 0x000110D0 File Offset: 0x0000F2D0
		protected override ExecutionControl OnExecute(RulesEvaluationContext baseContext)
		{
			TransportRulesEvaluationContext transportRulesEvaluationContext = (TransportRulesEvaluationContext)baseContext;
			string headerName = (string)base.Arguments[0].GetValue(transportRulesEvaluationContext);
			string value = (string)base.Arguments[1].GetValue(transportRulesEvaluationContext);
			TransportUtils.AddHeaderToMail(transportRulesEvaluationContext.MailItem.Message, headerName, value);
			return ExecutionControl.Execute;
		}
	}
}

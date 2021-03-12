using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000079 RID: 121
	internal class PrependSubject : TransportAction
	{
		// Token: 0x060003C5 RID: 965 RVA: 0x0001483A File Offset: 0x00012A3A
		public PrependSubject(ShortList<Argument> arguments) : base(arguments)
		{
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x00014843 File Offset: 0x00012A43
		public override string Name
		{
			get
			{
				return "PrependSubject";
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x0001484A File Offset: 0x00012A4A
		public override Type[] ArgumentsType
		{
			get
			{
				return PrependSubject.argumentTypes;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x00014851 File Offset: 0x00012A51
		public override TransportActionType Type
		{
			get
			{
				return TransportActionType.BifurcationNeeded;
			}
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00014854 File Offset: 0x00012A54
		protected override ExecutionControl OnExecute(RulesEvaluationContext baseContext)
		{
			TransportRulesEvaluationContext transportRulesEvaluationContext = (TransportRulesEvaluationContext)baseContext;
			string subject = transportRulesEvaluationContext.MailItem.Message.Subject;
			string text = (string)base.Arguments[0].GetValue(transportRulesEvaluationContext);
			if (string.IsNullOrEmpty(subject))
			{
				transportRulesEvaluationContext.MailItem.Message.Subject = text;
			}
			else if (!subject.StartsWith(text, StringComparison.OrdinalIgnoreCase))
			{
				transportRulesEvaluationContext.MailItem.Message.Subject = text + subject;
			}
			baseContext.ResetTextProcessingContext("Message.Subject", false);
			return ExecutionControl.Execute;
		}

		// Token: 0x0400025B RID: 603
		private static Type[] argumentTypes = new Type[]
		{
			typeof(string)
		};
	}
}

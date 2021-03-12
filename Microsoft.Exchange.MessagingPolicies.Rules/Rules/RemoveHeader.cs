using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200007D RID: 125
	internal class RemoveHeader : TransportAction
	{
		// Token: 0x060003E0 RID: 992 RVA: 0x00014E88 File Offset: 0x00013088
		public RemoveHeader(ShortList<Argument> arguments) : base(arguments)
		{
			if (!(base.Arguments[0] is Value))
			{
				throw new RulesValidationException(RulesStrings.ActionArgumentMismatch(this.Name));
			}
			string text = (string)base.Arguments[0].GetValue(null);
			if ("X-MS-Exchange-Inbox-Rules-Loop".Equals(text, StringComparison.OrdinalIgnoreCase) || "X-MS-Exchange-Transport-Rules-Loop".Equals(text, StringComparison.OrdinalIgnoreCase) || "X-MS-Gcc-Journal-Report".Equals(text, StringComparison.OrdinalIgnoreCase) || "X-MS-Exchange-Moderation-Loop".Equals(text, StringComparison.OrdinalIgnoreCase) || "X-MS-Exchange-Generated-Message-Source".Equals(text, StringComparison.OrdinalIgnoreCase))
			{
				throw new RulesValidationException(TransportRulesStrings.CannotRemoveHeader(text));
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x00014F2A File Offset: 0x0001312A
		public override string Name
		{
			get
			{
				return "RemoveHeader";
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x00014F31 File Offset: 0x00013131
		public override Type[] ArgumentsType
		{
			get
			{
				return RemoveHeader.argumentTypes;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x00014F38 File Offset: 0x00013138
		public override TransportActionType Type
		{
			get
			{
				return TransportActionType.BifurcationNeeded;
			}
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00014F3C File Offset: 0x0001313C
		protected override ExecutionControl OnExecute(RulesEvaluationContext baseContext)
		{
			TransportRulesEvaluationContext transportRulesEvaluationContext = (TransportRulesEvaluationContext)baseContext;
			string text = (string)base.Arguments[0].GetValue(transportRulesEvaluationContext);
			transportRulesEvaluationContext.MailItem.Message.MimeDocument.RootPart.Headers.RemoveAll(text);
			if (text.Equals("subject", StringComparison.OrdinalIgnoreCase))
			{
				transportRulesEvaluationContext.MailItem.Message.Subject = null;
			}
			return ExecutionControl.Execute;
		}

		// Token: 0x0400025F RID: 607
		private static Type[] argumentTypes = new Type[]
		{
			typeof(string)
		};
	}
}

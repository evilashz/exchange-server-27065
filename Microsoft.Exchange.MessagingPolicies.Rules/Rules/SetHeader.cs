using System;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000085 RID: 133
	internal class SetHeader : HeaderValueAction
	{
		// Token: 0x0600040A RID: 1034 RVA: 0x000157B0 File Offset: 0x000139B0
		public SetHeader(ShortList<Argument> arguments) : base(arguments)
		{
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x000157B9 File Offset: 0x000139B9
		public override string Name
		{
			get
			{
				return "SetHeader";
			}
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x000157C0 File Offset: 0x000139C0
		protected override ExecutionControl OnExecute(RulesEvaluationContext baseContext)
		{
			TransportRulesEvaluationContext transportRulesEvaluationContext = (TransportRulesEvaluationContext)baseContext;
			string text = (string)base.Arguments[0].GetValue(transportRulesEvaluationContext);
			string text2 = (string)base.Arguments[1].GetValue(transportRulesEvaluationContext);
			MailItem mailItem = transportRulesEvaluationContext.MailItem;
			if (text.Equals("subject", StringComparison.OrdinalIgnoreCase))
			{
				transportRulesEvaluationContext.MailItem.Message.Subject = text2;
			}
			HeaderList headers = mailItem.Message.MimeDocument.RootPart.Headers;
			Header header = headers.FindFirst(text);
			if (header != null)
			{
				header.Value = text2;
			}
			else
			{
				TransportUtils.AddHeaderToMail(mailItem.Message, text, text2);
			}
			baseContext.ResetTextProcessingContext(text, true);
			return ExecutionControl.Execute;
		}
	}
}

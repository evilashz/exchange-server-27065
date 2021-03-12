using System;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000060 RID: 96
	internal class SetHeaderUniqueValue : HeaderValueAction
	{
		// Token: 0x06000349 RID: 841 RVA: 0x00012DD7 File Offset: 0x00010FD7
		public SetHeaderUniqueValue(ShortList<Argument> arguments) : base(arguments)
		{
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600034A RID: 842 RVA: 0x00012DE0 File Offset: 0x00010FE0
		public override string Name
		{
			get
			{
				return "SetHeaderUniqueValue";
			}
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00012DE8 File Offset: 0x00010FE8
		protected override ExecutionControl OnExecute(RulesEvaluationContext baseContext)
		{
			TransportRulesEvaluationContext transportRulesEvaluationContext = (TransportRulesEvaluationContext)baseContext;
			string text = (string)base.Arguments[0].GetValue(transportRulesEvaluationContext);
			string value = (string)base.Arguments[1].GetValue(transportRulesEvaluationContext);
			MailItem mailItem = transportRulesEvaluationContext.MailItem;
			Header[] array = mailItem.Message.MimeDocument.RootPart.Headers.FindAll(text);
			foreach (Header header in array)
			{
				if (header.Value.Equals(value, StringComparison.OrdinalIgnoreCase))
				{
					return ExecutionControl.Execute;
				}
			}
			TransportUtils.AddHeaderToMail(mailItem.Message, text, value);
			return ExecutionControl.Execute;
		}
	}
}

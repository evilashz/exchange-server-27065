using System;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.MessagingPolicies.HygieneRules
{
	// Token: 0x0200000A RID: 10
	internal class SetHeaderAction : Microsoft.Exchange.MessagingPolicies.Rules.Action
	{
		// Token: 0x06000030 RID: 48 RVA: 0x000029A4 File Offset: 0x00000BA4
		public SetHeaderAction(ShortList<Argument> arguments) : base(arguments)
		{
			if (!(base.Arguments[0] is Value) || !(base.Arguments[1] is Value))
			{
				throw new RulesValidationException(RulesStrings.ActionRequiresConstantArguments(this.Name));
			}
			string text = (string)base.Arguments[0].GetValue(null);
			string value = (string)base.Arguments[1].GetValue(null);
			if (!TransportUtils.IsHeaderValid(text))
			{
				throw new RulesValidationException(HygieneRulesStrings.InvalidHeaderName(text));
			}
			if (!TransportUtils.IsHeaderSettable(text, value))
			{
				throw new RulesValidationException(HygieneRulesStrings.CannotSetHeader(text, value));
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002A49 File Offset: 0x00000C49
		public override string Name
		{
			get
			{
				return "SetHeader";
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002A50 File Offset: 0x00000C50
		public override Type[] ArgumentsType
		{
			get
			{
				return SetHeaderAction.argumentTypes;
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002A58 File Offset: 0x00000C58
		protected override ExecutionControl OnExecute(RulesEvaluationContext baseContext)
		{
			HygieneTransportRulesEvaluationContext hygieneTransportRulesEvaluationContext = (HygieneTransportRulesEvaluationContext)baseContext;
			string text = (string)base.Arguments[0].GetValue(hygieneTransportRulesEvaluationContext);
			string text2 = (string)base.Arguments[1].GetValue(hygieneTransportRulesEvaluationContext);
			MailItem mailItem = hygieneTransportRulesEvaluationContext.MailItem;
			if (text.Equals("subject", StringComparison.OrdinalIgnoreCase))
			{
				hygieneTransportRulesEvaluationContext.MailItem.Message.Subject = text2;
			}
			HeaderList headers = mailItem.Message.MimeDocument.RootPart.Headers;
			Header header = headers.FindFirst(text);
			if (header != null)
			{
				header.Value = text2;
				return ExecutionControl.Execute;
			}
			TransportUtils.AddHeaderToMail(mailItem.Message, text, text2);
			return ExecutionControl.Execute;
		}

		// Token: 0x04000016 RID: 22
		private static Type[] argumentTypes = new Type[]
		{
			typeof(string),
			typeof(string)
		};
	}
}

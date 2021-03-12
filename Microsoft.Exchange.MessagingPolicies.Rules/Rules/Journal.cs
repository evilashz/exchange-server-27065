using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200006C RID: 108
	internal class Journal : JournalBase
	{
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000387 RID: 903 RVA: 0x00013F98 File Offset: 0x00012198
		public override string Name
		{
			get
			{
				return "Journal";
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000388 RID: 904 RVA: 0x00013F9F File Offset: 0x0001219F
		public override Type[] ArgumentsType
		{
			get
			{
				return Journal.argumentTypes;
			}
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00013FA8 File Offset: 0x000121A8
		public Journal(ShortList<Argument> args) : base(args)
		{
			string address = args[0].GetValue(null).ToString();
			if (!SmtpAddress.IsValidSmtpAddress(address))
			{
				throw new RulesValidationException(TransportRulesStrings.InvalidAddress(address));
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600038A RID: 906 RVA: 0x00013FE3 File Offset: 0x000121E3
		protected override string MailItemProperty
		{
			get
			{
				return "Microsoft.Exchange.JournalTargetRecips";
			}
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00013FEA File Offset: 0x000121EA
		protected override string GetItemToAdd(TransportRulesEvaluationContext context)
		{
			return (string)base.Arguments[0].GetValue(context);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00014003 File Offset: 0x00012203
		internal string GetTargetAddress()
		{
			return (string)base.Arguments[0].GetValue(null);
		}

		// Token: 0x04000235 RID: 565
		private static Type[] argumentTypes = new Type[]
		{
			typeof(string)
		};
	}
}

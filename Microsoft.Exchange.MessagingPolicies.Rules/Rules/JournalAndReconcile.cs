using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200006D RID: 109
	internal class JournalAndReconcile : JournalBase
	{
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600038E RID: 910 RVA: 0x00014043 File Offset: 0x00012243
		public override Type[] ArgumentsType
		{
			get
			{
				return JournalAndReconcile.argumentTypes;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0001404A File Offset: 0x0001224A
		public override string Name
		{
			get
			{
				return "JournalAndReconcile";
			}
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00014054 File Offset: 0x00012254
		public JournalAndReconcile(ShortList<Argument> args) : base(args)
		{
			string text = args[0].GetValue(null).ToString();
			string address = args[1].GetValue(null).ToString();
			if (text.Length < 1)
			{
				throw new RulesValidationException(TransportRulesStrings.InvalidReconciliationGuid(text));
			}
			int offset = 0;
			if (text[0] == '!')
			{
				offset = 1;
			}
			if (!JournalAndReconcile.ValidateGuid(text, offset))
			{
				throw new RulesValidationException(TransportRulesStrings.InvalidReconciliationGuid(text));
			}
			if (!SmtpAddress.IsValidSmtpAddress(address))
			{
				throw new RulesValidationException(TransportRulesStrings.InvalidAddress(address));
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000391 RID: 913 RVA: 0x000140DB File Offset: 0x000122DB
		protected override string MailItemProperty
		{
			get
			{
				return "Microsoft.Exchange.JournalReconciliationAccounts";
			}
		}

		// Token: 0x06000392 RID: 914 RVA: 0x000140E2 File Offset: 0x000122E2
		protected override string GetItemToAdd(TransportRulesEvaluationContext context)
		{
			return string.Format("{0}{1}{2}", base.Arguments[0].GetValue(context), '+', base.Arguments[1].GetValue(context));
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0001411C File Offset: 0x0001231C
		private static bool ValidateGuid(string text, int offset)
		{
			if (text.Length - offset != "00000000-0000-0000-0000-000000000000".Length)
			{
				return false;
			}
			for (int i = 0; i < "00000000-0000-0000-0000-000000000000".Length; i++)
			{
				if ("00000000-0000-0000-0000-000000000000"[i] == '0')
				{
					if (!Uri.IsHexDigit(text[i + offset]))
					{
						return false;
					}
				}
				else if (text[i + offset] != '-')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000236 RID: 566
		private static Type[] argumentTypes = new Type[]
		{
			typeof(string),
			typeof(string)
		};
	}
}

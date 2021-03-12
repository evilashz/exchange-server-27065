using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000055 RID: 85
	internal class AddManagerAsRecipientType : TransportAction
	{
		// Token: 0x06000308 RID: 776 RVA: 0x00011127 File Offset: 0x0000F327
		public AddManagerAsRecipientType(ShortList<Argument> arguments) : base(arguments)
		{
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000309 RID: 777 RVA: 0x00011130 File Offset: 0x0000F330
		public override Version MinimumVersion
		{
			get
			{
				return TransportRuleConstants.VersionedContainerBaseVersion;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600030A RID: 778 RVA: 0x00011137 File Offset: 0x0000F337
		public override string Name
		{
			get
			{
				return "AddManagerAsRecipientType";
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0001113E File Offset: 0x0000F33E
		public override Type[] ArgumentsType
		{
			get
			{
				return AddManagerAsRecipientType.argumentTypes;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600030C RID: 780 RVA: 0x00011145 File Offset: 0x0000F345
		public override TransportActionType Type
		{
			get
			{
				return TransportActionType.BifurcationNeeded;
			}
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00011148 File Offset: 0x0000F348
		protected override ExecutionControl OnExecute(RulesEvaluationContext baseContext)
		{
			string senderManagerAddress = TransportUtils.GetSenderManagerAddress(baseContext);
			if (string.IsNullOrEmpty(senderManagerAddress))
			{
				return ExecutionControl.Execute;
			}
			TransportRulesEvaluationContext context = (TransportRulesEvaluationContext)baseContext;
			TransportAction transportAction = null;
			ShortList<Argument> shortList = new ShortList<Argument>();
			shortList.Add(new Value(senderManagerAddress));
			string text = (string)base.Arguments[0].GetValue(context);
			string a;
			if ((a = text.ToLower()) != null)
			{
				if (!(a == "to"))
				{
					if (!(a == "cc"))
					{
						if (!(a == "bcc"))
						{
							if (a == "redirect")
							{
								transportAction = new RedirectMessage(shortList);
							}
						}
						else
						{
							transportAction = new AddEnvelopeRecipient(shortList);
						}
					}
					else
					{
						transportAction = new AddCcRecipientSmtpOnly(shortList);
					}
				}
				else
				{
					transportAction = new AddToRecipientSmtpOnly(shortList);
				}
			}
			return transportAction.Execute(baseContext);
		}

		// Token: 0x040001EB RID: 491
		private static Type[] argumentTypes = new Type[]
		{
			typeof(string)
		};
	}
}

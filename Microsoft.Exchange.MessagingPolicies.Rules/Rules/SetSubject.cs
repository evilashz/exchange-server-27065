using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000087 RID: 135
	internal class SetSubject : TransportAction
	{
		// Token: 0x06000410 RID: 1040 RVA: 0x00015882 File Offset: 0x00013A82
		public SetSubject(ShortList<Argument> arguments) : base(arguments)
		{
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x0001588B File Offset: 0x00013A8B
		public override string Name
		{
			get
			{
				return "SetSubject";
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x00015892 File Offset: 0x00013A92
		public override Type[] ArgumentsType
		{
			get
			{
				return SetSubject.argumentTypes;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x00015899 File Offset: 0x00013A99
		public override TransportActionType Type
		{
			get
			{
				return TransportActionType.BifurcationNeeded;
			}
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0001589C File Offset: 0x00013A9C
		protected override ExecutionControl OnExecute(RulesEvaluationContext baseContext)
		{
			TransportRulesEvaluationContext transportRulesEvaluationContext = (TransportRulesEvaluationContext)baseContext;
			string subject = (string)base.Arguments[0].GetValue(transportRulesEvaluationContext);
			transportRulesEvaluationContext.MailItem.Message.Subject = subject;
			return ExecutionControl.Execute;
		}

		// Token: 0x0400026D RID: 621
		private static Type[] argumentTypes = new Type[]
		{
			typeof(string)
		};
	}
}

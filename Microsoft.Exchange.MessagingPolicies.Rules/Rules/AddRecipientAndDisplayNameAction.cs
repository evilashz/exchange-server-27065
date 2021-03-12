using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200004F RID: 79
	internal abstract class AddRecipientAndDisplayNameAction : AddRecipientAction
	{
		// Token: 0x060002F1 RID: 753 RVA: 0x00010EBB File Offset: 0x0000F0BB
		protected AddRecipientAndDisplayNameAction(ShortList<Argument> arguments, bool isToRecipient) : base(arguments, isToRecipient)
		{
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x00010EC5 File Offset: 0x0000F0C5
		public override Type[] ArgumentsType
		{
			get
			{
				return AddRecipientAndDisplayNameAction.displayNameAndAddressType;
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00010ECC File Offset: 0x0000F0CC
		public override string GetDisplayName(RulesEvaluationContext baseContext)
		{
			return (string)base.Arguments[1].GetValue(baseContext);
		}

		// Token: 0x040001E7 RID: 487
		private static Type[] displayNameAndAddressType = new Type[]
		{
			typeof(string),
			typeof(string)
		};
	}
}

using System;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000050 RID: 80
	internal class AddCcRecipient : AddRecipientAndDisplayNameAction
	{
		// Token: 0x060002F5 RID: 757 RVA: 0x00010F1C File Offset: 0x0000F11C
		public AddCcRecipient(ShortList<Argument> arguments) : base(arguments, false)
		{
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x00010F26 File Offset: 0x0000F126
		public override string Name
		{
			get
			{
				return "AddCcRecipient";
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00010F2D File Offset: 0x0000F12D
		public override RecipientP2Type RecipientP2Type
		{
			get
			{
				return RecipientP2Type.Cc;
			}
		}
	}
}

using System;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000056 RID: 86
	internal class AddToRecipient : AddRecipientAndDisplayNameAction
	{
		// Token: 0x0600030F RID: 783 RVA: 0x00011233 File Offset: 0x0000F433
		public AddToRecipient(ShortList<Argument> arguments) : base(arguments, true)
		{
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000310 RID: 784 RVA: 0x0001123D File Offset: 0x0000F43D
		public override string Name
		{
			get
			{
				return "AddToRecipient";
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000311 RID: 785 RVA: 0x00011244 File Offset: 0x0000F444
		public override RecipientP2Type RecipientP2Type
		{
			get
			{
				return RecipientP2Type.To;
			}
		}
	}
}

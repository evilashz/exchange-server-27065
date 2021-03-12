using System;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000057 RID: 87
	internal class AddToRecipientSmtpOnly : AddRecipientAction
	{
		// Token: 0x06000312 RID: 786 RVA: 0x00011247 File Offset: 0x0000F447
		public AddToRecipientSmtpOnly(ShortList<Argument> arguments) : base(arguments, true)
		{
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000313 RID: 787 RVA: 0x00011251 File Offset: 0x0000F451
		public override string Name
		{
			get
			{
				return "AddToRecipientSmtpOnly";
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000314 RID: 788 RVA: 0x00011258 File Offset: 0x0000F458
		public override RecipientP2Type RecipientP2Type
		{
			get
			{
				return RecipientP2Type.To;
			}
		}
	}
}

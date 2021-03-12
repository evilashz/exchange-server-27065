using System;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000051 RID: 81
	internal class AddCcRecipientSmtpOnly : AddRecipientAction
	{
		// Token: 0x060002F8 RID: 760 RVA: 0x00010F30 File Offset: 0x0000F130
		public AddCcRecipientSmtpOnly(ShortList<Argument> arguments) : base(arguments, false)
		{
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x00010F3A File Offset: 0x0000F13A
		public override string Name
		{
			get
			{
				return "AddCcRecipientSmtpOnly";
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002FA RID: 762 RVA: 0x00010F41 File Offset: 0x0000F141
		public override RecipientP2Type RecipientP2Type
		{
			get
			{
				return RecipientP2Type.Cc;
			}
		}
	}
}

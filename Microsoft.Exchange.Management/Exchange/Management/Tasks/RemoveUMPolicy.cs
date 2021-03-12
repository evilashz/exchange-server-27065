using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200045B RID: 1115
	[Cmdlet("Remove", "UMMailboxPolicy", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveUMPolicy : RemoveMailboxPolicyBase<UMMailboxPolicy>
	{
		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x06002772 RID: 10098 RVA: 0x0009BCB1 File Offset: 0x00099EB1
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveUMMailboxPolicy(this.Identity.ToString());
			}
		}

		// Token: 0x06002773 RID: 10099 RVA: 0x0009BCC3 File Offset: 0x00099EC3
		protected override bool HandleRemoveWithAssociatedUsers()
		{
			base.WriteError(new CannotDeleteAssociatedMailboxPolicyException(this.Identity.ToString()), ErrorCategory.WriteError, base.DataObject);
			return false;
		}
	}
}

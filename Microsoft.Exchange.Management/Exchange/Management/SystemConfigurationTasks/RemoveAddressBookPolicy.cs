using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020007EA RID: 2026
	[Cmdlet("Remove", "AddressBookPolicy", DefaultParameterSetName = "Identity", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveAddressBookPolicy : RemoveMailboxPolicyBase<AddressBookMailboxPolicy>
	{
		// Token: 0x17001569 RID: 5481
		// (get) Token: 0x060046D2 RID: 18130 RVA: 0x00122FD8 File Offset: 0x001211D8
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveAddressBookPolicy(this.Identity.ToString());
			}
		}

		// Token: 0x060046D3 RID: 18131 RVA: 0x00122FEA File Offset: 0x001211EA
		protected override bool HandleRemoveWithAssociatedUsers()
		{
			base.WriteError(new InvalidOperationException(Strings.ErrorRemoveAddressBookPolicyWithAssociatedUsers(base.DataObject.Name)), ErrorCategory.InvalidOperation, base.DataObject.Identity);
			return false;
		}
	}
}

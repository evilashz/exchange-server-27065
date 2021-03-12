using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000062 RID: 98
	[Cmdlet("Remove", "LinkedUser", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveLinkedUser : RemoveRecipientObjectTask<UserIdParameter, ADUser>
	{
		// Token: 0x1700026C RID: 620
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x0001B7D4 File Offset: 0x000199D4
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveUser(this.Identity.ToString());
			}
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0001B7E8 File Offset: 0x000199E8
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.DataObject.RecipientType != RecipientType.User || base.DataObject.RecipientTypeDetails != RecipientTypeDetails.LinkedUser)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorRemoveNonLinkededUser(base.DataObject.Identity.ToString())), ExchangeErrorCategory.Client, null);
			}
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0001B842 File Offset: 0x00019A42
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return User.FromDataObject((ADUser)dataObject);
		}
	}
}

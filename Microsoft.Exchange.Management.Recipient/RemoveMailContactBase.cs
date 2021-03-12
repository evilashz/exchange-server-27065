using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200007E RID: 126
	public abstract class RemoveMailContactBase : RemoveRecipientObjectTask<MailContactIdParameter, ADContact>
	{
		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000910 RID: 2320 RVA: 0x00026CBF File Offset: 0x00024EBF
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveMailContact(this.Identity.ToString());
			}
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x00026CD1 File Offset: 0x00024ED1
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return MailContact.FromDataObject((ADContact)dataObject);
		}
	}
}

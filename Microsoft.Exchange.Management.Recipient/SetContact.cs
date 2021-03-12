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
	// Token: 0x02000017 RID: 23
	[Cmdlet("Set", "Contact", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetContact : SetOrgPersonObjectTask<ContactIdParameter, Contact, ADContact>
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000141 RID: 321 RVA: 0x0000754B File Offset: 0x0000574B
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetContact(this.Identity.ToString());
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00007568 File Offset: 0x00005768
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			ADContact dataObject = this.DataObject;
			if (dataObject.RecipientTypeDetails == RecipientTypeDetails.MailForestContact && this.IsObjectStateChanged())
			{
				base.WriteError(new InvalidOperationException(Strings.SetMailForestContactNotAllowed(dataObject.Name)), ErrorCategory.InvalidOperation, this.DataObject.Identity);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000075C9 File Offset: 0x000057C9
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return Contact.FromDataObject((ADContact)dataObject);
		}
	}
}

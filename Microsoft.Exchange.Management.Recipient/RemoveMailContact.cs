using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200007F RID: 127
	[Cmdlet("Remove", "MailContact", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveMailContact : RemoveMailContactBase
	{
		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x00026CE6 File Offset: 0x00024EE6
		// (set) Token: 0x06000914 RID: 2324 RVA: 0x00026CEE File Offset: 0x00024EEE
		[Parameter(Mandatory = false)]
		public new SwitchParameter ForReconciliation
		{
			get
			{
				return base.ForReconciliation;
			}
			set
			{
				base.ForReconciliation = value;
			}
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x00026D00 File Offset: 0x00024F00
		protected override void InternalValidate()
		{
			base.InternalValidate();
			ADContact dataObject = base.DataObject;
			if (dataObject.RecipientTypeDetails == RecipientTypeDetails.MailForestContact)
			{
				base.WriteError(new TaskInvalidOperationException(Strings.RemoveMailForestContactNotAllowed(dataObject.Name)), ExchangeErrorCategory.Client, base.DataObject.Identity);
			}
			if (base.DataObject.CatchAllRecipientBL.Count > 0)
			{
				string domain = string.Join(", ", (from r in base.DataObject.CatchAllRecipientBL
				select r.Name).ToArray<string>());
				base.WriteError(new CannotRemoveMailContactCatchAllRecipientException(domain), ExchangeErrorCategory.Client, base.DataObject.Identity);
			}
		}
	}
}

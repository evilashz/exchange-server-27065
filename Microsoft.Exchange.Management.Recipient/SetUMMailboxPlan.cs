using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000F2 RID: 242
	[Cmdlet("Set", "UMMailboxPlan", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetUMMailboxPlan : SetUMMailboxBase<MailboxPlanIdParameter, UMMailboxPlan>
	{
		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x0600123F RID: 4671 RVA: 0x00042905 File Offset: 0x00040B05
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetUMMailboxPlan(this.Identity.ToString());
			}
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x00042918 File Offset: 0x00040B18
		protected override IConfigurable ResolveDataObject()
		{
			ADRecipient adrecipient = (ADRecipient)base.ResolveDataObject();
			if (MailboxTaskHelper.ExcludeMailboxPlan(adrecipient, true))
			{
				base.WriteError(new ManagementObjectNotFoundException(base.GetErrorMessageObjectNotFound(this.Identity.ToString(), typeof(ADUser).ToString(), (base.DataSession != null) ? base.DataSession.Source : null)), (ErrorCategory)1003, this.Identity);
			}
			return adrecipient;
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x00042987 File Offset: 0x00040B87
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return UMMailboxPlan.FromDataObject((ADUser)dataObject);
		}
	}
}

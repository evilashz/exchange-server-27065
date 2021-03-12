using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Common;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000007 RID: 7
	[Cmdlet("set", "CASMailboxPlan", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetCASMailboxPlan : SetCASMailboxBase<MailboxPlanIdParameter, CASMailboxPlan>
	{
		// Token: 0x0600003B RID: 59 RVA: 0x000036E8 File Offset: 0x000018E8
		protected override IConfigurable ResolveDataObject()
		{
			ADRecipient adrecipient = (ADRecipient)base.ResolveDataObject();
			if (MailboxTaskHelper.ExcludeMailboxPlan(adrecipient, true))
			{
				base.WriteError(new ManagementObjectNotFoundException(base.GetErrorMessageObjectNotFound(this.Identity.ToString(), typeof(ADUser).ToString(), (base.DataSession != null) ? base.DataSession.Source : null)), ErrorCategory.InvalidData, this.Identity);
			}
			return adrecipient;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003753 File Offset: 0x00001953
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return CASMailboxPlan.FromDataObject((ADUser)dataObject);
		}
	}
}

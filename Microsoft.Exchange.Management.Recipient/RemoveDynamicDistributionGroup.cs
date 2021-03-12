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
	// Token: 0x0200002D RID: 45
	[Cmdlet("Remove", "DynamicDistributionGroup", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveDynamicDistributionGroup : RemoveRecipientObjectTask<DynamicGroupIdParameter, ADDynamicGroup>
	{
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000AA5F File Offset: 0x00008C5F
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveDynamicDistributionGroup(this.Identity.ToString());
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000AA71 File Offset: 0x00008C71
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return DynamicDistributionGroup.FromDataObject((ADDynamicGroup)dataObject);
		}
	}
}

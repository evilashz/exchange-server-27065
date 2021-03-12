using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000BF RID: 191
	[Cmdlet("Remove", "SyncDistributionGroup", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveSyncDistributionGroup : RemoveDistributionGroupBase
	{
		// Token: 0x06000C59 RID: 3161 RVA: 0x00032BCB File Offset: 0x00030DCB
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return SyncDistributionGroup.FromDataObject((ADGroup)dataObject);
		}
	}
}

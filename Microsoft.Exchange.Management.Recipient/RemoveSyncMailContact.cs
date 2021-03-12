using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000CB RID: 203
	[Cmdlet("Remove", "SyncMailContact", DefaultParameterSetName = "Identity", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveSyncMailContact : RemoveMailContactBase
	{
		// Token: 0x06000ECF RID: 3791 RVA: 0x00037CE3 File Offset: 0x00035EE3
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return SyncMailContact.FromDataObject((ADContact)dataObject);
		}
	}
}

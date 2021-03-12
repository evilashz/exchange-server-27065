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
	// Token: 0x020000DD RID: 221
	[Cmdlet("Remove", "SyncGroup", DefaultParameterSetName = "Identity", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveSyncGroup : RemoveRecipientObjectTask<NonMailEnabledGroupIdParameter, ADGroup>
	{
		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x060010F6 RID: 4342 RVA: 0x0003CFFA File Offset: 0x0003B1FA
		// (set) Token: 0x060010F7 RID: 4343 RVA: 0x0003D002 File Offset: 0x0003B202
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

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x060010F8 RID: 4344 RVA: 0x0003D00B File Offset: 0x0003B20B
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveGroup(this.Identity.ToString());
			}
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x0003D01D File Offset: 0x0003B21D
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return SyncGroup.FromDataObject((ADGroup)dataObject);
		}
	}
}

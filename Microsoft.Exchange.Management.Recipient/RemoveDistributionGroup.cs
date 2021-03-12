using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200002B RID: 43
	[Cmdlet("Remove", "DistributionGroup", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveDistributionGroup : RemoveDistributionGroupBase
	{
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000A7EC File Offset: 0x000089EC
		// (set) Token: 0x06000210 RID: 528 RVA: 0x0000A7F4 File Offset: 0x000089F4
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

		// Token: 0x06000211 RID: 529 RVA: 0x0000A7FD File Offset: 0x000089FD
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return DistributionGroup.FromDataObject((ADGroup)dataObject);
		}
	}
}

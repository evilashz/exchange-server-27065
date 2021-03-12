using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A6F RID: 2671
	[Cmdlet("Remove", "IPBlocklistEntry", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveIPBlockListEntry : RemoveIPListEntry<IPBlockListEntry>
	{
		// Token: 0x17001CB6 RID: 7350
		// (get) Token: 0x06005F4C RID: 24396 RVA: 0x0018F865 File Offset: 0x0018DA65
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveIPBlockListEntry(this.Identity.ToString());
			}
		}
	}
}

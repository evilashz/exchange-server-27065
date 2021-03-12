using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A6E RID: 2670
	[Cmdlet("Remove", "IPAllowListEntry", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveIPAllowListEntry : RemoveIPListEntry<IPAllowListEntry>
	{
		// Token: 0x17001CB5 RID: 7349
		// (get) Token: 0x06005F4A RID: 24394 RVA: 0x0018F84B File Offset: 0x0018DA4B
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveIPAllowListEntry(this.Identity.ToString());
			}
		}
	}
}

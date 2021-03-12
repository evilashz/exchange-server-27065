using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A77 RID: 2679
	[Cmdlet("Remove", "IPAllowListProvider", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveIPAllowListProvider : RemoveSystemConfigurationObjectTask<IPAllowListProviderIdParameter, IPAllowListProvider>
	{
		// Token: 0x17001CC3 RID: 7363
		// (get) Token: 0x06005F6D RID: 24429 RVA: 0x0018FD41 File Offset: 0x0018DF41
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveIPAllowListProvider(this.Identity.ToString());
			}
		}
	}
}

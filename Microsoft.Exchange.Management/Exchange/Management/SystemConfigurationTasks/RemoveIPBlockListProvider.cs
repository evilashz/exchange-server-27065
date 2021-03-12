using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A7F RID: 2687
	[Cmdlet("Remove", "IPBlockListProvider", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveIPBlockListProvider : RemoveSystemConfigurationObjectTask<IPBlockListProviderIdParameter, IPBlockListProvider>
	{
		// Token: 0x17001CCE RID: 7374
		// (get) Token: 0x06005F82 RID: 24450 RVA: 0x0018FE29 File Offset: 0x0018E029
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveIPBlockListProvider(this.Identity.ToString());
			}
		}
	}
}

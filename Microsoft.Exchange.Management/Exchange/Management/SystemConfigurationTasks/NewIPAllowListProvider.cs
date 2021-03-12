using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A76 RID: 2678
	[Cmdlet("Add", "IPAllowListProvider", SupportsShouldProcess = true)]
	public sealed class NewIPAllowListProvider : NewIPListProvider<IPAllowListProvider>
	{
		// Token: 0x17001CC2 RID: 7362
		// (get) Token: 0x06005F6B RID: 24427 RVA: 0x0018FD1C File Offset: 0x0018DF1C
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageAddIPAllowListProvider(base.Name.ToString(), base.LookupDomain.ToString());
			}
		}
	}
}

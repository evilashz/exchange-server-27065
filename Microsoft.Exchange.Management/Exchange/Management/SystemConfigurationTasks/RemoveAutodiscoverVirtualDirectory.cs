using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C32 RID: 3122
	[Cmdlet("Remove", "AutodiscoverVirtualDirectory", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveAutodiscoverVirtualDirectory : RemoveExchangeVirtualDirectory<ADAutodiscoverVirtualDirectory>
	{
		// Token: 0x17002474 RID: 9332
		// (get) Token: 0x06007659 RID: 30297 RVA: 0x001E3468 File Offset: 0x001E1668
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveAutodiscoverVirtualDirectory;
			}
		}
	}
}

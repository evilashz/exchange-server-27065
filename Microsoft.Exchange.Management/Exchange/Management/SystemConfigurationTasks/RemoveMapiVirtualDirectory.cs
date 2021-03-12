using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C35 RID: 3125
	[Cmdlet("Remove", "MapiVirtualDirectory", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveMapiVirtualDirectory : RemoveExchangeVirtualDirectory<ADMapiVirtualDirectory>
	{
		// Token: 0x17002478 RID: 9336
		// (get) Token: 0x06007661 RID: 30305 RVA: 0x001E34D4 File Offset: 0x001E16D4
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveMapiVirtualDirectory;
			}
		}
	}
}

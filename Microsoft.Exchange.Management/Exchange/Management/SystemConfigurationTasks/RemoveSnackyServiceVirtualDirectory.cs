using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C3F RID: 3135
	[Cmdlet("Remove", "SnackyServiceVirtualDirectory", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveSnackyServiceVirtualDirectory : RemoveExchangeVirtualDirectory<ADSnackyServiceVirtualDirectory>
	{
	}
}

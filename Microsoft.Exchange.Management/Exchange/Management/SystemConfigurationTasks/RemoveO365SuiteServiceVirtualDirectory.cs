using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C37 RID: 3127
	[Cmdlet("Remove", "O365SuiteServiceVirtualDirectory", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveO365SuiteServiceVirtualDirectory : RemoveExchangeVirtualDirectory<ADO365SuiteServiceVirtualDirectory>
	{
	}
}

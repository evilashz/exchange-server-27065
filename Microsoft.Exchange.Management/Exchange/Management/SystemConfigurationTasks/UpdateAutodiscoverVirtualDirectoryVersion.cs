using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C54 RID: 3156
	[Cmdlet("Update", "AutodiscoverVirtualDirectoryVersion", SupportsShouldProcess = true)]
	public sealed class UpdateAutodiscoverVirtualDirectoryVersion : UpdateVirtualDirectoryVersion<ADAutodiscoverVirtualDirectory>
	{
	}
}

using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C59 RID: 3161
	[Cmdlet("Update", "WebServicesVirtualDirectoryVersion", SupportsShouldProcess = true)]
	public sealed class UpdateWebServicesVirtualDirectoryVersion : UpdateVirtualDirectoryVersion<ADWebServicesVirtualDirectory>
	{
	}
}

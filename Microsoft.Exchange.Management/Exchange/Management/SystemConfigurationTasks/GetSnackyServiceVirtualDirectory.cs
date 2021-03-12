using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C18 RID: 3096
	[Cmdlet("Get", "SnackyServiceVirtualDirectory", DefaultParameterSetName = "Identity")]
	public sealed class GetSnackyServiceVirtualDirectory : GetExchangeServiceVirtualDirectory<ADSnackyServiceVirtualDirectory>
	{
	}
}

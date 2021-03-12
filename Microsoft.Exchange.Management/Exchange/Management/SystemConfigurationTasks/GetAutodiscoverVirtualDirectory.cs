using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C0B RID: 3083
	[Cmdlet("Get", "AutodiscoverVirtualDirectory", DefaultParameterSetName = "Identity")]
	public sealed class GetAutodiscoverVirtualDirectory : GetExchangeServiceVirtualDirectory<ADAutodiscoverVirtualDirectory>
	{
	}
}

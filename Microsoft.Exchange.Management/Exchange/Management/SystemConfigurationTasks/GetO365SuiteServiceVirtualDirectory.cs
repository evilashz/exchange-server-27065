using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C10 RID: 3088
	[Cmdlet("Get", "O365SuiteServiceVirtualDirectory", DefaultParameterSetName = "Identity")]
	public sealed class GetO365SuiteServiceVirtualDirectory : GetExchangeServiceVirtualDirectory<ADO365SuiteServiceVirtualDirectory>
	{
	}
}

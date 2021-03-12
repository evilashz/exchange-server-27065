using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000610 RID: 1552
	[Cmdlet("Get", "AuthConfig")]
	public sealed class GetAuthConfig : GetSingletonSystemConfigurationObjectTask<AuthConfig>
	{
	}
}

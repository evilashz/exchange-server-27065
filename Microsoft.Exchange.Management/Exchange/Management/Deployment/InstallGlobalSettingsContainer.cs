using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001F3 RID: 499
	[Cmdlet("install", "GlobalSettingsContainer")]
	public sealed class InstallGlobalSettingsContainer : InstallContainerTaskBase<ADContainer>
	{
	}
}

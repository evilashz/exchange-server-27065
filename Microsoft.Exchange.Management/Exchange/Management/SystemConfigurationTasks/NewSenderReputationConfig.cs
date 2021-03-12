using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200075E RID: 1886
	[Cmdlet("New", "SenderReputationConfig")]
	public sealed class NewSenderReputationConfig : InstallContainerTaskBase<SenderReputationConfig>
	{
	}
}

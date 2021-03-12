using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002E6 RID: 742
	[Cmdlet("uninstall", "AddressRewriteConfigContainer")]
	public sealed class UninstallAddressRewriteConfigContainerTask : RemoveSystemConfigurationObjectTask<ContainerIdParameter, AddressRewriteConfigContainer>
	{
	}
}

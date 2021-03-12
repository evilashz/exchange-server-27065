using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002EE RID: 750
	[Cmdlet("uninstall", "TransportConfigContainer")]
	public sealed class UninstallTransportConfigContainerTask : RemoveSystemConfigurationObjectTask<ContainerIdParameter, TransportConfigContainer>
	{
	}
}

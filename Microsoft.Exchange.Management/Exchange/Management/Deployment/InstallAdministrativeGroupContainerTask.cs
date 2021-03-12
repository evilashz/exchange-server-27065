using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001DE RID: 478
	[Cmdlet("Install", "AdministrativeGroupContainer")]
	public sealed class InstallAdministrativeGroupContainerTask : InstallContainerTaskBase<AdministrativeGroupContainer>
	{
	}
}

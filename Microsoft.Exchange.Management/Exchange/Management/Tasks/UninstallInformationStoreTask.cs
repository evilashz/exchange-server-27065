using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002EA RID: 746
	[Cmdlet("uninstall", "InformationStore")]
	public sealed class UninstallInformationStoreTask : RemoveSystemConfigurationObjectTask<InformationStoreIdParameter, InformationStore>
	{
	}
}

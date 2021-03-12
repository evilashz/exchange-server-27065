using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002E7 RID: 743
	[Cmdlet("Uninstall", "ADTopologyService")]
	[LocDescription(Strings.IDs.UninstallADTopologyServiceTask)]
	public class UninstallADTopologyService : ManageADTopologyService
	{
		// Token: 0x060019AF RID: 6575 RVA: 0x00072684 File Offset: 0x00070884
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}

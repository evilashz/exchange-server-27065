using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D63 RID: 3427
	[Cmdlet("Uninstall", "UMCallRouter")]
	[LocDescription(Strings.IDs.UninstallUmCallRouterTask)]
	public class UninstallUMCallRouter : UMCallRouterTask
	{
		// Token: 0x0600836A RID: 33642 RVA: 0x00218A5C File Offset: 0x00216C5C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}

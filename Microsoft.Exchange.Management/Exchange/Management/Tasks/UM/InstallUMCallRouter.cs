using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D2D RID: 3373
	[Cmdlet("Install", "UMCallRouter")]
	[LocDescription(Strings.IDs.InstallUmCallRouterTask)]
	public class InstallUMCallRouter : UMCallRouterTask
	{
		// Token: 0x06008153 RID: 33107 RVA: 0x00210E57 File Offset: 0x0020F057
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.ReservePorts(5060, 2);
			base.Install();
			TaskLogger.LogExit();
		}
	}
}

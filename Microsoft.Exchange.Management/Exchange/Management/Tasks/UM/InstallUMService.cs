using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D2F RID: 3375
	[Cmdlet("Install", "UMService")]
	[LocDescription(Strings.IDs.InstallUmServiceTask)]
	public class InstallUMService : UMServiceTask
	{
		// Token: 0x0600815C RID: 33116 RVA: 0x00210EB9 File Offset: 0x0020F0B9
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.ReservePorts(5062, 7);
			base.Install();
			TaskLogger.LogExit();
		}
	}
}

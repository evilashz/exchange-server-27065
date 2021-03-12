using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000637 RID: 1591
	[LocDescription(Strings.IDs.InstallPop3ServiceTask)]
	[Cmdlet("Install", "Pop3Service")]
	public class InstallPop3Service : ManagePop3Service
	{
		// Token: 0x06003802 RID: 14338 RVA: 0x000E808A File Offset: 0x000E628A
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InstallPopImapService();
			base.ReservePort(110);
			base.ReservePort(995);
			TaskLogger.LogExit();
		}
	}
}

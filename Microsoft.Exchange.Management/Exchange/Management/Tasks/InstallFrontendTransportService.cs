using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000D00 RID: 3328
	[Cmdlet("Install", "FrontendTransportService")]
	public sealed class InstallFrontendTransportService : ManageFrontendTransportService
	{
		// Token: 0x06007FE0 RID: 32736 RVA: 0x0020B0B2 File Offset: 0x002092B2
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Install();
			TaskLogger.LogExit();
		}
	}
}

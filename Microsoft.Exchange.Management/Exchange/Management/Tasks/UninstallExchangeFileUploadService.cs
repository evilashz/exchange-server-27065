using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200076B RID: 1899
	[Cmdlet("Uninstall", "ExchangeFileUploadService")]
	public class UninstallExchangeFileUploadService : ManageExchangeFileUploadService
	{
		// Token: 0x0600434F RID: 17231 RVA: 0x00114504 File Offset: 0x00112704
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}

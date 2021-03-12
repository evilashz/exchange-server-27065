using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200076A RID: 1898
	[Cmdlet("Install", "ExchangeFileUploadService")]
	public class InstallExchangeFileUploadService : ManageExchangeFileUploadService
	{
		// Token: 0x0600434D RID: 17229 RVA: 0x001144EA File Offset: 0x001126EA
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Install();
			TaskLogger.LogExit();
		}
	}
}

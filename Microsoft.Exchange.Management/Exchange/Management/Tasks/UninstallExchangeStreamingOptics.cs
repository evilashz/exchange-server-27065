using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020007D3 RID: 2003
	[LocDescription(Strings.IDs.UninstallExchangeStreamingOpticsTask)]
	[Cmdlet("Uninstall", "ExchangeStreamingOptics")]
	public class UninstallExchangeStreamingOptics : ManageExchangeStreamingOptics
	{
		// Token: 0x06004620 RID: 17952 RVA: 0x0012013B File Offset: 0x0011E33B
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}

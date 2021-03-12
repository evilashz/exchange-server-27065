using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020007D2 RID: 2002
	[LocDescription(Strings.IDs.InstallExchangeStreamingOpticsTask)]
	[Cmdlet("Install", "ExchangeStreamingOptics")]
	public class InstallExchangeStreamingOptics : ManageExchangeStreamingOptics
	{
		// Token: 0x0600461E RID: 17950 RVA: 0x00120121 File Offset: 0x0011E321
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Install();
			TaskLogger.LogExit();
		}
	}
}

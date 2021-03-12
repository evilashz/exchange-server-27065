using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.PiiRedaction
{
	// Token: 0x02000626 RID: 1574
	[Cmdlet("Disable", "ExchangePiiMapping")]
	public sealed class DisableExchangePiiMapping : Task
	{
		// Token: 0x060037AB RID: 14251 RVA: 0x000E711C File Offset: 0x000E531C
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			base.ExchangeRunspaceConfig.EnablePiiMap = false;
			TaskLogger.Trace("Exchange PII mapping is disabled.", new object[0]);
		}
	}
}

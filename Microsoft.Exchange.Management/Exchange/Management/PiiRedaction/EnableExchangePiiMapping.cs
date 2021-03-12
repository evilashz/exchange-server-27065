using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.PiiRedaction
{
	// Token: 0x02000627 RID: 1575
	[Cmdlet("Enable", "ExchangePiiMapping")]
	public sealed class EnableExchangePiiMapping : Task
	{
		// Token: 0x060037AD RID: 14253 RVA: 0x000E7148 File Offset: 0x000E5348
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			base.ExchangeRunspaceConfig.EnablePiiMap = true;
			if (!base.NeedSuppressingPiiData)
			{
				base.WriteWarning("Exchange Pii mapping will not take effect because you have View-Only PII permission.");
				return;
			}
			TaskLogger.Trace("Exchange PII mapping is enabled.", new object[0]);
		}
	}
}

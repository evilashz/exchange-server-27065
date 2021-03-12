using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring.Management.Common;

namespace Microsoft.Office.Datacenter.ActiveMonitoring.Management
{
	// Token: 0x02000006 RID: 6
	[OutputType(new Type[]
	{
		typeof(MonitorHealthEntry)
	})]
	[Cmdlet("Get", "ServerHealth")]
	public sealed class GetServerHealth : GetHealthBase
	{
		// Token: 0x06000017 RID: 23 RVA: 0x00002318 File Offset: 0x00000518
		protected override void ProcessRecord()
		{
			try
			{
				this.monitorHealthCommon = new MonitorHealthCommon(base.Identity, base.HealthSet, base.HaImpactingOnly);
				base.ProcessRecord();
				LocalizedException ex = null;
				List<MonitorHealthEntry> monitorHealthEntries = this.monitorHealthCommon.GetMonitorHealthEntries(out ex);
				if (ex != null)
				{
					base.WriteWarning(ex.LocalizedString);
				}
				if (monitorHealthEntries != null)
				{
					foreach (MonitorHealthEntry sendToPipeline in monitorHealthEntries)
					{
						base.WriteObject(sendToPipeline);
					}
				}
			}
			catch (Exception exception)
			{
				base.WriteError(new ErrorRecord(exception, string.Empty, ErrorCategory.CloseError, null));
			}
		}

		// Token: 0x04000009 RID: 9
		private MonitorHealthCommon monitorHealthCommon;
	}
}

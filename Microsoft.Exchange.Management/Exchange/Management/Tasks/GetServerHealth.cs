using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring.Management.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000596 RID: 1430
	[OutputType(new Type[]
	{
		typeof(MonitorHealthEntry)
	})]
	[Cmdlet("Get", "ServerHealth")]
	public sealed class GetServerHealth : GetServerHealthBase
	{
		// Token: 0x0600325A RID: 12890 RVA: 0x000CD368 File Offset: 0x000CB568
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				this.monitorHealthCommon = new MonitorHealthCommon((!string.IsNullOrWhiteSpace(base.Identity.Fqdn)) ? base.Identity.Fqdn : base.Identity.ToString(), base.HealthSet, base.HaImpactingOnly);
				LocalizedException ex = null;
				List<MonitorHealthEntry> monitorHealthEntries = this.monitorHealthCommon.GetMonitorHealthEntries(out ex);
				if (ex != null)
				{
					this.WriteWarning(ex.LocalizedString);
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
				base.WriteError(exception, (ErrorCategory)1000, null);
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x04002359 RID: 9049
		private MonitorHealthCommon monitorHealthCommon;
	}
}

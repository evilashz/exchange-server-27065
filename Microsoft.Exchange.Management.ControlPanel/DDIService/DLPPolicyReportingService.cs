using System;
using System.Management.Automation;
using Microsoft.Exchange.Management.ControlPanel;
using Microsoft.Exchange.Management.FfoReporting;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x020001BA RID: 442
	public class DLPPolicyReportingService : DataSourceService
	{
		// Token: 0x060023EC RID: 9196 RVA: 0x0006E4C1 File Offset: 0x0006C6C1
		public PowerShellResults<MailTrafficPolicyReport> GetDLPTrafficData(DLPPolicyTrafficReportParameters parameters)
		{
			parameters.FaultIfNull();
			if (parameters != null)
			{
				return base.GetList<MailTrafficPolicyReport, DLPPolicyTrafficReportParameters>(new PSCommand().AddCommand("Get-MailTrafficPolicyReport"), parameters, null);
			}
			return null;
		}
	}
}

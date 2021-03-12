using System;
using System.Management.Automation;
using Microsoft.Exchange.Management.FfoReporting.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x020003AF RID: 943
	[Cmdlet("Get", "MobileDeviceDetailsReport")]
	[OutputType(new Type[]
	{
		typeof(MobileDeviceDetailsReport)
	})]
	public sealed class GetMobileDeviceDetailsReport : FfoReportingDalTask<MobileDeviceDetailsReport>
	{
		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x0600212A RID: 8490 RVA: 0x0008C2BC File Offset: 0x0008A4BC
		public override string ComponentName
		{
			get
			{
				return ExchangeComponent.FfoMobileDevices.Name;
			}
		}

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x0600212B RID: 8491 RVA: 0x0008C2C8 File Offset: 0x0008A4C8
		public override string MonitorEventName
		{
			get
			{
				return "FFO Reporting Task Status Monitor";
			}
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x0600212C RID: 8492 RVA: 0x0008C2CF File Offset: 0x0008A4CF
		public override string DalMonitorEventName
		{
			get
			{
				return "FFO DAL Retrieval Status Monitor";
			}
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x0600212D RID: 8493 RVA: 0x0008C2D6 File Offset: 0x0008A4D6
		// (set) Token: 0x0600212E RID: 8494 RVA: 0x0008C2DE File Offset: 0x0008A4DE
		[QueryParameter("StartDateQueryDefinition", new string[]
		{

		})]
		[Parameter(Mandatory = false)]
		public DateTime? StartDate { get; set; }

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x0600212F RID: 8495 RVA: 0x0008C2E7 File Offset: 0x0008A4E7
		// (set) Token: 0x06002130 RID: 8496 RVA: 0x0008C2EF File Offset: 0x0008A4EF
		[QueryParameter("EndDateQueryDefinition", new string[]
		{

		})]
		[Parameter(Mandatory = false)]
		public DateTime? EndDate { get; set; }
	}
}

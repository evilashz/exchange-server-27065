using System;
using System.Management.Automation;
using Microsoft.Exchange.Management.FfoReporting.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x02000396 RID: 918
	[OutputType(new Type[]
	{
		typeof(FfoMigrationReport)
	})]
	[Cmdlet("Get", "FfoMigrationReport")]
	public sealed class GetFfoMigrationReport : FfoReportingDalTask<FfoMigrationReport>
	{
		// Token: 0x06002022 RID: 8226 RVA: 0x000887FF File Offset: 0x000869FF
		public GetFfoMigrationReport() : base("Microsoft.Exchange.Hygiene.Data.AsyncQueue.AsyncQueueReport, Microsoft.Exchange.Hygiene.Data")
		{
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06002023 RID: 8227 RVA: 0x0008880C File Offset: 0x00086A0C
		public override string DataSessionTypeName
		{
			get
			{
				return "Microsoft.Exchange.Hygiene.Data.AsyncQueue.AsyncQueueSession";
			}
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06002024 RID: 8228 RVA: 0x00088813 File Offset: 0x00086A13
		public override string DataSessionMethodName
		{
			get
			{
				return "FindMigrationReport";
			}
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06002025 RID: 8229 RVA: 0x0008881A File Offset: 0x00086A1A
		public override string ComponentName
		{
			get
			{
				return ExchangeComponent.FfoRws.Name;
			}
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06002026 RID: 8230 RVA: 0x00088826 File Offset: 0x00086A26
		public override string MonitorEventName
		{
			get
			{
				return "FFO Reporting Task Status Monitor";
			}
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06002027 RID: 8231 RVA: 0x0008882D File Offset: 0x00086A2D
		public override string DalMonitorEventName
		{
			get
			{
				return "FFO DAL Retrieval Status Monitor";
			}
		}
	}
}

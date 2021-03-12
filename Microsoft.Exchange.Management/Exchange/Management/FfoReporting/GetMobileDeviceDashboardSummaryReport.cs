using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Management.FfoReporting.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x020003AE RID: 942
	[OutputType(new Type[]
	{
		typeof(MobileDeviceDashboardSummaryReport)
	})]
	[Cmdlet("Get", "MobileDeviceDashboardSummaryReport")]
	public sealed class GetMobileDeviceDashboardSummaryReport : FfoReportingDalTask<MobileDeviceDashboardSummaryReport>
	{
		// Token: 0x0600211D RID: 8477 RVA: 0x0008C151 File Offset: 0x0008A351
		public GetMobileDeviceDashboardSummaryReport() : base("Microsoft.Exchange.Hygiene.Data.DeviceSnapshot, Microsoft.Exchange.Hygiene.Data")
		{
		}

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x0600211E RID: 8478 RVA: 0x0008C15E File Offset: 0x0008A35E
		public override string DataSessionTypeName
		{
			get
			{
				return "Microsoft.Exchange.Hygiene.Data.MobileDeviceSession";
			}
		}

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x0600211F RID: 8479 RVA: 0x0008C165 File Offset: 0x0008A365
		public override string DataSessionMethodName
		{
			get
			{
				return "GetDashboardSummary";
			}
		}

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x06002120 RID: 8480 RVA: 0x0008C16C File Offset: 0x0008A36C
		public override string ComponentName
		{
			get
			{
				return ExchangeComponent.FfoMobileDevices.Name;
			}
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x06002121 RID: 8481 RVA: 0x0008C178 File Offset: 0x0008A378
		public override string MonitorEventName
		{
			get
			{
				return "FFO Reporting Task Status Monitor";
			}
		}

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06002122 RID: 8482 RVA: 0x0008C17F File Offset: 0x0008A37F
		public override string DalMonitorEventName
		{
			get
			{
				return "FFO DAL Retrieval Status Monitor";
			}
		}

		// Token: 0x06002123 RID: 8483 RVA: 0x0008C188 File Offset: 0x0008A388
		protected override IReadOnlyList<MobileDeviceDashboardSummaryReport> AggregateOutput()
		{
			IReadOnlyList<MobileDeviceDashboardSummaryReport> readOnlyList = base.AggregateOutput();
			if (readOnlyList.Count == 0)
			{
				MobileDeviceDashboardSummaryReport item = new MobileDeviceDashboardSummaryReport
				{
					Platform = null,
					TotalDevicesCount = 0,
					AllowedDevicesCount = 0,
					BlockedDevicesCount = 0,
					QuarantinedDevicesCount = 0,
					UnknownDevicesCount = 0,
					LastUpdatedTime = DateTime.UtcNow,
					StartDate = new DateTime?(new DateTime(2014, 1, 1, 0, 0, 0)),
					EndDate = new DateTime?(new DateTime(2014, 12, 31, 23, 59, 59))
				};
				readOnlyList = new List<MobileDeviceDashboardSummaryReport>
				{
					item
				};
			}
			return readOnlyList;
		}

		// Token: 0x06002124 RID: 8484 RVA: 0x0008C22D File Offset: 0x0008A42D
		protected override void CustomInternalValidate()
		{
			base.CustomInternalValidate();
			Schema.Utilities.CheckDates(this.StartDate, this.EndDate, new Schema.Utilities.NotifyNeedDefaultDatesDelegate(this.SetDefaultDates), new Schema.Utilities.ValidateDatesDelegate(Schema.Utilities.VerifyDateRange));
		}

		// Token: 0x06002125 RID: 8485 RVA: 0x0008C260 File Offset: 0x0008A460
		private void SetDefaultDates()
		{
			DateTime utcNow = DateTime.UtcNow;
			this.EndDate = new DateTime?(utcNow);
			this.StartDate = new DateTime?(utcNow.AddDays(-14.0));
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06002126 RID: 8486 RVA: 0x0008C29A File Offset: 0x0008A49A
		// (set) Token: 0x06002127 RID: 8487 RVA: 0x0008C2A2 File Offset: 0x0008A4A2
		[QueryParameter("StartDateQueryDefinition", new string[]
		{

		})]
		[Parameter(Mandatory = false)]
		public DateTime? StartDate { get; set; }

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x06002128 RID: 8488 RVA: 0x0008C2AB File Offset: 0x0008A4AB
		// (set) Token: 0x06002129 RID: 8489 RVA: 0x0008C2B3 File Offset: 0x0008A4B3
		[Parameter(Mandatory = false)]
		[QueryParameter("EndDateQueryDefinition", new string[]
		{

		})]
		public DateTime? EndDate { get; set; }

		// Token: 0x04001A90 RID: 6800
		private const string MobileDeviceDataSessionTypeName = "Microsoft.Exchange.Hygiene.Data.MobileDeviceSession";

		// Token: 0x04001A91 RID: 6801
		private const string MobileDeviceDataSessionMethodName = "GetDashboardSummary";

		// Token: 0x04001A92 RID: 6802
		private const string MobileDeviceDALTypeName = "Microsoft.Exchange.Hygiene.Data.DeviceSnapshot, Microsoft.Exchange.Hygiene.Data";
	}
}

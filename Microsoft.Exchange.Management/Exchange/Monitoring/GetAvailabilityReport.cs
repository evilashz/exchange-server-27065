using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Monitoring.Reporting;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200058A RID: 1418
	public abstract class GetAvailabilityReport<TIdentity> : ReportingTask<TIdentity> where TIdentity : IIdentityParameter, new()
	{
		// Token: 0x17000ECF RID: 3791
		// (get) Token: 0x060031F7 RID: 12791 RVA: 0x000CAF9B File Offset: 0x000C919B
		// (set) Token: 0x060031F8 RID: 12792 RVA: 0x000CAFB2 File Offset: 0x000C91B2
		[Parameter(ParameterSetName = "StartEndDateSet", Mandatory = true)]
		public DateTime StartDate
		{
			get
			{
				return (DateTime)base.Fields["StartDate"];
			}
			set
			{
				base.Fields["StartDate"] = value;
			}
		}

		// Token: 0x17000ED0 RID: 3792
		// (get) Token: 0x060031F9 RID: 12793 RVA: 0x000CAFCA File Offset: 0x000C91CA
		// (set) Token: 0x060031FA RID: 12794 RVA: 0x000CAFE1 File Offset: 0x000C91E1
		[Parameter(ParameterSetName = "StartEndDateSet", Mandatory = true)]
		public DateTime EndDate
		{
			get
			{
				return (DateTime)base.Fields["EndDate"];
			}
			set
			{
				base.Fields["EndDate"] = value;
			}
		}

		// Token: 0x17000ED1 RID: 3793
		// (get) Token: 0x060031FB RID: 12795 RVA: 0x000CAFF9 File Offset: 0x000C91F9
		// (set) Token: 0x060031FC RID: 12796 RVA: 0x000CB01A File Offset: 0x000C921A
		[Parameter(ParameterSetName = "ReportingPeriodSet", Mandatory = false)]
		public ReportingPeriod ReportingPeriod
		{
			get
			{
				return (ReportingPeriod)(base.Fields["ReportingPeriod"] ?? ReportingPeriod.LastMonth);
			}
			set
			{
				base.Fields["ReportingPeriod"] = value;
			}
		}

		// Token: 0x17000ED2 RID: 3794
		// (get) Token: 0x060031FD RID: 12797 RVA: 0x000CB032 File Offset: 0x000C9232
		// (set) Token: 0x060031FE RID: 12798 RVA: 0x000CB058 File Offset: 0x000C9258
		[Parameter(ParameterSetName = "StartEndDateSet", Mandatory = false)]
		[Parameter(ParameterSetName = "ReportingPeriodSet", Mandatory = false)]
		public SwitchParameter DailyStatistics
		{
			get
			{
				return (SwitchParameter)(base.Fields["DailyStatistics"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["DailyStatistics"] = value;
			}
		}

		// Token: 0x060031FF RID: 12799 RVA: 0x000CB070 File Offset: 0x000C9270
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			if (base.ParameterSetName == "StartEndDateSet")
			{
				if (this.EndDate < this.StartDate)
				{
					base.WriteError(new ArgumentException(Strings.InvalidReportingDateRange(this.StartDate, this.EndDate), "StartDate, EndDate"), (ErrorCategory)1000, null);
				}
				if (this.StartDate < new DateTime(1753, 1, 1))
				{
					base.WriteError(new ArgumentException(Strings.InvalidReportingStartDate(this.StartDate), "StartDate"), (ErrorCategory)1000, null);
				}
				this.utcStartDateTime = new ExDateTime(ExTimeZone.UtcTimeZone, this.StartDate.Date);
				this.utcEndDateTime = new ExDateTime(ExTimeZone.UtcTimeZone, this.EndDate.Date);
			}
			else if (base.ParameterSetName == "ReportingPeriodSet")
			{
				this.GetStartEndDateForReportingPeriod((ReportingPeriod)base.Fields["ReportingPeriod"], out this.utcStartDateTime, out this.utcEndDateTime);
			}
			base.InternalValidate();
			TaskLogger.LogExit();
		}

		// Token: 0x06003200 RID: 12800 RVA: 0x000CB198 File Offset: 0x000C9398
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter();
			if (dataObject != null)
			{
				ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)dataObject;
				if (adorganizationalUnit.OrganizationId.OrganizationalUnit != null && adorganizationalUnit.OrganizationId.ConfigurationUnit != null)
				{
					this.GetTenantAvailabilityReport(adorganizationalUnit.OrganizationId.OrganizationalUnit);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06003201 RID: 12801 RVA: 0x000CB1E4 File Offset: 0x000C93E4
		protected override void WriteResult<T>(IEnumerable<T> dataObjects)
		{
			TaskLogger.LogEnter();
			if (dataObjects != null)
			{
				if (this.Identity != null)
				{
					base.WriteResult<T>(dataObjects);
				}
				else if (base.CurrentOrganizationId.Equals(OrganizationId.ForestWideOrgId))
				{
					this.GetTenantAvailabilityReport(null);
				}
				else
				{
					if (base.CurrentOrganizationId.OrganizationalUnit != null && base.CurrentOrganizationId.ConfigurationUnit != null)
					{
						this.GetTenantAvailabilityReport(base.CurrentOrganizationId.OrganizationalUnit);
					}
					base.WriteResult<T>(dataObjects);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06003202 RID: 12802 RVA: 0x000CB261 File Offset: 0x000C9461
		protected virtual void WriteReport(ConfigurableObject reportObject)
		{
			base.WriteResult(reportObject);
		}

		// Token: 0x06003203 RID: 12803
		protected abstract void GetTenantAvailabilityReport(ADObjectId tenantId);

		// Token: 0x06003204 RID: 12804 RVA: 0x000CB26C File Offset: 0x000C946C
		private void GetStartEndDateForReportingPeriod(ReportingPeriod reportingPeriod, out ExDateTime startDateTime, out ExDateTime endDateTime)
		{
			startDateTime = ExDateTime.MinValue;
			endDateTime = ExDateTime.MaxValue;
			ExDateTime date = ExDateTime.UtcNow.Date;
			switch (reportingPeriod)
			{
			case ReportingPeriod.Today:
				startDateTime = date;
				endDateTime = startDateTime.Add(TimeSpan.FromDays(1.0)).Subtract(TimeSpan.FromMinutes(1.0));
				return;
			case ReportingPeriod.Yesterday:
				startDateTime = date.Subtract(TimeSpan.FromDays(1.0));
				endDateTime = startDateTime.AddDays(1.0).Subtract(TimeSpan.FromMinutes(1.0));
				return;
			case ReportingPeriod.LastWeek:
				startDateTime = date.Subtract(TimeSpan.FromDays((double)(7 + date.DayOfWeek)));
				endDateTime = date.Subtract(TimeSpan.FromDays((double)date.DayOfWeek)).Subtract(TimeSpan.FromMinutes(1.0));
				return;
			case ReportingPeriod.LastMonth:
				startDateTime = GetAvailabilityReport<TIdentity>.SubtractMonths(date, 1);
				endDateTime = GetAvailabilityReport<TIdentity>.GetLastMonthLastDate(date);
				return;
			case ReportingPeriod.Last3Months:
				startDateTime = GetAvailabilityReport<TIdentity>.SubtractMonths(date, 3);
				endDateTime = GetAvailabilityReport<TIdentity>.GetLastMonthLastDate(date);
				return;
			case ReportingPeriod.Last6Months:
				startDateTime = GetAvailabilityReport<TIdentity>.SubtractMonths(date, 6);
				endDateTime = GetAvailabilityReport<TIdentity>.GetLastMonthLastDate(date);
				return;
			case ReportingPeriod.Last12Months:
				startDateTime = GetAvailabilityReport<TIdentity>.SubtractMonths(date, 12);
				endDateTime = GetAvailabilityReport<TIdentity>.GetLastMonthLastDate(date);
				return;
			default:
				base.WriteError(new ArgumentException(Strings.InvalidReportingPeriod), (ErrorCategory)1000, null);
				return;
			}
		}

		// Token: 0x06003205 RID: 12805 RVA: 0x000CB418 File Offset: 0x000C9618
		private static ExDateTime SubtractMonths(ExDateTime dateTime, int monthsToSubtract)
		{
			int num = dateTime.Year;
			int num2 = dateTime.Month;
			num2 -= monthsToSubtract;
			if (num2 <= 0)
			{
				num--;
				num2 += 12;
			}
			return new ExDateTime(ExTimeZone.UtcTimeZone, num, num2, 1);
		}

		// Token: 0x06003206 RID: 12806 RVA: 0x000CB454 File Offset: 0x000C9654
		private static ExDateTime GetLastMonthLastDate(ExDateTime datetime)
		{
			return new ExDateTime(ExTimeZone.UtcTimeZone, datetime.Year, datetime.Month, 1).Subtract(TimeSpan.FromMinutes(1.0));
		}

		// Token: 0x0400233F RID: 9023
		protected const string StartEndDateParameterSetName = "StartEndDateSet";

		// Token: 0x04002340 RID: 9024
		protected const string ReportingPeriodParameterSetName = "ReportingPeriodSet";

		// Token: 0x04002341 RID: 9025
		protected const ReportingPeriod DefaultReportingPeriod = ReportingPeriod.LastMonth;

		// Token: 0x04002342 RID: 9026
		protected const int TotalDaysInWeek = 7;

		// Token: 0x04002343 RID: 9027
		protected ExDateTime utcStartDateTime = ExDateTime.MinValue;

		// Token: 0x04002344 RID: 9028
		protected ExDateTime utcEndDateTime = ExDateTime.MaxValue;
	}
}

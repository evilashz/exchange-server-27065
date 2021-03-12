using System;
using System.ServiceModel;
using System.Web;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000106 RID: 262
	public static class DatabaseUtility
	{
		// Token: 0x17001A12 RID: 6674
		// (get) Token: 0x06001F87 RID: 8071 RVA: 0x0005EFC5 File Offset: 0x0005D1C5
		public static string PageTitle
		{
			get
			{
				if (DatabasePageId.MaintenanceSchedule == DatabaseUtility.GetDatabasePageId())
				{
					return Strings.DatabaseMaintenanceSchedulePageTitle;
				}
				if (DatabasePageId.QuotaNotificationSchedule == DatabaseUtility.GetDatabasePageId())
				{
					return Strings.DatabaseQuotaNotificationSchedulePageTitle;
				}
				return string.Empty;
			}
		}

		// Token: 0x17001A13 RID: 6675
		// (get) Token: 0x06001F88 RID: 8072 RVA: 0x0005EFF2 File Offset: 0x0005D1F2
		public static string LegendForTrueValue
		{
			get
			{
				if (DatabasePageId.MaintenanceSchedule == DatabaseUtility.GetDatabasePageId())
				{
					return Strings.DatabaseSchedulerMaintenanceHours;
				}
				if (DatabasePageId.QuotaNotificationSchedule == DatabaseUtility.GetDatabasePageId())
				{
					return Strings.DatabaseSchedulerQuotaNotificationHours;
				}
				return string.Empty;
			}
		}

		// Token: 0x17001A14 RID: 6676
		// (get) Token: 0x06001F89 RID: 8073 RVA: 0x0005F01F File Offset: 0x0005D21F
		public static string LegendForFalseValue
		{
			get
			{
				if (DatabasePageId.MaintenanceSchedule == DatabaseUtility.GetDatabasePageId())
				{
					return Strings.DatabasechedulerNonMaintenanceHours;
				}
				if (DatabasePageId.QuotaNotificationSchedule == DatabaseUtility.GetDatabasePageId())
				{
					return Strings.DatabasechedulerNonQuotaNotificationHours;
				}
				return string.Empty;
			}
		}

		// Token: 0x06001F8A RID: 8074 RVA: 0x0005F04C File Offset: 0x0005D24C
		private static DatabasePageId GetDatabasePageId()
		{
			string text = HttpContext.Current.Request.QueryString["functionaltype"];
			int num;
			DatabasePageId result;
			if (!int.TryParse(text, out num) && Enum.TryParse<DatabasePageId>(text, out result))
			{
				return result;
			}
			throw new FaultException("The query string functionaltype = " + HttpContext.Current.Request.QueryString["functionaltype"] + " could not be understood.");
		}
	}
}

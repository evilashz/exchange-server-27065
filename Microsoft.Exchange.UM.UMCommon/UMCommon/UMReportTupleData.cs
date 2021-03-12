using System;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000147 RID: 327
	[DataContract(Name = "UMReportTupleData", Namespace = "http://schemas.microsoft.com/v1.0/UMReportAggregatedData")]
	internal class UMReportTupleData
	{
		// Token: 0x06000A81 RID: 2689 RVA: 0x00027314 File Offset: 0x00025514
		public UMReportTupleData()
		{
			this.dailyReport = new DailyReportDictionary();
			this.monthlyReport = new MonthlyReportDictionary();
			this.totalReport = new UMReportRawCounters(default(DateTime));
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x00027351 File Offset: 0x00025551
		public void AddCDR(CDRData cdrData)
		{
			this.AddCDRToDailyData(cdrData);
			this.AddCDRToMonthlyData(cdrData);
			this.AddCDRToTotalData(cdrData);
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x00027368 File Offset: 0x00025568
		public void CleanUp()
		{
			this.dailyReport = this.CleanupHelper<DailyReportDictionary>(this.dailyReport, true);
			this.monthlyReport = this.CleanupHelper<MonthlyReportDictionary>(this.monthlyReport, false);
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x00027390 File Offset: 0x00025590
		private T CleanupHelper<T>(T toClean, bool addDays) where T : UMReportDictionaryBase, new()
		{
			if (toClean.Count == 0)
			{
				return toClean;
			}
			DateTime dateTime;
			if (addDays)
			{
				dateTime = this.GetDateTimeValue(ExTimeZone.UtcTimeZone, ExDateTime.UtcNow.Year, ExDateTime.UtcNow.Month, ExDateTime.UtcNow.Day);
			}
			else
			{
				dateTime = this.GetDateTimeValue(ExTimeZone.UtcTimeZone, ExDateTime.UtcNow.Year, ExDateTime.UtcNow.Month, 1);
			}
			DateTime t = dateTime;
			foreach (DateTime dateTime2 in toClean.Keys)
			{
				if (dateTime2 < t)
				{
					t = dateTime2;
				}
			}
			T result = Activator.CreateInstance<T>();
			while (result.Count < result.MaxItemsInDictionary && dateTime >= t)
			{
				UMReportRawCounters value;
				if (!toClean.TryGetValue(dateTime, out value))
				{
					value = new UMReportRawCounters(dateTime);
				}
				result.Add(dateTime, value);
				dateTime = (addDays ? dateTime.AddDays(-1.0) : dateTime.AddMonths(-1));
			}
			return result;
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x000274EC File Offset: 0x000256EC
		public UMReportRawCounters[] QueryReport(GroupBy groupBy)
		{
			switch (groupBy)
			{
			case GroupBy.Day:
				return (from x in this.dailyReport.Values
				orderby x.Date descending
				select x).ToArray<UMReportRawCounters>();
			case GroupBy.Month:
				return (from x in this.monthlyReport.Values
				orderby x.Date descending
				select x).ToArray<UMReportRawCounters>();
			case GroupBy.Total:
				return new UMReportRawCounters[]
				{
					this.totalReport
				};
			default:
				throw new NotImplementedException("Unknown value for GroupBy");
			}
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x00027594 File Offset: 0x00025794
		private void AddCDRToMonthlyData(CDRData cdrData)
		{
			DateTime dateTimeValue = this.GetDateTimeValue(ExTimeZone.UtcTimeZone, cdrData.CallStartTime.Year, cdrData.CallStartTime.Month, 1);
			UMReportRawCounters umreportRawCounters;
			if (!this.monthlyReport.TryGetValue(dateTimeValue, out umreportRawCounters))
			{
				umreportRawCounters = new UMReportRawCounters(dateTimeValue);
				this.monthlyReport.Add(dateTimeValue, umreportRawCounters);
			}
			umreportRawCounters.AddCDR(cdrData);
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x000275F8 File Offset: 0x000257F8
		private void AddCDRToDailyData(CDRData cdrData)
		{
			DateTime dateTimeValue = this.GetDateTimeValue(ExTimeZone.UtcTimeZone, cdrData.CallStartTime.Year, cdrData.CallStartTime.Month, cdrData.CallStartTime.Day);
			UMReportRawCounters umreportRawCounters;
			if (!this.dailyReport.TryGetValue(dateTimeValue, out umreportRawCounters))
			{
				umreportRawCounters = new UMReportRawCounters(dateTimeValue);
				this.dailyReport.Add(dateTimeValue, umreportRawCounters);
			}
			umreportRawCounters.AddCDR(cdrData);
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x00027667 File Offset: 0x00025867
		private void AddCDRToTotalData(CDRData cdrData)
		{
			this.totalReport.AddCDR(cdrData);
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x00027678 File Offset: 0x00025878
		private DateTime GetDateTimeValue(ExTimeZone timeZone, int year, int month, int day)
		{
			ExDateTime exDateTime = new ExDateTime(timeZone, year, month, day, 0, 0, 0);
			return exDateTime.LocalTime;
		}

		// Token: 0x040005AF RID: 1455
		[DataMember(Name = "DailyReport")]
		private DailyReportDictionary dailyReport;

		// Token: 0x040005B0 RID: 1456
		[DataMember(Name = "MonthlyReport")]
		private MonthlyReportDictionary monthlyReport;

		// Token: 0x040005B1 RID: 1457
		[DataMember(Name = "TotalReport")]
		private UMReportRawCounters totalReport;
	}
}

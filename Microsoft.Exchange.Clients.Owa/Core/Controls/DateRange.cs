using System;
using System.Collections;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x020002BB RID: 699
	public class DateRange : IComparer
	{
		// Token: 0x06001B5E RID: 7006 RVA: 0x0009D1B3 File Offset: 0x0009B3B3
		public DateRange(ExDateTime start, ExDateTime end)
		{
			if (start > end)
			{
				throw new ArgumentException("start can't be larger than end");
			}
			this.start = start;
			this.end = end;
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x0009D1E0 File Offset: 0x0009B3E0
		public DateRange(ExTimeZone timeZone, DateTime startTime, DateTime endTime)
		{
			if (startTime > endTime)
			{
				throw new ArgumentException("start can't be larger than end");
			}
			this.visualStart = new DateTime?(startTime);
			this.visualEnd = new DateTime?(endTime);
			this.start = new ExDateTime(timeZone, startTime);
			this.end = new ExDateTime(timeZone, endTime);
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x0009D23C File Offset: 0x0009B43C
		public static bool IsDateInRangeArray(ExDateTime date, DateRange[] ranges)
		{
			if (ranges == null || ranges.Length <= 0)
			{
				throw new ArgumentException("ranges may not be null or empty array");
			}
			for (int i = 0; i < ranges.Length; i++)
			{
				if (ranges[i].IsDateInRange(date))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x0009D27C File Offset: 0x0009B47C
		public static ExDateTime GetMinStartTimeInRangeArray(DateRange[] dateRanges)
		{
			if (dateRanges == null || dateRanges.Length <= 0)
			{
				throw new ArgumentException("ranges may not be null or empty array");
			}
			ExDateTime exDateTime = dateRanges[0].Start;
			for (int i = 0; i < dateRanges.Length; i++)
			{
				if (ExDateTime.Compare(dateRanges[i].Start, exDateTime) < 0)
				{
					exDateTime = dateRanges[i].Start;
				}
			}
			return exDateTime;
		}

		// Token: 0x06001B62 RID: 7010 RVA: 0x0009D2D0 File Offset: 0x0009B4D0
		public static ExDateTime GetMaxEndTimeInRangeArray(DateRange[] dateRanges)
		{
			if (dateRanges == null || dateRanges.Length <= 0)
			{
				throw new ArgumentException("ranges may not be null or empty array");
			}
			ExDateTime exDateTime = dateRanges[0].End;
			for (int i = 0; i < dateRanges.Length; i++)
			{
				if (ExDateTime.Compare(dateRanges[i].End, exDateTime) > 0)
				{
					exDateTime = dateRanges[i].End;
				}
			}
			return exDateTime;
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06001B63 RID: 7011 RVA: 0x0009D323 File Offset: 0x0009B523
		public ExDateTime Start
		{
			get
			{
				return this.start;
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06001B64 RID: 7012 RVA: 0x0009D32B File Offset: 0x0009B52B
		public ExDateTime End
		{
			get
			{
				return this.end;
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06001B65 RID: 7013 RVA: 0x0009D333 File Offset: 0x0009B533
		public DateTime? VisualStart
		{
			get
			{
				return this.visualStart;
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06001B66 RID: 7014 RVA: 0x0009D33B File Offset: 0x0009B53B
		public DateTime? VisualEnd
		{
			get
			{
				return this.visualEnd;
			}
		}

		// Token: 0x06001B67 RID: 7015 RVA: 0x0009D344 File Offset: 0x0009B544
		public int Compare(object x, object y)
		{
			DateRange dateRange = x as DateRange;
			DateRange dateRange2 = y as DateRange;
			return ExDateTime.Compare(dateRange.Start, dateRange2.Start);
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x0009D370 File Offset: 0x0009B570
		public bool Intersects(ExDateTime start, ExDateTime end)
		{
			return (start < this.End && end > this.Start) || (start == end && start < this.End && end >= this.Start);
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x0009D3C0 File Offset: 0x0009B5C0
		public bool Includes(ExDateTime start, ExDateTime end)
		{
			return start >= this.Start && end <= this.End;
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x0009D3DE File Offset: 0x0009B5DE
		public bool IsDateInRange(ExDateTime date)
		{
			return date >= this.Start && date < this.End;
		}

		// Token: 0x040013E4 RID: 5092
		private ExDateTime start;

		// Token: 0x040013E5 RID: 5093
		private ExDateTime end;

		// Token: 0x040013E6 RID: 5094
		private DateTime? visualStart;

		// Token: 0x040013E7 RID: 5095
		private DateTime? visualEnd;
	}
}

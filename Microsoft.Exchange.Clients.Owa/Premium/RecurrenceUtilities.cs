using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004FE RID: 1278
	public sealed class RecurrenceUtilities
	{
		// Token: 0x060030FA RID: 12538 RVA: 0x001202FD File Offset: 0x0011E4FD
		internal RecurrenceUtilities(Recurrence recurrence, TextWriter output)
		{
			this.recurrence = recurrence;
			this.output = output;
			this.recurrenceType = this.MapRecurrenceType();
		}

		// Token: 0x060030FB RID: 12539 RVA: 0x00120328 File Offset: 0x0011E528
		public OwaRecurrenceType MapRecurrenceType()
		{
			OwaRecurrenceType result = OwaRecurrenceType.None;
			if (this.recurrence == null)
			{
				result = OwaRecurrenceType.None;
			}
			else if (this.recurrence.Pattern is DailyRecurrencePattern)
			{
				result = OwaRecurrenceType.Daily;
			}
			else if (this.recurrence.Pattern is WeeklyRecurrencePattern)
			{
				WeeklyRecurrencePattern weeklyRecurrencePattern = (WeeklyRecurrencePattern)this.recurrence.Pattern;
				if (weeklyRecurrencePattern.DaysOfWeek == DaysOfWeek.Weekdays)
				{
					result = (OwaRecurrenceType.Daily | OwaRecurrenceType.DailyEveryWeekday);
				}
				else
				{
					result = OwaRecurrenceType.Weekly;
				}
			}
			else if (this.recurrence.Pattern is MonthlyRecurrencePattern)
			{
				result = OwaRecurrenceType.Monthly;
			}
			else if (this.recurrence.Pattern is MonthlyThRecurrencePattern)
			{
				result = (OwaRecurrenceType.Monthly | OwaRecurrenceType.MonthlyTh);
			}
			else if (this.recurrence.Pattern is YearlyRecurrencePattern)
			{
				result = OwaRecurrenceType.Yearly;
			}
			else if (this.recurrence.Pattern is YearlyThRecurrencePattern)
			{
				result = (OwaRecurrenceType.Yearly | OwaRecurrenceType.YearlyTh);
			}
			else if (this.recurrence.Pattern is DailyRegeneratingPattern)
			{
				result = (OwaRecurrenceType.Daily | OwaRecurrenceType.DailyRegenerating);
			}
			else if (this.recurrence.Pattern is WeeklyRegeneratingPattern)
			{
				result = (OwaRecurrenceType.Weekly | OwaRecurrenceType.WeeklyRegenerating);
			}
			else if (this.recurrence.Pattern is MonthlyRegeneratingPattern)
			{
				result = (OwaRecurrenceType.Monthly | OwaRecurrenceType.MonthlyRegenerating);
			}
			else if (this.recurrence.Pattern is YearlyRegeneratingPattern)
			{
				result = (OwaRecurrenceType.Yearly | OwaRecurrenceType.YearlyRegenerating);
			}
			return result;
		}

		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x060030FC RID: 12540 RVA: 0x0012046F File Offset: 0x0011E66F
		public OwaRecurrenceType ItemRecurrenceType
		{
			get
			{
				return this.recurrenceType;
			}
		}

		// Token: 0x060030FD RID: 12541 RVA: 0x00120477 File Offset: 0x0011E677
		public int RecurrenceInterval()
		{
			if (this.recurrenceType != OwaRecurrenceType.None && this.recurrence != null && this.recurrence.Pattern is IntervalRecurrencePattern)
			{
				return ((IntervalRecurrencePattern)this.recurrence.Pattern).RecurrenceInterval;
			}
			return 1;
		}

		// Token: 0x060030FE RID: 12542 RVA: 0x001204B3 File Offset: 0x0011E6B3
		public int RegeneratingInterval()
		{
			if (this.recurrenceType != OwaRecurrenceType.None && this.recurrence != null && this.recurrence.Pattern is RegeneratingPattern)
			{
				return ((RegeneratingPattern)this.recurrence.Pattern).RecurrenceInterval;
			}
			return 1;
		}

		// Token: 0x060030FF RID: 12543 RVA: 0x001204F0 File Offset: 0x0011E6F0
		public int RecurrenceDays()
		{
			if (this.recurrence == null)
			{
				return 0;
			}
			OwaRecurrenceType owaRecurrenceType = this.recurrenceType;
			if (owaRecurrenceType == OwaRecurrenceType.Weekly)
			{
				return (int)((WeeklyRecurrencePattern)this.recurrence.Pattern).DaysOfWeek;
			}
			if (owaRecurrenceType == (OwaRecurrenceType.Monthly | OwaRecurrenceType.MonthlyTh))
			{
				return (int)((MonthlyThRecurrencePattern)this.recurrence.Pattern).DaysOfWeek;
			}
			if (owaRecurrenceType != (OwaRecurrenceType.Yearly | OwaRecurrenceType.YearlyTh))
			{
				return 0;
			}
			return (int)((YearlyThRecurrencePattern)this.recurrence.Pattern).DaysOfWeek;
		}

		// Token: 0x06003100 RID: 12544 RVA: 0x00120568 File Offset: 0x0011E768
		public int RecurrenceDay()
		{
			if (this.recurrence == null)
			{
				return 1;
			}
			OwaRecurrenceType owaRecurrenceType = this.recurrenceType;
			if (owaRecurrenceType == OwaRecurrenceType.Monthly)
			{
				return ((MonthlyRecurrencePattern)this.recurrence.Pattern).DayOfMonth;
			}
			if (owaRecurrenceType != OwaRecurrenceType.Yearly)
			{
				return 1;
			}
			return ((YearlyRecurrencePattern)this.recurrence.Pattern).DayOfMonth;
		}

		// Token: 0x06003101 RID: 12545 RVA: 0x001205C0 File Offset: 0x0011E7C0
		public int RecurrenceMonth()
		{
			if (this.recurrence == null)
			{
				return 1;
			}
			OwaRecurrenceType owaRecurrenceType = this.recurrenceType;
			if (owaRecurrenceType == OwaRecurrenceType.Yearly)
			{
				return ((YearlyRecurrencePattern)this.recurrence.Pattern).Month;
			}
			if (owaRecurrenceType != (OwaRecurrenceType.Yearly | OwaRecurrenceType.YearlyTh))
			{
				return 1;
			}
			return ((YearlyThRecurrencePattern)this.recurrence.Pattern).Month;
		}

		// Token: 0x06003102 RID: 12546 RVA: 0x0012061C File Offset: 0x0011E81C
		public int RecurrenceOrder()
		{
			if (this.recurrence == null)
			{
				return 0;
			}
			OwaRecurrenceType owaRecurrenceType = this.recurrenceType;
			if (owaRecurrenceType == (OwaRecurrenceType.Monthly | OwaRecurrenceType.MonthlyTh))
			{
				return (int)((MonthlyThRecurrencePattern)this.recurrence.Pattern).Order;
			}
			if (owaRecurrenceType != (OwaRecurrenceType.Yearly | OwaRecurrenceType.YearlyTh))
			{
				return 0;
			}
			return (int)((YearlyThRecurrencePattern)this.recurrence.Pattern).Order;
		}

		// Token: 0x06003103 RID: 12547 RVA: 0x00120679 File Offset: 0x0011E879
		public RecurrenceRangeType ItemRecurrenceRangeType()
		{
			if (this.recurrence == null)
			{
				return RecurrenceRangeType.NoEnd;
			}
			if (this.recurrenceType == OwaRecurrenceType.None)
			{
				return RecurrenceRangeType.NoEnd;
			}
			if (this.recurrence.Range is NumberedRecurrenceRange)
			{
				return RecurrenceRangeType.Numbered;
			}
			if (this.recurrence.Range is EndDateRecurrenceRange)
			{
				return RecurrenceRangeType.EndDate;
			}
			return RecurrenceRangeType.NoEnd;
		}

		// Token: 0x06003104 RID: 12548 RVA: 0x001206BC File Offset: 0x0011E8BC
		public void RenderRecurrenceRangeStart()
		{
			ExDateTime dateTime;
			if (this.recurrenceType != OwaRecurrenceType.None)
			{
				dateTime = this.recurrence.Range.StartDate;
			}
			else
			{
				dateTime = DateTimeUtilities.GetLocalTime();
			}
			RenderingUtilities.RenderDateTimeScriptObject(this.output, dateTime);
		}

		// Token: 0x06003105 RID: 12549 RVA: 0x001206F7 File Offset: 0x0011E8F7
		public int RecurrenceRangeOccurences()
		{
			if (this.recurrenceType != OwaRecurrenceType.None && this.ItemRecurrenceRangeType() == RecurrenceRangeType.Numbered)
			{
				return ((NumberedRecurrenceRange)this.recurrence.Range).NumberOfOccurrences;
			}
			return 10;
		}

		// Token: 0x06003106 RID: 12550 RVA: 0x00120724 File Offset: 0x0011E924
		public void RenderRecurrenceRangeEnd()
		{
			ExDateTime dateTime;
			if (this.recurrenceType != OwaRecurrenceType.None && this.ItemRecurrenceRangeType() == RecurrenceRangeType.EndDate)
			{
				dateTime = ((EndDateRecurrenceRange)this.recurrence.Range).EndDate;
			}
			else
			{
				dateTime = DateTimeUtilities.GetLocalTime();
			}
			RenderingUtilities.RenderDateTimeScriptObject(this.output, dateTime);
		}

		// Token: 0x040021D3 RID: 8659
		private Recurrence recurrence;

		// Token: 0x040021D4 RID: 8660
		private OwaRecurrenceType recurrenceType = OwaRecurrenceType.None;

		// Token: 0x040021D5 RID: 8661
		private TextWriter output;
	}
}

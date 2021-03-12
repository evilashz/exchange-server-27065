using System;
using Microsoft.Exchange.Data.ContentTypes.iCalendar;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000834 RID: 2100
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class TimeZoneRule : CalendarComponentBase
	{
		// Token: 0x06004E2E RID: 20014 RVA: 0x00147EFB File Offset: 0x001460FB
		internal TimeZoneRule(CalendarComponentBase root) : base(root)
		{
		}

		// Token: 0x17001628 RID: 5672
		// (get) Token: 0x06004E2F RID: 20015 RVA: 0x00147F1A File Offset: 0x0014611A
		internal NativeMethods.SystemTime TransitionDate
		{
			get
			{
				return this.transitionDate;
			}
		}

		// Token: 0x17001629 RID: 5673
		// (get) Token: 0x06004E30 RID: 20016 RVA: 0x00147F22 File Offset: 0x00146122
		internal int Offset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x1700162A RID: 5674
		// (get) Token: 0x06004E31 RID: 20017 RVA: 0x00147F2A File Offset: 0x0014612A
		internal ushort Year
		{
			get
			{
				return (ushort)this.start.Year;
			}
		}

		// Token: 0x1700162B RID: 5675
		// (get) Token: 0x06004E32 RID: 20018 RVA: 0x00147F38 File Offset: 0x00146138
		internal Recurrence RecurrenceRule
		{
			get
			{
				return this.icalRecurrence;
			}
		}

		// Token: 0x1700162C RID: 5676
		// (get) Token: 0x06004E33 RID: 20019 RVA: 0x00147F40 File Offset: 0x00146140
		internal bool RuleHasRecurrenceUntilField
		{
			get
			{
				return this.RecurrenceRule != null && this.RecurrenceRule.UntilDateTime > DateTime.MinValue;
			}
		}

		// Token: 0x06004E34 RID: 20020 RVA: 0x00147F64 File Offset: 0x00146164
		protected override void ProcessProperty(CalendarPropertyBase calendarProperty)
		{
			PropertyId propertyId = calendarProperty.CalendarPropertyId.PropertyId;
			if (propertyId != PropertyId.DateTimeStart)
			{
				switch (propertyId)
				{
				case PropertyId.TimeZoneName:
				case PropertyId.TimeZoneOffsetFrom:
					break;
				case PropertyId.TimeZoneOffsetTo:
					this.offset = (int)((TimeSpan)calendarProperty.Value).TotalMinutes;
					return;
				default:
					return;
				}
			}
			else
			{
				this.start = (ExDateTime)((DateTime)calendarProperty.Value);
			}
		}

		// Token: 0x06004E35 RID: 20021 RVA: 0x00147FCC File Offset: 0x001461CC
		protected override bool ValidateProperty(CalendarPropertyBase calendarProperty)
		{
			PropertyId propertyId = calendarProperty.CalendarPropertyId.PropertyId;
			if (propertyId == PropertyId.RecurrenceRule)
			{
				if (this.icalRecurrence != null)
				{
					base.Context.AddError(ServerStrings.InvalidICalElement("VTIMEZONE.TimeZoneRule.RRULE.Duplicate"));
					return false;
				}
				this.icalRecurrence = (Recurrence)calendarProperty.Value;
			}
			return true;
		}

		// Token: 0x06004E36 RID: 20022 RVA: 0x0014801C File Offset: 0x0014621C
		protected override bool ValidateProperties()
		{
			if (this.offset == 2147483647)
			{
				base.Context.AddError(ServerStrings.InvalidICalElement("VTIMEZONE.TimeZoneRule.TZOFFSETTO.Missing"));
				return false;
			}
			this.transitionDate = default(NativeMethods.SystemTime);
			if (this.icalRecurrence != null)
			{
				if (this.icalRecurrence.Frequency != Frequency.Yearly || this.icalRecurrence.Interval != 1 || this.icalRecurrence.ByMonth == null || this.icalRecurrence.ByMonth.Length != 1 || this.icalRecurrence.ByDayList == null || this.icalRecurrence.ByDayList.Length != 1)
				{
					base.Context.AddError(ServerStrings.InvalidICalElement("VTIMEZONE.TimeZoneRule"));
					return false;
				}
				this.transitionDate.Year = 0;
				this.transitionDate.Month = (ushort)this.icalRecurrence.ByMonth[0];
				this.transitionDate.DayOfWeek = (ushort)this.icalRecurrence.ByDayList[0].Day;
				short num = (short)this.icalRecurrence.ByDayList[0].OccurrenceNumber;
				if (num == -1)
				{
					num = 5;
				}
				this.transitionDate.Day = (ushort)num;
				if (this.transitionDate.Month == 0 && (this.transitionDate.DayOfWeek != 0 || this.transitionDate.Day != 0))
				{
					base.Context.AddError(ServerStrings.InvalidICalElement("VTIMEZONE.TimeZoneRule"));
					return false;
				}
			}
			if (this.start != ExDateTime.MaxValue)
			{
				this.transitionDate.Hour = (ushort)this.start.Hour;
				this.transitionDate.Minute = (ushort)this.start.Minute;
				this.transitionDate.Second = (ushort)this.start.Second;
				this.transitionDate.Milliseconds = (ushort)this.start.Millisecond;
			}
			if (this.start != ExDateTime.MaxValue && this.icalRecurrence == null)
			{
				this.transitionDate.Year = (ushort)this.start.Year;
				this.transitionDate.Month = (ushort)this.start.Month;
				this.transitionDate.Day = (ushort)this.start.Day;
			}
			return true;
		}

		// Token: 0x04002AB2 RID: 10930
		private ExDateTime start = ExDateTime.MaxValue;

		// Token: 0x04002AB3 RID: 10931
		private NativeMethods.SystemTime transitionDate;

		// Token: 0x04002AB4 RID: 10932
		private int offset = int.MaxValue;

		// Token: 0x04002AB5 RID: 10933
		private Recurrence icalRecurrence;
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Calendar;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.ContentTypes.iCalendar;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000814 RID: 2068
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class VItemBase : CalendarComponentBase
	{
		// Token: 0x06004D24 RID: 19748 RVA: 0x001402CD File Offset: 0x0013E4CD
		internal VItemBase(CalendarComponentBase root) : base(root)
		{
		}

		// Token: 0x1700160B RID: 5643
		// (get) Token: 0x06004D25 RID: 19749 RVA: 0x001402EF File Offset: 0x0013E4EF
		public Recurrence IcalRecurrence
		{
			get
			{
				if (this.xicalRule == null)
				{
					return this.icalRule;
				}
				return this.xicalRule;
			}
		}

		// Token: 0x1700160C RID: 5644
		// (get) Token: 0x06004D26 RID: 19750 RVA: 0x00140306 File Offset: 0x0013E506
		internal bool HasRRule
		{
			get
			{
				return this.hasRRule;
			}
		}

		// Token: 0x1700160D RID: 5645
		// (get) Token: 0x06004D27 RID: 19751 RVA: 0x0014030E File Offset: 0x0013E50E
		internal string Uid
		{
			get
			{
				return this.uid;
			}
		}

		// Token: 0x06004D28 RID: 19752 RVA: 0x00140316 File Offset: 0x0013E516
		internal void SetProperty(PropertyDefinition propertyDefinition, object propertyValue)
		{
			this.GetPropertyBag().SetProperty(propertyDefinition, propertyValue);
		}

		// Token: 0x06004D29 RID: 19753 RVA: 0x00140328 File Offset: 0x0013E528
		internal object TryGetProperty(PropertyDefinition propertyDefinition)
		{
			StorePropertyDefinition propertyDefinition2 = InternalSchema.ToStorePropertyDefinition(propertyDefinition);
			return this.TryGetProperty(propertyDefinition2);
		}

		// Token: 0x06004D2A RID: 19754 RVA: 0x00140343 File Offset: 0x0013E543
		internal object TryGetProperty(StorePropertyDefinition propertyDefinition)
		{
			return this.GetPropertyBag().TryGetProperty(propertyDefinition);
		}

		// Token: 0x06004D2B RID: 19755 RVA: 0x00140354 File Offset: 0x0013E554
		internal T GetValueOrDefault<T>(StorePropertyDefinition propertyDefinition)
		{
			return this.GetValueOrDefault<T>(propertyDefinition, default(T));
		}

		// Token: 0x06004D2C RID: 19756 RVA: 0x00140371 File Offset: 0x0013E571
		internal T GetValueOrDefault<T>(StorePropertyDefinition propertyDefinition, T defaultValue)
		{
			return PropertyBag.CheckPropertyValue<T>(propertyDefinition, this.TryGetProperty(propertyDefinition), defaultValue);
		}

		// Token: 0x06004D2D RID: 19757 RVA: 0x00140381 File Offset: 0x0013E581
		internal T? GetValueAsNullable<T>(StorePropertyDefinition propertyDefinition) where T : struct
		{
			return PropertyBag.CheckNullablePropertyValue<T>(propertyDefinition, this.TryGetProperty(propertyDefinition));
		}

		// Token: 0x06004D2E RID: 19758 RVA: 0x00140390 File Offset: 0x0013E590
		protected virtual PropertyBag GetPropertyBag()
		{
			return null;
		}

		// Token: 0x06004D2F RID: 19759 RVA: 0x00140394 File Offset: 0x0013E594
		protected override void ProcessProperty(CalendarPropertyBase calendarProperty)
		{
			PropertyId propertyId = calendarProperty.CalendarPropertyId.PropertyId;
			if (propertyId == PropertyId.Unknown)
			{
				if (string.Compare(calendarProperty.CalendarPropertyId.PropertyName, "X-MICROSOFT-RRULE", StringComparison.CurrentCultureIgnoreCase) == 0)
				{
					this.hasRRule = true;
				}
				return;
			}
			if (propertyId == PropertyId.Uid)
			{
				this.uid = (calendarProperty.Value as string);
				return;
			}
			if (propertyId != PropertyId.RecurrenceRule)
			{
				return;
			}
			this.hasRRule = true;
		}

		// Token: 0x06004D30 RID: 19760 RVA: 0x001403F8 File Offset: 0x0013E5F8
		protected override bool ValidateProperty(CalendarPropertyBase calendarProperty)
		{
			bool result = true;
			PropertyId propertyId = calendarProperty.CalendarPropertyId.PropertyId;
			if (propertyId <= PropertyId.DateTimeStart)
			{
				if (propertyId != PropertyId.Unknown)
				{
					if (propertyId == PropertyId.DateTimeStart)
					{
						if (calendarProperty is CalendarDateTime)
						{
							this.dtStart = (CalendarDateTime)calendarProperty;
						}
						else
						{
							result = false;
						}
					}
				}
				else if (string.Compare(calendarProperty.CalendarPropertyId.PropertyName, "X-MICROSOFT-RRULE", StringComparison.CurrentCultureIgnoreCase) == 0)
				{
					Recurrence recurrence = calendarProperty.Value as Recurrence;
					if (this.xicalRule != null || recurrence == null || !this.ValidateRRule(recurrence))
					{
						result = false;
					}
					else
					{
						this.xicalRule = recurrence;
						CalendarParameter parameter = calendarProperty.GetParameter("X-MICROSOFT-ISLEAPMONTH");
						if (parameter != null)
						{
							bool? flag = CalendarUtil.BooleanFromString((string)parameter.Value);
							if (flag == null)
							{
								result = false;
							}
							else
							{
								this.isLeapMonth = flag.Value;
							}
						}
					}
				}
				else if (string.Compare(calendarProperty.CalendarPropertyId.PropertyName, "X-ALT-DESC", StringComparison.CurrentCultureIgnoreCase) == 0)
				{
					result = this.ProcessXAltDesc(calendarProperty);
				}
			}
			else if (propertyId != PropertyId.Uid)
			{
				if (propertyId == PropertyId.RecurrenceRule)
				{
					Recurrence recurrence2 = calendarProperty.Value as Recurrence;
					if (this.icalRule != null || recurrence2 == null || !this.ValidateRRule(recurrence2))
					{
						result = false;
					}
					else
					{
						this.icalRule = recurrence2;
					}
				}
			}
			else
			{
				string text = calendarProperty.Value as string;
				if (text == null)
				{
					result = false;
				}
				else
				{
					this.uid = text;
				}
			}
			return result;
		}

		// Token: 0x06004D31 RID: 19761 RVA: 0x00140558 File Offset: 0x0013E758
		protected override bool ProcessSubComponent(CalendarComponentBase calendarComponent)
		{
			ComponentId componentId = calendarComponent.ComponentId;
			if (componentId == ComponentId.VAlarm)
			{
				VAlarm valarm = (VAlarm)calendarComponent;
				if (valarm.Action == VAlarmAction.Display)
				{
					if (this.displayVAlarm == null)
					{
						this.displayVAlarm = valarm;
					}
					else
					{
						ExTraceGlobals.ICalTracer.TraceDebug((long)this.GetHashCode(), "VItemBase::ProcessSubComponent. Ignoring additional display VALARMs in VItemBase.");
					}
				}
				else if (valarm.Action == VAlarmAction.Email)
				{
					if (this.emailVAlarms == null)
					{
						this.emailVAlarms = new List<VAlarm>();
					}
					if (this.emailVAlarms.Count < 12)
					{
						this.emailVAlarms.Add(valarm);
					}
					else
					{
						ExTraceGlobals.ICalTracer.TraceDebug<int>((long)this.GetHashCode(), "VItemBase::ProcessSubComponent. Ignoring email VALARM in VItemBase because max limit of {0} is reached.", 12);
					}
				}
				else
				{
					ExTraceGlobals.ICalTracer.TraceDebug((long)this.GetHashCode(), "VItemBase::ProcessSubComponent. Ignoring VALARM with unsupported action type in VItemBase.");
				}
			}
			return true;
		}

		// Token: 0x06004D32 RID: 19762 RVA: 0x0014061C File Offset: 0x0013E81C
		private bool ProcessXAltDesc(CalendarPropertyBase calendarProperty)
		{
			this.xAltDesc = (calendarProperty.Value as string);
			if (this.xAltDesc != null)
			{
				this.TweakOutlookSquigglie();
				CalendarParameter parameter = calendarProperty.GetParameter(ParameterId.FormatType);
				if (parameter != null)
				{
					string text = parameter.Value as string;
					if (text != null)
					{
						text = text.ToLowerInvariant();
						string a;
						if ((a = text) != null)
						{
							if (a == "text/plain")
							{
								this.bodyFormat = BodyFormat.TextPlain;
								return true;
							}
							if (a == "text/html")
							{
								this.bodyFormat = BodyFormat.TextHtml;
								return true;
							}
						}
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06004D33 RID: 19763 RVA: 0x001406A4 File Offset: 0x0013E8A4
		private void TweakOutlookSquigglie()
		{
			byte[] bytes = Encoding.Unicode.GetBytes(this.xAltDesc);
			using (MemoryStream memoryStream = new MemoryStream(bytes))
			{
				using (IcalSquigglieFixHtmlReader icalSquigglieFixHtmlReader = new IcalSquigglieFixHtmlReader(memoryStream, Charset.Unicode, true))
				{
					this.xAltDesc = icalSquigglieFixHtmlReader.ReadToEnd();
				}
			}
		}

		// Token: 0x06004D34 RID: 19764 RVA: 0x00140718 File Offset: 0x0013E918
		protected virtual void SetTimeZone(ExTimeZone itemTimeZone)
		{
			if (this.timeZone != null)
			{
				throw new ConversionFailedException(ConversionFailureReason.CorruptContent, null);
			}
			if (this.dtStart != null && !string.IsNullOrEmpty(this.dtStart.TimeZoneId))
			{
				this.timeZone = base.InboundContext.DeclaredTimeZones[this.dtStart.TimeZoneId];
				return;
			}
			if (base.InboundContext.DeclaredTimeZones.Count == 1)
			{
				this.timeZone = base.InboundContext.DeclaredTimeZones.Values.Single<ExTimeZone>();
				return;
			}
			bool flag = itemTimeZone != ExTimeZone.UnspecifiedTimeZone;
			ICalendarIcalConversionSettings calendarIcalConversionSettings = CalendarUtil.GetCalendarIcalConversionSettings();
			if (calendarIcalConversionSettings.LocalTimeZoneReferenceForRecurrenceNeeded)
			{
				flag = (itemTimeZone != ExTimeZone.UtcTimeZone && flag);
			}
			this.timeZone = (flag ? itemTimeZone : (ExTimeZone.CurrentTimeZone ?? ExTimeZone.UtcTimeZone));
		}

		// Token: 0x06004D35 RID: 19765 RVA: 0x001407E4 File Offset: 0x0013E9E4
		protected virtual bool PromoteComplexProperties()
		{
			this.SetProperty(InternalSchema.Importance, (this.importance == -1) ? 1 : this.importance);
			string text = this.xAltDesc ?? this.body;
			if (!string.IsNullOrEmpty(text) && (base.InboundContext.MaxBodyLength == null || (ulong)base.InboundContext.MaxBodyLength.Value >= (ulong)((long)text.Length)))
			{
				BodyWriteConfiguration bodyWriteConfiguration = new BodyWriteConfiguration(this.bodyFormat);
				bodyWriteConfiguration.SetTargetFormat(BodyFormat.ApplicationRtf, base.InboundContext.Charset.Name);
				using (TextWriter textWriter = this.item.Body.OpenTextWriter(bodyWriteConfiguration))
				{
					textWriter.Write(text);
				}
			}
			return true;
		}

		// Token: 0x06004D36 RID: 19766 RVA: 0x001408B8 File Offset: 0x0013EAB8
		protected Recurrence XsoRecurrenceFromICalRecurrence(Recurrence icalRecurrence, ExDateTime start)
		{
			int hour = start.Hour;
			int minute = start.Minute;
			if (icalRecurrence.ByHour != null)
			{
				hour = icalRecurrence.ByHour[0];
			}
			if (icalRecurrence.ByMinute != null)
			{
				minute = icalRecurrence.ByMinute[0];
			}
			start = new ExDateTime(start.TimeZone, start.Year, start.Month, start.Day, hour, minute, 0, 0);
			return this.CreateRecurrence(icalRecurrence, start);
		}

		// Token: 0x06004D37 RID: 19767 RVA: 0x00140928 File Offset: 0x0013EB28
		private bool ValidateRRule(Recurrence rrule)
		{
			return (rrule.BySecond == null || rrule.BySecond.Length == 1) && (rrule.ByMinute == null || rrule.ByMinute.Length == 1) && (rrule.ByHour == null || rrule.ByHour.Length == 1) && (rrule.ByMonth == null || rrule.ByMonth.Length == 1) && (rrule.ByMonthDay == null || rrule.ByMonthDay.Length == 1) && (rrule.BySetPosition == null || rrule.BySetPosition.Length == 1) && rrule.ByWeek == null && rrule.ByYearDay == null && (rrule.BySecond == null || rrule.BySecond[0] == 0) && (rrule.ByDayList == null || rrule.ByDayList.Length != 0) && (rrule.BySetPosition == null || rrule.BySetPosition[0] > 0 || rrule.BySetPosition[0] == -1) && (rrule.Frequency != Frequency.Daily || (rrule.ByMonthDay == null && rrule.ByMonth == null && rrule.BySetPosition == null)) && (rrule.Frequency != Frequency.Weekly || (rrule.ByMonthDay == null && rrule.ByMonth == null && rrule.BySetPosition == null)) && (rrule.Frequency != Frequency.Monthly || rrule.ByMonth == null);
		}

		// Token: 0x06004D38 RID: 19768 RVA: 0x00140A60 File Offset: 0x0013EC60
		private RecurrencePattern CreateYearlyRecurrence(Recurrence icalRecurrence, ExDateTime start, bool isLeapMonth, CalendarType calendarType)
		{
			RecurrencePattern result = null;
			if (icalRecurrence.ByDayList == null && icalRecurrence.BySetPosition == null)
			{
				int dayOfMonth = (icalRecurrence.ByMonthDay != null) ? icalRecurrence.ByMonthDay[0] : start.Day;
				int month = (icalRecurrence.ByMonth != null) ? icalRecurrence.ByMonth[0] : start.Month;
				result = new YearlyRecurrencePattern(dayOfMonth, month, isLeapMonth, icalRecurrence.Interval, calendarType);
			}
			else if (icalRecurrence.ByDayList != null && icalRecurrence.ByMonthDay == null)
			{
				int month2 = (icalRecurrence.ByMonth != null) ? icalRecurrence.ByMonth[0] : start.Month;
				DaysOfWeek daysOfWeek = VItemBase.DaysOfWeekFromByDayList(icalRecurrence.ByDayList, start);
				RecurrenceOrderType? recurrenceOrderType = null;
				int occurrenceNumber = icalRecurrence.ByDayList[0].OccurrenceNumber;
				if (occurrenceNumber == 0 && icalRecurrence.BySetPosition != null)
				{
					recurrenceOrderType = new RecurrenceOrderType?((RecurrenceOrderType)icalRecurrence.BySetPosition[0]);
				}
				else if (occurrenceNumber != 0 && icalRecurrence.BySetPosition == null)
				{
					recurrenceOrderType = new RecurrenceOrderType?((RecurrenceOrderType)occurrenceNumber);
				}
				if (recurrenceOrderType != null)
				{
					result = new YearlyThRecurrencePattern(daysOfWeek, recurrenceOrderType.Value, month2, isLeapMonth, icalRecurrence.Interval, calendarType);
				}
			}
			return result;
		}

		// Token: 0x06004D39 RID: 19769 RVA: 0x00140B78 File Offset: 0x0013ED78
		private RecurrencePattern CreateMonthlyRecurrence(Recurrence icalRecurrence, ExDateTime start, CalendarType calendarType)
		{
			RecurrencePattern result = null;
			if (icalRecurrence.ByDayList == null && icalRecurrence.BySetPosition == null)
			{
				int dayOfMonth = (icalRecurrence.ByMonthDay != null) ? icalRecurrence.ByMonthDay[0] : start.Day;
				result = new MonthlyRecurrencePattern(dayOfMonth, icalRecurrence.Interval, calendarType);
			}
			else if (icalRecurrence.ByDayList != null && icalRecurrence.ByMonthDay == null)
			{
				DaysOfWeek daysOfWeek = VItemBase.DaysOfWeekFromByDayList(icalRecurrence.ByDayList, start);
				int occurrenceNumber = icalRecurrence.ByDayList[0].OccurrenceNumber;
				RecurrenceOrderType? recurrenceOrderType = null;
				if (icalRecurrence.BySetPosition != null)
				{
					if (occurrenceNumber == 0 && icalRecurrence.BySetPosition[0] >= -1 && icalRecurrence.BySetPosition[0] <= 4 && icalRecurrence.BySetPosition[0] != 0)
					{
						recurrenceOrderType = new RecurrenceOrderType?((RecurrenceOrderType)icalRecurrence.BySetPosition[0]);
					}
				}
				else if (occurrenceNumber != 0)
				{
					recurrenceOrderType = new RecurrenceOrderType?((RecurrenceOrderType)occurrenceNumber);
				}
				if (recurrenceOrderType != null)
				{
					result = new MonthlyThRecurrencePattern(daysOfWeek, recurrenceOrderType.Value, icalRecurrence.Interval, calendarType);
				}
			}
			return result;
		}

		// Token: 0x06004D3A RID: 19770 RVA: 0x00140C68 File Offset: 0x0013EE68
		private RecurrencePattern CreateWeeklyRecurrence(Recurrence icalRecurrence, ExDateTime start)
		{
			RecurrencePattern result;
			if (icalRecurrence.ByDayList != null)
			{
				DaysOfWeek daysOfWeek = VItemBase.DaysOfWeekFromByDayList(icalRecurrence.ByDayList, start);
				result = new WeeklyRecurrencePattern(daysOfWeek, icalRecurrence.Interval, icalRecurrence.WorkWeekStart);
			}
			else
			{
				result = new WeeklyRecurrencePattern((DaysOfWeek)(1 << (int)start.DayOfWeek), icalRecurrence.Interval, icalRecurrence.WorkWeekStart);
			}
			return result;
		}

		// Token: 0x06004D3B RID: 19771 RVA: 0x00140CC0 File Offset: 0x0013EEC0
		private RecurrencePattern CreateDailyRecurrence(Recurrence icalRecurrence, ExDateTime start)
		{
			if (icalRecurrence.ByDayList == null)
			{
				return new DailyRecurrencePattern(icalRecurrence.Interval);
			}
			return this.CreateWeeklyRecurrence(icalRecurrence, start);
		}

		// Token: 0x06004D3C RID: 19772 RVA: 0x00140CE0 File Offset: 0x0013EEE0
		private Recurrence CreateRecurrence(Recurrence icalRecurrence, ExDateTime start)
		{
			Recurrence result = null;
			RecurrenceRange recurrenceRange = null;
			Exception ex = null;
			try
			{
				RecurrencePattern recurrencePattern;
				switch (icalRecurrence.Frequency)
				{
				case Frequency.Daily:
					recurrencePattern = this.CreateDailyRecurrence(icalRecurrence, start);
					break;
				case Frequency.Weekly:
					recurrencePattern = this.CreateWeeklyRecurrence(icalRecurrence, start);
					break;
				case Frequency.Monthly:
					recurrencePattern = this.CreateMonthlyRecurrence(icalRecurrence, start, base.Context.Type);
					break;
				case Frequency.Yearly:
					recurrencePattern = this.CreateYearlyRecurrence(icalRecurrence, start, this.isLeapMonth, base.Context.Type);
					break;
				default:
					ExTraceGlobals.ICalTracer.TraceError<string, Frequency>((long)this.GetHashCode(), "VItemBase::CreateRecurrence. Unsupported recurrence pattern. UID:'{0}'. Frequency:'{1}'.", this.Uid, icalRecurrence.Frequency);
					base.Context.AddError(ServerStrings.InvalidICalElement("RRULE.UnSupportedRecurrencePattern"));
					return null;
				}
				if (!this.CreateRange(icalRecurrence, start, out recurrenceRange))
				{
					return null;
				}
				if (recurrencePattern != null && recurrenceRange != null)
				{
					result = new Recurrence(recurrencePattern, recurrenceRange);
				}
			}
			catch (ArgumentException ex2)
			{
				ex = ex2;
			}
			catch (RecurrenceException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				ExTraceGlobals.ICalTracer.TraceError<string, Exception>((long)this.GetHashCode(), "VEvent::CreateRecurrence. UID:'{0}'. Found exception:'{1}'", this.Uid, ex);
				base.Context.AddError(ServerStrings.InvalidICalElement("RRULE"));
			}
			return result;
		}

		// Token: 0x06004D3D RID: 19773 RVA: 0x00140E20 File Offset: 0x0013F020
		private bool CreateRange(Recurrence icalRecurrence, ExDateTime start, out RecurrenceRange range)
		{
			range = null;
			ExDateTime exDateTime = (ExDateTime)icalRecurrence.UntilDateTime;
			if (exDateTime == ExDateTime.MinValue)
			{
				exDateTime = (ExDateTime)icalRecurrence.UntilDate;
			}
			if (exDateTime != ExDateTime.MinValue)
			{
				if (!(exDateTime > start))
				{
					range = null;
					ExTraceGlobals.ICalTracer.TraceError<string>((long)this.GetHashCode(), "VEvent::CreateRange. Invalid until date in recurrence range. UID:'{0}'.", this.Uid);
					base.Context.AddError(ServerStrings.InvalidICalElement("RRULE.InvalidRecurrenceUntil"));
					return false;
				}
				range = new EndDateRecurrenceRange(start, start.TimeZone.ConvertDateTime(exDateTime));
			}
			else if (icalRecurrence.Count > 0)
			{
				if (!NumberedRecurrenceRange.IsValidNumberedRecurrenceRange(icalRecurrence.Count))
				{
					range = null;
					ExTraceGlobals.ICalTracer.TraceError<string>((long)this.GetHashCode(), "VItemBase::CreateRange. Invalid number of occurrences in recurrence range. UID:'{0}'.", this.Uid);
					base.Context.AddError(ServerStrings.InvalidICalElement("RRULE.InvalidRecurrenceCount"));
					return false;
				}
				range = new NumberedRecurrenceRange(start, icalRecurrence.Count);
			}
			else
			{
				range = new NoEndRecurrenceRange(start);
			}
			return true;
		}

		// Token: 0x06004D3E RID: 19774 RVA: 0x00140F20 File Offset: 0x0013F120
		protected Recurrence ICalRecurrenceFromXsoRecurrence(Recurrence xsoRecurrence)
		{
			Recurrence recurrence = new Recurrence();
			RecurrencePattern pattern = xsoRecurrence.Pattern;
			RecurrenceRange range = xsoRecurrence.Range;
			if (pattern is DailyRecurrencePattern)
			{
				recurrence.Frequency = Frequency.Daily;
			}
			else if (pattern is WeeklyRecurrencePattern)
			{
				recurrence.Frequency = Frequency.Weekly;
				WeeklyRecurrencePattern weeklyRecurrencePattern = (WeeklyRecurrencePattern)pattern;
				recurrence.ByDayList = VItemBase.ByDayListFromDaysOfWeek(weeklyRecurrencePattern.DaysOfWeek);
				recurrence.WorkWeekStart = weeklyRecurrencePattern.FirstDayOfWeek;
			}
			else if (pattern is MonthlyRecurrencePattern)
			{
				recurrence.Frequency = Frequency.Monthly;
				MonthlyRecurrencePattern monthlyRecurrencePattern = (MonthlyRecurrencePattern)pattern;
				recurrence.ByMonthDay = new int[]
				{
					monthlyRecurrencePattern.DayOfMonth
				};
			}
			else if (pattern is MonthlyThRecurrencePattern)
			{
				recurrence.Frequency = Frequency.Monthly;
				MonthlyThRecurrencePattern monthlyThRecurrencePattern = (MonthlyThRecurrencePattern)pattern;
				recurrence.ByDayList = VItemBase.ByDayListFromDaysOfWeek(monthlyThRecurrencePattern.DaysOfWeek);
				int order = (int)monthlyThRecurrencePattern.Order;
				if (recurrence.ByDayList.Length == 1)
				{
					recurrence.ByDayList[0].OccurrenceNumber = order;
				}
				else
				{
					recurrence.BySetPosition = new int[]
					{
						order
					};
				}
			}
			else if (pattern is YearlyRecurrencePattern)
			{
				recurrence.Frequency = Frequency.Yearly;
				YearlyRecurrencePattern yearlyRecurrencePattern = (YearlyRecurrencePattern)pattern;
				recurrence.ByMonth = new int[]
				{
					yearlyRecurrencePattern.Month
				};
				recurrence.ByMonthDay = new int[]
				{
					yearlyRecurrencePattern.DayOfMonth
				};
			}
			else if (pattern is YearlyThRecurrencePattern)
			{
				recurrence.Frequency = Frequency.Yearly;
				YearlyThRecurrencePattern yearlyThRecurrencePattern = (YearlyThRecurrencePattern)pattern;
				recurrence.ByMonth = new int[]
				{
					yearlyThRecurrencePattern.Month
				};
				recurrence.ByDayList = VItemBase.ByDayListFromDaysOfWeek(yearlyThRecurrencePattern.DaysOfWeek);
				int order2 = (int)yearlyThRecurrencePattern.Order;
				if (recurrence.ByDayList.Length == 1)
				{
					recurrence.ByDayList[0].OccurrenceNumber = order2;
				}
				else
				{
					recurrence.BySetPosition = new int[]
					{
						order2
					};
				}
			}
			IntervalRecurrencePattern intervalRecurrencePattern = pattern as IntervalRecurrencePattern;
			if (intervalRecurrencePattern != null)
			{
				recurrence.Interval = intervalRecurrencePattern.RecurrenceInterval;
			}
			else
			{
				recurrence.Interval = 1;
			}
			if (range is EndDateRecurrenceRange)
			{
				EndDateRecurrenceRange endDateRecurrenceRange = (EndDateRecurrenceRange)range;
				recurrence.UntilDateTime = (DateTime)(endDateRecurrenceRange.EndDate + xsoRecurrence.StartOffset).ToUtc();
			}
			else if (range is NumberedRecurrenceRange)
			{
				NumberedRecurrenceRange numberedRecurrenceRange = (NumberedRecurrenceRange)range;
				recurrence.Count = numberedRecurrenceRange.NumberOfOccurrences;
			}
			return recurrence;
		}

		// Token: 0x06004D3F RID: 19775 RVA: 0x00141184 File Offset: 0x0013F384
		private static Recurrence.ByDay[] ByDayListFromDaysOfWeek(DaysOfWeek daysOfWeek)
		{
			List<Recurrence.ByDay> list = new List<Recurrence.ByDay>();
			int num = (int)daysOfWeek;
			for (int i = 0; i < 7; i++)
			{
				if ((num & 1) == 1)
				{
					list.Add(new Recurrence.ByDay((DayOfWeek)i, 0));
				}
				num >>= 1;
			}
			return list.ToArray();
		}

		// Token: 0x06004D40 RID: 19776 RVA: 0x001411C4 File Offset: 0x0013F3C4
		private static DaysOfWeek DaysOfWeekFromByDayList(Recurrence.ByDay[] byDayList, ExDateTime start)
		{
			DaysOfWeek daysOfWeek = DaysOfWeek.None;
			foreach (Recurrence.ByDay byDay in byDayList)
			{
				if (byDay.OccurrenceNumber != 0 && byDayList.Length > 1)
				{
					throw new ConversionFailedException(ConversionFailureReason.CorruptContent, ServerStrings.InvalidICalElement("BYDAYLIST"), null);
				}
				daysOfWeek |= (DaysOfWeek)(1 << (int)byDay.Day);
			}
			return daysOfWeek;
		}

		// Token: 0x06004D41 RID: 19777 RVA: 0x00141224 File Offset: 0x0013F424
		protected void DemoteAttachments()
		{
			if (this.item.AttachmentCollection == null || this.item.AttachmentCollection.Count == 0)
			{
				return;
			}
			ICollection<PropertyDefinition> collection = new List<PropertyDefinition>(1);
			collection.Add(AttachmentSchema.AttachContentId);
			foreach (AttachmentHandle handle in this.item.AttachmentCollection)
			{
				using (Attachment attachment = this.item.AttachmentCollection.Open(handle, collection))
				{
					string attachmentContentId = ItemToMimeConverter.GetAttachmentContentId(attachment, base.OutboundContext.AttachmentLinks);
					if (attachmentContentId == null || attachmentContentId.Trim().Length == 0)
					{
						ExTraceGlobals.ICalTracer.TraceError<string>((long)this.GetHashCode(), "VItemBase::DemoteAttachments. Incomplete attachment content id. Item:{0}", this.item.Id.ToString());
						base.Context.AddError(ServerStrings.InvalidICalElement("Attachment.ContentId"));
					}
					else
					{
						this.calendarWriter.StartProperty(PropertyId.Attachment);
						this.calendarWriter.WritePropertyValue("CID:" + attachmentContentId);
					}
				}
			}
		}

		// Token: 0x06004D42 RID: 19778 RVA: 0x00141358 File Offset: 0x0013F558
		protected void DemoteEmailReminders()
		{
			if (base.Context.Method != CalendarMethod.Request && base.Context.Method != CalendarMethod.Publish)
			{
				return;
			}
			Reminders<EventTimeBasedInboxReminder> reminders = Reminders<EventTimeBasedInboxReminder>.Get(this.item, InternalSchema.EventTimeBasedInboxReminders);
			if (reminders != null)
			{
				foreach (EventTimeBasedInboxReminder eventTimeBasedInboxReminder in reminders.ReminderList)
				{
					TimeSpan minutes = TimeSpan.FromMinutes((double)eventTimeBasedInboxReminder.ReminderOffset).Negate();
					VAlarm.Demote(base.OutboundContext.Writer, minutes, eventTimeBasedInboxReminder.CustomMessage, this.item.Session.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
				}
			}
		}

		// Token: 0x06004D43 RID: 19779 RVA: 0x00141430 File Offset: 0x0013F630
		protected void DemoteSimpleProperty(SchemaInfo schemaInfo)
		{
			PropertyDefinition propertyDefinition = (PropertyDefinition)schemaInfo.DemotionMethod;
			object obj = this.TryGetProperty(propertyDefinition);
			if (!(obj is PropertyError))
			{
				if (schemaInfo.CalendarPropertyId.PropertyId != PropertyId.Unknown)
				{
					this.calendarWriter.StartProperty(schemaInfo.CalendarPropertyId.PropertyId);
				}
				else
				{
					this.calendarWriter.StartProperty(schemaInfo.CalendarPropertyId.PropertyName);
				}
				if (obj is ExDateTime)
				{
					VItemBase.DemoteDateTimeValue(this, (ExDateTime)obj);
					return;
				}
				this.calendarWriter.WritePropertyValue(obj);
			}
		}

		// Token: 0x06004D44 RID: 19780 RVA: 0x001414B8 File Offset: 0x0013F6B8
		protected static void DemoteDateTimeValue(VItemBase vevent, ExDateTime dateTime)
		{
			if (dateTime.TimeZone != ExTimeZone.UtcTimeZone)
			{
				vevent.calendarWriter.StartParameter(ParameterId.TimeZoneId);
				vevent.calendarWriter.WriteParameterValue(dateTime.TimeZone.AlternativeId);
			}
			vevent.calendarWriter.WritePropertyValue((DateTime)dateTime);
		}

		// Token: 0x06004D45 RID: 19781 RVA: 0x0014150B File Offset: 0x0013F70B
		protected void DemoteRecurrenceProperties(Recurrence recurrence)
		{
			if (recurrence == null)
			{
				return;
			}
			if (Recurrence.IsGregorianCompatible(base.Context.Type))
			{
				this.DemoteRecurrenceProperties(recurrence, "RRULE", "EXDATE");
				return;
			}
			this.DemoteRecurrenceProperties(recurrence, "X-MICROSOFT-RRULE", "X-MICROSOFT-EXDATE");
		}

		// Token: 0x06004D46 RID: 19782 RVA: 0x00141548 File Offset: 0x0013F748
		private void DemoteRecurrenceProperties(Recurrence recurrence, string rRuleName, string exDateName)
		{
			Recurrence value = this.ICalRecurrenceFromXsoRecurrence(recurrence);
			this.calendarWriter.StartProperty(rRuleName);
			if (base.Context.Type != CalendarType.Default)
			{
				this.calendarWriter.StartParameter(ParameterId.ValueType);
				this.calendarWriter.WriteParameterValue("RECUR");
				IYearlyPatternInfo yearlyPatternInfo = recurrence.Pattern as IYearlyPatternInfo;
				if (yearlyPatternInfo != null)
				{
					this.calendarWriter.StartParameter("X-MICROSOFT-ISLEAPMONTH");
					this.calendarWriter.WriteParameterValue(yearlyPatternInfo.IsLeapMonth ? "TRUE" : "FALSE");
				}
			}
			this.calendarWriter.WritePropertyValue(value);
			ExDateTime[] deletedOccurrences = recurrence.GetDeletedOccurrences();
			if (deletedOccurrences.Length > 0)
			{
				this.calendarWriter.StartProperty(exDateName);
				if (base.Context.Type != CalendarType.Default)
				{
					this.calendarWriter.StartParameter(ParameterId.ValueType);
					this.calendarWriter.WriteParameterValue("DATE-TIME");
				}
				this.calendarWriter.StartParameter(ParameterId.TimeZoneId);
				this.calendarWriter.WriteParameterValue(recurrence.ReadExTimeZone.AlternativeId);
				foreach (ExDateTime exDateTime in deletedOccurrences)
				{
					this.calendarWriter.WritePropertyValue((DateTime)exDateTime);
				}
			}
		}

		// Token: 0x06004D47 RID: 19783 RVA: 0x00141688 File Offset: 0x0013F888
		protected void DemoteBody(Body body)
		{
			string value = null;
			using (TextReader textReader = body.OpenTextReader(BodyFormat.TextPlain))
			{
				value = textReader.ReadToEnd();
			}
			if (!string.IsNullOrEmpty(value))
			{
				if (CalendarUtil.IsReplyOrCounter(base.Context.Method))
				{
					this.calendarWriter.StartProperty(PropertyId.Comment);
				}
				else
				{
					this.calendarWriter.StartProperty(PropertyId.Description);
				}
				if (!string.IsNullOrEmpty(this.itemLanguageName))
				{
					this.calendarWriter.StartParameter(ParameterId.Language);
					this.calendarWriter.WriteParameterValue(this.itemLanguageName);
				}
				this.calendarWriter.WritePropertyValue(value);
			}
		}

		// Token: 0x06004D48 RID: 19784 RVA: 0x00141734 File Offset: 0x0013F934
		protected void DemoteReminder()
		{
			if (base.Context.Method != CalendarMethod.Request && base.Context.Method != CalendarMethod.Publish)
			{
				return;
			}
			if (!this.item.GetValueOrDefault<bool>(InternalSchema.ReminderIsSetInternal))
			{
				return;
			}
			int? valueAsNullable = this.item.GetValueAsNullable<int>(InternalSchema.ReminderMinutesBeforeStartInternal);
			if (valueAsNullable == null)
			{
				return;
			}
			valueAsNullable = new int?(Reminder.NormalizeMinutesBeforeStart(valueAsNullable.Value, 15));
			TimeSpan minutes = ((double)valueAsNullable.Value != TimeSpan.MinValue.TotalMinutes) ? TimeSpan.FromMinutes((double)valueAsNullable.Value).Negate() : TimeSpan.FromMinutes(-15.0);
			VAlarm.Demote(base.OutboundContext.Writer, minutes, "REMINDER", null);
		}

		// Token: 0x06004D49 RID: 19785 RVA: 0x001417F5 File Offset: 0x0013F9F5
		protected static bool PromoteDescription(VItemBase vitem, CalendarPropertyBase property)
		{
			vitem.body = (string)property.Value;
			return true;
		}

		// Token: 0x06004D4A RID: 19786 RVA: 0x00141809 File Offset: 0x0013FA09
		protected static bool PromoteComment(VItemBase vitem, CalendarPropertyBase property)
		{
			if (vitem.body == null)
			{
				vitem.body = (string)property.Value;
			}
			return true;
		}

		// Token: 0x06004D4B RID: 19787 RVA: 0x00141828 File Offset: 0x0013FA28
		protected static bool PromoteClass(VItemBase vitem, CalendarPropertyBase property)
		{
			string text = (string)property.Value;
			string a;
			if ((a = text.ToUpper()) != null)
			{
				if (a == "CONFIDENTIAL")
				{
					vitem.SetProperty(InternalSchema.Sensitivity, Sensitivity.CompanyConfidential);
					return true;
				}
				if (a == "PRIVATE")
				{
					vitem.SetProperty(InternalSchema.Sensitivity, Sensitivity.Private);
					return true;
				}
				if (a == "PERSONAL")
				{
					vitem.SetProperty(InternalSchema.Sensitivity, Sensitivity.Personal);
					return true;
				}
				if (!(a == "PUBLIC"))
				{
				}
			}
			vitem.SetProperty(InternalSchema.Sensitivity, Sensitivity.Normal);
			return true;
		}

		// Token: 0x06004D4C RID: 19788 RVA: 0x001418CC File Offset: 0x0013FACC
		protected static bool PromotePriority(VItemBase vitem, CalendarPropertyBase property)
		{
			if (vitem.importance == -1)
			{
				int num = (int)property.Value;
				if (1 <= num && num <= 4)
				{
					vitem.importance = 2;
				}
				else if (6 <= num && num <= 9)
				{
					vitem.importance = 0;
				}
				else
				{
					vitem.importance = 1;
				}
			}
			return true;
		}

		// Token: 0x06004D4D RID: 19789 RVA: 0x0014191C File Offset: 0x0013FB1C
		protected static bool PromoteXImportance(VItemBase vitem, CalendarPropertyBase property)
		{
			if (!int.TryParse((string)property.Value, out vitem.importance))
			{
				ExTraceGlobals.ICalTracer.TraceError<LocalizedString>((long)vitem.GetHashCode(), "VEvent::PromoteXImportance. {0}.", ServerStrings.InvalidICalElement(property.CalendarPropertyId.PropertyId.ToString()));
				vitem.importance = 1;
			}
			return true;
		}

		// Token: 0x06004D4E RID: 19790 RVA: 0x00141979 File Offset: 0x0013FB79
		protected static bool PromoteSubject(VItemBase vitem, CalendarPropertyBase property)
		{
			if (vitem.Context.Method == CalendarMethod.Publish || vitem.GetValueOrDefault<string>(InternalSchema.Subject) == null)
			{
				vitem.SetProperty(InternalSchema.Subject, property.Value);
			}
			return true;
		}

		// Token: 0x06004D4F RID: 19791 RVA: 0x001419A8 File Offset: 0x0013FBA8
		protected static void DemoteClass(VItemBase vitem)
		{
			string value;
			switch (vitem.GetValueOrDefault<Sensitivity>(InternalSchema.Sensitivity))
			{
			case Sensitivity.Normal:
				value = "PUBLIC";
				goto IL_44;
			case Sensitivity.Personal:
				value = "PERSONAL";
				goto IL_44;
			case Sensitivity.CompanyConfidential:
				value = "CONFIDENTIAL";
				goto IL_44;
			}
			value = "PRIVATE";
			IL_44:
			vitem.calendarWriter.StartProperty(PropertyId.Class);
			vitem.calendarWriter.WritePropertyValue(value);
		}

		// Token: 0x06004D50 RID: 19792 RVA: 0x00141A14 File Offset: 0x0013FC14
		protected static void DemoteXImportance(VItemBase vitem)
		{
			int valueOrDefault = vitem.GetValueOrDefault<int>(InternalSchema.Importance, 1);
			vitem.calendarWriter.StartProperty("X-MICROSOFT-CDO-IMPORTANCE");
			vitem.calendarWriter.WritePropertyValue(valueOrDefault);
		}

		// Token: 0x04002A05 RID: 10757
		protected string uid = string.Empty;

		// Token: 0x04002A06 RID: 10758
		protected int importance = -1;

		// Token: 0x04002A07 RID: 10759
		protected VAlarm displayVAlarm;

		// Token: 0x04002A08 RID: 10760
		protected List<VAlarm> emailVAlarms;

		// Token: 0x04002A09 RID: 10761
		protected Recurrence icalRule;

		// Token: 0x04002A0A RID: 10762
		protected Recurrence xicalRule;

		// Token: 0x04002A0B RID: 10763
		protected Recurrence xsoRecurrence;

		// Token: 0x04002A0C RID: 10764
		protected CalendarDateTime dtStart;

		// Token: 0x04002A0D RID: 10765
		protected bool isLeapMonth;

		// Token: 0x04002A0E RID: 10766
		protected string body;

		// Token: 0x04002A0F RID: 10767
		protected string xAltDesc;

		// Token: 0x04002A10 RID: 10768
		protected BodyFormat bodyFormat = BodyFormat.TextPlain;

		// Token: 0x04002A11 RID: 10769
		protected bool hasRRule;

		// Token: 0x04002A12 RID: 10770
		protected Item item;

		// Token: 0x04002A13 RID: 10771
		protected ExTimeZone timeZone;

		// Token: 0x04002A14 RID: 10772
		protected string itemLanguageName;

		// Token: 0x04002A15 RID: 10773
		protected CalendarWriter calendarWriter;
	}
}

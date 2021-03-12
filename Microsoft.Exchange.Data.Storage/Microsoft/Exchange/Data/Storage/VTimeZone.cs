using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.ContentTypes.iCalendar;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000833 RID: 2099
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class VTimeZone : CalendarComponentBase
	{
		// Token: 0x06004E20 RID: 20000 RVA: 0x00147634 File Offset: 0x00145834
		internal VTimeZone(CalendarComponentBase root) : base(root)
		{
			this.standardRules = new Dictionary<ushort, TimeZoneRule>();
			this.daylightRules = new Dictionary<ushort, TimeZoneRule>();
		}

		// Token: 0x06004E21 RID: 20001 RVA: 0x00147653 File Offset: 0x00145853
		internal VTimeZone(CalendarComponentBase root, REG_TIMEZONE_INFO tzInfo, string timeZoneId) : base(root)
		{
			this.timeZoneInfo = tzInfo;
			this.timeZoneId = timeZoneId;
		}

		// Token: 0x06004E22 RID: 20002 RVA: 0x0014766C File Offset: 0x0014586C
		internal void Demote()
		{
			TimeSpan transitionBiasTimeSpan = VTimeZone.GetTransitionBiasTimeSpan(this.timeZoneInfo.Bias, this.timeZoneInfo.StandardBias);
			TimeSpan transitionBiasTimeSpan2 = VTimeZone.GetTransitionBiasTimeSpan(this.timeZoneInfo.Bias, this.timeZoneInfo.DaylightBias);
			Recurrence recurrence = null;
			Recurrence recurrence2 = null;
			ExDateTime exDateTime;
			VTimeZone.GetStartAndRuleFromTimeZoneTransition(this.timeZoneInfo.StandardDate, out exDateTime, out recurrence);
			ExDateTime exDateTime2;
			VTimeZone.GetStartAndRuleFromTimeZoneTransition(this.timeZoneInfo.DaylightDate, out exDateTime2, out recurrence2);
			base.OutboundContext.Writer.StartComponent(ComponentId.VTimeZone);
			base.OutboundContext.Writer.WriteProperty(PropertyId.TimeZoneId, this.timeZoneId);
			base.OutboundContext.Writer.StartComponent(ComponentId.Standard);
			base.OutboundContext.Writer.StartProperty(PropertyId.DateTimeStart);
			base.OutboundContext.Writer.WritePropertyValue((DateTime)exDateTime);
			base.OutboundContext.Writer.StartProperty(PropertyId.TimeZoneOffsetFrom);
			base.OutboundContext.Writer.WritePropertyValue(transitionBiasTimeSpan2);
			base.OutboundContext.Writer.StartProperty(PropertyId.TimeZoneOffsetTo);
			base.OutboundContext.Writer.WritePropertyValue(transitionBiasTimeSpan);
			if (recurrence != null)
			{
				base.OutboundContext.Writer.StartProperty(PropertyId.RecurrenceRule);
				base.OutboundContext.Writer.WritePropertyValue(recurrence);
			}
			base.OutboundContext.Writer.EndComponent();
			base.OutboundContext.Writer.StartComponent(ComponentId.Daylight);
			base.OutboundContext.Writer.StartProperty(PropertyId.DateTimeStart);
			base.OutboundContext.Writer.WritePropertyValue((DateTime)exDateTime2);
			base.OutboundContext.Writer.StartProperty(PropertyId.TimeZoneOffsetFrom);
			base.OutboundContext.Writer.WritePropertyValue(transitionBiasTimeSpan);
			base.OutboundContext.Writer.StartProperty(PropertyId.TimeZoneOffsetTo);
			base.OutboundContext.Writer.WritePropertyValue(transitionBiasTimeSpan2);
			if (recurrence2 != null)
			{
				base.OutboundContext.Writer.StartProperty(PropertyId.RecurrenceRule);
				base.OutboundContext.Writer.WritePropertyValue(recurrence2);
			}
			base.OutboundContext.Writer.EndComponent();
			base.OutboundContext.Writer.EndComponent();
		}

		// Token: 0x06004E23 RID: 20003 RVA: 0x00147890 File Offset: 0x00145A90
		internal ExTimeZone Promote()
		{
			ExTimeZone result = null;
			TimeZoneRule timeZoneRule = null;
			TimeZoneRule timeZoneRule2 = null;
			SortedSet<ushort> sortedSet = new SortedSet<ushort>();
			VTimeZone.CollectAllRuleYears(this.standardRules, sortedSet);
			VTimeZone.CollectAllRuleYears(this.daylightRules, sortedSet);
			List<RegistryTimeZoneRule> list = new List<RegistryTimeZoneRule>(sortedSet.Count);
			foreach (ushort num in sortedSet)
			{
				TimeZoneRule timeZoneRule3 = VTimeZone.UpdateAndGetCurrentRule(num, this.daylightRules, ref timeZoneRule2);
				TimeZoneRule timeZoneRule4 = VTimeZone.UpdateAndGetCurrentRule(num, this.standardRules, ref timeZoneRule);
				if (timeZoneRule3 != null || timeZoneRule4 != null)
				{
					list.Add(new RegistryTimeZoneRule((int)num, this.GetNativeTimeZoneRule(timeZoneRule4, timeZoneRule3)));
				}
			}
			try
			{
				result = TimeZoneHelper.CreateCustomExTimeZoneFromRegRules(list[0].RegTimeZoneInfo, this.timeZoneId, this.timeZoneId, list);
			}
			catch (InvalidTimeZoneException ex)
			{
				ExTraceGlobals.ICalTracer.TraceDebug<string>((long)this.GetHashCode(), "ToExTimeZone::ToExTimeZone. Following error found when construct customized time zone: {0}", ex.Message);
				base.Context.AddError(ServerStrings.InvalidICalElement("VTIMEZONE"));
			}
			return result;
		}

		// Token: 0x06004E24 RID: 20004 RVA: 0x001479B4 File Offset: 0x00145BB4
		protected override void ProcessProperty(CalendarPropertyBase calendarProperty)
		{
			PropertyId propertyId = calendarProperty.CalendarPropertyId.PropertyId;
			if (propertyId != PropertyId.TimeZoneId)
			{
				return;
			}
			this.timeZoneId = CalendarUtil.RemoveDoubleQuotes((string)calendarProperty.Value);
		}

		// Token: 0x06004E25 RID: 20005 RVA: 0x001479EC File Offset: 0x00145BEC
		protected override bool ProcessSubComponent(CalendarComponentBase calendarComponent)
		{
			bool result = true;
			TimeZoneRule timeZoneRule = calendarComponent as TimeZoneRule;
			if (timeZoneRule != null)
			{
				ushort year = timeZoneRule.Year;
				ComponentId componentId = calendarComponent.ComponentId;
				if (componentId != ComponentId.Standard)
				{
					if (componentId == ComponentId.Daylight)
					{
						if (this.daylightRules.ContainsKey(year))
						{
							ExTraceGlobals.ICalTracer.TraceError<ushort>(0L, "VTimeZone::ProcessSubComponent:ComponentId.Daylight. Ignoring the repeated year timezone definition. Year: {0}", year);
						}
						else
						{
							this.daylightRules.Add(year, timeZoneRule);
						}
					}
				}
				else if (this.standardRules.ContainsKey(year))
				{
					ExTraceGlobals.ICalTracer.TraceError<ushort>(0L, "VTimeZone::ProcessSubComponent:ComponentId.Standard. Ignoring the repeated year timezone definition. Year: {0}", year);
				}
				else
				{
					this.standardRules.Add(year, timeZoneRule);
				}
			}
			return result;
		}

		// Token: 0x06004E26 RID: 20006 RVA: 0x00147A8C File Offset: 0x00145C8C
		protected override bool ValidateProperties()
		{
			if (string.IsNullOrEmpty(this.timeZoneId))
			{
				return false;
			}
			if (this.standardRules.Count == 0 && this.daylightRules.Count == 0)
			{
				return false;
			}
			foreach (TimeZoneRule timeZoneRule in this.standardRules.Values.Union(this.daylightRules.Values))
			{
				if (!timeZoneRule.Validate())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004E27 RID: 20007 RVA: 0x00147B24 File Offset: 0x00145D24
		private static TimeSpan GetTransitionBiasTimeSpan(int bias, int transitionBias)
		{
			TimeSpan t = new TimeSpan(600000000L * (long)(bias + transitionBias));
			if (t == TimeSpan.MinValue)
			{
				ExTraceGlobals.ICalTracer.TraceError<int, int>(0L, "VTimeZone::GetTransitionBiasTimeSpan. The timezone is invalid.\r\nBias: {0}, TransitionBias: {1}", bias, transitionBias);
				throw new CorruptDataException(ServerStrings.ExCorruptedTimeZone);
			}
			return t.Negate();
		}

		// Token: 0x06004E28 RID: 20008 RVA: 0x00147B78 File Offset: 0x00145D78
		private static void GetStartAndRuleFromTimeZoneTransition(NativeMethods.SystemTime systemTime, out ExDateTime start, out Recurrence rule)
		{
			rule = null;
			int num;
			int num2;
			int num3;
			if (systemTime.Year == 0)
			{
				if (systemTime.Month != 0)
				{
					rule = VTimeZone.CreateTimeZoneRRule(systemTime);
				}
				DateTime minSupportedDateTime = CultureInfo.CurrentCulture.Calendar.MinSupportedDateTime;
				DateTime maxSupportedDateTime = CultureInfo.CurrentCulture.Calendar.MaxSupportedDateTime;
				if (minSupportedDateTime.Year < 1601 && maxSupportedDateTime.Year >= 1601)
				{
					num = 1601;
					num2 = 1;
					num3 = 1;
				}
				else
				{
					num = minSupportedDateTime.Year;
					num2 = minSupportedDateTime.Month;
					num3 = minSupportedDateTime.Day;
				}
			}
			else
			{
				num = (int)systemTime.Year;
				num2 = (int)systemTime.Month;
				num3 = (int)systemTime.Day;
			}
			try
			{
				start = new ExDateTime(ExTimeZone.UnspecifiedTimeZone, num, num2, num3, (int)systemTime.Hour, (int)systemTime.Minute, (int)systemTime.Second, (int)systemTime.Milliseconds);
			}
			catch (ArgumentOutOfRangeException innerException)
			{
				ExTraceGlobals.ICalTracer.TraceError(0L, "VTimezone::GetStartAndRuleFromTimeZoneTransition. The timezone is invalid.\r\nYear: {0}, Month: {1}, Day: {2}, Hour: {3}, Minute: {4}, Second: {5}, Milliseconds: {6}.", new object[]
				{
					num,
					num2,
					num3,
					systemTime.Hour,
					systemTime.Minute,
					systemTime.Second,
					systemTime.Milliseconds
				});
				throw new CorruptDataException(ServerStrings.ExCorruptedTimeZone, innerException);
			}
		}

		// Token: 0x06004E29 RID: 20009 RVA: 0x00147CEC File Offset: 0x00145EEC
		private static Recurrence CreateTimeZoneRRule(NativeMethods.SystemTime systemTime)
		{
			int num = (int)systemTime.Day;
			if (num == 5)
			{
				num = -1;
			}
			DayOfWeek dayOfWeek = (DayOfWeek)systemTime.DayOfWeek;
			Recurrence.ByDay byDay = new Recurrence.ByDay(dayOfWeek, num);
			return new Recurrence
			{
				Frequency = Frequency.Yearly,
				Interval = 1,
				ByMonth = new int[]
				{
					(int)systemTime.Month
				},
				ByDayList = new Recurrence.ByDay[]
				{
					byDay
				}
			};
		}

		// Token: 0x06004E2A RID: 20010 RVA: 0x00147D68 File Offset: 0x00145F68
		private static void CollectAllRuleYears(Dictionary<ushort, TimeZoneRule> rules, SortedSet<ushort> allRuleYears)
		{
			foreach (ushort num in rules.Keys)
			{
				allRuleYears.Add(num);
				TimeZoneRule timeZoneRule = rules[num];
				if (timeZoneRule.RuleHasRecurrenceUntilField && timeZoneRule.RecurrenceRule.UntilDateTime.Year < DateTime.MaxValue.Year)
				{
					allRuleYears.Add((ushort)(timeZoneRule.RecurrenceRule.UntilDateTime.Year + 1));
				}
			}
		}

		// Token: 0x06004E2B RID: 20011 RVA: 0x00147E10 File Offset: 0x00146010
		private static TimeZoneRule UpdateAndGetCurrentRule(ushort currentYear, Dictionary<ushort, TimeZoneRule> rules, ref TimeZoneRule recentRule)
		{
			TimeZoneRule timeZoneRule;
			if (!rules.TryGetValue(currentYear, out timeZoneRule))
			{
				timeZoneRule = VTimeZone.ValidateRecentRule(recentRule, currentYear);
			}
			recentRule = timeZoneRule;
			return timeZoneRule;
		}

		// Token: 0x06004E2C RID: 20012 RVA: 0x00147E38 File Offset: 0x00146038
		private static TimeZoneRule ValidateRecentRule(TimeZoneRule recentRule, ushort currentYear)
		{
			if (recentRule == null)
			{
				return null;
			}
			if (!recentRule.RuleHasRecurrenceUntilField || recentRule.RecurrenceRule.UntilDateTime.Year >= (int)currentYear)
			{
				return recentRule;
			}
			return null;
		}

		// Token: 0x06004E2D RID: 20013 RVA: 0x00147E6C File Offset: 0x0014606C
		private REG_TIMEZONE_INFO GetNativeTimeZoneRule(TimeZoneRule standard, TimeZoneRule daylight)
		{
			if (standard != null && daylight != null && standard.Offset == daylight.Offset)
			{
				daylight = null;
			}
			REG_TIMEZONE_INFO result = default(REG_TIMEZONE_INFO);
			if (standard == null)
			{
				result.Bias = -daylight.Offset;
				result.DaylightBias = 0;
			}
			else
			{
				result.Bias = -standard.Offset;
				result.StandardBias = 0;
				if (daylight != null)
				{
					result.StandardDate = standard.TransitionDate;
					result.DaylightDate = daylight.TransitionDate;
					result.DaylightBias = standard.Offset - daylight.Offset;
				}
			}
			return result;
		}

		// Token: 0x04002AAE RID: 10926
		private string timeZoneId;

		// Token: 0x04002AAF RID: 10927
		private REG_TIMEZONE_INFO timeZoneInfo;

		// Token: 0x04002AB0 RID: 10928
		private Dictionary<ushort, TimeZoneRule> standardRules;

		// Token: 0x04002AB1 RID: 10929
		private Dictionary<ushort, TimeZoneRule> daylightRules;
	}
}

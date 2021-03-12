using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200059A RID: 1434
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class TimeZoneDefinitionType
	{
		// Token: 0x06002881 RID: 10369 RVA: 0x000AB946 File Offset: 0x000A9B46
		[OnDeserializing]
		private void Init(StreamingContext context)
		{
			this.Init();
		}

		// Token: 0x06002882 RID: 10370 RVA: 0x000AB950 File Offset: 0x000A9B50
		private void Init()
		{
			this.periods = new List<PeriodType>();
			this.transitionsGroups = new List<ArrayOfTransitionsType>();
			this.timeZone = null;
			this.periodsDictionary = null;
			this.Id = null;
			this.Name = null;
			this.Periods = null;
			this.Transitions = null;
			this.TransitionsGroups = null;
		}

		// Token: 0x06002883 RID: 10371 RVA: 0x000AB9A4 File Offset: 0x000A9BA4
		private void AddPeriod(PeriodType period)
		{
			this.periods.Add(period);
		}

		// Token: 0x06002884 RID: 10372 RVA: 0x000AB9B2 File Offset: 0x000A9BB2
		private void AddTransitionsGroup(ArrayOfTransitionsType transitionGroup)
		{
			this.transitionsGroups.Add(transitionGroup);
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06002885 RID: 10373 RVA: 0x000AB9C0 File Offset: 0x000A9BC0
		// (set) Token: 0x06002886 RID: 10374 RVA: 0x000AB9CD File Offset: 0x000A9BCD
		[DataMember(EmitDefaultValue = false, Order = 1)]
		[XmlArrayItem("Period", IsNullable = false)]
		public PeriodType[] Periods
		{
			get
			{
				return this.periods.ToArray();
			}
			set
			{
				this.periods.Clear();
				if (value != null)
				{
					this.periods.AddRange(value);
				}
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06002887 RID: 10375 RVA: 0x000AB9E9 File Offset: 0x000A9BE9
		// (set) Token: 0x06002888 RID: 10376 RVA: 0x000AB9F6 File Offset: 0x000A9BF6
		[DataMember(EmitDefaultValue = false, Order = 2)]
		[XmlArrayItem("TransitionsGroup", IsNullable = false)]
		public ArrayOfTransitionsType[] TransitionsGroups
		{
			get
			{
				return this.transitionsGroups.ToArray();
			}
			set
			{
				this.transitionsGroups.Clear();
				if (value != null)
				{
					this.transitionsGroups.AddRange(value);
				}
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06002889 RID: 10377 RVA: 0x000ABA12 File Offset: 0x000A9C12
		// (set) Token: 0x0600288A RID: 10378 RVA: 0x000ABA1A File Offset: 0x000A9C1A
		[DataMember(EmitDefaultValue = false, Order = 3)]
		public ArrayOfTransitionsType Transitions { get; set; }

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x0600288B RID: 10379 RVA: 0x000ABA23 File Offset: 0x000A9C23
		// (set) Token: 0x0600288C RID: 10380 RVA: 0x000ABA2B File Offset: 0x000A9C2B
		[DataMember(EmitDefaultValue = false, Order = 0)]
		[XmlAttribute]
		public string Name { get; set; }

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x0600288D RID: 10381 RVA: 0x000ABA34 File Offset: 0x000A9C34
		// (set) Token: 0x0600288E RID: 10382 RVA: 0x000ABA3C File Offset: 0x000A9C3C
		[DataMember(EmitDefaultValue = false, Order = 0)]
		[XmlAttribute]
		public string Id { get; set; }

		// Token: 0x0600288F RID: 10383 RVA: 0x000ABA45 File Offset: 0x000A9C45
		public TimeZoneDefinitionType()
		{
			this.Init();
		}

		// Token: 0x06002890 RID: 10384 RVA: 0x000ABA53 File Offset: 0x000A9C53
		public TimeZoneDefinitionType(ExTimeZone exchTimeZone) : this()
		{
			this.timeZone = exchTimeZone;
			this.Id = this.timeZone.Id;
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06002891 RID: 10385 RVA: 0x000ABA73 File Offset: 0x000A9C73
		public ExTimeZone ExTimeZone
		{
			get
			{
				if (this.TryAsCustomTimeZone())
				{
					this.ValidateAndProcessData();
					this.ConvertToExTimeZone();
				}
				return this.timeZone;
			}
		}

		// Token: 0x06002892 RID: 10386 RVA: 0x000ABA90 File Offset: 0x000A9C90
		private static TimeSpan ConvertOffsetToTimeSpan(string timeOffset, TransitionType transitionToPeriod, ArrayOfTransitionsType transitionToGroup)
		{
			TimeSpan result;
			try
			{
				result = XmlConvert.ToTimeSpan(timeOffset);
			}
			catch (FormatException innerException)
			{
				throw new TimeZoneException(Strings.IDs.MessageInvalidTimeZoneInvalidOffsetFormat, innerException, transitionToGroup, transitionToPeriod);
			}
			return result;
		}

		// Token: 0x06002893 RID: 10387 RVA: 0x000ABACC File Offset: 0x000A9CCC
		private static string RenderXsDuration(TimeSpan timeSpan)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (timeSpan.Ticks < 0L)
			{
				timeSpan = timeSpan.Negate();
				stringBuilder.Append("-");
			}
			stringBuilder.Append("PT");
			if (timeSpan.Hours != 0 || timeSpan.Ticks == 0L)
			{
				stringBuilder.Append(timeSpan.Hours.ToString());
				stringBuilder.Append("H");
			}
			if (timeSpan.Minutes != 0)
			{
				stringBuilder.Append(timeSpan.Minutes.ToString());
				stringBuilder.Append("M");
			}
			if (timeSpan.Seconds != 0 || timeSpan.Milliseconds != 0)
			{
				stringBuilder.Append(timeSpan.Seconds.ToString());
				if (timeSpan.Milliseconds != 0)
				{
					stringBuilder.Append("." + timeSpan.Milliseconds.ToString("000"));
				}
				stringBuilder.Append("S");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002894 RID: 10388 RVA: 0x000ABBD8 File Offset: 0x000A9DD8
		private void ValidateAndProcessData()
		{
			for (int i = 0; i < this.TransitionsGroups.Length; i++)
			{
				for (int j = i + 1; j < this.TransitionsGroups.Length; j++)
				{
					if (!string.IsNullOrEmpty(this.TransitionsGroups[i].Id) && this.TransitionsGroups[i].Id == this.TransitionsGroups[j].Id)
					{
						throw new TimeZoneException((Strings.IDs)3276944824U, new string[]
						{
							"TransitionsGroup.Id",
							"TransitionsGroup.index.0",
							"TransitionsGroup.index.1"
						}, new string[]
						{
							this.TransitionsGroups[i].Id,
							i.ToString(),
							j.ToString()
						});
					}
				}
			}
			for (int k = 0; k < this.Periods.Length; k++)
			{
				for (int l = k + 1; l < this.Periods.Length; l++)
				{
					if (this.Periods[k].Id == this.Periods[l].Id)
					{
						throw new TimeZoneException(Strings.IDs.MessageInvalidTimeZoneDuplicatePeriods, new string[]
						{
							"Periods.Period.Id",
							"Periods.Period.index.0",
							"Periods.Period.index.1"
						}, new string[]
						{
							this.Periods[k].Id,
							k.ToString(),
							l.ToString()
						});
					}
				}
			}
			this.periodsDictionary = new Dictionary<string, PeriodType>(this.Periods.Length);
			foreach (PeriodType periodType in this.Periods)
			{
				this.periodsDictionary.Add(periodType.Id, periodType);
			}
			this.Name = (this.Name ?? string.Empty);
		}

		// Token: 0x06002895 RID: 10389 RVA: 0x000ABDD0 File Offset: 0x000A9FD0
		private bool TryAsCustomTimeZone()
		{
			bool flag = this.Periods != null && this.Periods.Length != 0;
			bool flag2 = this.Transitions != null && this.Transitions.Transition != null && this.Transitions.Transition.Length != 0;
			bool flag3 = this.TransitionsGroups != null && this.TransitionsGroups.Length != 0;
			this.timeZone = null;
			if (!flag && !flag2 && !flag3)
			{
				if (ExTimeZoneEnumerator.Instance.TryGetTimeZoneByName(this.Id, out this.timeZone))
				{
					return false;
				}
				throw new TimeZoneException((Strings.IDs)2530022313U, new string[]
				{
					"Id"
				}, new string[]
				{
					this.Id
				});
			}
			else
			{
				if (flag && flag2 && flag3)
				{
					return true;
				}
				throw new TimeZoneException((Strings.IDs)2852570616U, new string[]
				{
					"Id"
				}, new string[]
				{
					this.Id
				});
			}
		}

		// Token: 0x06002896 RID: 10390 RVA: 0x000ABEDC File Offset: 0x000AA0DC
		private void ConvertToExTimeZone()
		{
			ExTimeZoneInformation exTimeZoneInformation = new ExTimeZoneInformation("tzone://Microsoft/Custom", this.Name);
			exTimeZoneInformation.AlternativeId = this.Id;
			if (this.Transitions.Transition[0].GetType() != typeof(TransitionType))
			{
				throw new TimeZoneException((Strings.IDs)3332140560U, this.Transitions, this.Transitions.Transition[0]);
			}
			for (int i = 0; i < this.Transitions.Transition.Length; i++)
			{
				DateTime? endTransition;
				if (i != this.Transitions.Transition.Length - 1)
				{
					AbsoluteDateTransitionType absoluteDateTransitionType = this.Transitions.Transition[i + 1] as AbsoluteDateTransitionType;
					if (absoluteDateTransitionType == null)
					{
						throw new TimeZoneException((Strings.IDs)3644766027U, this.Transitions, this.Transitions.Transition[i + 1]);
					}
					endTransition = new DateTime?(absoluteDateTransitionType.DateTime);
				}
				else
				{
					endTransition = null;
				}
				ExTimeZoneRuleGroup exTimeZoneRuleGroup = new ExTimeZoneRuleGroup(endTransition);
				this.AddRulesToRuleGroup(exTimeZoneRuleGroup, this.Transitions.Transition[i], this.Transitions);
				if (exTimeZoneRuleGroup.Rules.Count != 0)
				{
					exTimeZoneInformation.AddGroup(exTimeZoneRuleGroup);
				}
			}
			try
			{
				this.timeZone = new ExTimeZone(exTimeZoneInformation);
			}
			catch (ArgumentException exception)
			{
				throw new TimeZoneException(exception);
			}
			catch (NotImplementedException exception2)
			{
				throw new TimeZoneException(exception2);
			}
			catch (InvalidOperationException exception3)
			{
				throw new TimeZoneException(exception3);
			}
		}

		// Token: 0x06002897 RID: 10391 RVA: 0x000AC060 File Offset: 0x000AA260
		private void AddRulesToRuleGroup(ExTimeZoneRuleGroup timeZoneRuleGroup, TransitionType transition, ArrayOfTransitionsType transitions)
		{
			if (transition.To.Kind != TransitionTargetKindType.Group)
			{
				throw new TimeZoneException((Strings.IDs)2265146620U, transitions, transition);
			}
			foreach (ArrayOfTransitionsType arrayOfTransitionsType in this.TransitionsGroups)
			{
				if (arrayOfTransitionsType.Id == transition.To.Value)
				{
					int num = arrayOfTransitionsType.Transition.Length;
					for (int j = 0; j < num; j++)
					{
						int num2 = (j + 1) % num;
						TransitionType transitionType = arrayOfTransitionsType.Transition[j];
						TransitionType transitionFromPeriod = arrayOfTransitionsType.Transition[num2];
						if (transitionType.To.Kind != TransitionTargetKindType.Period)
						{
							throw new TimeZoneException(Strings.IDs.MessageInvalidTimeZoneReferenceToPeriod, arrayOfTransitionsType, transitionType);
						}
						this.AddRuleToRuleGroup(timeZoneRuleGroup, transitionType, transitionFromPeriod, arrayOfTransitionsType);
					}
					return;
				}
			}
			throw new TimeZoneException(Strings.IDs.MessageInvalidTimeZoneMissedGroup, transitions, transition);
		}

		// Token: 0x06002898 RID: 10392 RVA: 0x000AC140 File Offset: 0x000AA340
		private void AddRuleToRuleGroup(ExTimeZoneRuleGroup timeZoneRuleGroup, TransitionType transitionToPeriod, TransitionType transitionFromPeriod, ArrayOfTransitionsType transitionToGroup)
		{
			RecurringDayTransitionType recurringDayTransitionType = transitionFromPeriod as RecurringDayTransitionType;
			RecurringDateTransitionType recurringDateTransitionType = transitionFromPeriod as RecurringDateTransitionType;
			if (!this.periodsDictionary.ContainsKey(transitionToPeriod.To.Value))
			{
				throw new TimeZoneException((Strings.IDs)3865092385U, transitionToGroup, transitionToPeriod);
			}
			PeriodType periodType = this.periodsDictionary[transitionToPeriod.To.Value];
			TimeSpan bias = XmlConvert.ToTimeSpan(periodType.Bias);
			bias = bias.Negate();
			ExYearlyRecurringTime observanceEnd;
			if (recurringDateTransitionType != null)
			{
				TimeSpan timeSpan = TimeZoneDefinitionType.ConvertOffsetToTimeSpan(recurringDateTransitionType.TimeOffset, transitionToPeriod, transitionToGroup);
				try
				{
					observanceEnd = new ExYearlyRecurringDate(recurringDateTransitionType.Month, recurringDateTransitionType.Day, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
					goto IL_16C;
				}
				catch (ArgumentOutOfRangeException ex)
				{
					throw new TimeZoneException((Strings.IDs)3961981453U, ex, transitionToGroup, transitionToPeriod, "ParameterName", ex.ParamName);
				}
			}
			if (recurringDayTransitionType != null)
			{
				TimeSpan timeSpan2 = TimeZoneDefinitionType.ConvertOffsetToTimeSpan(recurringDayTransitionType.TimeOffset, transitionToPeriod, transitionToGroup);
				DayOfWeek? dayOfWeek = this.ConvertToDayOfWeek(recurringDayTransitionType.DayOfWeek);
				if (dayOfWeek == null)
				{
					throw new TimeZoneException(Strings.IDs.MessageInvalidTimeZoneDayOfWeekValue, transitionToGroup, transitionToPeriod);
				}
				try
				{
					observanceEnd = new ExYearlyRecurringDay(recurringDayTransitionType.Occurrence, dayOfWeek.Value, recurringDayTransitionType.Month, timeSpan2.Hours, timeSpan2.Minutes, timeSpan2.Seconds, timeSpan2.Milliseconds);
					goto IL_16C;
				}
				catch (ArgumentOutOfRangeException ex2)
				{
					throw new TimeZoneException((Strings.IDs)3961981453U, ex2, transitionToGroup, transitionToPeriod, "ParameterName", ex2.ParamName);
				}
			}
			observanceEnd = null;
			IL_16C:
			ExTimeZoneRule ruleInfo = new ExTimeZoneRule(periodType.Id, periodType.Name, bias, observanceEnd);
			timeZoneRuleGroup.AddRule(ruleInfo);
		}

		// Token: 0x06002899 RID: 10393 RVA: 0x000AC2F4 File Offset: 0x000AA4F4
		private DayOfWeek? ConvertToDayOfWeek(string dayOfWeek)
		{
			switch (dayOfWeek)
			{
			case "Monday":
				return new DayOfWeek?(DayOfWeek.Monday);
			case "Tuesday":
				return new DayOfWeek?(DayOfWeek.Tuesday);
			case "Wednesday":
				return new DayOfWeek?(DayOfWeek.Wednesday);
			case "Thursday":
				return new DayOfWeek?(DayOfWeek.Thursday);
			case "Friday":
				return new DayOfWeek?(DayOfWeek.Friday);
			case "Saturday":
				return new DayOfWeek?(DayOfWeek.Saturday);
			case "Sunday":
				return new DayOfWeek?(DayOfWeek.Sunday);
			}
			return null;
		}

		// Token: 0x0600289A RID: 10394 RVA: 0x000AC3E4 File Offset: 0x000AA5E4
		public void Render(bool returnFullTimeZoneData, IFormatProvider iFormatProvider)
		{
			if (this.timeZone != null)
			{
				this.Name = this.timeZone.LocalizableDisplayName.ToString(iFormatProvider);
				this.Id = (this.timeZone.AlternativeId ?? string.Empty);
				if (returnFullTimeZoneData)
				{
					this.RenderTimeZoneContentElements();
				}
			}
		}

		// Token: 0x0600289B RID: 10395 RVA: 0x000AC438 File Offset: 0x000AA638
		private void RenderTimeZoneContentElements()
		{
			this.Periods = null;
			this.transitionsGroups = new List<ArrayOfTransitionsType>();
			this.Transitions = new ArrayOfTransitionsType();
			int num = 0;
			this.RenderTransitionToGroup(this.Transitions, num);
			foreach (ExTimeZoneRuleGroup exTimeZoneRuleGroup in this.timeZone.TimeZoneInformation.Groups)
			{
				ArrayOfTransitionsType lstTransitionsGroup = this.RenderTransitionsGroup(this.transitionsGroups, num);
				for (int i = 0; i < exTimeZoneRuleGroup.Rules.Count; i++)
				{
					int index = (i + 1) % exTimeZoneRuleGroup.Rules.Count;
					this.RenderPeriodAndReference(this.periods, lstTransitionsGroup, exTimeZoneRuleGroup.Rules[i], exTimeZoneRuleGroup.Rules[index], num);
				}
				num++;
				if (exTimeZoneRuleGroup.EndTransition != null)
				{
					AbsoluteDateTransitionType absoluteDateTransitionType = this.RenderTransitionToGroup(this.Transitions, num) as AbsoluteDateTransitionType;
					if (absoluteDateTransitionType != null)
					{
						absoluteDateTransitionType.DateTime = new DateTime(exTimeZoneRuleGroup.EndTransition.Value.Ticks, DateTimeKind.Unspecified);
					}
				}
			}
		}

		// Token: 0x0600289C RID: 10396 RVA: 0x000AC570 File Offset: 0x000AA770
		private ArrayOfTransitionsType RenderTransitionsGroup(List<ArrayOfTransitionsType> lstTransitionsGroups, int ruleGroupIdx)
		{
			ArrayOfTransitionsType arrayOfTransitionsType = new ArrayOfTransitionsType(true, ruleGroupIdx.ToString(), null);
			lstTransitionsGroups.Add(arrayOfTransitionsType);
			return arrayOfTransitionsType;
		}

		// Token: 0x0600289D RID: 10397 RVA: 0x000AC594 File Offset: 0x000AA794
		private void RenderPeriodAndReference(List<PeriodType> lstPeriods, ArrayOfTransitionsType lstTransitionsGroup, ExTimeZoneRule rule, ExTimeZoneRule nextRule, int ruleGroupIdx)
		{
			string bias = TimeZoneDefinitionType.RenderXsDuration(rule.Bias.Negate());
			PeriodType item = new PeriodType(bias, rule.DisplayName, rule.Id);
			lstPeriods.Add(item);
			this.RenderTransitionToPeriod(lstTransitionsGroup, rule, nextRule, ruleGroupIdx);
		}

		// Token: 0x0600289E RID: 10398 RVA: 0x000AC5DC File Offset: 0x000AA7DC
		private TransitionType RenderTransitionToGroup(ArrayOfTransitionsType lstTransitions, int ruleGroupIdx)
		{
			TransitionType transitionType = (ruleGroupIdx == 0) ? new TransitionType() : new AbsoluteDateTransitionType();
			transitionType.To = new TransitionTargetType(TransitionTargetKindType.Group, ruleGroupIdx.ToString());
			lstTransitions.Add(transitionType);
			return transitionType;
		}

		// Token: 0x0600289F RID: 10399 RVA: 0x000AC614 File Offset: 0x000AA814
		private void RenderTransitionToPeriod(ArrayOfTransitionsType lstTransitionsGroup, ExTimeZoneRule rule, ExTimeZoneRule nextRule, int ruleGroupIds)
		{
			ExYearlyRecurringTime observanceEnd = rule.ObservanceEnd;
			ExYearlyRecurringDate exYearlyRecurringDate = rule.ObservanceEnd as ExYearlyRecurringDate;
			ExYearlyRecurringDay exYearlyRecurringDay = rule.ObservanceEnd as ExYearlyRecurringDay;
			TimeSpan timeSpan;
			int month;
			if (observanceEnd != null)
			{
				if (observanceEnd.Hour < 0)
				{
					timeSpan = new TimeSpan(0, -observanceEnd.Hour, observanceEnd.Minute, observanceEnd.Second, observanceEnd.Milliseconds).Negate();
				}
				else
				{
					timeSpan = new TimeSpan(0, observanceEnd.Hour, observanceEnd.Minute, observanceEnd.Second, observanceEnd.Milliseconds);
				}
				month = observanceEnd.Month;
			}
			else
			{
				timeSpan = TimeSpan.Zero;
				month = 0;
			}
			TransitionTargetType to = new TransitionTargetType(TransitionTargetKindType.Period, nextRule.Id);
			TransitionType transition;
			if (exYearlyRecurringDay != null)
			{
				transition = new RecurringDayTransitionType(to, TimeZoneDefinitionType.RenderXsDuration(timeSpan), month, exYearlyRecurringDay.DayOfWeek.ToString(), exYearlyRecurringDay.Occurrence);
			}
			else if (exYearlyRecurringDate != null)
			{
				transition = new RecurringDateTransitionType(to, TimeZoneDefinitionType.RenderXsDuration(timeSpan), month, exYearlyRecurringDate.Day);
			}
			else
			{
				if (observanceEnd != null)
				{
					return;
				}
				transition = new TransitionType(to);
			}
			lstTransitionsGroup.Add(transition);
		}

		// Token: 0x040019CB RID: 6603
		private const string IdString = "Id";

		// Token: 0x040019CC RID: 6604
		private const string CustomTimeZoneIdString = "tzone://Microsoft/Custom";

		// Token: 0x040019CD RID: 6605
		private List<PeriodType> periods;

		// Token: 0x040019CE RID: 6606
		private List<ArrayOfTransitionsType> transitionsGroups;

		// Token: 0x040019CF RID: 6607
		private Dictionary<string, PeriodType> periodsDictionary;

		// Token: 0x040019D0 RID: 6608
		private ExTimeZone timeZone;
	}
}

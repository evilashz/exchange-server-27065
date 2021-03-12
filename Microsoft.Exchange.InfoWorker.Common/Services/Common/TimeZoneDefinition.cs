using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Xml;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common;

namespace Microsoft.Exchange.Services.Common
{
	// Token: 0x0200024A RID: 586
	internal class TimeZoneDefinition
	{
		// Token: 0x060010DB RID: 4315 RVA: 0x0004D0A6 File Offset: 0x0004B2A6
		public TimeZoneDefinition(XmlElement xmlParent)
		{
			if (this.ParseAttributes(xmlParent) && this.ParsePeriods(xmlParent) && this.ParseTransitionsGroups(xmlParent))
			{
				this.ParseTransitions(xmlParent);
			}
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x0004D0D1 File Offset: 0x0004B2D1
		public TimeZoneDefinition(ExTimeZone exchTimeZone)
		{
			this.timeZone = exchTimeZone;
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x0004D0E0 File Offset: 0x0004B2E0
		public TimeZoneDefinition(string id)
		{
			this.timeZone = null;
			if (ExTimeZoneEnumerator.Instance.TryGetTimeZoneByName(id, out this.timeZone))
			{
				this.id = id;
			}
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x0004D109 File Offset: 0x0004B309
		internal TimeZoneDefinition(string name, string id)
		{
			this.name = name;
			this.id = id;
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x060010DF RID: 4319 RVA: 0x0004D11F File Offset: 0x0004B31F
		public ExTimeZone ExTimeZone
		{
			get
			{
				if (this.TryAsCustomTimeZone())
				{
					this.ValidateAndProcessXmlData();
					this.ConvertToExTimeZone();
				}
				return this.timeZone;
			}
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x0004D13C File Offset: 0x0004B33C
		private void ValidateAndProcessXmlData()
		{
			for (int i = 0; i < this.transitionsGroups.Length; i++)
			{
				for (int j = i + 1; j < this.transitionsGroups.Length; j++)
				{
					if (!string.IsNullOrEmpty(this.transitionsGroups[i].Id) && this.transitionsGroups[i].Id == this.transitionsGroups[j].Id)
					{
						throw new TimeZoneException((Strings.IDs)3276944824U, new string[]
						{
							"TransitionsGroup.Id",
							"TransitionsGroup.index.0",
							"TransitionsGroup.index.1"
						}, new string[]
						{
							this.transitionsGroups[i].Id,
							i.ToString(),
							j.ToString()
						});
					}
				}
			}
			for (int k = 0; k < this.periods.Length; k++)
			{
				for (int l = k + 1; l < this.periods.Length; l++)
				{
					if (this.periods[k].Id == this.periods[l].Id)
					{
						throw new TimeZoneException(Strings.IDs.MessageInvalidTimeZoneDuplicatePeriods, new string[]
						{
							"Periods.Period.Id",
							"Periods.Period.index.0",
							"Periods.Period.index.1"
						}, new string[]
						{
							this.periods[k].Id,
							k.ToString(),
							l.ToString()
						});
					}
				}
			}
			this.periodsDictionary = new Dictionary<string, TimeZoneDefinition.PeriodType>(this.periods.Length);
			foreach (TimeZoneDefinition.PeriodType periodType in this.periods)
			{
				this.periodsDictionary.Add(periodType.Id, periodType);
			}
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x0004D320 File Offset: 0x0004B520
		private bool TryAsCustomTimeZone()
		{
			bool flag = this.periods != null && this.periods.Length != 0;
			bool flag2 = this.transitions != null && this.transitions.Transitions != null && this.transitions.Transitions.Length != 0;
			bool flag3 = this.transitionsGroups != null && this.transitionsGroups.Length != 0;
			this.timeZone = null;
			if (!flag && !flag2 && !flag3)
			{
				if (ExTimeZoneEnumerator.Instance.TryGetTimeZoneByName(this.id, out this.timeZone))
				{
					return false;
				}
				throw new TimeZoneException((Strings.IDs)2530022313U, new string[]
				{
					"Id"
				}, new string[]
				{
					this.id
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
					this.id
				});
			}
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x0004D42C File Offset: 0x0004B62C
		private void ConvertToExTimeZone()
		{
			ExTimeZoneInformation exTimeZoneInformation = new ExTimeZoneInformation("tzone://Microsoft/Custom", this.name);
			exTimeZoneInformation.AlternativeId = this.id;
			if (this.transitions.Transitions[0].GetType() != typeof(TimeZoneDefinition.Transition))
			{
				throw new TimeZoneException((Strings.IDs)3332140560U, this.transitions, this.transitions.Transitions[0]);
			}
			for (int i = 0; i < this.transitions.Transitions.Length; i++)
			{
				DateTime? endTransition;
				if (i != this.transitions.Transitions.Length - 1)
				{
					TimeZoneDefinition.AbsoluteDateTransition absoluteDateTransition = this.transitions.Transitions[i + 1] as TimeZoneDefinition.AbsoluteDateTransition;
					if (absoluteDateTransition == null)
					{
						throw new TimeZoneException((Strings.IDs)3644766027U, this.transitions, this.transitions.Transitions[i + 1]);
					}
					endTransition = new DateTime?(absoluteDateTransition.DateTime);
				}
				else
				{
					endTransition = null;
				}
				ExTimeZoneRuleGroup exTimeZoneRuleGroup = new ExTimeZoneRuleGroup(endTransition);
				this.AddRulesToRuleGroup(exTimeZoneRuleGroup, this.transitions.Transitions[i], this.transitions);
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

		// Token: 0x060010E3 RID: 4323 RVA: 0x0004D5B0 File Offset: 0x0004B7B0
		private void AddRulesToRuleGroup(ExTimeZoneRuleGroup timeZoneRuleGroup, TimeZoneDefinition.Transition transition, TimeZoneDefinition.TransitionsGroup transitions)
		{
			if (transition.To.Kind != TimeZoneDefinition.TransitionTargetKindType.Group)
			{
				throw new TimeZoneException((Strings.IDs)2265146620U, transitions, transition);
			}
			foreach (TimeZoneDefinition.TransitionsGroup transitionsGroup in this.transitionsGroups)
			{
				if (transitionsGroup.Id == transition.To.Value)
				{
					int num = transitionsGroup.Transitions.Length;
					for (int j = 0; j < num; j++)
					{
						int num2 = (j + 1) % num;
						TimeZoneDefinition.Transition transition2 = transitionsGroup.Transitions[j];
						TimeZoneDefinition.Transition transitionFromPeriod = transitionsGroup.Transitions[num2];
						if (transition2.To.Kind != TimeZoneDefinition.TransitionTargetKindType.Period)
						{
							throw new TimeZoneException(Strings.IDs.MessageInvalidTimeZoneReferenceToPeriod, transitionsGroup, transition2);
						}
						this.AddRuleToRuleGroup(timeZoneRuleGroup, transition2, transitionFromPeriod, transitionsGroup);
					}
					return;
				}
			}
			throw new TimeZoneException(Strings.IDs.MessageInvalidTimeZoneMissedGroup, transitions, transition);
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x0004D690 File Offset: 0x0004B890
		private void AddRuleToRuleGroup(ExTimeZoneRuleGroup timeZoneRuleGroup, TimeZoneDefinition.Transition transitionToPeriod, TimeZoneDefinition.Transition transitionFromPeriod, TimeZoneDefinition.TransitionsGroup transitionToGroup)
		{
			TimeZoneDefinition.RecurringDayTransition recurringDayTransition = transitionFromPeriod as TimeZoneDefinition.RecurringDayTransition;
			TimeZoneDefinition.RecurringDateTransition recurringDateTransition = transitionFromPeriod as TimeZoneDefinition.RecurringDateTransition;
			if (!this.periodsDictionary.ContainsKey(transitionToPeriod.To.Value))
			{
				throw new TimeZoneException((Strings.IDs)3865092385U, transitionToGroup, transitionToPeriod);
			}
			TimeZoneDefinition.PeriodType periodType = this.periodsDictionary[transitionToPeriod.To.Value];
			TimeSpan bias = XmlConvert.ToTimeSpan(periodType.Bias);
			bias = bias.Negate();
			ExYearlyRecurringTime observanceEnd;
			if (recurringDateTransition != null)
			{
				TimeSpan timeSpan = TimeZoneDefinition.ConvertOffsetToTimeSpan(recurringDateTransition.TimeOffset, transitionToPeriod, transitionToGroup);
				try
				{
					observanceEnd = new ExYearlyRecurringDate(recurringDateTransition.Month, recurringDateTransition.Day, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
					goto IL_16C;
				}
				catch (ArgumentOutOfRangeException ex)
				{
					throw new TimeZoneException((Strings.IDs)3961981453U, ex, transitionToGroup, transitionToPeriod, "ParameterName", ex.ParamName);
				}
			}
			if (recurringDayTransition != null)
			{
				TimeSpan timeSpan2 = TimeZoneDefinition.ConvertOffsetToTimeSpan(recurringDayTransition.TimeOffset, transitionToPeriod, transitionToGroup);
				DayOfWeek? dayOfWeek = this.ConvertToDayOfWeek(recurringDayTransition.DayOfWeek);
				if (dayOfWeek == null)
				{
					throw new TimeZoneException(Strings.IDs.MessageInvalidTimeZoneDayOfWeekValue, transitionToGroup, transitionToPeriod);
				}
				try
				{
					observanceEnd = new ExYearlyRecurringDay(recurringDayTransition.Occurrence, dayOfWeek.Value, recurringDayTransition.Month, timeSpan2.Hours, timeSpan2.Minutes, timeSpan2.Seconds, timeSpan2.Milliseconds);
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

		// Token: 0x060010E5 RID: 4325 RVA: 0x0004D844 File Offset: 0x0004BA44
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

		// Token: 0x060010E6 RID: 4326 RVA: 0x0004D934 File Offset: 0x0004BB34
		private static TimeSpan ConvertOffsetToTimeSpan(string timeOffset, TimeZoneDefinition.Transition transitionToPeriod, TimeZoneDefinition.TransitionsGroup transitionToGroup)
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

		// Token: 0x060010E7 RID: 4327 RVA: 0x0004D970 File Offset: 0x0004BB70
		private bool ParseAttributes(XmlElement xmlParent)
		{
			this.name = xmlParent.GetAttribute("Name");
			this.id = xmlParent.GetAttribute("Id");
			return !string.IsNullOrEmpty(this.id);
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x0004D9A4 File Offset: 0x0004BBA4
		private bool ParsePeriods(XmlElement xmlParent)
		{
			XmlElement xmlElement = xmlParent["Periods", xmlParent.NamespaceURI];
			if (xmlElement == null || xmlElement.ChildNodes.Count == 0)
			{
				return false;
			}
			int num = 0;
			this.periods = new TimeZoneDefinition.PeriodType[xmlElement.ChildNodes.Count];
			foreach (object obj in xmlElement.ChildNodes)
			{
				XmlElement xmlParent2 = (XmlElement)obj;
				string andVerifyNotNullString = TimeZoneDefinition.GetAndVerifyNotNullString(xmlParent2, "Bias", Strings.IDs.MessageInvalidTimeZonePeriodNullBias, true);
				string andVerifyNotNullString2 = TimeZoneDefinition.GetAndVerifyNotNullString(xmlParent2, "Name", (Strings.IDs)2912574056U, true);
				string andVerifyNotNullString3 = TimeZoneDefinition.GetAndVerifyNotNullString(xmlParent2, "Id", (Strings.IDs)3558532322U, true);
				this.periods[num++] = new TimeZoneDefinition.PeriodType(andVerifyNotNullString, andVerifyNotNullString2, andVerifyNotNullString3);
			}
			return true;
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x0004DA98 File Offset: 0x0004BC98
		private bool ParseTransitionsGroups(XmlElement xmlParent)
		{
			XmlElement xmlElement = xmlParent["TransitionsGroups", xmlParent.NamespaceURI];
			if (xmlElement == null || xmlElement.ChildNodes.Count == 0)
			{
				return false;
			}
			int num = 0;
			this.transitionsGroups = new TimeZoneDefinition.TransitionsGroup[xmlElement.ChildNodes.Count];
			foreach (object obj in xmlElement.ChildNodes)
			{
				XmlElement xmlTransitionGroup = (XmlElement)obj;
				this.transitionsGroups[num] = this.ParseTransitionsGroup(xmlTransitionGroup, true);
				num++;
			}
			return true;
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x0004DB40 File Offset: 0x0004BD40
		private TimeZoneDefinition.TransitionsGroup ParseTransitionsGroup(XmlElement xmlTransitionGroup, bool checkTransitionsGroup)
		{
			TimeZoneDefinition.TransitionsGroup transitionsGroup = new TimeZoneDefinition.TransitionsGroup(checkTransitionsGroup);
			transitionsGroup.Id = TimeZoneDefinition.GetAndVerifyNotNullString(xmlTransitionGroup, "Id", (Strings.IDs)4098403379U, checkTransitionsGroup);
			transitionsGroup.Transitions = this.ParseTransitionArray(xmlTransitionGroup);
			if (checkTransitionsGroup && transitionsGroup.Transitions.Length > 2)
			{
				throw new TimeZoneException((Strings.IDs)3442221872U, transitionsGroup, transitionsGroup.Transitions[2]);
			}
			return transitionsGroup;
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x0004DBA8 File Offset: 0x0004BDA8
		private TimeZoneDefinition.Transition[] ParseTransitionArray(XmlElement xmlTransitions)
		{
			int count = xmlTransitions.ChildNodes.Count;
			TimeZoneDefinition.Transition[] array = new TimeZoneDefinition.Transition[count];
			int num = 0;
			foreach (object obj in xmlTransitions.ChildNodes)
			{
				XmlElement xmlTransition = (XmlElement)obj;
				array[num] = this.ParseTransitionElement(xmlTransition);
				num++;
			}
			return array;
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x0004DC28 File Offset: 0x0004BE28
		private TimeZoneDefinition.Transition ParseTransitionElement(XmlElement xmlTransition)
		{
			string localName = xmlTransition.LocalName;
			string a;
			if ((a = localName) != null)
			{
				if (a == "AbsoluteDateTransition")
				{
					return this.ParseAbsoluteDateTransition(xmlTransition);
				}
				if (a == "RecurringDateTransition")
				{
					return this.ParseRecurringDateTransition(xmlTransition);
				}
				if (a == "RecurringDayTransition")
				{
					return this.ParseRecurringDayTransition(xmlTransition);
				}
			}
			return this.ParseTransition(xmlTransition);
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x0004DC89 File Offset: 0x0004BE89
		private TimeZoneDefinition.Transition ParseTransition(XmlElement xmlParent)
		{
			return new TimeZoneDefinition.Transition(new TimeZoneDefinition.TransitionTargetType(xmlParent));
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x0004DC98 File Offset: 0x0004BE98
		private TimeZoneDefinition.AbsoluteDateTransition ParseAbsoluteDateTransition(XmlElement xmlParent)
		{
			string s = this.ParseStringElement(xmlParent, "DateTime");
			DateTime dateTime = XmlConvert.ToDateTime(s, XmlDateTimeSerializationMode.RoundtripKind);
			return new TimeZoneDefinition.AbsoluteDateTransition(new TimeZoneDefinition.TransitionTargetType(xmlParent), dateTime);
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x0004DCC8 File Offset: 0x0004BEC8
		private TimeZoneDefinition.RecurringDateTransition ParseRecurringDateTransition(XmlElement xmlParent)
		{
			return new TimeZoneDefinition.RecurringDateTransition(new TimeZoneDefinition.TransitionTargetType(xmlParent), this.ParseStringElement(xmlParent, "TimeOffset"), this.ParseIntElement(xmlParent, "Month"), this.ParseIntElement(xmlParent, "Day"));
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x0004DD08 File Offset: 0x0004BF08
		private TimeZoneDefinition.RecurringDayTransition ParseRecurringDayTransition(XmlElement xmlParent)
		{
			return new TimeZoneDefinition.RecurringDayTransition(new TimeZoneDefinition.TransitionTargetType(xmlParent), this.ParseStringElement(xmlParent, "TimeOffset"), this.ParseIntElement(xmlParent, "Month"), this.ParseStringElement(xmlParent, "DayOfWeek"), this.ParseIntElement(xmlParent, "Occurrence"));
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x0004DD54 File Offset: 0x0004BF54
		private string ParseStringElement(XmlElement xmlParent, string xmlElementName)
		{
			XmlElement xmlElement = xmlParent[xmlElementName, xmlParent.NamespaceURI];
			return xmlElement.InnerText;
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x0004DD78 File Offset: 0x0004BF78
		private int ParseIntElement(XmlElement xmlParent, string xmlElementName)
		{
			XmlElement xmlElement = xmlParent[xmlElementName, xmlParent.NamespaceURI];
			return int.Parse(xmlElement.InnerText);
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x0004DDA0 File Offset: 0x0004BFA0
		private bool ParseTransitions(XmlElement xmlParent)
		{
			XmlElement xmlElement = xmlParent["Transitions", xmlParent.NamespaceURI];
			if (xmlElement == null || xmlElement.ChildNodes.Count == 0)
			{
				return false;
			}
			this.transitions = this.ParseTransitionsGroup(xmlElement, false);
			return true;
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x0004DDE0 File Offset: 0x0004BFE0
		private static string GetAndVerifyNotNullString(XmlElement xmlParent, string xmlAttributeName, Enum messageId, bool checkNull)
		{
			string attribute = xmlParent.GetAttribute(xmlAttributeName);
			if (string.IsNullOrEmpty(attribute) && checkNull)
			{
				throw new TimeZoneException(messageId);
			}
			return attribute;
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x0004DE08 File Offset: 0x0004C008
		public void Render(XmlElement xmlParent, string prefix, string typeNameSpace, string xmlElementName, bool returnFullTimeZoneData, CultureInfo cultureInfo)
		{
			if (this.timeZone != null)
			{
				this.typeNameSpace = typeNameSpace;
				this.typePrefix = prefix;
				XmlElement xmlElement = TimeZoneDefinition.XmlHelper.CreateElement(xmlParent, prefix, xmlElementName, this.typeNameSpace);
				this.name = this.timeZone.LocalizableDisplayName.ToString(cultureInfo);
				this.id = (this.timeZone.AlternativeId ?? string.Empty);
				TimeZoneDefinition.XmlHelper.CreateAttribute(xmlElement, "Name", this.name, false);
				TimeZoneDefinition.XmlHelper.CreateAttribute(xmlElement, "Id", this.id, false);
				if (returnFullTimeZoneData)
				{
					this.RenderTimeZoneContentElements(xmlElement);
				}
			}
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x0004DEA4 File Offset: 0x0004C0A4
		private void RenderTimeZoneContentElements(XmlElement xmlParent)
		{
			XmlElement xmlPeriods = TimeZoneDefinition.XmlHelper.CreateElement(xmlParent, this.typePrefix, "Periods", this.typeNameSpace);
			XmlElement xmlParent2 = TimeZoneDefinition.XmlHelper.CreateElement(xmlParent, this.typePrefix, "TransitionsGroups", this.typeNameSpace);
			XmlElement xmlTransitions = TimeZoneDefinition.XmlHelper.CreateElement(xmlParent, this.typePrefix, "Transitions", this.typeNameSpace);
			int num = 0;
			this.RenderTransitionToGroup(xmlTransitions, num);
			foreach (ExTimeZoneRuleGroup exTimeZoneRuleGroup in this.timeZone.TimeZoneInformation.Groups)
			{
				XmlElement xmlTransitionsGroup = this.RenderTransitionsGroup(xmlParent2, num);
				for (int i = 0; i < exTimeZoneRuleGroup.Rules.Count; i++)
				{
					int index = (i + 1) % exTimeZoneRuleGroup.Rules.Count;
					this.RenderPeriodAndReference(xmlPeriods, xmlTransitionsGroup, exTimeZoneRuleGroup.Rules[i], exTimeZoneRuleGroup.Rules[index], num);
				}
				num++;
				if (exTimeZoneRuleGroup.EndTransition != null)
				{
					XmlElement parentElement = this.RenderTransitionToGroup(xmlTransitions, num);
					string textValue = XmlConvert.ToString(exTimeZoneRuleGroup.EndTransition.Value, XmlDateTimeSerializationMode.Unspecified);
					TimeZoneDefinition.XmlHelper.CreateTextElement(parentElement, this.typePrefix, "DateTime", textValue, this.typeNameSpace);
				}
			}
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x0004E004 File Offset: 0x0004C204
		private XmlElement RenderTransitionsGroup(XmlElement xmlParent, int ruleGroupIdx)
		{
			XmlElement xmlElement = TimeZoneDefinition.XmlHelper.CreateElement(xmlParent, this.typePrefix, "TransitionsGroup", this.typeNameSpace);
			TimeZoneDefinition.XmlHelper.CreateAttribute(xmlElement, "Id", ruleGroupIdx.ToString());
			return xmlElement;
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x0004E040 File Offset: 0x0004C240
		private void RenderPeriodAndReference(XmlElement xmlPeriods, XmlElement xmlTransitionsGroup, ExTimeZoneRule rule, ExTimeZoneRule nextRule, int ruleGroupIdx)
		{
			XmlElement parentElement = TimeZoneDefinition.XmlHelper.CreateElement(xmlPeriods, this.typePrefix, "Period", this.typeNameSpace);
			TimeZoneDefinition.XmlHelper.CreateAttribute(parentElement, "Bias", TimeZoneDefinition.RenderXsDuration(rule.Bias.Negate()));
			TimeZoneDefinition.XmlHelper.CreateAttribute(parentElement, "Name", rule.DisplayName);
			TimeZoneDefinition.XmlHelper.CreateAttribute(parentElement, "Id", rule.Id);
			this.RenderTransitionToPeriod(xmlTransitionsGroup, rule, nextRule, ruleGroupIdx);
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x0004E0B4 File Offset: 0x0004C2B4
		private XmlElement RenderTransitionToGroup(XmlElement xmlTransitions, int ruleGroupIdx)
		{
			XmlElement xmlElement = TimeZoneDefinition.XmlHelper.CreateElement(xmlTransitions, this.typePrefix, (ruleGroupIdx == 0) ? "Transition" : "AbsoluteDateTransition", this.typeNameSpace);
			XmlElement parentElement = TimeZoneDefinition.XmlHelper.CreateTextElement(xmlElement, this.typePrefix, "To", ruleGroupIdx.ToString(), this.typeNameSpace);
			TimeZoneDefinition.XmlHelper.CreateAttribute(parentElement, "Kind", "Group");
			return xmlElement;
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x0004E114 File Offset: 0x0004C314
		private void RenderTransitionToPeriod(XmlElement xmlTransitionsGroup, ExTimeZoneRule rule, ExTimeZoneRule nextRule, int ruleGroupIds)
		{
			ExYearlyRecurringTime observanceEnd = rule.ObservanceEnd;
			ExYearlyRecurringDate exYearlyRecurringDate = rule.ObservanceEnd as ExYearlyRecurringDate;
			ExYearlyRecurringDay exYearlyRecurringDay = rule.ObservanceEnd as ExYearlyRecurringDay;
			string localName;
			if (exYearlyRecurringDay != null)
			{
				localName = "RecurringDayTransition";
			}
			else if (exYearlyRecurringDate != null)
			{
				localName = "RecurringDateTransition";
			}
			else
			{
				if (observanceEnd != null)
				{
					return;
				}
				localName = "Transition";
			}
			XmlElement xmlElement = TimeZoneDefinition.XmlHelper.CreateElement(xmlTransitionsGroup, this.typePrefix, localName, this.typeNameSpace);
			XmlElement parentElement = TimeZoneDefinition.XmlHelper.CreateTextElement(xmlElement, this.typePrefix, "To", nextRule.Id, this.typeNameSpace);
			TimeZoneDefinition.XmlHelper.CreateAttribute(parentElement, "Kind", "Period");
			if (observanceEnd != null)
			{
				this.RenderExYearlyRecurringTime(xmlElement, observanceEnd);
				if (exYearlyRecurringDay != null)
				{
					TimeZoneDefinition.XmlHelper.CreateTextElement(xmlElement, this.typePrefix, "DayOfWeek", exYearlyRecurringDay.DayOfWeek.ToString(), this.typeNameSpace);
					TimeZoneDefinition.XmlHelper.CreateTextElement(xmlElement, this.typePrefix, "Occurrence", exYearlyRecurringDay.Occurrence.ToString(), this.typeNameSpace);
					return;
				}
				if (exYearlyRecurringDate != null)
				{
					TimeZoneDefinition.XmlHelper.CreateTextElement(xmlElement, this.typePrefix, "Day", exYearlyRecurringDate.Day.ToString(), this.typeNameSpace);
				}
			}
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x0004E23C File Offset: 0x0004C43C
		private void RenderExYearlyRecurringTime(XmlElement xmlTransition, ExYearlyRecurringTime recurringTime)
		{
			if (recurringTime != null)
			{
				TimeSpan timeSpan;
				if (recurringTime.Hour < 0)
				{
					timeSpan = new TimeSpan(0, -recurringTime.Hour, recurringTime.Minute, recurringTime.Second, recurringTime.Milliseconds).Negate();
				}
				else
				{
					timeSpan = new TimeSpan(0, recurringTime.Hour, recurringTime.Minute, recurringTime.Second, recurringTime.Milliseconds);
				}
				TimeZoneDefinition.XmlHelper.CreateTextElement(xmlTransition, this.typePrefix, "TimeOffset", TimeZoneDefinition.RenderXsDuration(timeSpan), this.typeNameSpace);
				TimeZoneDefinition.XmlHelper.CreateTextElement(xmlTransition, this.typePrefix, "Month", recurringTime.Month.ToString(), this.typeNameSpace);
			}
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x0004E2E8 File Offset: 0x0004C4E8
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

		// Token: 0x04000B5D RID: 2909
		private const string XmlAttributeBias = "Bias";

		// Token: 0x04000B5E RID: 2910
		private const string XmlAttributeId = "Id";

		// Token: 0x04000B5F RID: 2911
		private const string XmlAttributeKind = "Kind";

		// Token: 0x04000B60 RID: 2912
		private const string XmlAttributeName = "Name";

		// Token: 0x04000B61 RID: 2913
		private const string XmlElementAbsoluteDateTransition = "AbsoluteDateTransition";

		// Token: 0x04000B62 RID: 2914
		private const string XmlElementDateTime = "DateTime";

		// Token: 0x04000B63 RID: 2915
		private const string XmlElementDay = "Day";

		// Token: 0x04000B64 RID: 2916
		private const string XmlElementDayOfWeek = "DayOfWeek";

		// Token: 0x04000B65 RID: 2917
		private const string XmlElementGroup = "Group";

		// Token: 0x04000B66 RID: 2918
		private const string XmlElementMonth = "Month";

		// Token: 0x04000B67 RID: 2919
		private const string XmlElementTimeOffset = "TimeOffset";

		// Token: 0x04000B68 RID: 2920
		private const string XmlElementTo = "To";

		// Token: 0x04000B69 RID: 2921
		private const string XmlElementTransition = "Transition";

		// Token: 0x04000B6A RID: 2922
		private const string XmlElementTransitions = "Transitions";

		// Token: 0x04000B6B RID: 2923
		private const string XmlElementTransitionsGroups = "TransitionsGroups";

		// Token: 0x04000B6C RID: 2924
		private const string XmlElementTransitionsGroup = "TransitionsGroup";

		// Token: 0x04000B6D RID: 2925
		private const string XmlElementOccurrence = "Occurrence";

		// Token: 0x04000B6E RID: 2926
		private const string XmlElemendPeriod = "Period";

		// Token: 0x04000B6F RID: 2927
		private const string XmlElementPeriods = "Periods";

		// Token: 0x04000B70 RID: 2928
		private const string XmlElementRecurringDateTransition = "RecurringDateTransition";

		// Token: 0x04000B71 RID: 2929
		private const string XmlElementRecurringDayTransition = "RecurringDayTransition";

		// Token: 0x04000B72 RID: 2930
		private const string customTimeZoneId = "tzone://Microsoft/Custom";

		// Token: 0x04000B73 RID: 2931
		private string id;

		// Token: 0x04000B74 RID: 2932
		private string name;

		// Token: 0x04000B75 RID: 2933
		protected TimeZoneDefinition.PeriodType[] periods;

		// Token: 0x04000B76 RID: 2934
		protected TimeZoneDefinition.TransitionsGroup[] transitionsGroups;

		// Token: 0x04000B77 RID: 2935
		protected TimeZoneDefinition.TransitionsGroup transitions;

		// Token: 0x04000B78 RID: 2936
		private Dictionary<string, TimeZoneDefinition.PeriodType> periodsDictionary;

		// Token: 0x04000B79 RID: 2937
		private ExTimeZone timeZone;

		// Token: 0x04000B7A RID: 2938
		private string typeNameSpace;

		// Token: 0x04000B7B RID: 2939
		private string typePrefix;

		// Token: 0x0200024B RID: 587
		internal enum TransitionTargetKindType
		{
			// Token: 0x04000B7D RID: 2941
			Period,
			// Token: 0x04000B7E RID: 2942
			Group
		}

		// Token: 0x0200024C RID: 588
		internal class TransitionsGroup
		{
			// Token: 0x060010FD RID: 4349 RVA: 0x0004E3F1 File Offset: 0x0004C5F1
			public TransitionsGroup(bool transitionsGroup, string id, TimeZoneDefinition.Transition[] transitions) : this(transitionsGroup)
			{
				this.id = id;
				this.transitions = transitions;
			}

			// Token: 0x060010FE RID: 4350 RVA: 0x0004E408 File Offset: 0x0004C608
			internal TransitionsGroup(bool transitionsGroup)
			{
				this.name = (transitionsGroup ? "TransitionsGroup" : "Transitions");
			}

			// Token: 0x17000466 RID: 1126
			// (get) Token: 0x060010FF RID: 4351 RVA: 0x0004E425 File Offset: 0x0004C625
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x17000467 RID: 1127
			// (get) Token: 0x06001100 RID: 4352 RVA: 0x0004E42D File Offset: 0x0004C62D
			// (set) Token: 0x06001101 RID: 4353 RVA: 0x0004E435 File Offset: 0x0004C635
			public TimeZoneDefinition.Transition[] Transitions
			{
				get
				{
					return this.transitions;
				}
				set
				{
					this.transitions = value;
				}
			}

			// Token: 0x17000468 RID: 1128
			// (get) Token: 0x06001102 RID: 4354 RVA: 0x0004E43E File Offset: 0x0004C63E
			// (set) Token: 0x06001103 RID: 4355 RVA: 0x0004E446 File Offset: 0x0004C646
			public string Id
			{
				get
				{
					return this.id;
				}
				set
				{
					this.id = value;
				}
			}

			// Token: 0x04000B7F RID: 2943
			private readonly string name;

			// Token: 0x04000B80 RID: 2944
			private TimeZoneDefinition.Transition[] transitions;

			// Token: 0x04000B81 RID: 2945
			private string id;
		}

		// Token: 0x0200024D RID: 589
		internal class Transition
		{
			// Token: 0x06001104 RID: 4356 RVA: 0x0004E44F File Offset: 0x0004C64F
			public Transition(TimeZoneDefinition.TransitionTargetType to)
			{
				this.to = to;
			}

			// Token: 0x17000469 RID: 1129
			// (get) Token: 0x06001105 RID: 4357 RVA: 0x0004E45E File Offset: 0x0004C65E
			// (set) Token: 0x06001106 RID: 4358 RVA: 0x0004E466 File Offset: 0x0004C666
			public TimeZoneDefinition.TransitionTargetType To
			{
				get
				{
					return this.to;
				}
				set
				{
					this.to = value;
				}
			}

			// Token: 0x04000B82 RID: 2946
			private TimeZoneDefinition.TransitionTargetType to;
		}

		// Token: 0x0200024E RID: 590
		internal class TransitionTargetType
		{
			// Token: 0x06001107 RID: 4359 RVA: 0x0004E470 File Offset: 0x0004C670
			public TransitionTargetType(XmlElement xmlParent)
			{
				XmlElement xmlElement = xmlParent["To", xmlParent.NamespaceURI];
				string attribute = xmlElement.GetAttribute("Kind");
				this.Value = xmlElement.InnerText;
				string a;
				if ((a = attribute) != null)
				{
					if (a == "Group")
					{
						this.Kind = TimeZoneDefinition.TransitionTargetKindType.Group;
						return;
					}
					if (!(a == "Period"))
					{
						return;
					}
					this.Kind = TimeZoneDefinition.TransitionTargetKindType.Period;
				}
			}

			// Token: 0x06001108 RID: 4360 RVA: 0x0004E4DC File Offset: 0x0004C6DC
			public TransitionTargetType(TimeZoneDefinition.TransitionTargetKindType kind, string target)
			{
				this.kind = kind;
				this.value = target;
			}

			// Token: 0x1700046A RID: 1130
			// (get) Token: 0x06001109 RID: 4361 RVA: 0x0004E4F2 File Offset: 0x0004C6F2
			// (set) Token: 0x0600110A RID: 4362 RVA: 0x0004E4FA File Offset: 0x0004C6FA
			public TimeZoneDefinition.TransitionTargetKindType Kind
			{
				get
				{
					return this.kind;
				}
				set
				{
					this.kind = value;
				}
			}

			// Token: 0x1700046B RID: 1131
			// (get) Token: 0x0600110B RID: 4363 RVA: 0x0004E503 File Offset: 0x0004C703
			// (set) Token: 0x0600110C RID: 4364 RVA: 0x0004E50B File Offset: 0x0004C70B
			public string Value
			{
				get
				{
					return this.value;
				}
				set
				{
					this.value = value;
				}
			}

			// Token: 0x04000B83 RID: 2947
			private TimeZoneDefinition.TransitionTargetKindType kind;

			// Token: 0x04000B84 RID: 2948
			private string value;
		}

		// Token: 0x0200024F RID: 591
		internal class AbsoluteDateTransition : TimeZoneDefinition.Transition
		{
			// Token: 0x0600110D RID: 4365 RVA: 0x0004E514 File Offset: 0x0004C714
			public AbsoluteDateTransition(TimeZoneDefinition.TransitionTargetType to, DateTime dateTime) : base(to)
			{
				this.dateTime = dateTime;
			}

			// Token: 0x1700046C RID: 1132
			// (get) Token: 0x0600110E RID: 4366 RVA: 0x0004E524 File Offset: 0x0004C724
			// (set) Token: 0x0600110F RID: 4367 RVA: 0x0004E52C File Offset: 0x0004C72C
			public DateTime DateTime
			{
				get
				{
					return this.dateTime;
				}
				set
				{
					this.dateTime = value;
				}
			}

			// Token: 0x04000B85 RID: 2949
			private DateTime dateTime;
		}

		// Token: 0x02000250 RID: 592
		internal abstract class RecurringTimeTransitionType : TimeZoneDefinition.Transition
		{
			// Token: 0x06001110 RID: 4368 RVA: 0x0004E535 File Offset: 0x0004C735
			public RecurringTimeTransitionType(TimeZoneDefinition.TransitionTargetType to, string timeOffset, int month) : base(to)
			{
				this.timeOffset = timeOffset;
				this.month = month;
			}

			// Token: 0x1700046D RID: 1133
			// (get) Token: 0x06001111 RID: 4369 RVA: 0x0004E54C File Offset: 0x0004C74C
			// (set) Token: 0x06001112 RID: 4370 RVA: 0x0004E554 File Offset: 0x0004C754
			public string TimeOffset
			{
				get
				{
					return this.timeOffset;
				}
				set
				{
					this.timeOffset = value;
				}
			}

			// Token: 0x1700046E RID: 1134
			// (get) Token: 0x06001113 RID: 4371 RVA: 0x0004E55D File Offset: 0x0004C75D
			// (set) Token: 0x06001114 RID: 4372 RVA: 0x0004E565 File Offset: 0x0004C765
			public int Month
			{
				get
				{
					return this.month;
				}
				set
				{
					this.month = value;
				}
			}

			// Token: 0x04000B86 RID: 2950
			private string timeOffset;

			// Token: 0x04000B87 RID: 2951
			private int month;
		}

		// Token: 0x02000251 RID: 593
		internal class RecurringDayTransition : TimeZoneDefinition.RecurringTimeTransitionType
		{
			// Token: 0x06001115 RID: 4373 RVA: 0x0004E56E File Offset: 0x0004C76E
			public RecurringDayTransition(TimeZoneDefinition.TransitionTargetType to, string timeOffset, int month, string dayOfWeek, int occurrence) : base(to, timeOffset, month)
			{
				this.dayOfWeek = dayOfWeek;
				this.occurrence = occurrence;
			}

			// Token: 0x1700046F RID: 1135
			// (get) Token: 0x06001116 RID: 4374 RVA: 0x0004E589 File Offset: 0x0004C789
			// (set) Token: 0x06001117 RID: 4375 RVA: 0x0004E591 File Offset: 0x0004C791
			public string DayOfWeek
			{
				get
				{
					return this.dayOfWeek;
				}
				set
				{
					this.dayOfWeek = value;
				}
			}

			// Token: 0x17000470 RID: 1136
			// (get) Token: 0x06001118 RID: 4376 RVA: 0x0004E59A File Offset: 0x0004C79A
			// (set) Token: 0x06001119 RID: 4377 RVA: 0x0004E5A2 File Offset: 0x0004C7A2
			public int Occurrence
			{
				get
				{
					return this.occurrence;
				}
				set
				{
					this.occurrence = value;
				}
			}

			// Token: 0x04000B88 RID: 2952
			private string dayOfWeek;

			// Token: 0x04000B89 RID: 2953
			private int occurrence;
		}

		// Token: 0x02000252 RID: 594
		internal class RecurringDateTransition : TimeZoneDefinition.RecurringTimeTransitionType
		{
			// Token: 0x0600111A RID: 4378 RVA: 0x0004E5AB File Offset: 0x0004C7AB
			public RecurringDateTransition(TimeZoneDefinition.TransitionTargetType to, string timeOffset, int month, int day) : base(to, timeOffset, month)
			{
				this.day = day;
			}

			// Token: 0x17000471 RID: 1137
			// (get) Token: 0x0600111B RID: 4379 RVA: 0x0004E5BE File Offset: 0x0004C7BE
			// (set) Token: 0x0600111C RID: 4380 RVA: 0x0004E5C6 File Offset: 0x0004C7C6
			public int Day
			{
				get
				{
					return this.day;
				}
				set
				{
					this.day = value;
				}
			}

			// Token: 0x04000B8A RID: 2954
			private int day;
		}

		// Token: 0x02000253 RID: 595
		internal class PeriodType
		{
			// Token: 0x0600111D RID: 4381 RVA: 0x0004E5CF File Offset: 0x0004C7CF
			public PeriodType(string bias, string name, string id)
			{
				this.bias = bias;
				this.name = name;
				this.id = id;
			}

			// Token: 0x17000472 RID: 1138
			// (get) Token: 0x0600111E RID: 4382 RVA: 0x0004E5EC File Offset: 0x0004C7EC
			// (set) Token: 0x0600111F RID: 4383 RVA: 0x0004E5F4 File Offset: 0x0004C7F4
			public string Bias
			{
				get
				{
					return this.bias;
				}
				set
				{
					this.bias = value;
				}
			}

			// Token: 0x17000473 RID: 1139
			// (get) Token: 0x06001120 RID: 4384 RVA: 0x0004E5FD File Offset: 0x0004C7FD
			// (set) Token: 0x06001121 RID: 4385 RVA: 0x0004E605 File Offset: 0x0004C805
			public string Name
			{
				get
				{
					return this.name;
				}
				set
				{
					this.name = value;
				}
			}

			// Token: 0x17000474 RID: 1140
			// (get) Token: 0x06001122 RID: 4386 RVA: 0x0004E60E File Offset: 0x0004C80E
			// (set) Token: 0x06001123 RID: 4387 RVA: 0x0004E616 File Offset: 0x0004C816
			public string Id
			{
				get
				{
					return this.id;
				}
				set
				{
					this.id = value;
				}
			}

			// Token: 0x04000B8B RID: 2955
			private string bias;

			// Token: 0x04000B8C RID: 2956
			private string name;

			// Token: 0x04000B8D RID: 2957
			private string id;
		}

		// Token: 0x02000254 RID: 596
		private class Diagnostics
		{
			// Token: 0x06001124 RID: 4388 RVA: 0x0004E61F File Offset: 0x0004C81F
			[Conditional("DEBUG")]
			internal static void Assert(bool condition)
			{
			}

			// Token: 0x06001125 RID: 4389 RVA: 0x0004E621 File Offset: 0x0004C821
			[Conditional("DEBUG")]
			internal static void Assert(bool condition, string format, params object[] parameters)
			{
			}
		}

		// Token: 0x02000255 RID: 597
		private class XmlHelper
		{
			// Token: 0x06001127 RID: 4391 RVA: 0x0004E62C File Offset: 0x0004C82C
			public static XmlElement CreateElement(XmlElement parentElement, string prefix, string localName, string namespaceUri)
			{
				XmlElement xmlElement = TimeZoneDefinition.XmlHelper.CreateElement(parentElement.OwnerDocument, prefix, localName, namespaceUri);
				parentElement.AppendChild(xmlElement);
				return xmlElement;
			}

			// Token: 0x06001128 RID: 4392 RVA: 0x0004E651 File Offset: 0x0004C851
			public static XmlElement CreateElement(XmlDocument xmlDocument, string prefix, string localName, string namespaceUri)
			{
				return xmlDocument.CreateElement(prefix, localName, namespaceUri);
			}

			// Token: 0x06001129 RID: 4393 RVA: 0x0004E65C File Offset: 0x0004C85C
			public static XmlElement CreateTextElement(XmlElement parentElement, string prefix, string localName, string textValue, string namespaceUri)
			{
				XmlElement xmlElement = TimeZoneDefinition.XmlHelper.CreateElement(parentElement, prefix, localName, namespaceUri);
				TimeZoneDefinition.XmlHelper.AppendText(xmlElement, textValue);
				return xmlElement;
			}

			// Token: 0x0600112A RID: 4394 RVA: 0x0004E67C File Offset: 0x0004C87C
			private static void AppendText(XmlElement parentElement, string textValue)
			{
				if (!string.IsNullOrEmpty(textValue))
				{
					XmlText newChild = parentElement.OwnerDocument.CreateTextNode(textValue);
					parentElement.AppendChild(newChild);
				}
			}

			// Token: 0x0600112B RID: 4395 RVA: 0x0004E6A6 File Offset: 0x0004C8A6
			public static XmlAttribute CreateAttribute(XmlElement parentElement, string attributeName, string attributeValue)
			{
				return TimeZoneDefinition.XmlHelper.CreateAttribute(parentElement, attributeName, attributeValue, true);
			}

			// Token: 0x0600112C RID: 4396 RVA: 0x0004E6B4 File Offset: 0x0004C8B4
			public static XmlAttribute CreateAttribute(XmlElement parentElement, string attributeName, string attributeValue, bool checkAttributeValueIsEmpty)
			{
				XmlAttribute xmlAttribute = parentElement.OwnerDocument.CreateAttribute(attributeName);
				xmlAttribute.Value = attributeValue;
				parentElement.Attributes.Append(xmlAttribute);
				return xmlAttribute;
			}
		}
	}
}

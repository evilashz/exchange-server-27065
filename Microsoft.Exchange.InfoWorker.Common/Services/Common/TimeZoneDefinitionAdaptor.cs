using System;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.Services.Common
{
	// Token: 0x02000256 RID: 598
	internal class TimeZoneDefinitionAdaptor : TimeZoneDefinition
	{
		// Token: 0x0600112E RID: 4398 RVA: 0x0004E6F0 File Offset: 0x0004C8F0
		private void ConvertPeriods()
		{
			this.periods = new TimeZoneDefinition.PeriodType[this.timeZoneDefinitionProxy.Periods.Length];
			int num = 0;
			foreach (Microsoft.Exchange.SoapWebClient.EWS.PeriodType periodType in this.timeZoneDefinitionProxy.Periods)
			{
				this.periods[num++] = new TimeZoneDefinition.PeriodType(periodType.Bias, periodType.Name, periodType.Id);
			}
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x0004E758 File Offset: 0x0004C958
		private void ConvertTransitionsGroups()
		{
			this.transitionsGroups = new TimeZoneDefinition.TransitionsGroup[this.timeZoneDefinitionProxy.TransitionsGroups.Length];
			int num = 0;
			foreach (ArrayOfTransitionsType allTransitions in this.timeZoneDefinitionProxy.TransitionsGroups)
			{
				TimeZoneDefinition.TransitionsGroup container = new TimeZoneDefinition.TransitionsGroup(true);
				this.transitionsGroups[num++] = this.ConvertTransitions(container, allTransitions);
			}
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x0004E7C0 File Offset: 0x0004C9C0
		private void ConvertTransitions()
		{
			TimeZoneDefinition.TransitionsGroup container = new TimeZoneDefinition.TransitionsGroup(false);
			this.transitions = this.ConvertTransitions(container, this.timeZoneDefinitionProxy.Transitions);
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x0004E7EC File Offset: 0x0004C9EC
		public TimeZoneDefinitionAdaptor(TimeZoneDefinitionType timeZoneDefinitionProxy) : base(timeZoneDefinitionProxy.Name, timeZoneDefinitionProxy.Id)
		{
			this.timeZoneDefinitionProxy = timeZoneDefinitionProxy;
			this.ConvertPeriods();
			this.ConvertTransitions();
			this.ConvertTransitionsGroups();
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x0004E81C File Offset: 0x0004CA1C
		private TimeZoneDefinition.TransitionsGroup ConvertTransitions(TimeZoneDefinition.TransitionsGroup container, ArrayOfTransitionsType allTransitions)
		{
			container.Id = allTransitions.Id;
			container.Transitions = new TimeZoneDefinition.Transition[allTransitions.Items.Length];
			int num = 0;
			foreach (TransitionType transitionType in allTransitions.Items)
			{
				container.Transitions[num++] = this.ConvertTransition(transitionType);
			}
			return container;
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x0004E877 File Offset: 0x0004CA77
		private TimeZoneDefinition.TransitionTargetKindType ConvertTransitionKind(Microsoft.Exchange.SoapWebClient.EWS.TransitionTargetKindType transitionKindProxy)
		{
			if (transitionKindProxy == Microsoft.Exchange.SoapWebClient.EWS.TransitionTargetKindType.Group)
			{
				return TimeZoneDefinition.TransitionTargetKindType.Group;
			}
			return TimeZoneDefinition.TransitionTargetKindType.Period;
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x0004E880 File Offset: 0x0004CA80
		private TimeZoneDefinition.Transition ConvertTransition(TransitionType transitionType)
		{
			TimeZoneDefinition.TransitionTargetType to = new TimeZoneDefinition.TransitionTargetType(this.ConvertTransitionKind(transitionType.To.Kind), transitionType.To.Value);
			AbsoluteDateTransitionType absoluteDateTransitionType = transitionType as AbsoluteDateTransitionType;
			if (absoluteDateTransitionType != null)
			{
				return new TimeZoneDefinition.AbsoluteDateTransition(to, absoluteDateTransitionType.DateTime);
			}
			RecurringDayTransitionType recurringDayTransitionType = transitionType as RecurringDayTransitionType;
			if (recurringDayTransitionType != null)
			{
				return new TimeZoneDefinition.RecurringDayTransition(to, recurringDayTransitionType.TimeOffset, recurringDayTransitionType.Month, recurringDayTransitionType.DayOfWeek, recurringDayTransitionType.Occurrence);
			}
			RecurringDateTransitionType recurringDateTransitionType = transitionType as RecurringDateTransitionType;
			if (recurringDateTransitionType != null)
			{
				return new TimeZoneDefinition.RecurringDateTransition(to, recurringDateTransitionType.TimeOffset, recurringDateTransitionType.Month, recurringDateTransitionType.Day);
			}
			return new TimeZoneDefinition.Transition(to);
		}

		// Token: 0x04000B8E RID: 2958
		private TimeZoneDefinitionType timeZoneDefinitionProxy;
	}
}

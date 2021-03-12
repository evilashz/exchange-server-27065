using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.UM.PersonalAutoAttendant
{
	// Token: 0x02000116 RID: 278
	internal class PAARulesEvaluator : IRuleEvaluator
	{
		// Token: 0x06000922 RID: 2338 RVA: 0x00023C3B File Offset: 0x00021E3B
		private PAARulesEvaluator(IList<IRuleEvaluator> rules)
		{
			this.rules = rules;
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x00023C4C File Offset: 0x00021E4C
		public static PAARulesEvaluator Create(PersonalAutoAttendant paa)
		{
			IList<IRuleEvaluator> list = new List<IRuleEvaluator>();
			if (paa.ExtensionList.Count > 0)
			{
				list.Add(new ExtensionListEvaluator(paa.ExtensionList));
			}
			if (paa.TimeOfDay != TimeOfDayEnum.None)
			{
				list.Add(new TimeOfDayRuleEvaluator(paa.TimeOfDay, paa.WorkingPeriod));
			}
			if (paa.OutOfOffice != OutOfOfficeStatusEnum.None)
			{
				list.Add(new OutOfOfficeRuleEvaluator(paa.OutOfOffice));
			}
			if (paa.CallerIdList.Count > 0)
			{
				list.Add(new CallerIdRuleEvaluator(paa.CallerIdList));
			}
			if (paa.FreeBusy != FreeBusyStatusEnum.None)
			{
				list.Add(new FreeBusyRuleEvaluator(paa.FreeBusy));
			}
			return new PAARulesEvaluator(list);
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00023CF4 File Offset: 0x00021EF4
		public bool Evaluate(IDataLoader dataLoader)
		{
			for (int i = 0; i < this.rules.Count; i++)
			{
				IRuleEvaluator ruleEvaluator = this.rules[i];
				if (!ruleEvaluator.Evaluate(dataLoader))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400051F RID: 1311
		private IList<IRuleEvaluator> rules;
	}
}

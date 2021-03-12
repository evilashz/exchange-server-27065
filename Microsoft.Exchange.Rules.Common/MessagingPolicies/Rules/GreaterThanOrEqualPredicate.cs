using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200001D RID: 29
	public class GreaterThanOrEqualPredicate : PredicateCondition
	{
		// Token: 0x06000097 RID: 151 RVA: 0x0000369F File Offset: 0x0000189F
		public GreaterThanOrEqualPredicate(Property property, ShortList<string> entries, RulesCreationContext creationContext) : base(property, entries, creationContext)
		{
			if (!base.Property.IsNumerical)
			{
				throw new RulesValidationException(RulesStrings.NumericalPropertyRequiredForPredicate(this.Name));
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000036C8 File Offset: 0x000018C8
		public override string Name
		{
			get
			{
				return "greaterThanOrEqual";
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000099 RID: 153 RVA: 0x000036CF File Offset: 0x000018CF
		public override Version MinimumVersion
		{
			get
			{
				if (string.CompareOrdinal(base.Property.Name, "Message.Size") == 0)
				{
					return Rule.BaseVersion15;
				}
				return base.MinimumVersion;
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000036F4 File Offset: 0x000018F4
		public override bool Evaluate(RulesEvaluationContext context)
		{
			bool flag = base.ComparePropertyAndValue(context) >= 0;
			base.UpdateEvaluationHistory(context, flag, null, 0);
			return flag;
		}
	}
}

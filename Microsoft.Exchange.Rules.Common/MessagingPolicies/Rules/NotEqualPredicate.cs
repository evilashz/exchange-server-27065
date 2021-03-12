using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000020 RID: 32
	public class NotEqualPredicate : PredicateCondition
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x000037C7 File Offset: 0x000019C7
		public NotEqualPredicate(Property property, ShortList<string> entries, RulesCreationContext creationContext) : base(property, entries, creationContext)
		{
			if (!base.Property.IsNumerical)
			{
				throw new RulesValidationException(RulesStrings.NumericalPropertyRequiredForPredicate(this.Name));
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000037F0 File Offset: 0x000019F0
		public override string Name
		{
			get
			{
				return "notEqual";
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000037F8 File Offset: 0x000019F8
		public override bool Evaluate(RulesEvaluationContext context)
		{
			bool flag = base.ComparePropertyAndValue(context) != 0;
			base.UpdateEvaluationHistory(context, flag, null, 0);
			return flag;
		}
	}
}

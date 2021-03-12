using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200001C RID: 28
	public class LessThanPredicate : PredicateCondition
	{
		// Token: 0x06000094 RID: 148 RVA: 0x0000364B File Offset: 0x0000184B
		public LessThanPredicate(Property property, ShortList<string> entries, RulesCreationContext creationContext) : base(property, entries, creationContext)
		{
			if (!base.Property.IsNumerical)
			{
				throw new RulesValidationException(RulesStrings.NumericalPropertyRequiredForPredicate(this.Name));
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00003674 File Offset: 0x00001874
		public override string Name
		{
			get
			{
				return "lessThan";
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000367C File Offset: 0x0000187C
		public override bool Evaluate(RulesEvaluationContext context)
		{
			bool flag = base.ComparePropertyAndValue(context) < 0;
			base.UpdateEvaluationHistory(context, flag, null, 0);
			return flag;
		}
	}
}

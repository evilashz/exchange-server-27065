using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200001E RID: 30
	public class LessThanOrEqualPredicate : PredicateCondition
	{
		// Token: 0x0600009B RID: 155 RVA: 0x0000371A File Offset: 0x0000191A
		public LessThanOrEqualPredicate(Property property, ShortList<string> entries, RulesCreationContext creationContext) : base(property, entries, creationContext)
		{
			if (!base.Property.IsNumerical)
			{
				throw new RulesValidationException(RulesStrings.NumericalPropertyRequiredForPredicate(this.Name));
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00003743 File Offset: 0x00001943
		public override string Name
		{
			get
			{
				return "lessThanOrEqual";
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000374C File Offset: 0x0000194C
		public override bool Evaluate(RulesEvaluationContext context)
		{
			bool flag = base.ComparePropertyAndValue(context) <= 0;
			base.UpdateEvaluationHistory(context, flag, null, 0);
			return flag;
		}
	}
}

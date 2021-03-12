using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200001B RID: 27
	public class GreaterThanPredicate : PredicateCondition
	{
		// Token: 0x06000091 RID: 145 RVA: 0x000035F6 File Offset: 0x000017F6
		public GreaterThanPredicate(Property property, ShortList<string> entries, RulesCreationContext creationContext) : base(property, entries, creationContext)
		{
			if (!base.Property.IsNumerical)
			{
				throw new RulesValidationException(RulesStrings.NumericalPropertyRequiredForPredicate(this.Name));
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000092 RID: 146 RVA: 0x0000361F File Offset: 0x0000181F
		public override string Name
		{
			get
			{
				return "greaterThan";
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003628 File Offset: 0x00001828
		public override bool Evaluate(RulesEvaluationContext context)
		{
			bool flag = base.ComparePropertyAndValue(context) > 0;
			base.UpdateEvaluationHistory(context, flag, null, 0);
			return flag;
		}
	}
}

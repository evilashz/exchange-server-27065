using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200001F RID: 31
	public class EqualPredicate : PredicateCondition
	{
		// Token: 0x0600009E RID: 158 RVA: 0x00003772 File Offset: 0x00001972
		public EqualPredicate(Property property, ShortList<string> entries, RulesCreationContext creationContext) : base(property, entries, creationContext)
		{
			if (!base.Property.IsNumerical)
			{
				throw new RulesValidationException(RulesStrings.NumericalPropertyRequiredForPredicate(this.Name));
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600009F RID: 159 RVA: 0x0000379B File Offset: 0x0000199B
		public override string Name
		{
			get
			{
				return "equal";
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000037A4 File Offset: 0x000019A4
		public override bool Evaluate(RulesEvaluationContext context)
		{
			bool flag = base.ComparePropertyAndValue(context) == 0;
			base.UpdateEvaluationHistory(context, flag, null, 0);
			return flag;
		}
	}
}

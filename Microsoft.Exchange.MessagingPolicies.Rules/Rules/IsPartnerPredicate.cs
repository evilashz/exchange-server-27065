using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200009F RID: 159
	internal class IsPartnerPredicate : PredicateCondition
	{
		// Token: 0x06000480 RID: 1152 RVA: 0x00016BED File Offset: 0x00014DED
		public IsPartnerPredicate(Property property, ShortList<string> entries, RulesCreationContext creationContext) : base(property, entries, creationContext)
		{
			if (!base.Property.IsString)
			{
				throw new RulesValidationException(RulesStrings.StringPropertyOrValueRequired(this.Name));
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x00016C16 File Offset: 0x00014E16
		public override string Name
		{
			get
			{
				return "isPartner";
			}
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00016C20 File Offset: 0x00014E20
		public override bool Evaluate(RulesEvaluationContext baseContext)
		{
			TransportRulesEvaluationContext transportRulesEvaluationContext = (TransportRulesEvaluationContext)baseContext;
			transportRulesEvaluationContext.PredicateName = this.Name;
			return false;
		}
	}
}

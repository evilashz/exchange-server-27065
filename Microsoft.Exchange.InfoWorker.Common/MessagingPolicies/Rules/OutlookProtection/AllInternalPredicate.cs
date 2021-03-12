using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules.OutlookProtection
{
	// Token: 0x0200017F RID: 383
	internal sealed class AllInternalPredicate : PredicateCondition
	{
		// Token: 0x06000A43 RID: 2627 RVA: 0x0002C00B File Offset: 0x0002A20B
		public AllInternalPredicate() : base(new StringProperty("Message.ToCcBcc"), new ShortList<string>(), new RulesCreationContext())
		{
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x0002C027 File Offset: 0x0002A227
		public override string Name
		{
			get
			{
				return "allInternal";
			}
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0002C02E File Offset: 0x0002A22E
		public override bool Evaluate(RulesEvaluationContext context)
		{
			throw new NotSupportedException("Outlook Protection rules are only evaluated on Outlook.");
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0002C03A File Offset: 0x0002A23A
		protected override Value BuildValue(ShortList<string> entries, RulesCreationContext creationContext)
		{
			return null;
		}
	}
}

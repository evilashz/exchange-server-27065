using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules.OutlookProtection
{
	// Token: 0x02000184 RID: 388
	internal sealed class RecipientIsPredicate : PredicateCondition
	{
		// Token: 0x06000A5D RID: 2653 RVA: 0x0002C399 File Offset: 0x0002A599
		public RecipientIsPredicate(ShortList<string> entries, RulesCreationContext creationContext) : base(new StringProperty("Message.ToCcBcc"), entries, creationContext)
		{
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000A5E RID: 2654 RVA: 0x0002C3AD File Offset: 0x0002A5AD
		public override string Name
		{
			get
			{
				return "recipientIs";
			}
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0002C3B4 File Offset: 0x0002A5B4
		public override bool Evaluate(RulesEvaluationContext context)
		{
			throw new NotSupportedException("Outlook Protection rules are only evaluated on Outlook.");
		}
	}
}

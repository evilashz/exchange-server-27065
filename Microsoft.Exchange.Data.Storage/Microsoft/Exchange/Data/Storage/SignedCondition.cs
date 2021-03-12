using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000BA7 RID: 2983
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SignedCondition : Condition
	{
		// Token: 0x06006AC8 RID: 27336 RVA: 0x001C7C07 File Offset: 0x001C5E07
		private SignedCondition(Rule rule) : base(ConditionType.SignedCondition, rule)
		{
		}

		// Token: 0x06006AC9 RID: 27337 RVA: 0x001C7C14 File Offset: 0x001C5E14
		public static SignedCondition Create(Rule rule)
		{
			Condition.CheckParams(new object[]
			{
				rule
			});
			return new SignedCondition(rule);
		}

		// Token: 0x06006ACA RID: 27338 RVA: 0x001C7C38 File Offset: 0x001C5E38
		internal override Restriction BuildRestriction()
		{
			PropTag tag = base.Rule.PropertyDefinitionToPropTagFromCache(InternalSchema.IsSigned);
			return new Restriction.PropertyRestriction(Restriction.RelOp.Equal, tag, true);
		}
	}
}

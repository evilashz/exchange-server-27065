using System;
using System.Collections.Generic;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000D3 RID: 211
	public class LessThanPredicate : PredicateCondition
	{
		// Token: 0x06000560 RID: 1376 RVA: 0x00010821 File Offset: 0x0000EA21
		public LessThanPredicate(Property property, List<string> entries) : base(property, entries)
		{
			if (!base.Property.IsComparableType)
			{
				throw new CompliancePolicyValidationException(string.Format("Type {0} is not supported by Predicate '{1}'", base.Property.Type, this.Name));
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x00010859 File Offset: 0x0000EA59
		public override string Name
		{
			get
			{
				return "lessThan";
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x00010860 File Offset: 0x0000EA60
		public override Version MinimumVersion
		{
			get
			{
				if (base.Property.Type == typeof(long))
				{
					return LessThanPredicate.minVersion;
				}
				return base.MinimumVersion;
			}
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0001088A File Offset: 0x0000EA8A
		public override bool Evaluate(PolicyEvaluationContext context)
		{
			return base.CompareComparablePropertyAndValue(context) < 0;
		}

		// Token: 0x04000336 RID: 822
		private static readonly Version minVersion = new Version("1.00.0002.000");
	}
}

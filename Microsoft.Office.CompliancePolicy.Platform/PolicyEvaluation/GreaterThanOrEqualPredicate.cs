using System;
using System.Collections.Generic;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000CF RID: 207
	public class GreaterThanOrEqualPredicate : PredicateCondition
	{
		// Token: 0x0600054C RID: 1356 RVA: 0x00010476 File Offset: 0x0000E676
		public GreaterThanOrEqualPredicate(Property property, List<string> entries) : base(property, entries)
		{
			if (!base.Property.IsComparableType)
			{
				throw new CompliancePolicyValidationException(string.Format("Type {0} is not supported by Predicate '{1}'", base.Property.Type, this.Name));
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x000104AE File Offset: 0x0000E6AE
		public override string Name
		{
			get
			{
				return "greaterThanOrEqual";
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x000104B5 File Offset: 0x0000E6B5
		public override Version MinimumVersion
		{
			get
			{
				if (base.Property.Type == typeof(long))
				{
					return GreaterThanOrEqualPredicate.minVersion;
				}
				return base.MinimumVersion;
			}
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x000104DF File Offset: 0x0000E6DF
		public override bool Evaluate(PolicyEvaluationContext context)
		{
			return base.CompareComparablePropertyAndValue(context) >= 0;
		}

		// Token: 0x04000332 RID: 818
		private static readonly Version minVersion = new Version("1.00.0002.000");
	}
}

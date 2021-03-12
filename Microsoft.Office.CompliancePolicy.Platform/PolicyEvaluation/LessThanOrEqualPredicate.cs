using System;
using System.Collections.Generic;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000D2 RID: 210
	public class LessThanOrEqualPredicate : PredicateCondition
	{
		// Token: 0x0600055B RID: 1371 RVA: 0x00010798 File Offset: 0x0000E998
		public LessThanOrEqualPredicate(Property property, List<string> entries) : base(property, entries)
		{
			if (!base.Property.IsComparableType)
			{
				throw new CompliancePolicyValidationException(string.Format("Type {0} is not supported by Predicate '{1}'", base.Property.Type, this.Name));
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x000107D0 File Offset: 0x0000E9D0
		public override string Name
		{
			get
			{
				return "lessThanOrEqual";
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x000107D7 File Offset: 0x0000E9D7
		public override Version MinimumVersion
		{
			get
			{
				if (base.Property.Type == typeof(long))
				{
					return LessThanOrEqualPredicate.minVersion;
				}
				return base.MinimumVersion;
			}
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00010801 File Offset: 0x0000EA01
		public override bool Evaluate(PolicyEvaluationContext context)
		{
			return base.CompareComparablePropertyAndValue(context) <= 0;
		}

		// Token: 0x04000335 RID: 821
		private static readonly Version minVersion = new Version("1.00.0002.000");
	}
}

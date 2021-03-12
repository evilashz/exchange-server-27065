using System;
using System.Collections.Generic;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000D0 RID: 208
	public class GreaterThanPredicate : PredicateCondition
	{
		// Token: 0x06000551 RID: 1361 RVA: 0x000104FF File Offset: 0x0000E6FF
		public GreaterThanPredicate(Property property, List<string> entries) : base(property, entries)
		{
			if (!base.Property.IsComparableType)
			{
				throw new CompliancePolicyValidationException(string.Format("Type {0} is not supported by Predicate '{1}'", base.Property.Type, this.Name));
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x00010537 File Offset: 0x0000E737
		public override string Name
		{
			get
			{
				return "greaterThan";
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x0001053E File Offset: 0x0000E73E
		public override Version MinimumVersion
		{
			get
			{
				if (base.Property.Type == typeof(long))
				{
					return GreaterThanPredicate.minVersion;
				}
				return base.MinimumVersion;
			}
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00010568 File Offset: 0x0000E768
		public override bool Evaluate(PolicyEvaluationContext context)
		{
			return base.CompareComparablePropertyAndValue(context) > 0;
		}

		// Token: 0x04000333 RID: 819
		private static readonly Version minVersion = new Version("1.00.0002.000");
	}
}

using System;
using System.Collections.Generic;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000D5 RID: 213
	public class NotEqualPredicate : PredicateCondition
	{
		// Token: 0x0600056A RID: 1386 RVA: 0x0001095B File Offset: 0x0000EB5B
		public NotEqualPredicate(Property property, List<string> entries) : base(property, entries)
		{
			if (!base.Property.IsEquatableType)
			{
				throw new CompliancePolicyValidationException(string.Format("Type {0} is not supported by Predicate '{1}'", base.Property.Type, this.Name));
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x00010993 File Offset: 0x0000EB93
		public override string Name
		{
			get
			{
				return "notEqual";
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x0001099C File Offset: 0x0000EB9C
		public override Version MinimumVersion
		{
			get
			{
				if (base.Property.IsEnumType || base.Property.Type == typeof(long) || base.Property.Type == typeof(bool) || base.Property.Type == typeof(string) || base.Property.Type == typeof(Guid))
				{
					return NotEqualPredicate.minVersion;
				}
				return base.MinimumVersion;
			}
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00010A32 File Offset: 0x0000EC32
		public override bool Evaluate(PolicyEvaluationContext context)
		{
			return !base.CompareEquatablePropertyAndValue(context);
		}

		// Token: 0x04000337 RID: 823
		private static readonly Version minVersion = new Version("1.00.0002.000");
	}
}

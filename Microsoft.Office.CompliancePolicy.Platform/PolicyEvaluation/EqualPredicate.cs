using System;
using System.Collections.Generic;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000CD RID: 205
	public class EqualPredicate : PredicateCondition
	{
		// Token: 0x06000543 RID: 1347 RVA: 0x0001031E File Offset: 0x0000E51E
		public EqualPredicate(Property property, List<string> entries) : base(property, entries)
		{
			if (!base.Property.IsEquatableType)
			{
				throw new CompliancePolicyValidationException(string.Format("Type {0} is not supported by Predicate '{1}'", base.Property.Type, this.Name));
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x00010356 File Offset: 0x0000E556
		public override string Name
		{
			get
			{
				return "equal";
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x00010360 File Offset: 0x0000E560
		public override Version MinimumVersion
		{
			get
			{
				if (base.Property.IsEnumType || base.Property.Type == typeof(long) || base.Property.Type == typeof(bool) || base.Property.Type == typeof(string) || base.Property.Type == typeof(Guid))
				{
					return EqualPredicate.minVersion;
				}
				return base.MinimumVersion;
			}
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x000103F6 File Offset: 0x0000E5F6
		public override bool Evaluate(PolicyEvaluationContext context)
		{
			return base.CompareEquatablePropertyAndValue(context);
		}

		// Token: 0x04000331 RID: 817
		private static readonly Version minVersion = new Version("1.00.0002.000");
	}
}

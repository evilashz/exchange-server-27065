using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000D1 RID: 209
	public class IsPredicate : PredicateCondition
	{
		// Token: 0x06000556 RID: 1366 RVA: 0x00010588 File Offset: 0x0000E788
		public IsPredicate(Property property, List<string> entries) : base(property, entries)
		{
			if (!entries.Any<string>())
			{
				throw new ArgumentException("entries can not be empty");
			}
			if (!base.Property.IsEquatableType && !base.Property.IsEquatableCollectionType)
			{
				throw new CompliancePolicyValidationException(string.Format("Property type {0} is not supported by Predicate '{1}'", base.Property.Type, this.Name));
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x000105EB File Offset: 0x0000E7EB
		public override string Name
		{
			get
			{
				return "is";
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x000105F4 File Offset: 0x0000E7F4
		public override Version MinimumVersion
		{
			get
			{
				if (base.Property.Type == typeof(string) || base.Property.IsCollectionOfType(typeof(string)) || base.Property.Type == typeof(Guid))
				{
					return base.MinimumVersion;
				}
				return IsPredicate.minVersion;
			}
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0001065C File Offset: 0x0000E85C
		public override bool Evaluate(PolicyEvaluationContext context)
		{
			if (!base.Property.IsEquatableTo(base.Value))
			{
				throw new CompliancePolicyValidationException(string.Format("Rule '{0}' is in inconsitent state due to unknown property '{1}'", context.CurrentRule.Name, base.Property.Name));
			}
			object value = base.Property.GetValue(context);
			object value2 = base.Value.GetValue(context);
			if (value == null && value2 == null)
			{
				return true;
			}
			if (value == null || value2 == null)
			{
				return false;
			}
			if (!Argument.IsTypeEquatable(value2.GetType()) && !Argument.IsTypeEquatableCollection(value2.GetType()))
			{
				throw new CompliancePolicyValidationException(string.Format("Rule '{0}' contains an invalid property '{1}'", context.CurrentRule.Name, base.Property.Name));
			}
			if (value.GetType() == typeof(string) || Argument.IsTypeCollectionOfType(value.GetType(), typeof(string)))
			{
				return PolicyUtils.CompareStringValues(value, value2, CaseInsensitiveStringComparer.Instance);
			}
			if (value.GetType() == typeof(Guid) || Argument.IsTypeCollectionOfType(value.GetType(), typeof(Guid)))
			{
				return PolicyUtils.CompareGuidValues(value, value2);
			}
			return PolicyUtils.CompareValues(value, value2);
		}

		// Token: 0x04000334 RID: 820
		private static readonly Version minVersion = new Version("1.00.0002.000");
	}
}

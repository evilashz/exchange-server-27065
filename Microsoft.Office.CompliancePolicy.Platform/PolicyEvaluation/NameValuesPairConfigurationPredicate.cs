using System;
using System.Collections.Generic;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000D4 RID: 212
	public class NameValuesPairConfigurationPredicate : PredicateCondition
	{
		// Token: 0x06000565 RID: 1381 RVA: 0x000108A8 File Offset: 0x0000EAA8
		internal NameValuesPairConfigurationPredicate(Property property, List<string> values) : base(property, values)
		{
			if ((base.Property.Type != typeof(string) && !base.Property.IsCollectionOfType(typeof(string))) || (base.Value.Type != typeof(string) && !base.Value.IsCollectionOfType(typeof(string))))
			{
				throw new CompliancePolicyValidationException(string.Format("Predicate '{0}' requires string property and value", this.Name));
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x00010939 File Offset: 0x0000EB39
		public override string Name
		{
			get
			{
				return "NameValuesPairConfiguration";
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x00010940 File Offset: 0x0000EB40
		public override Version MinimumVersion
		{
			get
			{
				return new Version("1.00.0001.000");
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x0001094C File Offset: 0x0000EB4C
		public override ConditionType ConditionType
		{
			get
			{
				return ConditionType.Predicate;
			}
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0001094F File Offset: 0x0000EB4F
		public override bool Evaluate(PolicyEvaluationContext context)
		{
			throw new CompliancePolicyException("Evaluate is not support on NameValuesPairConfigurationPredicate!");
		}
	}
}

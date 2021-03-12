using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory.ValidationRules
{
	// Token: 0x02000A2D RID: 2605
	[Serializable]
	internal class RestrictedValidationRule : ValidationRule
	{
		// Token: 0x060077EC RID: 30700 RVA: 0x0018AF34 File Offset: 0x00189134
		public RestrictedValidationRule(ValidationRuleDefinition ruleDefinition, IList<CapabilityIdentifierEvaluator> restrictedCapabilityEvaluators, IList<CapabilityIdentifierEvaluator> overridingAllowCapabilityEvaluators, RoleEntry roleEntry) : base(ruleDefinition, restrictedCapabilityEvaluators, overridingAllowCapabilityEvaluators, roleEntry)
		{
		}

		// Token: 0x060077ED RID: 30701 RVA: 0x0018AF44 File Offset: 0x00189144
		protected override bool InternalTryValidate(ADRawEntry adObject, out RuleValidationException validationException)
		{
			validationException = null;
			if (!base.IsOverridingAllowCapabilityFound(adObject))
			{
				foreach (CapabilityIdentifierEvaluator capabilityIdentifierEvaluator in base.RestrictedCapabilityEvaluators)
				{
					switch (capabilityIdentifierEvaluator.Evaluate(adObject))
					{
					case CapabilityEvaluationResult.Yes:
						validationException = new RuleValidationException(base.GetValidationRuleErrorMessage(adObject, capabilityIdentifierEvaluator.Capability));
						return false;
					}
				}
				return true;
			}
			return true;
		}
	}
}

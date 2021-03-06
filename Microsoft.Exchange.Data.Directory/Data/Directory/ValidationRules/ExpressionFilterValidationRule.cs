using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics.Components.Authorization;

namespace Microsoft.Exchange.Data.Directory.ValidationRules
{
	// Token: 0x02000A2E RID: 2606
	[Serializable]
	internal class ExpressionFilterValidationRule : ValidationRule
	{
		// Token: 0x060077EE RID: 30702 RVA: 0x0018AFD4 File Offset: 0x001891D4
		public ExpressionFilterValidationRule(ValidationRuleDefinition ruleDefinition, IList<CapabilityIdentifierEvaluator> restrictedCapabilityEvaluators, IList<CapabilityIdentifierEvaluator> overridingAllowCapabilityEvaluators, RoleEntry roleEntry) : base(ruleDefinition, restrictedCapabilityEvaluators, overridingAllowCapabilityEvaluators, roleEntry)
		{
		}

		// Token: 0x060077EF RID: 30703 RVA: 0x0018B010 File Offset: 0x00189210
		protected override bool InternalTryValidate(ADRawEntry adObject, out RuleValidationException validationException)
		{
			validationException = null;
			CapabilityIdentifierEvaluator capabilityIdentifierEvaluator = base.RestrictedCapabilityEvaluators.FirstOrDefault((CapabilityIdentifierEvaluator x) => CapabilityEvaluationResult.Yes == x.Evaluate(adObject));
			ExTraceGlobals.AccessCheckTracer.TraceDebug<string, string>((long)this.GetHashCode(), "ExpressionFilterValidationRule.InternalTryValidate({0}). CurlpritCapability {1}.", adObject.GetDistinguishedNameOrName(), (capabilityIdentifierEvaluator != null) ? capabilityIdentifierEvaluator.Capability.ToString() : "<NULL>");
			if (capabilityIdentifierEvaluator == null)
			{
				return true;
			}
			foreach (ValidationRuleExpression validationRuleExpression in base.RuleDefinition.Expressions)
			{
				if (validationRuleExpression.ApplicableObjects != null && validationRuleExpression.ApplicableObjects.Count > 0)
				{
					Type right = validationRuleExpression.ApplicableObjects.FirstOrDefault((Type x) => x.IsAssignableFrom(adObject.GetType()));
					if (null == right)
					{
						ExTraceGlobals.AccessCheckTracer.TraceDebug<string, Type, string>((long)this.GetHashCode(), "ExpressionFilterValidationRule.InternalTryValidate({0}). Object type '{1}' is not on the list of applicable types for expression {2}.", adObject.GetDistinguishedNameOrName(), adObject.GetType(), validationRuleExpression.QueryString);
						continue;
					}
				}
				bool flag = true;
				foreach (PropertyDefinition propertyDefinition in validationRuleExpression.QueryFilter.FilterProperties())
				{
					if (!adObject.propertyBag.Contains((ProviderPropertyDefinition)propertyDefinition))
					{
						ExTraceGlobals.AccessCheckTracer.TraceDebug<string, string>((long)this.GetHashCode(), "ExpressionFilterValidationRule.InternalTryValidate({0}). Missing Property {1}.", adObject.GetDistinguishedNameOrName(), propertyDefinition.Name);
						flag = false;
						break;
					}
				}
				if (flag && !OpathFilterEvaluator.FilterMatches(validationRuleExpression.QueryFilter, adObject) && !base.IsOverridingAllowCapabilityFound(adObject))
				{
					validationException = new RuleValidationException(base.GetValidationRuleErrorMessage(adObject, capabilityIdentifierEvaluator.Capability));
					return false;
				}
			}
			return true;
		}
	}
}

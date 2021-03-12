using System;
using System.Collections.Generic;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ValidationRules;
using Microsoft.Exchange.Diagnostics.Components.Authorization;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x02000244 RID: 580
	internal static class ValidationRuleFactory
	{
		// Token: 0x06001485 RID: 5253 RVA: 0x0004C934 File Offset: 0x0004AB34
		internal static bool HasApplicableValidationRules(string cmdletFullName, ADRawEntry currentExecutingUser)
		{
			if (string.IsNullOrEmpty(cmdletFullName))
			{
				throw new ArgumentNullException("cmdletFullName");
			}
			if (currentExecutingUser == null)
			{
				throw new ArgumentNullException("currentExecutingUser");
			}
			return RBACValidationRulesList.Instance.HasApplicableValidationRules(cmdletFullName);
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x0004C962 File Offset: 0x0004AB62
		internal static IList<ValidationRule> GetApplicableValidationRules(string cmdletFullName, IList<string> parameters, ADRawEntry currentExecutingUser)
		{
			if (currentExecutingUser == null)
			{
				throw new ArgumentNullException("currentExecutingUser");
			}
			if (string.IsNullOrEmpty(cmdletFullName))
			{
				throw new ArgumentNullException("cmdletFullName");
			}
			return ValidationRuleFactory.GetApplicableValidationRules(cmdletFullName, parameters, ValidationRuleFactory.GetValidationRuleSkuForUser(currentExecutingUser));
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x0004C994 File Offset: 0x0004AB94
		internal static IList<ValidationRule> GetApplicableValidationRules(string cmdletFullName, IList<string> parameters, ValidationRuleSkus applicableSku)
		{
			IList<RoleEntryValidationRuleTuple> applicableRules = RBACValidationRulesList.Instance.GetApplicableRules(cmdletFullName, parameters, applicableSku);
			List<ValidationRule> list = new List<ValidationRule>(applicableRules.Count);
			foreach (RoleEntryValidationRuleTuple roleEntryValidationRuleTuple in applicableRules)
			{
				list.Add(ValidationRuleFactory.Create(roleEntryValidationRuleTuple.RuleDefinition, roleEntryValidationRuleTuple.MatchingRoleEntry));
			}
			return list;
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x0004CA08 File Offset: 0x0004AC08
		internal static List<ValidationRule> GetValidationRulesByFeature(string feature)
		{
			if (string.IsNullOrEmpty(feature))
			{
				throw new ArgumentNullException("feature");
			}
			IList<ValidationRuleDefinition> applicableRules = RBACValidationRulesList.Instance.GetApplicableRules(feature);
			List<ValidationRule> list = new List<ValidationRule>(applicableRules.Count);
			foreach (ValidationRuleDefinition definition in applicableRules)
			{
				list.Add(ValidationRuleFactory.Create(definition, null));
			}
			return list;
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x0004CA84 File Offset: 0x0004AC84
		private static ValidationRule Create(ValidationRuleDefinition definition, RoleEntry roleEntry)
		{
			ExTraceGlobals.PublicCreationAPITracer.TraceDebug<string, string>((long)definition.GetHashCode(), "Entering ValidationRuleFactory.GetValidationRule({0}) - Creating ValidationRule. Name: '{1}'", (null == roleEntry) ? "<NULL>" : roleEntry.ToString(), definition.Name);
			OrganizationValidationRuleDefinition organizationValidationRuleDefinition = definition as OrganizationValidationRuleDefinition;
			if (organizationValidationRuleDefinition != null)
			{
				return new OrganizationValidationRule(organizationValidationRuleDefinition, roleEntry);
			}
			IEnumerable<Capability> restrictedCapabilities = definition.RestrictedCapabilities;
			List<CapabilityIdentifierEvaluator> list = new List<CapabilityIdentifierEvaluator>();
			foreach (Capability capability in restrictedCapabilities)
			{
				list.Add(CapabilityIdentifierEvaluatorFactory.Create(capability));
			}
			IEnumerable<Capability> overridingAllowCapabilities = definition.OverridingAllowCapabilities;
			List<CapabilityIdentifierEvaluator> list2 = new List<CapabilityIdentifierEvaluator>();
			foreach (Capability capability2 in overridingAllowCapabilities)
			{
				list2.Add(CapabilityIdentifierEvaluatorFactory.Create(capability2));
			}
			if (definition.Expressions == null)
			{
				return new RestrictedValidationRule(definition, list, list2, roleEntry);
			}
			return new ExpressionFilterValidationRule(definition, list, list2, roleEntry);
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x0004CB98 File Offset: 0x0004AD98
		private static ValidationRuleSkus GetValidationRuleSkuForUser(ADRawEntry currentExecutingUser)
		{
			ValidationRuleSkus result = ValidationRuleSkus.None;
			bool flag = OrganizationId.ForestWideOrgId.Equals(currentExecutingUser[ADObjectSchema.OrganizationId]);
			switch (ValidationRuleFactory.ExchangeSku)
			{
			case Datacenter.ExchangeSku.Enterprise:
				if (flag)
				{
					result = ValidationRuleSkus.Enterprise;
				}
				break;
			case Datacenter.ExchangeSku.ExchangeDatacenter:
			case Datacenter.ExchangeSku.DatacenterDedicated:
				if (flag)
				{
					result = ValidationRuleSkus.Datacenter;
				}
				else
				{
					result = ValidationRuleSkus.DatacenterTenant;
				}
				break;
			case Datacenter.ExchangeSku.PartnerHosted:
				if (flag)
				{
					result = ValidationRuleSkus.Hosted;
				}
				else
				{
					result = ValidationRuleSkus.HostedTenant;
				}
				break;
			}
			return result;
		}

		// Token: 0x040005EC RID: 1516
		private static readonly Datacenter.ExchangeSku ExchangeSku = Datacenter.GetExchangeSku();
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.Authorization;

namespace Microsoft.Exchange.Data.Directory.ValidationRules
{
	// Token: 0x02000A2C RID: 2604
	[Serializable]
	internal abstract class ValidationRule
	{
		// Token: 0x060077DE RID: 30686 RVA: 0x0018ACC0 File Offset: 0x00188EC0
		public ValidationRule(ValidationRuleDefinition ruleDefinition, IList<CapabilityIdentifierEvaluator> restrictedCapabilityEvaluators, IList<CapabilityIdentifierEvaluator> overridingAllowCapabilityEvaluators, RoleEntry roleEntry)
		{
			if (ruleDefinition == null)
			{
				throw new ArgumentNullException("ruleDefinition");
			}
			if (restrictedCapabilityEvaluators == null)
			{
				throw new ArgumentNullException("restrictedCapabilityEvaluators");
			}
			if (overridingAllowCapabilityEvaluators == null)
			{
				throw new ArgumentNullException("overridingAllowCapabilityEvaluators");
			}
			this.RestrictedCapabilityEvaluators = restrictedCapabilityEvaluators;
			this.OverridingAllowCapabilityEvaluators = overridingAllowCapabilityEvaluators;
			this.ruleDefinition = ruleDefinition;
			this.roleEntry = roleEntry;
		}

		// Token: 0x17002ACA RID: 10954
		// (get) Token: 0x060077DF RID: 30687 RVA: 0x0018AD1A File Offset: 0x00188F1A
		public string Name
		{
			get
			{
				return this.ruleDefinition.Name;
			}
		}

		// Token: 0x17002ACB RID: 10955
		// (get) Token: 0x060077E0 RID: 30688 RVA: 0x0018AD27 File Offset: 0x00188F27
		public string Cmdlet
		{
			get
			{
				if (!(null != this.roleEntry))
				{
					return null;
				}
				return this.roleEntry.Name;
			}
		}

		// Token: 0x17002ACC RID: 10956
		// (get) Token: 0x060077E1 RID: 30689 RVA: 0x0018AD44 File Offset: 0x00188F44
		public ICollection<string> Parameters
		{
			get
			{
				if (!(null != this.roleEntry))
				{
					return null;
				}
				return this.roleEntry.Parameters;
			}
		}

		// Token: 0x17002ACD RID: 10957
		// (get) Token: 0x060077E2 RID: 30690 RVA: 0x0018AD61 File Offset: 0x00188F61
		// (set) Token: 0x060077E3 RID: 30691 RVA: 0x0018AD69 File Offset: 0x00188F69
		public IList<CapabilityIdentifierEvaluator> RestrictedCapabilityEvaluators { get; private set; }

		// Token: 0x17002ACE RID: 10958
		// (get) Token: 0x060077E4 RID: 30692 RVA: 0x0018AD72 File Offset: 0x00188F72
		// (set) Token: 0x060077E5 RID: 30693 RVA: 0x0018AD7A File Offset: 0x00188F7A
		public IList<CapabilityIdentifierEvaluator> OverridingAllowCapabilityEvaluators { get; private set; }

		// Token: 0x17002ACF RID: 10959
		// (get) Token: 0x060077E6 RID: 30694 RVA: 0x0018AD83 File Offset: 0x00188F83
		protected ValidationRuleDefinition RuleDefinition
		{
			get
			{
				return this.ruleDefinition;
			}
		}

		// Token: 0x060077E7 RID: 30695 RVA: 0x0018AD8C File Offset: 0x00188F8C
		public bool TryValidate(ADRawEntry adObject, out RuleValidationException validationException)
		{
			if (adObject == null)
			{
				throw new ArgumentNullException("adObject");
			}
			ExTraceGlobals.AccessCheckTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "Entering {0}.TryValidate({1}). Rule {2}.", base.GetType().Name, adObject.GetDistinguishedNameOrName(), this.ruleDefinition.Name);
			bool result = this.InternalTryValidate(adObject, out validationException);
			ExTraceGlobals.AccessCheckTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "{0}.TryValidate({1}). returns {2}", base.GetType().Name, adObject.GetDistinguishedNameOrName(), result.ToString());
			return result;
		}

		// Token: 0x060077E8 RID: 30696
		protected abstract bool InternalTryValidate(ADRawEntry adObject, out RuleValidationException validationException);

		// Token: 0x060077E9 RID: 30697 RVA: 0x0018AE11 File Offset: 0x00189011
		protected LocalizedString GetValidationRuleErrorMessage(ADRawEntry adObject, Capability culpritCapability)
		{
			return this.GetValidationRuleErrorMessage(adObject, culpritCapability.ToString());
		}

		// Token: 0x060077EA RID: 30698 RVA: 0x0018AE28 File Offset: 0x00189028
		protected LocalizedString GetValidationRuleErrorMessage(ADRawEntry adObject, string culpritCapabilityOrFilter)
		{
			if (null == this.roleEntry)
			{
				return LocalizedString.Empty;
			}
			return this.ruleDefinition.ErrorString((adObject.Id != null) ? adObject.Id.Name : ((string)adObject[ADObjectSchema.Name]), this.roleEntry.Name, string.Join(",", this.roleEntry.Parameters.ToArray<string>()), culpritCapabilityOrFilter);
		}

		// Token: 0x060077EB RID: 30699 RVA: 0x0018AEC0 File Offset: 0x001890C0
		protected bool IsOverridingAllowCapabilityFound(ADRawEntry adObject)
		{
			CapabilityIdentifierEvaluator capabilityIdentifierEvaluator = this.OverridingAllowCapabilityEvaluators.FirstOrDefault((CapabilityIdentifierEvaluator x) => x.Evaluate(adObject) == CapabilityEvaluationResult.Yes);
			ExTraceGlobals.AccessCheckTracer.TraceDebug<string, string>((long)this.GetHashCode(), "ValidationRule.IsOverridingAllowCapabilityFound({0}). OverridingAllowCapability: {1}.", adObject.GetDistinguishedNameOrName(), (capabilityIdentifierEvaluator != null) ? capabilityIdentifierEvaluator.Capability.ToString() : "<NULL>");
			return capabilityIdentifierEvaluator != null;
		}

		// Token: 0x04004CBF RID: 19647
		private ValidationRuleDefinition ruleDefinition;

		// Token: 0x04004CC0 RID: 19648
		private RoleEntry roleEntry;
	}
}

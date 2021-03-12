using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Authorization;

namespace Microsoft.Exchange.Data.Directory.ValidationRules
{
	// Token: 0x02000A25 RID: 2597
	internal class FederatedUserCapabilityIdentifierEvaluator : TenantScopedPropertyCapabilityEvaluator
	{
		// Token: 0x060077C3 RID: 30659 RVA: 0x00189F93 File Offset: 0x00188193
		public FederatedUserCapabilityIdentifierEvaluator(Capability capability) : base(capability)
		{
		}

		// Token: 0x060077C4 RID: 30660 RVA: 0x00189F9C File Offset: 0x0018819C
		public override CapabilityEvaluationResult Evaluate(ADRawEntry adObject)
		{
			if (adObject == null)
			{
				throw new ArgumentNullException("adObject");
			}
			ExTraceGlobals.AccessCheckTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Entering FederatedUserCapabilityIdentifierEvaluator.Evaluate('{0}') CapabilityToCheck '{1}'.", adObject.GetDistinguishedNameOrName(), base.Capability.ToString());
			CapabilityEvaluationResult capabilityEvaluationResult = CapabilityEvaluationResult.NotApplicable;
			ADUser aduser = adObject as ADUser;
			ReducedRecipient reducedRecipient = adObject as ReducedRecipient;
			if (aduser == null && reducedRecipient == null)
			{
				ExTraceGlobals.AccessCheckTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "FederatedUserCapabilityIdentifierEvaluator.Evaluate('{0}') return '{1}'. CapabilityToCheck '{2}' - adObject in not ADUser/ReducedRecipient.", adObject.GetDistinguishedNameOrName(), capabilityEvaluationResult.ToString(), base.Capability.ToString());
				return capabilityEvaluationResult;
			}
			SmtpAddress value = (aduser != null) ? aduser.WindowsLiveID : reducedRecipient.WindowsLiveID;
			if (value == SmtpAddress.Empty || value.Domain == null)
			{
				ExTraceGlobals.AccessCheckTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "FederatedUserCapabilityIdentifierEvaluator.Evaluate('{0}') return '{1}'. CapabilityToCheck '{2}' - user/recipient is not Live enabled.", adObject.GetDistinguishedNameOrName(), capabilityEvaluationResult.ToString(), base.Capability.ToString());
				return capabilityEvaluationResult;
			}
			OrganizationId organizationId = (aduser != null) ? aduser.OrganizationId : reducedRecipient.OrganizationId;
			if (organizationId == null || OrganizationId.ForestWideOrgId.Equals(organizationId))
			{
				ExTraceGlobals.AccessCheckTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "FederatedUserCapabilityIdentifierEvaluator.Evaluate('{0}') return '{1}'. CapabilityToCheck '{2}' - user/recipient does not belong to tenant scope.", adObject.GetDistinguishedNameOrName(), capabilityEvaluationResult.ToString(), base.Capability.ToString());
				return capabilityEvaluationResult;
			}
			IConfigurationSession tenantScopedSystemConfigurationSession = base.GetTenantScopedSystemConfigurationSession(organizationId);
			ExchangeConfigurationUnit exchangeConfigurationUnit = tenantScopedSystemConfigurationSession.Read<ExchangeConfigurationUnit>(organizationId.ConfigurationUnit);
			if (exchangeConfigurationUnit == null)
			{
				ExTraceGlobals.AccessCheckTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "FederatedUserCapabilityIdentifierEvaluator.Evaluate('{0}') return '{1}'. CapabilityToCheck '{2}' - user/recipient does is not in tenant scope.", adObject.GetDistinguishedNameOrName(), capabilityEvaluationResult.ToString(), base.Capability.ToString());
				return capabilityEvaluationResult;
			}
			capabilityEvaluationResult = (((exchangeConfigurationUnit.ObjectVersion < 13000) ? exchangeConfigurationUnit.IsFederated : FederatedUserCapabilityIdentifierEvaluator.IsNamespaceFederated(organizationId, value.Domain)) ? CapabilityEvaluationResult.Yes : CapabilityEvaluationResult.No);
			ExTraceGlobals.AccessCheckTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "FederatedUserCapabilityIdentifierEvaluator.Evaluate('{0}') return '{1}'. CapabilityToCheck '{2}'", adObject.GetDistinguishedNameOrName(), capabilityEvaluationResult.ToString(), base.Capability.ToString());
			return capabilityEvaluationResult;
		}

		// Token: 0x060077C5 RID: 30661 RVA: 0x0018A1B8 File Offset: 0x001883B8
		protected static bool IsNamespaceFederated(OrganizationId organizationId, string domain)
		{
			OrganizationIdCacheValue organizationIdCacheValue = OrganizationIdCache.Singleton.Get(organizationId);
			return organizationIdCacheValue.GetNamespaceAuthenticationType(domain) == AuthenticationType.Federated;
		}

		// Token: 0x060077C6 RID: 30662 RVA: 0x0018A1DB File Offset: 0x001883DB
		public override bool TryGetFilter(OrganizationId organizationId, out QueryFilter queryFilter, out LocalizedString errorMessage)
		{
			return ADRecipient.TryGetAuthenticationTypeFilterInternal(AuthenticationType.Federated, organizationId, out queryFilter, out errorMessage);
		}
	}
}

using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.Authorization;

namespace Microsoft.Exchange.Data.Directory.ValidationRules
{
	// Token: 0x02000A24 RID: 2596
	internal class ResourceMailboxCapabilityIdentifierEvaluator : CapabilityIdentifierEvaluator
	{
		// Token: 0x060077BF RID: 30655 RVA: 0x00189E4A File Offset: 0x0018804A
		public ResourceMailboxCapabilityIdentifierEvaluator(Capability capability) : base(capability)
		{
		}

		// Token: 0x060077C0 RID: 30656 RVA: 0x00189E54 File Offset: 0x00188054
		public override CapabilityEvaluationResult Evaluate(ADRawEntry adObject)
		{
			if (adObject == null)
			{
				throw new ArgumentNullException("adObject");
			}
			ExTraceGlobals.AccessCheckTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Entering ResourceMailboxCapabilityIdentifierEvaluator.Evaluate('{0}') CapabilityToCheck '{1}'.", adObject.GetDistinguishedNameOrName(), base.Capability.ToString());
			CapabilityEvaluationResult capabilityEvaluationResult = CapabilityEvaluationResult.NotApplicable;
			ADRecipient adrecipient = adObject as ADRecipient;
			if (!(adObject is ReducedRecipient) && adrecipient == null)
			{
				ExTraceGlobals.AccessCheckTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "ResourceMailboxCapabilityIdentifierEvaluator.Evaluate('{0}') return '{1}'. CapabilityToCheck '{2}' - adObject in not ReducedRecipient or ADUser.", adObject.GetDistinguishedNameOrName(), capabilityEvaluationResult.ToString(), base.Capability.ToString());
				return capabilityEvaluationResult;
			}
			capabilityEvaluationResult = ((adObject[ReducedRecipientSchema.ResourceType] != null) ? CapabilityEvaluationResult.Yes : CapabilityEvaluationResult.No);
			ExTraceGlobals.AccessCheckTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "ResourceMailboxCapabilityIdentifierEvaluator.Evaluate('{0}') return '{1}'. CapabilityToCheck '{2}'", adObject.GetDistinguishedNameOrName(), capabilityEvaluationResult.ToString(), base.Capability.ToString());
			return capabilityEvaluationResult;
		}

		// Token: 0x060077C1 RID: 30657 RVA: 0x00189F34 File Offset: 0x00188134
		public override bool TryGetFilter(OrganizationId organizationId, out QueryFilter queryFilter, out LocalizedString errorMessage)
		{
			queryFilter = ResourceMailboxCapabilityIdentifierEvaluator.filter;
			errorMessage = LocalizedString.Empty;
			return true;
		}

		// Token: 0x04004CB8 RID: 19640
		private static QueryFilter filter = new OrFilter(new QueryFilter[]
		{
			new ComparisonFilter(ComparisonOperator.Equal, ReducedRecipientSchema.ResourceType, ExchangeResourceType.Room),
			new ComparisonFilter(ComparisonOperator.Equal, ReducedRecipientSchema.ResourceType, ExchangeResourceType.Equipment)
		});
	}
}

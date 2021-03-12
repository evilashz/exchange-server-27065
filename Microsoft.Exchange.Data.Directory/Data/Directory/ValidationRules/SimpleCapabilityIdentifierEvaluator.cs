using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.Authorization;

namespace Microsoft.Exchange.Data.Directory.ValidationRules
{
	// Token: 0x02000A23 RID: 2595
	internal class SimpleCapabilityIdentifierEvaluator : CapabilityIdentifierEvaluator
	{
		// Token: 0x060077BC RID: 30652 RVA: 0x00189D2D File Offset: 0x00187F2D
		public SimpleCapabilityIdentifierEvaluator(Capability capability) : base(capability)
		{
			this.filter = new ComparisonFilter(ComparisonOperator.Equal, SharedPropertyDefinitions.RawCapabilities, base.Capability);
		}

		// Token: 0x060077BD RID: 30653 RVA: 0x00189D54 File Offset: 0x00187F54
		public override CapabilityEvaluationResult Evaluate(ADRawEntry adObject)
		{
			if (adObject == null)
			{
				throw new ArgumentNullException("adObject");
			}
			ExTraceGlobals.AccessCheckTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Entering SimpleCapabilityIdentifierEvaluator.Evaluate('{0}') CapabilityToCheck '{1}'.", adObject.GetDistinguishedNameOrName(), base.Capability.ToString());
			if (!adObject.propertyBag.Contains(SharedPropertyDefinitions.RawCapabilities))
			{
				ExTraceGlobals.AccessCheckTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "SimpleCapabilityIdentifierEvaluator.Evaluate('{0}') return '{1}'. CapabilityToCheck '{2}' - object doesn't have the Capabilities property.", adObject.GetDistinguishedNameOrName(), CapabilityEvaluationResult.NotApplicable.ToString(), base.Capability.ToString());
				return CapabilityEvaluationResult.NotApplicable;
			}
			CapabilityEvaluationResult capabilityEvaluationResult;
			if (OpathFilterEvaluator.FilterMatches(this.filter, adObject))
			{
				capabilityEvaluationResult = CapabilityEvaluationResult.Yes;
			}
			else
			{
				capabilityEvaluationResult = CapabilityEvaluationResult.No;
			}
			ExTraceGlobals.AccessCheckTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "SimpleCapabilityIdentifierEvaluator.Evaluate('{0}') return '{1}'. CapabilityToCheck '{2}'", adObject.GetDistinguishedNameOrName(), capabilityEvaluationResult.ToString(), base.Capability.ToString());
			return capabilityEvaluationResult;
		}

		// Token: 0x060077BE RID: 30654 RVA: 0x00189E34 File Offset: 0x00188034
		public override bool TryGetFilter(OrganizationId organizationId, out QueryFilter queryFilter, out LocalizedString errorMessage)
		{
			queryFilter = this.filter;
			errorMessage = LocalizedString.Empty;
			return true;
		}

		// Token: 0x04004CB7 RID: 19639
		private readonly QueryFilter filter;
	}
}

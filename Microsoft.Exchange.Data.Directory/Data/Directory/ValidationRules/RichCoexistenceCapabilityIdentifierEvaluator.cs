using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.Authorization;

namespace Microsoft.Exchange.Data.Directory.ValidationRules
{
	// Token: 0x02000A28 RID: 2600
	internal class RichCoexistenceCapabilityIdentifierEvaluator : MasteredOnPremiseCapabilityIdentifierEvaluator
	{
		// Token: 0x060077CF RID: 30671 RVA: 0x0018A763 File Offset: 0x00188963
		public RichCoexistenceCapabilityIdentifierEvaluator(Capability capability) : base(capability)
		{
		}

		// Token: 0x060077D0 RID: 30672 RVA: 0x0018A76C File Offset: 0x0018896C
		public override CapabilityEvaluationResult Evaluate(ADRawEntry adObject)
		{
			if (adObject == null)
			{
				throw new ArgumentNullException("adObject");
			}
			ExTraceGlobals.AccessCheckTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Entering RichCoexistenceCapabilityIdentifierEvaluator.Evaluate('{0}') CapabilityToCheck '{1}'.", adObject.GetDistinguishedNameOrName(), base.Capability.ToString());
			if (!adObject.propertyBag.Contains(IADMailStorageSchema.RemoteRecipientType))
			{
				ExTraceGlobals.AccessCheckTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "RichCoexistenceCapabilityIdentifierEvaluator.Evaluate('{0}') return '{1}'. CapabilityToCheck '{2}'. Object doesnt have 'RemoteRecipientType' property", adObject.GetDistinguishedNameOrName(), CapabilityEvaluationResult.NotApplicable.ToString(), base.Capability.ToString());
				return CapabilityEvaluationResult.NotApplicable;
			}
			if (OpathFilterEvaluator.FilterMatches(RichCoexistenceCapabilityIdentifierEvaluator.filter, adObject))
			{
				ExTraceGlobals.AccessCheckTracer.TraceDebug<string, string>((long)this.GetHashCode(), "RichCoexistenceCapabilityIdentifierEvaluator.Evaluate('{0}') adObject has RemoteRecipientType set. CapabilityToCheck '{1}'. ", adObject.GetDistinguishedNameOrName(), base.Capability.ToString());
				return base.Evaluate(adObject);
			}
			ExTraceGlobals.AccessCheckTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "RichCoexistenceCapabilityIdentifierEvaluator.Evaluate('{0}') return '{1}'. CapabilityToCheck '{2}'", adObject.GetDistinguishedNameOrName(), CapabilityEvaluationResult.No.ToString(), base.Capability.ToString());
			return CapabilityEvaluationResult.No;
		}

		// Token: 0x060077D1 RID: 30673 RVA: 0x0018A878 File Offset: 0x00188A78
		public override bool TryGetFilter(OrganizationId organizationId, out QueryFilter queryFilter, out LocalizedString errorMessage)
		{
			errorMessage = LocalizedString.Empty;
			if (base.TryGetFilter(organizationId, out queryFilter, out errorMessage))
			{
				queryFilter = QueryFilter.AndTogether(new QueryFilter[]
				{
					RichCoexistenceCapabilityIdentifierEvaluator.filter,
					queryFilter
				});
				return true;
			}
			return false;
		}

		// Token: 0x04004CBB RID: 19643
		private static readonly QueryFilter filter = new ComparisonFilter(ComparisonOperator.NotEqual, IADMailStorageSchema.RemoteRecipientType, RemoteRecipientType.None);
	}
}

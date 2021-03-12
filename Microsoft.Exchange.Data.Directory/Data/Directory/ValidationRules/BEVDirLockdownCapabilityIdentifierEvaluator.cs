using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Authorization;

namespace Microsoft.Exchange.Data.Directory.ValidationRules
{
	// Token: 0x02000A29 RID: 2601
	internal class BEVDirLockdownCapabilityIdentifierEvaluator : CapabilityIdentifierEvaluator
	{
		// Token: 0x060077D3 RID: 30675 RVA: 0x0018A8D3 File Offset: 0x00188AD3
		public BEVDirLockdownCapabilityIdentifierEvaluator(Capability capability) : base(capability)
		{
		}

		// Token: 0x060077D4 RID: 30676 RVA: 0x0018A8DC File Offset: 0x00188ADC
		public override CapabilityEvaluationResult Evaluate(ADRawEntry adObject)
		{
			if (adObject == null)
			{
				throw new ArgumentNullException("adObject");
			}
			ExchangeVirtualDirectory exchangeVirtualDirectory = adObject as ExchangeVirtualDirectory;
			if (exchangeVirtualDirectory == null)
			{
				ExTraceGlobals.AccessCheckTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "BEVDirLockdownCapabilityIdentifierEvaluator.Evaluate('{0}') return '{1}'. CapabilityToCheck '{2}'. Object isn't a ExchangeVirtualDirectory object.", adObject.GetDistinguishedNameOrName(), CapabilityEvaluationResult.NotApplicable.ToString(), base.Capability.ToString());
				return CapabilityEvaluationResult.NotApplicable;
			}
			if (exchangeVirtualDirectory.Name.Contains("Exchange Back End"))
			{
				return CapabilityEvaluationResult.Yes;
			}
			return CapabilityEvaluationResult.No;
		}

		// Token: 0x060077D5 RID: 30677 RVA: 0x0018A94F File Offset: 0x00188B4F
		public override bool TryGetFilter(OrganizationId organizationId, out QueryFilter queryFilter, out LocalizedString errorMessage)
		{
			errorMessage = LocalizedString.Empty;
			queryFilter = BEVDirLockdownCapabilityIdentifierEvaluator.filter;
			return false;
		}

		// Token: 0x04004CBC RID: 19644
		private static readonly QueryFilter filter;
	}
}

using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory.ValidationRules
{
	// Token: 0x02000A21 RID: 2593
	internal abstract class CapabilityIdentifierEvaluator
	{
		// Token: 0x060077B5 RID: 30645 RVA: 0x00189CCC File Offset: 0x00187ECC
		protected CapabilityIdentifierEvaluator(Capability capability)
		{
			this.Capability = capability;
		}

		// Token: 0x17002AC7 RID: 10951
		// (get) Token: 0x060077B6 RID: 30646 RVA: 0x00189CDB File Offset: 0x00187EDB
		// (set) Token: 0x060077B7 RID: 30647 RVA: 0x00189CE3 File Offset: 0x00187EE3
		public Capability Capability { get; private set; }

		// Token: 0x060077B8 RID: 30648
		public abstract CapabilityEvaluationResult Evaluate(ADRawEntry adObject);

		// Token: 0x060077B9 RID: 30649
		public abstract bool TryGetFilter(OrganizationId organizationId, out QueryFilter queryFilter, out LocalizedString errorMessage);
	}
}

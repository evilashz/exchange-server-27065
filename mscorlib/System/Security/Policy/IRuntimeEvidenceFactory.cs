using System;
using System.Collections.Generic;

namespace System.Security.Policy
{
	// Token: 0x0200032D RID: 813
	internal interface IRuntimeEvidenceFactory
	{
		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06002956 RID: 10582
		IEvidenceFactory Target { get; }

		// Token: 0x06002957 RID: 10583
		IEnumerable<EvidenceBase> GetFactorySuppliedEvidence();

		// Token: 0x06002958 RID: 10584
		EvidenceBase GenerateEvidence(Type evidenceType);
	}
}

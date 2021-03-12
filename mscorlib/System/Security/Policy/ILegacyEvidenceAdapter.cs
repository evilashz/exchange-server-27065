using System;

namespace System.Security.Policy
{
	// Token: 0x02000322 RID: 802
	internal interface ILegacyEvidenceAdapter
	{
		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x0600291C RID: 10524
		object EvidenceObject { get; }

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x0600291D RID: 10525
		Type EvidenceType { get; }
	}
}

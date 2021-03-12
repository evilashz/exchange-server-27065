using System;
using System.Collections;

namespace System
{
	// Token: 0x0200007A RID: 122
	internal interface IWellKnownStringEqualityComparer
	{
		// Token: 0x060005A9 RID: 1449
		IEqualityComparer GetRandomizedEqualityComparer();

		// Token: 0x060005AA RID: 1450
		IEqualityComparer GetEqualityComparerForSerialization();
	}
}

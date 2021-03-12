using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200032D RID: 813
	internal interface ICostFactory
	{
		// Token: 0x06002355 RID: 9045
		CostIndex CreateIndex(Cost[] costIndices, int maxConditionsPerBucket, GetCost getCost, IsBelowCost isBelowCost, ShouldAddToIndex shouldAddToIndex, Trace tracer);

		// Token: 0x06002356 RID: 9046
		CostMap CreateMap(CostConfiguration costConfig, IsLocked isLocked, IsLockedOnQueue isLockedOnQueue, Trace tracer);
	}
}

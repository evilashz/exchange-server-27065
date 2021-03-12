using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200032E RID: 814
	internal class CostFactory : ICostFactory
	{
		// Token: 0x06002357 RID: 9047 RVA: 0x000863BA File Offset: 0x000845BA
		public CostIndex CreateIndex(Cost[] costIndices, int maxConditionsPerBucket, GetCost getCost, IsBelowCost isBelowCost, ShouldAddToIndex shouldAddToIndex, Trace tracer)
		{
			return new CostIndex(costIndices, maxConditionsPerBucket, getCost, isBelowCost, shouldAddToIndex, tracer);
		}

		// Token: 0x06002358 RID: 9048 RVA: 0x000863CA File Offset: 0x000845CA
		public CostMap CreateMap(CostConfiguration costConfig, IsLocked isLocked, IsLockedOnQueue isLockedOnQueue, Trace tracer)
		{
			return new CostMap(costConfig, this, isLocked, isLockedOnQueue, tracer);
		}
	}
}

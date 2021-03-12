using System;

namespace Microsoft.Exchange.Hygiene.Cache.Data
{
	// Token: 0x0200005F RID: 95
	internal interface ICachePrimingInfo
	{
		// Token: 0x060003B6 RID: 950
		CachePrimingState GetCurrentPrimingState(Type cacheObjectType);

		// Token: 0x060003B7 RID: 951
		CacheFailoverMode GetCurrentFailoverMode(Type cacheObjectType, CacheFailoverMode requestedFailoverMode, CachePrimingState primingState = CachePrimingState.Unknown);
	}
}

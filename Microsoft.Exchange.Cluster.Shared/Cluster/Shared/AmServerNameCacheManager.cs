using System;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x02000009 RID: 9
	internal class AmServerNameCacheManager : TimerComponent
	{
		// Token: 0x0600004F RID: 79 RVA: 0x00002E3F File Offset: 0x0000103F
		public AmServerNameCacheManager() : base(new TimeSpan(0, 0, 0), TimeSpan.FromSeconds((double)RegistryParameters.AmServerNameCacheTTLInSec), "AmServerNameCacheManager")
		{
			AmServerNameCache.Instance.Enable();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002E69 File Offset: 0x00001069
		protected override void TimerCallbackInternal()
		{
			AmServerNameCache.Instance.PopulateForDag();
		}
	}
}

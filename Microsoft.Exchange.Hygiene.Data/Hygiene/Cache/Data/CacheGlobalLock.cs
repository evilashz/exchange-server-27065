using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Hygiene.Data;

namespace Microsoft.Exchange.Hygiene.Cache.Data
{
	// Token: 0x0200004F RID: 79
	[Serializable]
	internal class CacheGlobalLock : ConfigurablePropertyBag
	{
		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600031E RID: 798 RVA: 0x000098AE File Offset: 0x00007AAE
		public static string CacheIdentity
		{
			get
			{
				return "CacheGlobalLock";
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600031F RID: 799 RVA: 0x000098B5 File Offset: 0x00007AB5
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(CacheGlobalLock.CacheIdentity);
			}
		}
	}
}

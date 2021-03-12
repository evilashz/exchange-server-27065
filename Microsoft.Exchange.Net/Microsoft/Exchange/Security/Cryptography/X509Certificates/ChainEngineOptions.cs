using System;

namespace Microsoft.Exchange.Security.Cryptography.X509Certificates
{
	// Token: 0x02000A8F RID: 2703
	[Flags]
	internal enum ChainEngineOptions : uint
	{
		// Token: 0x0400328E RID: 12942
		CacheEndCert = 1U,
		// Token: 0x0400328F RID: 12943
		ThreadStoreSync = 2U,
		// Token: 0x04003290 RID: 12944
		CacheOnlyUrlRetrieval = 4U,
		// Token: 0x04003291 RID: 12945
		UseLocalMachineStore = 8U,
		// Token: 0x04003292 RID: 12946
		EnableCacheAutoUpdate = 16U,
		// Token: 0x04003293 RID: 12947
		EnableShareStore = 32U
	}
}

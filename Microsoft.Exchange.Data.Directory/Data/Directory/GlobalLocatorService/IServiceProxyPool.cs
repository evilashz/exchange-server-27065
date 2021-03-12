using System;

namespace Microsoft.Exchange.Data.Directory.GlobalLocatorService
{
	// Token: 0x0200012F RID: 303
	internal interface IServiceProxyPool<T>
	{
		// Token: 0x06000C9E RID: 3230
		T Acquire();

		// Token: 0x06000C9F RID: 3231
		void Release(T serviceProxy);
	}
}

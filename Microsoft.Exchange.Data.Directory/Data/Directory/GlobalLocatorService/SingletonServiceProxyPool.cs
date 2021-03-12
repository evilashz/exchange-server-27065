using System;

namespace Microsoft.Exchange.Data.Directory.GlobalLocatorService
{
	// Token: 0x02000131 RID: 305
	internal class SingletonServiceProxyPool<T> : IServiceProxyPool<T>
	{
		// Token: 0x06000CA4 RID: 3236 RVA: 0x00038BF3 File Offset: 0x00036DF3
		internal SingletonServiceProxyPool(T serviceProxy)
		{
			this.serviceProxy = serviceProxy;
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x00038C02 File Offset: 0x00036E02
		public T Acquire()
		{
			return this.serviceProxy;
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x00038C0A File Offset: 0x00036E0A
		public void Release(T serviceProxy)
		{
		}

		// Token: 0x04000687 RID: 1671
		private T serviceProxy;
	}
}

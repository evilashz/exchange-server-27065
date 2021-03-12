using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200011E RID: 286
	public interface ICmdletProxyable
	{
		// Token: 0x06000A03 RID: 2563
		object GetProxyInfo();

		// Token: 0x06000A04 RID: 2564
		void SetProxyInfo(object proxyInfo);
	}
}

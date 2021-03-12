using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000C3 RID: 195
	[Serializable]
	public abstract class MeumProxyAddress : ProxyAddress
	{
		// Token: 0x06000506 RID: 1286 RVA: 0x00011A20 File Offset: 0x0000FC20
		public MeumProxyAddress(string address, bool primaryAddress) : base(ProxyAddressPrefix.Meum, address, primaryAddress)
		{
		}
	}
}

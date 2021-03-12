using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200025A RID: 602
	public sealed class CustomNetworkProtocol : NetworkProtocol
	{
		// Token: 0x0600144C RID: 5196 RVA: 0x0003FE5A File Offset: 0x0003E05A
		public CustomNetworkProtocol(string protocolName, string displayName) : base(protocolName, displayName)
		{
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x0003FE64 File Offset: 0x0003E064
		public CustomNetworkProtocol(string protocolName) : base(protocolName, protocolName)
		{
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x0003FE6E File Offset: 0x0003E06E
		public override NetworkAddress GetNetworkAddress(string address)
		{
			if (string.IsNullOrEmpty(address))
			{
				throw new ArgumentNullException("address");
			}
			return new CustomNetworkAddress(this, address);
		}
	}
}

using System;
using System.Net;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x0200027C RID: 636
	internal class IPAddressPortPair
	{
		// Token: 0x06001B81 RID: 7041 RVA: 0x00070C08 File Offset: 0x0006EE08
		public IPAddressPortPair(IPAddress address, ushort port)
		{
			this.address = address;
			this.port = port;
		}

		// Token: 0x06001B82 RID: 7042 RVA: 0x00070C20 File Offset: 0x0006EE20
		public override bool Equals(object obj)
		{
			IPAddressPortPair ipaddressPortPair = obj as IPAddressPortPair;
			return ipaddressPortPair != null && this.address.Equals(ipaddressPortPair.address) && this.port == ipaddressPortPair.port;
		}

		// Token: 0x06001B83 RID: 7043 RVA: 0x00070C5C File Offset: 0x0006EE5C
		public override int GetHashCode()
		{
			return this.address.GetHashCode() ^ (int)this.port;
		}

		// Token: 0x06001B84 RID: 7044 RVA: 0x00070C70 File Offset: 0x0006EE70
		public override string ToString()
		{
			return string.Format("{0}:{1}", this.address, this.port.ToString());
		}

		// Token: 0x04000CF9 RID: 3321
		private readonly IPAddress address;

		// Token: 0x04000CFA RID: 3322
		private readonly ushort port;
	}
}

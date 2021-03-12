using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000C7 RID: 199
	[Serializable]
	internal sealed class MeumProxyAddressPrefix : ProxyAddressPrefix
	{
		// Token: 0x06000512 RID: 1298 RVA: 0x00011CCD File Offset: 0x0000FECD
		internal MeumProxyAddressPrefix() : base("MEUM")
		{
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00011CDA File Offset: 0x0000FEDA
		public override ProxyAddress GetProxyAddress(string address, bool primaryAddress)
		{
			return MeumProxyAddressFactory.CreateFromAddressString(address, primaryAddress);
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00011CE3 File Offset: 0x0000FEE3
		public override ProxyAddressTemplate GetProxyAddressTemplate(string addressTemplate, bool primaryAddress)
		{
			throw new NotSupportedException("Mserve UM proxy address templates are not supported");
		}
	}
}

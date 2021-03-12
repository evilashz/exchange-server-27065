using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000CF RID: 207
	[Serializable]
	internal sealed class X400ProxyAddressPrefix : ProxyAddressPrefix
	{
		// Token: 0x0600055E RID: 1374 RVA: 0x00012E75 File Offset: 0x00011075
		public X400ProxyAddressPrefix() : base("X400")
		{
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00012E82 File Offset: 0x00011082
		public override ProxyAddress GetProxyAddress(string address, bool isPrimaryAddress)
		{
			return new X400ProxyAddress(address, isPrimaryAddress);
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00012E8B File Offset: 0x0001108B
		public override ProxyAddressTemplate GetProxyAddressTemplate(string addressTemplate, bool isPrimaryAddress)
		{
			return new X400ProxyAddressTemplate(addressTemplate, isPrimaryAddress);
		}
	}
}

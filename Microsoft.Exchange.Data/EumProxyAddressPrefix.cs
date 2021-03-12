using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000BE RID: 190
	[Serializable]
	internal sealed class EumProxyAddressPrefix : ProxyAddressPrefix
	{
		// Token: 0x060004F6 RID: 1270 RVA: 0x0001184A File Offset: 0x0000FA4A
		internal EumProxyAddressPrefix() : base("EUM")
		{
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00011857 File Offset: 0x0000FA57
		public override ProxyAddress GetProxyAddress(string address, bool isPrimaryAddress)
		{
			if (address.IndexOf("phone-context=") == -1)
			{
				return new InvalidProxyAddress(null, ProxyAddressPrefix.UM, address, isPrimaryAddress, new ArgumentOutOfRangeException(DataStrings.ExceptionInvalidEumAddress(address), null));
			}
			return new EumProxyAddress(address, isPrimaryAddress);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0001188D File Offset: 0x0000FA8D
		public override ProxyAddressTemplate GetProxyAddressTemplate(string addressTemplate, bool isPrimaryAddress)
		{
			return new EumProxyAddressTemplate(addressTemplate, isPrimaryAddress);
		}
	}
}

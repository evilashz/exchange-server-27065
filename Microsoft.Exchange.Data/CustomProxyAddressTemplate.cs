using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000BC RID: 188
	[Serializable]
	public sealed class CustomProxyAddressTemplate : ProxyAddressTemplate
	{
		// Token: 0x060004F1 RID: 1265 RVA: 0x000117D3 File Offset: 0x0000F9D3
		public CustomProxyAddressTemplate(CustomProxyAddressPrefix prefix, string addressTemplate, bool isPrimaryAddress) : base(prefix, addressTemplate, isPrimaryAddress)
		{
		}
	}
}

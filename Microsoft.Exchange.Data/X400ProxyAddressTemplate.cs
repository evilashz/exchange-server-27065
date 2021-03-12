using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000D0 RID: 208
	[Serializable]
	public sealed class X400ProxyAddressTemplate : ProxyAddressTemplate
	{
		// Token: 0x06000561 RID: 1377 RVA: 0x00012E94 File Offset: 0x00011094
		public X400ProxyAddressTemplate(string addressTemplate, bool isPrimaryAddress) : base(ProxyAddressPrefix.X400, addressTemplate, isPrimaryAddress)
		{
		}
	}
}

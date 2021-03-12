using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000BA RID: 186
	[Serializable]
	public sealed class CustomProxyAddressPrefix : ProxyAddressPrefix
	{
		// Token: 0x060004DF RID: 1247 RVA: 0x000115BD File Offset: 0x0000F7BD
		public CustomProxyAddressPrefix(string prefix) : base(prefix)
		{
			this.displayName = base.PrimaryPrefix;
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x000115D2 File Offset: 0x0000F7D2
		public CustomProxyAddressPrefix(string prefix, string displayName) : base(prefix)
		{
			this.displayName = displayName;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x000115E2 File Offset: 0x0000F7E2
		public override ProxyAddress GetProxyAddress(string address, bool isPrimaryAddress)
		{
			return new CustomProxyAddress(this, address, isPrimaryAddress);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x000115EC File Offset: 0x0000F7EC
		public override ProxyAddressTemplate GetProxyAddressTemplate(string addressTemplate, bool isPrimaryAddress)
		{
			return new CustomProxyAddressTemplate(this, addressTemplate, isPrimaryAddress);
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x000115F6 File Offset: 0x0000F7F6
		public override string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		// Token: 0x04000302 RID: 770
		private readonly string displayName;
	}
}

using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000157 RID: 343
	public class ProxyAddressTemplateDataHandler : ProxyAddressBaseDataHandler
	{
		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000E02 RID: 3586 RVA: 0x000353F6 File Offset: 0x000335F6
		// (set) Token: 0x06000E03 RID: 3587 RVA: 0x000353FE File Offset: 0x000335FE
		public override ProxyAddressBase ProxyAddressBase
		{
			get
			{
				return base.ProxyAddressBase;
			}
			set
			{
				base.ProxyAddressBase = value;
				if (value != null)
				{
					base.Prefix = value.PrefixString;
					base.Address = ((ProxyAddressTemplate)value).AddressTemplateString;
				}
			}
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x0003542D File Offset: 0x0003362D
		protected override ProxyAddressBase InternalGetProxyAddressBase(string prefix, string address)
		{
			return ProxyAddressTemplate.Parse(prefix, address);
		}
	}
}

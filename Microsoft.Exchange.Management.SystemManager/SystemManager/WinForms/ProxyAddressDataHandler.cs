using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000154 RID: 340
	public class ProxyAddressDataHandler : ProxyAddressBaseDataHandler
	{
		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x00035248 File Offset: 0x00033448
		// (set) Token: 0x06000DF3 RID: 3571 RVA: 0x00035250 File Offset: 0x00033450
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
					base.Address = ((ProxyAddress)value).AddressString;
				}
			}
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x0003527F File Offset: 0x0003347F
		protected override ProxyAddressBase InternalGetProxyAddressBase(string prefix, string address)
		{
			return ProxyAddress.Parse(prefix, address);
		}
	}
}

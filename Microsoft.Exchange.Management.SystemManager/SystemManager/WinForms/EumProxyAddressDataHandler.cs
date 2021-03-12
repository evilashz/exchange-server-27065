using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000156 RID: 342
	public class EumProxyAddressDataHandler : ProxyAddressDataHandler
	{
		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x0003531D File Offset: 0x0003351D
		// (set) Token: 0x06000DF9 RID: 3577 RVA: 0x00035328 File Offset: 0x00033528
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
					string valueString = value.ValueString;
					EumAddress eumAddress = EumAddress.Parse(valueString);
					this.extension = eumAddress.Extension;
					this.phoneContext = eumAddress.PhoneContext;
				}
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000DFA RID: 3578 RVA: 0x0003536D File Offset: 0x0003356D
		// (set) Token: 0x06000DFB RID: 3579 RVA: 0x00035375 File Offset: 0x00033575
		public string Extension
		{
			get
			{
				return this.extension;
			}
			set
			{
				if (this.extension != value)
				{
					this.extension = value;
					this.UpdateAddress(this.extension, this.PhoneContext);
				}
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000DFC RID: 3580 RVA: 0x0003539E File Offset: 0x0003359E
		// (set) Token: 0x06000DFD RID: 3581 RVA: 0x000353A6 File Offset: 0x000335A6
		public string PhoneContext
		{
			get
			{
				return this.phoneContext;
			}
			set
			{
				if (this.phoneContext != value)
				{
					this.phoneContext = value;
					this.UpdateAddress(this.Extension, this.phoneContext);
				}
			}
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x000353CF File Offset: 0x000335CF
		private void UpdateAddress(string extension, string phoneContext)
		{
			base.Address = EumAddress.BuildAddressString(extension, phoneContext);
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x000353DE File Offset: 0x000335DE
		protected override ProxyAddressBase InternalGetProxyAddressBase(string prefix, string address)
		{
			return ProxyAddress.Parse(prefix, address);
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000E00 RID: 3584 RVA: 0x000353E7 File Offset: 0x000335E7
		protected override string BindingProperty
		{
			get
			{
				return "Extension";
			}
		}

		// Token: 0x04000593 RID: 1427
		private string extension;

		// Token: 0x04000594 RID: 1428
		private string phoneContext;
	}
}

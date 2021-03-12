using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005BF RID: 1471
	[Serializable]
	internal class ServiceStatusPropertyBag : PropertyBag
	{
		// Token: 0x0600339B RID: 13211 RVA: 0x000D1828 File Offset: 0x000CFA28
		public ServiceStatusPropertyBag(bool readOnly, int initialSize) : base(readOnly, initialSize)
		{
			base.SetField(this.ObjectVersionPropertyDefinition, ExchangeObjectVersion.Exchange2010);
		}

		// Token: 0x0600339C RID: 13212 RVA: 0x000D1844 File Offset: 0x000CFA44
		public ServiceStatusPropertyBag() : base(false, 16)
		{
			base.SetField(this.ObjectVersionPropertyDefinition, ExchangeObjectVersion.Exchange2010);
		}

		// Token: 0x17000F67 RID: 3943
		// (get) Token: 0x0600339D RID: 13213 RVA: 0x000D1861 File Offset: 0x000CFA61
		internal override ProviderPropertyDefinition ObjectVersionPropertyDefinition
		{
			get
			{
				return ServiceStatusSchema.ExchangeVersion;
			}
		}

		// Token: 0x17000F68 RID: 3944
		// (get) Token: 0x0600339E RID: 13214 RVA: 0x000D1868 File Offset: 0x000CFA68
		internal override ProviderPropertyDefinition ObjectStatePropertyDefinition
		{
			get
			{
				return ServiceStatusSchema.ObjectState;
			}
		}

		// Token: 0x17000F69 RID: 3945
		// (get) Token: 0x0600339F RID: 13215 RVA: 0x000D186F File Offset: 0x000CFA6F
		internal override ProviderPropertyDefinition ObjectIdentityPropertyDefinition
		{
			get
			{
				return ServiceStatusSchema.Identity;
			}
		}
	}
}

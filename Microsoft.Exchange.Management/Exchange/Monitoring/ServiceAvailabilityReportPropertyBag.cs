using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005BC RID: 1468
	[Serializable]
	internal class ServiceAvailabilityReportPropertyBag : PropertyBag
	{
		// Token: 0x06003382 RID: 13186 RVA: 0x000D1520 File Offset: 0x000CF720
		public ServiceAvailabilityReportPropertyBag(bool readOnly, int initialSize) : base(readOnly, initialSize)
		{
			base.SetField(this.ObjectVersionPropertyDefinition, ExchangeObjectVersion.Exchange2010);
		}

		// Token: 0x06003383 RID: 13187 RVA: 0x000D153C File Offset: 0x000CF73C
		public ServiceAvailabilityReportPropertyBag() : base(false, 16)
		{
			base.SetField(this.ObjectVersionPropertyDefinition, ExchangeObjectVersion.Exchange2010);
		}

		// Token: 0x17000F59 RID: 3929
		// (get) Token: 0x06003384 RID: 13188 RVA: 0x000D1559 File Offset: 0x000CF759
		internal override ProviderPropertyDefinition ObjectVersionPropertyDefinition
		{
			get
			{
				return ServiceAvailabilityReportSchema.ExchangeVersion;
			}
		}

		// Token: 0x17000F5A RID: 3930
		// (get) Token: 0x06003385 RID: 13189 RVA: 0x000D1560 File Offset: 0x000CF760
		internal override ProviderPropertyDefinition ObjectStatePropertyDefinition
		{
			get
			{
				return ServiceAvailabilityReportSchema.ObjectState;
			}
		}

		// Token: 0x17000F5B RID: 3931
		// (get) Token: 0x06003386 RID: 13190 RVA: 0x000D1567 File Offset: 0x000CF767
		internal override ProviderPropertyDefinition ObjectIdentityPropertyDefinition
		{
			get
			{
				return ServiceAvailabilityReportSchema.Identity;
			}
		}
	}
}

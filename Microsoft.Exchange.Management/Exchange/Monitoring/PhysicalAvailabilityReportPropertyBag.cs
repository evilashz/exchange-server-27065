using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005B4 RID: 1460
	[Serializable]
	internal class PhysicalAvailabilityReportPropertyBag : PropertyBag
	{
		// Token: 0x06003334 RID: 13108 RVA: 0x000D0782 File Offset: 0x000CE982
		public PhysicalAvailabilityReportPropertyBag(bool readOnly, int initialSize) : base(readOnly, initialSize)
		{
			base.SetField(this.ObjectVersionPropertyDefinition, ExchangeObjectVersion.Exchange2010);
		}

		// Token: 0x06003335 RID: 13109 RVA: 0x000D079E File Offset: 0x000CE99E
		public PhysicalAvailabilityReportPropertyBag() : base(false, 16)
		{
			base.SetField(this.ObjectVersionPropertyDefinition, ExchangeObjectVersion.Exchange2010);
		}

		// Token: 0x17000F34 RID: 3892
		// (get) Token: 0x06003336 RID: 13110 RVA: 0x000D07BB File Offset: 0x000CE9BB
		internal override ProviderPropertyDefinition ObjectVersionPropertyDefinition
		{
			get
			{
				return PhysicalAvailabilityReportSchema.ExchangeVersion;
			}
		}

		// Token: 0x17000F35 RID: 3893
		// (get) Token: 0x06003337 RID: 13111 RVA: 0x000D07C2 File Offset: 0x000CE9C2
		internal override ProviderPropertyDefinition ObjectStatePropertyDefinition
		{
			get
			{
				return PhysicalAvailabilityReportSchema.ObjectState;
			}
		}

		// Token: 0x17000F36 RID: 3894
		// (get) Token: 0x06003338 RID: 13112 RVA: 0x000D07C9 File Offset: 0x000CE9C9
		internal override ProviderPropertyDefinition ObjectIdentityPropertyDefinition
		{
			get
			{
				return PhysicalAvailabilityReportSchema.Identity;
			}
		}
	}
}

using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005B0 RID: 1456
	[Serializable]
	internal class PerimeterQueuePropertyBag : PropertyBag
	{
		// Token: 0x06003320 RID: 13088 RVA: 0x000D0539 File Offset: 0x000CE739
		public PerimeterQueuePropertyBag(bool readOnly, int initialSize) : base(readOnly, initialSize)
		{
			base.SetField(this.ObjectVersionPropertyDefinition, ExchangeObjectVersion.Exchange2010);
		}

		// Token: 0x06003321 RID: 13089 RVA: 0x000D0555 File Offset: 0x000CE755
		public PerimeterQueuePropertyBag() : base(false, 16)
		{
			base.SetField(this.ObjectVersionPropertyDefinition, ExchangeObjectVersion.Exchange2010);
		}

		// Token: 0x17000F2C RID: 3884
		// (get) Token: 0x06003322 RID: 13090 RVA: 0x000D0572 File Offset: 0x000CE772
		internal override ProviderPropertyDefinition ObjectVersionPropertyDefinition
		{
			get
			{
				return PerimeterQueueStatusSchema.ExchangeVersion;
			}
		}

		// Token: 0x17000F2D RID: 3885
		// (get) Token: 0x06003323 RID: 13091 RVA: 0x000D0579 File Offset: 0x000CE779
		internal override ProviderPropertyDefinition ObjectStatePropertyDefinition
		{
			get
			{
				return PerimeterQueueStatusSchema.ObjectState;
			}
		}

		// Token: 0x17000F2E RID: 3886
		// (get) Token: 0x06003324 RID: 13092 RVA: 0x000D0580 File Offset: 0x000CE780
		internal override ProviderPropertyDefinition ObjectIdentityPropertyDefinition
		{
			get
			{
				return PerimeterQueueStatusSchema.Identity;
			}
		}
	}
}

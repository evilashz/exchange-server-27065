using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005B7 RID: 1463
	[Serializable]
	internal class RecipientStatisticsReportPropertyBag : PropertyBag
	{
		// Token: 0x06003353 RID: 13139 RVA: 0x000D0BB8 File Offset: 0x000CEDB8
		public RecipientStatisticsReportPropertyBag(bool readOnly, int initialSize) : base(readOnly, initialSize)
		{
			base.SetField(this.ObjectVersionPropertyDefinition, ExchangeObjectVersion.Exchange2010);
		}

		// Token: 0x06003354 RID: 13140 RVA: 0x000D0BD4 File Offset: 0x000CEDD4
		public RecipientStatisticsReportPropertyBag() : base(false, 16)
		{
			base.SetField(this.ObjectVersionPropertyDefinition, ExchangeObjectVersion.Exchange2010);
		}

		// Token: 0x17000F45 RID: 3909
		// (get) Token: 0x06003355 RID: 13141 RVA: 0x000D0BF1 File Offset: 0x000CEDF1
		internal override ProviderPropertyDefinition ObjectVersionPropertyDefinition
		{
			get
			{
				return RecipientStatisticsReportSchema.ExchangeVersion;
			}
		}

		// Token: 0x17000F46 RID: 3910
		// (get) Token: 0x06003356 RID: 13142 RVA: 0x000D0BF8 File Offset: 0x000CEDF8
		internal override ProviderPropertyDefinition ObjectStatePropertyDefinition
		{
			get
			{
				return RecipientStatisticsReportSchema.ObjectState;
			}
		}

		// Token: 0x17000F47 RID: 3911
		// (get) Token: 0x06003357 RID: 13143 RVA: 0x000D0BFF File Offset: 0x000CEDFF
		internal override ProviderPropertyDefinition ObjectIdentityPropertyDefinition
		{
			get
			{
				return RecipientStatisticsReportSchema.Identity;
			}
		}
	}
}

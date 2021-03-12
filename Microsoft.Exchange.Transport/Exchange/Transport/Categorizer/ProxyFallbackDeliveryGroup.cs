using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000244 RID: 580
	internal class ProxyFallbackDeliveryGroup : ServerCollectionDeliveryGroup
	{
		// Token: 0x06001961 RID: 6497 RVA: 0x00066A9F File Offset: 0x00064C9F
		public ProxyFallbackDeliveryGroup(RoutedServerCollection fallbackServers) : base(fallbackServers)
		{
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06001962 RID: 6498 RVA: 0x00066AA8 File Offset: 0x00064CA8
		public override string Name
		{
			get
			{
				return "ProxyFallbackDeliveryGroup";
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001963 RID: 6499 RVA: 0x00066AAF File Offset: 0x00064CAF
		public override DeliveryType DeliveryType
		{
			get
			{
				return DeliveryType.Undefined;
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001964 RID: 6500 RVA: 0x00066AB2 File Offset: 0x00064CB2
		public override Guid NextHopGuid
		{
			get
			{
				return Guid.Empty;
			}
		}
	}
}

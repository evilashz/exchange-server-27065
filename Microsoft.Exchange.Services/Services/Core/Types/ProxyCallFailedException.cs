using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000854 RID: 2132
	internal class ProxyCallFailedException : ServicePermanentException
	{
		// Token: 0x06003D6E RID: 15726 RVA: 0x000D7892 File Offset: 0x000D5A92
		public ProxyCallFailedException() : base((CoreResources.IDs)3032417457U)
		{
		}

		// Token: 0x17000EB2 RID: 3762
		// (get) Token: 0x06003D6F RID: 15727 RVA: 0x000D78A4 File Offset: 0x000D5AA4
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007SP1;
			}
		}
	}
}

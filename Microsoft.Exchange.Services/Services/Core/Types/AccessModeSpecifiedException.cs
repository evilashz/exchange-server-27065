using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006A3 RID: 1699
	internal sealed class AccessModeSpecifiedException : ServicePermanentException
	{
		// Token: 0x0600346C RID: 13420 RVA: 0x000BCEF2 File Offset: 0x000BB0F2
		public AccessModeSpecifiedException() : base((CoreResources.IDs)3314483401U)
		{
		}

		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x0600346D RID: 13421 RVA: 0x000BCF04 File Offset: 0x000BB104
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2010SP1;
			}
		}
	}
}

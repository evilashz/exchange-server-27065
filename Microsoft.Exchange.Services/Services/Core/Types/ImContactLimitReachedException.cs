using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200079C RID: 1948
	internal class ImContactLimitReachedException : ServicePermanentException
	{
		// Token: 0x06003A7B RID: 14971 RVA: 0x000CF088 File Offset: 0x000CD288
		public ImContactLimitReachedException() : base(ResponseCodeType.ErrorImContactLimitReached, CoreResources.ErrorImContactLimitReached(Global.UnifiedContactStoreConfiguration.MaxImContacts))
		{
		}

		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x06003A7C RID: 14972 RVA: 0x000CF0A4 File Offset: 0x000CD2A4
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2012;
			}
		}
	}
}

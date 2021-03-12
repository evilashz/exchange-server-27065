using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200087F RID: 2175
	internal class ServiceDiscoveryFailedException : ServicePermanentException
	{
		// Token: 0x06003E5D RID: 15965 RVA: 0x000D85B4 File Offset: 0x000D67B4
		public ServiceDiscoveryFailedException(Exception innerException) : base(CoreResources.IDs.ErrorProxyServiceDiscoveryFailed, innerException)
		{
		}

		// Token: 0x17000F14 RID: 3860
		// (get) Token: 0x06003E5E RID: 15966 RVA: 0x000D85C7 File Offset: 0x000D67C7
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007SP1;
			}
		}
	}
}

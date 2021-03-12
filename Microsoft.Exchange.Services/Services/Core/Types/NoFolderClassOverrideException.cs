using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200081E RID: 2078
	internal class NoFolderClassOverrideException : ServicePermanentException
	{
		// Token: 0x06003C39 RID: 15417 RVA: 0x000D5A55 File Offset: 0x000D3C55
		public NoFolderClassOverrideException() : base((CoreResources.IDs)3753602229U)
		{
		}

		// Token: 0x17000E44 RID: 3652
		// (get) Token: 0x06003C3A RID: 15418 RVA: 0x000D5A67 File Offset: 0x000D3C67
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}

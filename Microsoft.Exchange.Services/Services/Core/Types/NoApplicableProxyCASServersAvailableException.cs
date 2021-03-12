using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200081D RID: 2077
	internal class NoApplicableProxyCASServersAvailableException : ServicePermanentException
	{
		// Token: 0x06003C37 RID: 15415 RVA: 0x000D5A3C File Offset: 0x000D3C3C
		public NoApplicableProxyCASServersAvailableException() : base((CoreResources.IDs)4164112684U)
		{
		}

		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x06003C38 RID: 15416 RVA: 0x000D5A4E File Offset: 0x000D3C4E
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007SP1;
			}
		}
	}
}

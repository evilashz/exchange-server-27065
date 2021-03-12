using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000829 RID: 2089
	internal class NoRespondingCASInDestinationSiteException : ServicePermanentException
	{
		// Token: 0x06003C6A RID: 15466 RVA: 0x000D5CBD File Offset: 0x000D3EBD
		public NoRespondingCASInDestinationSiteException() : base((CoreResources.IDs)4252309617U)
		{
		}

		// Token: 0x17000E5A RID: 3674
		// (get) Token: 0x06003C6B RID: 15467 RVA: 0x000D5CCF File Offset: 0x000D3ECF
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007SP1;
			}
		}
	}
}

using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200081C RID: 2076
	internal sealed class NameResolutionNoResultsException : ServicePermanentException
	{
		// Token: 0x06003C35 RID: 15413 RVA: 0x000D5A23 File Offset: 0x000D3C23
		public NameResolutionNoResultsException() : base(CoreResources.IDs.ErrorNameResolutionNoResults)
		{
		}

		// Token: 0x17000E42 RID: 3650
		// (get) Token: 0x06003C36 RID: 15414 RVA: 0x000D5A35 File Offset: 0x000D3C35
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}

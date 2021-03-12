using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000816 RID: 2070
	internal sealed class MoreThanOneAccessModeSpecifiedException : ServicePermanentException
	{
		// Token: 0x06003C29 RID: 15401 RVA: 0x000D598D File Offset: 0x000D3B8D
		public MoreThanOneAccessModeSpecifiedException() : base(CoreResources.IDs.ErrorMoreThanOneAccessModeSpecified)
		{
		}

		// Token: 0x17000E3C RID: 3644
		// (get) Token: 0x06003C2A RID: 15402 RVA: 0x000D599F File Offset: 0x000D3B9F
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}

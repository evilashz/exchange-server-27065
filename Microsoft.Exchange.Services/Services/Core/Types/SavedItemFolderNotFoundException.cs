using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200086C RID: 2156
	internal sealed class SavedItemFolderNotFoundException : ServicePermanentException
	{
		// Token: 0x06003DCC RID: 15820 RVA: 0x000D7E84 File Offset: 0x000D6084
		public SavedItemFolderNotFoundException() : base((CoreResources.IDs)3610830273U)
		{
		}

		// Token: 0x06003DCD RID: 15821 RVA: 0x000D7E96 File Offset: 0x000D6096
		public SavedItemFolderNotFoundException(Exception innerException) : base((CoreResources.IDs)3610830273U, innerException)
		{
		}

		// Token: 0x17000ED5 RID: 3797
		// (get) Token: 0x06003DCE RID: 15822 RVA: 0x000D7EA9 File Offset: 0x000D60A9
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}

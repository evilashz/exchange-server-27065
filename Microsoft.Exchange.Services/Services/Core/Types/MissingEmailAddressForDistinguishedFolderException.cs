using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200080D RID: 2061
	internal sealed class MissingEmailAddressForDistinguishedFolderException : ServicePermanentException
	{
		// Token: 0x06003C15 RID: 15381 RVA: 0x000D588B File Offset: 0x000D3A8B
		public MissingEmailAddressForDistinguishedFolderException() : base(CoreResources.IDs.ErrorMissingEmailAddress)
		{
		}

		// Token: 0x17000E33 RID: 3635
		// (get) Token: 0x06003C16 RID: 15382 RVA: 0x000D589D File Offset: 0x000D3A9D
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}

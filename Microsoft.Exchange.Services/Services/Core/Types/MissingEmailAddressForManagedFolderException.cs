using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200080E RID: 2062
	internal class MissingEmailAddressForManagedFolderException : ServicePermanentException
	{
		// Token: 0x06003C17 RID: 15383 RVA: 0x000D58A4 File Offset: 0x000D3AA4
		public MissingEmailAddressForManagedFolderException() : base(CoreResources.IDs.ErrorMissingEmailAddressForManagedFolder)
		{
		}

		// Token: 0x17000E34 RID: 3636
		// (get) Token: 0x06003C18 RID: 15384 RVA: 0x000D58B6 File Offset: 0x000D3AB6
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}

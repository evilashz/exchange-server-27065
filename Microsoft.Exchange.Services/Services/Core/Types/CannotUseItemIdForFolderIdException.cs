using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200071F RID: 1823
	internal sealed class CannotUseItemIdForFolderIdException : ServicePermanentException
	{
		// Token: 0x0600374F RID: 14159 RVA: 0x000C56A5 File Offset: 0x000C38A5
		public CannotUseItemIdForFolderIdException() : base((CoreResources.IDs)2423603834U)
		{
		}

		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x06003750 RID: 14160 RVA: 0x000C56B7 File Offset: 0x000C38B7
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}

using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200078D RID: 1933
	internal sealed class FolderNotFoundException : ServicePermanentException
	{
		// Token: 0x060039A9 RID: 14761 RVA: 0x000CB7FE File Offset: 0x000C99FE
		public FolderNotFoundException() : base((CoreResources.IDs)3395659933U)
		{
		}

		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x060039AA RID: 14762 RVA: 0x000CB810 File Offset: 0x000C9A10
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}

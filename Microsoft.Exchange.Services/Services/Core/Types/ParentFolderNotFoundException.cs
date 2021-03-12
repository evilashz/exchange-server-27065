using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200083A RID: 2106
	internal sealed class ParentFolderNotFoundException : ServicePermanentException
	{
		// Token: 0x06003CC0 RID: 15552 RVA: 0x000D6392 File Offset: 0x000D4592
		public ParentFolderNotFoundException() : base((CoreResources.IDs)4217637937U)
		{
		}

		// Token: 0x06003CC1 RID: 15553 RVA: 0x000D63A4 File Offset: 0x000D45A4
		public ParentFolderNotFoundException(Exception innerException) : base((CoreResources.IDs)4217637937U, innerException)
		{
		}

		// Token: 0x17000E7C RID: 3708
		// (get) Token: 0x06003CC2 RID: 15554 RVA: 0x000D63B7 File Offset: 0x000D45B7
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}

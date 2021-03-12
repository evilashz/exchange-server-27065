using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020008AF RID: 2223
	internal sealed class UnsupportedCultureException : ServicePermanentException
	{
		// Token: 0x06003F38 RID: 16184 RVA: 0x000DB20E File Offset: 0x000D940E
		public UnsupportedCultureException() : base(CoreResources.IDs.ErrorUnsupportedCulture)
		{
		}

		// Token: 0x06003F39 RID: 16185 RVA: 0x000DB220 File Offset: 0x000D9420
		public UnsupportedCultureException(Exception innerException) : base(CoreResources.IDs.ErrorUnsupportedCulture, innerException)
		{
		}

		// Token: 0x17000F4E RID: 3918
		// (get) Token: 0x06003F3A RID: 16186 RVA: 0x000DB233 File Offset: 0x000D9433
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}

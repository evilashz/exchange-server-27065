using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020008B1 RID: 2225
	internal sealed class UnsupportedMimeConversionException : ServicePermanentException
	{
		// Token: 0x06003F3D RID: 16189 RVA: 0x000DB253 File Offset: 0x000D9453
		public UnsupportedMimeConversionException() : base(CoreResources.IDs.ErrorUnsupportedMimeConversion)
		{
		}

		// Token: 0x17000F50 RID: 3920
		// (get) Token: 0x06003F3E RID: 16190 RVA: 0x000DB265 File Offset: 0x000D9465
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}

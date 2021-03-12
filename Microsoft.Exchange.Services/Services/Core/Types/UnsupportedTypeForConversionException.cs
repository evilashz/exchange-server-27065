using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020008B7 RID: 2231
	internal sealed class UnsupportedTypeForConversionException : ServicePermanentException
	{
		// Token: 0x06003F4A RID: 16202 RVA: 0x000DB30C File Offset: 0x000D950C
		public UnsupportedTypeForConversionException() : base(CoreResources.IDs.ErrorUnsupportedTypeForConversion)
		{
		}

		// Token: 0x17000F56 RID: 3926
		// (get) Token: 0x06003F4B RID: 16203 RVA: 0x000DB31E File Offset: 0x000D951E
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}

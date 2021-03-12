using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020008B0 RID: 2224
	internal sealed class UnsupportedMapiPropertyTypeException : ServicePermanentException
	{
		// Token: 0x06003F3B RID: 16187 RVA: 0x000DB23A File Offset: 0x000D943A
		public UnsupportedMapiPropertyTypeException() : base(CoreResources.IDs.ErrorUnsupportedMapiPropertyType)
		{
		}

		// Token: 0x17000F4F RID: 3919
		// (get) Token: 0x06003F3C RID: 16188 RVA: 0x000DB24C File Offset: 0x000D944C
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}

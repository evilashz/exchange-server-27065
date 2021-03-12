using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000817 RID: 2071
	[Serializable]
	internal sealed class MoveCopyException : ServicePermanentException
	{
		// Token: 0x06003C2B RID: 15403 RVA: 0x000D59A6 File Offset: 0x000D3BA6
		public MoveCopyException() : base((CoreResources.IDs)2524108663U)
		{
		}

		// Token: 0x17000E3D RID: 3645
		// (get) Token: 0x06003C2C RID: 15404 RVA: 0x000D59B8 File Offset: 0x000D3BB8
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}

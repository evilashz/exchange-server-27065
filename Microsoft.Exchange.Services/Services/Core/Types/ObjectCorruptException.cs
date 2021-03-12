using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200082E RID: 2094
	internal sealed class ObjectCorruptException : ServicePermanentException
	{
		// Token: 0x06003C71 RID: 15473 RVA: 0x000D5D18 File Offset: 0x000D3F18
		public ObjectCorruptException(Exception innerException, bool useItemError) : base(useItemError ? ((CoreResources.IDs)2624402344U) : ((CoreResources.IDs)2966054199U), innerException)
		{
		}

		// Token: 0x17000E5C RID: 3676
		// (get) Token: 0x06003C72 RID: 15474 RVA: 0x000D5D35 File Offset: 0x000D3F35
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}

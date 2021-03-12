using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000859 RID: 2137
	internal sealed class QueryFilterTooLongException : ServicePermanentException
	{
		// Token: 0x06003D7E RID: 15742 RVA: 0x000D7931 File Offset: 0x000D5B31
		public QueryFilterTooLongException() : base((CoreResources.IDs)2285125742U)
		{
		}

		// Token: 0x17000EB9 RID: 3769
		// (get) Token: 0x06003D7F RID: 15743 RVA: 0x000D7943 File Offset: 0x000D5B43
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}

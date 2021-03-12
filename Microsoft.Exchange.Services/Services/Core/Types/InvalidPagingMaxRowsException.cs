using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007BE RID: 1982
	internal sealed class InvalidPagingMaxRowsException : ServicePermanentException
	{
		// Token: 0x06003AD4 RID: 15060 RVA: 0x000CF804 File Offset: 0x000CDA04
		public InvalidPagingMaxRowsException() : base((CoreResources.IDs)2467205866U)
		{
		}

		// Token: 0x17000DDD RID: 3549
		// (get) Token: 0x06003AD5 RID: 15061 RVA: 0x000CF816 File Offset: 0x000CDA16
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}

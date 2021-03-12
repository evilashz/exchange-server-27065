using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200085A RID: 2138
	[Serializable]
	internal sealed class ReadReceiptNotPendingException : ServicePermanentException
	{
		// Token: 0x06003D80 RID: 15744 RVA: 0x000D794A File Offset: 0x000D5B4A
		public ReadReceiptNotPendingException() : base((CoreResources.IDs)2875907804U)
		{
		}

		// Token: 0x17000EBA RID: 3770
		// (get) Token: 0x06003D81 RID: 15745 RVA: 0x000D795C File Offset: 0x000D5B5C
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}

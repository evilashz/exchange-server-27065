using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200087A RID: 2170
	[Serializable]
	internal sealed class SentTaskRequestUpdateException : ServicePermanentException
	{
		// Token: 0x06003E4B RID: 15947 RVA: 0x000D8490 File Offset: 0x000D6690
		public SentTaskRequestUpdateException() : base(CoreResources.IDs.ErrorSentTaskRequestUpdate)
		{
		}

		// Token: 0x17000F0F RID: 3855
		// (get) Token: 0x06003E4C RID: 15948 RVA: 0x000D84A2 File Offset: 0x000D66A2
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}

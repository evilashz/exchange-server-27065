using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007B0 RID: 1968
	internal sealed class InvalidFractionalPagingParametersException : ServicePermanentException
	{
		// Token: 0x06003AB0 RID: 15024 RVA: 0x000CF40E File Offset: 0x000CD60E
		public InvalidFractionalPagingParametersException() : base((CoreResources.IDs)2620420056U)
		{
		}

		// Token: 0x17000DCF RID: 3535
		// (get) Token: 0x06003AB1 RID: 15025 RVA: 0x000CF420 File Offset: 0x000CD620
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}

using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007B7 RID: 1975
	internal sealed class InvalidIndexedPagingParametersException : ServicePermanentException
	{
		// Token: 0x06003ABF RID: 15039 RVA: 0x000CF4E2 File Offset: 0x000CD6E2
		public InvalidIndexedPagingParametersException() : base(CoreResources.IDs.ErrorInvalidIndexedPagingParameters)
		{
		}

		// Token: 0x17000DD6 RID: 3542
		// (get) Token: 0x06003AC0 RID: 15040 RVA: 0x000CF4F4 File Offset: 0x000CD6F4
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}

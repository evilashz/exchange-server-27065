using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007AA RID: 1962
	internal sealed class InvalidExchangeImpersonationHeaderException : ServicePermanentException
	{
		// Token: 0x06003A9D RID: 15005 RVA: 0x000CF32D File Offset: 0x000CD52D
		public InvalidExchangeImpersonationHeaderException() : base(CoreResources.IDs.ErrorInvalidExchangeImpersonationHeaderData)
		{
		}

		// Token: 0x17000DC9 RID: 3529
		// (get) Token: 0x06003A9E RID: 15006 RVA: 0x000CF33F File Offset: 0x000CD53F
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}

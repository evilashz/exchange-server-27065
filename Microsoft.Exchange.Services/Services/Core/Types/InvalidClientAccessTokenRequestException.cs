using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007A6 RID: 1958
	internal sealed class InvalidClientAccessTokenRequestException : ServicePermanentException
	{
		// Token: 0x06003A91 RID: 14993 RVA: 0x000CF289 File Offset: 0x000CD489
		public InvalidClientAccessTokenRequestException() : base((CoreResources.IDs)2958727324U)
		{
		}

		// Token: 0x06003A92 RID: 14994 RVA: 0x000CF29B File Offset: 0x000CD49B
		public InvalidClientAccessTokenRequestException(Enum messageId) : base(ResponseCodeType.ErrorInvalidClientAccessTokenRequest, messageId)
		{
		}

		// Token: 0x06003A93 RID: 14995 RVA: 0x000CF2A9 File Offset: 0x000CD4A9
		public InvalidClientAccessTokenRequestException(Enum messageId, Exception innerException) : base(ResponseCodeType.ErrorInvalidClientAccessTokenRequest, messageId, innerException)
		{
		}

		// Token: 0x17000DC5 RID: 3525
		// (get) Token: 0x06003A94 RID: 14996 RVA: 0x000CF2B8 File Offset: 0x000CD4B8
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2012;
			}
		}
	}
}

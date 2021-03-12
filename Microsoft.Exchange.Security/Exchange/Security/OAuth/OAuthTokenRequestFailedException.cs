using System;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x020000F3 RID: 243
	public class OAuthTokenRequestFailedException : Exception
	{
		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x00036FEB File Offset: 0x000351EB
		// (set) Token: 0x06000827 RID: 2087 RVA: 0x00036FF3 File Offset: 0x000351F3
		public OAuthOutboundErrorCodes ErrorCode { get; private set; }

		// Token: 0x06000828 RID: 2088 RVA: 0x00036FFC File Offset: 0x000351FC
		public OAuthTokenRequestFailedException(OAuthOutboundErrorCodes error, object[] args, Exception innerException = null) : base(OAuthOutboundErrorsUtil.GetDescription(error, args), innerException)
		{
			this.ErrorCode = error;
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00037013 File Offset: 0x00035213
		public OAuthTokenRequestFailedException(OAuthOutboundErrorCodes error, string args = null, Exception innerException = null) : base(OAuthOutboundErrorsUtil.GetDescription(error, args), innerException)
		{
			this.ErrorCode = error;
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0003702A File Offset: 0x0003522A
		public string GetKeyForErrorCode()
		{
			return this.ErrorCode.ToString();
		}
	}
}

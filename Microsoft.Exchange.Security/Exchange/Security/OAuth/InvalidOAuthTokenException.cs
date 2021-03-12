using System;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x020000BC RID: 188
	internal class InvalidOAuthTokenException : Exception
	{
		// Token: 0x06000652 RID: 1618 RVA: 0x0002F141 File Offset: 0x0002D341
		public InvalidOAuthTokenException(OAuthErrors error, object[] args = null, Exception innerException = null) : base((args == null) ? OAuthErrorsUtil.GetDescription(error) : string.Format(OAuthErrorsUtil.GetDescription(error), args), innerException)
		{
			this.ErrorCode = error;
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x0002F168 File Offset: 0x0002D368
		public OAuthErrorCategory ErrorCategory
		{
			get
			{
				return OAuthErrorsUtil.GetErrorCategory(this.ErrorCode);
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x0002F175 File Offset: 0x0002D375
		// (set) Token: 0x06000655 RID: 1621 RVA: 0x0002F17D File Offset: 0x0002D37D
		public OAuthErrors ErrorCode { get; private set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x0002F186 File Offset: 0x0002D386
		// (set) Token: 0x06000657 RID: 1623 RVA: 0x0002F18E File Offset: 0x0002D38E
		public string ExtraData { get; set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x0002F197 File Offset: 0x0002D397
		// (set) Token: 0x06000659 RID: 1625 RVA: 0x0002F19F File Offset: 0x0002D39F
		public bool LogEvent { get; set; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x0002F1A8 File Offset: 0x0002D3A8
		// (set) Token: 0x0600065B RID: 1627 RVA: 0x0002F1B0 File Offset: 0x0002D3B0
		public string LogPeriodicKey { get; set; }

		// Token: 0x04000620 RID: 1568
		public static Lazy<InvalidOAuthTokenException> OAuthRequestProxyToDownLevelException = new Lazy<InvalidOAuthTokenException>(() => new InvalidOAuthTokenException(OAuthErrors.UserOAuthNotSupported, null, null));
	}
}

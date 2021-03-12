using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007D2 RID: 2002
	internal sealed class InvalidSerializedAccessTokenException : ServicePermanentException
	{
		// Token: 0x06003B0A RID: 15114 RVA: 0x000CFB29 File Offset: 0x000CDD29
		public InvalidSerializedAccessTokenException() : base((CoreResources.IDs)2485795088U)
		{
		}

		// Token: 0x06003B0B RID: 15115 RVA: 0x000CFB3B File Offset: 0x000CDD3B
		public InvalidSerializedAccessTokenException(Exception innerException) : base((CoreResources.IDs)2485795088U, innerException)
		{
		}

		// Token: 0x06003B0C RID: 15116 RVA: 0x000CFB4E File Offset: 0x000CDD4E
		public InvalidSerializedAccessTokenException(Enum messageId, Exception innerException) : base(ResponseCodeType.ErrorInvalidSerializedAccessToken, messageId, innerException)
		{
		}

		// Token: 0x17000DF1 RID: 3569
		// (get) Token: 0x06003B0D RID: 15117 RVA: 0x000CFB5D File Offset: 0x000CDD5D
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}

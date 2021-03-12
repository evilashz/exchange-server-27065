using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007AD RID: 1965
	[Serializable]
	internal class InvalidStoreIdException : ServicePermanentException
	{
		// Token: 0x06003AA5 RID: 15013 RVA: 0x000CF3A2 File Offset: 0x000CD5A2
		public InvalidStoreIdException(Enum messageId) : base(messageId)
		{
		}

		// Token: 0x06003AA6 RID: 15014 RVA: 0x000CF3AB File Offset: 0x000CD5AB
		public InvalidStoreIdException(Enum messageId, Exception innerException) : base(messageId, innerException)
		{
		}

		// Token: 0x06003AA7 RID: 15015 RVA: 0x000CF3B5 File Offset: 0x000CD5B5
		public InvalidStoreIdException(ResponseCodeType responseCode, Enum messageId) : base(responseCode, messageId)
		{
		}

		// Token: 0x17000DCC RID: 3532
		// (get) Token: 0x06003AA8 RID: 15016 RVA: 0x000CF3BF File Offset: 0x000CD5BF
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}

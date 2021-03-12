using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007B2 RID: 1970
	[Serializable]
	internal class InvalidIdFormatVersionException : ServicePermanentException
	{
		// Token: 0x06003AB4 RID: 15028 RVA: 0x000CF440 File Offset: 0x000CD640
		public InvalidIdFormatVersionException() : base(CoreResources.IDs.ErrorInvalidIdMalformedEwsLegacyIdFormat)
		{
			this.exchangeVersion = ExchangeVersion.Exchange2007SP1;
		}

		// Token: 0x06003AB5 RID: 15029 RVA: 0x000CF45D File Offset: 0x000CD65D
		public InvalidIdFormatVersionException(Enum messageId) : base(ResponseCodeType.ErrorInvalidIdMalformed, messageId)
		{
			this.exchangeVersion = ExchangeVersion.Exchange2007;
		}

		// Token: 0x17000DD1 RID: 3537
		// (get) Token: 0x06003AB6 RID: 15030 RVA: 0x000CF476 File Offset: 0x000CD676
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return this.exchangeVersion;
			}
		}

		// Token: 0x040020A9 RID: 8361
		private ExchangeVersion exchangeVersion;
	}
}

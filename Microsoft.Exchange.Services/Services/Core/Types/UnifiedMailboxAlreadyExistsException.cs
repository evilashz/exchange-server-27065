using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020008AC RID: 2220
	internal sealed class UnifiedMailboxAlreadyExistsException : ServicePermanentException
	{
		// Token: 0x06003F18 RID: 16152 RVA: 0x000DAA91 File Offset: 0x000D8C91
		public UnifiedMailboxAlreadyExistsException(Enum messageId) : base(ResponseCodeType.ErrorUnifiedMailboxAlreadyExists, messageId)
		{
		}

		// Token: 0x06003F19 RID: 16153 RVA: 0x000DAA9F File Offset: 0x000D8C9F
		public UnifiedMailboxAlreadyExistsException(Enum messageId, Exception innerException) : base(ResponseCodeType.ErrorUnifiedMailboxAlreadyExists, messageId, innerException)
		{
		}

		// Token: 0x17000F45 RID: 3909
		// (get) Token: 0x06003F1A RID: 16154 RVA: 0x000DAAAE File Offset: 0x000D8CAE
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2013;
			}
		}
	}
}

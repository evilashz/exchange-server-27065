using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007ED RID: 2029
	internal sealed class MailboxHoldNotFoundException : ServicePermanentException
	{
		// Token: 0x06003B6F RID: 15215 RVA: 0x000D0120 File Offset: 0x000CE320
		public MailboxHoldNotFoundException(Enum messageId) : base(ResponseCodeType.ErrorMailboxHoldNotFound, messageId)
		{
		}

		// Token: 0x06003B70 RID: 15216 RVA: 0x000D012E File Offset: 0x000CE32E
		public MailboxHoldNotFoundException(Enum messageId, Exception innerException) : base(ResponseCodeType.ErrorMailboxHoldNotFound, messageId, innerException)
		{
		}

		// Token: 0x17000E12 RID: 3602
		// (get) Token: 0x06003B71 RID: 15217 RVA: 0x000D013D File Offset: 0x000CE33D
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007SP1;
			}
		}
	}
}

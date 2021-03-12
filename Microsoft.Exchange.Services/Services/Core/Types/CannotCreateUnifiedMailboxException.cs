using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000713 RID: 1811
	internal sealed class CannotCreateUnifiedMailboxException : ServicePermanentException
	{
		// Token: 0x0600372D RID: 14125 RVA: 0x000C5532 File Offset: 0x000C3732
		public CannotCreateUnifiedMailboxException(Enum messageId) : base(ResponseCodeType.ErrorCannotCreateUnifiedMailbox, messageId)
		{
		}

		// Token: 0x0600372E RID: 14126 RVA: 0x000C553D File Offset: 0x000C373D
		public CannotCreateUnifiedMailboxException(Enum messageId, Exception innerException) : base(ResponseCodeType.ErrorCannotCreateUnifiedMailbox, messageId, innerException)
		{
		}

		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x0600372F RID: 14127 RVA: 0x000C5549 File Offset: 0x000C3749
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2013;
			}
		}
	}
}

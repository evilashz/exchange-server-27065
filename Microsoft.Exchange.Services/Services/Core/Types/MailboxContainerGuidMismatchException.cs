using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007EC RID: 2028
	internal sealed class MailboxContainerGuidMismatchException : ServicePermanentException
	{
		// Token: 0x06003B6B RID: 15211 RVA: 0x000D00F2 File Offset: 0x000CE2F2
		public MailboxContainerGuidMismatchException(Enum messageId) : base(ResponseCodeType.ErrorMailboxContainerGuidMismatch, messageId)
		{
		}

		// Token: 0x06003B6C RID: 15212 RVA: 0x000D0100 File Offset: 0x000CE300
		public MailboxContainerGuidMismatchException(Enum messageId, Exception innerException) : base(ResponseCodeType.ErrorMailboxContainerGuidMismatch, messageId, innerException)
		{
		}

		// Token: 0x06003B6D RID: 15213 RVA: 0x000D010F File Offset: 0x000CE30F
		public MailboxContainerGuidMismatchException(ResponseCodeType responseCodeType, LocalizedString message) : base(responseCodeType, message)
		{
		}

		// Token: 0x17000E11 RID: 3601
		// (get) Token: 0x06003B6E RID: 15214 RVA: 0x000D0119 File Offset: 0x000CE319
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2013;
			}
		}
	}
}

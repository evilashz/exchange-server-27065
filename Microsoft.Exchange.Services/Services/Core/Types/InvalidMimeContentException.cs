using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007BC RID: 1980
	internal sealed class InvalidMimeContentException : ServicePermanentException
	{
		// Token: 0x06003ACF RID: 15055 RVA: 0x000CF7D1 File Offset: 0x000CD9D1
		public InvalidMimeContentException(Enum messageId) : base(messageId)
		{
		}

		// Token: 0x06003AD0 RID: 15056 RVA: 0x000CF7DA File Offset: 0x000CD9DA
		public InvalidMimeContentException(Enum messageId, Exception innerException) : base(messageId, innerException)
		{
		}

		// Token: 0x17000DDB RID: 3547
		// (get) Token: 0x06003AD1 RID: 15057 RVA: 0x000CF7E4 File Offset: 0x000CD9E4
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}

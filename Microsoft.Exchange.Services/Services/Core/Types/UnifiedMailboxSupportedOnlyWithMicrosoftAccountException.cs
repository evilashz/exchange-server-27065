using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020008AD RID: 2221
	internal sealed class UnifiedMailboxSupportedOnlyWithMicrosoftAccountException : ServicePermanentException
	{
		// Token: 0x06003F1B RID: 16155 RVA: 0x000DAAB5 File Offset: 0x000D8CB5
		public UnifiedMailboxSupportedOnlyWithMicrosoftAccountException(Enum messageId) : base(ResponseCodeType.ErrorUnifiedMailboxSupportedOnlyWithMicrosoftAccount, messageId)
		{
		}

		// Token: 0x06003F1C RID: 16156 RVA: 0x000DAAC3 File Offset: 0x000D8CC3
		public UnifiedMailboxSupportedOnlyWithMicrosoftAccountException(Enum messageId, Exception innerException) : base(ResponseCodeType.ErrorUnifiedMailboxSupportedOnlyWithMicrosoftAccount, messageId, innerException)
		{
		}

		// Token: 0x17000F46 RID: 3910
		// (get) Token: 0x06003F1D RID: 16157 RVA: 0x000DAAD2 File Offset: 0x000D8CD2
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2013;
			}
		}
	}
}

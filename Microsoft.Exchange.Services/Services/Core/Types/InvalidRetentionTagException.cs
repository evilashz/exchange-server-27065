using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007CE RID: 1998
	internal sealed class InvalidRetentionTagException : ServicePermanentException
	{
		// Token: 0x06003B01 RID: 15105 RVA: 0x000CFAC2 File Offset: 0x000CDCC2
		public InvalidRetentionTagException(Enum messageId) : base(messageId)
		{
		}

		// Token: 0x06003B02 RID: 15106 RVA: 0x000CFACB File Offset: 0x000CDCCB
		public InvalidRetentionTagException(Enum messageId, Exception innerException) : base(messageId, innerException)
		{
		}

		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x06003B03 RID: 15107 RVA: 0x000CFAD5 File Offset: 0x000CDCD5
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2012;
			}
		}
	}
}

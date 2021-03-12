using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007CD RID: 1997
	internal sealed class InvalidRestrictionException : ServicePermanentException
	{
		// Token: 0x06003AFE RID: 15102 RVA: 0x000CFAA8 File Offset: 0x000CDCA8
		public InvalidRestrictionException(Enum messageId) : base(messageId)
		{
		}

		// Token: 0x06003AFF RID: 15103 RVA: 0x000CFAB1 File Offset: 0x000CDCB1
		public InvalidRestrictionException(Enum messageId, Exception innerException) : base(messageId, innerException)
		{
		}

		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x06003B00 RID: 15104 RVA: 0x000CFABB File Offset: 0x000CDCBB
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}

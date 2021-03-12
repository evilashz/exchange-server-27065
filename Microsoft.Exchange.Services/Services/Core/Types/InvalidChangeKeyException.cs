using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007A5 RID: 1957
	internal sealed class InvalidChangeKeyException : ServicePermanentException
	{
		// Token: 0x06003A8E RID: 14990 RVA: 0x000CF25D File Offset: 0x000CD45D
		public InvalidChangeKeyException() : base(CoreResources.IDs.ErrorInvalidChangeKey)
		{
		}

		// Token: 0x06003A8F RID: 14991 RVA: 0x000CF26F File Offset: 0x000CD46F
		public InvalidChangeKeyException(Exception innerException) : base(CoreResources.IDs.ErrorInvalidChangeKey, innerException)
		{
		}

		// Token: 0x17000DC4 RID: 3524
		// (get) Token: 0x06003A90 RID: 14992 RVA: 0x000CF282 File Offset: 0x000CD482
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}

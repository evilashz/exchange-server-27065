using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007A7 RID: 1959
	internal sealed class InvalidCompleteDateException : ServicePermanentException
	{
		// Token: 0x06003A95 RID: 14997 RVA: 0x000CF2BF File Offset: 0x000CD4BF
		public InvalidCompleteDateException(Enum messageId) : base(ResponseCodeType.ErrorInvalidCompleteDate, messageId)
		{
		}

		// Token: 0x06003A96 RID: 14998 RVA: 0x000CF2CD File Offset: 0x000CD4CD
		public InvalidCompleteDateException(Enum messageId, Exception innerException) : base(ResponseCodeType.ErrorInvalidCompleteDate, messageId, innerException)
		{
		}

		// Token: 0x17000DC6 RID: 3526
		// (get) Token: 0x06003A97 RID: 14999 RVA: 0x000CF2DC File Offset: 0x000CD4DC
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}

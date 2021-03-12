using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Services.ExchangeService
{
	// Token: 0x02000DE7 RID: 3559
	[Serializable]
	internal class ExchangeServicePermanentException : LocalizedException
	{
		// Token: 0x06005C0F RID: 23567 RVA: 0x0011DEEA File Offset: 0x0011C0EA
		public ExchangeServicePermanentException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06005C10 RID: 23568 RVA: 0x0011DEF3 File Offset: 0x0011C0F3
		public ExchangeServicePermanentException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06005C11 RID: 23569 RVA: 0x0011DEFD File Offset: 0x0011C0FD
		public ExchangeServicePermanentException(LocalizedException innerException) : this(innerException.LocalizedString, innerException)
		{
		}
	}
}

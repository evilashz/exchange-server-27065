using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Services.ExchangeService
{
	// Token: 0x02000DE8 RID: 3560
	[Serializable]
	internal class ExchangeServiceTransientException : TransientException
	{
		// Token: 0x06005C12 RID: 23570 RVA: 0x0011DF0C File Offset: 0x0011C10C
		public ExchangeServiceTransientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06005C13 RID: 23571 RVA: 0x0011DF15 File Offset: 0x0011C115
		public ExchangeServiceTransientException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06005C14 RID: 23572 RVA: 0x0011DF1F File Offset: 0x0011C11F
		public ExchangeServiceTransientException(LocalizedException innerException) : this(innerException.LocalizedString, innerException)
		{
		}
	}
}

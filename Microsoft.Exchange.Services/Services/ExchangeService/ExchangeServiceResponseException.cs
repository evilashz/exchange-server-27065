using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Services.ExchangeService
{
	// Token: 0x02000DE9 RID: 3561
	[Serializable]
	internal class ExchangeServiceResponseException : ExchangeServicePermanentException
	{
		// Token: 0x06005C15 RID: 23573 RVA: 0x0011DF2E File Offset: 0x0011C12E
		public ExchangeServiceResponseException(LocalizedString message) : base(message)
		{
		}
	}
}

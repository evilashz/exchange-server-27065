using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.InfoWorker.Common
{
	// Token: 0x02000011 RID: 17
	internal class IWPermanentException : LocalizedException
	{
		// Token: 0x06000033 RID: 51 RVA: 0x00002C16 File Offset: 0x00000E16
		public IWPermanentException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002C1F File Offset: 0x00000E1F
		public IWPermanentException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

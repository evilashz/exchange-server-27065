using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B0A RID: 2826
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SingleInstanceItemHandlerPermanentException : LocalizedException
	{
		// Token: 0x060066A4 RID: 26276 RVA: 0x001B342A File Offset: 0x001B162A
		public SingleInstanceItemHandlerPermanentException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060066A5 RID: 26277 RVA: 0x001B3433 File Offset: 0x001B1633
		public SingleInstanceItemHandlerPermanentException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

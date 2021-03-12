using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B09 RID: 2825
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SingleInstanceItemHandlerTransientException : TransientException
	{
		// Token: 0x060066A2 RID: 26274 RVA: 0x001B3417 File Offset: 0x001B1617
		public SingleInstanceItemHandlerTransientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060066A3 RID: 26275 RVA: 0x001B3420 File Offset: 0x001B1620
		public SingleInstanceItemHandlerTransientException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

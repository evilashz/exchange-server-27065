using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000053 RID: 83
	internal class MissingObjectException : AIPermanentException
	{
		// Token: 0x060002B1 RID: 689 RVA: 0x0000ED3A File Offset: 0x0000CF3A
		public MissingObjectException(LocalizedString message) : this(message, null)
		{
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000ED44 File Offset: 0x0000CF44
		public MissingObjectException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

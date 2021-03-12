using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x0200004D RID: 77
	internal class DeadMailboxException : AIPermanentException
	{
		// Token: 0x060002A7 RID: 679 RVA: 0x0000EB5B File Offset: 0x0000CD5B
		public DeadMailboxException(Exception innerException) : base(Strings.descDeadMailboxException, innerException)
		{
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000EB69 File Offset: 0x0000CD69
		public DeadMailboxException(LocalizedString explain) : base(explain, null)
		{
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000EB73 File Offset: 0x0000CD73
		public DeadMailboxException(LocalizedString explain, Exception innerException) : base(explain, innerException)
		{
		}
	}
}

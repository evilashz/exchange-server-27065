using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000043 RID: 67
	internal class TransientMailboxException : AITransientException
	{
		// Token: 0x0600028D RID: 653 RVA: 0x0000E80D File Offset: 0x0000CA0D
		public TransientMailboxException(LocalizedString explain, Exception innerException, RetrySchedule retrySchedule) : base(explain, innerException, retrySchedule)
		{
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000E818 File Offset: 0x0000CA18
		public TransientMailboxException(Exception innerException) : this(LocalizedString.Empty, innerException, null)
		{
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000E827 File Offset: 0x0000CA27
		public TransientMailboxException(LocalizedString explain) : this(explain, null, null)
		{
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000E832 File Offset: 0x0000CA32
		public TransientMailboxException(RetrySchedule retrySchedule) : this(LocalizedString.Empty, null, retrySchedule)
		{
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000E841 File Offset: 0x0000CA41
		public TransientMailboxException() : this(LocalizedString.Empty, null, null)
		{
		}
	}
}

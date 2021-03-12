using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000055 RID: 85
	internal class TransientServerException : AITransientException
	{
		// Token: 0x060002B5 RID: 693 RVA: 0x0000EDA1 File Offset: 0x0000CFA1
		public TransientServerException(LocalizedString explain, Exception innerException, RetrySchedule retrySchedule) : base(explain, innerException, retrySchedule)
		{
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000EDAC File Offset: 0x0000CFAC
		public TransientServerException(Exception innerException) : this(LocalizedString.Empty, innerException, null)
		{
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000EDBB File Offset: 0x0000CFBB
		public TransientServerException(LocalizedString explain) : this(explain, null, null)
		{
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000EDC6 File Offset: 0x0000CFC6
		public TransientServerException(RetrySchedule retrySchedule) : this(LocalizedString.Empty, null, retrySchedule)
		{
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000EDD5 File Offset: 0x0000CFD5
		public TransientServerException() : this(LocalizedString.Empty, null, null)
		{
		}
	}
}

using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x0200004A RID: 74
	internal class TransientDatabaseException : AITransientException
	{
		// Token: 0x0600029D RID: 669 RVA: 0x0000E9EF File Offset: 0x0000CBEF
		public TransientDatabaseException(LocalizedString explain, Exception innerException, RetrySchedule retrySchedule) : base(explain, innerException, retrySchedule)
		{
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000E9FA File Offset: 0x0000CBFA
		public TransientDatabaseException(Exception innerException) : this(LocalizedString.Empty, innerException, null)
		{
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000EA09 File Offset: 0x0000CC09
		public TransientDatabaseException(LocalizedString explain) : this(explain, null, null)
		{
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000EA14 File Offset: 0x0000CC14
		public TransientDatabaseException(RetrySchedule retrySchedule) : this(LocalizedString.Empty, null, retrySchedule)
		{
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000EA23 File Offset: 0x0000CC23
		public TransientDatabaseException() : this(LocalizedString.Empty, null, null)
		{
		}
	}
}

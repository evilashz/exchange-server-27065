using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200077E RID: 1918
	[Serializable]
	public class SubmissionQuotaExceededException : StoragePermanentException
	{
		// Token: 0x060048D6 RID: 18646 RVA: 0x001318DA File Offset: 0x0012FADA
		public SubmissionQuotaExceededException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060048D7 RID: 18647 RVA: 0x001318E3 File Offset: 0x0012FAE3
		protected SubmissionQuotaExceededException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

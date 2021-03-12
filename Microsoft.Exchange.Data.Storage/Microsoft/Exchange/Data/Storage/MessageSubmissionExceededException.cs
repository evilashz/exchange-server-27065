using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200074D RID: 1869
	[Serializable]
	public class MessageSubmissionExceededException : StoragePermanentException
	{
		// Token: 0x06004830 RID: 18480 RVA: 0x00130AB6 File Offset: 0x0012ECB6
		public MessageSubmissionExceededException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06004831 RID: 18481 RVA: 0x00130AC0 File Offset: 0x0012ECC0
		protected MessageSubmissionExceededException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

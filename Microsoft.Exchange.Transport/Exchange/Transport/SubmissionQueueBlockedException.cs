using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000072 RID: 114
	[Serializable]
	internal sealed class SubmissionQueueBlockedException : ApplicationException
	{
		// Token: 0x06000368 RID: 872 RVA: 0x0000F72A File Offset: 0x0000D92A
		public SubmissionQueueBlockedException() : base("The Process Manager indicated that submission queue is blocked.")
		{
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000F737 File Offset: 0x0000D937
		public SubmissionQueueBlockedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x040001DF RID: 479
		private const string SubmissionQueueBlockedMessage = "The Process Manager indicated that submission queue is blocked.";
	}
}

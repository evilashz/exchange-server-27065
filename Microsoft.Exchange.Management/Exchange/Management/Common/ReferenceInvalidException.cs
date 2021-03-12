using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Common
{
	// Token: 0x0200012E RID: 302
	[Serializable]
	public class ReferenceInvalidException : ReferenceException
	{
		// Token: 0x06000AC1 RID: 2753 RVA: 0x00032565 File Offset: 0x00030765
		public ReferenceInvalidException(string referenceValue, LocalizedException innerException) : base(referenceValue, innerException)
		{
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x0003256F File Offset: 0x0003076F
		public ReferenceInvalidException(string referenceValue, Exception innerException) : base(referenceValue, innerException)
		{
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x00032579 File Offset: 0x00030779
		public ReferenceInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

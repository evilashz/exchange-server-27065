using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000070 RID: 112
	[Serializable]
	internal class TransportComponentLoadFailedException : ApplicationException
	{
		// Token: 0x06000361 RID: 865 RVA: 0x0000F6A8 File Offset: 0x0000D8A8
		public TransportComponentLoadFailedException() : base(Strings.TransportComponentLoadFailed)
		{
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000F6BA File Offset: 0x0000D8BA
		public TransportComponentLoadFailedException(string message) : base(message)
		{
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000F6C3 File Offset: 0x0000D8C3
		public TransportComponentLoadFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000F6CD File Offset: 0x0000D8CD
		public TransportComponentLoadFailedException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}

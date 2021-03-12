using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200051A RID: 1306
	[Serializable]
	public class ProtocolException : Exception
	{
		// Token: 0x06002F27 RID: 12071 RVA: 0x000BE422 File Offset: 0x000BC622
		public ProtocolException()
		{
		}

		// Token: 0x06002F28 RID: 12072 RVA: 0x000BE42A File Offset: 0x000BC62A
		public ProtocolException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06002F29 RID: 12073 RVA: 0x000BE434 File Offset: 0x000BC634
		public ProtocolException(string message) : base(message)
		{
		}

		// Token: 0x06002F2A RID: 12074 RVA: 0x000BE43D File Offset: 0x000BC63D
		protected ProtocolException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}
	}
}

using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Conversion
{
	// Token: 0x020006AA RID: 1706
	internal class MdbefException : ApplicationException
	{
		// Token: 0x06001F97 RID: 8087 RVA: 0x0003BDD0 File Offset: 0x00039FD0
		public MdbefException(string message) : base(message)
		{
		}

		// Token: 0x06001F98 RID: 8088 RVA: 0x0003BDD9 File Offset: 0x00039FD9
		public MdbefException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001F99 RID: 8089 RVA: 0x0003BDE3 File Offset: 0x00039FE3
		public MdbefException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

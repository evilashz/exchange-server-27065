using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020001C0 RID: 448
	[Serializable]
	public sealed class LogException : TransientException
	{
		// Token: 0x06000C8C RID: 3212 RVA: 0x0002E886 File Offset: 0x0002CA86
		public LogException(string message) : base(new LocalizedString(message))
		{
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x0002E894 File Offset: 0x0002CA94
		public LogException(string message, Exception innerException) : base(new LocalizedString(message), innerException)
		{
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x0002E8A3 File Offset: 0x0002CAA3
		private LogException(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context)
		{
		}
	}
}

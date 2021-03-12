using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.ProcessManager
{
	// Token: 0x020007FB RID: 2043
	internal class ComProcessTimeoutException : ComProcessManagerException
	{
		// Token: 0x06002AEC RID: 10988 RVA: 0x0005DBDB File Offset: 0x0005BDDB
		internal ComProcessTimeoutException(string message) : base(message)
		{
		}

		// Token: 0x06002AED RID: 10989 RVA: 0x0005DBE4 File Offset: 0x0005BDE4
		internal ComProcessTimeoutException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x06002AEE RID: 10990 RVA: 0x0005DBEE File Offset: 0x0005BDEE
		public ComProcessTimeoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

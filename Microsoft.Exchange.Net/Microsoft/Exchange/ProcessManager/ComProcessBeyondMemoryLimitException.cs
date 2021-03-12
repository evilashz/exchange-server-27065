using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.ProcessManager
{
	// Token: 0x020007FE RID: 2046
	internal class ComProcessBeyondMemoryLimitException : ComProcessManagerException
	{
		// Token: 0x06002AF5 RID: 10997 RVA: 0x0005DC32 File Offset: 0x0005BE32
		internal ComProcessBeyondMemoryLimitException(string message) : base(message)
		{
		}

		// Token: 0x06002AF6 RID: 10998 RVA: 0x0005DC3B File Offset: 0x0005BE3B
		internal ComProcessBeyondMemoryLimitException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x06002AF7 RID: 10999 RVA: 0x0005DC45 File Offset: 0x0005BE45
		public ComProcessBeyondMemoryLimitException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

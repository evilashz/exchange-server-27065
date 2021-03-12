using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.ProcessManager
{
	// Token: 0x020007FD RID: 2045
	internal class ComProcessManagerInitializationException : ComProcessManagerException
	{
		// Token: 0x06002AF2 RID: 10994 RVA: 0x0005DC15 File Offset: 0x0005BE15
		internal ComProcessManagerInitializationException(string message) : base(message)
		{
		}

		// Token: 0x06002AF3 RID: 10995 RVA: 0x0005DC1E File Offset: 0x0005BE1E
		internal ComProcessManagerInitializationException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x06002AF4 RID: 10996 RVA: 0x0005DC28 File Offset: 0x0005BE28
		public ComProcessManagerInitializationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

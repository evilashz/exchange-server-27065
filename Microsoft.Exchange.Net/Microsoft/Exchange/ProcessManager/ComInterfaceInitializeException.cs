using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.ProcessManager
{
	// Token: 0x020007FC RID: 2044
	internal class ComInterfaceInitializeException : ComProcessManagerException
	{
		// Token: 0x06002AEF RID: 10991 RVA: 0x0005DBF8 File Offset: 0x0005BDF8
		internal ComInterfaceInitializeException(string message) : base(message)
		{
		}

		// Token: 0x06002AF0 RID: 10992 RVA: 0x0005DC01 File Offset: 0x0005BE01
		internal ComInterfaceInitializeException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x06002AF1 RID: 10993 RVA: 0x0005DC0B File Offset: 0x0005BE0B
		public ComInterfaceInitializeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

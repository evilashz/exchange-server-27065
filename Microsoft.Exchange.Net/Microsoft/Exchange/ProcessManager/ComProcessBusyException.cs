using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.ProcessManager
{
	// Token: 0x020007FA RID: 2042
	internal class ComProcessBusyException : ComProcessManagerException
	{
		// Token: 0x06002AE9 RID: 10985 RVA: 0x0005DBBE File Offset: 0x0005BDBE
		internal ComProcessBusyException(string message) : base(message)
		{
		}

		// Token: 0x06002AEA RID: 10986 RVA: 0x0005DBC7 File Offset: 0x0005BDC7
		internal ComProcessBusyException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x06002AEB RID: 10987 RVA: 0x0005DBD1 File Offset: 0x0005BDD1
		public ComProcessBusyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

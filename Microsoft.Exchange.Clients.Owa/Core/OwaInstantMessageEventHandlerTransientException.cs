using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001B8 RID: 440
	[Serializable]
	public class OwaInstantMessageEventHandlerTransientException : OwaTransientException
	{
		// Token: 0x06000F0E RID: 3854 RVA: 0x0005E797 File Offset: 0x0005C997
		public OwaInstantMessageEventHandlerTransientException(string message) : base(message)
		{
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x0005E7A0 File Offset: 0x0005C9A0
		public OwaInstantMessageEventHandlerTransientException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

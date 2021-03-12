using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001BB RID: 443
	[Serializable]
	public class OwaNotificationPipeException : OwaTransientException
	{
		// Token: 0x06000F14 RID: 3860 RVA: 0x0005E7D0 File Offset: 0x0005C9D0
		public OwaNotificationPipeException(string message) : base(message)
		{
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x0005E7D9 File Offset: 0x0005C9D9
		public OwaNotificationPipeException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

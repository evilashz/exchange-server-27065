using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200010F RID: 271
	[Serializable]
	public class OwaNotificationPipeException : OwaTransientException
	{
		// Token: 0x060009A8 RID: 2472 RVA: 0x00022B15 File Offset: 0x00020D15
		public OwaNotificationPipeException(string message) : base(message)
		{
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x00022B1E File Offset: 0x00020D1E
		public OwaNotificationPipeException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

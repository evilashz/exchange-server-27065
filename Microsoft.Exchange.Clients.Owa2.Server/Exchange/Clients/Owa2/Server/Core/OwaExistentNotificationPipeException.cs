using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000110 RID: 272
	[Serializable]
	public sealed class OwaExistentNotificationPipeException : OwaNotificationPipeException
	{
		// Token: 0x060009AA RID: 2474 RVA: 0x00022B28 File Offset: 0x00020D28
		public OwaExistentNotificationPipeException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x00022B32 File Offset: 0x00020D32
		public OwaExistentNotificationPipeException(string message) : base(message)
		{
		}
	}
}

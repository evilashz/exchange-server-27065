using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200011C RID: 284
	[Serializable]
	public sealed class OwaNotificationPipeWriteException : OwaNotificationPipeException
	{
		// Token: 0x060009C3 RID: 2499 RVA: 0x00022C48 File Offset: 0x00020E48
		public OwaNotificationPipeWriteException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001BC RID: 444
	[Serializable]
	public sealed class OwaNotificationPipeWriteException : OwaNotificationPipeException
	{
		// Token: 0x06000F16 RID: 3862 RVA: 0x0005E7E3 File Offset: 0x0005C9E3
		public OwaNotificationPipeWriteException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

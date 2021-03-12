using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001B7 RID: 439
	[Serializable]
	public sealed class OwaAsyncRequestTimeoutException : OwaTransientException
	{
		// Token: 0x06000F0D RID: 3853 RVA: 0x0005E78D File Offset: 0x0005C98D
		public OwaAsyncRequestTimeoutException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

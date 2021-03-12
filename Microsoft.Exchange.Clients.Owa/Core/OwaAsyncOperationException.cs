using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001B6 RID: 438
	[Serializable]
	public sealed class OwaAsyncOperationException : OwaPermanentException
	{
		// Token: 0x06000F0C RID: 3852 RVA: 0x0005E783 File Offset: 0x0005C983
		public OwaAsyncOperationException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

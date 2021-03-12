using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200043F RID: 1087
	internal class BifInfoException : ApplicationException
	{
		// Token: 0x06003273 RID: 12915 RVA: 0x000C596F File Offset: 0x000C3B6F
		public BifInfoException(string message) : base(message)
		{
		}

		// Token: 0x06003274 RID: 12916 RVA: 0x000C5978 File Offset: 0x000C3B78
		public BifInfoException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

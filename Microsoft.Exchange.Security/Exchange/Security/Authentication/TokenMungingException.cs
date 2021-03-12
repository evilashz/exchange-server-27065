using System;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x0200011A RID: 282
	internal class TokenMungingException : Exception
	{
		// Token: 0x06000924 RID: 2340 RVA: 0x0003C01E File Offset: 0x0003A21E
		public TokenMungingException(string message) : base(message)
		{
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0003C027 File Offset: 0x0003A227
		public TokenMungingException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

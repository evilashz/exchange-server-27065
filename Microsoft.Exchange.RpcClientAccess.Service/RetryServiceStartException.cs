using System;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000003 RID: 3
	internal class RetryServiceStartException : Exception
	{
		// Token: 0x06000002 RID: 2 RVA: 0x00002138 File Offset: 0x00000338
		public RetryServiceStartException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002142 File Offset: 0x00000342
		public RetryServiceStartException(string message) : base(message)
		{
		}
	}
}

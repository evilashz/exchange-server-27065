using System;

namespace Microsoft.Exchange.SharedCache.Client
{
	// Token: 0x02000002 RID: 2
	public class CacheClientException : Exception
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public CacheClientException(string message) : base(message)
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020D9 File Offset: 0x000002D9
		public CacheClientException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

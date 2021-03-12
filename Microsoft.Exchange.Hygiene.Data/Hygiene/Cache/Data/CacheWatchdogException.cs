using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Hygiene.Cache.Data
{
	// Token: 0x0200005B RID: 91
	[Serializable]
	internal class CacheWatchdogException : HygieneCacheException
	{
		// Token: 0x06000383 RID: 899 RVA: 0x0000A34B File Offset: 0x0000854B
		public CacheWatchdogException()
		{
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000A353 File Offset: 0x00008553
		public CacheWatchdogException(string message) : base(message)
		{
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000A35C File Offset: 0x0000855C
		public CacheWatchdogException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000A366 File Offset: 0x00008566
		protected CacheWatchdogException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

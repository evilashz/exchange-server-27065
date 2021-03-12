using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Hygiene.Cache.Data
{
	// Token: 0x0200004C RID: 76
	[Serializable]
	internal class CacheDeserializationException : HygieneCacheException
	{
		// Token: 0x06000308 RID: 776 RVA: 0x00009705 File Offset: 0x00007905
		public CacheDeserializationException()
		{
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000970D File Offset: 0x0000790D
		public CacheDeserializationException(string message) : base(message)
		{
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00009716 File Offset: 0x00007916
		public CacheDeserializationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00009720 File Offset: 0x00007920
		protected CacheDeserializationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Hygiene.Cache.Data
{
	// Token: 0x0200004B RID: 75
	[Serializable]
	internal class CacheDataLossException : HygieneCacheException
	{
		// Token: 0x06000304 RID: 772 RVA: 0x000096E0 File Offset: 0x000078E0
		public CacheDataLossException()
		{
		}

		// Token: 0x06000305 RID: 773 RVA: 0x000096E8 File Offset: 0x000078E8
		public CacheDataLossException(string message) : base(message)
		{
		}

		// Token: 0x06000306 RID: 774 RVA: 0x000096F1 File Offset: 0x000078F1
		public CacheDataLossException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000307 RID: 775 RVA: 0x000096FB File Offset: 0x000078FB
		protected CacheDataLossException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

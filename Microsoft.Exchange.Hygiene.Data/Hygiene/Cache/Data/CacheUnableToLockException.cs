using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Hygiene.Cache.Data
{
	// Token: 0x0200005A RID: 90
	[Serializable]
	internal class CacheUnableToLockException : HygieneCacheException
	{
		// Token: 0x0600037F RID: 895 RVA: 0x0000A326 File Offset: 0x00008526
		public CacheUnableToLockException()
		{
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000A32E File Offset: 0x0000852E
		public CacheUnableToLockException(string message) : base(message)
		{
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000A337 File Offset: 0x00008537
		public CacheUnableToLockException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000A341 File Offset: 0x00008541
		protected CacheUnableToLockException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

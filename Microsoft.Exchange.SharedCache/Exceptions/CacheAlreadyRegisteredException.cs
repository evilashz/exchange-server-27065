using System;

namespace Microsoft.Exchange.SharedCache.Exceptions
{
	// Token: 0x02000018 RID: 24
	public class CacheAlreadyRegisteredException : SharedCacheExceptionBase
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x00003D64 File Offset: 0x00001F64
		public CacheAlreadyRegisteredException(Guid cacheGuid) : base("Cache guid " + cacheGuid.ToString() + " already registered.")
		{
		}
	}
}

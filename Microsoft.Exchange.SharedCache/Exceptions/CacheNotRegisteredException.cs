using System;
using Microsoft.Exchange.Rpc.SharedCache;

namespace Microsoft.Exchange.SharedCache.Exceptions
{
	// Token: 0x02000019 RID: 25
	public class CacheNotRegisteredException : SharedCacheExceptionBase
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x00003D88 File Offset: 0x00001F88
		public CacheNotRegisteredException(Guid cacheGuid) : base(ResponseCode.CacheGuidNotFound, "Cache guid " + cacheGuid.ToString() + " not found.")
		{
		}
	}
}

using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.SharedCache.Client
{
	// Token: 0x02000006 RID: 6
	public static class SerializationHelper
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002190 File Offset: 0x00000390
		public static T Deserialize<T>(byte[] serializedBytes) where T : ISharedCacheEntry, new()
		{
			ArgumentValidator.ThrowIfNull("serializedBytes", serializedBytes);
			T result = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
			result.FromByteArray(serializedBytes);
			return result;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021D8 File Offset: 0x000003D8
		public static byte[] Serialize(ISharedCacheEntry serializableObject)
		{
			ArgumentValidator.ThrowIfNull("serializableObject", serializableObject);
			return serializableObject.ToByteArray();
		}
	}
}

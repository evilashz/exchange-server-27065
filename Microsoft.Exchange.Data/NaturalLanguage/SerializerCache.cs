using System;
using System.Collections.Concurrent;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.NaturalLanguage
{
	// Token: 0x02000062 RID: 98
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class SerializerCache
	{
		// Token: 0x0600032E RID: 814 RVA: 0x0000DA7A File Offset: 0x0000BC7A
		public static XmlSerializer GetSerializer(Type type)
		{
			return SerializerCache.cache.GetOrAdd(type, (Type t) => new XmlSerializer(t));
		}

		// Token: 0x04000143 RID: 323
		private static ConcurrentDictionary<Type, XmlSerializer> cache = new ConcurrentDictionary<Type, XmlSerializer>();
	}
}

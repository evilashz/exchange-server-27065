using System;
using System.Collections;

namespace System.Runtime.Serialization
{
	// Token: 0x0200072B RID: 1835
	internal static class SerializationEventsCache
	{
		// Token: 0x060051C8 RID: 20936 RVA: 0x0011ED28 File Offset: 0x0011CF28
		internal static SerializationEvents GetSerializationEventsForType(Type t)
		{
			SerializationEvents serializationEvents;
			if ((serializationEvents = (SerializationEvents)SerializationEventsCache.cache[t]) == null)
			{
				object syncRoot = SerializationEventsCache.cache.SyncRoot;
				lock (syncRoot)
				{
					if ((serializationEvents = (SerializationEvents)SerializationEventsCache.cache[t]) == null)
					{
						serializationEvents = new SerializationEvents(t);
						SerializationEventsCache.cache[t] = serializationEvents;
					}
				}
			}
			return serializationEvents;
		}

		// Token: 0x04002409 RID: 9225
		private static Hashtable cache = new Hashtable();
	}
}

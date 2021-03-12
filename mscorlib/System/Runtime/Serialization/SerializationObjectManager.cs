using System;
using System.Collections;
using System.Security;

namespace System.Runtime.Serialization
{
	// Token: 0x02000729 RID: 1833
	public sealed class SerializationObjectManager
	{
		// Token: 0x060051BC RID: 20924 RVA: 0x0011E88E File Offset: 0x0011CA8E
		public SerializationObjectManager(StreamingContext context)
		{
			this.m_context = context;
			this.m_objectSeenTable = new Hashtable();
		}

		// Token: 0x060051BD RID: 20925 RVA: 0x0011E8B4 File Offset: 0x0011CAB4
		[SecurityCritical]
		public void RegisterObject(object obj)
		{
			SerializationEvents serializationEventsForType = SerializationEventsCache.GetSerializationEventsForType(obj.GetType());
			if (serializationEventsForType.HasOnSerializingEvents && this.m_objectSeenTable[obj] == null)
			{
				this.m_objectSeenTable[obj] = true;
				serializationEventsForType.InvokeOnSerializing(obj, this.m_context);
				this.AddOnSerialized(obj);
			}
		}

		// Token: 0x060051BE RID: 20926 RVA: 0x0011E909 File Offset: 0x0011CB09
		public void RaiseOnSerializedEvent()
		{
			if (this.m_onSerializedHandler != null)
			{
				this.m_onSerializedHandler(this.m_context);
			}
		}

		// Token: 0x060051BF RID: 20927 RVA: 0x0011E924 File Offset: 0x0011CB24
		[SecuritySafeCritical]
		private void AddOnSerialized(object obj)
		{
			SerializationEvents serializationEventsForType = SerializationEventsCache.GetSerializationEventsForType(obj.GetType());
			this.m_onSerializedHandler = serializationEventsForType.AddOnSerialized(obj, this.m_onSerializedHandler);
		}

		// Token: 0x04002402 RID: 9218
		private Hashtable m_objectSeenTable = new Hashtable();

		// Token: 0x04002403 RID: 9219
		private SerializationEventHandler m_onSerializedHandler;

		// Token: 0x04002404 RID: 9220
		private StreamingContext m_context;
	}
}

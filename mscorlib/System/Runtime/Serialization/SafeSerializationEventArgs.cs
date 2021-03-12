using System;
using System.Collections.Generic;

namespace System.Runtime.Serialization
{
	// Token: 0x02000726 RID: 1830
	public sealed class SafeSerializationEventArgs : EventArgs
	{
		// Token: 0x060051AD RID: 20909 RVA: 0x0011E532 File Offset: 0x0011C732
		internal SafeSerializationEventArgs(StreamingContext streamingContext)
		{
			this.m_streamingContext = streamingContext;
		}

		// Token: 0x060051AE RID: 20910 RVA: 0x0011E54C File Offset: 0x0011C74C
		public void AddSerializedState(ISafeSerializationData serializedState)
		{
			if (serializedState == null)
			{
				throw new ArgumentNullException("serializedState");
			}
			if (!serializedState.GetType().IsSerializable)
			{
				throw new ArgumentException(Environment.GetResourceString("Serialization_NonSerType", new object[]
				{
					serializedState.GetType(),
					serializedState.GetType().Assembly.FullName
				}));
			}
			this.m_serializedStates.Add(serializedState);
		}

		// Token: 0x17000D9A RID: 3482
		// (get) Token: 0x060051AF RID: 20911 RVA: 0x0011E5B2 File Offset: 0x0011C7B2
		internal IList<object> SerializedStates
		{
			get
			{
				return this.m_serializedStates;
			}
		}

		// Token: 0x17000D9B RID: 3483
		// (get) Token: 0x060051B0 RID: 20912 RVA: 0x0011E5BA File Offset: 0x0011C7BA
		public StreamingContext StreamingContext
		{
			get
			{
				return this.m_streamingContext;
			}
		}

		// Token: 0x040023FA RID: 9210
		private StreamingContext m_streamingContext;

		// Token: 0x040023FB RID: 9211
		private List<object> m_serializedStates = new List<object>();
	}
}

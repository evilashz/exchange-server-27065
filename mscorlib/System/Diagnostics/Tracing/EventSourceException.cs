using System;
using System.Runtime.Serialization;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000406 RID: 1030
	[__DynamicallyInvokable]
	[Serializable]
	public class EventSourceException : Exception
	{
		// Token: 0x0600346C RID: 13420 RVA: 0x000CC740 File Offset: 0x000CA940
		[__DynamicallyInvokable]
		public EventSourceException() : base(Environment.GetResourceString("EventSource_ListenerWriteFailure"))
		{
		}

		// Token: 0x0600346D RID: 13421 RVA: 0x000CC752 File Offset: 0x000CA952
		[__DynamicallyInvokable]
		public EventSourceException(string message) : base(message)
		{
		}

		// Token: 0x0600346E RID: 13422 RVA: 0x000CC75B File Offset: 0x000CA95B
		[__DynamicallyInvokable]
		public EventSourceException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600346F RID: 13423 RVA: 0x000CC765 File Offset: 0x000CA965
		protected EventSourceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06003470 RID: 13424 RVA: 0x000CC76F File Offset: 0x000CA96F
		internal EventSourceException(Exception innerException) : base(Environment.GetResourceString("EventSource_ListenerWriteFailure"), innerException)
		{
		}
	}
}

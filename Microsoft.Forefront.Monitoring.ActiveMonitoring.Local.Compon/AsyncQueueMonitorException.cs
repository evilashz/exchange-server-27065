using System;
using System.Runtime.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x02000011 RID: 17
	[Serializable]
	public class AsyncQueueMonitorException : Exception
	{
		// Token: 0x0600007E RID: 126 RVA: 0x000044C3 File Offset: 0x000026C3
		public AsyncQueueMonitorException()
		{
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000044CB File Offset: 0x000026CB
		public AsyncQueueMonitorException(string message) : base(message)
		{
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000044D4 File Offset: 0x000026D4
		public AsyncQueueMonitorException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000044DE File Offset: 0x000026DE
		protected AsyncQueueMonitorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

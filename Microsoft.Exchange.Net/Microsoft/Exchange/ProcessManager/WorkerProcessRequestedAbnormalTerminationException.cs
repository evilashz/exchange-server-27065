using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.ProcessManager
{
	// Token: 0x02000825 RID: 2085
	[Serializable]
	internal class WorkerProcessRequestedAbnormalTerminationException : Exception
	{
		// Token: 0x06002C36 RID: 11318 RVA: 0x0006466F File Offset: 0x0006286F
		internal WorkerProcessRequestedAbnormalTerminationException(string message) : base(message)
		{
		}

		// Token: 0x06002C37 RID: 11319 RVA: 0x00064678 File Offset: 0x00062878
		public WorkerProcessRequestedAbnormalTerminationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

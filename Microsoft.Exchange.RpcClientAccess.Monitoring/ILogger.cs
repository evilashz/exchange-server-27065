using System;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000041 RID: 65
	internal interface ILogger
	{
		// Token: 0x0600019B RID: 411
		void TaskStarted(ITaskDescriptor task);

		// Token: 0x0600019C RID: 412
		void TaskCompleted(ITaskDescriptor task, TaskResult result);
	}
}

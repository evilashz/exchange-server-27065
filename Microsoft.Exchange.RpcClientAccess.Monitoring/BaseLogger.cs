using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000042 RID: 66
	internal abstract class BaseLogger : ILogger
	{
		// Token: 0x0600019D RID: 413 RVA: 0x00005FC1 File Offset: 0x000041C1
		public BaseLogger()
		{
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600019E RID: 414 RVA: 0x00005FCC File Offset: 0x000041CC
		// (remove) Token: 0x0600019F RID: 415 RVA: 0x00006004 File Offset: 0x00004204
		public event Action<LocalizedString> LogOutput;

		// Token: 0x060001A0 RID: 416
		public abstract void TaskStarted(ITaskDescriptor task);

		// Token: 0x060001A1 RID: 417
		public abstract void TaskCompleted(ITaskDescriptor task, TaskResult result);

		// Token: 0x060001A2 RID: 418 RVA: 0x00006039 File Offset: 0x00004239
		protected virtual void OnLogOutput(LocalizedString output)
		{
			if (this.LogOutput != null)
			{
				this.LogOutput(output);
			}
		}
	}
}

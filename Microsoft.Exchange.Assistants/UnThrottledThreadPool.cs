using System;
using System.Threading;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000091 RID: 145
	internal sealed class UnThrottledThreadPool : IThreadPool
	{
		// Token: 0x06000471 RID: 1137 RVA: 0x00017B12 File Offset: 0x00015D12
		public void QueueUserWorkItem(WaitCallback waitCallback, object state)
		{
			ThreadPool.QueueUserWorkItem(waitCallback, state);
		}
	}
}

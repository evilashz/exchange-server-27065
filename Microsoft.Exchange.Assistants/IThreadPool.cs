using System;
using System.Threading;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000064 RID: 100
	internal interface IThreadPool
	{
		// Token: 0x060002D6 RID: 726
		void QueueUserWorkItem(WaitCallback callback, object state);
	}
}

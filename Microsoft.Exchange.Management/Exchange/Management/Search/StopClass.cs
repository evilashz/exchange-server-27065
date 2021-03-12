using System;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Search
{
	// Token: 0x02000162 RID: 354
	internal class StopClass
	{
		// Token: 0x06000CE5 RID: 3301 RVA: 0x0003AF98 File Offset: 0x00039198
		internal void SetStop()
		{
			lock (this)
			{
				TestSearch.TestSearchTracer.TraceDebug((long)this.GetHashCode(), "SetStop is called");
				this.shouldStop = true;
			}
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x0003AFEC File Offset: 0x000391EC
		internal void CheckStop()
		{
			lock (this)
			{
				if (this.shouldStop)
				{
					throw new TestSearchOperationAbortedException();
				}
			}
		}

		// Token: 0x04000643 RID: 1603
		private bool shouldStop;
	}
}

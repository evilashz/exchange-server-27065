using System;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring.Local
{
	// Token: 0x0200007B RID: 123
	internal interface IIndex<TItem>
	{
		// Token: 0x060006CF RID: 1743
		void Add(TItem item, TracingContext traceContext);
	}
}

using System;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x02000012 RID: 18
	public interface IWorkBrokerBase
	{
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001D0 RID: 464
		TimeSpan DefaultResultWindow { get; }

		// Token: 0x060001D1 RID: 465
		bool IsLocal();
	}
}

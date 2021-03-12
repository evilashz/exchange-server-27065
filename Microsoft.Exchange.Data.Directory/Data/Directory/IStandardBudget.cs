using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009BE RID: 2494
	internal interface IStandardBudget : IBudget, IDisposable
	{
		// Token: 0x060073B4 RID: 29620
		CostHandle StartConnection(string callerInfo);

		// Token: 0x060073B5 RID: 29621
		void EndConnection();
	}
}

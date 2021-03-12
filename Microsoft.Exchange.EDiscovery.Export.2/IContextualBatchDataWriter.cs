using System;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x0200000D RID: 13
	internal interface IContextualBatchDataWriter<T> : IBatchDataWriter<T>, IDisposable
	{
		// Token: 0x0600004A RID: 74
		void EnterDataContext(DataContext dataContext);

		// Token: 0x0600004B RID: 75
		void ExitDataContext(bool errorHappened);

		// Token: 0x0600004C RID: 76
		void ExitPFDataContext(bool errorHappened);
	}
}

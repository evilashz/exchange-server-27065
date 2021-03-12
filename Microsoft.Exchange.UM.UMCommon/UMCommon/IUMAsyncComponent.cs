using System;
using System.Threading;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020000B5 RID: 181
	internal interface IUMAsyncComponent
	{
		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000661 RID: 1633
		AutoResetEvent StoppedEvent { get; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000662 RID: 1634
		bool IsInitialized { get; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000663 RID: 1635
		string Name { get; }

		// Token: 0x06000664 RID: 1636
		void StartNow(StartupStage stage);

		// Token: 0x06000665 RID: 1637
		void StopAsync();

		// Token: 0x06000666 RID: 1638
		void CleanupAfterStopped();
	}
}

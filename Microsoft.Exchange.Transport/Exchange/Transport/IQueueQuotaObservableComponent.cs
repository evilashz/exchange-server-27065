using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000325 RID: 805
	internal interface IQueueQuotaObservableComponent
	{
		// Token: 0x14000015 RID: 21
		// (add) Token: 0x060022C5 RID: 8901
		// (remove) Token: 0x060022C6 RID: 8902
		event Action<TransportMailItem> OnAcquire;

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x060022C7 RID: 8903
		// (remove) Token: 0x060022C8 RID: 8904
		event Action<TransportMailItem> OnRelease;
	}
}

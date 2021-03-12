using System;

namespace Microsoft.Exchange.Diagnostics.WorkloadManagement
{
	// Token: 0x020001EC RID: 492
	internal interface IContextPlugin
	{
		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000DF8 RID: 3576
		// (set) Token: 0x06000DF9 RID: 3577
		Guid? LocalId { get; set; }

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000DFA RID: 3578
		bool IsContextPresent { get; }

		// Token: 0x06000DFB RID: 3579
		void SetId();

		// Token: 0x06000DFC RID: 3580
		bool CheckId();

		// Token: 0x06000DFD RID: 3581
		void Clear();
	}
}

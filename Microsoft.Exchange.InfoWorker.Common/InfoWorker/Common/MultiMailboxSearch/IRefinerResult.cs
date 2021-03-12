using System;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x02000219 RID: 537
	internal interface IRefinerResult
	{
		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000EAA RID: 3754
		string Value { get; }

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000EAB RID: 3755
		long Count { get; }

		// Token: 0x06000EAC RID: 3756
		void Merge(IRefinerResult result);
	}
}

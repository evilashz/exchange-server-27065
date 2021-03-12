using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200078A RID: 1930
	public interface IStoreObjectChange
	{
		// Token: 0x17000DA8 RID: 3496
		// (get) Token: 0x060039A1 RID: 14753
		// (set) Token: 0x060039A2 RID: 14754
		PropertyUpdate[] PropertyUpdates { get; set; }
	}
}

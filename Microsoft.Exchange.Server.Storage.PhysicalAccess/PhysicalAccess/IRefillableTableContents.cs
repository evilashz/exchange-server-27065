using System;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x0200005B RID: 91
	public interface IRefillableTableContents
	{
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060003E8 RID: 1000
		bool CanRefill { get; }

		// Token: 0x060003E9 RID: 1001
		void MarkChunkConsumed();
	}
}

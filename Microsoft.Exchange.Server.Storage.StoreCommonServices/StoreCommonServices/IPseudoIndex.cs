using System;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000FC RID: 252
	public interface IPseudoIndex : IIndex
	{
		// Token: 0x17000287 RID: 647
		// (get) Token: 0x060009E7 RID: 2535
		bool ShouldBeCurrent { get; }

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x060009E8 RID: 2536
		object[] IndexTableFunctionParameters { get; }

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x060009E9 RID: 2537
		int RedundantKeyColumnsCount { get; }

		// Token: 0x060009EA RID: 2538
		CategorizedTableParams GetCategorizedTableParams(Context context);
	}
}

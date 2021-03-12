using System;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200002F RID: 47
	public interface IStateObject
	{
		// Token: 0x060001D7 RID: 471
		void OnBeforeCommit(Context context);

		// Token: 0x060001D8 RID: 472
		void OnCommit(Context context);

		// Token: 0x060001D9 RID: 473
		void OnAbort(Context context);
	}
}

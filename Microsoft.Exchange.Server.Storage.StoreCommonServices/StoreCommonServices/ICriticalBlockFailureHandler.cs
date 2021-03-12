using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200002E RID: 46
	public interface ICriticalBlockFailureHandler
	{
		// Token: 0x060001D6 RID: 470
		void OnCriticalBlockFailed(LID lid, Context context, CriticalBlockScope criticalBlockScope);
	}
}

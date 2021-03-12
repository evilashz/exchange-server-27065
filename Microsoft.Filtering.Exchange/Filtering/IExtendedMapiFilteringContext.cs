using System;
using Microsoft.Exchange.UnifiedContent.Exchange;

namespace Microsoft.Filtering
{
	// Token: 0x02000013 RID: 19
	internal interface IExtendedMapiFilteringContext : IMapiFilteringContext
	{
		// Token: 0x06000046 RID: 70
		void SetFipsRecoveryOptions(RecoveryOptions options);

		// Token: 0x06000047 RID: 71
		RecoveryOptions GetFipsRecoveryOptions();
	}
}

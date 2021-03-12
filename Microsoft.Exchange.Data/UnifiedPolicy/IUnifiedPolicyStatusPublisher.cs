using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.UnifiedPolicy
{
	// Token: 0x020002B7 RID: 695
	internal interface IUnifiedPolicyStatusPublisher
	{
		// Token: 0x06001914 RID: 6420
		void PublishStatus(IEnumerable<object> statuses, bool deleteConfiguration);
	}
}

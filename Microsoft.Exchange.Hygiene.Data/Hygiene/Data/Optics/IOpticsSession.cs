using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Hygiene.Data.Optics
{
	// Token: 0x020001B3 RID: 435
	internal interface IOpticsSession
	{
		// Token: 0x06001240 RID: 4672
		IEnumerable<ReputationQueryResult> Query(IEnumerable<ReputationQueryInput> queryInputs);
	}
}

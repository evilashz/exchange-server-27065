using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Servicelets.DiagnosticsAggregation
{
	// Token: 0x0200000A RID: 10
	internal interface IGroupQueuesDataProvider
	{
		// Token: 0x0600004C RID: 76
		void Start();

		// Token: 0x0600004D RID: 77
		void Stop();

		// Token: 0x0600004E RID: 78
		IDictionary<ADObjectId, ServerQueuesSnapshot> GetCurrentGroupServerToQueuesMap();
	}
}

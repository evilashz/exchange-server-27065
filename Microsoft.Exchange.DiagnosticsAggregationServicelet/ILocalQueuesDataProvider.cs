using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Servicelets.DiagnosticsAggregation
{
	// Token: 0x0200000B RID: 11
	internal interface ILocalQueuesDataProvider
	{
		// Token: 0x0600004F RID: 79
		void Start();

		// Token: 0x06000050 RID: 80
		void Stop();

		// Token: 0x06000051 RID: 81
		ADObjectId GetLocalServerId();

		// Token: 0x06000052 RID: 82
		ServerQueuesSnapshot GetLocalServerQueues();
	}
}

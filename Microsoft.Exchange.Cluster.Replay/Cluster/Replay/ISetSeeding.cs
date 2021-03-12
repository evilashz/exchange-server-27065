using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200010D RID: 269
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ISetSeeding : IGetStatus
	{
		// Token: 0x06000A68 RID: 2664
		void CheckReseedBlocked();

		// Token: 0x06000A69 RID: 2665
		bool TryBeginDbSeed(RpcSeederArgs rpcSeederArgs);

		// Token: 0x06000A6A RID: 2666
		void EndDbSeed();

		// Token: 0x06000A6B RID: 2667
		void FailedDbSeed(ExEventLog.EventTuple errorEventTuple, LocalizedString errorMessage, ExtendedErrorInfo errorInfo);
	}
}

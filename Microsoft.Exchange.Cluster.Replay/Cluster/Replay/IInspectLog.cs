using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002DB RID: 731
	internal interface IInspectLog
	{
		// Token: 0x06001D08 RID: 7432
		bool InspectLog(long logfileNumber, bool fRecopyOnFailure);
	}
}

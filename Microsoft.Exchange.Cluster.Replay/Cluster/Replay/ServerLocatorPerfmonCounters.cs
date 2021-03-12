using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000335 RID: 821
	internal sealed class ServerLocatorPerfmonCounters
	{
		// Token: 0x06002172 RID: 8562 RVA: 0x0009B73C File Offset: 0x0009993C
		public void RecordOneWCFCall()
		{
			ReplayServerPerfmon.WCFGetServerForDatabaseCalls.Increment();
			ReplayServerPerfmon.WCFGetServerForDatabaseCallsPerSec.Increment();
		}

		// Token: 0x06002173 RID: 8563 RVA: 0x0009B754 File Offset: 0x00099954
		public void RecordOneWCFGetAllCall()
		{
			ReplayServerPerfmon.WCFGetAllCalls.Increment();
			ReplayServerPerfmon.WCFGetAllCallsPerSec.Increment();
		}

		// Token: 0x06002174 RID: 8564 RVA: 0x0009B76C File Offset: 0x0009996C
		public void RecordOneWCFCallError()
		{
			ReplayServerPerfmon.WCFGetServerForDatabaseCallErrors.Increment();
			ReplayServerPerfmon.WCFGetServerForDatabaseCallErrorsPerSec.Increment();
		}

		// Token: 0x06002175 RID: 8565 RVA: 0x0009B784 File Offset: 0x00099984
		public void RecordWCFCallLatency(long tics)
		{
			ReplayServerPerfmon.AvgWCFCallLatency.IncrementBy(tics);
			ReplayServerPerfmon.AvgWCFCallLatencyBase.Increment();
		}

		// Token: 0x06002176 RID: 8566 RVA: 0x0009B79D File Offset: 0x0009999D
		public void RecordWCFGetAllCallLatency(long tics)
		{
			ReplayServerPerfmon.AvgWCFGetAllCallLatency.IncrementBy(tics);
			ReplayServerPerfmon.AvgWCFGetAllCallLatencyBase.Increment();
		}
	}
}

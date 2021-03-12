using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Monitoring
{
	// Token: 0x0200027A RID: 634
	internal class PersistentStateDiscovery : MaintenanceWorkItem
	{
		// Token: 0x060011E5 RID: 4581 RVA: 0x0007880C File Offset: 0x00076A0C
		protected override void DoWork(CancellationToken cancellationToken)
		{
			WTFDiagnostics.TraceInformation<DateTime>(ExTraceGlobals.PersistentStateTracer, base.TraceContext, "[PersistentStateDiscovery]: DoWork start, currentTime={0}", DateTime.UtcNow, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PersistentState\\PersistentStateDiscovery.cs", 28);
			try
			{
				LocalDataAccess.WriteAllPersistentResults(cancellationToken);
				WTFDiagnostics.TraceInformation<DateTime>(ExTraceGlobals.PersistentStateTracer, base.TraceContext, "[PersistentStateDiscovery]: DoWork end, currentTime={0}", DateTime.UtcNow, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PersistentState\\PersistentStateDiscovery.cs", 34);
			}
			catch (Exception arg)
			{
				WTFDiagnostics.TraceInformation<Exception>(ExTraceGlobals.PersistentStateTracer, base.TraceContext, "[PersistentStateDiscovery]: Dowork failed to write PersistentState.\n{0}", arg, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PersistentState\\PersistentStateDiscovery.cs", 38);
			}
		}
	}
}

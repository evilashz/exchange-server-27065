using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.UnifiedPolicy
{
	// Token: 0x0200053A RID: 1338
	public sealed class UnifiedPolicySync : MaintenanceWorkItem
	{
		// Token: 0x060020CD RID: 8397 RVA: 0x000C7F54 File Offset: 0x000C6154
		protected override void DoWork(CancellationToken cancellationToken)
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.TransportTracer, base.TraceContext, "UnifiedPolicySync.DoWork", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\UnifiedPolicy\\UnifiedPolicySync.cs", 29);
			if (!LocalEndpointManager.Instance.ExchangeServerRoleEndpoint.IsMailboxRoleInstalled)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.TransportTracer, base.TraceContext, "UnifiedPolicySync.DoWork(): Mailbox role not installed. Skip.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\UnifiedPolicy\\UnifiedPolicySync.cs", 33);
				base.Result.StateAttribute1 = "UnifiedPolicySync: Mailbox role not installed. Skip.";
				return;
			}
			GenericWorkItemHelper.CreateAllDefinitions(new List<string>
			{
				"UnifiedPolicySyncLAM.xml"
			}, base.Broker, base.TraceContext, base.Result);
		}
	}
}

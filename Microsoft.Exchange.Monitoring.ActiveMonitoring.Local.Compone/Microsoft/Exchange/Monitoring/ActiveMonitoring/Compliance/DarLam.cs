using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Compliance
{
	// Token: 0x02000134 RID: 308
	public sealed class DarLam : MaintenanceWorkItem
	{
		// Token: 0x06000909 RID: 2313 RVA: 0x00034990 File Offset: 0x00032B90
		protected override void DoWork(CancellationToken cancellationToken)
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.TransportTracer, base.TraceContext, "DarLam.DoWork", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Compliance\\DarLam.cs", 29);
			if (!LocalEndpointManager.Instance.ExchangeServerRoleEndpoint.IsMailboxRoleInstalled)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.TransportTracer, base.TraceContext, "DarLam.DoWork(): Mailbox role not installed. Skip.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Compliance\\DarLam.cs", 33);
				base.Result.StateAttribute1 = "DarLam: Mailbox role not installed. Skip.";
				return;
			}
			GenericWorkItemHelper.CreateAllDefinitions(new List<string>
			{
				"DarLam.xml"
			}, base.Broker, base.TraceContext, base.Result);
		}
	}
}

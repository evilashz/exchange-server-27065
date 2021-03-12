﻿using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x02000267 RID: 615
	public sealed class FrontendDiscovery : MaintenanceWorkItem
	{
		// Token: 0x0600146C RID: 5228 RVA: 0x0003C614 File Offset: 0x0003A814
		protected override void DoWork(CancellationToken cancellationToken)
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.TransportTracer, base.TraceContext, "FrontendDiscovery.DoWork", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Transport\\Discovery\\FrontEndDiscovery.cs", 31);
			if (!DiscoveryUtils.IsFrontendTransportRoleInstalled())
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.TransportTracer, base.TraceContext, "FrontendDiscovery.DoWork(): Frontend role not installed. Skip.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Transport\\Discovery\\FrontEndDiscovery.cs", 35);
				base.Result.StateAttribute1 = "FrontendDiscovery: Frontend role not installed. Skip.";
				return;
			}
			GenericWorkItemHelper.CreateAllDefinitions(new List<string>
			{
				"Core_Frontend.xml",
				"SmtpProbes_Frontend.xml"
			}, base.Broker, base.TraceContext, base.Result);
		}
	}
}

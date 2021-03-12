using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Classification
{
	// Token: 0x02000513 RID: 1299
	public sealed class ClassificationDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06001FF8 RID: 8184 RVA: 0x000C32FC File Offset: 0x000C14FC
		protected override void DoWork(CancellationToken cancellationToken)
		{
			LinkedList<string> linkedList = new LinkedList<string>();
			LocalEndpointManager instance = LocalEndpointManager.Instance;
			if (instance.ExchangeServerRoleEndpoint != null)
			{
				if (instance.ExchangeServerRoleEndpoint.IsBridgeheadRoleInstalled)
				{
					linkedList.AddLast("Classification.xml");
				}
				else
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.ClassificationTracer, base.TraceContext, "[ClassificationDiscovery.DoWork]: Bridgehead role is not installed on this server.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Classification\\ClassificationDiscovery.cs", 41);
				}
				if (instance.ExchangeServerRoleEndpoint.IsAdminToolsRoleInstalled)
				{
					linkedList.AddLast("ClassificationManagement.xml");
				}
				else
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.ClassificationTracer, base.TraceContext, "[ClassificationDiscovery.DoWork]: Admin Tools role is not installed on this server.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Classification\\ClassificationDiscovery.cs", 53);
				}
			}
			if (linkedList.Count > 0)
			{
				linkedList.AddFirst("ClassificationMaintenanceDefinition.xml");
				GenericWorkItemHelper.CreateAllDefinitions(linkedList, base.Broker, base.TraceContext, base.Result);
				return;
			}
			base.Result.StateAttribute1 = "ClassificationDiscovery: neither Bridgehead nor Admin Tools role is not installed on this server.";
		}
	}
}

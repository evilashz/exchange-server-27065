using System;
using System.Threading;
using System.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x02000207 RID: 519
	internal sealed class FfoRwsDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06000FED RID: 4077 RVA: 0x0002AC18 File Offset: 0x00028E18
		protected override void DoWork(CancellationToken cancellationToken)
		{
			try
			{
				ExchangeServerRoleEndpoint exchangeServerRoleEndpoint = LocalEndpointManager.Instance.ExchangeServerRoleEndpoint;
				if (exchangeServerRoleEndpoint == null || (!FfoRwsDiscovery.SupportFfoReportingCmdlets(exchangeServerRoleEndpoint) && !FfoRwsDiscovery.SupportFfoReportingWebService(exchangeServerRoleEndpoint)))
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.RWSTracer, FfoRwsDiscovery.traceContext, "[FFO RwsDiscovery.DoWork]: This server does not support FFO Reporting cmdlets or FFO Reporting WS.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Rws\\FFORwsDiscovery.cs", 47);
					base.Result.StateAttribute1 = "FFO RwsDiscovery: This server does not support FFO Reporting cmdlets or FFO Reporting WS.";
				}
				else
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.RWSTracer, FfoRwsDiscovery.traceContext, "[FFO RwsDiscovery.DoWork]: Discovery Started.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Rws\\FFORwsDiscovery.cs", 57);
					XmlNode definitionNode = GenericWorkItemHelper.GetDefinitionNode("FfoRws.xml", FfoRwsDiscovery.traceContext);
					GenericWorkItemHelper.CreatePerfCounterDefinitions(definitionNode, base.Broker, FfoRwsDiscovery.traceContext, base.Result);
					GenericWorkItemHelper.CreateNTEventDefinitions(definitionNode, base.Broker, FfoRwsDiscovery.traceContext, base.Result);
					if (FfoRwsDiscovery.SupportFfoReportingWebService(exchangeServerRoleEndpoint))
					{
						GenericWorkItemHelper.CreateCustomDefinitions(definitionNode, base.Broker, FfoRwsDiscovery.traceContext, base.Result);
					}
					else
					{
						WTFDiagnostics.TraceInformation(ExTraceGlobals.RWSTracer, FfoRwsDiscovery.traceContext, "[FFO RwsDiscovery.DoWork]: Custom Probes/Monitors/Responders are not generated. This server does not support FFO Reporting WS.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Rws\\FFORwsDiscovery.cs", 80);
					}
					GenericWorkItemHelper.CompleteDiscovery(FfoRwsDiscovery.traceContext);
					WTFDiagnostics.TraceInformation(ExTraceGlobals.RWSTracer, FfoRwsDiscovery.traceContext, "[FFO RwsDiscovery.DoWork]: Discovery Completed.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Rws\\FFORwsDiscovery.cs", 89);
				}
			}
			catch (Exception ex)
			{
				WTFDiagnostics.TraceError<string>(ExTraceGlobals.RWSTracer, FfoRwsDiscovery.traceContext, "[FFO RwsDiscovery.DoWork]: Exception occurred during discovery. {0}", ex.ToString(), null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Rws\\FFORwsDiscovery.cs", 96);
				throw;
			}
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x0002AD90 File Offset: 0x00028F90
		private static bool SupportFfoReportingWebService(ExchangeServerRoleEndpoint exchangeEndpoint)
		{
			if (!DatacenterRegistry.IsForefrontForOffice())
			{
				return exchangeEndpoint.IsClientAccessRoleInstalled;
			}
			return FfoLocalEndpointManager.IsWebServiceInstalled;
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x0002ADA5 File Offset: 0x00028FA5
		private static bool SupportFfoReportingCmdlets(ExchangeServerRoleEndpoint exchangeEndpoint)
		{
			return exchangeEndpoint.IsCafeRoleInstalled;
		}

		// Token: 0x040007A9 RID: 1961
		private static TracingContext traceContext = new TracingContext();
	}
}

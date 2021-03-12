using System;
using System.Threading;
using Microsoft.Exchange.Monitoring.ActiveMonitoring;
using Microsoft.Exchange.Net;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x0200009B RID: 155
	public sealed class SmartAlertsConnectivityDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06000432 RID: 1074 RVA: 0x0001A8DC File Offset: 0x00018ADC
		protected override void DoWork(CancellationToken cancellationToken)
		{
			if (string.IsNullOrEmpty(ComputerInformation.DnsPhysicalDomainName))
			{
				base.Result.StateAttribute1 = "SmartAlertsConnectivityDiscovery: Installation quits due to invalid forest information(i.e. ComputerInformation.DnsPhysicalDomainName is either null or empty).";
				return;
			}
			if (ComputerInformation.DnsPhysicalDomainName.ToLower().Contains(".extest.microsoft.com"))
			{
				base.Result.StateAttribute1 = string.Format("SmartAlertsConnectivityDiscovery: Probe doesn't deploy in TDS domain '{0}'. ", ComputerInformation.DnsPhysicalDomainName);
				return;
			}
			bool isBackgroundRoleInstalled = FfoLocalEndpointManager.IsBackgroundRoleInstalled;
			if (isBackgroundRoleInstalled)
			{
				GenericWorkItemHelper.CreateAllDefinitions(new string[]
				{
					"SmartAlertsConnectivity.xml"
				}, base.Broker, base.TraceContext, base.Result);
				return;
			}
			base.Result.StateAttribute1 = "SmartAlertsConnectivityDiscovery: BGD role is not installed on this server.";
		}
	}
}

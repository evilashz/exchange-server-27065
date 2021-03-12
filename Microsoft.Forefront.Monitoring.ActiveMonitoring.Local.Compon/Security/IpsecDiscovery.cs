using System;
using System.Threading;
using Microsoft.Exchange.Monitoring.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Security
{
	// Token: 0x02000208 RID: 520
	public sealed class IpsecDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06000FF2 RID: 4082 RVA: 0x0002ADC4 File Offset: 0x00028FC4
		protected override void DoWork(CancellationToken cancellationToken)
		{
			bool flag = FfoLocalEndpointManager.IsForefrontForOfficeDatacenter && (FfoLocalEndpointManager.IsBackgroundRoleInstalled || FfoLocalEndpointManager.IsDomainNameServerRoleInstalled || FfoLocalEndpointManager.IsFrontendTransportRoleInstalled || FfoLocalEndpointManager.IsHubTransportRoleInstalled || FfoLocalEndpointManager.IsWebServiceInstalled);
			if (flag)
			{
				GenericWorkItemHelper.CreateAllDefinitions(new string[]
				{
					"IpsecProbes.xml"
				}, base.Broker, base.TraceContext, base.Result);
			}
		}
	}
}

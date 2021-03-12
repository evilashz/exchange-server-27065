using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.MailFlowTestHelper;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Transport
{
	// Token: 0x020004EA RID: 1258
	public sealed class TransportDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06001F28 RID: 7976 RVA: 0x000BEB50 File Offset: 0x000BCD50
		protected override void DoWork(CancellationToken cancellationToken)
		{
			LocalEndpointManager instance = LocalEndpointManager.Instance;
			if (instance.ExchangeServerRoleEndpoint == null)
			{
				return;
			}
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 47, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Transport\\Discovery\\TransportDiscovery.cs");
			if (topologyConfigurationSession == null)
			{
				return;
			}
			if (!CrossPremiseTestMailFlowHelper.IsCrossPremise(topologyConfigurationSession))
			{
				return;
			}
			if (instance.ExchangeServerRoleEndpoint.IsMailboxRoleInstalled && !Datacenter.IsMicrosoftHostedOnly(true))
			{
				TransportDiscovery.workitems.Add(new TestMailflow());
			}
			TransportDiscovery.workitems.ForEach(delegate(IWorkItem wi)
			{
				wi.Initialize(base.Definition, base.Broker, base.TraceContext);
			});
		}

		// Token: 0x040016CA RID: 5834
		private static List<IWorkItem> workitems = new List<IWorkItem>();
	}
}

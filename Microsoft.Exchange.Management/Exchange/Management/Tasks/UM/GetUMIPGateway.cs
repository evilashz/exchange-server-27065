using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D22 RID: 3362
	[Cmdlet("Get", "UMIPGateway", DefaultParameterSetName = "Identity")]
	public sealed class GetUMIPGateway : GetMultitenancySystemConfigurationObjectTask<UMIPGatewayIdParameter, UMIPGateway>
	{
		// Token: 0x17002803 RID: 10243
		// (get) Token: 0x06008102 RID: 33026 RVA: 0x0020FF30 File Offset: 0x0020E130
		// (set) Token: 0x06008103 RID: 33027 RVA: 0x0020FF38 File Offset: 0x0020E138
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeSimulator
		{
			get
			{
				return this.includeSimulator;
			}
			set
			{
				this.includeSimulator = value;
			}
		}

		// Token: 0x17002804 RID: 10244
		// (get) Token: 0x06008104 RID: 33028 RVA: 0x0020FF41 File Offset: 0x0020E141
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06008105 RID: 33029 RVA: 0x0020FF44 File Offset: 0x0020E144
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter();
			UMIPGateway umipgateway = (UMIPGateway)dataObject;
			this.InitializeForwardingAddress(umipgateway);
			if (this.IncludeSimulator)
			{
				base.WriteResult(dataObject);
			}
			else if (!umipgateway.Simulator)
			{
				base.WriteResult(dataObject);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06008106 RID: 33030 RVA: 0x0020FF90 File Offset: 0x0020E190
		private void InitializeForwardingAddress(UMIPGateway gw)
		{
			if (gw.GlobalCallRoutingScheme == UMGlobalCallRoutingScheme.GatewayGuid)
			{
				Guid guid = gw.Guid;
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 111, "InitializeForwardingAddress", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\um\\GetUMIPGateway.cs");
				Server server = topologyConfigurationSession.FindLocalServer();
				List<UMServer> compatibleUMRpcServers = Utility.GetCompatibleUMRpcServers(server.ServerSite, null, topologyConfigurationSession);
				string text = string.Empty;
				using (List<UMServer>.Enumerator enumerator = compatibleUMRpcServers.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						UMServer umserver = enumerator.Current;
						text = umserver.UMForwardingAddressTemplate;
					}
				}
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), gw.OrganizationId, null, false);
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, sessionSettings, 139, "InitializeForwardingAddress", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\um\\GetUMIPGateway.cs");
				ExchangeConfigurationUnit exchangeConfigurationUnit = tenantOrTopologyConfigurationSession.Read<ExchangeConfigurationUnit>(gw.ConfigurationUnit);
				if (!string.IsNullOrEmpty(text) && exchangeConfigurationUnit != null && !string.IsNullOrEmpty(exchangeConfigurationUnit.ExternalDirectoryOrganizationId))
				{
					gw.ForwardingAddress = string.Format(CultureInfo.InvariantCulture, text, new object[]
					{
						exchangeConfigurationUnit.ExternalDirectoryOrganizationId
					});
				}
			}
		}

		// Token: 0x04003F1A RID: 16154
		private SwitchParameter includeSimulator;
	}
}

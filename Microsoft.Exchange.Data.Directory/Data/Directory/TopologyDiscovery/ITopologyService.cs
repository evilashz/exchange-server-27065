using System;
using System.Collections.Generic;
using System.ServiceModel;
using Microsoft.Exchange.Data.Directory.ExchangeTopology;

namespace Microsoft.Exchange.Data.Directory.TopologyDiscovery
{
	// Token: 0x020006C0 RID: 1728
	[ServiceContract(Namespace = "http://Microsoft.Exchange.Directory.TopologyService", ConfigurationName = "Microsoft.Exchange.Directory.TopologyService.ITopologyService")]
	internal interface ITopologyService
	{
		// Token: 0x06004FC0 RID: 20416
		[OperationContract(Name = "GetExchangeTopology", Action = "http://Microsoft.Exchange.Directory.TopologyService/ITopologyService/GetExchangeTopology", ReplyAction = "http://Microsoft.Exchange.Directory.TopologyService/ITopologyService/GetExchangeTopologyResponse")]
		[FaultContract(typeof(TopologyServiceFault))]
		byte[][] GetExchangeTopology(DateTime currentTopologyTimestamp, ExchangeTopologyScope topologyScope, bool forceRefresh);

		// Token: 0x06004FC1 RID: 20417
		[FaultContract(typeof(TopologyServiceFault))]
		[OperationContract(Name = "GetServiceVersion", Action = "http://Microsoft.Exchange.Directory.TopologyService/ITopologyService/GetServiceVersion", ReplyAction = "http://Microsoft.Exchange.Directory.TopologyService/ITopologyService/GetServiceVersionResponse")]
		ServiceVersion GetServiceVersion();

		// Token: 0x06004FC2 RID: 20418
		[OperationContract(Name = "GetAllTopologyVersions", Action = "http://Microsoft.Exchange.Directory.TopologyService/ITopologyService/GetAllTopologyVersions", ReplyAction = "http://Microsoft.Exchange.Directory.TopologyService/ITopologyService/GetAllTopologyVersionsResponse")]
		[FaultContract(typeof(TopologyServiceFault))]
		List<TopologyVersion> GetAllTopologyVersions();

		// Token: 0x06004FC3 RID: 20419
		[FaultContract(typeof(ArgumentException))]
		[FaultContract(typeof(TopologyServiceFault))]
		[OperationContract(Name = "GetTopologyVersions", Action = "http://Microsoft.Exchange.Directory.TopologyService/ITopologyService/GetTopologyVersions", ReplyAction = "http://Microsoft.Exchange.Directory.TopologyService/ITopologyService/GetTopologyVersionsResponse")]
		[FaultContract(typeof(ArgumentNullException))]
		List<TopologyVersion> GetTopologyVersions(List<string> partitionFqdns);

		// Token: 0x06004FC4 RID: 20420
		[OperationContract(Name = "GetServersForRole", AsyncPattern = true, Action = "http://Microsoft.Exchange.Directory.TopologyService/ITopologyService/GetServersForRole", ReplyAction = "http://Microsoft.Exchange.Directory.TopologyService/ITopologyService/GetServersForRoleResponse")]
		IAsyncResult BeginGetServersForRole(string partitionFqdn, List<string> currentlyUsedServers, ADServerRole role, int serversRequested, bool forestWideAffinityRequested, AsyncCallback callback, object asyncState);

		// Token: 0x06004FC5 RID: 20421
		List<ServerInfo> EndGetServersForRole(IAsyncResult result);

		// Token: 0x06004FC6 RID: 20422
		[OperationContract(Name = "GetServerFromDomainDN", AsyncPattern = true, Action = "http://Microsoft.Exchange.Directory.TopologyService/ITopologyService/GetServerFromDomainDN", ReplyAction = "http://Microsoft.Exchange.Directory.TopologyService/ITopologyService/GetServerFromDomainDNResponse")]
		IAsyncResult BeginGetServerFromDomainDN(string domainDN, AsyncCallback callback, object asyncState);

		// Token: 0x06004FC7 RID: 20423
		ServerInfo EndGetServerFromDomainDN(IAsyncResult result);

		// Token: 0x06004FC8 RID: 20424
		[OperationContract(Name = "SetConfigDC", AsyncPattern = true, Action = "http://Microsoft.Exchange.Directory.TopologyService/ITopologyService/SetConfigDC", ReplyAction = "http://Microsoft.Exchange.Directory.TopologyService/ITopologyService/SetConfigDCResponse")]
		IAsyncResult BeginSetConfigDC(string partitionFqdn, string serverName, AsyncCallback callback, object asyncState);

		// Token: 0x06004FC9 RID: 20425
		void EndSetConfigDC(IAsyncResult result);

		// Token: 0x06004FCA RID: 20426
		[FaultContract(typeof(ArgumentException))]
		[FaultContract(typeof(InvalidOperationException))]
		[OperationContract(Name = "ReportServerDown", Action = "http://Microsoft.Exchange.Directory.TopologyService/ITopologyService/ReportServerDown")]
		[FaultContract(typeof(TopologyServiceFault))]
		[FaultContract(typeof(ArgumentNullException))]
		void ReportServerDown(string partitionFqdn, string serverName, ADServerRole role);
	}
}

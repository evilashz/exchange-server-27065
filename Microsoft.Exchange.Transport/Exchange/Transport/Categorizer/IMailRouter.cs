using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000239 RID: 569
	internal interface IMailRouter
	{
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060018F9 RID: 6393
		// (remove) Token: 0x060018FA RID: 6394
		event RoutingTablesChangedHandler RoutingTablesChanged;

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x060018FB RID: 6395
		IList<RoutingAddress> ExternalPostmasterAddresses { get; }

		// Token: 0x060018FC RID: 6396
		void RouteToMultipleDestinations(TransportMailItem mailItem, TaskContext taskContext);

		// Token: 0x060018FD RID: 6397
		bool TryGetServersForNextHop(NextHopSolutionKey nextHopKey, out IEnumerable<INextHopServer> servers, out SmtpSendConnectorConfig connector);

		// Token: 0x060018FE RID: 6398
		bool TryGetOutboundFrontendServers(out IEnumerable<INextHopServer> servers, out bool externalOutboundFrontendProxyEnabled);

		// Token: 0x060018FF RID: 6399
		void ApplyDelayedFanout(TransportMailItem mailItem);

		// Token: 0x06001900 RID: 6400
		bool TrySelectHubServersForDatabases(IList<ADObjectId> databaseIds, Guid? externalOrganizationId, out IEnumerable<INextHopServer> hubServers);

		// Token: 0x06001901 RID: 6401
		bool TrySelectHubServersUsingDagSelector(Guid externalOrganizationId, out IEnumerable<INextHopServer> hubServers);

		// Token: 0x06001902 RID: 6402
		bool TrySelectHubServersForShadow(ShadowRoutingConfiguration shadowRoutingConfig, out IEnumerable<INextHopServer> hubServers);

		// Token: 0x06001903 RID: 6403
		bool TryGetLocalSendConnector<ConnectorType>(Guid connectorGuid, out ConnectorType connector) where ConnectorType : MailGateway;

		// Token: 0x06001904 RID: 6404
		IList<ConnectorType> GetLocalSendConnectors<ConnectorType>() where ConnectorType : MailGateway;

		// Token: 0x06001905 RID: 6405
		bool IsHubTransportServer(string serverFqdn);

		// Token: 0x06001906 RID: 6406
		bool IsInLocalSite(string serverFqdn);

		// Token: 0x06001907 RID: 6407
		bool TryGetServerFqdnByLegacyDN(string serverLegacyDN, out string serverFqdn);

		// Token: 0x06001908 RID: 6408
		bool TryGetServerLegacyDNByFqdn(string serverFqdn, out string serverLegacyDN);

		// Token: 0x06001909 RID: 6409
		bool TryGetRelatedServersForShadowQueue(NextHopSolutionKey shadowQueueKey, out IEnumerable<INextHopServer> servers);

		// Token: 0x0600190A RID: 6410
		bool IsJournalMessage(IReadOnlyMailItem mailItem);
	}
}

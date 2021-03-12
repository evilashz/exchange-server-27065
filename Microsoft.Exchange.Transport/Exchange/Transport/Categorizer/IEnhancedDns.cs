using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Protocols.Smtp;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000229 RID: 553
	internal interface IEnhancedDns
	{
		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06001852 RID: 6226
		SmtpSendConnectorConfig EnterpriseRelayConnector { get; }

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06001853 RID: 6227
		SmtpSendConnectorConfig ClientProxyConnector { get; }

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06001854 RID: 6228
		// (set) Token: 0x06001855 RID: 6229
		int MaxDataPerRequest { get; set; }

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06001856 RID: 6230
		// (set) Token: 0x06001857 RID: 6231
		DnsServerList ServerList { get; set; }

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001858 RID: 6232
		// (set) Token: 0x06001859 RID: 6233
		IEnumerable<IPAddress> LocalIPAddresses { get; set; }

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x0600185A RID: 6234
		// (set) Token: 0x0600185B RID: 6235
		TimeSpan Timeout { get; set; }

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x0600185C RID: 6236
		// (set) Token: 0x0600185D RID: 6237
		TimeSpan QueryRetryInterval { get; set; }

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x0600185E RID: 6238
		// (set) Token: 0x0600185F RID: 6239
		AddressFamily DefaultAddressFamily { get; set; }

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06001860 RID: 6240
		// (set) Token: 0x06001861 RID: 6241
		DnsQueryOptions Options { get; set; }

		// Token: 0x06001862 RID: 6242
		void SetRunTimeDependencies(IMailRouter router);

		// Token: 0x06001863 RID: 6243
		void Load();

		// Token: 0x06001864 RID: 6244
		void Unload();

		// Token: 0x06001865 RID: 6245
		string OnUnhandledException(Exception e);

		// Token: 0x06001866 RID: 6246
		void FlushCache();

		// Token: 0x06001867 RID: 6247
		IAsyncResult BeginResolveToNextHop(NextHopSolutionKey key, RiskLevel riskLevel, int outboundIPPool, AsyncCallback requestCallback, object stateObject);

		// Token: 0x06001868 RID: 6248
		IAsyncResult BeginResolveProxyNextHop(IEnumerable<INextHopServer> destinations, bool internalDestination, SmtpSendConnectorConfig sendConnector, SmtpOutProxyType proxyType, RiskLevel riskLevel, int outboundIPPool, AsyncCallback requestCallback, object stateObject);

		// Token: 0x06001869 RID: 6249
		void HandleTransportServerConfigChange(TransportServerConfiguration args);

		// Token: 0x0600186A RID: 6250
		string GetDiagnosticComponentName();

		// Token: 0x0600186B RID: 6251
		XElement GetDiagnosticInfo(DiagnosableParameters parameters);

		// Token: 0x0600186C RID: 6252
		void AdapterServerList(Guid adapterGuid);

		// Token: 0x0600186D RID: 6253
		void AdapterServerList(Guid adapterGuid, bool excludeServersFromLoopbackAdapters, bool excludeIPv6SiteLocalAddresses);

		// Token: 0x0600186E RID: 6254
		void InitializeFromMachineServerList();

		// Token: 0x0600186F RID: 6255
		void InitializeServerList(IPAddress[] addresses);

		// Token: 0x06001870 RID: 6256
		IAsyncResult BeginResolveToAddresses(string domainName, AddressFamily type, AsyncCallback requestCallback, object state);

		// Token: 0x06001871 RID: 6257
		IAsyncResult BeginResolveToAddresses(string domainName, AddressFamily type, DnsQueryOptions options, AsyncCallback requestCallback, object state);

		// Token: 0x06001872 RID: 6258
		IAsyncResult BeginResolveToAddresses(string domainName, AddressFamily type, DnsServerList list, DnsQueryOptions options, AsyncCallback requestCallback, object state);

		// Token: 0x06001873 RID: 6259
		IAsyncResult BeginResolveToAddresses(string domainName, DnsServerList list, DnsQueryOptions options, AsyncCallback requestCallback, object state);

		// Token: 0x06001874 RID: 6260
		IAsyncResult BeginResolveToMailServers(string domainName, AsyncCallback requestCallback, object state);

		// Token: 0x06001875 RID: 6261
		IAsyncResult BeginResolveToMailServers(string domainName, DnsQueryOptions options, AsyncCallback requestCallback, object state);

		// Token: 0x06001876 RID: 6262
		IAsyncResult BeginResolveToMailServers(string domainName, DnsServerList list, DnsQueryOptions options, AsyncCallback requestCallback, object state);

		// Token: 0x06001877 RID: 6263
		IAsyncResult BeginResolveToMailServers(string domainName, bool implicitSearch, AddressFamily type, AsyncCallback requestCallback, object state);

		// Token: 0x06001878 RID: 6264
		IAsyncResult BeginRetrieveTextRecords(string domainName, AsyncCallback requestCallback, object state);

		// Token: 0x06001879 RID: 6265
		IAsyncResult BeginRetrieveTextRecords(string domainName, DnsQueryOptions options, AsyncCallback requestCallback, object state);

		// Token: 0x0600187A RID: 6266
		IAsyncResult BeginRetrieveTextRecords(string domainName, DnsServerList list, DnsQueryOptions options, AsyncCallback requestCallback, object state);

		// Token: 0x0600187B RID: 6267
		IAsyncResult BeginRetrieveSoaRecords(string domainName, AsyncCallback requestCallback, object state);

		// Token: 0x0600187C RID: 6268
		IAsyncResult BeginRetrieveSoaRecords(string domainName, DnsQueryOptions options, AsyncCallback requestCallback, object state);

		// Token: 0x0600187D RID: 6269
		IAsyncResult BeginRetrieveSoaRecords(string domainName, DnsServerList list, DnsQueryOptions options, AsyncCallback requestCallback, object state);

		// Token: 0x0600187E RID: 6270
		IAsyncResult BeginRetrieveCNameRecords(string domainName, AsyncCallback requestCallback, object state);

		// Token: 0x0600187F RID: 6271
		IAsyncResult BeginRetrieveCNameRecords(string domainName, DnsQueryOptions options, AsyncCallback requestCallback, object state);

		// Token: 0x06001880 RID: 6272
		IAsyncResult BeginRetrieveCNameRecords(string domainName, DnsServerList list, DnsQueryOptions options, AsyncCallback requestCallback, object state);

		// Token: 0x06001881 RID: 6273
		IAsyncResult BeginResolveToNames(IPAddress address, AsyncCallback requestCallback, object state);

		// Token: 0x06001882 RID: 6274
		IAsyncResult BeginResolveToNames(IPAddress address, DnsQueryOptions options, AsyncCallback requestCallback, object state);

		// Token: 0x06001883 RID: 6275
		IAsyncResult BeginResolveToNames(IPAddress address, DnsServerList list, DnsQueryOptions options, AsyncCallback requestCallback, object state);

		// Token: 0x06001884 RID: 6276
		IAsyncResult BeginRetrieveSrvRecords(string name, DnsQueryOptions options, AsyncCallback requestCallback, object state);

		// Token: 0x06001885 RID: 6277
		IAsyncResult BeginRetrieveNsRecords(string domainName, AsyncCallback requestCallback, object state);

		// Token: 0x06001886 RID: 6278
		IAsyncResult BeginRetrieveNsRecords(string domainName, DnsQueryOptions options, AsyncCallback requestCallback, object state);

		// Token: 0x06001887 RID: 6279
		IAsyncResult BeginRetrieveNsRecords(string domainName, DnsServerList list, DnsQueryOptions options, AsyncCallback requestCallback, object state);
	}
}

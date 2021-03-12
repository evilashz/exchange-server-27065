using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Common;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004B7 RID: 1207
	internal class ReceiveConnectorManager
	{
		// Token: 0x06003677 RID: 13943 RVA: 0x000DEAF0 File Offset: 0x000DCCF0
		public ReceiveConnectorManager(ISmtpReceiveConfiguration configuration)
		{
			ArgumentValidator.ThrowIfNull("configuration", configuration);
			this.role = configuration.TransportConfiguration.ProcessTransportRole;
			this.mailboxDeliveryAcceptAnonymousUsers = configuration.TransportConfiguration.MailboxDeliveryAcceptAnonymousUsers;
			this.minimumAvailabilityConnectionsToMonitor = configuration.TransportConfiguration.SmtpAvailabilityMinConnectionsToMonitor;
		}

		// Token: 0x06003678 RID: 13944 RVA: 0x000DEB80 File Offset: 0x000DCD80
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Connectors: {0}\r\n", string.Join(", ", from c in this.connectorsByName
			select c.Value.Connector.Name));
			stringBuilder.AppendFormat("Bindings: {0}\r\n", string.Join<IPEndPoint>(", ", this.Bindings));
			return stringBuilder.ToString();
		}

		// Token: 0x17001044 RID: 4164
		// (get) Token: 0x06003679 RID: 13945 RVA: 0x000DEBF3 File Offset: 0x000DCDF3
		public List<IPEndPoint> Bindings
		{
			get
			{
				return this.bindings;
			}
		}

		// Token: 0x17001045 RID: 4165
		// (get) Token: 0x0600367A RID: 13946 RVA: 0x000DEBFB File Offset: 0x000DCDFB
		// (set) Token: 0x0600367B RID: 13947 RVA: 0x000DEC03 File Offset: 0x000DCE03
		public InMemoryReceiveConnector DeliveryReceiveConnector { get; private set; }

		// Token: 0x0600367C RID: 13948 RVA: 0x000DEC0C File Offset: 0x000DCE0C
		public void ApplyReceiveConnectors(IEnumerable<ReceiveConnector> connectorsFromActiveDirectoryForAllRoles, Server localServer)
		{
			ArgumentValidator.ThrowIfNull("connectorsFromActiveDirectoryForAllRoles", connectorsFromActiveDirectoryForAllRoles);
			ArgumentValidator.ThrowIfNull("localServer", localServer);
			List<ReceiveConnector> list = Util.EnabledReceiveConnectorsForRole(connectorsFromActiveDirectoryForAllRoles, this.role);
			this.AddRoleSpecificReceiveConnectors(list, localServer);
			SmtpInConnectorMap<SmtpReceiveConnectorStub> smtpInConnectorMap = new SmtpInConnectorMap<SmtpReceiveConnectorStub>();
			Dictionary<string, SmtpReceiveConnectorStub> currentEntries = new Dictionary<string, SmtpReceiveConnectorStub>();
			foreach (ReceiveConnector receiveConnector in list)
			{
				SmtpReceiveConnectorStub smtpReceiveConnectorStub = new SmtpReceiveConnectorStub(receiveConnector, Util.CreateReceivePerfCounters(receiveConnector, this.role), Util.GetOrCreateAvailabilityPerfCounters(this.cachedAvailabilityPerfCounters, receiveConnector, this.role, this.minimumAvailabilityConnectionsToMonitor));
				SmtpReceiveConnectorStub smtpReceiveConnectorStub2;
				if (this.connectorsByName.TryGetValue(receiveConnector.Name, out smtpReceiveConnectorStub2))
				{
					smtpReceiveConnectorStub.ConnectionTable = smtpReceiveConnectorStub2.ConnectionTable;
				}
				smtpInConnectorMap.AddEntry(receiveConnector.Bindings.ToArray(), receiveConnector.RemoteIPRanges.ToArray(), smtpReceiveConnectorStub);
				TransportHelpers.AttemptAddToDictionary<string, SmtpReceiveConnectorStub>(currentEntries, receiveConnector.Name, smtpReceiveConnectorStub, null);
			}
			this.bindings = Util.BindingsFromReceiveConnectors(list, this.role);
			this.connectorMap = smtpInConnectorMap;
			this.connectorsByName = currentEntries;
		}

		// Token: 0x0600367D RID: 13949 RVA: 0x000DED30 File Offset: 0x000DCF30
		public void ApplyLocalServerConfiguration(Server transportServer)
		{
			ArgumentValidator.ThrowIfNull("transportServer", transportServer);
			if (this.DeliveryReceiveConnector != null)
			{
				this.DeliveryReceiveConnector.ApplyLocalServerConfiguration(transportServer);
			}
		}

		// Token: 0x0600367E RID: 13950 RVA: 0x000DED51 File Offset: 0x000DCF51
		public bool TryLookupIncomingConnection(IPEndPoint localEndpoint, IPEndPoint remoteEndpoint, out SmtpReceiveConnectorStub receiveConnectorStub)
		{
			ArgumentValidator.ThrowIfNull("localEndpoint", localEndpoint);
			ArgumentValidator.ThrowIfNull("remoteEndpoint", remoteEndpoint);
			receiveConnectorStub = this.connectorMap.Lookup(localEndpoint.Address, localEndpoint.Port, remoteEndpoint.Address);
			return receiveConnectorStub != null;
		}

		// Token: 0x0600367F RID: 13951 RVA: 0x000DED90 File Offset: 0x000DCF90
		protected virtual InMemoryReceiveConnector CreateDeliveryReceiveConnector(Server localServer)
		{
			return new MailboxDeliveryReceiveConnector(Util.FormatMailboxDeliveryReceiveConnectorName(localServer.Name), localServer, this.mailboxDeliveryAcceptAnonymousUsers);
		}

		// Token: 0x06003680 RID: 13952 RVA: 0x000DEDA9 File Offset: 0x000DCFA9
		private void AddRoleSpecificReceiveConnectors(List<ReceiveConnector> connectors, Server localServer)
		{
			if (this.role == ProcessTransportRole.MailboxDelivery)
			{
				this.DeliveryReceiveConnector = this.CreateDeliveryReceiveConnector(localServer);
				connectors.Add(this.DeliveryReceiveConnector);
			}
		}

		// Token: 0x04001BCD RID: 7117
		private readonly ProcessTransportRole role;

		// Token: 0x04001BCE RID: 7118
		private readonly bool mailboxDeliveryAcceptAnonymousUsers;

		// Token: 0x04001BCF RID: 7119
		private readonly int minimumAvailabilityConnectionsToMonitor;

		// Token: 0x04001BD0 RID: 7120
		private List<IPEndPoint> bindings = new List<IPEndPoint>();

		// Token: 0x04001BD1 RID: 7121
		private SmtpInConnectorMap<SmtpReceiveConnectorStub> connectorMap = new SmtpInConnectorMap<SmtpReceiveConnectorStub>();

		// Token: 0x04001BD2 RID: 7122
		private Dictionary<string, SmtpReceiveConnectorStub> connectorsByName = new Dictionary<string, SmtpReceiveConnectorStub>();

		// Token: 0x04001BD3 RID: 7123
		private readonly ConcurrentDictionary<string, ISmtpAvailabilityPerfCounters> cachedAvailabilityPerfCounters = new ConcurrentDictionary<string, ISmtpAvailabilityPerfCounters>();
	}
}

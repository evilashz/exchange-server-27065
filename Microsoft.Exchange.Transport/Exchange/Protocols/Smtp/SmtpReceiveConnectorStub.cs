using System;
using System.Net;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Transport.Logging;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004C7 RID: 1223
	internal class SmtpReceiveConnectorStub
	{
		// Token: 0x0600384A RID: 14410 RVA: 0x000E7C04 File Offset: 0x000E5E04
		public SmtpReceiveConnectorStub(ReceiveConnector connector, ISmtpReceivePerfCounters receivePerfCounters, ISmtpAvailabilityPerfCounters availabilityPerfCounters)
		{
			ArgumentValidator.ThrowIfNull("connector", connector);
			ArgumentValidator.ThrowIfNull("receivePerfCounters", receivePerfCounters);
			ArgumentValidator.ThrowIfNull("availabilityPerfCounters", availabilityPerfCounters);
			this.connector = connector;
			this.securityDescriptor = connector.GetSecurityDescriptor();
			this.smtpReceivePerfCounterInstance = receivePerfCounters;
			this.smtpAvailabilityPerfCounters = availabilityPerfCounters;
			this.checkMaxInboundConnection = !connector.MaxInboundConnection.IsUnlimited;
			if (this.checkMaxInboundConnection)
			{
				this.maxInboundConnection = connector.MaxInboundConnection.Value;
			}
			this.checkMaxInboundConnectionPerSource = !connector.MaxInboundConnectionPerSource.IsUnlimited;
			if (this.checkMaxInboundConnectionPerSource)
			{
				this.maxInboundConnectionPerSource = connector.MaxInboundConnectionPerSource.Value;
			}
			this.checkMaxInboundConnectionPercentagePerSource = (connector.MaxInboundConnectionPercentagePerSource < 100);
			if (this.checkMaxInboundConnectionPercentagePerSource)
			{
				this.maxInboundConnectionPercentagePerSource = connector.MaxInboundConnectionPercentagePerSource;
				this.maxInboundConnectionPercentagePerSourceFraction = (double)connector.MaxInboundConnectionPercentagePerSource / 100.0;
			}
			this.DetermineCapabilities();
		}

		// Token: 0x170010FD RID: 4349
		// (get) Token: 0x0600384B RID: 14411 RVA: 0x000E7D0B File Offset: 0x000E5F0B
		// (set) Token: 0x0600384C RID: 14412 RVA: 0x000E7D13 File Offset: 0x000E5F13
		public ClientIPTable ConnectionTable
		{
			get
			{
				return this.clientIpTable;
			}
			set
			{
				this.clientIpTable = value;
			}
		}

		// Token: 0x170010FE RID: 4350
		// (get) Token: 0x0600384D RID: 14413 RVA: 0x000E7D1C File Offset: 0x000E5F1C
		public ReceiveConnector Connector
		{
			get
			{
				return this.connector;
			}
		}

		// Token: 0x170010FF RID: 4351
		// (get) Token: 0x0600384E RID: 14414 RVA: 0x000E7D24 File Offset: 0x000E5F24
		public ISmtpReceivePerfCounters SmtpReceivePerfCounterInstance
		{
			get
			{
				return this.smtpReceivePerfCounterInstance;
			}
		}

		// Token: 0x17001100 RID: 4352
		// (get) Token: 0x0600384F RID: 14415 RVA: 0x000E7D2C File Offset: 0x000E5F2C
		public ISmtpAvailabilityPerfCounters SmtpAvailabilityPerfCounters
		{
			get
			{
				return this.smtpAvailabilityPerfCounters;
			}
		}

		// Token: 0x17001101 RID: 4353
		// (get) Token: 0x06003850 RID: 14416 RVA: 0x000E7D34 File Offset: 0x000E5F34
		public RawSecurityDescriptor SecurityDescriptor
		{
			get
			{
				return this.securityDescriptor;
			}
		}

		// Token: 0x17001102 RID: 4354
		// (get) Token: 0x06003851 RID: 14417 RVA: 0x000E7D3C File Offset: 0x000E5F3C
		public SmtpReceiveCapabilities NoTlsCapabilities
		{
			get
			{
				return this.noTlsCapabilities;
			}
		}

		// Token: 0x17001103 RID: 4355
		// (get) Token: 0x06003852 RID: 14418 RVA: 0x000E7D44 File Offset: 0x000E5F44
		public bool ContainsTlsDomainCapabilities
		{
			get
			{
				return this.tlsDomainCapabilities != null;
			}
		}

		// Token: 0x06003853 RID: 14419 RVA: 0x000E7D54 File Offset: 0x000E5F54
		public ClientData AddConnection(IPAddress ipAddress, out bool maxConnectionsExceeded, out bool maxConnectionsPerSourceExceeded)
		{
			int totalConnections;
			ClientData clientData = this.clientIpTable.Add(ipAddress, out totalConnections);
			this.CheckIfConnectionThresholdsExceeded(ipAddress, clientData.Count, totalConnections, out maxConnectionsExceeded, out maxConnectionsPerSourceExceeded);
			return clientData;
		}

		// Token: 0x06003854 RID: 14420 RVA: 0x000E7D84 File Offset: 0x000E5F84
		public ClientData AddConnection(IPAddress ipAddress, ulong significantAddressBytes, out bool maxConnectionsExceeded, out bool maxConnectionsPerSourceExceeded)
		{
			int totalConnections;
			ClientData clientData = this.clientIpTable.Add(ipAddress, significantAddressBytes, out totalConnections);
			this.CheckIfConnectionThresholdsExceeded(ipAddress, clientData.Count, totalConnections, out maxConnectionsExceeded, out maxConnectionsPerSourceExceeded);
			return clientData;
		}

		// Token: 0x06003855 RID: 14421 RVA: 0x000E7DB3 File Offset: 0x000E5FB3
		public void RemoveConnection(IPAddress ip)
		{
			this.clientIpTable.Remove(ip);
		}

		// Token: 0x06003856 RID: 14422 RVA: 0x000E7DC1 File Offset: 0x000E5FC1
		public void RemoveConnection(ulong significantIPAddressBytes)
		{
			this.clientIpTable.Remove(significantIPAddressBytes);
		}

		// Token: 0x06003857 RID: 14423 RVA: 0x000E7DCF File Offset: 0x000E5FCF
		public bool TryGetTlsDomainCapabilities(ICertificateValidator certificateValidator, X509Certificate2 tlsRemoteCertificate, IProtocolLogSession protocolLogSession, out SmtpReceiveDomainCapabilities smtpReceiveDomainCapabilities)
		{
			return this.TryGetTlsDomainCapabilities(certificateValidator, new X509Certificate2Wrapper(tlsRemoteCertificate), protocolLogSession, out smtpReceiveDomainCapabilities);
		}

		// Token: 0x06003858 RID: 14424 RVA: 0x000E7DE4 File Offset: 0x000E5FE4
		public bool TryGetTlsDomainCapabilities(ICertificateValidator certificateValidator, IX509Certificate2 tlsRemoteCertificate, IProtocolLogSession protocolLogSession, out SmtpReceiveDomainCapabilities smtpReceiveDomainCapabilities)
		{
			ArgumentValidator.ThrowIfNull("certificateValidator", certificateValidator);
			ArgumentValidator.ThrowIfNull("tlsRemoteCertificate", tlsRemoteCertificate);
			ArgumentValidator.ThrowIfNull("protocolLogSession", protocolLogSession);
			if (this.tlsDomainCapabilities == null)
			{
				smtpReceiveDomainCapabilities = null;
				return false;
			}
			string text;
			return certificateValidator.FindBestMatchingCertificateFqdn<SmtpReceiveDomainCapabilities>(this.tlsDomainCapabilities, tlsRemoteCertificate, MatchOptions.None, protocolLogSession, out smtpReceiveDomainCapabilities, out text);
		}

		// Token: 0x06003859 RID: 14425 RVA: 0x000E7E34 File Offset: 0x000E6034
		private void DetermineCapabilities()
		{
			foreach (SmtpReceiveDomainCapabilities smtpReceiveDomainCapabilities in this.connector.TlsDomainCapabilities)
			{
				if (smtpReceiveDomainCapabilities.Domain.SmtpDomain == null)
				{
					ExTraceGlobals.SmtpReceiveTracer.TraceError<SmtpReceiveDomainCapabilities, string>((long)this.GetHashCode(), "Ignoring wildcard domain capabilities <{0}> of Receive Connector <{1}>", smtpReceiveDomainCapabilities, this.connector.Name);
				}
				else if (SmtpReceiveConnectorStub.NoTlsDomain.Equals(smtpReceiveDomainCapabilities.Domain.SmtpDomain))
				{
					this.noTlsCapabilities = smtpReceiveDomainCapabilities.Capabilities;
				}
				else if (smtpReceiveDomainCapabilities.Capabilities != SmtpReceiveCapabilities.None)
				{
					if (this.tlsDomainCapabilities == null)
					{
						this.tlsDomainCapabilities = new MatchableDomainMap<Tuple<X500DistinguishedName, SmtpReceiveDomainCapabilities>>(this.connector.TlsDomainCapabilities.Count);
					}
					MatchableDomain domain = new MatchableDomain(smtpReceiveDomainCapabilities.Domain);
					X500DistinguishedName item = null;
					if (smtpReceiveDomainCapabilities.SmtpX509Identifier != null && !string.IsNullOrEmpty(smtpReceiveDomainCapabilities.SmtpX509Identifier.CertificateIssuer))
					{
						item = new X500DistinguishedName(smtpReceiveDomainCapabilities.SmtpX509Identifier.CertificateIssuer);
					}
					this.tlsDomainCapabilities.Add(domain, Tuple.Create<X500DistinguishedName, SmtpReceiveDomainCapabilities>(item, smtpReceiveDomainCapabilities));
				}
			}
		}

		// Token: 0x0600385A RID: 14426 RVA: 0x000E7F5C File Offset: 0x000E615C
		private void CheckIfConnectionThresholdsExceeded(IPAddress ipAddress, int clientIPCount, int totalConnections, out bool maxConnectionsExceeded, out bool maxConnectionsPerSourceExceeded)
		{
			if (this.checkMaxInboundConnection)
			{
				maxConnectionsExceeded = (totalConnections > this.maxInboundConnection);
				if (maxConnectionsExceeded)
				{
					ExTraceGlobals.SmtpReceiveTracer.TraceDebug<int, int>((long)ipAddress.GetHashCode(), "Connection from {0} rejected: {1} (total) > {2} (maximum)", totalConnections, this.maxInboundConnection);
					this.SmtpAvailabilityPerfCounters.UpdatePerformanceCounters(LegitimateSmtpAvailabilityCategory.RejectDueToMaxInboundConnectionLimit);
				}
			}
			else
			{
				maxConnectionsExceeded = false;
			}
			if (this.checkMaxInboundConnectionPerSource)
			{
				maxConnectionsPerSourceExceeded = (clientIPCount > this.maxInboundConnectionPerSource);
				if (maxConnectionsPerSourceExceeded)
				{
					ExTraceGlobals.SmtpReceiveTracer.TraceDebug<IPAddress, int, int>((long)ipAddress.GetHashCode(), "Connection from {0} rejected: {1} (total from this source) > {2} (maximum per source)", ipAddress, clientIPCount, this.maxInboundConnectionPerSource);
				}
			}
			else
			{
				maxConnectionsPerSourceExceeded = false;
			}
			if (!maxConnectionsPerSourceExceeded && this.checkMaxInboundConnectionPercentagePerSource)
			{
				if (this.maxInboundConnectionPercentagePerSource <= 0)
				{
					maxConnectionsPerSourceExceeded = true;
					return;
				}
				if (this.checkMaxInboundConnection)
				{
					int num = this.maxInboundConnection - totalConnections;
					double num2 = Math.Ceiling((double)num * this.maxInboundConnectionPercentagePerSourceFraction);
					maxConnectionsPerSourceExceeded = ((double)clientIPCount > num2);
					if (maxConnectionsPerSourceExceeded)
					{
						ExTraceGlobals.SmtpReceiveTracer.TraceDebug((long)ipAddress.GetHashCode(), "Connection from {0} rejected: connections from this source = {1}, avail = {2}, percentage of avail (limit) = {3}", new object[]
						{
							ipAddress,
							clientIPCount,
							num,
							num2
						});
					}
				}
			}
		}

		// Token: 0x04001CE5 RID: 7397
		public static readonly SmtpDomain NoTlsDomain = new SmtpDomain("NO-TLS");

		// Token: 0x04001CE6 RID: 7398
		private readonly ReceiveConnector connector;

		// Token: 0x04001CE7 RID: 7399
		private ClientIPTable clientIpTable = new ClientIPTable();

		// Token: 0x04001CE8 RID: 7400
		private readonly ISmtpReceivePerfCounters smtpReceivePerfCounterInstance;

		// Token: 0x04001CE9 RID: 7401
		private readonly ISmtpAvailabilityPerfCounters smtpAvailabilityPerfCounters;

		// Token: 0x04001CEA RID: 7402
		private readonly RawSecurityDescriptor securityDescriptor;

		// Token: 0x04001CEB RID: 7403
		private readonly bool checkMaxInboundConnection;

		// Token: 0x04001CEC RID: 7404
		private readonly int maxInboundConnection;

		// Token: 0x04001CED RID: 7405
		private readonly bool checkMaxInboundConnectionPerSource;

		// Token: 0x04001CEE RID: 7406
		private readonly int maxInboundConnectionPerSource;

		// Token: 0x04001CEF RID: 7407
		private readonly bool checkMaxInboundConnectionPercentagePerSource;

		// Token: 0x04001CF0 RID: 7408
		private readonly int maxInboundConnectionPercentagePerSource;

		// Token: 0x04001CF1 RID: 7409
		private readonly double maxInboundConnectionPercentagePerSourceFraction;

		// Token: 0x04001CF2 RID: 7410
		private SmtpReceiveCapabilities noTlsCapabilities;

		// Token: 0x04001CF3 RID: 7411
		private MatchableDomainMap<Tuple<X500DistinguishedName, SmtpReceiveDomainCapabilities>> tlsDomainCapabilities;
	}
}

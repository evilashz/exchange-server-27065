using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Configuration;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004D2 RID: 1234
	internal class TransportConfigAdapter : ITransportConfigProvider
	{
		// Token: 0x17001176 RID: 4470
		// (get) Token: 0x060038DE RID: 14558 RVA: 0x000E8740 File Offset: 0x000E6940
		public bool AcceptAndFixSmtpAddressWithInvalidLocalPart
		{
			get
			{
				return this.appConfig.SmtpDataConfiguration.AcceptAndFixSmtpAddressWithInvalidLocalPart;
			}
		}

		// Token: 0x17001177 RID: 4471
		// (get) Token: 0x060038DF RID: 14559 RVA: 0x000E8752 File Offset: 0x000E6952
		public bool AdvertiseADRecipientCache
		{
			get
			{
				return this.appConfig.MessageContextBlobConfiguration.AdvertiseADRecipientCache;
			}
		}

		// Token: 0x17001178 RID: 4472
		// (get) Token: 0x060038E0 RID: 14560 RVA: 0x000E8764 File Offset: 0x000E6964
		public ByteQuantifiedSize AdrcCacheMaxBlobSize
		{
			get
			{
				return this.appConfig.MessageContextBlobConfiguration.AdrcCacheMaxBlobSize;
			}
		}

		// Token: 0x17001179 RID: 4473
		// (get) Token: 0x060038E1 RID: 14561 RVA: 0x000E8776 File Offset: 0x000E6976
		public bool AdvertiseExtendedProperties
		{
			get
			{
				return this.appConfig.MessageContextBlobConfiguration.AdvertiseExtendedProperties;
			}
		}

		// Token: 0x1700117A RID: 4474
		// (get) Token: 0x060038E2 RID: 14562 RVA: 0x000E8788 File Offset: 0x000E6988
		public bool AdvertiseFastIndex
		{
			get
			{
				return this.appConfig.MessageContextBlobConfiguration.AdvertiseFastIndex;
			}
		}

		// Token: 0x1700117B RID: 4475
		// (get) Token: 0x060038E3 RID: 14563 RVA: 0x000E879A File Offset: 0x000E699A
		public bool AntispamAgentsEnabled
		{
			get
			{
				return this.transportConfiguration.LocalServer.TransportServer.AntispamAgentsEnabled;
			}
		}

		// Token: 0x1700117C RID: 4476
		// (get) Token: 0x060038E4 RID: 14564 RVA: 0x000E87B1 File Offset: 0x000E69B1
		public bool BlockedSessionLoggingEnabled
		{
			get
			{
				return this.appConfig.SmtpReceiveConfiguration.BlockedSessionLoggingEnabled;
			}
		}

		// Token: 0x1700117D RID: 4477
		// (get) Token: 0x060038E5 RID: 14565 RVA: 0x000E87C3 File Offset: 0x000E69C3
		public bool ClientCertificateChainValidationEnabled
		{
			get
			{
				return this.appConfig.SecureMail.ClientCertificateChainValidationEnabled;
			}
		}

		// Token: 0x1700117E RID: 4478
		// (get) Token: 0x060038E6 RID: 14566 RVA: 0x000E87D5 File Offset: 0x000E69D5
		public bool DisableHandleInheritance
		{
			get
			{
				return this.appConfig.SmtpReceiveConfiguration.DisableHandleInheritance;
			}
		}

		// Token: 0x1700117F RID: 4479
		// (get) Token: 0x060038E7 RID: 14567 RVA: 0x000E87E7 File Offset: 0x000E69E7
		public bool EnableForwardingProhibitedFeature
		{
			get
			{
				return this.appConfig.Resolver.EnableForwardingProhibitedFeature;
			}
		}

		// Token: 0x17001180 RID: 4480
		// (get) Token: 0x060038E8 RID: 14568 RVA: 0x000E87F9 File Offset: 0x000E69F9
		public bool ExclusiveAddressUse
		{
			get
			{
				return this.appConfig.SmtpReceiveConfiguration.ExclusiveAddressUse;
			}
		}

		// Token: 0x17001181 RID: 4481
		// (get) Token: 0x060038E9 RID: 14569 RVA: 0x000E880B File Offset: 0x000E6A0B
		public ByteQuantifiedSize ExtendedPropertiesMaxBlobSize
		{
			get
			{
				return this.appConfig.MessageContextBlobConfiguration.ExtendedPropertiesMaxBlobSize;
			}
		}

		// Token: 0x17001182 RID: 4482
		// (get) Token: 0x060038EA RID: 14570 RVA: 0x000E881D File Offset: 0x000E6A1D
		public ByteQuantifiedSize FastIndexMaxBlobSize
		{
			get
			{
				return this.appConfig.MessageContextBlobConfiguration.FastIndexMaxBlobSize;
			}
		}

		// Token: 0x17001183 RID: 4483
		// (get) Token: 0x060038EB RID: 14571 RVA: 0x000E882F File Offset: 0x000E6A2F
		public AcceptedDomainTable FirstOrgAcceptedDomainTable
		{
			get
			{
				return this.transportConfiguration.FirstOrgAcceptedDomainTable;
			}
		}

		// Token: 0x17001184 RID: 4484
		// (get) Token: 0x060038EC RID: 14572 RVA: 0x000E883C File Offset: 0x000E6A3C
		public string Fqdn
		{
			get
			{
				return this.transportConfiguration.LocalServer.TransportServer.Fqdn;
			}
		}

		// Token: 0x17001185 RID: 4485
		// (get) Token: 0x060038ED RID: 14573 RVA: 0x000E8853 File Offset: 0x000E6A53
		public bool GrantExchangeServerPermissions
		{
			get
			{
				return this.appConfig.SmtpReceiveConfiguration.GrantExchangeServerPermissions;
			}
		}

		// Token: 0x17001186 RID: 4486
		// (get) Token: 0x060038EE RID: 14574 RVA: 0x000E8865 File Offset: 0x000E6A65
		public bool InboundApplyMissingEncoding
		{
			get
			{
				return this.appConfig.SmtpDataConfiguration.InboundApplyMissingEncoding;
			}
		}

		// Token: 0x17001187 RID: 4487
		// (get) Token: 0x060038EF RID: 14575 RVA: 0x000E8877 File Offset: 0x000E6A77
		public MultiValuedProperty<IPRange> InternalSMTPServers
		{
			get
			{
				return this.transportConfiguration.TransportSettings.TransportSettings.InternalSMTPServers;
			}
		}

		// Token: 0x17001188 RID: 4488
		// (get) Token: 0x060038F0 RID: 14576 RVA: 0x000E888E File Offset: 0x000E6A8E
		public string InternalTransportCertificateThumbprint
		{
			get
			{
				return this.transportConfiguration.LocalServer.TransportServer.InternalTransportCertificateThumbprint;
			}
		}

		// Token: 0x17001189 RID: 4489
		// (get) Token: 0x060038F1 RID: 14577 RVA: 0x000E88A5 File Offset: 0x000E6AA5
		public bool IsFrontendTransportServer
		{
			get
			{
				return this.transportConfiguration.LocalServer.TransportServer.IsFrontendTransportServer;
			}
		}

		// Token: 0x1700118A RID: 4490
		// (get) Token: 0x060038F2 RID: 14578 RVA: 0x000E88BC File Offset: 0x000E6ABC
		public bool IsHubTransportServer
		{
			get
			{
				return this.transportConfiguration.LocalServer.TransportServer.IsHubTransportServer;
			}
		}

		// Token: 0x1700118B RID: 4491
		// (get) Token: 0x060038F3 RID: 14579 RVA: 0x000E88D3 File Offset: 0x000E6AD3
		public bool IsIpv6ReceiveConnectionThrottlingEnabled
		{
			get
			{
				return this.isIpv6ReceiveConnectionThrottlingEnabled;
			}
		}

		// Token: 0x1700118C RID: 4492
		// (get) Token: 0x060038F4 RID: 14580 RVA: 0x000E88DB File Offset: 0x000E6ADB
		public bool IsReceiveTlsThrottlingEnabled
		{
			get
			{
				return this.isReceiveTlsThrottlingEnabled;
			}
		}

		// Token: 0x1700118D RID: 4493
		// (get) Token: 0x060038F5 RID: 14581 RVA: 0x000E88E3 File Offset: 0x000E6AE3
		public TimeSpan KerberosTicketCacheFlushMinInterval
		{
			get
			{
				return this.appConfig.SmtpAvailabilityConfiguration.KerberosTicketCacheFlushMinInterval;
			}
		}

		// Token: 0x1700118E RID: 4494
		// (get) Token: 0x060038F6 RID: 14582 RVA: 0x000E88F5 File Offset: 0x000E6AF5
		public Server LocalServer
		{
			get
			{
				return this.transportConfiguration.LocalServer.TransportServer;
			}
		}

		// Token: 0x1700118F RID: 4495
		// (get) Token: 0x060038F7 RID: 14583 RVA: 0x000E8907 File Offset: 0x000E6B07
		public bool MailboxDeliveryAcceptAnonymousUsers
		{
			get
			{
				return this.appConfig.SmtpReceiveConfiguration.MailboxDeliveryAcceptAnonymousUsers;
			}
		}

		// Token: 0x17001190 RID: 4496
		// (get) Token: 0x060038F8 RID: 14584 RVA: 0x000E8919 File Offset: 0x000E6B19
		public Unlimited<ByteQuantifiedSize> MaxSendSize
		{
			get
			{
				return this.transportConfiguration.TransportSettings.TransportSettings.MaxSendSize;
			}
		}

		// Token: 0x17001191 RID: 4497
		// (get) Token: 0x060038F9 RID: 14585 RVA: 0x000E8930 File Offset: 0x000E6B30
		public int MaxTlsConnectionsPerMinute
		{
			get
			{
				return this.transportConfiguration.LocalServer.TransportServer.MaxReceiveTlsRatePerMinute;
			}
		}

		// Token: 0x17001192 RID: 4498
		// (get) Token: 0x060038FA RID: 14586 RVA: 0x000E8947 File Offset: 0x000E6B47
		public int NetworkConnectionReceiveBufferSize
		{
			get
			{
				return this.appConfig.SmtpReceiveConfiguration.NetworkConnectionReceiveBufferSize;
			}
		}

		// Token: 0x17001193 RID: 4499
		// (get) Token: 0x060038FB RID: 14587 RVA: 0x000E8959 File Offset: 0x000E6B59
		public TransportAppConfig.ISmtpMailCommandConfig MailSmtpCommandConfig
		{
			get
			{
				return this.appConfig.SmtpMailCommandConfiguration;
			}
		}

		// Token: 0x17001194 RID: 4500
		// (get) Token: 0x060038FC RID: 14588 RVA: 0x000E8966 File Offset: 0x000E6B66
		public int MaxProxyHopCount
		{
			get
			{
				return this.appConfig.SmtpReceiveConfiguration.MaxProxyHopCount;
			}
		}

		// Token: 0x17001195 RID: 4501
		// (get) Token: 0x060038FD RID: 14589 RVA: 0x000E8978 File Offset: 0x000E6B78
		public string PhysicalMachineName
		{
			get
			{
				return TransportConfigAdapter.physicalMachineName.Value;
			}
		}

		// Token: 0x17001196 RID: 4502
		// (get) Token: 0x060038FE RID: 14590 RVA: 0x000E8984 File Offset: 0x000E6B84
		public bool OneLevelWildcardMatchForCertSelection
		{
			get
			{
				return this.appConfig.SmtpReceiveConfiguration.OneLevelWildcardMatchForCertSelection;
			}
		}

		// Token: 0x17001197 RID: 4503
		// (get) Token: 0x060038FF RID: 14591 RVA: 0x000E8996 File Offset: 0x000E6B96
		public Guid OrganizationGuid
		{
			get
			{
				return this.transportConfiguration.TransportSettings.OrganizationGuid;
			}
		}

		// Token: 0x17001198 RID: 4504
		// (get) Token: 0x06003900 RID: 14592 RVA: 0x000E89A8 File Offset: 0x000E6BA8
		public bool OutboundRejectBareLinefeeds
		{
			get
			{
				return this.appConfig.SmtpDataConfiguration.OutboundRejectBareLinefeeds;
			}
		}

		// Token: 0x17001199 RID: 4505
		// (get) Token: 0x06003901 RID: 14593 RVA: 0x000E89BA File Offset: 0x000E6BBA
		public ProcessTransportRole ProcessTransportRole
		{
			get
			{
				return this.transportConfiguration.ProcessTransportRole;
			}
		}

		// Token: 0x1700119A RID: 4506
		// (get) Token: 0x06003902 RID: 14594 RVA: 0x000E89C7 File Offset: 0x000E6BC7
		public bool RejectUnscopedMessages
		{
			get
			{
				return this.appConfig.SmtpReceiveConfiguration.RejectUnscopedMessages;
			}
		}

		// Token: 0x1700119B RID: 4507
		// (get) Token: 0x06003903 RID: 14595 RVA: 0x000E89D9 File Offset: 0x000E6BD9
		public RemoteDomainTable RemoteDomainTable
		{
			get
			{
				return this.transportConfiguration.RemoteDomainTable;
			}
		}

		// Token: 0x1700119C RID: 4508
		// (get) Token: 0x06003904 RID: 14596 RVA: 0x000E89E6 File Offset: 0x000E6BE6
		public bool SmtpAcceptAnyRecipient
		{
			get
			{
				return this.appConfig.SmtpReceiveConfiguration.SMTPAcceptAnyRecipient;
			}
		}

		// Token: 0x1700119D RID: 4509
		// (get) Token: 0x06003905 RID: 14597 RVA: 0x000E89F8 File Offset: 0x000E6BF8
		public int SmtpAvailabilityMinConnectionsToMonitor
		{
			get
			{
				return this.appConfig.SmtpAvailabilityConfiguration.SmtpAvailabilityMinConnectionsToMonitor;
			}
		}

		// Token: 0x1700119E RID: 4510
		// (get) Token: 0x06003906 RID: 14598 RVA: 0x000E8A0A File Offset: 0x000E6C0A
		public string SmtpDataResponse
		{
			get
			{
				return this.appConfig.SmtpDataConfiguration.SmtpDataResponse;
			}
		}

		// Token: 0x1700119F RID: 4511
		// (get) Token: 0x06003907 RID: 14599 RVA: 0x000E8A1C File Offset: 0x000E6C1C
		public int SubjectAlternativeNameLimit
		{
			get
			{
				return this.appConfig.SecureMail.SubjectAlternativeNameLimit;
			}
		}

		// Token: 0x170011A0 RID: 4512
		// (get) Token: 0x06003908 RID: 14600 RVA: 0x000E8A2E File Offset: 0x000E6C2E
		public bool TarpitMuaSubmission
		{
			get
			{
				return this.appConfig.SmtpReceiveConfiguration.TarpitMuaSubmission;
			}
		}

		// Token: 0x170011A1 RID: 4513
		// (get) Token: 0x06003909 RID: 14601 RVA: 0x000E8A40 File Offset: 0x000E6C40
		public IEnumerable<SmtpDomain> TlsReceiveDomainSecureList
		{
			get
			{
				return this.transportConfiguration.TransportSettings.TransportSettings.TLSReceiveDomainSecureList;
			}
		}

		// Token: 0x170011A2 RID: 4514
		// (get) Token: 0x0600390A RID: 14602 RVA: 0x000E8A57 File Offset: 0x000E6C57
		public bool TransferADRecipientCache
		{
			get
			{
				return this.appConfig.MessageContextBlobConfiguration.TransferADRecipientCache;
			}
		}

		// Token: 0x170011A3 RID: 4515
		// (get) Token: 0x0600390B RID: 14603 RVA: 0x000E8A69 File Offset: 0x000E6C69
		public bool TransferExtendedProperties
		{
			get
			{
				return this.appConfig.MessageContextBlobConfiguration.TransferExtendedProperties;
			}
		}

		// Token: 0x170011A4 RID: 4516
		// (get) Token: 0x0600390C RID: 14604 RVA: 0x000E8A7B File Offset: 0x000E6C7B
		public bool TransferFastIndex
		{
			get
			{
				return this.appConfig.MessageContextBlobConfiguration.TransferFastIndex;
			}
		}

		// Token: 0x170011A5 RID: 4517
		// (get) Token: 0x0600390D RID: 14605 RVA: 0x000E8A8D File Offset: 0x000E6C8D
		public bool TreatCRLTransientFailuresAsSuccessEnabled
		{
			get
			{
				return this.appConfig.SecureMail.TreatCRLTransientFailuresAsSuccessEnabled;
			}
		}

		// Token: 0x170011A6 RID: 4518
		// (get) Token: 0x0600390E RID: 14606 RVA: 0x000E8A9F File Offset: 0x000E6C9F
		public Version Version
		{
			get
			{
				return this.transportConfiguration.LocalServer.TransportServer.AdminDisplayVersion;
			}
		}

		// Token: 0x170011A7 RID: 4519
		// (get) Token: 0x0600390F RID: 14607 RVA: 0x000E8ABB File Offset: 0x000E6CBB
		public bool WaitForSmtpSessionsAtShutdown
		{
			get
			{
				return this.appConfig.SmtpReceiveConfiguration.WaitForSmtpSessionsAtShutdown;
			}
		}

		// Token: 0x170011A8 RID: 4520
		// (get) Token: 0x06003910 RID: 14608 RVA: 0x000E8ACD File Offset: 0x000E6CCD
		public bool WatsonReportOnFailureEnabled
		{
			get
			{
				return this.appConfig.MessageContextBlobConfiguration.WatsonReportOnFailureEnabled;
			}
		}

		// Token: 0x170011A9 RID: 4521
		// (get) Token: 0x06003911 RID: 14609 RVA: 0x000E8ADF File Offset: 0x000E6CDF
		public bool Xexch50Enabled
		{
			get
			{
				return this.transportConfiguration.TransportSettings.TransportSettings.Xexch50Enabled;
			}
		}

		// Token: 0x06003912 RID: 14610 RVA: 0x000E8AF6 File Offset: 0x000E6CF6
		public static ITransportConfigProvider Create(ITransportAppConfig appConfig, ITransportConfiguration transportConfiguration)
		{
			ArgumentValidator.ThrowIfNull("appConfig", appConfig);
			ArgumentValidator.ThrowIfNull("transportConfiguration", transportConfiguration);
			return new TransportConfigAdapter(appConfig, transportConfiguration);
		}

		// Token: 0x06003913 RID: 14611 RVA: 0x000E8B15 File Offset: 0x000E6D15
		public bool IsTlsReceiveSecureDomain(string domainName)
		{
			return this.transportConfiguration.TransportSettings.TransportSettings.IsTLSReceiveSecureDomain(domainName);
		}

		// Token: 0x06003914 RID: 14612 RVA: 0x000E8B2D File Offset: 0x000E6D2D
		public PerTenantAcceptedDomainTable GetAcceptedDomainTable(OrganizationId orgId)
		{
			return this.transportConfiguration.GetAcceptedDomainTable(orgId);
		}

		// Token: 0x06003915 RID: 14613 RVA: 0x000E8B3C File Offset: 0x000E6D3C
		private TransportConfigAdapter(ITransportAppConfig appConfig, ITransportConfiguration transportConfiguration)
		{
			this.appConfig = appConfig;
			this.transportConfiguration = transportConfiguration;
			this.isIpv6ReceiveConnectionThrottlingEnabled = this.appConfig.SmtpReceiveConfiguration.Ipv6ReceiveConnectionThrottlingEnabled;
			this.isReceiveTlsThrottlingEnabled = this.appConfig.SmtpReceiveConfiguration.ReceiveTlsThrottlingEnabled;
		}

		// Token: 0x04001CFB RID: 7419
		private readonly bool isIpv6ReceiveConnectionThrottlingEnabled;

		// Token: 0x04001CFC RID: 7420
		private readonly bool isReceiveTlsThrottlingEnabled;

		// Token: 0x04001CFD RID: 7421
		private static readonly Lazy<string> physicalMachineName = new Lazy<string>(() => ComputerInformation.DnsPhysicalFullyQualifiedDomainName);

		// Token: 0x04001CFE RID: 7422
		private readonly ITransportAppConfig appConfig;

		// Token: 0x04001CFF RID: 7423
		private readonly ITransportConfiguration transportConfiguration;
	}
}

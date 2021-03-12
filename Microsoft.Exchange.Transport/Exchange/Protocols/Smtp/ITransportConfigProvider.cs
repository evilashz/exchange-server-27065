using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Configuration;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004CF RID: 1231
	internal interface ITransportConfigProvider
	{
		// Token: 0x17001139 RID: 4409
		// (get) Token: 0x06003898 RID: 14488
		bool AcceptAndFixSmtpAddressWithInvalidLocalPart { get; }

		// Token: 0x1700113A RID: 4410
		// (get) Token: 0x06003899 RID: 14489
		bool AdvertiseADRecipientCache { get; }

		// Token: 0x1700113B RID: 4411
		// (get) Token: 0x0600389A RID: 14490
		ByteQuantifiedSize AdrcCacheMaxBlobSize { get; }

		// Token: 0x1700113C RID: 4412
		// (get) Token: 0x0600389B RID: 14491
		bool AdvertiseExtendedProperties { get; }

		// Token: 0x1700113D RID: 4413
		// (get) Token: 0x0600389C RID: 14492
		bool AdvertiseFastIndex { get; }

		// Token: 0x1700113E RID: 4414
		// (get) Token: 0x0600389D RID: 14493
		bool AntispamAgentsEnabled { get; }

		// Token: 0x1700113F RID: 4415
		// (get) Token: 0x0600389E RID: 14494
		bool BlockedSessionLoggingEnabled { get; }

		// Token: 0x17001140 RID: 4416
		// (get) Token: 0x0600389F RID: 14495
		bool ClientCertificateChainValidationEnabled { get; }

		// Token: 0x17001141 RID: 4417
		// (get) Token: 0x060038A0 RID: 14496
		bool DisableHandleInheritance { get; }

		// Token: 0x17001142 RID: 4418
		// (get) Token: 0x060038A1 RID: 14497
		bool EnableForwardingProhibitedFeature { get; }

		// Token: 0x17001143 RID: 4419
		// (get) Token: 0x060038A2 RID: 14498
		bool ExclusiveAddressUse { get; }

		// Token: 0x17001144 RID: 4420
		// (get) Token: 0x060038A3 RID: 14499
		ByteQuantifiedSize ExtendedPropertiesMaxBlobSize { get; }

		// Token: 0x17001145 RID: 4421
		// (get) Token: 0x060038A4 RID: 14500
		ByteQuantifiedSize FastIndexMaxBlobSize { get; }

		// Token: 0x17001146 RID: 4422
		// (get) Token: 0x060038A5 RID: 14501
		AcceptedDomainTable FirstOrgAcceptedDomainTable { get; }

		// Token: 0x17001147 RID: 4423
		// (get) Token: 0x060038A6 RID: 14502
		string Fqdn { get; }

		// Token: 0x17001148 RID: 4424
		// (get) Token: 0x060038A7 RID: 14503
		bool GrantExchangeServerPermissions { get; }

		// Token: 0x17001149 RID: 4425
		// (get) Token: 0x060038A8 RID: 14504
		bool InboundApplyMissingEncoding { get; }

		// Token: 0x1700114A RID: 4426
		// (get) Token: 0x060038A9 RID: 14505
		MultiValuedProperty<IPRange> InternalSMTPServers { get; }

		// Token: 0x1700114B RID: 4427
		// (get) Token: 0x060038AA RID: 14506
		string InternalTransportCertificateThumbprint { get; }

		// Token: 0x1700114C RID: 4428
		// (get) Token: 0x060038AB RID: 14507
		bool IsFrontendTransportServer { get; }

		// Token: 0x1700114D RID: 4429
		// (get) Token: 0x060038AC RID: 14508
		bool IsHubTransportServer { get; }

		// Token: 0x1700114E RID: 4430
		// (get) Token: 0x060038AD RID: 14509
		bool IsIpv6ReceiveConnectionThrottlingEnabled { get; }

		// Token: 0x1700114F RID: 4431
		// (get) Token: 0x060038AE RID: 14510
		bool IsReceiveTlsThrottlingEnabled { get; }

		// Token: 0x17001150 RID: 4432
		// (get) Token: 0x060038AF RID: 14511
		TimeSpan KerberosTicketCacheFlushMinInterval { get; }

		// Token: 0x17001151 RID: 4433
		// (get) Token: 0x060038B0 RID: 14512
		Server LocalServer { get; }

		// Token: 0x17001152 RID: 4434
		// (get) Token: 0x060038B1 RID: 14513
		TransportAppConfig.ISmtpMailCommandConfig MailSmtpCommandConfig { get; }

		// Token: 0x17001153 RID: 4435
		// (get) Token: 0x060038B2 RID: 14514
		bool MailboxDeliveryAcceptAnonymousUsers { get; }

		// Token: 0x17001154 RID: 4436
		// (get) Token: 0x060038B3 RID: 14515
		int MaxProxyHopCount { get; }

		// Token: 0x17001155 RID: 4437
		// (get) Token: 0x060038B4 RID: 14516
		Unlimited<ByteQuantifiedSize> MaxSendSize { get; }

		// Token: 0x17001156 RID: 4438
		// (get) Token: 0x060038B5 RID: 14517
		int MaxTlsConnectionsPerMinute { get; }

		// Token: 0x17001157 RID: 4439
		// (get) Token: 0x060038B6 RID: 14518
		int NetworkConnectionReceiveBufferSize { get; }

		// Token: 0x17001158 RID: 4440
		// (get) Token: 0x060038B7 RID: 14519
		string PhysicalMachineName { get; }

		// Token: 0x17001159 RID: 4441
		// (get) Token: 0x060038B8 RID: 14520
		bool OneLevelWildcardMatchForCertSelection { get; }

		// Token: 0x1700115A RID: 4442
		// (get) Token: 0x060038B9 RID: 14521
		Guid OrganizationGuid { get; }

		// Token: 0x1700115B RID: 4443
		// (get) Token: 0x060038BA RID: 14522
		bool OutboundRejectBareLinefeeds { get; }

		// Token: 0x1700115C RID: 4444
		// (get) Token: 0x060038BB RID: 14523
		ProcessTransportRole ProcessTransportRole { get; }

		// Token: 0x1700115D RID: 4445
		// (get) Token: 0x060038BC RID: 14524
		bool RejectUnscopedMessages { get; }

		// Token: 0x1700115E RID: 4446
		// (get) Token: 0x060038BD RID: 14525
		RemoteDomainTable RemoteDomainTable { get; }

		// Token: 0x1700115F RID: 4447
		// (get) Token: 0x060038BE RID: 14526
		bool SmtpAcceptAnyRecipient { get; }

		// Token: 0x17001160 RID: 4448
		// (get) Token: 0x060038BF RID: 14527
		int SmtpAvailabilityMinConnectionsToMonitor { get; }

		// Token: 0x17001161 RID: 4449
		// (get) Token: 0x060038C0 RID: 14528
		string SmtpDataResponse { get; }

		// Token: 0x17001162 RID: 4450
		// (get) Token: 0x060038C1 RID: 14529
		int SubjectAlternativeNameLimit { get; }

		// Token: 0x17001163 RID: 4451
		// (get) Token: 0x060038C2 RID: 14530
		bool TarpitMuaSubmission { get; }

		// Token: 0x17001164 RID: 4452
		// (get) Token: 0x060038C3 RID: 14531
		IEnumerable<SmtpDomain> TlsReceiveDomainSecureList { get; }

		// Token: 0x17001165 RID: 4453
		// (get) Token: 0x060038C4 RID: 14532
		bool TransferADRecipientCache { get; }

		// Token: 0x17001166 RID: 4454
		// (get) Token: 0x060038C5 RID: 14533
		bool TransferExtendedProperties { get; }

		// Token: 0x17001167 RID: 4455
		// (get) Token: 0x060038C6 RID: 14534
		bool TransferFastIndex { get; }

		// Token: 0x17001168 RID: 4456
		// (get) Token: 0x060038C7 RID: 14535
		bool TreatCRLTransientFailuresAsSuccessEnabled { get; }

		// Token: 0x17001169 RID: 4457
		// (get) Token: 0x060038C8 RID: 14536
		Version Version { get; }

		// Token: 0x1700116A RID: 4458
		// (get) Token: 0x060038C9 RID: 14537
		bool WaitForSmtpSessionsAtShutdown { get; }

		// Token: 0x1700116B RID: 4459
		// (get) Token: 0x060038CA RID: 14538
		bool WatsonReportOnFailureEnabled { get; }

		// Token: 0x1700116C RID: 4460
		// (get) Token: 0x060038CB RID: 14539
		bool Xexch50Enabled { get; }

		// Token: 0x060038CC RID: 14540
		bool IsTlsReceiveSecureDomain(string domainName);

		// Token: 0x060038CD RID: 14541
		PerTenantAcceptedDomainTable GetAcceptedDomainTable(OrganizationId orgId);
	}
}

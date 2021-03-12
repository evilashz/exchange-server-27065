using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Transport.Configuration;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020002CF RID: 719
	internal interface ITransportConfiguration
	{
		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06001FBF RID: 8127
		AcceptedDomainTable FirstOrgAcceptedDomainTable { get; }

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06001FC0 RID: 8128
		// (remove) Token: 0x06001FC1 RID: 8129
		event ConfigurationUpdateHandler<AcceptedDomainTable> FirstOrgAcceptedDomainTableChanged;

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06001FC2 RID: 8130
		RemoteDomainTable RemoteDomainTable { get; }

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06001FC3 RID: 8131
		// (remove) Token: 0x06001FC4 RID: 8132
		event ConfigurationUpdateHandler<RemoteDomainTable> RemoteDomainTableChanged;

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06001FC5 RID: 8133
		X400AuthoritativeDomainTable X400AuthoritativeDomainTable { get; }

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06001FC6 RID: 8134
		ReceiveConnectorConfiguration LocalReceiveConnectors { get; }

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06001FC7 RID: 8135
		// (remove) Token: 0x06001FC8 RID: 8136
		event ConfigurationUpdateHandler<ReceiveConnectorConfiguration> LocalReceiveConnectorsChanged;

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06001FC9 RID: 8137
		TransportServerConfiguration LocalServer { get; }

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06001FCA RID: 8138
		// (remove) Token: 0x06001FCB RID: 8139
		event ConfigurationUpdateHandler<TransportServerConfiguration> LocalServerChanged;

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x06001FCC RID: 8140
		TransportSettingsConfiguration TransportSettings { get; }

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x06001FCD RID: 8141
		MicrosoftExchangeRecipientPerTenantSettings MicrosoftExchangeRecipient { get; }

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06001FCE RID: 8142
		// (remove) Token: 0x06001FCF RID: 8143
		event ConfigurationUpdateHandler<TransportSettingsConfiguration> TransportSettingsChanged;

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x06001FD0 RID: 8144
		TransportAppConfig AppConfig { get; }

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x06001FD1 RID: 8145
		ProcessTransportRole ProcessTransportRole { get; }

		// Token: 0x06001FD2 RID: 8146
		void ClearCaches();

		// Token: 0x06001FD3 RID: 8147
		bool TryGetTransportSettings(OrganizationId orgId, out PerTenantTransportSettings perTenantTransportSettings);

		// Token: 0x06001FD4 RID: 8148
		PerTenantTransportSettings GetTransportSettings(OrganizationId orgId);

		// Token: 0x06001FD5 RID: 8149
		bool TryGetPerimeterSettings(OrganizationId orgId, out PerTenantPerimeterSettings perTenantPerimeterSettings);

		// Token: 0x06001FD6 RID: 8150
		PerTenantPerimeterSettings GetPerimeterSettings(OrganizationId orgId);

		// Token: 0x06001FD7 RID: 8151
		bool TryGetMicrosoftExchangeRecipient(OrganizationId orgId, out MicrosoftExchangeRecipientPerTenantSettings perTenantMicrosoftExchangeRecipient);

		// Token: 0x06001FD8 RID: 8152
		MicrosoftExchangeRecipientPerTenantSettings GetMicrosoftExchangeRecipient(OrganizationId orgId);

		// Token: 0x06001FD9 RID: 8153
		bool TryGetRemoteDomainTable(OrganizationId orgId, out PerTenantRemoteDomainTable perTenantRemoteDomains);

		// Token: 0x06001FDA RID: 8154
		PerTenantRemoteDomainTable GetRemoteDomainTable(OrganizationId orgId);

		// Token: 0x06001FDB RID: 8155
		bool TryGetAcceptedDomainTable(OrganizationId orgId, out PerTenantAcceptedDomainTable perTenantAcceptedDomains);

		// Token: 0x06001FDC RID: 8156
		PerTenantAcceptedDomainTable GetAcceptedDomainTable(OrganizationId orgId);

		// Token: 0x06001FDD RID: 8157
		bool TryGetJournalingRules(OrganizationId orgId, out JournalingRulesPerTenantSettings perTenantJournalingRules);

		// Token: 0x06001FDE RID: 8158
		JournalingRulesPerTenantSettings GetJournalingRules(OrganizationId orgId);

		// Token: 0x06001FDF RID: 8159
		bool TryGetReconciliationAccounts(OrganizationId orgId, out ReconciliationAccountPerTenantSettings perTenantReconciliationSettings);

		// Token: 0x06001FE0 RID: 8160
		ReconciliationAccountPerTenantSettings GetReconciliationAccounts(OrganizationId orgId);

		// Token: 0x06001FE1 RID: 8161
		bool TryGetTenantOutboundConnectors(OrganizationId orgId, out PerTenantOutboundConnectors perTenantOutboundConnectors);
	}
}

using System;
using System.Xml.Linq;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000297 RID: 663
	internal interface ITransportAppConfig
	{
		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06001C2E RID: 7214
		TransportAppConfig.ResourceManagerConfig ResourceManager { get; }

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06001C2F RID: 7215
		TransportAppConfig.JetDatabaseConfig JetDatabase { get; }

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06001C30 RID: 7216
		TransportAppConfig.DumpsterConfig Dumpster { get; }

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06001C31 RID: 7217
		TransportAppConfig.ShadowRedundancyConfig ShadowRedundancy { get; }

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06001C32 RID: 7218
		TransportAppConfig.RemoteDeliveryConfig RemoteDelivery { get; }

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06001C33 RID: 7219
		TransportAppConfig.MapiSubmissionConfig MapiSubmission { get; }

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06001C34 RID: 7220
		TransportAppConfig.ResolverConfig Resolver { get; }

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06001C35 RID: 7221
		TransportAppConfig.RoutingConfig Routing { get; }

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06001C36 RID: 7222
		TransportAppConfig.ContentConversionConfig ContentConversion { get; }

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06001C37 RID: 7223
		TransportAppConfig.IPFilteringDatabaseConfig IPFilteringDatabase { get; }

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06001C38 RID: 7224
		TransportAppConfig.IMessageResubmissionConfig MessageResubmission { get; }

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06001C39 RID: 7225
		TransportAppConfig.QueueDatabaseConfig QueueDatabase { get; }

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06001C3A RID: 7226
		TransportAppConfig.WorkerProcessConfig WorkerProcess { get; }

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06001C3B RID: 7227
		TransportAppConfig.LatencyTrackerConfig LatencyTracker { get; }

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06001C3C RID: 7228
		TransportAppConfig.RecipientValidatorConfig RecipientValidtor { get; }

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06001C3D RID: 7229
		TransportAppConfig.PerTenantCacheConfig PerTenantCache { get; }

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06001C3E RID: 7230
		TransportAppConfig.MessageThrottlingConfiguration MessageThrottlingConfig { get; }

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06001C3F RID: 7231
		TransportAppConfig.SMTPOutConnectionCacheConfig ConnectionCacheConfig { get; }

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06001C40 RID: 7232
		TransportAppConfig.IsMemberOfResolverConfiguration TransportIsMemberOfResolverConfig { get; }

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06001C41 RID: 7233
		TransportAppConfig.IsMemberOfResolverConfiguration MailboxRulesIsMemberOfResolverConfig { get; }

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06001C42 RID: 7234
		TransportAppConfig.SmtpAvailabilityConfig SmtpAvailabilityConfiguration { get; }

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06001C43 RID: 7235
		TransportAppConfig.SmtpDataConfig SmtpDataConfiguration { get; }

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06001C44 RID: 7236
		TransportAppConfig.SmtpMailCommandConfig SmtpMailCommandConfiguration { get; }

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06001C45 RID: 7237
		TransportAppConfig.MessageContextBlobConfig MessageContextBlobConfiguration { get; }

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06001C46 RID: 7238
		TransportAppConfig.SmtpReceiveConfig SmtpReceiveConfiguration { get; }

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06001C47 RID: 7239
		TransportAppConfig.SmtpSendConfig SmtpSendConfiguration { get; }

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06001C48 RID: 7240
		TransportAppConfig.SmtpProxyConfig SmtpProxyConfiguration { get; }

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06001C49 RID: 7241
		TransportAppConfig.SmtpInboundProxyConfig SmtpInboundProxyConfiguration { get; }

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06001C4A RID: 7242
		TransportAppConfig.SmtpOutboundProxyConfig SmtpOutboundProxyConfiguration { get; }

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06001C4B RID: 7243
		TransportAppConfig.DeliveryQueuePrioritizationConfig DeliveryQueuePrioritizationConfiguration { get; }

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06001C4C RID: 7244
		TransportAppConfig.QueueConfig QueueConfiguration { get; }

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06001C4D RID: 7245
		TransportAppConfig.DeliveryFailureConfig DeliveryFailureConfiguration { get; }

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06001C4E RID: 7246
		TransportAppConfig.SecureMailConfig SecureMail { get; }

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06001C4F RID: 7247
		TransportAppConfig.LoggingConfig Logging { get; }

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06001C50 RID: 7248
		TransportAppConfig.FlowControlLogConfig FlowControlLog { get; }

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06001C51 RID: 7249
		TransportAppConfig.ConditionalThrottlingConfig ThrottlingConfig { get; }

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06001C52 RID: 7250
		TransportAppConfig.TransportRulesConfig TransportRuleConfig { get; }

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06001C53 RID: 7251
		TransportAppConfig.PoisonMessageConfig PoisonMessage { get; }

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06001C54 RID: 7252
		TransportAppConfig.SmtpMessageThrottlingAgentConfig SmtpMessageThrottlingConfig { get; }

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06001C55 RID: 7253
		TransportAppConfig.StateManagementConfig StateManagement { get; }

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06001C56 RID: 7254
		TransportAppConfig.BootLoaderConfig BootLoader { get; }

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06001C57 RID: 7255
		TransportAppConfig.ProcessingQuotaConfig ProcessingQuota { get; }

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06001C58 RID: 7256
		TransportAppConfig.ADPollingConfig ADPolling { get; }

		// Token: 0x06001C59 RID: 7257
		XElement GetDiagnosticInfo();
	}
}

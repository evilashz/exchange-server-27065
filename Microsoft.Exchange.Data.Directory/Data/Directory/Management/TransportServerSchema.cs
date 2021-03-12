using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000777 RID: 1911
	internal class TransportServerSchema : ADPresentationSchema
	{
		// Token: 0x06005E03 RID: 24067 RVA: 0x001438D2 File Offset: 0x00141AD2
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<ServerSchema>();
		}

		// Token: 0x04003F8B RID: 16267
		public static readonly ADPropertyDefinition DelayNotificationTimeout = ServerSchema.DelayNotificationTimeout;

		// Token: 0x04003F8C RID: 16268
		public static readonly ADPropertyDefinition MessageExpirationTimeout = ServerSchema.MessageExpirationTimeout;

		// Token: 0x04003F8D RID: 16269
		public static readonly ADPropertyDefinition QueueMaxIdleTime = ServerSchema.QueueMaxIdleTime;

		// Token: 0x04003F8E RID: 16270
		public static readonly ADPropertyDefinition MessageRetryInterval = ServerSchema.MessageRetryInterval;

		// Token: 0x04003F8F RID: 16271
		public static readonly ADPropertyDefinition TransientFailureRetryInterval = ServerSchema.TransientFailureRetryInterval;

		// Token: 0x04003F90 RID: 16272
		public static readonly ADPropertyDefinition TransientFailureRetryCount = ServerSchema.TransientFailureRetryCount;

		// Token: 0x04003F91 RID: 16273
		public static readonly ADPropertyDefinition MaxOutboundConnections = ServerSchema.MaxOutboundConnections;

		// Token: 0x04003F92 RID: 16274
		public static readonly ADPropertyDefinition MaxPerDomainOutboundConnections = ServerSchema.MaxPerDomainOutboundConnections;

		// Token: 0x04003F93 RID: 16275
		public static readonly ADPropertyDefinition MaxConnectionRatePerMinute = ServerSchema.MaxConnectionRatePerMinute;

		// Token: 0x04003F94 RID: 16276
		public static readonly ADPropertyDefinition ReceiveProtocolLogPath = ServerSchema.ReceiveProtocolLogPath;

		// Token: 0x04003F95 RID: 16277
		public static readonly ADPropertyDefinition SendProtocolLogPath = ServerSchema.SendProtocolLogPath;

		// Token: 0x04003F96 RID: 16278
		public static readonly ADPropertyDefinition OutboundConnectionFailureRetryInterval = ServerSchema.OutboundConnectionFailureRetryInterval;

		// Token: 0x04003F97 RID: 16279
		public static readonly ADPropertyDefinition ReceiveProtocolLogMaxAge = ServerSchema.ReceiveProtocolLogMaxAge;

		// Token: 0x04003F98 RID: 16280
		public static readonly ADPropertyDefinition ReceiveProtocolLogMaxDirectorySize = ServerSchema.ReceiveProtocolLogMaxDirectorySize;

		// Token: 0x04003F99 RID: 16281
		public static readonly ADPropertyDefinition ReceiveProtocolLogMaxFileSize = ServerSchema.ReceiveProtocolLogMaxFileSize;

		// Token: 0x04003F9A RID: 16282
		public static readonly ADPropertyDefinition SendProtocolLogMaxAge = ServerSchema.SendProtocolLogMaxAge;

		// Token: 0x04003F9B RID: 16283
		public static readonly ADPropertyDefinition SendProtocolLogMaxDirectorySize = ServerSchema.SendProtocolLogMaxDirectorySize;

		// Token: 0x04003F9C RID: 16284
		public static readonly ADPropertyDefinition SendProtocolLogMaxFileSize = ServerSchema.SendProtocolLogMaxFileSize;

		// Token: 0x04003F9D RID: 16285
		public static readonly ADPropertyDefinition InternalDNSAdapterDisabled = ServerSchema.InternalDNSAdapterDisabled;

		// Token: 0x04003F9E RID: 16286
		public static readonly ADPropertyDefinition InternalDNSAdapterGuid = ServerSchema.InternalDNSAdapterGuid;

		// Token: 0x04003F9F RID: 16287
		public static readonly ADPropertyDefinition InternalDNSServers = ServerSchema.InternalDNSServers;

		// Token: 0x04003FA0 RID: 16288
		public static readonly ADPropertyDefinition InternalDNSProtocolOption = ServerSchema.InternalDNSProtocolOption;

		// Token: 0x04003FA1 RID: 16289
		public static readonly ADPropertyDefinition ExternalDNSAdapterDisabled = ServerSchema.ExternalDNSAdapterDisabled;

		// Token: 0x04003FA2 RID: 16290
		public static readonly ADPropertyDefinition ExternalDNSAdapterGuid = ServerSchema.ExternalDNSAdapterGuid;

		// Token: 0x04003FA3 RID: 16291
		public static readonly ADPropertyDefinition ExternalDNSServers = ServerSchema.ExternalDNSServers;

		// Token: 0x04003FA4 RID: 16292
		public static readonly ADPropertyDefinition ExternalDNSProtocolOption = ServerSchema.ExternalDNSProtocolOption;

		// Token: 0x04003FA5 RID: 16293
		public static readonly ADPropertyDefinition ExternalIPAddress = ServerSchema.ExternalIPAddress;

		// Token: 0x04003FA6 RID: 16294
		public static readonly ADPropertyDefinition MaxConcurrentMailboxDeliveries = ServerSchema.MaxConcurrentMailboxDeliveries;

		// Token: 0x04003FA7 RID: 16295
		public static readonly ADPropertyDefinition PoisonThreshold = ServerSchema.PoisonThreshold;

		// Token: 0x04003FA8 RID: 16296
		public static readonly ADPropertyDefinition MessageTrackingLogPath = ServerSchema.MessageTrackingLogPath;

		// Token: 0x04003FA9 RID: 16297
		public static readonly ADPropertyDefinition MessageTrackingLogMaxAge = ServerSchema.MessageTrackingLogMaxAge;

		// Token: 0x04003FAA RID: 16298
		public static readonly ADPropertyDefinition MessageTrackingLogMaxDirectorySize = ServerSchema.MessageTrackingLogMaxDirectorySize;

		// Token: 0x04003FAB RID: 16299
		public static readonly ADPropertyDefinition MessageTrackingLogMaxFileSize = ServerSchema.MessageTrackingLogMaxFileSize;

		// Token: 0x04003FAC RID: 16300
		public static readonly ADPropertyDefinition IrmLogPath = ServerSchema.IrmLogPath;

		// Token: 0x04003FAD RID: 16301
		public static readonly ADPropertyDefinition IrmLogMaxAge = ServerSchema.IrmLogMaxAge;

		// Token: 0x04003FAE RID: 16302
		public static readonly ADPropertyDefinition IrmLogMaxDirectorySize = ServerSchema.IrmLogMaxDirectorySize;

		// Token: 0x04003FAF RID: 16303
		public static readonly ADPropertyDefinition IrmLogMaxFileSize = ServerSchema.IrmLogMaxFileSize;

		// Token: 0x04003FB0 RID: 16304
		public static readonly ADPropertyDefinition ActiveUserStatisticsLogPath = ServerSchema.ActiveUserStatisticsLogPath;

		// Token: 0x04003FB1 RID: 16305
		public static readonly ADPropertyDefinition ActiveUserStatisticsLogMaxAge = ServerSchema.ActiveUserStatisticsLogMaxAge;

		// Token: 0x04003FB2 RID: 16306
		public static readonly ADPropertyDefinition ActiveUserStatisticsLogMaxDirectorySize = ServerSchema.ActiveUserStatisticsLogMaxDirectorySize;

		// Token: 0x04003FB3 RID: 16307
		public static readonly ADPropertyDefinition ActiveUserStatisticsLogMaxFileSize = ServerSchema.ActiveUserStatisticsLogMaxFileSize;

		// Token: 0x04003FB4 RID: 16308
		public static readonly ADPropertyDefinition ServerStatisticsLogPath = ServerSchema.ServerStatisticsLogPath;

		// Token: 0x04003FB5 RID: 16309
		public static readonly ADPropertyDefinition ServerStatisticsLogMaxAge = ServerSchema.ServerStatisticsLogMaxAge;

		// Token: 0x04003FB6 RID: 16310
		public static readonly ADPropertyDefinition ServerStatisticsLogMaxDirectorySize = ServerSchema.ServerStatisticsLogMaxDirectorySize;

		// Token: 0x04003FB7 RID: 16311
		public static readonly ADPropertyDefinition ServerStatisticsLogMaxFileSize = ServerSchema.ServerStatisticsLogMaxFileSize;

		// Token: 0x04003FB8 RID: 16312
		public static readonly ADPropertyDefinition MessageTrackingLogSubjectLoggingEnabled = ServerSchema.MessageTrackingLogSubjectLoggingEnabled;

		// Token: 0x04003FB9 RID: 16313
		public static readonly ADPropertyDefinition PipelineTracingEnabled = ServerSchema.PipelineTracingEnabled;

		// Token: 0x04003FBA RID: 16314
		public static readonly ADPropertyDefinition ContentConversionTracingEnabled = ServerSchema.ContentConversionTracingEnabled;

		// Token: 0x04003FBB RID: 16315
		public static readonly ADPropertyDefinition PipelineTracingPath = ServerSchema.PipelineTracingPath;

		// Token: 0x04003FBC RID: 16316
		public static readonly ADPropertyDefinition PipelineTracingSenderAddress = ServerSchema.PipelineTracingSenderAddress;

		// Token: 0x04003FBD RID: 16317
		public static readonly ADPropertyDefinition ConnectivityLogEnabled = ServerSchema.ConnectivityLogEnabled;

		// Token: 0x04003FBE RID: 16318
		public static readonly ADPropertyDefinition ConnectivityLogPath = ServerSchema.ConnectivityLogPath;

		// Token: 0x04003FBF RID: 16319
		public static readonly ADPropertyDefinition ConnectivityLogMaxAge = ServerSchema.ConnectivityLogMaxAge;

		// Token: 0x04003FC0 RID: 16320
		public static readonly ADPropertyDefinition ConnectivityLogMaxDirectorySize = ServerSchema.ConnectivityLogMaxDirectorySize;

		// Token: 0x04003FC1 RID: 16321
		public static readonly ADPropertyDefinition ConnectivityLogMaxFileSize = ServerSchema.ConnectivityLogMaxFileSize;

		// Token: 0x04003FC2 RID: 16322
		public static readonly ADPropertyDefinition PickupDirectoryPath = ServerSchema.PickupDirectoryPath;

		// Token: 0x04003FC3 RID: 16323
		public static readonly ADPropertyDefinition ReplayDirectoryPath = ServerSchema.ReplayDirectoryPath;

		// Token: 0x04003FC4 RID: 16324
		public static readonly ADPropertyDefinition PickupDirectoryMaxMessagesPerMinute = ServerSchema.PickupDirectoryMaxMessagesPerMinute;

		// Token: 0x04003FC5 RID: 16325
		public static readonly ADPropertyDefinition PickupDirectoryMaxHeaderSize = ServerSchema.PickupDirectoryMaxHeaderSize;

		// Token: 0x04003FC6 RID: 16326
		public static readonly ADPropertyDefinition PickupDirectoryMaxRecipientsPerMessage = ServerSchema.PickupDirectoryMaxRecipientsPerMessage;

		// Token: 0x04003FC7 RID: 16327
		public static readonly ADPropertyDefinition RoutingTableLogPath = ServerSchema.RoutingTableLogPath;

		// Token: 0x04003FC8 RID: 16328
		public static readonly ADPropertyDefinition RoutingTableLogMaxAge = ServerSchema.RoutingTableLogMaxAge;

		// Token: 0x04003FC9 RID: 16329
		public static readonly ADPropertyDefinition RoutingTableLogMaxDirectorySize = ServerSchema.RoutingTableLogMaxDirectorySize;

		// Token: 0x04003FCA RID: 16330
		public static readonly ADPropertyDefinition IntraOrgConnectorProtocolLoggingLevel = ServerSchema.IntraOrgConnectorProtocolLoggingLevel;

		// Token: 0x04003FCB RID: 16331
		public static readonly ADPropertyDefinition MessageTrackingLogEnabled = ServerSchema.MessageTrackingLogEnabled;

		// Token: 0x04003FCC RID: 16332
		public static readonly ADPropertyDefinition IrmLogEnabled = ServerSchema.IrmLogEnabled;

		// Token: 0x04003FCD RID: 16333
		public static readonly ADPropertyDefinition PoisonMessageDetectionEnabled = ServerSchema.PoisonMessageDetectionEnabled;

		// Token: 0x04003FCE RID: 16334
		public static readonly ADPropertyDefinition AntispamAgentsEnabled = ServerSchema.AntispamAgentsEnabled;

		// Token: 0x04003FCF RID: 16335
		public static readonly ADPropertyDefinition RootDropDirectoryPath = ServerSchema.RootDropDirectoryPath;

		// Token: 0x04003FD0 RID: 16336
		public static readonly ADPropertyDefinition RecipientValidationCacheEnabled = ServerSchema.RecipientValidationCacheEnabled;

		// Token: 0x04003FD1 RID: 16337
		public static readonly ADPropertyDefinition AntispamUpdatesEnabled = ServerSchema.AntispamUpdatesEnabled;

		// Token: 0x04003FD2 RID: 16338
		public static readonly ADPropertyDefinition InternalTransportCertificateThumbprint = ServerSchema.InternalTransportCertificateThumbprint;

		// Token: 0x04003FD3 RID: 16339
		public static readonly ADPropertyDefinition TransportSyncEnabled = ServerSchema.TransportSyncEnabled;

		// Token: 0x04003FD4 RID: 16340
		public static readonly ADPropertyDefinition TransportSyncPopEnabled = ServerSchema.TransportSyncPopEnabled;

		// Token: 0x04003FD5 RID: 16341
		public static readonly ADPropertyDefinition WindowsLiveHotmailTransportSyncEnabled = ServerSchema.WindowsLiveHotmailTransportSyncEnabled;

		// Token: 0x04003FD6 RID: 16342
		public static readonly ADPropertyDefinition TransportSyncExchangeEnabled = ServerSchema.TransportSyncExchangeEnabled;

		// Token: 0x04003FD7 RID: 16343
		public static readonly ADPropertyDefinition TransportSyncImapEnabled = ServerSchema.TransportSyncImapEnabled;

		// Token: 0x04003FD8 RID: 16344
		public static readonly ADPropertyDefinition MaxNumberOfTransportSyncAttempts = ServerSchema.MaxNumberOfTransportSyncAttempts;

		// Token: 0x04003FD9 RID: 16345
		public static readonly ADPropertyDefinition MaxActiveTransportSyncJobsPerProcessor = ServerSchema.MaxActiveTransportSyncJobsPerProcessor;

		// Token: 0x04003FDA RID: 16346
		public static readonly ADPropertyDefinition HttpTransportSyncProxyServer = ServerSchema.HttpTransportSyncProxyServer;

		// Token: 0x04003FDB RID: 16347
		public static readonly ADPropertyDefinition HttpProtocolLogEnabled = ServerSchema.HttpProtocolLogEnabled;

		// Token: 0x04003FDC RID: 16348
		public static readonly ADPropertyDefinition HttpProtocolLogFilePath = ServerSchema.HttpProtocolLogFilePath;

		// Token: 0x04003FDD RID: 16349
		public static readonly ADPropertyDefinition HttpProtocolLogMaxAge = ServerSchema.HttpProtocolLogMaxAge;

		// Token: 0x04003FDE RID: 16350
		public static readonly ADPropertyDefinition HttpProtocolLogMaxDirectorySize = ServerSchema.HttpProtocolLogMaxDirectorySize;

		// Token: 0x04003FDF RID: 16351
		public static readonly ADPropertyDefinition HttpProtocolLogMaxFileSize = ServerSchema.HttpProtocolLogMaxFileSize;

		// Token: 0x04003FE0 RID: 16352
		public static readonly ADPropertyDefinition HttpProtocolLogLoggingLevel = ServerSchema.HttpProtocolLogLoggingLevel;

		// Token: 0x04003FE1 RID: 16353
		public static readonly ADPropertyDefinition TransportSyncLogEnabled = ServerSchema.TransportSyncLogEnabled;

		// Token: 0x04003FE2 RID: 16354
		public static readonly ADPropertyDefinition TransportSyncLogFilePath = ServerSchema.TransportSyncLogFilePath;

		// Token: 0x04003FE3 RID: 16355
		public static readonly ADPropertyDefinition TransportSyncLogLoggingLevel = ServerSchema.TransportSyncLogLoggingLevel;

		// Token: 0x04003FE4 RID: 16356
		public static readonly ADPropertyDefinition TransportSyncLogMaxAge = ServerSchema.TransportSyncLogMaxAge;

		// Token: 0x04003FE5 RID: 16357
		public static readonly ADPropertyDefinition TransportSyncLogMaxDirectorySize = ServerSchema.TransportSyncLogMaxDirectorySize;

		// Token: 0x04003FE6 RID: 16358
		public static readonly ADPropertyDefinition TransportSyncLogMaxFileSize = ServerSchema.TransportSyncLogMaxFileSize;

		// Token: 0x04003FE7 RID: 16359
		public static readonly ADPropertyDefinition TransportSyncAccountsPoisonDetectionEnabled = ServerSchema.TransportSyncAccountsPoisonDetectionEnabled;

		// Token: 0x04003FE8 RID: 16360
		public static readonly ADPropertyDefinition TransportSyncAccountsPoisonAccountThreshold = ServerSchema.TransportSyncAccountsPoisonAccountThreshold;

		// Token: 0x04003FE9 RID: 16361
		public static readonly ADPropertyDefinition TransportSyncAccountsPoisonItemThreshold = ServerSchema.TransportSyncAccountsPoisonItemThreshold;

		// Token: 0x04003FEA RID: 16362
		public static readonly ADPropertyDefinition TransportSyncRemoteConnectionTimeout = ServerSchema.TransportSyncRemoteConnectionTimeout;

		// Token: 0x04003FEB RID: 16363
		public static readonly ADPropertyDefinition TransportSyncMaxDownloadSizePerItem = ServerSchema.TransportSyncMaxDownloadSizePerItem;

		// Token: 0x04003FEC RID: 16364
		public static readonly ADPropertyDefinition TransportSyncMaxDownloadSizePerConnection = ServerSchema.TransportSyncMaxDownloadSizePerConnection;

		// Token: 0x04003FED RID: 16365
		public static readonly ADPropertyDefinition TransportSyncMaxDownloadItemsPerConnection = ServerSchema.TransportSyncMaxDownloadItemsPerConnection;

		// Token: 0x04003FEE RID: 16366
		public static readonly ADPropertyDefinition DeltaSyncClientCertificateThumbprint = ServerSchema.DeltaSyncClientCertificateThumbprint;

		// Token: 0x04003FEF RID: 16367
		public static readonly ADPropertyDefinition IntraOrgConnectorSmtpMaxMessagesPerConnection = ServerSchema.IntraOrgConnectorSmtpMaxMessagesPerConnection;

		// Token: 0x04003FF0 RID: 16368
		public static readonly ADPropertyDefinition TransportSyncLinkedInEnabled = ServerSchema.TransportSyncLinkedInEnabled;

		// Token: 0x04003FF1 RID: 16369
		public static readonly ADPropertyDefinition TransportSyncFacebookEnabled = ServerSchema.TransportSyncFacebookEnabled;
	}
}

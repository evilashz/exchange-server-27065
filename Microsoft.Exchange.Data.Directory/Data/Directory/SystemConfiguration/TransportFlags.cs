using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000581 RID: 1409
	[Flags]
	internal enum TransportFlags
	{
		// Token: 0x04002C73 RID: 11379
		None = 0,
		// Token: 0x04002C74 RID: 11380
		MessageTrackingLogEnabled = 1,
		// Token: 0x04002C75 RID: 11381
		ExternalDNSAdapterDisabled = 2,
		// Token: 0x04002C76 RID: 11382
		InternalDNSAdapterDisabled = 4,
		// Token: 0x04002C77 RID: 11383
		PoisonMessageDetectionEnabled = 8,
		// Token: 0x04002C78 RID: 11384
		IrmLogEnabled = 1024,
		// Token: 0x04002C79 RID: 11385
		RecipientValidationCacheEnabled = 2048,
		// Token: 0x04002C7A RID: 11386
		AntispamAgentsEnabled = 4096,
		// Token: 0x04002C7B RID: 11387
		ConnectivityLogEnabled = 8192,
		// Token: 0x04002C7C RID: 11388
		MessageTrackingLogSubjectLoggingEnabled = 16384,
		// Token: 0x04002C7D RID: 11389
		PipelineTracingEnabled = 32768,
		// Token: 0x04002C7E RID: 11390
		GatewayEdgeSyncSubscribed = 65536,
		// Token: 0x04002C7F RID: 11391
		AntispamUpdatesEnabled = 524288,
		// Token: 0x04002C80 RID: 11392
		ContentConversionTracingEnabled = 1048576,
		// Token: 0x04002C81 RID: 11393
		UseDowngradedExchangeServerAuth = 2097152,
		// Token: 0x04002C82 RID: 11394
		MailboxDeliverySmtpUtf8Enabled = 4194304
	}
}

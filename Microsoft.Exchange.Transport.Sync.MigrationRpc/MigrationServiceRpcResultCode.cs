using System;

namespace Microsoft.Exchange.Transport.Sync.Migration.Rpc
{
	// Token: 0x0200000F RID: 15
	internal enum MigrationServiceRpcResultCode
	{
		// Token: 0x0400004B RID: 75
		Success = 4097,
		// Token: 0x0400004C RID: 76
		VersionMismatchError = 8193,
		// Token: 0x0400004D RID: 77
		ArgumentMismatchError,
		// Token: 0x0400004E RID: 78
		ServerShutdown = 40963,
		// Token: 0x0400004F RID: 79
		UnknownMethodError = 8196,
		// Token: 0x04000050 RID: 80
		ResultParseError,
		// Token: 0x04000051 RID: 81
		IncorrectMethodInvokedError,
		// Token: 0x04000052 RID: 82
		InvalidSubscriptionAction,
		// Token: 0x04000053 RID: 83
		PropertyBagMissingError,
		// Token: 0x04000054 RID: 84
		ServerNotInitialized = 40969,
		// Token: 0x04000055 RID: 85
		InvalidSubscriptionMessageId = 16385,
		// Token: 0x04000056 RID: 86
		SubscriptionCreationFailed,
		// Token: 0x04000057 RID: 87
		MailboxNotFound = 16643,
		// Token: 0x04000058 RID: 88
		SubscriptionNotFound,
		// Token: 0x04000059 RID: 89
		SubscriptionUpdateFailed = 16389,
		// Token: 0x0400005A RID: 90
		MigrationJobNotFound,
		// Token: 0x0400005B RID: 91
		MigrationTransientError = 49159,
		// Token: 0x0400005C RID: 92
		StorageTransientError,
		// Token: 0x0400005D RID: 93
		MigrationPermanentError = 16393,
		// Token: 0x0400005E RID: 94
		SubscriptionAlreadyFinalized,
		// Token: 0x0400005F RID: 95
		StoragePermanentError = 16400,
		// Token: 0x04000060 RID: 96
		RpcException = 8209,
		// Token: 0x04000061 RID: 97
		RpcTransientException = 40978
	}
}

using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000005 RID: 5
	public enum LogField
	{
		// Token: 0x04000019 RID: 25
		[DisplayName("RequestTime")]
		RequestTime,
		// Token: 0x0400001A RID: 26
		[DisplayName("BrokerStatus")]
		BrokerStatus,
		// Token: 0x0400001B RID: 27
		[DisplayName("Exception")]
		Exception,
		// Token: 0x0400001C RID: 28
		[DisplayName("ConsumerId")]
		ConsumerId,
		// Token: 0x0400001D RID: 29
		[DisplayName("RejectReason")]
		RejectReason,
		// Token: 0x0400001E RID: 30
		[DisplayName("NotificationType")]
		NotificationType,
		// Token: 0x0400001F RID: 31
		[DisplayName("HandlerTotalCount")]
		HandlerTotalCount,
		// Token: 0x04000020 RID: 32
		[DisplayName("HandlerFailureCount")]
		HandlerFailureCount,
		// Token: 0x04000021 RID: 33
		[DisplayName("CheckedForDatabaseChanges")]
		CheckedForDatabaseChanges,
		// Token: 0x04000022 RID: 34
		[DisplayName("FailedDatabases")]
		FailedDatabases,
		// Token: 0x04000023 RID: 35
		[DisplayName("RemoteConduitStatus")]
		RemoteConduitStatus,
		// Token: 0x04000024 RID: 36
		[DisplayName("ManagerTotalCount")]
		ManagerTotalCount,
		// Token: 0x04000025 RID: 37
		[DisplayName("ManagerCleanupCount")]
		ManagerCleanupCount,
		// Token: 0x04000026 RID: 38
		[DisplayName("ManagerFailureCount")]
		ManagerFailureCount
	}
}

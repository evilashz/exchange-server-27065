using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000007 RID: 7
	internal static class TestHooks
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000293C File Offset: 0x00000B3C
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002943 File Offset: 0x00000B43
		public static Action<ExEventLog.EventTuple, object[]> OnEventLog { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000294B File Offset: 0x00000B4B
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002952 File Offset: 0x00000B52
		public static Action<Guid, Guid> OnMailboxCreateEvent { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000029 RID: 41 RVA: 0x0000295A File Offset: 0x00000B5A
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002961 File Offset: 0x00000B61
		public static Action<Guid, Guid> OnMailboxRemoveEvent { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002969 File Offset: 0x00000B69
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002970 File Offset: 0x00000B70
		public static Action<BrokerDatabaseData> OnDatabaseAddedEvent { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002978 File Offset: 0x00000B78
		// (set) Token: 0x0600002E RID: 46 RVA: 0x0000297F File Offset: 0x00000B7F
		public static Action<BrokerDatabaseData> OnDatabaseRemovedEvent { get; set; }
	}
}

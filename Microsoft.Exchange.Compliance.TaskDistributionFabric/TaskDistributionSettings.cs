using System;

namespace Microsoft.Exchange.Compliance.TaskDistributionFabric
{
	// Token: 0x02000003 RID: 3
	internal static class TaskDistributionSettings
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		static TaskDistributionSettings()
		{
			TaskDistributionSettings.MaxQueuePerBlock = 100;
			TaskDistributionSettings.ApplicationExecutionTime = TimeSpan.FromMinutes(5.0);
			TaskDistributionSettings.DataLookupTime = TimeSpan.FromMinutes(1.0);
			TaskDistributionSettings.DispatchQueueTime = TimeSpan.FromMilliseconds(500.0);
			TaskDistributionSettings.GeneralOperationTime = TimeSpan.FromMinutes(1.0);
			TaskDistributionSettings.IncomingEntryExpiryTime = TimeSpan.FromMinutes(5.0);
			TaskDistributionSettings.OutgoingEntryExpiryTime = TimeSpan.FromMinutes(5.0);
			TaskDistributionSettings.RemoteExecutionTime = TimeSpan.FromMinutes(3.0);
			TaskDistributionSettings.IncomingEntryRetriesToAbandon = 5;
			TaskDistributionSettings.OutgoingEntryRetriesToAbandon = 5;
			TaskDistributionSettings.IncomingEntryRetriesToFailure = 3;
			TaskDistributionSettings.OutgoingEntryRetriesToFailure = 3;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002187 File Offset: 0x00000387
		// (set) Token: 0x06000003 RID: 3 RVA: 0x0000218E File Offset: 0x0000038E
		public static int MaxQueuePerBlock { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002196 File Offset: 0x00000396
		// (set) Token: 0x06000005 RID: 5 RVA: 0x0000219D File Offset: 0x0000039D
		public static bool EnableDispatchQueue { get; set; } = false;

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000021A5 File Offset: 0x000003A5
		// (set) Token: 0x06000007 RID: 7 RVA: 0x000021AC File Offset: 0x000003AC
		public static TimeSpan DispatchQueueTime { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000021B4 File Offset: 0x000003B4
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000021BB File Offset: 0x000003BB
		public static TimeSpan DataLookupTime { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000021C3 File Offset: 0x000003C3
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000021CA File Offset: 0x000003CA
		public static TimeSpan ApplicationExecutionTime { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000021D2 File Offset: 0x000003D2
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000021D9 File Offset: 0x000003D9
		public static TimeSpan GeneralOperationTime { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000021E1 File Offset: 0x000003E1
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000021E8 File Offset: 0x000003E8
		public static TimeSpan RemoteExecutionTime { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000021F0 File Offset: 0x000003F0
		// (set) Token: 0x06000011 RID: 17 RVA: 0x000021F7 File Offset: 0x000003F7
		public static TimeSpan IncomingEntryExpiryTime { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000021FF File Offset: 0x000003FF
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002206 File Offset: 0x00000406
		public static TimeSpan OutgoingEntryExpiryTime { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000220E File Offset: 0x0000040E
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002215 File Offset: 0x00000415
		public static int OutgoingEntryRetriesToFailure { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000221D File Offset: 0x0000041D
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002224 File Offset: 0x00000424
		public static int OutgoingEntryRetriesToAbandon { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000018 RID: 24 RVA: 0x0000222C File Offset: 0x0000042C
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002233 File Offset: 0x00000433
		public static int IncomingEntryRetriesToFailure { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000223B File Offset: 0x0000043B
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002242 File Offset: 0x00000442
		public static int IncomingEntryRetriesToAbandon { get; set; }
	}
}

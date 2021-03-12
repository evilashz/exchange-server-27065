using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000005 RID: 5
	public class UserWorkloadManagerResult
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000223C File Offset: 0x0000043C
		// (set) Token: 0x06000011 RID: 17 RVA: 0x00002244 File Offset: 0x00000444
		public int MaxTasksQueued { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000224D File Offset: 0x0000044D
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002255 File Offset: 0x00000455
		public int MaxThreadCount { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000225E File Offset: 0x0000045E
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002266 File Offset: 0x00000466
		public string MaxDelayCacheTime { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000226F File Offset: 0x0000046F
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002277 File Offset: 0x00000477
		public int CurrentWorkerThreads { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002280 File Offset: 0x00000480
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002288 File Offset: 0x00000488
		public bool IsQueueFull { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002291 File Offset: 0x00000491
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002299 File Offset: 0x00000499
		public bool Canceled { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000022A2 File Offset: 0x000004A2
		// (set) Token: 0x0600001D RID: 29 RVA: 0x000022AA File Offset: 0x000004AA
		public int TotalTaskCount { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000022B3 File Offset: 0x000004B3
		// (set) Token: 0x0600001F RID: 31 RVA: 0x000022BB File Offset: 0x000004BB
		public int QueuedTaskCount { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000022C4 File Offset: 0x000004C4
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000022CC File Offset: 0x000004CC
		public string SyncToAsyncRatio { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000022D5 File Offset: 0x000004D5
		// (set) Token: 0x06000023 RID: 35 RVA: 0x000022DD File Offset: 0x000004DD
		public bool SynchronousExecutionAllowed { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000022E6 File Offset: 0x000004E6
		// (set) Token: 0x06000025 RID: 37 RVA: 0x000022EE File Offset: 0x000004EE
		public int DelayedTaskCount { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000022F7 File Offset: 0x000004F7
		// (set) Token: 0x06000027 RID: 39 RVA: 0x000022FF File Offset: 0x000004FF
		public int TaskSubmissionFailuresPerMinute { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002308 File Offset: 0x00000508
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00002310 File Offset: 0x00000510
		public int TasksCompletedPerMinute { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002319 File Offset: 0x00000519
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002321 File Offset: 0x00000521
		public int TaskTimeoutsPerMinute { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002C RID: 44 RVA: 0x0000232A File Offset: 0x0000052A
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00002332 File Offset: 0x00000532
		public List<WLMTaskDetails> QueuedTasks { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600002E RID: 46 RVA: 0x0000233B File Offset: 0x0000053B
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002343 File Offset: 0x00000543
		public List<WLMTaskDetails> DelayedTasks { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000030 RID: 48 RVA: 0x0000234C File Offset: 0x0000054C
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002354 File Offset: 0x00000554
		public List<WLMTaskDetails> ExecutingTasks { get; set; }
	}
}

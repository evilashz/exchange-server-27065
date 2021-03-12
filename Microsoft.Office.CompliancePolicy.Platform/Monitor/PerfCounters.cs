using System;
using System.Collections.Generic;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Monitor
{
	// Token: 0x0200007F RID: 127
	public static class PerfCounters
	{
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000346 RID: 838 RVA: 0x0000B63B File Offset: 0x0000983B
		public static Dictionary<ConfigurationObjectType, KeyValuePair<string, string>> PolicyObjectWsPerfCounters
		{
			get
			{
				return PerfCounters.policyObjectWsPerfCounters;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0000B642 File Offset: 0x00009842
		public static Dictionary<ConfigurationObjectType, KeyValuePair<string, string>> PolicyObjectCrudMgrPerfCounters
		{
			get
			{
				return PerfCounters.policyObjectCrudMgrPerfCounters;
			}
		}

		// Token: 0x040001F3 RID: 499
		public const string RecommendedCategoryName = "MSUnified Compliance Sync";

		// Token: 0x040001F4 RID: 500
		public const string TotalProcessingTimePerSyncRequest = "Total Processing Time Per Sync Request";

		// Token: 0x040001F5 RID: 501
		public const string TotalProcessingTimePerSyncRequestBase = "Total Processing Time Per Sync Request Base";

		// Token: 0x040001F6 RID: 502
		public const string ExecutionDelayTimePerSyncRequest = "Execution Delay Time Per Sync Request";

		// Token: 0x040001F7 RID: 503
		public const string ExecutionDelayTimePerSyncRequestBase = "Execution Delay Time Per Sync Request Base";

		// Token: 0x040001F8 RID: 504
		public const string InitializationTimePerSyncRequest = "Initialization Time Per Sync Request";

		// Token: 0x040001F9 RID: 505
		public const string InitializationTimePerSyncRequestBase = "Initialization Time Per Sync Request Base";

		// Token: 0x040001FA RID: 506
		public const string TenantInfoProcessingTimePerSyncRequest = "TenantInfo Processing Time Per Sync Request";

		// Token: 0x040001FB RID: 507
		public const string TenantInfoProcessingTimePerSyncRequestBase = "TenantInfo Processing Time Per Sync Request Base";

		// Token: 0x040001FC RID: 508
		public const string PersistentQueueProcessingTimePerSyncRequest = "Persistent Queue Processing Time Per Sync Request";

		// Token: 0x040001FD RID: 509
		public const string PersistentQueueProcessingTimePerSyncRequestBase = "Persistent Queue Processing Time Per Sync Request Base";

		// Token: 0x040001FE RID: 510
		public const string ProcessedSyncRequestNumberPerSecond = "Processed Sync Request Number Per Second";

		// Token: 0x040001FF RID: 511
		public const string ProcessedSyncRequestNumber = "Processed Sync Request Number";

		// Token: 0x04000200 RID: 512
		public const string SuccessfulSyncRequestNumberPerSecond = "Successful Sync Request Number Per Second";

		// Token: 0x04000201 RID: 513
		public const string SuccessfulSyncRequestNumber = "Successful Sync Request Number";

		// Token: 0x04000202 RID: 514
		public const string PolicySyncWsCallTransientErrorNumberPerSecond = "Policy Sync Ws Call Transient Error Number Per Second";

		// Token: 0x04000203 RID: 515
		public const string PolicySyncWsCallTransientErrorNumber = "Policy Sync Ws Call Transient Error Number";

		// Token: 0x04000204 RID: 516
		public const string PolicySyncCrudMgrTransientErrorNumberPerSecond = "Policy Sync CrudMgr Transient Error Number Per Second";

		// Token: 0x04000205 RID: 517
		public const string PolicySyncCrudMgrTransientErrorNumber = "Policy Sync CrudMgr Transient Error Number";

		// Token: 0x04000206 RID: 518
		public const string PolicySyncWsCallPermanentErrorNumberPerSecond = "Policy Sync Ws Call Permanent Error Number Per Second";

		// Token: 0x04000207 RID: 519
		public const string PolicySyncWsCallPermanentErrorNumber = "Policy Sync Ws Call Permanent Error Number";

		// Token: 0x04000208 RID: 520
		public const string PolicySyncCrudMgrPermanentErrorNumberPerSecond = "Policy Sync CrudMgr Permanent Error Number Per Second";

		// Token: 0x04000209 RID: 521
		public const string PolicySyncCrudMgrPermanentErrorNumber = "Policy Sync CrudMgr Permanent Error Number";

		// Token: 0x0400020A RID: 522
		public const string StatusUpdatePermanentErrorNumberPerSecond = "Status Update Permanent Error Number Per Second";

		// Token: 0x0400020B RID: 523
		public const string StatusUpdatePermanentErrorNumber = "Status Update Permanent Error Number";

		// Token: 0x0400020C RID: 524
		public const string SyncRequestRetryNumberPerSecond = "Sync Request Retry Number Per Second";

		// Token: 0x0400020D RID: 525
		public const string SyncRequestRetryNumber = "Sync Request Retry Number";

		// Token: 0x0400020E RID: 526
		public const string WsCallNumberPerSyncRequest = "Ws Call Number Per Sync Request";

		// Token: 0x0400020F RID: 527
		public const string WsCallNumberPerSyncRequestBase = "Ws Call Number Per Sync Request Base";

		// Token: 0x04000210 RID: 528
		public const string TenantNumberPerSyncRequest = "Tenant Number Per Sync Request";

		// Token: 0x04000211 RID: 529
		public const string TenantNumberPerSyncRequestBase = "Tenant Number Per Sync Request Base";

		// Token: 0x04000212 RID: 530
		public const string ObjectNumberPerSyncRequest = "Object Number Per Sync Request";

		// Token: 0x04000213 RID: 531
		public const string ObjectNumberPerSyncRequestBase = "Object Number Per Sync Request Base";

		// Token: 0x04000214 RID: 532
		public const string DarTasksTransientFailed = "DarTasksTransientFailed";

		// Token: 0x04000215 RID: 533
		public const string DarTasksInStateNone = "DarTasksInStateNone";

		// Token: 0x04000216 RID: 534
		public const string DarTasksInStateReady = "DarTasksInStateReady";

		// Token: 0x04000217 RID: 535
		public const string DarTasksInStateRunning = "DarTasksInStateRunning";

		// Token: 0x04000218 RID: 536
		public const string DarTasksInStateCompleted = "DarTasksInStateCompleted";

		// Token: 0x04000219 RID: 537
		public const string DarTasksInStateFailed = "DarTasksInStateFailed";

		// Token: 0x0400021A RID: 538
		public const string DarTasksInStateCancelled = "DarTasksInStateCancelled";

		// Token: 0x0400021B RID: 539
		public const string DarTaskAverageDuration = "DarTaskAverageDuration";

		// Token: 0x0400021C RID: 540
		public const string DarTaskAverageDurationBase = "DarTaskAverageDurationBase";

		// Token: 0x0400021D RID: 541
		private static Dictionary<ConfigurationObjectType, KeyValuePair<string, string>> policyObjectWsPerfCounters = new Dictionary<ConfigurationObjectType, KeyValuePair<string, string>>
		{
			{
				ConfigurationObjectType.Policy,
				new KeyValuePair<string, string>("Ws Call Time For Policy Per Sync Request", "Ws Call Time For Policy Per Sync Request Base")
			},
			{
				ConfigurationObjectType.Rule,
				new KeyValuePair<string, string>("Ws Call Time For Rule Per Sync Request", "Ws Call Time For Rule Per Sync Request Base")
			},
			{
				ConfigurationObjectType.Binding,
				new KeyValuePair<string, string>("Ws Call Time For Binding Per Sync Request", "Ws Call Time For Binding Per Sync Request Base")
			},
			{
				ConfigurationObjectType.Association,
				new KeyValuePair<string, string>("Ws Call Time For Association Per Sync Request", "Ws Call Time For Association Per Sync Request Base")
			}
		};

		// Token: 0x0400021E RID: 542
		private static Dictionary<ConfigurationObjectType, KeyValuePair<string, string>> policyObjectCrudMgrPerfCounters = new Dictionary<ConfigurationObjectType, KeyValuePair<string, string>>
		{
			{
				ConfigurationObjectType.Policy,
				new KeyValuePair<string, string>("CrudMgr Time For Policy Per Sync Request", "CrudMgr Time For Policy Per Sync Request Base")
			},
			{
				ConfigurationObjectType.Rule,
				new KeyValuePair<string, string>("CrudMgr Time For Rule Per Sync Request", "CrudMgr Time For Rule Per Sync Request Base")
			},
			{
				ConfigurationObjectType.Binding,
				new KeyValuePair<string, string>("CrudMgr Time For Binding Per Sync Request", "CrudMgr Time For Binding Per Sync Request Base")
			},
			{
				ConfigurationObjectType.Association,
				new KeyValuePair<string, string>("CrudMgr Time For Association Per Sync Request", "CrudMgr Time For Association Per Sync Request Base")
			}
		};
	}
}

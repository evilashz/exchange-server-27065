using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.CommonCode
{
	// Token: 0x0200001D RID: 29
	internal static class UnifiedPolicySyncPerfCounters
	{
		// Token: 0x060000E2 RID: 226 RVA: 0x00005824 File Offset: 0x00003A24
		public static void GetPerfCounterInfo(XElement element)
		{
			if (UnifiedPolicySyncPerfCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in UnifiedPolicySyncPerfCounters.AllCounters)
			{
				try
				{
					element.Add(new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.CounterName), exPerformanceCounter.NextValue()));
				}
				catch (XmlException ex)
				{
					XElement content = new XElement("Error", ex.Message);
					element.Add(content);
				}
			}
		}

		// Token: 0x04000056 RID: 86
		public const string CategoryName = "MSUnified Compliance Sync";

		// Token: 0x04000057 RID: 87
		public static readonly ExPerformanceCounter TotalProcessingTimePerSyncRequest = new ExPerformanceCounter("MSUnified Compliance Sync", "Total Processing Time Per Sync Request", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000058 RID: 88
		public static readonly ExPerformanceCounter TotalProcessingTimePerSyncRequestBase = new ExPerformanceCounter("MSUnified Compliance Sync", "Total Processing Time Per Sync Request Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000059 RID: 89
		public static readonly ExPerformanceCounter ExecutionDelayTimePerSyncRequest = new ExPerformanceCounter("MSUnified Compliance Sync", "Execution Delay Time Per Sync Request", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400005A RID: 90
		public static readonly ExPerformanceCounter ExecutionDelayTimePerSyncRequestBase = new ExPerformanceCounter("MSUnified Compliance Sync", "Execution Delay Time Per Sync Request Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400005B RID: 91
		public static readonly ExPerformanceCounter InitializationTimePerSyncRequest = new ExPerformanceCounter("MSUnified Compliance Sync", "Initialization Time Per Sync Request", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400005C RID: 92
		public static readonly ExPerformanceCounter InitializationTimePerSyncRequestBase = new ExPerformanceCounter("MSUnified Compliance Sync", "Initialization Time Per Sync Request Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400005D RID: 93
		public static readonly ExPerformanceCounter WsCallTimeForPolicyPerSyncRequest = new ExPerformanceCounter("MSUnified Compliance Sync", "Ws Call Time For Policy Per Sync Request", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400005E RID: 94
		public static readonly ExPerformanceCounter WsCallTimeForPolicyPerSyncRequestBase = new ExPerformanceCounter("MSUnified Compliance Sync", "Ws Call Time For Policy Per Sync Request Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400005F RID: 95
		public static readonly ExPerformanceCounter WsCallTimeForRulePerSyncRequest = new ExPerformanceCounter("MSUnified Compliance Sync", "Ws Call Time For Rule Per Sync Request", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000060 RID: 96
		public static readonly ExPerformanceCounter WsCallTimeForRulePerSyncRequestBase = new ExPerformanceCounter("MSUnified Compliance Sync", "Ws Call Time For Rule Per Sync Request Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000061 RID: 97
		public static readonly ExPerformanceCounter WsCallTimeForBindingPerSyncRequest = new ExPerformanceCounter("MSUnified Compliance Sync", "Ws Call Time For Binding Per Sync Request", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000062 RID: 98
		public static readonly ExPerformanceCounter WsCallTimeForBindingPerSyncRequestBase = new ExPerformanceCounter("MSUnified Compliance Sync", "Ws Call Time For Binding Per Sync Request Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000063 RID: 99
		public static readonly ExPerformanceCounter WsCallTimeForAssociationPerSyncRequest = new ExPerformanceCounter("MSUnified Compliance Sync", "Ws Call Time For Association Per Sync Request", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000064 RID: 100
		public static readonly ExPerformanceCounter WsCallTimeForAssociationPerSyncRequestBase = new ExPerformanceCounter("MSUnified Compliance Sync", "Ws Call Time For Association Per Sync Request Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000065 RID: 101
		public static readonly ExPerformanceCounter CrudMgrTimeForPolicyPerSyncRequest = new ExPerformanceCounter("MSUnified Compliance Sync", "CrudMgr Time For Policy Per Sync Request", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000066 RID: 102
		public static readonly ExPerformanceCounter CrudMgrTimeForPolicyPerSyncRequestBase = new ExPerformanceCounter("MSUnified Compliance Sync", "CrudMgr Time For Policy Per Sync Request Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000067 RID: 103
		public static readonly ExPerformanceCounter CrudMgrTimeForRulePerSyncRequest = new ExPerformanceCounter("MSUnified Compliance Sync", "CrudMgr Time For Rule Per Sync Request", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000068 RID: 104
		public static readonly ExPerformanceCounter CrudMgrTimeForRulePerSyncRequestBase = new ExPerformanceCounter("MSUnified Compliance Sync", "CrudMgr Time For Rule Per Sync Request Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000069 RID: 105
		public static readonly ExPerformanceCounter CrudMgrTimeForBindingPerSyncRequest = new ExPerformanceCounter("MSUnified Compliance Sync", "CrudMgr Time For Binding Per Sync Request", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400006A RID: 106
		public static readonly ExPerformanceCounter CrudMgrTimeForBindingPerSyncRequestBase = new ExPerformanceCounter("MSUnified Compliance Sync", "CrudMgr Time For Binding Per Sync Request Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400006B RID: 107
		public static readonly ExPerformanceCounter CrudMgrTimeForAssociationPerSyncRequest = new ExPerformanceCounter("MSUnified Compliance Sync", "CrudMgr Time For Association Per Sync Request", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400006C RID: 108
		public static readonly ExPerformanceCounter CrudMgrTimeForAssociationPerSyncRequestBase = new ExPerformanceCounter("MSUnified Compliance Sync", "CrudMgr Time For Association Per Sync Request Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400006D RID: 109
		public static readonly ExPerformanceCounter TenantInfoProcessingTimePerSyncRequest = new ExPerformanceCounter("MSUnified Compliance Sync", "TenantInfo Processing Time Per Sync Request", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400006E RID: 110
		public static readonly ExPerformanceCounter TenantInfoProcessingTimePerSyncRequestBase = new ExPerformanceCounter("MSUnified Compliance Sync", "TenantInfo Processing Time Per Sync Request Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400006F RID: 111
		public static readonly ExPerformanceCounter PersistentQueueProcessingTimePerSyncRequest = new ExPerformanceCounter("MSUnified Compliance Sync", "Persistent Queue Processing Time Per Sync Request", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000070 RID: 112
		public static readonly ExPerformanceCounter PersistentQueueProcessingTimePerSyncRequestBase = new ExPerformanceCounter("MSUnified Compliance Sync", "Persistent Queue Processing Time Per Sync Request Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000071 RID: 113
		public static readonly ExPerformanceCounter ProcessedSyncRequestNumberPerSecond = new ExPerformanceCounter("MSUnified Compliance Sync", "Processed Sync Request Number Per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000072 RID: 114
		public static readonly ExPerformanceCounter ProcessedSyncRequestNumber = new ExPerformanceCounter("MSUnified Compliance Sync", "Processed Sync Request Number", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000073 RID: 115
		public static readonly ExPerformanceCounter SuccessfulSyncRequestNumberPerSecond = new ExPerformanceCounter("MSUnified Compliance Sync", "Successful Sync Request Number Per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000074 RID: 116
		public static readonly ExPerformanceCounter SuccessfulSyncRequestNumber = new ExPerformanceCounter("MSUnified Compliance Sync", "Successful Sync Request Number", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000075 RID: 117
		public static readonly ExPerformanceCounter PolicySyncWsCallTransientErrorNumberPerSecond = new ExPerformanceCounter("MSUnified Compliance Sync", "Policy Sync Ws Call Transient Error Number Per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000076 RID: 118
		public static readonly ExPerformanceCounter PolicySyncWsCallTransientErrorNumber = new ExPerformanceCounter("MSUnified Compliance Sync", "Policy Sync Ws Call Transient Error Number", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000077 RID: 119
		public static readonly ExPerformanceCounter PolicySyncCrudMgrTransientErrorNumberPerSecond = new ExPerformanceCounter("MSUnified Compliance Sync", "Policy Sync CrudMgr Transient Error Number Per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000078 RID: 120
		public static readonly ExPerformanceCounter PolicySyncCrudMgrTransientErrorNumber = new ExPerformanceCounter("MSUnified Compliance Sync", "Policy Sync CrudMgr Transient Error Number", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000079 RID: 121
		public static readonly ExPerformanceCounter PolicySyncWsCallPermanentErrorNumberPerSecond = new ExPerformanceCounter("MSUnified Compliance Sync", "Policy Sync Ws Call Permanent Error Number Per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400007A RID: 122
		public static readonly ExPerformanceCounter PolicySyncWsCallPermanentErrorNumber = new ExPerformanceCounter("MSUnified Compliance Sync", "Policy Sync Ws Call Permanent Error Number", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400007B RID: 123
		public static readonly ExPerformanceCounter PolicySyncCrudMgrPermanentErrorNumberPerSecond = new ExPerformanceCounter("MSUnified Compliance Sync", "Policy Sync CrudMgr Permanent Error Number Per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400007C RID: 124
		public static readonly ExPerformanceCounter PolicySyncCrudMgrPermanentErrorNumber = new ExPerformanceCounter("MSUnified Compliance Sync", "Policy Sync CrudMgr Permanent Error Number", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400007D RID: 125
		public static readonly ExPerformanceCounter StatusUpdatePermanentErrorNumberPerSecond = new ExPerformanceCounter("MSUnified Compliance Sync", "Status Update Permanent Error Number Per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400007E RID: 126
		public static readonly ExPerformanceCounter StatusUpdatePermanentErrorNumber = new ExPerformanceCounter("MSUnified Compliance Sync", "Status Update Permanent Error Number", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400007F RID: 127
		public static readonly ExPerformanceCounter SyncRequestRetryNumberPerSecond = new ExPerformanceCounter("MSUnified Compliance Sync", "Sync Request Retry Number Per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000080 RID: 128
		public static readonly ExPerformanceCounter SyncRequestRetryNumber = new ExPerformanceCounter("MSUnified Compliance Sync", "Sync Request Retry Number", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000081 RID: 129
		public static readonly ExPerformanceCounter WsCallNumberPerSyncRequest = new ExPerformanceCounter("MSUnified Compliance Sync", "Ws Call Number Per Sync Request", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000082 RID: 130
		public static readonly ExPerformanceCounter WsCallNumberPerSyncRequestBase = new ExPerformanceCounter("MSUnified Compliance Sync", "Ws Call Number Per Sync Request Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000083 RID: 131
		public static readonly ExPerformanceCounter TenantNumberPerSyncRequest = new ExPerformanceCounter("MSUnified Compliance Sync", "Tenant Number Per Sync Request", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000084 RID: 132
		public static readonly ExPerformanceCounter TenantNumberPerSyncRequestBase = new ExPerformanceCounter("MSUnified Compliance Sync", "Tenant Number Per Sync Request Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000085 RID: 133
		public static readonly ExPerformanceCounter ObjectNumberPerSyncRequest = new ExPerformanceCounter("MSUnified Compliance Sync", "Object Number Per Sync Request", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000086 RID: 134
		public static readonly ExPerformanceCounter ObjectNumberPerSyncRequestBase = new ExPerformanceCounter("MSUnified Compliance Sync", "Object Number Per Sync Request Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000087 RID: 135
		public static readonly ExPerformanceCounter DarTasksTransientFailed = new ExPerformanceCounter("MSUnified Compliance Sync", "DarTasksTransientFailed", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000088 RID: 136
		public static readonly ExPerformanceCounter DarTasksInStateNone = new ExPerformanceCounter("MSUnified Compliance Sync", "DarTasksInStateNone", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000089 RID: 137
		public static readonly ExPerformanceCounter DarTasksInStateReady = new ExPerformanceCounter("MSUnified Compliance Sync", "DarTasksInStateReady", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400008A RID: 138
		public static readonly ExPerformanceCounter DarTasksInStateRunning = new ExPerformanceCounter("MSUnified Compliance Sync", "DarTasksInStateRunning", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400008B RID: 139
		public static readonly ExPerformanceCounter DarTasksInStateCompleted = new ExPerformanceCounter("MSUnified Compliance Sync", "DarTasksInStateCompleted", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400008C RID: 140
		public static readonly ExPerformanceCounter DarTasksInStateFailed = new ExPerformanceCounter("MSUnified Compliance Sync", "DarTasksInStateFailed", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400008D RID: 141
		public static readonly ExPerformanceCounter DarTasksInStateCancelled = new ExPerformanceCounter("MSUnified Compliance Sync", "DarTasksInStateCancelled", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400008E RID: 142
		public static readonly ExPerformanceCounter DarTaskAverageDuration = new ExPerformanceCounter("MSUnified Compliance Sync", "DarTaskAverageDuration", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400008F RID: 143
		public static readonly ExPerformanceCounter DarTaskAverageDurationBase = new ExPerformanceCounter("MSUnified Compliance Sync", "DarTaskAverageDurationBase", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000090 RID: 144
		private static readonly ExPerformanceCounter DarQueuedGrowthRate = new ExPerformanceCounter("MSUnified Compliance Sync", "DAR Queued Growth Rate", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000091 RID: 145
		public static readonly ExPerformanceCounter DarQueueLength = new ExPerformanceCounter("MSUnified Compliance Sync", "DAR Queue Length", string.Empty, null, new ExPerformanceCounter[]
		{
			UnifiedPolicySyncPerfCounters.DarQueuedGrowthRate
		});

		// Token: 0x04000092 RID: 146
		private static readonly ExPerformanceCounter DarQueueSchedulingRate = new ExPerformanceCounter("MSUnified Compliance Sync", "DAR Queue Scheduling Rate", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000093 RID: 147
		public static readonly ExPerformanceCounter DarScheduledTasks = new ExPerformanceCounter("MSUnified Compliance Sync", "DAR Scheduled Tasks", string.Empty, null, new ExPerformanceCounter[]
		{
			UnifiedPolicySyncPerfCounters.DarQueueSchedulingRate
		});

		// Token: 0x04000094 RID: 148
		private static readonly ExPerformanceCounter DarQueueProcessingRate = new ExPerformanceCounter("MSUnified Compliance Sync", "DAR Queue Processing Rate", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000095 RID: 149
		public static readonly ExPerformanceCounter DarProcessedTasks = new ExPerformanceCounter("MSUnified Compliance Sync", "DAR Processed Tasks", string.Empty, null, new ExPerformanceCounter[]
		{
			UnifiedPolicySyncPerfCounters.DarQueueProcessingRate
		});

		// Token: 0x04000096 RID: 150
		private static readonly ExPerformanceCounter DarTasksFailuresRate = new ExPerformanceCounter("MSUnified Compliance Sync", "DAR Tasks Failures Rate", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000097 RID: 151
		public static readonly ExPerformanceCounter DarFailedTasks = new ExPerformanceCounter("MSUnified Compliance Sync", "DAR Failed Tasks", string.Empty, null, new ExPerformanceCounter[]
		{
			UnifiedPolicySyncPerfCounters.DarTasksFailuresRate
		});

		// Token: 0x04000098 RID: 152
		private static readonly ExPerformanceCounter DarTasksTransientFailureRate = new ExPerformanceCounter("MSUnified Compliance Sync", "DAR Tasks Transient Failure Rate", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000099 RID: 153
		public static readonly ExPerformanceCounter DarTransientFailedTasks = new ExPerformanceCounter("MSUnified Compliance Sync", "DAR Transient Failed Tasks", string.Empty, null, new ExPerformanceCounter[]
		{
			UnifiedPolicySyncPerfCounters.DarTasksTransientFailureRate
		});

		// Token: 0x0400009A RID: 154
		public static readonly ExPerformanceCounter DarTaskAveDuration = new ExPerformanceCounter("MSUnified Compliance Sync", "DAR Task Average Duration", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400009B RID: 155
		public static readonly ExPerformanceCounter DarTaskAveDurationBase = new ExPerformanceCounter("MSUnified Compliance Sync", "DAR Task Average Duration Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400009C RID: 156
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			UnifiedPolicySyncPerfCounters.TotalProcessingTimePerSyncRequest,
			UnifiedPolicySyncPerfCounters.TotalProcessingTimePerSyncRequestBase,
			UnifiedPolicySyncPerfCounters.ExecutionDelayTimePerSyncRequest,
			UnifiedPolicySyncPerfCounters.ExecutionDelayTimePerSyncRequestBase,
			UnifiedPolicySyncPerfCounters.InitializationTimePerSyncRequest,
			UnifiedPolicySyncPerfCounters.InitializationTimePerSyncRequestBase,
			UnifiedPolicySyncPerfCounters.WsCallTimeForPolicyPerSyncRequest,
			UnifiedPolicySyncPerfCounters.WsCallTimeForPolicyPerSyncRequestBase,
			UnifiedPolicySyncPerfCounters.WsCallTimeForRulePerSyncRequest,
			UnifiedPolicySyncPerfCounters.WsCallTimeForRulePerSyncRequestBase,
			UnifiedPolicySyncPerfCounters.WsCallTimeForBindingPerSyncRequest,
			UnifiedPolicySyncPerfCounters.WsCallTimeForBindingPerSyncRequestBase,
			UnifiedPolicySyncPerfCounters.WsCallTimeForAssociationPerSyncRequest,
			UnifiedPolicySyncPerfCounters.WsCallTimeForAssociationPerSyncRequestBase,
			UnifiedPolicySyncPerfCounters.CrudMgrTimeForPolicyPerSyncRequest,
			UnifiedPolicySyncPerfCounters.CrudMgrTimeForPolicyPerSyncRequestBase,
			UnifiedPolicySyncPerfCounters.CrudMgrTimeForRulePerSyncRequest,
			UnifiedPolicySyncPerfCounters.CrudMgrTimeForRulePerSyncRequestBase,
			UnifiedPolicySyncPerfCounters.CrudMgrTimeForBindingPerSyncRequest,
			UnifiedPolicySyncPerfCounters.CrudMgrTimeForBindingPerSyncRequestBase,
			UnifiedPolicySyncPerfCounters.CrudMgrTimeForAssociationPerSyncRequest,
			UnifiedPolicySyncPerfCounters.CrudMgrTimeForAssociationPerSyncRequestBase,
			UnifiedPolicySyncPerfCounters.TenantInfoProcessingTimePerSyncRequest,
			UnifiedPolicySyncPerfCounters.TenantInfoProcessingTimePerSyncRequestBase,
			UnifiedPolicySyncPerfCounters.PersistentQueueProcessingTimePerSyncRequest,
			UnifiedPolicySyncPerfCounters.PersistentQueueProcessingTimePerSyncRequestBase,
			UnifiedPolicySyncPerfCounters.ProcessedSyncRequestNumberPerSecond,
			UnifiedPolicySyncPerfCounters.ProcessedSyncRequestNumber,
			UnifiedPolicySyncPerfCounters.SuccessfulSyncRequestNumberPerSecond,
			UnifiedPolicySyncPerfCounters.SuccessfulSyncRequestNumber,
			UnifiedPolicySyncPerfCounters.PolicySyncWsCallTransientErrorNumberPerSecond,
			UnifiedPolicySyncPerfCounters.PolicySyncWsCallTransientErrorNumber,
			UnifiedPolicySyncPerfCounters.PolicySyncCrudMgrTransientErrorNumberPerSecond,
			UnifiedPolicySyncPerfCounters.PolicySyncCrudMgrTransientErrorNumber,
			UnifiedPolicySyncPerfCounters.PolicySyncWsCallPermanentErrorNumberPerSecond,
			UnifiedPolicySyncPerfCounters.PolicySyncWsCallPermanentErrorNumber,
			UnifiedPolicySyncPerfCounters.PolicySyncCrudMgrPermanentErrorNumberPerSecond,
			UnifiedPolicySyncPerfCounters.PolicySyncCrudMgrPermanentErrorNumber,
			UnifiedPolicySyncPerfCounters.StatusUpdatePermanentErrorNumberPerSecond,
			UnifiedPolicySyncPerfCounters.StatusUpdatePermanentErrorNumber,
			UnifiedPolicySyncPerfCounters.SyncRequestRetryNumberPerSecond,
			UnifiedPolicySyncPerfCounters.SyncRequestRetryNumber,
			UnifiedPolicySyncPerfCounters.WsCallNumberPerSyncRequest,
			UnifiedPolicySyncPerfCounters.WsCallNumberPerSyncRequestBase,
			UnifiedPolicySyncPerfCounters.TenantNumberPerSyncRequest,
			UnifiedPolicySyncPerfCounters.TenantNumberPerSyncRequestBase,
			UnifiedPolicySyncPerfCounters.ObjectNumberPerSyncRequest,
			UnifiedPolicySyncPerfCounters.ObjectNumberPerSyncRequestBase,
			UnifiedPolicySyncPerfCounters.DarTasksTransientFailed,
			UnifiedPolicySyncPerfCounters.DarTasksInStateNone,
			UnifiedPolicySyncPerfCounters.DarTasksInStateReady,
			UnifiedPolicySyncPerfCounters.DarTasksInStateRunning,
			UnifiedPolicySyncPerfCounters.DarTasksInStateCompleted,
			UnifiedPolicySyncPerfCounters.DarTasksInStateFailed,
			UnifiedPolicySyncPerfCounters.DarTasksInStateCancelled,
			UnifiedPolicySyncPerfCounters.DarTaskAverageDuration,
			UnifiedPolicySyncPerfCounters.DarTaskAverageDurationBase,
			UnifiedPolicySyncPerfCounters.DarQueueLength,
			UnifiedPolicySyncPerfCounters.DarScheduledTasks,
			UnifiedPolicySyncPerfCounters.DarProcessedTasks,
			UnifiedPolicySyncPerfCounters.DarFailedTasks,
			UnifiedPolicySyncPerfCounters.DarTransientFailedTasks,
			UnifiedPolicySyncPerfCounters.DarTaskAveDuration,
			UnifiedPolicySyncPerfCounters.DarTaskAveDurationBase
		};
	}
}

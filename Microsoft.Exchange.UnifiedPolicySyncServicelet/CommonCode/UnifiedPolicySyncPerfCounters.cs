using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.CommonCode
{
	// Token: 0x02000005 RID: 5
	internal static class UnifiedPolicySyncPerfCounters
	{
		// Token: 0x06000002 RID: 2 RVA: 0x00002164 File Offset: 0x00000364
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

		// Token: 0x04000012 RID: 18
		public const string CategoryName = "MSUnified Compliance Sync";

		// Token: 0x04000013 RID: 19
		public static readonly ExPerformanceCounter TotalProcessingTimePerSyncRequest = new ExPerformanceCounter("MSUnified Compliance Sync", "Total Processing Time Per Sync Request", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000014 RID: 20
		public static readonly ExPerformanceCounter TotalProcessingTimePerSyncRequestBase = new ExPerformanceCounter("MSUnified Compliance Sync", "Total Processing Time Per Sync Request Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000015 RID: 21
		public static readonly ExPerformanceCounter ExecutionDelayTimePerSyncRequest = new ExPerformanceCounter("MSUnified Compliance Sync", "Execution Delay Time Per Sync Request", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000016 RID: 22
		public static readonly ExPerformanceCounter ExecutionDelayTimePerSyncRequestBase = new ExPerformanceCounter("MSUnified Compliance Sync", "Execution Delay Time Per Sync Request Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000017 RID: 23
		public static readonly ExPerformanceCounter InitializationTimePerSyncRequest = new ExPerformanceCounter("MSUnified Compliance Sync", "Initialization Time Per Sync Request", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000018 RID: 24
		public static readonly ExPerformanceCounter InitializationTimePerSyncRequestBase = new ExPerformanceCounter("MSUnified Compliance Sync", "Initialization Time Per Sync Request Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000019 RID: 25
		public static readonly ExPerformanceCounter WsCallTimeForPolicyPerSyncRequest = new ExPerformanceCounter("MSUnified Compliance Sync", "Ws Call Time For Policy Per Sync Request", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400001A RID: 26
		public static readonly ExPerformanceCounter WsCallTimeForPolicyPerSyncRequestBase = new ExPerformanceCounter("MSUnified Compliance Sync", "Ws Call Time For Policy Per Sync Request Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400001B RID: 27
		public static readonly ExPerformanceCounter WsCallTimeForRulePerSyncRequest = new ExPerformanceCounter("MSUnified Compliance Sync", "Ws Call Time For Rule Per Sync Request", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400001C RID: 28
		public static readonly ExPerformanceCounter WsCallTimeForRulePerSyncRequestBase = new ExPerformanceCounter("MSUnified Compliance Sync", "Ws Call Time For Rule Per Sync Request Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400001D RID: 29
		public static readonly ExPerformanceCounter WsCallTimeForBindingPerSyncRequest = new ExPerformanceCounter("MSUnified Compliance Sync", "Ws Call Time For Binding Per Sync Request", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400001E RID: 30
		public static readonly ExPerformanceCounter WsCallTimeForBindingPerSyncRequestBase = new ExPerformanceCounter("MSUnified Compliance Sync", "Ws Call Time For Binding Per Sync Request Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400001F RID: 31
		public static readonly ExPerformanceCounter WsCallTimeForAssociationPerSyncRequest = new ExPerformanceCounter("MSUnified Compliance Sync", "Ws Call Time For Association Per Sync Request", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000020 RID: 32
		public static readonly ExPerformanceCounter WsCallTimeForAssociationPerSyncRequestBase = new ExPerformanceCounter("MSUnified Compliance Sync", "Ws Call Time For Association Per Sync Request Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000021 RID: 33
		public static readonly ExPerformanceCounter CrudMgrTimeForPolicyPerSyncRequest = new ExPerformanceCounter("MSUnified Compliance Sync", "CrudMgr Time For Policy Per Sync Request", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000022 RID: 34
		public static readonly ExPerformanceCounter CrudMgrTimeForPolicyPerSyncRequestBase = new ExPerformanceCounter("MSUnified Compliance Sync", "CrudMgr Time For Policy Per Sync Request Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000023 RID: 35
		public static readonly ExPerformanceCounter CrudMgrTimeForRulePerSyncRequest = new ExPerformanceCounter("MSUnified Compliance Sync", "CrudMgr Time For Rule Per Sync Request", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000024 RID: 36
		public static readonly ExPerformanceCounter CrudMgrTimeForRulePerSyncRequestBase = new ExPerformanceCounter("MSUnified Compliance Sync", "CrudMgr Time For Rule Per Sync Request Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000025 RID: 37
		public static readonly ExPerformanceCounter CrudMgrTimeForBindingPerSyncRequest = new ExPerformanceCounter("MSUnified Compliance Sync", "CrudMgr Time For Binding Per Sync Request", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000026 RID: 38
		public static readonly ExPerformanceCounter CrudMgrTimeForBindingPerSyncRequestBase = new ExPerformanceCounter("MSUnified Compliance Sync", "CrudMgr Time For Binding Per Sync Request Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000027 RID: 39
		public static readonly ExPerformanceCounter CrudMgrTimeForAssociationPerSyncRequest = new ExPerformanceCounter("MSUnified Compliance Sync", "CrudMgr Time For Association Per Sync Request", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000028 RID: 40
		public static readonly ExPerformanceCounter CrudMgrTimeForAssociationPerSyncRequestBase = new ExPerformanceCounter("MSUnified Compliance Sync", "CrudMgr Time For Association Per Sync Request Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000029 RID: 41
		public static readonly ExPerformanceCounter TenantInfoProcessingTimePerSyncRequest = new ExPerformanceCounter("MSUnified Compliance Sync", "TenantInfo Processing Time Per Sync Request", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400002A RID: 42
		public static readonly ExPerformanceCounter TenantInfoProcessingTimePerSyncRequestBase = new ExPerformanceCounter("MSUnified Compliance Sync", "TenantInfo Processing Time Per Sync Request Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400002B RID: 43
		public static readonly ExPerformanceCounter PersistentQueueProcessingTimePerSyncRequest = new ExPerformanceCounter("MSUnified Compliance Sync", "Persistent Queue Processing Time Per Sync Request", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400002C RID: 44
		public static readonly ExPerformanceCounter PersistentQueueProcessingTimePerSyncRequestBase = new ExPerformanceCounter("MSUnified Compliance Sync", "Persistent Queue Processing Time Per Sync Request Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400002D RID: 45
		public static readonly ExPerformanceCounter ProcessedSyncRequestNumberPerSecond = new ExPerformanceCounter("MSUnified Compliance Sync", "Processed Sync Request Number Per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400002E RID: 46
		public static readonly ExPerformanceCounter ProcessedSyncRequestNumber = new ExPerformanceCounter("MSUnified Compliance Sync", "Processed Sync Request Number", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400002F RID: 47
		public static readonly ExPerformanceCounter SuccessfulSyncRequestNumberPerSecond = new ExPerformanceCounter("MSUnified Compliance Sync", "Successful Sync Request Number Per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000030 RID: 48
		public static readonly ExPerformanceCounter SuccessfulSyncRequestNumber = new ExPerformanceCounter("MSUnified Compliance Sync", "Successful Sync Request Number", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000031 RID: 49
		public static readonly ExPerformanceCounter PolicySyncWsCallTransientErrorNumberPerSecond = new ExPerformanceCounter("MSUnified Compliance Sync", "Policy Sync Ws Call Transient Error Number Per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000032 RID: 50
		public static readonly ExPerformanceCounter PolicySyncWsCallTransientErrorNumber = new ExPerformanceCounter("MSUnified Compliance Sync", "Policy Sync Ws Call Transient Error Number", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000033 RID: 51
		public static readonly ExPerformanceCounter PolicySyncCrudMgrTransientErrorNumberPerSecond = new ExPerformanceCounter("MSUnified Compliance Sync", "Policy Sync CrudMgr Transient Error Number Per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000034 RID: 52
		public static readonly ExPerformanceCounter PolicySyncCrudMgrTransientErrorNumber = new ExPerformanceCounter("MSUnified Compliance Sync", "Policy Sync CrudMgr Transient Error Number", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000035 RID: 53
		public static readonly ExPerformanceCounter PolicySyncWsCallPermanentErrorNumberPerSecond = new ExPerformanceCounter("MSUnified Compliance Sync", "Policy Sync Ws Call Permanent Error Number Per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000036 RID: 54
		public static readonly ExPerformanceCounter PolicySyncWsCallPermanentErrorNumber = new ExPerformanceCounter("MSUnified Compliance Sync", "Policy Sync Ws Call Permanent Error Number", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000037 RID: 55
		public static readonly ExPerformanceCounter PolicySyncCrudMgrPermanentErrorNumberPerSecond = new ExPerformanceCounter("MSUnified Compliance Sync", "Policy Sync CrudMgr Permanent Error Number Per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000038 RID: 56
		public static readonly ExPerformanceCounter PolicySyncCrudMgrPermanentErrorNumber = new ExPerformanceCounter("MSUnified Compliance Sync", "Policy Sync CrudMgr Permanent Error Number", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000039 RID: 57
		public static readonly ExPerformanceCounter StatusUpdatePermanentErrorNumberPerSecond = new ExPerformanceCounter("MSUnified Compliance Sync", "Status Update Permanent Error Number Per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400003A RID: 58
		public static readonly ExPerformanceCounter StatusUpdatePermanentErrorNumber = new ExPerformanceCounter("MSUnified Compliance Sync", "Status Update Permanent Error Number", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400003B RID: 59
		public static readonly ExPerformanceCounter SyncRequestRetryNumberPerSecond = new ExPerformanceCounter("MSUnified Compliance Sync", "Sync Request Retry Number Per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400003C RID: 60
		public static readonly ExPerformanceCounter SyncRequestRetryNumber = new ExPerformanceCounter("MSUnified Compliance Sync", "Sync Request Retry Number", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400003D RID: 61
		public static readonly ExPerformanceCounter WsCallNumberPerSyncRequest = new ExPerformanceCounter("MSUnified Compliance Sync", "Ws Call Number Per Sync Request", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400003E RID: 62
		public static readonly ExPerformanceCounter WsCallNumberPerSyncRequestBase = new ExPerformanceCounter("MSUnified Compliance Sync", "Ws Call Number Per Sync Request Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400003F RID: 63
		public static readonly ExPerformanceCounter TenantNumberPerSyncRequest = new ExPerformanceCounter("MSUnified Compliance Sync", "Tenant Number Per Sync Request", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000040 RID: 64
		public static readonly ExPerformanceCounter TenantNumberPerSyncRequestBase = new ExPerformanceCounter("MSUnified Compliance Sync", "Tenant Number Per Sync Request Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000041 RID: 65
		public static readonly ExPerformanceCounter ObjectNumberPerSyncRequest = new ExPerformanceCounter("MSUnified Compliance Sync", "Object Number Per Sync Request", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000042 RID: 66
		public static readonly ExPerformanceCounter ObjectNumberPerSyncRequestBase = new ExPerformanceCounter("MSUnified Compliance Sync", "Object Number Per Sync Request Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000043 RID: 67
		public static readonly ExPerformanceCounter DarTasksTransientFailed = new ExPerformanceCounter("MSUnified Compliance Sync", "DarTasksTransientFailed", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000044 RID: 68
		public static readonly ExPerformanceCounter DarTasksInStateNone = new ExPerformanceCounter("MSUnified Compliance Sync", "DarTasksInStateNone", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000045 RID: 69
		public static readonly ExPerformanceCounter DarTasksInStateReady = new ExPerformanceCounter("MSUnified Compliance Sync", "DarTasksInStateReady", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000046 RID: 70
		public static readonly ExPerformanceCounter DarTasksInStateRunning = new ExPerformanceCounter("MSUnified Compliance Sync", "DarTasksInStateRunning", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000047 RID: 71
		public static readonly ExPerformanceCounter DarTasksInStateCompleted = new ExPerformanceCounter("MSUnified Compliance Sync", "DarTasksInStateCompleted", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000048 RID: 72
		public static readonly ExPerformanceCounter DarTasksInStateFailed = new ExPerformanceCounter("MSUnified Compliance Sync", "DarTasksInStateFailed", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000049 RID: 73
		public static readonly ExPerformanceCounter DarTasksInStateCancelled = new ExPerformanceCounter("MSUnified Compliance Sync", "DarTasksInStateCancelled", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400004A RID: 74
		public static readonly ExPerformanceCounter DarTaskAverageDuration = new ExPerformanceCounter("MSUnified Compliance Sync", "DarTaskAverageDuration", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400004B RID: 75
		public static readonly ExPerformanceCounter DarTaskAverageDurationBase = new ExPerformanceCounter("MSUnified Compliance Sync", "DarTaskAverageDurationBase", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400004C RID: 76
		private static readonly ExPerformanceCounter DarQueuedGrowthRate = new ExPerformanceCounter("MSUnified Compliance Sync", "DAR Queued Growth Rate", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400004D RID: 77
		public static readonly ExPerformanceCounter DarQueueLength = new ExPerformanceCounter("MSUnified Compliance Sync", "DAR Queue Length", string.Empty, null, new ExPerformanceCounter[]
		{
			UnifiedPolicySyncPerfCounters.DarQueuedGrowthRate
		});

		// Token: 0x0400004E RID: 78
		private static readonly ExPerformanceCounter DarQueueSchedulingRate = new ExPerformanceCounter("MSUnified Compliance Sync", "DAR Queue Scheduling Rate", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400004F RID: 79
		public static readonly ExPerformanceCounter DarScheduledTasks = new ExPerformanceCounter("MSUnified Compliance Sync", "DAR Scheduled Tasks", string.Empty, null, new ExPerformanceCounter[]
		{
			UnifiedPolicySyncPerfCounters.DarQueueSchedulingRate
		});

		// Token: 0x04000050 RID: 80
		private static readonly ExPerformanceCounter DarQueueProcessingRate = new ExPerformanceCounter("MSUnified Compliance Sync", "DAR Queue Processing Rate", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000051 RID: 81
		public static readonly ExPerformanceCounter DarProcessedTasks = new ExPerformanceCounter("MSUnified Compliance Sync", "DAR Processed Tasks", string.Empty, null, new ExPerformanceCounter[]
		{
			UnifiedPolicySyncPerfCounters.DarQueueProcessingRate
		});

		// Token: 0x04000052 RID: 82
		private static readonly ExPerformanceCounter DarTasksFailuresRate = new ExPerformanceCounter("MSUnified Compliance Sync", "DAR Tasks Failures Rate", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000053 RID: 83
		public static readonly ExPerformanceCounter DarFailedTasks = new ExPerformanceCounter("MSUnified Compliance Sync", "DAR Failed Tasks", string.Empty, null, new ExPerformanceCounter[]
		{
			UnifiedPolicySyncPerfCounters.DarTasksFailuresRate
		});

		// Token: 0x04000054 RID: 84
		private static readonly ExPerformanceCounter DarTasksTransientFailureRate = new ExPerformanceCounter("MSUnified Compliance Sync", "DAR Tasks Transient Failure Rate", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000055 RID: 85
		public static readonly ExPerformanceCounter DarTransientFailedTasks = new ExPerformanceCounter("MSUnified Compliance Sync", "DAR Transient Failed Tasks", string.Empty, null, new ExPerformanceCounter[]
		{
			UnifiedPolicySyncPerfCounters.DarTasksTransientFailureRate
		});

		// Token: 0x04000056 RID: 86
		public static readonly ExPerformanceCounter DarTaskAveDuration = new ExPerformanceCounter("MSUnified Compliance Sync", "DAR Task Average Duration", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000057 RID: 87
		public static readonly ExPerformanceCounter DarTaskAveDurationBase = new ExPerformanceCounter("MSUnified Compliance Sync", "DAR Task Average Duration Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000058 RID: 88
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

using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Diagnostics.WorkloadManagement
{
	// Token: 0x02000205 RID: 517
	internal class WorkloadManagementLogger
	{
		// Token: 0x06000F22 RID: 3874 RVA: 0x0003D846 File Offset: 0x0003BA46
		internal WorkloadManagementLogger(IWorkloadLogger log)
		{
			this.logger = log;
			ActivityContext.OnActivityEvent += this.OnActivityContextEvent;
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x0003D868 File Offset: 0x0003BA68
		internal static List<KeyValuePair<string, object>> FormatWlmActivity(IActivityScope activityScope, bool includeMetaData = true)
		{
			List<KeyValuePair<string, object>> list = null;
			if (activityScope != null)
			{
				list = activityScope.GetFormattableStatistics();
				if (includeMetaData)
				{
					List<KeyValuePair<string, object>> formattableMetadata = activityScope.GetFormattableMetadata();
					if (formattableMetadata != null)
					{
						foreach (KeyValuePair<string, object> item in formattableMetadata)
						{
							if (!item.Key.StartsWith("ActivityStandardMetadata"))
							{
								list.Add(item);
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x0003D94C File Offset: 0x0003BB4C
		internal static bool SetWorkloadMetadataValues(string workloadType, string workloadClassification, bool isServiceAccount, bool isInteractive, IActivityScope activityScope = null)
		{
			WorkloadManagementLogger.RegisterMetadataIfNecessary();
			return WorkloadManagementLogger.DoIfStarted(activityScope, delegate(IActivityScope scope)
			{
				scope.SetProperty(WlmMetaData.WorkloadType, workloadType);
				scope.SetProperty(WlmMetaData.WorkloadClassification, workloadClassification);
				scope.SetProperty(WlmMetaData.IsServiceAccount, isServiceAccount.ToString());
				scope.SetProperty(WlmMetaData.IsInteractive, isInteractive.ToString());
			});
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x0003D9B0 File Offset: 0x0003BBB0
		internal static bool SetBudgetType(string budgetType, IActivityScope activityScope = null)
		{
			WorkloadManagementLogger.RegisterMetadataIfNecessary();
			return WorkloadManagementLogger.DoIfStarted(activityScope, delegate(IActivityScope scope)
			{
				scope.SetProperty(WlmMetaData.BudgetType, budgetType);
			});
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x0003DA20 File Offset: 0x0003BC20
		internal static bool SetOverBudget(string policyPart, string policyValue, IActivityScope activityScope = null)
		{
			WorkloadManagementLogger.RegisterMetadataIfNecessary();
			return WorkloadManagementLogger.DoIfStarted(activityScope, delegate(IActivityScope scope)
			{
				ActivityContext.AddOperation(scope, ActivityOperationType.OverBudget, null, 0f, 1);
				scope.SetProperty(WlmMetaData.OverBudgetPolicyPart, policyPart);
				scope.SetProperty(WlmMetaData.OverBudgetPolicyValue, policyValue);
			});
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x0003DA78 File Offset: 0x0003BC78
		internal static bool SetQueueTime(TimeSpan queueTime, IActivityScope activityScope = null)
		{
			return WorkloadManagementLogger.DoIfStarted(activityScope, delegate(IActivityScope scope)
			{
				ActivityContext.AddOperation(scope, ActivityOperationType.QueueTime, null, (float)queueTime.TotalMilliseconds, 1);
			});
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x0003DAEC File Offset: 0x0003BCEC
		internal static bool SetThrottlingValues(TimeSpan delay, bool user, string instance, IActivityScope activityScope = null)
		{
			return WorkloadManagementLogger.DoIfStarted(activityScope, delegate(IActivityScope scope)
			{
				if (user)
				{
					ActivityContext.AddOperation(scope, ActivityOperationType.UserDelay, instance, (float)delay.TotalMilliseconds, 1);
					return;
				}
				ActivityContext.AddOperation(scope, ActivityOperationType.ResourceDelay, instance, (float)delay.TotalMilliseconds, 1);
			});
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x0003DB4C File Offset: 0x0003BD4C
		internal static bool SetBudgetUsage(TimeSpan usage, string instance, IActivityScope activityScope = null)
		{
			return WorkloadManagementLogger.DoIfStarted(activityScope, delegate(IActivityScope scope)
			{
				ActivityContext.AddOperation(scope, ActivityOperationType.BudgetUsed, instance, (float)usage.TotalMilliseconds, 1);
			});
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x0003DBA0 File Offset: 0x0003BDA0
		internal static bool SetResourceBlocked(string resourceInstance, IActivityScope activityScope = null)
		{
			return WorkloadManagementLogger.DoIfStarted(activityScope, delegate(IActivityScope scope)
			{
				ActivityContext.AddOperation(scope, ActivityOperationType.ResourceBlocked, resourceInstance, 0f, 1);
			});
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x0003DBE8 File Offset: 0x0003BDE8
		internal static bool SetBudgetBalance(string budgetBalance, IActivityScope activityScope = null)
		{
			WorkloadManagementLogger.RegisterMetadataIfNecessary();
			return WorkloadManagementLogger.DoIfStarted(activityScope, delegate(IActivityScope scope)
			{
				scope.SetProperty(WlmMetaData.BudgetBalance, budgetBalance);
			});
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x0003DC19 File Offset: 0x0003BE19
		internal void OnActivityContextEvent(object sender, ActivityEventArgs args)
		{
			this.logger.LogActivityEvent((IActivityScope)sender, args.ActivityEventType);
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x0003DC32 File Offset: 0x0003BE32
		private static void RegisterMetadataIfNecessary()
		{
			if (!WorkloadManagementLogger.wlmMetadataIsRegistered)
			{
				ActivityContext.RegisterMetadata(typeof(WlmMetaData));
				WorkloadManagementLogger.wlmMetadataIsRegistered = true;
			}
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x0003DC50 File Offset: 0x0003BE50
		private static bool DoIfStarted(IActivityScope activityScope, Action<IActivityScope> action)
		{
			if (activityScope == null)
			{
				activityScope = ActivityContext.GetCurrentActivityScope();
			}
			if (activityScope != null && activityScope.Status == ActivityContextStatus.ActivityStarted)
			{
				action(activityScope);
				return true;
			}
			return false;
		}

		// Token: 0x04000AC3 RID: 2755
		private static bool wlmMetadataIsRegistered;

		// Token: 0x04000AC4 RID: 2756
		private IWorkloadLogger logger;
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Storage.Compliance.DAR;
using Microsoft.Exchange.Management.CompliancePolicy.DarTasks.Protocol;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Office.CompliancePolicy.Dar;

namespace Microsoft.Office.CompliancePolicy.Exchange.Dar.Utility
{
	// Token: 0x0200001B RID: 27
	internal static class TaskHelper
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000CD RID: 205 RVA: 0x000050C7 File Offset: 0x000032C7
		public static HashSet<DarTaskState> CompletedTaskStates
		{
			get
			{
				return TaskHelper.completedTaskStates;
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000050CE File Offset: 0x000032CE
		public static bool IsActiveTask(DarTask task)
		{
			return task != null && !TaskHelper.CompletedTaskStates.Contains(task.TaskState);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000050FC File Offset: 0x000032FC
		public static SearchFilter GetCompletedTaskFilter(string taskType = null, DateTime? minCompletionDate = null, DateTime? maxCompletionDate = null, string taskId = null)
		{
			List<SearchFilter> list = new List<SearchFilter>();
			list.Add(new SearchFilter.SearchFilterCollection(1, (from t in TaskHelper.CompletedTaskStates
			select new SearchFilter.IsEqualTo(TaskStoreObjectExtendedStoreSchema.TaskState, (int)t)).ToArray<SearchFilter.IsEqualTo>()));
			if (maxCompletionDate != null)
			{
				list.Add(new SearchFilter.IsLessThanOrEqualTo(TaskStoreObjectExtendedStoreSchema.TaskCompletionTime, maxCompletionDate.Value));
			}
			if (minCompletionDate != null)
			{
				list.Add(new SearchFilter.IsGreaterThanOrEqualTo(TaskStoreObjectExtendedStoreSchema.TaskCompletionTime, minCompletionDate.Value));
			}
			if (!string.IsNullOrEmpty(taskType))
			{
				list.Add(new SearchFilter.IsEqualTo(TaskStoreObjectExtendedStoreSchema.TaskType, taskType));
			}
			if (!string.IsNullOrEmpty(taskId))
			{
				list.Add(new SearchFilter.IsEqualTo(TaskAggregateStoreObjectSchema.Id.StorePropertyDefinition, taskId));
			}
			return new SearchFilter.SearchFilterCollection(0, list.ToArray());
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000051D8 File Offset: 0x000033D8
		public static SearchFilter GetTaskFilter(string taskType = null, DarTaskState? taskState = null, DateTime? minScheduledTime = null, DateTime? maxScheduledTime = null, DateTime? minCompletedTime = null, DateTime? maxCompletedTime = null)
		{
			DarTaskParams darTaskParams = new DarTaskParams();
			if (taskType != null)
			{
				darTaskParams.TaskType = taskType;
			}
			if (taskState != null)
			{
				darTaskParams.TaskState = taskState.Value;
			}
			if (minScheduledTime != null)
			{
				darTaskParams.MinQueuedTime = minScheduledTime.Value;
			}
			if (maxScheduledTime != null)
			{
				darTaskParams.MaxQueuedTime = maxScheduledTime.Value;
			}
			if (minCompletedTime != null)
			{
				darTaskParams.MinCompletionTime = minCompletedTime.Value;
			}
			if (maxCompletedTime != null)
			{
				darTaskParams.MaxCompletionTime = maxCompletedTime.Value;
			}
			return TaskHelper.GetTaskFilter(darTaskParams);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000526C File Offset: 0x0000346C
		public static SearchFilter GetTaskFilter(DarTaskParams parameters)
		{
			List<SearchFilter> list = new List<SearchFilter>();
			if (parameters.TaskType != null)
			{
				list.Add(new SearchFilter.IsEqualTo(TaskAggregateStoreObjectSchema.Id.StorePropertyDefinition, parameters.TaskType));
			}
			if (parameters.TaskId != null)
			{
				list.Add(new SearchFilter.IsEqualTo(TaskAggregateStoreObjectSchema.Id.StorePropertyDefinition, parameters.TaskId));
			}
			if (parameters.MinCompletionTime != default(DateTime))
			{
				list.Add(new SearchFilter.IsGreaterThanOrEqualTo(TaskStoreObjectSchema.TaskCompletionTime.StorePropertyDefinition, parameters.MinCompletionTime));
			}
			if (parameters.MaxCompletionTime != default(DateTime))
			{
				list.Add(new SearchFilter.IsLessThanOrEqualTo(TaskStoreObjectSchema.TaskCompletionTime.StorePropertyDefinition, parameters.MaxCompletionTime));
			}
			if (parameters.MinQueuedTime != default(DateTime))
			{
				list.Add(new SearchFilter.IsGreaterThanOrEqualTo(TaskStoreObjectSchema.TaskQueuedTime.StorePropertyDefinition, parameters.MinQueuedTime));
			}
			if (parameters.MaxQueuedTime != default(DateTime))
			{
				list.Add(new SearchFilter.IsLessThanOrEqualTo(TaskStoreObjectSchema.TaskQueuedTime.StorePropertyDefinition, parameters.MaxQueuedTime));
			}
			if (parameters.TaskState != DarTaskState.None)
			{
				list.Add(new SearchFilter.IsEqualTo(TaskStoreObjectSchema.TaskState.StorePropertyDefinition, (int)parameters.TaskState));
			}
			if (!string.IsNullOrEmpty(parameters.TaskType))
			{
				list.Add(new SearchFilter.IsEqualTo(TaskStoreObjectSchema.TaskType.StorePropertyDefinition, parameters.TaskType));
			}
			return TaskHelper.GetSearchFilterFromCollection(list);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000053F4 File Offset: 0x000035F4
		public static SearchFilter GetTaskAggregateFilter(DarTaskAggregateParams parameters)
		{
			List<SearchFilter> list = new List<SearchFilter>();
			if (parameters.TaskId != null)
			{
				list.Add(new SearchFilter.IsEqualTo(TaskAggregateStoreObjectSchema.Id.StorePropertyDefinition, parameters.TaskId));
			}
			if (!string.IsNullOrEmpty(parameters.TaskType))
			{
				list.Add(new SearchFilter.IsEqualTo(TaskAggregateStoreObjectSchema.TaskType.StorePropertyDefinition, parameters.TaskType));
			}
			return TaskHelper.GetSearchFilterFromCollection(list);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000546C File Offset: 0x0000366C
		public static SearchFilter GetActiveTaskFilter()
		{
			return new SearchFilter.Not(new SearchFilter.SearchFilterCollection(1, (from t in TaskHelper.CompletedTaskStates
			select new SearchFilter.IsEqualTo(TaskStoreObjectExtendedStoreSchema.TaskState, (int)t)).ToArray<SearchFilter.IsEqualTo>()));
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000054D0 File Offset: 0x000036D0
		public static void Validate(DarTask task, DarServiceProvider serviceProvider)
		{
			if (string.IsNullOrEmpty(task.Id))
			{
				throw new ArgumentException("task.Id");
			}
			if (task.TenantId == null)
			{
				throw new ArgumentException("task.TenantId");
			}
			if (string.IsNullOrEmpty(task.TaskType))
			{
				throw new ArgumentException("task.TaskType");
			}
			if (!serviceProvider.DarTaskFactory.GetAllTaskTypes().Any((string t) => t.Equals(task.TaskType, StringComparison.InvariantCultureIgnoreCase)))
			{
				throw new ArgumentException("task.TaskType");
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00005565 File Offset: 0x00003765
		public static bool IsValid(DarTask task)
		{
			return !string.IsNullOrEmpty(task.Id) && !string.IsNullOrEmpty(task.TaskType) && task.TenantId != null;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00005590 File Offset: 0x00003790
		public static void Validate(DarTaskAggregate taskAggregate, DarServiceProvider serviceProvider)
		{
			if (string.IsNullOrEmpty(taskAggregate.Id))
			{
				throw new ArgumentException("taskAggregate.Id");
			}
			if (taskAggregate.ScopeId == null)
			{
				throw new ArgumentException("taskAggregate.ScopeId");
			}
			if (string.IsNullOrEmpty(taskAggregate.TaskType))
			{
				throw new ArgumentException("taskAggregate.TaskType");
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000055E0 File Offset: 0x000037E0
		private static SearchFilter GetSearchFilterFromCollection(List<SearchFilter> filters)
		{
			if (filters.Count == 0)
			{
				return null;
			}
			if (filters.Count == 1)
			{
				return filters[0];
			}
			return new SearchFilter.SearchFilterCollection(0, filters);
		}

		// Token: 0x04000053 RID: 83
		private static HashSet<DarTaskState> completedTaskStates = new HashSet<DarTaskState>
		{
			DarTaskState.Completed,
			DarTaskState.Failed,
			DarTaskState.Cancelled
		};
	}
}

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Storage.Compliance.DAR;
using Microsoft.Exchange.Management.CompliancePolicy.DarTasks.Protocol;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Office.CompliancePolicy.Dar;
using Microsoft.Office.CompliancePolicy.Exchange.Dar.LocStrings;
using Microsoft.Office.CompliancePolicy.Exchange.Dar.Utility;

namespace Microsoft.Office.CompliancePolicy.Exchange.Dar.Execution
{
	// Token: 0x0200000E RID: 14
	public class TaskAggregates
	{
		// Token: 0x0600007C RID: 124 RVA: 0x000038C4 File Offset: 0x00001AC4
		public DarTaskAggregate Get(string tenantId, string type, string correlationId)
		{
			return this.taskAggregateList.GetOrAdd(new TaskAggregates.TaskAggregateKey
			{
				TenantId = tenantId,
				TaskType = type
			}, delegate(TaskAggregates.TaskAggregateKey key)
			{
				SearchFilter taskAggregateFilter = TaskHelper.GetTaskAggregateFilter(new DarTaskAggregateParams
				{
					TaskType = type
				});
				DarTaskAggregate darTaskAggregate = (from ta in TenantStore.Find<TaskAggregateStoreObject>(key.TenantId, taskAggregateFilter, false, correlationId)
				select ta.ToDarTaskAggregate(InstanceManager.Current.Provider)).FirstOrDefault<DarTaskAggregate>();
				if (darTaskAggregate == null)
				{
					darTaskAggregate = TaskAggregates.CreateDefaultTaskAggregate(tenantId, type);
					TenantStore.SaveTaskAggregate(darTaskAggregate, correlationId);
				}
				return darTaskAggregate;
			});
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003950 File Offset: 0x00001B50
		public IEnumerable<DarTaskAggregate> GetAll(string tenantId, string correlationId)
		{
			return from kvp in this.taskAggregateList
			where kvp.Key.TenantId == tenantId
			select kvp.Value;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000039A4 File Offset: 0x00001BA4
		public void Set(DarTaskAggregate taskAggregate, string correlationId)
		{
			TenantStore.SaveTaskAggregate(taskAggregate, correlationId);
			TaskAggregates.TaskAggregateKey key = new TaskAggregates.TaskAggregateKey
			{
				TenantId = taskAggregate.ScopeId,
				TaskType = taskAggregate.TaskType
			};
			this.taskAggregateList[key] = taskAggregate;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000039EC File Offset: 0x00001BEC
		public bool Remove(string tenantId, string type, string correlationId)
		{
			TaskAggregates.TaskAggregateKey key = new TaskAggregates.TaskAggregateKey
			{
				TenantId = tenantId,
				TaskType = type
			};
			SearchFilter searchFilter = new SearchFilter.IsEqualTo(TaskAggregateStoreObjectSchema.TaskType.StorePropertyDefinition, type);
			TenantStore.DeleteTaskAggregates(tenantId, searchFilter, correlationId);
			DarTaskAggregate darTaskAggregate;
			return this.taskAggregateList.TryRemove(key, out darTaskAggregate);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003A3A File Offset: 0x00001C3A
		public void ValidateEnqueue(DarTask darTask, string correlationId)
		{
			if (!this.Get(darTask.TenantId, darTask.TaskType, correlationId).Enabled)
			{
				throw new ApplicationException(Strings.TaskIsDisabled);
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003A64 File Offset: 0x00001C64
		private static DarTaskAggregate CreateDefaultTaskAggregate(string tenantId, string type)
		{
			DarTaskAggregate darTaskAggregate = InstanceManager.Current.Provider.DarTaskFactory.CreateTaskAggregate(type);
			darTaskAggregate.ScopeId = tenantId;
			darTaskAggregate.Enabled = true;
			return darTaskAggregate;
		}

		// Token: 0x0400002C RID: 44
		private readonly ConcurrentDictionary<TaskAggregates.TaskAggregateKey, DarTaskAggregate> taskAggregateList = new ConcurrentDictionary<TaskAggregates.TaskAggregateKey, DarTaskAggregate>();

		// Token: 0x0200000F RID: 15
		private struct TaskAggregateKey
		{
			// Token: 0x0400002E RID: 46
			public string TenantId;

			// Token: 0x0400002F RID: 47
			public string TaskType;
		}
	}
}

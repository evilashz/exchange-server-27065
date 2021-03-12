using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Hygiene.Data.DataProvider;

namespace Microsoft.Exchange.Hygiene.Data.BackgroundJobBackend
{
	// Token: 0x02000034 RID: 52
	internal sealed class BackgroundJobBackendSession : HygieneSession
	{
		// Token: 0x06000192 RID: 402 RVA: 0x00005F22 File Offset: 0x00004122
		public BackgroundJobBackendSession()
		{
			this.dataProvider = ConfigDataProviderFactory.Default.Create(DatabaseType.BackgroundJobBackend);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00005F3C File Offset: 0x0000413C
		public RoleDefinition[] FindRoleByNameVersion(string roleName, string roleVersion)
		{
			QueryFilter filter = null;
			int num = 0;
			if (roleName != null)
			{
				num++;
			}
			if (roleVersion != null)
			{
				num++;
			}
			if (num > 0)
			{
				QueryFilter[] array = new QueryFilter[num];
				int num2 = 0;
				if (roleName != null)
				{
					array[num2++] = new ComparisonFilter(ComparisonOperator.Equal, BackgroundJobBackendSession.RoleNameQueryProperty, roleName);
				}
				if (roleVersion != null)
				{
					array[num2++] = new ComparisonFilter(ComparisonOperator.Equal, BackgroundJobBackendSession.RoleVersionQueryProperty, roleVersion);
				}
				if (num > 1)
				{
					filter = new OrFilter(array);
				}
				else
				{
					filter = array[0];
				}
			}
			IConfigurable[] array2 = this.dataProvider.Find<RoleDefinition>(filter, null, false, null);
			if (array2 == null)
			{
				return null;
			}
			return BackgroundJobBackendSession.IConfigurableArrayTo<RoleDefinition>(array2);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00005FC4 File Offset: 0x000041C4
		public RegionDefinition[] FindRegionIdByName(string regionName)
		{
			QueryFilter filter = null;
			if (regionName != null)
			{
				filter = new ComparisonFilter(ComparisonOperator.Equal, BackgroundJobBackendSession.NameQueryProperty, regionName);
			}
			IConfigurable[] array = this.dataProvider.Find<RegionDefinition>(filter, null, false, null);
			if (array == null)
			{
				return null;
			}
			return BackgroundJobBackendSession.IConfigurableArrayTo<RegionDefinition>(array);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00006000 File Offset: 0x00004200
		public DataCenterDefinition[] FindDataCenterIdByName(string dcName)
		{
			QueryFilter filter = null;
			if (dcName != null)
			{
				filter = new ComparisonFilter(ComparisonOperator.Equal, BackgroundJobBackendSession.NameQueryProperty, dcName);
			}
			IConfigurable[] array = this.dataProvider.Find<DataCenterDefinition>(filter, null, false, null);
			if (array == null)
			{
				return null;
			}
			return BackgroundJobBackendSession.IConfigurableArrayTo<DataCenterDefinition>(array);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000603C File Offset: 0x0000423C
		public BackgroundJobMgrInstance[] FindBackgroundJobMgrInstances(Guid roleId, string machineName)
		{
			QueryFilter[] array = new QueryFilter[2];
			array[0] = new ComparisonFilter(ComparisonOperator.Equal, BackgroundJobBackendSession.RoleIdQueryProperty, roleId);
			IConfigurable[] array2;
			if (machineName != null)
			{
				array[1] = new ComparisonFilter(ComparisonOperator.Equal, BackgroundJobBackendSession.MachineNameQueryProperty, machineName);
				OrFilter filter = new OrFilter(array);
				array2 = this.dataProvider.Find<BackgroundJobMgrInstance>(filter, null, false, null);
			}
			else
			{
				array2 = this.dataProvider.Find<BackgroundJobMgrInstance>(array[0], null, false, null);
			}
			if (array2 == null)
			{
				return null;
			}
			return BackgroundJobBackendSession.IConfigurableArrayTo<BackgroundJobMgrInstance>(array2);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x000060AC File Offset: 0x000042AC
		public BackgroundJobMgrInstance FindSchedulerInstance(Guid roleId, DateTime heartbeatThreshold, Regions region, long? datacenter = null)
		{
			QueryFilter filter = QueryFilter.AndTogether(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, BackgroundJobBackendSession.RoleIdQueryProperty, roleId),
				new ComparisonFilter(ComparisonOperator.Equal, BackgroundJobBackendSession.heartbeatDatetimeThresholdQueryProperty, heartbeatThreshold),
				new ComparisonFilter(ComparisonOperator.Equal, BackgroundJobBackendSession.RegionSelectionSetQueryProperty, region),
				new ComparisonFilter(ComparisonOperator.Equal, BackgroundJobBackendSession.DataCenterIdQueryProperty, datacenter)
			});
			IConfigurable[] array = this.dataProvider.Find<BackgroundJobMgrInstance>(filter, null, false, null);
			if (array == null || array.Length == 0)
			{
				return null;
			}
			return BackgroundJobBackendSession.IConfigurableToT<BackgroundJobMgrInstance>(array[0]);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000613C File Offset: 0x0000433C
		public BackgroundJobMgrInstance[] FindBackgroundJobMgrInstances(Guid roleId, DateTime lastCheckedDatetime, bool active)
		{
			OrFilter filter = new OrFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, BackgroundJobBackendSession.RoleIdQueryProperty, roleId),
				new ComparisonFilter(ComparisonOperator.Equal, BackgroundJobBackendSession.LastCheckedDatetimeQueryProperty, lastCheckedDatetime),
				new ComparisonFilter(ComparisonOperator.Equal, BackgroundJobBackendSession.ActiveQueryProperty, active)
			});
			IConfigurable[] array = this.dataProvider.Find<BackgroundJobMgrInstance>(filter, null, false, null);
			if (array == null)
			{
				return null;
			}
			return BackgroundJobBackendSession.IConfigurableArrayTo<BackgroundJobMgrInstance>(array);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x000061B0 File Offset: 0x000043B0
		public JobDefinition[] FindJobDefinitions(Guid? roleId = null, Guid? jobId = null, string jobName = null)
		{
			QueryFilter filter = null;
			if (roleId != null || jobId != null || jobName != null)
			{
				List<QueryFilter> list = new List<QueryFilter>(3);
				if (roleId != null)
				{
					list.Add(new ComparisonFilter(ComparisonOperator.Equal, BackgroundJobBackendSession.RoleIdQueryProperty, roleId));
				}
				if (jobId != null)
				{
					list.Add(new ComparisonFilter(ComparisonOperator.Equal, BackgroundJobBackendSession.JobIdQueryProperty, jobId));
				}
				if (jobName != null)
				{
					list.Add(new ComparisonFilter(ComparisonOperator.Equal, BackgroundJobBackendSession.JobNameQueryProperty, jobName));
				}
				if (list.Count == 1)
				{
					filter = list[0];
				}
				else
				{
					filter = new OrFilter(list.ToArray());
				}
			}
			IConfigurable[] array = this.dataProvider.Find<JobDefinition>(filter, null, false, null);
			if (array == null)
			{
				return null;
			}
			return BackgroundJobBackendSession.IConfigurableArrayTo<JobDefinition>(array);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000626C File Offset: 0x0000446C
		public ScheduleItem[] FindScheduleItems(Guid roleId, Guid? scheduleId = null, bool? active = null, int? regionSelectionSet = null, IEnumerable<long> dataCenterIdCollection = null, Guid? jobId = null)
		{
			int num = 2;
			if (scheduleId != null)
			{
				num++;
			}
			if (active != null)
			{
				num++;
			}
			if (regionSelectionSet != null)
			{
				num++;
			}
			if (jobId != null)
			{
				num++;
			}
			QueryFilter[] array = new QueryFilter[num];
			num = 0;
			array[num++] = new ComparisonFilter(ComparisonOperator.Equal, BackgroundJobBackendSession.RoleIdQueryProperty, roleId);
			if (scheduleId != null)
			{
				array[num++] = new ComparisonFilter(ComparisonOperator.Equal, BackgroundJobBackendSession.ScheduleIdQueryProperty, scheduleId);
			}
			if (active != null)
			{
				array[num++] = new ComparisonFilter(ComparisonOperator.Equal, BackgroundJobBackendSession.ActiveQueryProperty, active);
			}
			if (regionSelectionSet != null)
			{
				array[num++] = new ComparisonFilter(ComparisonOperator.Equal, BackgroundJobBackendSession.RegionSelectionSetQueryProperty, regionSelectionSet);
			}
			BatchPropertyTable batchPropertyTable = new BatchPropertyTable();
			if (dataCenterIdCollection != null)
			{
				foreach (long num2 in dataCenterIdCollection)
				{
					batchPropertyTable.AddPropertyValue(Guid.NewGuid(), BackgroundJobBackendSession.DCSelectionSetProperty2, num2);
				}
			}
			array[num++] = new ComparisonFilter(ComparisonOperator.Equal, BackgroundJobBackendSession.TvpDCIdList, batchPropertyTable);
			if (jobId != null)
			{
				array[num++] = new ComparisonFilter(ComparisonOperator.Equal, BackgroundJobBackendSession.JobIdQueryProperty, jobId);
			}
			QueryFilter filter;
			if (num > 1)
			{
				filter = new OrFilter(array);
			}
			else
			{
				filter = array[0];
			}
			IConfigurable[] array2 = this.dataProvider.Find<ScheduleItem>(filter, null, false, null);
			if (array2 == null)
			{
				return null;
			}
			return BackgroundJobBackendSession.IConfigurableArrayTo<ScheduleItem>(array2);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000063F8 File Offset: 0x000045F8
		public TaskItem[] FindTasks(Guid roleId, Guid? ownerId = null, TaskExecutionStateType? taskExecutionState = null, SchedulingType? schedulingType = null, int? regionSelectionSet = null, Guid? scheduleId = null, Guid? taskId = null, TaskCompletionStatusType? taskCompletionStatus = null, IEnumerable<long> dataCenterIdCollection = null, Guid? jobId = null, Guid? activeJobId = null)
		{
			if (taskExecutionState == null)
			{
				IPagedReader<TaskItem> pagedReader = this.FindTasks(roleId, ownerId, null, null, schedulingType, regionSelectionSet, scheduleId, taskId, taskCompletionStatus, dataCenterIdCollection, 1000, jobId, activeJobId);
				return pagedReader.ReadAllPages();
			}
			IPagedReader<TaskItem> pagedReader2 = this.FindTasks(roleId, ownerId, new TaskExecutionStateType[]
			{
				taskExecutionState.Value
			}, null, schedulingType, regionSelectionSet, scheduleId, taskId, taskCompletionStatus, dataCenterIdCollection, 1000, jobId, activeJobId);
			return pagedReader2.ReadAllPages();
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000646C File Offset: 0x0000466C
		public TaskItem[] FindTasks(Guid roleId, Guid? ownerId, IEnumerable<TaskExecutionStateType> taskExecutionStates, IEnumerable<TaskExecutionStateType> taskExecutionExclusionStates, SchedulingType? schedulingType, int? regionSelectionSet, Guid? scheduleId, Guid? taskId, TaskCompletionStatusType? taskCompletionStatus, IEnumerable<long> dataCenterIdCollection, Guid? jobId = null, Guid? activeJobId = null)
		{
			IPagedReader<TaskItem> pagedReader = this.FindTasks(roleId, ownerId, taskExecutionStates, taskExecutionExclusionStates, schedulingType, regionSelectionSet, scheduleId, taskId, taskCompletionStatus, dataCenterIdCollection, 1000, jobId, activeJobId);
			return pagedReader.ReadAllPages();
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000064A0 File Offset: 0x000046A0
		public IPagedReader<TaskItem> FindTasks(Guid roleId, Guid? ownerId, IEnumerable<TaskExecutionStateType> taskExecutionStates, IEnumerable<TaskExecutionStateType> taskExecutionExclusionStates, SchedulingType? schedulingType, int? regionSelectionSet, Guid? scheduleId, Guid? taskId, TaskCompletionStatusType? taskCompletionStatus, IEnumerable<long> dataCenterIdCollection, int pageSize, Guid? jobId = null, Guid? activeJobId = null)
		{
			int num = 5;
			if (jobId != null)
			{
				num++;
			}
			if (activeJobId != null)
			{
				num++;
			}
			if (ownerId != null)
			{
				num++;
			}
			if (schedulingType != null)
			{
				num++;
			}
			if (regionSelectionSet != null)
			{
				num++;
			}
			if (scheduleId != null)
			{
				num++;
			}
			if (taskId != null)
			{
				num++;
			}
			if (taskCompletionStatus != null)
			{
				num++;
			}
			QueryFilter[] array = new QueryFilter[num];
			num = 0;
			array[num++] = new ComparisonFilter(ComparisonOperator.Equal, BackgroundJobBackendSession.RoleIdQueryProperty, roleId);
			if (jobId != null)
			{
				array[num++] = new ComparisonFilter(ComparisonOperator.Equal, BackgroundJobBackendSession.JobIdQueryProperty, jobId);
			}
			if (activeJobId != null)
			{
				array[num++] = new ComparisonFilter(ComparisonOperator.Equal, BackgroundJobBackendSession.ActiveJobIdQueryProperty, activeJobId);
			}
			if (ownerId != null)
			{
				array[num++] = new ComparisonFilter(ComparisonOperator.Equal, BackgroundJobBackendSession.OwnerIdQueryProperty, ownerId);
			}
			if (schedulingType != null)
			{
				array[num++] = new ComparisonFilter(ComparisonOperator.Equal, BackgroundJobBackendSession.SchedulingTypeQueryProperty, (byte)schedulingType.Value);
			}
			if (regionSelectionSet != null)
			{
				array[num++] = new ComparisonFilter(ComparisonOperator.Equal, BackgroundJobBackendSession.RegionSelectionSetQueryProperty, regionSelectionSet);
			}
			if (scheduleId != null)
			{
				array[num++] = new ComparisonFilter(ComparisonOperator.Equal, BackgroundJobBackendSession.ScheduleIdQueryProperty, scheduleId);
			}
			if (taskId != null)
			{
				array[num++] = new ComparisonFilter(ComparisonOperator.Equal, BackgroundJobBackendSession.TaskIdQueryProperty, taskId);
			}
			if (taskCompletionStatus != null)
			{
				array[num++] = new ComparisonFilter(ComparisonOperator.Equal, BackgroundJobBackendSession.TaskCompletionStatusQueryProperty, taskCompletionStatus);
			}
			BatchPropertyTable batchPropertyTable = new BatchPropertyTable();
			if (dataCenterIdCollection != null)
			{
				foreach (long num2 in dataCenterIdCollection)
				{
					batchPropertyTable.AddPropertyValue(Guid.NewGuid(), BackgroundJobBackendSession.DCSelectionSetProperty2, num2);
				}
			}
			array[num++] = new ComparisonFilter(ComparisonOperator.Equal, BackgroundJobBackendSession.TvpDCIdList, batchPropertyTable);
			PropertyTable propertyTable = new PropertyTable();
			MultiValuedProperty<int> multiValuedProperty = new MultiValuedProperty<int>();
			if (taskExecutionStates != null)
			{
				foreach (TaskExecutionStateType item in taskExecutionStates)
				{
					multiValuedProperty.Add((int)item);
				}
				propertyTable.AddPropertyValue(BackgroundJobBackendSession.TaskExecutionStateQueryMvpProperty, multiValuedProperty);
			}
			array[num++] = new ComparisonFilter(ComparisonOperator.Equal, BackgroundJobBackendSession.TvpTaskExecutionStates, propertyTable);
			PropertyTable propertyTable2 = new PropertyTable();
			MultiValuedProperty<int> multiValuedProperty2 = new MultiValuedProperty<int>();
			if (taskExecutionExclusionStates != null)
			{
				foreach (TaskExecutionStateType item2 in taskExecutionExclusionStates)
				{
					multiValuedProperty2.Add((int)item2);
				}
				propertyTable2.AddPropertyValue(BackgroundJobBackendSession.TaskExecutionStateQueryMvpProperty, multiValuedProperty2);
			}
			array[num++] = new ComparisonFilter(ComparisonOperator.Equal, BackgroundJobBackendSession.TvpTaskExecutionExclusionStates, propertyTable2);
			array[num++] = new ComparisonFilter(ComparisonOperator.Equal, BackgroundJobBackendSession.PageSizeQueryProperty, pageSize);
			QueryFilter queryFilter = new OrFilter(array);
			return this.GetPagedReader<TaskItem>(queryFilter, pageSize, jobId == null);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000067D8 File Offset: 0x000049D8
		public void Save(DataCenterDefinition dcDef)
		{
			if (dcDef == null)
			{
				throw new ArgumentNullException("dcDef");
			}
			AuditHelper.ApplyAuditProperties(dcDef, default(Guid), null);
			this.dataProvider.Save(dcDef);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00006810 File Offset: 0x00004A10
		public void Save(RoleDefinition roleDef)
		{
			if (roleDef == null)
			{
				throw new ArgumentNullException("roleDef");
			}
			AuditHelper.ApplyAuditProperties(roleDef, default(Guid), null);
			this.dataProvider.Save(roleDef);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00006848 File Offset: 0x00004A48
		public void Save(RegionDefinition regionDef)
		{
			if (regionDef == null)
			{
				throw new ArgumentNullException("regionDef");
			}
			AuditHelper.ApplyAuditProperties(regionDef, default(Guid), null);
			this.dataProvider.Save(regionDef);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00006880 File Offset: 0x00004A80
		public void Save(BackgroundJobMgrInstance bjmInstance)
		{
			if (bjmInstance == null)
			{
				throw new ArgumentNullException("bjmInstance");
			}
			AuditHelper.ApplyAuditProperties(bjmInstance, default(Guid), null);
			this.dataProvider.Save(bjmInstance);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000068B8 File Offset: 0x00004AB8
		public void UpdateBackgroundJobMgrAsTimedOut(BackgroundJobMgrTimedOutInstance bjmTimedOutInstance)
		{
			if (bjmTimedOutInstance == null)
			{
				throw new ArgumentNullException("bjmTimedOutInstance");
			}
			AuditHelper.ApplyAuditProperties(bjmTimedOutInstance, default(Guid), null);
			this.dataProvider.Save(bjmTimedOutInstance);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x000068F0 File Offset: 0x00004AF0
		public void UpdateScheduleItemActive(ScheduleItemActiveUpdate scheduleItemActiveUpdate)
		{
			if (scheduleItemActiveUpdate == null)
			{
				throw new ArgumentNullException("scheduleItemActiveUpdate");
			}
			AuditHelper.ApplyAuditProperties(scheduleItemActiveUpdate, default(Guid), null);
			this.dataProvider.Save(scheduleItemActiveUpdate);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00006928 File Offset: 0x00004B28
		public void UpdateBackgroundJobMgrHeartBeat(BackgroundJobMgrHeartBeatUpdate bjmHeartBeatUpdate)
		{
			if (bjmHeartBeatUpdate == null)
			{
				throw new ArgumentNullException("bjmHeartBeatUpdate");
			}
			AuditHelper.ApplyAuditProperties(bjmHeartBeatUpdate, default(Guid), null);
			this.dataProvider.Save(bjmHeartBeatUpdate);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00006960 File Offset: 0x00004B60
		public void Save(ScheduleItem scheduleItem)
		{
			if (scheduleItem == null)
			{
				throw new ArgumentNullException("scheduleItem");
			}
			AuditHelper.ApplyAuditProperties(scheduleItem, default(Guid), null);
			this.dataProvider.Save(scheduleItem);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00006998 File Offset: 0x00004B98
		public void Save(JobDefinition jobDefinition)
		{
			if (jobDefinition == null)
			{
				throw new ArgumentNullException("jobDefinition");
			}
			AuditHelper.ApplyAuditProperties(jobDefinition, default(Guid), null);
			this.dataProvider.Save(jobDefinition);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x000069D0 File Offset: 0x00004BD0
		public void SaveTasksSynchronized(SaveTaskItemBatch saveTaskItemBatch)
		{
			if (saveTaskItemBatch == null)
			{
				throw new ArgumentNullException("saveTaskItemBatch");
			}
			AuditHelper.ApplyAuditProperties(saveTaskItemBatch, default(Guid), null);
			this.dataProvider.Save(saveTaskItemBatch);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00006A08 File Offset: 0x00004C08
		public void UpdateTaskStatus(TaskStatusUpdate taskStatusUpdate)
		{
			if (taskStatusUpdate == null)
			{
				throw new ArgumentNullException("taskStatusUpdate");
			}
			AuditHelper.ApplyAuditProperties(taskStatusUpdate, default(Guid), null);
			this.dataProvider.Save(taskStatusUpdate);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00006A40 File Offset: 0x00004C40
		public void SyncTaskStatusUpdate(SyncTaskStatusUpdate syncTaskStatusUpdate)
		{
			if (syncTaskStatusUpdate == null)
			{
				throw new ArgumentNullException("syncTaskStatusUpdate");
			}
			AuditHelper.ApplyAuditProperties(syncTaskStatusUpdate, default(Guid), null);
			this.dataProvider.Save(syncTaskStatusUpdate);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00006A78 File Offset: 0x00004C78
		public void TryTakeTaskOwnership(TakeTaskOwnership takeTaskOwnership)
		{
			if (takeTaskOwnership == null)
			{
				throw new ArgumentNullException("takeTaskOwnership");
			}
			AuditHelper.ApplyAuditProperties(takeTaskOwnership, default(Guid), null);
			this.dataProvider.Save(takeTaskOwnership);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00006AB0 File Offset: 0x00004CB0
		private IPagedReader<T> GetPagedReader<T>(QueryFilter queryFilter, int pageSize, bool queryAllPartition) where T : IConfigurable, new()
		{
			if (!queryAllPartition)
			{
				return new ConfigDataProviderPagedReader<T>(this.dataProvider, null, queryFilter, null, pageSize);
			}
			object[] allPhysicalPartitions = ((IPartitionedDataProvider)this.dataProvider).GetAllPhysicalPartitions();
			IPagedReader<T>[] array = new IPagedReader<T>[allPhysicalPartitions.Length];
			for (int i = 0; i < allPhysicalPartitions.Length; i++)
			{
				array[i] = new ConfigDataProviderPagedReader<T>(this.dataProvider, null, QueryFilter.AndTogether(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, DalHelper.PhysicalInstanceKeyProp, allPhysicalPartitions[i]),
					queryFilter
				}), null, pageSize);
			}
			return new CompositePagedReader<T>(array);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00006B31 File Offset: 0x00004D31
		private static T IConfigurableToT<T>(IConfigurable configurable) where T : IConfigurable
		{
			return (T)((object)configurable);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00006B3C File Offset: 0x00004D3C
		private static T[] IConfigurableArrayTo<T>(IConfigurable[] configurables) where T : IConfigurable
		{
			T[] array = new T[configurables.Length];
			for (int i = 0; i < configurables.Length; i++)
			{
				array[i] = BackgroundJobBackendSession.IConfigurableToT<T>(configurables[i]);
			}
			return array;
		}

		// Token: 0x04000110 RID: 272
		internal static readonly BackgroundJobBackendPropertyDefinition RoleNameQueryProperty = new BackgroundJobBackendPropertyDefinition("roleName", typeof(string), PropertyDefinitionFlags.Mandatory, null);

		// Token: 0x04000111 RID: 273
		internal static readonly BackgroundJobBackendPropertyDefinition RoleVersionQueryProperty = new BackgroundJobBackendPropertyDefinition("roleVersion", typeof(string), PropertyDefinitionFlags.Mandatory, null);

		// Token: 0x04000112 RID: 274
		internal static readonly BackgroundJobBackendPropertyDefinition RoleIdQueryProperty = new BackgroundJobBackendPropertyDefinition("roleId", typeof(Guid), PropertyDefinitionFlags.Mandatory | PropertyDefinitionFlags.PersistDefaultValue, Guid.Empty);

		// Token: 0x04000113 RID: 275
		internal static readonly BackgroundJobBackendPropertyDefinition JobIdQueryProperty = new BackgroundJobBackendPropertyDefinition("jobId", typeof(Guid), PropertyDefinitionFlags.Mandatory | PropertyDefinitionFlags.PersistDefaultValue, Guid.Empty);

		// Token: 0x04000114 RID: 276
		internal static readonly BackgroundJobBackendPropertyDefinition ActiveJobIdQueryProperty = new BackgroundJobBackendPropertyDefinition("activeJobId", typeof(Guid), PropertyDefinitionFlags.Mandatory | PropertyDefinitionFlags.PersistDefaultValue, Guid.Empty);

		// Token: 0x04000115 RID: 277
		internal static readonly BackgroundJobBackendPropertyDefinition NameQueryProperty = new BackgroundJobBackendPropertyDefinition("name", typeof(string), PropertyDefinitionFlags.Mandatory, null);

		// Token: 0x04000116 RID: 278
		internal static readonly BackgroundJobBackendPropertyDefinition JobNameQueryProperty = new BackgroundJobBackendPropertyDefinition("jobName", typeof(string), PropertyDefinitionFlags.Mandatory, null);

		// Token: 0x04000117 RID: 279
		internal static readonly BackgroundJobBackendPropertyDefinition MachineNameQueryProperty = new BackgroundJobBackendPropertyDefinition("machineName", typeof(string), PropertyDefinitionFlags.Mandatory, null);

		// Token: 0x04000118 RID: 280
		internal static readonly BackgroundJobBackendPropertyDefinition LastCheckedDatetimeQueryProperty = new BackgroundJobBackendPropertyDefinition("lastCheckedDatetime", typeof(DateTime), PropertyDefinitionFlags.Mandatory | PropertyDefinitionFlags.PersistDefaultValue, new DateTime(0L));

		// Token: 0x04000119 RID: 281
		internal static readonly BackgroundJobBackendPropertyDefinition ActiveQueryProperty = new BackgroundJobBackendPropertyDefinition("active", typeof(bool), PropertyDefinitionFlags.Mandatory | PropertyDefinitionFlags.PersistDefaultValue, false);

		// Token: 0x0400011A RID: 282
		internal static readonly BackgroundJobBackendPropertyDefinition RegionSelectionSetQueryProperty = new BackgroundJobBackendPropertyDefinition("regionSelectionSet", typeof(int), PropertyDefinitionFlags.Mandatory | PropertyDefinitionFlags.PersistDefaultValue, 0);

		// Token: 0x0400011B RID: 283
		internal static readonly BackgroundJobBackendPropertyDefinition OwnerIdQueryProperty = new BackgroundJobBackendPropertyDefinition("ownerId", typeof(Guid), PropertyDefinitionFlags.Mandatory | PropertyDefinitionFlags.PersistDefaultValue, Guid.Empty);

		// Token: 0x0400011C RID: 284
		internal static readonly BackgroundJobBackendPropertyDefinition ScheduleIdQueryProperty = new BackgroundJobBackendPropertyDefinition("scheduleId", typeof(Guid), PropertyDefinitionFlags.Mandatory | PropertyDefinitionFlags.PersistDefaultValue, Guid.Empty);

		// Token: 0x0400011D RID: 285
		internal static readonly BackgroundJobBackendPropertyDefinition TaskExecutionStateQueryProperty = new BackgroundJobBackendPropertyDefinition("taskExecutionState", typeof(byte), PropertyDefinitionFlags.Mandatory, 0);

		// Token: 0x0400011E RID: 286
		internal static readonly BackgroundJobBackendPropertyDefinition SchedulingTypeQueryProperty = new BackgroundJobBackendPropertyDefinition("schedulingType", typeof(byte), PropertyDefinitionFlags.Mandatory, 0);

		// Token: 0x0400011F RID: 287
		internal static readonly BackgroundJobBackendPropertyDefinition TaskIdQueryProperty = new BackgroundJobBackendPropertyDefinition("taskId", typeof(Guid), PropertyDefinitionFlags.Mandatory | PropertyDefinitionFlags.PersistDefaultValue, Guid.Empty);

		// Token: 0x04000120 RID: 288
		internal static readonly BackgroundJobBackendPropertyDefinition TaskCompletionStatusQueryProperty = new BackgroundJobBackendPropertyDefinition("taskCompletionStatus", typeof(byte), PropertyDefinitionFlags.Mandatory, 0);

		// Token: 0x04000121 RID: 289
		internal static readonly BackgroundJobBackendPropertyDefinition DataCenterIdQueryProperty = new BackgroundJobBackendPropertyDefinition("DCId", typeof(long?), PropertyDefinitionFlags.None, null);

		// Token: 0x04000122 RID: 290
		internal static readonly BackgroundJobBackendPropertyDefinition TvpDCIdList = new BackgroundJobBackendPropertyDefinition("tvp_DCIdList", typeof(DataTable), PropertyDefinitionFlags.Mandatory, null);

		// Token: 0x04000123 RID: 291
		internal static readonly BackgroundJobBackendPropertyDefinition DCSelectionSetProperty2 = new BackgroundJobBackendPropertyDefinition("DCSelectionSet2", typeof(long), PropertyDefinitionFlags.Mandatory | PropertyDefinitionFlags.PersistDefaultValue, 0L);

		// Token: 0x04000124 RID: 292
		internal static readonly BackgroundJobBackendPropertyDefinition TvpTaskExecutionStates = new BackgroundJobBackendPropertyDefinition("tvp_TaskExecutionStates", typeof(DataTable), PropertyDefinitionFlags.Mandatory, null);

		// Token: 0x04000125 RID: 293
		internal static readonly BackgroundJobBackendPropertyDefinition TvpTaskExecutionExclusionStates = new BackgroundJobBackendPropertyDefinition("tvp_TaskExecutionExclusionStates", typeof(DataTable), PropertyDefinitionFlags.Mandatory, null);

		// Token: 0x04000126 RID: 294
		internal static readonly HygienePropertyDefinition PageSizeQueryProperty = new HygienePropertyDefinition("PageSize", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000127 RID: 295
		internal static readonly BackgroundJobBackendPropertyDefinition heartbeatDatetimeThresholdQueryProperty = new BackgroundJobBackendPropertyDefinition("heartbeatDatetimeThreshold", typeof(DateTime), PropertyDefinitionFlags.Mandatory | PropertyDefinitionFlags.PersistDefaultValue, new DateTime(0L));

		// Token: 0x04000128 RID: 296
		internal static readonly BackgroundJobBackendPropertyDefinition TaskExecutionStateQueryMvpProperty = new BackgroundJobBackendPropertyDefinition("taskExecutionState", typeof(int), PropertyDefinitionFlags.MultiValued | PropertyDefinitionFlags.Mandatory, null);

		// Token: 0x04000129 RID: 297
		private readonly IConfigDataProvider dataProvider;
	}
}

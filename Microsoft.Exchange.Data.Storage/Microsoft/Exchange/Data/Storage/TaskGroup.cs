using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E73 RID: 3699
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TaskGroup : FolderTreeData
	{
		// Token: 0x06008042 RID: 32834 RVA: 0x00231433 File Offset: 0x0022F633
		internal TaskGroup(ICoreItem coreItem) : base(coreItem)
		{
			if (base.IsNew)
			{
				this.InitializeNewGroup();
				return;
			}
			this.groupClassId = FolderTreeData.GetSafeGuidFromByteArray(base.GetValueOrDefault<byte[]>(TaskGroupSchema.GroupClassId));
		}

		// Token: 0x1700222C RID: 8748
		public override object this[PropertyDefinition propertyDefinition]
		{
			get
			{
				this.CheckDisposed("this::get");
				return base[propertyDefinition];
			}
			set
			{
				this.CheckDisposed("this::set");
				base[propertyDefinition] = value;
				if (propertyDefinition == TaskGroupSchema.GroupClassId)
				{
					byte[] guid = value as byte[];
					this.groupClassId = FolderTreeData.GetSafeGuidFromByteArray(guid);
				}
			}
		}

		// Token: 0x1700222D RID: 8749
		// (get) Token: 0x06008045 RID: 32837 RVA: 0x002314BB File Offset: 0x0022F6BB
		public override Schema Schema
		{
			get
			{
				this.CheckDisposed("Schema::get");
				return TaskGroupSchema.Instance;
			}
		}

		// Token: 0x1700222E RID: 8750
		// (get) Token: 0x06008046 RID: 32838 RVA: 0x002314CD File Offset: 0x0022F6CD
		// (set) Token: 0x06008047 RID: 32839 RVA: 0x002314E0 File Offset: 0x0022F6E0
		public Guid GroupClassId
		{
			get
			{
				this.CheckDisposed("GroupClassId::get");
				return this.groupClassId;
			}
			private set
			{
				this.CheckDisposed("GroupClassId::set");
				this[TaskGroupSchema.GroupClassId] = value.ToByteArray();
			}
		}

		// Token: 0x1700222F RID: 8751
		// (get) Token: 0x06008048 RID: 32840 RVA: 0x002314FF File Offset: 0x0022F6FF
		// (set) Token: 0x06008049 RID: 32841 RVA: 0x00231517 File Offset: 0x0022F717
		public string GroupName
		{
			get
			{
				this.CheckDisposed("GroupName::get");
				return base.GetValueOrDefault<string>(ItemSchema.Subject);
			}
			set
			{
				this.CheckDisposed("GroupName::set");
				this[ItemSchema.Subject] = value;
			}
		}

		// Token: 0x17002230 RID: 8752
		// (get) Token: 0x0600804A RID: 32842 RVA: 0x00231530 File Offset: 0x0022F730
		public TaskGroupType GroupType
		{
			get
			{
				this.CheckDisposed("GroupType::get");
				if (this.GroupClassId.Equals(FolderTreeData.MyFoldersClassId))
				{
					return TaskGroupType.MyTasks;
				}
				return TaskGroupType.Normal;
			}
		}

		// Token: 0x0600804B RID: 32843 RVA: 0x00231560 File Offset: 0x0022F760
		public static TaskGroupInfoList GetTaskGroupsView(MailboxSession session)
		{
			bool flag = false;
			bool flag2 = true;
			Dictionary<Guid, TaskGroupInfo> guidToGroupMapping = new Dictionary<Guid, TaskGroupInfo>();
			Dictionary<StoreObjectId, TaskGroupEntryInfo> dictionary = new Dictionary<StoreObjectId, TaskGroupEntryInfo>();
			List<FolderTreeDataInfo> duplicateNodes = new List<FolderTreeDataInfo>();
			Dictionary<TaskGroupType, TaskGroupInfo> defaultGroups = new Dictionary<TaskGroupType, TaskGroupInfo>();
			TaskGroupInfoList taskGroupInfoList = new TaskGroupInfoList(duplicateNodes, defaultGroups, dictionary);
			using (Folder folder = Folder.Bind(session, DefaultFolderType.CommonViews))
			{
				using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.Associated, null, TaskGroup.TaskGroupViewSortOrder, TaskGroup.TaskInfoProperties))
				{
					queryResult.SeekToCondition(SeekReference.OriginBeginning, TaskGroup.TaskSectionFilter);
					while (flag2)
					{
						IStorePropertyBag[] propertyBags = queryResult.GetPropertyBags(10000);
						if (propertyBags.Length == 0)
						{
							break;
						}
						foreach (IStorePropertyBag storePropertyBag in propertyBags)
						{
							if (!TaskGroup.IsTaskSection(storePropertyBag))
							{
								flag2 = false;
								break;
							}
							if (TaskGroup.IsFolderTreeData(storePropertyBag))
							{
								if (TaskGroup.IsTaskGroup(storePropertyBag))
								{
									if (flag)
									{
										ExTraceGlobals.StorageTracer.TraceDebug<VersionedId, string>(0L, "Unexpected processing task group out of order. ItemId: {0}, Subject: {1}", (VersionedId)storePropertyBag.TryGetProperty(ItemSchema.Id), storePropertyBag.GetValueOrDefault<string>(ItemSchema.Subject, string.Empty));
									}
									else
									{
										TaskGroup.AddGroupToList(storePropertyBag, guidToGroupMapping, taskGroupInfoList);
									}
								}
								else if (TaskGroup.IsTaskGroupEntry(storePropertyBag))
								{
									flag = true;
									TaskGroup.AddTaskFolderToList(storePropertyBag, guidToGroupMapping, dictionary, taskGroupInfoList);
								}
							}
						}
					}
				}
			}
			return taskGroupInfoList;
		}

		// Token: 0x0600804C RID: 32844 RVA: 0x002316C0 File Offset: 0x0022F8C0
		public static TaskGroup BindDefaultGroup(MailboxSession session)
		{
			return TaskGroup.Bind(session, FolderTreeData.MyFoldersClassId);
		}

		// Token: 0x0600804D RID: 32845 RVA: 0x002316D0 File Offset: 0x0022F8D0
		public static TaskGroup Bind(MailboxSession session, Guid groupClassId)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(groupClassId, "groupClassId");
			if (groupClassId.Equals(Guid.Empty))
			{
				throw new ArgumentException("Invalid GroupClassId", "groupClassId");
			}
			TaskGroup taskGroup = null;
			bool flag = true;
			bool flag2 = false;
			TaskGroup result;
			using (Folder folder = Folder.Bind(session, DefaultFolderType.CommonViews))
			{
				using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.Associated, null, TaskGroup.TaskGroupViewSortOrder, TaskGroup.TaskInfoProperties))
				{
					queryResult.SeekToCondition(SeekReference.OriginBeginning, TaskGroup.TaskSectionFilter);
					while (flag)
					{
						IStorePropertyBag[] propertyBags = queryResult.GetPropertyBags(10000);
						if (propertyBags.Length == 0)
						{
							break;
						}
						for (int i = 0; i < propertyBags.Length; i++)
						{
							IStorePropertyBag storePropertyBag = propertyBags[i];
							if (!TaskGroup.IsTaskSection(storePropertyBag))
							{
								flag = false;
								break;
							}
							if (TaskGroup.IsFolderTreeData(storePropertyBag))
							{
								if (!flag2 && TaskGroup.IsTaskGroup(storePropertyBag, groupClassId))
								{
									flag2 = true;
									VersionedId storeId = (VersionedId)storePropertyBag.TryGetProperty(ItemSchema.Id);
									taskGroup = TaskGroup.Bind(session, storeId);
								}
								if (flag2)
								{
									taskGroup.LoadChildNodesCollection(propertyBags, i);
									break;
								}
							}
						}
					}
					if (flag2)
					{
						result = taskGroup;
					}
					else
					{
						result = TaskGroup.CreateMyTasksGroup(session);
					}
				}
			}
			return result;
		}

		// Token: 0x0600804E RID: 32846 RVA: 0x00231814 File Offset: 0x0022FA14
		public static TaskGroup Bind(MailboxSession session, StoreId storeId)
		{
			return TaskGroup.Bind(session, storeId, null);
		}

		// Token: 0x0600804F RID: 32847 RVA: 0x00231820 File Offset: 0x0022FA20
		public static TaskGroup Bind(MailboxSession session, StoreId storeId, ICollection<PropertyDefinition> propsToReturn)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(storeId, "storeId");
			TaskGroup taskGroup = ItemBuilder.ItemBind<TaskGroup>(session, storeId, TaskGroupSchema.Instance, propsToReturn);
			taskGroup.MailboxSession = session;
			return taskGroup;
		}

		// Token: 0x06008050 RID: 32848 RVA: 0x0023185C File Offset: 0x0022FA5C
		public static TaskGroup Create(MailboxSession session)
		{
			Util.ThrowOnNullArgument(session, "session");
			TaskGroup taskGroup = ItemBuilder.CreateNewItem<TaskGroup>(session, session.GetDefaultFolderId(DefaultFolderType.CommonViews), ItemCreateInfo.TaskGroupInfo, CreateMessageType.Associated);
			taskGroup.MailboxSession = session;
			return taskGroup;
		}

		// Token: 0x06008051 RID: 32849 RVA: 0x00231891 File Offset: 0x0022FA91
		public ReadOnlyCollection<TaskGroupEntryInfo> GetChildTaskFolders()
		{
			this.CheckDisposed("GetChildTaskFolders");
			this.LoadChildNodesCollection();
			return new ReadOnlyCollection<TaskGroupEntryInfo>(this.children);
		}

		// Token: 0x06008052 RID: 32850 RVA: 0x002318AF File Offset: 0x0022FAAF
		public TaskGroupInfo GetTaskGroupInfo()
		{
			return TaskGroup.GetTaskGroupInfoFromRow(this);
		}

		// Token: 0x06008053 RID: 32851 RVA: 0x002318B8 File Offset: 0x0022FAB8
		internal static bool IsFolderTreeData(IStorePropertyBag row)
		{
			string valueOrDefault = row.GetValueOrDefault<string>(StoreObjectSchema.ItemClass, string.Empty);
			return ObjectClass.IsFolderTreeData(valueOrDefault);
		}

		// Token: 0x06008054 RID: 32852 RVA: 0x002318DC File Offset: 0x0022FADC
		internal static bool IsTaskSection(IStorePropertyBag row)
		{
			FolderTreeDataSection valueOrDefault = row.GetValueOrDefault<FolderTreeDataSection>(FolderTreeDataSchema.GroupSection, FolderTreeDataSection.None);
			return valueOrDefault == FolderTreeDataSection.Tasks;
		}

		// Token: 0x06008055 RID: 32853 RVA: 0x002318FC File Offset: 0x0022FAFC
		internal static bool IsTaskGroupEntry(IStorePropertyBag row)
		{
			FolderTreeDataType valueOrDefault = row.GetValueOrDefault<FolderTreeDataType>(FolderTreeDataSchema.Type, FolderTreeDataType.Undefined);
			return valueOrDefault == FolderTreeDataType.NormalFolder;
		}

		// Token: 0x06008056 RID: 32854 RVA: 0x0023191C File Offset: 0x0022FB1C
		internal static bool IsTaskGroup(IStorePropertyBag row)
		{
			FolderTreeDataType valueOrDefault = row.GetValueOrDefault<FolderTreeDataType>(FolderTreeDataSchema.Type, FolderTreeDataType.Undefined);
			return valueOrDefault == FolderTreeDataType.Header;
		}

		// Token: 0x06008057 RID: 32855 RVA: 0x0023193C File Offset: 0x0022FB3C
		internal static bool IsTaskGroup(IStorePropertyBag row, Guid groupId)
		{
			byte[] valueOrDefault = row.GetValueOrDefault<byte[]>(TaskGroupSchema.GroupClassId, null);
			return TaskGroup.IsTaskGroup(row) && Util.CompareByteArray(groupId.ToByteArray(), valueOrDefault);
		}

		// Token: 0x06008058 RID: 32856 RVA: 0x00231970 File Offset: 0x0022FB70
		internal static bool IsTaskFolderInGroup(IStorePropertyBag row, Guid groupClassId)
		{
			byte[] valueOrDefault = row.GetValueOrDefault<byte[]>(FolderTreeDataSchema.ParentGroupClassId, null);
			return TaskGroup.IsTaskGroupEntry(row) && Util.CompareByteArray(groupClassId.ToByteArray(), valueOrDefault);
		}

		// Token: 0x06008059 RID: 32857 RVA: 0x002319A4 File Offset: 0x0022FBA4
		private static void AddGroupToList(IStorePropertyBag row, Dictionary<Guid, TaskGroupInfo> guidToGroupMapping, TaskGroupInfoList taskGroups)
		{
			TaskGroupInfo taskGroupInfoFromRow = TaskGroup.GetTaskGroupInfoFromRow(row);
			if (taskGroupInfoFromRow == null)
			{
				return;
			}
			TaskGroupInfo taskGroupInfo;
			if (guidToGroupMapping.TryGetValue(taskGroupInfoFromRow.GroupClassId, out taskGroupInfo))
			{
				if (taskGroupInfo.LastModifiedTime.CompareTo(taskGroupInfoFromRow.LastModifiedTime) > 0)
				{
					taskGroups.DuplicateNodes.Add(taskGroupInfoFromRow);
					return;
				}
				guidToGroupMapping[taskGroupInfoFromRow.GroupClassId] = taskGroupInfoFromRow;
				taskGroups.DuplicateNodes.Add(taskGroupInfo);
				taskGroups.Remove(taskGroupInfo);
				if (taskGroups.DefaultGroups.ContainsKey(taskGroupInfoFromRow.GroupType))
				{
					taskGroups.DefaultGroups[taskGroupInfoFromRow.GroupType] = taskGroupInfoFromRow;
				}
			}
			else
			{
				guidToGroupMapping.Add(taskGroupInfoFromRow.GroupClassId, taskGroupInfoFromRow);
				if (taskGroupInfoFromRow.GroupType != TaskGroupType.Normal)
				{
					taskGroups.DefaultGroups.Add(taskGroupInfoFromRow.GroupType, taskGroupInfoFromRow);
				}
			}
			taskGroups.Add(taskGroupInfoFromRow);
		}

		// Token: 0x0600805A RID: 32858 RVA: 0x00231A68 File Offset: 0x0022FC68
		private static void AddTaskFolderToList(IStorePropertyBag row, Dictionary<Guid, TaskGroupInfo> guidToGroupMapping, Dictionary<StoreObjectId, TaskGroupEntryInfo> taskFolderIdToGroupEntryMapping, TaskGroupInfoList taskGroups)
		{
			TaskGroupEntryInfo taskGroupEntryInfoFromRow = TaskGroupEntry.GetTaskGroupEntryInfoFromRow(row);
			if (taskGroupEntryInfoFromRow == null)
			{
				return;
			}
			TaskGroupInfo taskGroupInfo;
			if (!guidToGroupMapping.TryGetValue(taskGroupEntryInfoFromRow.ParentGroupClassId, out taskGroupInfo))
			{
				ExTraceGlobals.StorageTracer.TraceDebug<string, Guid, VersionedId>(0L, "Found a task group entry with an invalid parent id. TaskFolderName: {0}, ParentId: {1}, ItemId: {2}", taskGroupEntryInfoFromRow.FolderName, taskGroupEntryInfoFromRow.ParentGroupClassId, taskGroupEntryInfoFromRow.Id);
				return;
			}
			TaskGroupEntryInfo taskGroupEntryInfo;
			if (taskFolderIdToGroupEntryMapping.TryGetValue(taskGroupEntryInfoFromRow.TaskFolderId, out taskGroupEntryInfo))
			{
				if (taskGroupEntryInfo.LastModifiedTime.CompareTo(taskGroupEntryInfoFromRow.LastModifiedTime) > 0)
				{
					taskGroups.DuplicateNodes.Add(taskGroupEntryInfoFromRow);
					return;
				}
				taskGroups.DuplicateNodes.Add(taskGroupEntryInfo);
				guidToGroupMapping[taskGroupEntryInfo.ParentGroupClassId].TaskFolders.Remove(taskGroupEntryInfo);
				taskFolderIdToGroupEntryMapping[taskGroupEntryInfoFromRow.TaskFolderId] = taskGroupEntryInfoFromRow;
			}
			else
			{
				taskFolderIdToGroupEntryMapping.Add(taskGroupEntryInfoFromRow.TaskFolderId, taskGroupEntryInfoFromRow);
			}
			taskGroupInfo.TaskFolders.Add(taskGroupEntryInfoFromRow);
		}

		// Token: 0x0600805B RID: 32859 RVA: 0x00231B34 File Offset: 0x0022FD34
		private static TaskGroup CreateMyTasksGroup(MailboxSession session)
		{
			TaskGroup taskGroup = TaskGroup.InternalCreateDefaultGroup(session);
			bool flag = TaskGroup.AddDefaultTaskFolder(session, taskGroup, DefaultFolderType.ToDoSearch, ClientStrings.ToDoSearch.ToString(session.InternalCulture));
			string folderType = DefaultFolderType.ToDoSearch.ToString();
			if (flag)
			{
				flag = TaskGroup.AddDefaultTaskFolder(session, taskGroup, DefaultFolderType.Tasks, ClientStrings.Tasks.ToString(session.InternalCulture));
				folderType = DefaultFolderType.Tasks.ToString();
			}
			if (!flag)
			{
				AggregateOperationResult aggregateOperationResult = session.Delete(DeleteItemFlags.HardDelete, new StoreId[]
				{
					taskGroup.Id
				});
				if (aggregateOperationResult.OperationResult != OperationResult.Succeeded)
				{
					ExTraceGlobals.StorageTracer.TraceWarning<SmtpAddress>(0L, "Unable to delete default task group after failing to add the default task folder to it. User: {0}", session.MailboxOwner.MailboxInfo.PrimarySmtpAddress);
				}
				throw new DefaultTaskNodeCreationException(folderType);
			}
			return taskGroup;
		}

		// Token: 0x0600805C RID: 32860 RVA: 0x00231BF4 File Offset: 0x0022FDF4
		private static bool AddDefaultTaskFolder(MailboxSession session, TaskGroup taskGroup, DefaultFolderType folderType, string folderName)
		{
			bool result = true;
			using (TaskGroupEntry taskGroupEntry = TaskGroupEntry.Create(session, session.GetDefaultFolderId(folderType), FolderTreeData.MyFoldersClassId, taskGroup.GroupName))
			{
				taskGroupEntry.FolderName = folderName;
				ConflictResolutionResult conflictResolutionResult = taskGroupEntry.Save(SaveMode.NoConflictResolution);
				if (conflictResolutionResult.SaveStatus != SaveResult.Success)
				{
					ExTraceGlobals.StorageTracer.TraceWarning<DefaultFolderType, SmtpAddress>(0L, "Unable to associate default task folder type: {0} with the MyTasks group for user: {1}. Attempting to delete default tasks group.", folderType, session.MailboxOwner.MailboxInfo.PrimarySmtpAddress);
					result = false;
				}
			}
			return result;
		}

		// Token: 0x0600805D RID: 32861 RVA: 0x00231C74 File Offset: 0x0022FE74
		private static TaskGroup InternalCreateDefaultGroup(MailboxSession session)
		{
			TaskGroup taskGroup;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				taskGroup = TaskGroup.Create(session);
				taskGroup.GroupName = ClientStrings.MyTasks.ToString(session.InternalCulture);
				taskGroup.GroupClassId = FolderTreeData.MyFoldersClassId;
				disposeGuard.Add<TaskGroup>(taskGroup);
				ConflictResolutionResult conflictResolutionResult = taskGroup.Save(SaveMode.NoConflictResolution);
				if (conflictResolutionResult.SaveStatus != SaveResult.Success)
				{
					ExTraceGlobals.StorageTracer.TraceWarning<SmtpAddress>(0L, "Unable to create default group for user: {0}", session.MailboxOwner.MailboxInfo.PrimarySmtpAddress);
					throw new DefaultTaskGroupCreationException();
				}
				taskGroup.Load();
				disposeGuard.Success();
			}
			return taskGroup;
		}

		// Token: 0x0600805E RID: 32862 RVA: 0x00231D24 File Offset: 0x0022FF24
		private static TaskGroupInfo GetTaskGroupInfoFromRow(IStorePropertyBag row)
		{
			VersionedId id = (VersionedId)row.TryGetProperty(ItemSchema.Id);
			byte[] valueOrDefault = row.GetValueOrDefault<byte[]>(TaskGroupSchema.GroupClassId, null);
			string valueOrDefault2 = row.GetValueOrDefault<string>(ItemSchema.Subject, string.Empty);
			byte[] valueOrDefault3 = row.GetValueOrDefault<byte[]>(FolderTreeDataSchema.Ordinal, null);
			ExDateTime valueOrDefault4 = row.GetValueOrDefault<ExDateTime>(StoreObjectSchema.LastModifiedTime, ExDateTime.MinValue);
			Guid safeGuidFromByteArray = FolderTreeData.GetSafeGuidFromByteArray(valueOrDefault);
			if (safeGuidFromByteArray.Equals(Guid.Empty))
			{
				ExTraceGlobals.StorageTracer.TraceDebug<int>(0L, "Found task group with invalid group class id. ArrayLength: {0}", (valueOrDefault == null) ? -1 : valueOrDefault.Length);
				return null;
			}
			return new TaskGroupInfo(valueOrDefault2, id, safeGuidFromByteArray, TaskGroup.GetGroupTypeFromGuid(safeGuidFromByteArray), valueOrDefault3, valueOrDefault4);
		}

		// Token: 0x0600805F RID: 32863 RVA: 0x00231DC4 File Offset: 0x0022FFC4
		private static TaskGroupType GetGroupTypeFromGuid(Guid groupClassId)
		{
			if (groupClassId.Equals(FolderTreeData.MyFoldersClassId))
			{
				return TaskGroupType.MyTasks;
			}
			return TaskGroupType.Normal;
		}

		// Token: 0x06008060 RID: 32864 RVA: 0x00231DF4 File Offset: 0x0022FFF4
		protected override void OnBeforeSave()
		{
			base.OnBeforeSave();
			if (base.IsNew)
			{
				bool flag;
				byte[] nodeBefore = FolderTreeData.GetOrdinalValueOfFirstMatchingNode(base.MailboxSession, TaskGroup.FindLastGroupOrdinalSortOrder, (IStorePropertyBag row) => TaskGroup.IsFolderTreeData(row) && TaskGroup.IsTaskSection(row) && TaskGroup.IsTaskGroup(row), TaskGroup.TaskInfoProperties, out flag);
				if (flag && !FolderTreeData.MyFoldersClassId.Equals(this.GroupClassId))
				{
					using (TaskGroup taskGroup = TaskGroup.CreateMyTasksGroup(base.MailboxSession))
					{
						nodeBefore = taskGroup.NodeOrdinal;
					}
				}
				base.SetNodeOrdinalInternal(nodeBefore, null);
			}
		}

		// Token: 0x06008061 RID: 32865 RVA: 0x00231E94 File Offset: 0x00230094
		private void InitializeNewGroup()
		{
			this[StoreObjectSchema.ItemClass] = "IPM.Microsoft.WunderBar.Link";
			this[FolderTreeDataSchema.Type] = FolderTreeDataType.Header;
			this[FolderTreeDataSchema.FolderTreeDataFlags] = 0;
			this[FolderTreeDataSchema.GroupSection] = FolderTreeDataSection.Tasks;
			this[TaskGroupSchema.GroupClassId] = Guid.NewGuid().ToByteArray();
			this[FolderTreeDataSchema.ClassId] = TaskGroup.TaskSectionClassId.ToByteArray();
		}

		// Token: 0x06008062 RID: 32866 RVA: 0x00231F14 File Offset: 0x00230114
		private void LoadChildNodesCollection()
		{
			if (base.IsNew || this.hasLoadedTaskFoldersCollection)
			{
				return;
			}
			using (Folder folder = Folder.Bind(base.MailboxSession, DefaultFolderType.CommonViews))
			{
				using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.Associated, null, TaskGroup.TaskGroupViewSortOrder, TaskGroup.TaskInfoProperties))
				{
					for (;;)
					{
						IStorePropertyBag[] propertyBags = queryResult.GetPropertyBags(10000);
						if (propertyBags.Length == 0)
						{
							break;
						}
						this.LoadChildNodesCollection(propertyBags, 0);
					}
				}
			}
		}

		// Token: 0x06008063 RID: 32867 RVA: 0x00231FA4 File Offset: 0x002301A4
		private void LoadChildNodesCollection(IStorePropertyBag[] rows, int startIndex)
		{
			for (int i = startIndex; i < rows.Length; i++)
			{
				IStorePropertyBag storePropertyBag = rows[i];
				if (TaskGroup.IsTaskSection(storePropertyBag) && TaskGroup.IsFolderTreeData(storePropertyBag) && TaskGroup.IsTaskGroupEntry(storePropertyBag))
				{
					byte[] valueOrDefault = storePropertyBag.GetValueOrDefault<byte[]>(FolderTreeDataSchema.ParentGroupClassId, null);
					if (valueOrDefault == null || valueOrDefault.Length != 16)
					{
						ExTraceGlobals.StorageTracer.TraceDebug<int>(0L, "Found TaskGroupEntry with invalid parent group id. ArrayLength: {0}", (valueOrDefault == null) ? -1 : valueOrDefault.Length);
					}
					else
					{
						Guid g = new Guid(valueOrDefault);
						if (this.groupClassId.Equals(g))
						{
							TaskGroupEntryInfo taskGroupEntryInfoFromRow = TaskGroupEntry.GetTaskGroupEntryInfoFromRow(storePropertyBag);
							if (taskGroupEntryInfoFromRow != null)
							{
								this.children.Add(taskGroupEntryInfoFromRow);
							}
						}
					}
				}
			}
			this.hasLoadedTaskFoldersCollection = true;
		}

		// Token: 0x04005695 RID: 22165
		internal static readonly Guid TaskSectionClassId = new Guid("{00067803-0000-0000-C000-000000000046}");

		// Token: 0x04005696 RID: 22166
		private static readonly QueryFilter TaskSectionFilter = new ComparisonFilter(ComparisonOperator.Equal, FolderTreeDataSchema.GroupSection, FolderTreeDataSection.Tasks);

		// Token: 0x04005697 RID: 22167
		private static readonly SortBy[] TaskGroupViewSortOrder = new SortBy[]
		{
			new SortBy(FolderTreeDataSchema.GroupSection, SortOrder.Ascending),
			new SortBy(StoreObjectSchema.ItemClass, SortOrder.Ascending),
			new SortBy(FolderTreeDataSchema.ParentGroupClassId, SortOrder.Ascending),
			new SortBy(FolderTreeDataSchema.Ordinal, SortOrder.Ascending)
		};

		// Token: 0x04005698 RID: 22168
		internal static readonly PropertyDefinition[] TaskInfoProperties = new PropertyDefinition[]
		{
			ItemSchema.Id,
			StoreObjectSchema.LastModifiedTime,
			StoreObjectSchema.ItemClass,
			ItemSchema.Subject,
			FolderTreeDataSchema.GroupSection,
			TaskGroupSchema.GroupClassId,
			FolderTreeDataSchema.Type,
			FolderTreeDataSchema.Ordinal,
			FolderTreeDataSchema.ParentGroupClassId,
			TaskGroupEntrySchema.NodeEntryId,
			TaskGroupEntrySchema.StoreEntryId,
			FolderTreeDataSchema.FolderTreeDataFlags
		};

		// Token: 0x04005699 RID: 22169
		private static readonly SortBy[] FindLastGroupOrdinalSortOrder = new SortBy[]
		{
			new SortBy(StoreObjectSchema.ItemClass, SortOrder.Ascending),
			new SortBy(FolderTreeDataSchema.GroupSection, SortOrder.Ascending),
			new SortBy(FolderTreeDataSchema.Type, SortOrder.Descending),
			new SortBy(FolderTreeDataSchema.Ordinal, SortOrder.Descending)
		};

		// Token: 0x0400569A RID: 22170
		private readonly List<TaskGroupEntryInfo> children = new List<TaskGroupEntryInfo>();

		// Token: 0x0400569B RID: 22171
		private Guid groupClassId;

		// Token: 0x0400569C RID: 22172
		private bool hasLoadedTaskFoldersCollection;
	}
}

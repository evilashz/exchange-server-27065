using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E74 RID: 3700
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TaskGroupEntry : FolderTreeData
	{
		// Token: 0x06008066 RID: 32870 RVA: 0x00232175 File Offset: 0x00230375
		public TaskGroupEntry(ICoreItem coreItem) : base(coreItem)
		{
			if (base.IsNew)
			{
				this.InitializeNewTaskGroupEntry();
				return;
			}
			this.Initialize();
		}

		// Token: 0x06008067 RID: 32871 RVA: 0x00232193 File Offset: 0x00230393
		public static TaskGroupEntry Bind(MailboxSession session, StoreId storeId)
		{
			return TaskGroupEntry.Bind(session, storeId, null);
		}

		// Token: 0x06008068 RID: 32872 RVA: 0x002321A0 File Offset: 0x002303A0
		public static TaskGroupEntry Bind(MailboxSession session, StoreId storeId, ICollection<PropertyDefinition> propsToReturn)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(storeId, "storeId");
			TaskGroupEntry taskGroupEntry = ItemBuilder.ItemBind<TaskGroupEntry>(session, storeId, TaskGroupEntrySchema.Instance, propsToReturn);
			taskGroupEntry.MailboxSession = session;
			return taskGroupEntry;
		}

		// Token: 0x06008069 RID: 32873 RVA: 0x002321D9 File Offset: 0x002303D9
		public static TaskGroupEntry Create(MailboxSession session, Folder taskFolder, TaskGroup parentGroup)
		{
			Util.ThrowOnNullArgument(parentGroup, "parentGroup");
			Util.ThrowOnNullArgument(taskFolder, "TaskFolder");
			return TaskGroupEntry.Create(session, taskFolder.Id.ObjectId, parentGroup.GroupClassId, parentGroup.GroupName);
		}

		// Token: 0x0600806A RID: 32874 RVA: 0x00232210 File Offset: 0x00230410
		public static TaskGroupEntry Create(MailboxSession session, StoreObjectId taskFolderId, TaskGroup parentGroup)
		{
			Util.ThrowOnNullArgument(parentGroup, "parentGroup");
			TaskGroupEntry taskGroupEntry = TaskGroupEntry.Create(session, taskFolderId, parentGroup.GroupClassId, parentGroup.GroupName);
			taskGroupEntry.parentGroup = parentGroup;
			return taskGroupEntry;
		}

		// Token: 0x0600806B RID: 32875 RVA: 0x00232244 File Offset: 0x00230444
		internal static TaskGroupEntry Create(MailboxSession session, StoreObjectId taskFolderId, Guid parentGroupClassId, string parentGroupName)
		{
			Util.ThrowOnNullArgument(taskFolderId, "taskFolderId");
			if (taskFolderId.ObjectType != StoreObjectType.TasksFolder && taskFolderId.ObjectType != StoreObjectType.SearchFolder)
			{
				throw new NotSupportedException("A task group entry can only be associated with a storeobject of type task folder.");
			}
			TaskGroupEntry taskGroupEntry = TaskGroupEntry.Create(session, parentGroupClassId, parentGroupName);
			taskGroupEntry.TaskFolderId = taskFolderId;
			taskGroupEntry.StoreEntryId = Microsoft.Exchange.Data.Storage.StoreEntryId.ToProviderStoreEntryId(session.MailboxOwner);
			return taskGroupEntry;
		}

		// Token: 0x0600806C RID: 32876 RVA: 0x0023229C File Offset: 0x0023049C
		internal static TaskGroupEntry Create(MailboxSession session, Guid parentGroupClassId, string parentGroupName)
		{
			Util.ThrowOnNullArgument(session, "session");
			TaskGroupEntry taskGroupEntry = ItemBuilder.CreateNewItem<TaskGroupEntry>(session, session.GetDefaultFolderId(DefaultFolderType.CommonViews), ItemCreateInfo.TaskGroupEntryInfo, CreateMessageType.Associated);
			taskGroupEntry.MailboxSession = session;
			taskGroupEntry.ParentGroupClassId = parentGroupClassId;
			taskGroupEntry.ParentGroupName = parentGroupName;
			return taskGroupEntry;
		}

		// Token: 0x0600806D RID: 32877 RVA: 0x002322E0 File Offset: 0x002304E0
		internal static TaskGroupEntryInfo GetTaskGroupEntryInfoFromRow(IStorePropertyBag row)
		{
			VersionedId versionedId = (VersionedId)row.TryGetProperty(ItemSchema.Id);
			byte[] valueOrDefault = row.GetValueOrDefault<byte[]>(TaskGroupEntrySchema.NodeEntryId, null);
			byte[] valueOrDefault2 = row.GetValueOrDefault<byte[]>(FolderTreeDataSchema.ParentGroupClassId, null);
			string valueOrDefault3 = row.GetValueOrDefault<string>(ItemSchema.Subject, string.Empty);
			byte[] valueOrDefault4 = row.GetValueOrDefault<byte[]>(FolderTreeDataSchema.Ordinal, null);
			row.GetValueOrDefault<byte[]>(TaskGroupEntrySchema.StoreEntryId, null);
			ExDateTime valueOrDefault5 = row.GetValueOrDefault<ExDateTime>(StoreObjectSchema.LastModifiedTime, ExDateTime.MinValue);
			row.GetValueOrDefault<FolderTreeDataType>(FolderTreeDataSchema.Type, FolderTreeDataType.NormalFolder);
			FolderTreeDataFlags valueOrDefault6 = row.GetValueOrDefault<FolderTreeDataFlags>(FolderTreeDataSchema.FolderTreeDataFlags, FolderTreeDataFlags.None);
			Guid safeGuidFromByteArray = FolderTreeData.GetSafeGuidFromByteArray(valueOrDefault2);
			if (safeGuidFromByteArray.Equals(Guid.Empty))
			{
				ExTraceGlobals.StorageTracer.TraceDebug<int>(0L, "Found TaskGroupEntry with invalid parent group class id. ArrayLength: {0}", (valueOrDefault2 == null) ? -1 : valueOrDefault2.Length);
				return null;
			}
			if (IdConverter.IsFolderId(valueOrDefault))
			{
				StoreObjectId storeObjectId = StoreObjectId.FromProviderSpecificId(valueOrDefault);
				if ((valueOrDefault6 & FolderTreeDataFlags.IsDefaultStore) == FolderTreeDataFlags.IsDefaultStore)
				{
					return new TaskGroupEntryInfo(valueOrDefault3, versionedId, storeObjectId, safeGuidFromByteArray, valueOrDefault4, valueOrDefault5);
				}
				ExTraceGlobals.StorageTracer.TraceDebug<StoreObjectType, string, VersionedId>(0L, "Found TaskGroupEntry of type {0} referencing a non-task folder. ObjectType: {0}. TaskFfolderName: {1}. Id: {2}.", storeObjectId.ObjectType, valueOrDefault3, versionedId);
			}
			return null;
		}

		// Token: 0x17002231 RID: 8753
		// (get) Token: 0x0600806E RID: 32878 RVA: 0x002323ED File Offset: 0x002305ED
		public VersionedId TaskGroupEntryId
		{
			get
			{
				this.CheckDisposed("TaskGroupEntryId::get");
				return base.GetValueOrDefault<VersionedId>(ItemSchema.Id, null);
			}
		}

		// Token: 0x17002232 RID: 8754
		// (get) Token: 0x0600806F RID: 32879 RVA: 0x00232406 File Offset: 0x00230606
		// (set) Token: 0x06008070 RID: 32880 RVA: 0x0023241E File Offset: 0x0023061E
		public byte[] TaskFolderRecordKey
		{
			get
			{
				this.CheckDisposed("TaskRecordKey::get");
				return base.GetValueOrDefault<byte[]>(TaskGroupEntrySchema.NodeRecordKey);
			}
			set
			{
				this.CheckDisposed("TaskRecordKey::set");
				this[TaskGroupEntrySchema.NodeRecordKey] = value;
			}
		}

		// Token: 0x17002233 RID: 8755
		// (get) Token: 0x06008071 RID: 32881 RVA: 0x00232437 File Offset: 0x00230637
		// (set) Token: 0x06008072 RID: 32882 RVA: 0x0023244A File Offset: 0x0023064A
		public StoreObjectId TaskFolderId
		{
			get
			{
				this.CheckDisposed("TaskFolderId::get");
				return this.taskFolderObjectId;
			}
			set
			{
				this.CheckDisposed("TaskFolderId::set");
				this[TaskGroupEntrySchema.NodeEntryId] = value.ProviderLevelItemId;
			}
		}

		// Token: 0x17002234 RID: 8756
		// (get) Token: 0x06008073 RID: 32883 RVA: 0x00232468 File Offset: 0x00230668
		// (set) Token: 0x06008074 RID: 32884 RVA: 0x00232480 File Offset: 0x00230680
		public byte[] StoreEntryId
		{
			get
			{
				this.CheckDisposed("StoreEntryId::get");
				return base.GetValueOrDefault<byte[]>(TaskGroupEntrySchema.StoreEntryId);
			}
			set
			{
				this.CheckDisposed("StoreEntryId::set");
				this[TaskGroupEntrySchema.StoreEntryId] = value;
			}
		}

		// Token: 0x17002235 RID: 8757
		// (get) Token: 0x06008075 RID: 32885 RVA: 0x00232499 File Offset: 0x00230699
		// (set) Token: 0x06008076 RID: 32886 RVA: 0x002324AC File Offset: 0x002306AC
		public Guid ParentGroupClassId
		{
			get
			{
				this.CheckDisposed("ParentGroupClassId::get");
				return this.parentGroupClassId;
			}
			set
			{
				this.CheckDisposed("ParentGroupClassId::set");
				this[FolderTreeDataSchema.ParentGroupClassId] = value.ToByteArray();
			}
		}

		// Token: 0x17002236 RID: 8758
		// (get) Token: 0x06008077 RID: 32887 RVA: 0x002324CB File Offset: 0x002306CB
		// (set) Token: 0x06008078 RID: 32888 RVA: 0x002324E3 File Offset: 0x002306E3
		public string ParentGroupName
		{
			get
			{
				this.CheckDisposed("ParentGroupName::get");
				return base.GetValueOrDefault<string>(TaskGroupEntrySchema.ParentGroupName);
			}
			private set
			{
				this.CheckDisposed("ParentGroupName::set");
				this[TaskGroupEntrySchema.ParentGroupName] = value;
			}
		}

		// Token: 0x17002237 RID: 8759
		// (get) Token: 0x06008079 RID: 32889 RVA: 0x002324FC File Offset: 0x002306FC
		// (set) Token: 0x0600807A RID: 32890 RVA: 0x00232514 File Offset: 0x00230714
		public string FolderName
		{
			get
			{
				this.CheckDisposed("FolderName::get");
				return base.GetValueOrDefault<string>(ItemSchema.Subject);
			}
			set
			{
				this.CheckDisposed("FolderName::set");
				this[ItemSchema.Subject] = value;
			}
		}

		// Token: 0x17002238 RID: 8760
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
				if (propertyDefinition == TaskGroupEntrySchema.NodeEntryId)
				{
					this.SetTaskFolderId(value as byte[]);
					return;
				}
				if (propertyDefinition == FolderTreeDataSchema.ParentGroupClassId)
				{
					this.parentGroupClassId = FolderTreeData.GetSafeGuidFromByteArray(value as byte[]);
				}
			}
		}

		// Token: 0x17002239 RID: 8761
		// (get) Token: 0x0600807D RID: 32893 RVA: 0x00232592 File Offset: 0x00230792
		public override Schema Schema
		{
			get
			{
				this.CheckDisposed("Schema::get");
				return TaskGroupEntrySchema.Instance;
			}
		}

		// Token: 0x0600807E RID: 32894 RVA: 0x002325A4 File Offset: 0x002307A4
		public TaskGroupEntryInfo GetTaskGroupEntryInfo()
		{
			return TaskGroupEntry.GetTaskGroupEntryInfoFromRow(this);
		}

		// Token: 0x0600807F RID: 32895 RVA: 0x002325CC File Offset: 0x002307CC
		protected override void OnBeforeSave()
		{
			base.OnBeforeSave();
			if (base.IsNew)
			{
				if (Guid.Empty.Equals(this.ParentGroupClassId))
				{
					throw new NotSupportedException("A new Task group entry needs to have its ParentGroupClassId set.");
				}
				byte[] nodeBefore = null;
				if (this.parentGroup != null)
				{
					ReadOnlyCollection<TaskGroupEntryInfo> childTaskFolders = this.parentGroup.GetChildTaskFolders();
					if (childTaskFolders.Count > 0)
					{
						nodeBefore = childTaskFolders[childTaskFolders.Count - 1].Ordinal;
					}
				}
				else
				{
					bool flag;
					nodeBefore = FolderTreeData.GetOrdinalValueOfFirstMatchingNode(base.MailboxSession, TaskGroupEntry.FindLastTaskFolderOrdinalSortOrder, (IStorePropertyBag row) => TaskGroup.IsFolderTreeData(row) && TaskGroup.IsTaskSection(row) && TaskGroup.IsTaskFolderInGroup(row, this.ParentGroupClassId), TaskGroup.TaskInfoProperties, out flag);
				}
				base.SetNodeOrdinalInternal(nodeBefore, null);
			}
		}

		// Token: 0x06008080 RID: 32896 RVA: 0x00232673 File Offset: 0x00230873
		private void Initialize()
		{
			this.SetTaskFolderId(base.GetValueOrDefault<byte[]>(TaskGroupEntrySchema.NodeEntryId));
			this.parentGroupClassId = FolderTreeData.GetSafeGuidFromByteArray(base.GetValueOrDefault<byte[]>(FolderTreeDataSchema.ParentGroupClassId));
		}

		// Token: 0x06008081 RID: 32897 RVA: 0x0023269C File Offset: 0x0023089C
		private void InitializeNewTaskGroupEntry()
		{
			this[StoreObjectSchema.ItemClass] = "IPM.Microsoft.WunderBar.Link";
			this[FolderTreeDataSchema.GroupSection] = FolderTreeDataSection.Tasks;
			this[FolderTreeDataSchema.ClassId] = TaskGroup.TaskSectionClassId.ToByteArray();
			this[FolderTreeDataSchema.Type] = FolderTreeDataType.NormalFolder;
			this[FolderTreeDataSchema.FolderTreeDataFlags] = FolderTreeDataFlags.IsDefaultStore;
		}

		// Token: 0x06008082 RID: 32898 RVA: 0x00232708 File Offset: 0x00230908
		private void SetTaskFolderId(byte[] entryId)
		{
			if (IdConverter.IsFolderId(entryId))
			{
				this.taskFolderObjectId = StoreObjectId.FromProviderSpecificIdOrNull(entryId);
			}
		}

		// Token: 0x0400569E RID: 22174
		private static readonly SortBy[] FindLastTaskFolderOrdinalSortOrder = new SortBy[]
		{
			new SortBy(StoreObjectSchema.ItemClass, SortOrder.Ascending),
			new SortBy(FolderTreeDataSchema.GroupSection, SortOrder.Ascending),
			new SortBy(FolderTreeDataSchema.ParentGroupClassId, SortOrder.Descending),
			new SortBy(FolderTreeDataSchema.Ordinal, SortOrder.Descending)
		};

		// Token: 0x0400569F RID: 22175
		private StoreObjectId taskFolderObjectId;

		// Token: 0x040056A0 RID: 22176
		private Guid parentGroupClassId;

		// Token: 0x040056A1 RID: 22177
		private TaskGroup parentGroup;
	}
}

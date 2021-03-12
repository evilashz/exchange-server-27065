using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x02000016 RID: 22
	internal class FolderList : IComparer<object[]>
	{
		// Token: 0x06000095 RID: 149 RVA: 0x00005A2C File Offset: 0x00003C2C
		public FolderList(UserContext userContext, MailboxSession mailboxSession, NavigationModule navigationModule, int maxFolders, bool isListDeletedFolders, PropertyDefinition sortProperty, PropertyDefinition[] extendedProperties)
		{
			string text;
			switch (navigationModule)
			{
			case NavigationModule.Mail:
				text = "IPF.Note";
				break;
			case NavigationModule.Calendar:
				text = "IPF.Appointment";
				break;
			case NavigationModule.Contacts:
				text = "IPF.Contact";
				break;
			case NavigationModule.Tasks:
				text = "IPF.Task";
				break;
			default:
				throw new ArgumentOutOfRangeException("navigationModule", "Module " + navigationModule);
			}
			this.Initialize(userContext, mailboxSession, userContext.GetRootFolderId(mailboxSession), new string[]
			{
				text
			}, null, maxFolders, isListDeletedFolders, true, false, null, sortProperty, extendedProperties);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00005AD4 File Offset: 0x00003CD4
		public FolderList(UserContext userContext, MailboxSession mailboxSession, string[] folderTypes, int maxFolders, bool isListDeletedFolders, PropertyDefinition sortProperty, PropertyDefinition[] extendedProperties)
		{
			this.Initialize(userContext, mailboxSession, userContext.GetRootFolderId(mailboxSession), folderTypes, null, maxFolders, isListDeletedFolders, true, false, null, sortProperty, extendedProperties);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00005B1C File Offset: 0x00003D1C
		public FolderList(UserContext userContext, MailboxSession mailboxSession, StoreObjectId storeObjectId, QueryFilter queryFilter, int maxFolders, bool isListDeletedFolders, bool isDeepTraversal, bool isListRootFolder, PropertyDefinition sortProperty, PropertyDefinition[] extendedProperties)
		{
			this.Initialize(userContext, mailboxSession, storeObjectId, null, queryFilter, maxFolders, isListDeletedFolders, isDeepTraversal, isListRootFolder, null, sortProperty, extendedProperties);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00005B60 File Offset: 0x00003D60
		public FolderList(UserContext userContext, MailboxSession mailboxSession, StoreObjectId rootFolderId, int maxFolders, bool isListDeletedFolders, bool isDeepTraversal, bool isListRootFolder, FolderList parentFolderList)
		{
			this.Initialize(userContext, mailboxSession, rootFolderId, null, null, maxFolders, isListDeletedFolders, isDeepTraversal, isListRootFolder, parentFolderList, null, null);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00005BA0 File Offset: 0x00003DA0
		private static void CombinePropertyList(PropertyDefinition[] extendedProperties, out PropertyDefinition[] allProperties, out Dictionary<PropertyDefinition, int> allPropertyIndexes)
		{
			List<PropertyDefinition> list = new List<PropertyDefinition>(FolderList.defaultQueryProperties.Length + extendedProperties.Length);
			for (int i = 0; i < FolderList.defaultQueryProperties.Length; i++)
			{
				list.Add(FolderList.defaultQueryProperties[i]);
			}
			foreach (PropertyDefinition item in extendedProperties)
			{
				if (!list.Contains(item))
				{
					list.Add(item);
				}
			}
			allProperties = list.ToArray();
			allPropertyIndexes = Utilities.GetPropertyToIndexMap(allProperties);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00005C10 File Offset: 0x00003E10
		private void Initialize(UserContext userContext, MailboxSession session, StoreObjectId rootFolderId, string[] folderTypes, QueryFilter queryFilter, int maxFolders, bool isListDeletedFolders, bool isDeepTraversal, bool isListRootFolder, FolderList parentFolderList, PropertyDefinition sortProperty, PropertyDefinition[] extendedProperties)
		{
			this.userContext = userContext;
			this.maxFolders = maxFolders;
			this.isListDeletedFolders = isListDeletedFolders;
			this.sortProperty = sortProperty;
			this.mailboxSession = session;
			this.rootFolderId = rootFolderId;
			this.isDeepTraversal = isDeepTraversal;
			this.queryFilter = queryFilter;
			this.folderTypes = folderTypes;
			this.isListRootFolder = isListRootFolder;
			if (parentFolderList != null)
			{
				this.queryProperties = parentFolderList.queryProperties;
				this.propertyIndexes = parentFolderList.propertyIndexes;
				this.parentFolderList = parentFolderList;
			}
			else if (extendedProperties != null)
			{
				if (extendedProperties.Equals(FolderList.FolderTreeQueryProperties))
				{
					this.queryProperties = FolderList.FolderTreeQueryProperties;
					this.propertyIndexes = FolderList.FolderTreePropertyIndexes;
				}
				else if (extendedProperties.Equals(FolderList.FolderPropertiesInBasic))
				{
					this.queryProperties = FolderList.FolderPropertiesInBasic;
					this.propertyIndexes = FolderList.FolderPropertyToIndexInBasic;
				}
				else
				{
					FolderList.CombinePropertyList(extendedProperties, out this.queryProperties, out this.propertyIndexes);
				}
			}
			this.Load();
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00005CFB File Offset: 0x00003EFB
		public int Count
		{
			get
			{
				if (this.folders == null)
				{
					return 0;
				}
				return this.folders.Count;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00005D12 File Offset: 0x00003F12
		public MailboxSession MailboxSession
		{
			get
			{
				return this.mailboxSession;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00005D1A File Offset: 0x00003F1A
		protected UserContext UserContext
		{
			get
			{
				return this.userContext;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00005D22 File Offset: 0x00003F22
		internal Dictionary<PropertyDefinition, int> QueryPropertyMap
		{
			get
			{
				return this.propertyIndexes;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00005D2A File Offset: 0x00003F2A
		internal PropertyDefinition[] QueryProperties
		{
			get
			{
				return this.queryProperties;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00005D34 File Offset: 0x00003F34
		private Dictionary<StoreObjectId, object[]> FolderMapping
		{
			get
			{
				if (this.folderMapping == null)
				{
					int num = this.propertyIndexes[FolderSchema.Id];
					this.folderMapping = new Dictionary<StoreObjectId, object[]>(this.folders.Count);
					foreach (object[] array in this.folders)
					{
						this.folderMapping[((VersionedId)array[num]).ObjectId] = array;
					}
				}
				return this.folderMapping;
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00005DD0 File Offset: 0x00003FD0
		internal bool ContainsFolder(StoreObjectId folderId)
		{
			return this.FolderMapping.ContainsKey(folderId);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00005DDE File Offset: 0x00003FDE
		internal object[] GetFolderProperties(StoreObjectId folderId)
		{
			if (this.FolderMapping.ContainsKey(folderId))
			{
				return this.FolderMapping[folderId];
			}
			return null;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00005DFC File Offset: 0x00003FFC
		internal object GetFolderProperty(StoreObjectId folderId, PropertyDefinition propertyDefinition)
		{
			if (!this.propertyIndexes.ContainsKey(propertyDefinition))
			{
				throw new ArgumentException(string.Format("Doesn't contain this property: {0}", propertyDefinition.ToString()));
			}
			object[] folderProperties = this.GetFolderProperties(folderId);
			if (folderProperties == null)
			{
				return null;
			}
			return folderProperties[this.propertyIndexes[propertyDefinition]];
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00005E48 File Offset: 0x00004048
		internal bool TryGetFolderProperty<T>(StoreObjectId folderId, PropertyDefinition propertyDefinition, out T value)
		{
			object folderProperty = this.GetFolderProperty(folderId, propertyDefinition);
			if (folderProperty != null && folderProperty is T)
			{
				value = (T)((object)folderProperty);
				return true;
			}
			value = default(T);
			return false;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00005E80 File Offset: 0x00004080
		internal StoreObjectId[] GetFolderIds()
		{
			if (this.folderIds == null)
			{
				int num = this.propertyIndexes[FolderSchema.Id];
				this.folderIds = new StoreObjectId[this.folders.Count];
				for (int i = 0; i < this.folders.Count; i++)
				{
					this.folderIds[i] = ((VersionedId)this.folders[i][num]).ObjectId;
				}
			}
			return this.folderIds;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00005EF8 File Offset: 0x000040F8
		public object GetPropertyValue(int index, PropertyDefinition propertyDefinition)
		{
			if (this.folders == null)
			{
				throw new OwaInvalidOperationException("The folder list hasn't been loaded!");
			}
			object[] array = this.folders[index];
			return array[this.propertyIndexes[propertyDefinition]];
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00005F34 File Offset: 0x00004134
		public bool IsFolderDeleted(int index)
		{
			if (!this.isListDeletedFolders)
			{
				return false;
			}
			StoreObjectId objectId = ((VersionedId)this.GetPropertyValue(index, FolderSchema.Id)).ObjectId;
			return this.deletedFolderIds.Contains(objectId);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00005F70 File Offset: 0x00004170
		private void Load()
		{
			this.folders = new List<object[]>(50);
			Folder folder = null;
			object[][] array = null;
			try
			{
				if (this.parentFolderList == null)
				{
					if (this.userContext.IsWebPartRequest && this.userContext.IsExplicitLogon)
					{
						folder = Utilities.SafeFolderBind(this.mailboxSession, this.rootFolderId, this.queryProperties);
						if (folder == null)
						{
							return;
						}
					}
					else
					{
						folder = Folder.Bind(this.mailboxSession, this.rootFolderId, this.queryProperties);
					}
					this.deletedFolderIds = new List<StoreObjectId>(10);
					if (this.isListRootFolder)
					{
						this.folders.Add(folder.GetProperties(this.queryProperties));
					}
					using (QueryResult queryResult = folder.FolderQuery(this.isDeepTraversal ? FolderQueryFlags.DeepTraversal : FolderQueryFlags.None, (this.queryFilter == null) ? FolderList.NonHiddenFilter : new AndFilter(new QueryFilter[]
					{
						FolderList.NonHiddenFilter,
						this.queryFilter
					}), null, this.queryProperties))
					{
						array = Utilities.FetchRowsFromQueryResult(queryResult, 10000);
						goto IL_104;
					}
				}
				array = this.FetchRowsFromParentFolderList();
				IL_104:
				bool flag = false;
				int num = -1;
				int i = 0;
				while (i < array.Length)
				{
					bool flag2 = false;
					StoreObjectId storeObjectId = null;
					object[] array2 = array[i];
					if (!flag)
					{
						storeObjectId = ((VersionedId)array2[this.propertyIndexes[FolderSchema.Id]]).ObjectId;
						if (Utilities.IsDefaultFolderId(this.mailboxSession, storeObjectId, DefaultFolderType.DeletedItems))
						{
							num = (int)array2[this.propertyIndexes[FolderSchema.FolderHierarchyDepth]];
						}
						else if (num != -1)
						{
							int num2 = (int)array2[this.propertyIndexes[FolderSchema.FolderHierarchyDepth]];
							if (num2 == num)
							{
								flag = true;
								flag2 = false;
							}
							else
							{
								flag2 = true;
							}
						}
					}
					if (flag2)
					{
						if (this.isListDeletedFolders)
						{
							goto IL_1E1;
						}
					}
					else
					{
						if (this.folderTypes == null)
						{
							goto IL_1E1;
						}
						object obj = array2[this.propertyIndexes[StoreObjectSchema.ContainerClass]];
						if (!(obj is PropertyError))
						{
							string folderType = (string)obj;
							if (this.IsQueriedType(folderType))
							{
								goto IL_1E1;
							}
						}
					}
					IL_212:
					i++;
					continue;
					IL_1E1:
					if (this.folders.Count >= this.maxFolders)
					{
						break;
					}
					this.folders.Add(array2);
					if (flag2)
					{
						this.deletedFolderIds.Add(storeObjectId);
						goto IL_212;
					}
					goto IL_212;
				}
				if (this.sortProperty != null)
				{
					this.folders.Sort(this);
				}
			}
			finally
			{
				if (folder != null)
				{
					folder.Dispose();
				}
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000061F4 File Offset: 0x000043F4
		public int Compare(object[] rowX, object[] rowY)
		{
			if (this.sortProperty == null)
			{
				return 0;
			}
			object obj = rowX[this.propertyIndexes[this.sortProperty]];
			object obj2 = rowY[this.propertyIndexes[this.sortProperty]];
			if (this.sortProperty == StoreObjectSchema.CreationTime)
			{
				ExDateTime dt = (ExDateTime)obj;
				ExDateTime dt2 = (ExDateTime)obj2;
				return ExDateTime.Compare(dt, dt2);
			}
			if (this.sortProperty == FolderSchema.Id || this.sortProperty == FolderSchema.FolderHierarchyDepth)
			{
				int num = (int)obj;
				int num2 = (int)obj2;
				return num - num2;
			}
			string strA = (string)obj;
			string strB = (string)obj2;
			return string.Compare(strA, strB, StringComparison.CurrentCulture);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000062A0 File Offset: 0x000044A0
		private bool IsQueriedType(string folderType)
		{
			if (this.folderTypes == null)
			{
				return true;
			}
			for (int i = 0; i < this.folderTypes.Length; i++)
			{
				if (ObjectClass.IsOfClass(folderType, this.folderTypes[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000062E0 File Offset: 0x000044E0
		private object[][] FetchRowsFromParentFolderList()
		{
			HashSet<StoreObjectId> hashSet = new HashSet<StoreObjectId>();
			List<object[]> list = new List<object[]>();
			int num = 0;
			bool flag = false;
			hashSet.Add(this.rootFolderId);
			foreach (object[] array in this.parentFolderList.folders)
			{
				StoreObjectId objectId = ((VersionedId)array[this.propertyIndexes[FolderSchema.Id]]).ObjectId;
				StoreObjectId item = (StoreObjectId)array[this.propertyIndexes[StoreObjectSchema.ParentItemId]];
				if (this.rootFolderId.Equals(objectId))
				{
					num = (int)array[this.propertyIndexes[FolderSchema.FolderHierarchyDepth]];
					if (this.isListRootFolder && !flag)
					{
						object[] array2 = new object[this.queryProperties.Length];
						array.CopyTo(array2, 0);
						array2[this.propertyIndexes[FolderSchema.FolderHierarchyDepth]] = 0;
						this.folders.Add(array2);
						flag = true;
					}
				}
				if (hashSet.Contains(item))
				{
					int num2 = (int)array[this.propertyIndexes[FolderSchema.FolderHierarchyDepth]];
					object[] array2 = new object[this.queryProperties.Length];
					array.CopyTo(array2, 0);
					array2[this.propertyIndexes[FolderSchema.FolderHierarchyDepth]] = num2 - num;
					list.Add(array2);
					if (this.isDeepTraversal && !hashSet.Contains(objectId))
					{
						hashSet.Add(objectId);
					}
				}
			}
			if (list != null)
			{
				return list.ToArray();
			}
			return new object[0][];
		}

		// Token: 0x04000064 RID: 100
		private static readonly PropertyDefinition[] defaultQueryProperties = new PropertyDefinition[]
		{
			FolderSchema.Id,
			FolderSchema.DisplayName,
			StoreObjectSchema.ContainerClass,
			StoreObjectSchema.CreationTime,
			FolderSchema.FolderHierarchyDepth
		};

		// Token: 0x04000065 RID: 101
		internal static readonly PropertyDefinition[] FolderPropertiesInBasic = new PropertyDefinition[]
		{
			FolderSchema.Id,
			FolderSchema.DisplayName,
			StoreObjectSchema.ContainerClass,
			FolderSchema.UnreadCount,
			FolderSchema.ItemCount,
			FolderSchema.ExtendedFolderFlags,
			StoreObjectSchema.CreationTime,
			FolderSchema.FolderHierarchyDepth
		};

		// Token: 0x04000066 RID: 102
		internal static readonly PropertyDefinition[] FolderTreeQueryProperties = new PropertyDefinition[]
		{
			FolderSchema.Id,
			StoreObjectSchema.ParentEntryId,
			StoreObjectSchema.ParentItemId,
			StoreObjectSchema.RecordKey,
			FolderSchema.SearchFolderAllowAgeout,
			FolderSchema.DisplayName,
			StoreObjectSchema.CreationTime,
			StoreObjectSchema.LastModifiedTime,
			FolderSchema.UnreadCount,
			FolderSchema.ItemCount,
			StoreObjectSchema.ContainerClass,
			FolderSchema.FolderHierarchyDepth,
			FolderSchema.HasChildren,
			FolderSchema.ExtendedFolderFlags,
			FolderSchema.IsOutlookSearchFolder,
			FolderSchema.OutlookSearchFolderClsId,
			ViewStateProperties.FilteredViewLabel,
			ViewStateProperties.FilteredViewAccessTime,
			ViewStateProperties.TreeNodeCollapseStatus,
			FolderSchema.AdminFolderFlags,
			FolderSchema.ELCPolicyIds,
			StoreObjectSchema.EffectiveRights
		};

		// Token: 0x04000067 RID: 103
		internal static readonly Dictionary<PropertyDefinition, int> FolderPropertyToIndexInBasic = Utilities.GetPropertyToIndexMap(FolderList.FolderPropertiesInBasic);

		// Token: 0x04000068 RID: 104
		internal static readonly Dictionary<PropertyDefinition, int> FolderTreePropertyIndexes = Utilities.GetPropertyToIndexMap(FolderList.FolderTreeQueryProperties);

		// Token: 0x04000069 RID: 105
		private static readonly Dictionary<PropertyDefinition, int> defaultPropertyIndexes = Utilities.GetPropertyToIndexMap(FolderList.defaultQueryProperties);

		// Token: 0x0400006A RID: 106
		private UserContext userContext;

		// Token: 0x0400006B RID: 107
		private MailboxSession mailboxSession;

		// Token: 0x0400006C RID: 108
		private StoreObjectId rootFolderId;

		// Token: 0x0400006D RID: 109
		private string[] folderTypes;

		// Token: 0x0400006E RID: 110
		private int maxFolders;

		// Token: 0x0400006F RID: 111
		private bool isListDeletedFolders;

		// Token: 0x04000070 RID: 112
		private bool isListRootFolder;

		// Token: 0x04000071 RID: 113
		private bool isDeepTraversal;

		// Token: 0x04000072 RID: 114
		private PropertyDefinition sortProperty;

		// Token: 0x04000073 RID: 115
		private QueryFilter queryFilter;

		// Token: 0x04000074 RID: 116
		private PropertyDefinition[] queryProperties = FolderList.defaultQueryProperties;

		// Token: 0x04000075 RID: 117
		private Dictionary<PropertyDefinition, int> propertyIndexes = FolderList.defaultPropertyIndexes;

		// Token: 0x04000076 RID: 118
		private Dictionary<StoreObjectId, object[]> folderMapping;

		// Token: 0x04000077 RID: 119
		private StoreObjectId[] folderIds;

		// Token: 0x04000078 RID: 120
		private FolderList parentFolderList;

		// Token: 0x04000079 RID: 121
		private List<object[]> folders;

		// Token: 0x0400007A RID: 122
		private List<StoreObjectId> deletedFolderIds;

		// Token: 0x0400007B RID: 123
		protected static readonly QueryFilter NonHiddenFilter = new ComparisonFilter(ComparisonOperator.NotEqual, FolderSchema.IsHidden, true);
	}
}

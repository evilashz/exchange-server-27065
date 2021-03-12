using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.AirSync;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000A3 RID: 163
	internal class FolderTree : ICustomSerializableBuilder, ICustomSerializable, IEnumerable
	{
		// Token: 0x060008FF RID: 2303 RVA: 0x00035BE4 File Offset: 0x00033DE4
		public FolderTree()
		{
			this.folderTree = new Dictionary<ISyncItemId, FolderTree.FolderInfo>();
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x00035BF7 File Offset: 0x00033DF7
		// (set) Token: 0x06000901 RID: 2305 RVA: 0x00035BFE File Offset: 0x00033DFE
		public ushort TypeId
		{
			get
			{
				return FolderTree.typeId;
			}
			set
			{
				FolderTree.typeId = value;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x00035C06 File Offset: 0x00033E06
		public bool IsDirty
		{
			get
			{
				return this.isDirty;
			}
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00035C10 File Offset: 0x00033E10
		public static void BuildFolderTree(MailboxSession mailboxSession, SyncState syncState)
		{
			StoreObjectId defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.Root);
			SharingSubscriptionData[] array = null;
			using (SharingSubscriptionManager sharingSubscriptionManager = new SharingSubscriptionManager(mailboxSession))
			{
				array = sharingSubscriptionManager.GetAll();
			}
			using (Folder folder = Folder.Bind(mailboxSession, defaultFolderId))
			{
				FolderTree folderTree;
				using (QueryResult queryResult = folder.FolderQuery(FolderQueryFlags.DeepTraversal, null, null, FolderTree.fetchProperties))
				{
					folderTree = new FolderTree();
					object[][] rows;
					do
					{
						rows = queryResult.GetRows(10000);
						for (int i = 0; i < rows.Length; i++)
						{
							MailboxSyncItemId mailboxSyncItemId = MailboxSyncItemId.CreateForNewItem(((VersionedId)rows[i][0]).ObjectId);
							MailboxSyncItemId mailboxSyncItemId2 = MailboxSyncItemId.CreateForNewItem((StoreObjectId)rows[i][1]);
							folderTree.AddFolder(mailboxSyncItemId);
							object obj = rows[i][3];
							int num = (obj is PropertyError) ? 0 : ((int)obj);
							if ((num & 1073741824) != 0)
							{
								for (int j = 0; j < array.Length; j++)
								{
									if (array[j].LocalFolderId.Equals(mailboxSyncItemId.NativeId))
									{
										folderTree.SetPermissions(mailboxSyncItemId, SyncPermissions.Readonly);
										folderTree.SetOwner(mailboxSyncItemId, array[j].SharerIdentity);
										break;
									}
								}
							}
							if (!defaultFolderId.Equals(mailboxSyncItemId2.NativeId))
							{
								folderTree.AddFolder(mailboxSyncItemId2);
								folderTree.LinkChildToParent(mailboxSyncItemId2, mailboxSyncItemId);
							}
							if ((bool)rows[i][2])
							{
								folderTree.SetHidden(mailboxSyncItemId, true);
							}
						}
					}
					while (rows.Length != 0);
				}
				syncState[CustomStateDatumType.FullFolderTree] = folderTree;
				syncState[CustomStateDatumType.RecoveryFullFolderTree] = syncState[CustomStateDatumType.FullFolderTree];
			}
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x00035DFC File Offset: 0x00033FFC
		public bool AddFolder(ISyncItemId folderId)
		{
			if (this.folderTree.ContainsKey(folderId))
			{
				return false;
			}
			this.folderTree[folderId] = new FolderTree.FolderInfo();
			this.isDirty = true;
			return true;
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x00035E27 File Offset: 0x00034027
		public bool Contains(ISyncItemId folderId)
		{
			return this.folderTree.ContainsKey(folderId);
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x00035E35 File Offset: 0x00034035
		public List<ISyncItemId> GetChildren(ISyncItemId folderId)
		{
			return new List<ISyncItemId>(this.GetFolderInfo(folderId).Children);
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x00035E48 File Offset: 0x00034048
		public string GetOwner(ISyncItemId folderId)
		{
			return this.GetFolderInfo(folderId).Owner;
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x00035E56 File Offset: 0x00034056
		public ISyncItemId GetParentId(ISyncItemId folderId)
		{
			return this.GetFolderInfo(folderId).ParentId;
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x00035E64 File Offset: 0x00034064
		public SyncPermissions GetPermissions(ISyncItemId folderId)
		{
			return this.GetFolderInfo(folderId).Permissions;
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x00035E72 File Offset: 0x00034072
		public bool IsHidden(ISyncItemId folderId)
		{
			return this.GetFolderInfo(folderId).Hidden;
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x00035E80 File Offset: 0x00034080
		public bool IsHiddenDueToParent(ISyncItemId folderId)
		{
			return this.GetFolderInfo(folderId).HiddenDueToParent;
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x00035E8E File Offset: 0x0003408E
		public bool IsSharedFolder(ISyncItemId folderId)
		{
			return this.GetFolderInfo(folderId).IsSharedFolder;
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x00035E9C File Offset: 0x0003409C
		public void LinkChildToParent(ISyncItemId parentId, ISyncItemId childId)
		{
			this.LinkChildToParent(parentId, childId, null);
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x00035EA7 File Offset: 0x000340A7
		public void LinkChildToParent(ISyncItemId parentId, ISyncItemId childId, HashSet<ISyncItemId> visibilityChangedList)
		{
			this.GetFolderInfo(parentId).AddChild(childId, this, visibilityChangedList);
			this.GetFolderInfo(childId).ParentId = parentId;
			this.isDirty = true;
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x00035ECC File Offset: 0x000340CC
		public void RemoveFolder(ISyncItemId folderId)
		{
			ISyncItemId parentId = this.GetFolderInfo(folderId).ParentId;
			if (parentId != null)
			{
				this.UnlinkChild(parentId, folderId);
			}
			this.isDirty |= this.folderTree.Remove(folderId);
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x00035F0A File Offset: 0x0003410A
		public void RemoveFolderAndChildren(ISyncItemId folderId, FolderIdMapping folderIdMapping)
		{
			this.RemoveFolderAndChildren(folderId, folderIdMapping, 0, this.folderTree.Keys.Count);
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x00035F26 File Offset: 0x00034126
		public void SetHidden(ISyncItemId folderId, bool newValue)
		{
			this.SetHidden(folderId, newValue, null);
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x00035F31 File Offset: 0x00034131
		public void SetHidden(ISyncItemId folderId, bool newValue, HashSet<ISyncItemId> visibilityChangedList)
		{
			this.isDirty |= this.GetFolderInfo(folderId).SetHidden(newValue, this, visibilityChangedList);
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00035F4F File Offset: 0x0003414F
		public void SetHiddenDueToParent(ISyncItemId folderId, bool newValue, HashSet<ISyncItemId> visibilityChangedList)
		{
			this.isDirty |= this.GetFolderInfo(folderId).SetHiddenDueToParent(newValue, this, visibilityChangedList);
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x00035F70 File Offset: 0x00034170
		public void SetOwner(ISyncItemId folderId, string owner)
		{
			FolderTree.FolderInfo folderInfo = this.GetFolderInfo(folderId);
			if (!string.Equals(folderInfo.Owner, owner))
			{
				folderInfo.Owner = owner;
				this.isDirty = true;
			}
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x00035FA4 File Offset: 0x000341A4
		public void SetPermissions(ISyncItemId folderId, SyncPermissions permissions)
		{
			FolderTree.FolderInfo folderInfo = this.GetFolderInfo(folderId);
			if (folderInfo.Permissions != permissions)
			{
				folderInfo.Permissions = permissions;
				this.isDirty = true;
			}
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x00035FD0 File Offset: 0x000341D0
		public void TraceTree()
		{
			AirSyncDiagnostics.TraceError(ExTraceGlobals.AlgorithmTracer, this, "Inconsistency found in FolderTree.  Dumping contents of tree:");
			foreach (KeyValuePair<ISyncItemId, FolderTree.FolderInfo> keyValuePair in this.folderTree)
			{
				FolderTree.FolderInfo value = keyValuePair.Value;
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < value.Children.Count; i++)
				{
					stringBuilder.Append(value.Children[i]);
					if (i + 1 < value.Children.Count)
					{
						stringBuilder.Append(", ");
					}
				}
				AirSyncDiagnostics.TraceError(ExTraceGlobals.AlgorithmTracer, this, "FolderSyncId: {0}, ParentSyncId: {1}, Hidden: {2}, HiddenDueToParent: {3}, Children: {4}", new object[]
				{
					keyValuePair.Key,
					value.ParentId,
					value.Hidden,
					value.HiddenDueToParent,
					stringBuilder
				});
			}
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x000360D8 File Offset: 0x000342D8
		public void UnlinkChild(ISyncItemId parentId, ISyncItemId childId)
		{
			FolderTree.FolderInfo folderInfo = this.GetFolderInfo(parentId);
			FolderTree.FolderInfo folderInfo2 = this.GetFolderInfo(childId);
			if (!parentId.Equals(folderInfo2.ParentId) || !folderInfo.Children.Contains(childId))
			{
				AirSyncDiagnostics.TraceError<ISyncItemId, ISyncItemId, ISyncItemId>(ExTraceGlobals.AlgorithmTracer, this, "Tried to unlink a child folder from a folder that was not its parent.  ChildSyncId: {0}, child's ParentSyncId: {1}, ParentSyncId passed in: {2}", childId, folderInfo2.ParentId, parentId);
				this.TraceTree();
				return;
			}
			if (folderInfo.Children.Remove(childId))
			{
				this.GetFolderInfo(childId).ParentId = null;
				this.isDirty = true;
			}
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x00036152 File Offset: 0x00034352
		public ICustomSerializable BuildObject()
		{
			return new FolderTree();
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0003615C File Offset: 0x0003435C
		public void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool)
		{
			GenericDictionaryData<DerivedData<ISyncItemId>, ISyncItemId, FolderTree.FolderInfo> genericDictionaryData = new GenericDictionaryData<DerivedData<ISyncItemId>, ISyncItemId, FolderTree.FolderInfo>();
			genericDictionaryData.DeserializeData(reader, componentDataPool);
			this.folderTree = genericDictionaryData.Data;
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x00036183 File Offset: 0x00034383
		public void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool)
		{
			new GenericDictionaryData<DerivedData<ISyncItemId>, ISyncItemId, FolderTree.FolderInfo>(this.folderTree).SerializeData(writer, componentDataPool);
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x00036197 File Offset: 0x00034397
		public IEnumerator GetEnumerator()
		{
			return this.folderTree.Keys.GetEnumerator();
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x000361B0 File Offset: 0x000343B0
		private FolderTree.FolderInfo GetFolderInfo(ISyncItemId folderId)
		{
			if (folderId == null)
			{
				throw new ArgumentNullException("folderId");
			}
			if (!this.folderTree.ContainsKey(folderId))
			{
				this.TraceTree();
				AirSyncPermanentException ex = new AirSyncPermanentException(HttpStatusCode.InternalServerError, StatusCode.ServerError, null, false);
				if (Command.CurrentCommand.ProtocolLogger != null)
				{
					string text = string.Format("{0}InvalidFolderIdException", Command.CurrentCommand.Request.CommandType);
					Command.CurrentCommand.ProtocolLogger.SetValue(ProtocolLoggerData.Error, text);
					ex.ErrorStringForProtocolLogger = text;
				}
				else
				{
					ex.ErrorStringForProtocolLogger = "BadFolderIdInFolderTree";
				}
				throw ex;
			}
			return this.folderTree[folderId];
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x00036250 File Offset: 0x00034450
		private bool RemoveFolderAndChildren(ISyncItemId folderId, FolderIdMapping folderIdMapping, int foldersSeen, int numberOfFoldersInTree)
		{
			FolderTree.FolderInfo folderInfo = this.GetFolderInfo(folderId);
			foldersSeen++;
			if (foldersSeen > numberOfFoldersInTree)
			{
				AirSyncDiagnostics.TraceError<int>(ExTraceGlobals.AlgorithmTracer, this, "Error: Loop detected in folder tree.  NumberOfFoldersInTree: {0}", numberOfFoldersInTree);
				this.TraceTree();
				return false;
			}
			int num = 0;
			while (folderInfo.Children.Count > num)
			{
				ISyncItemId syncItemId = folderInfo.Children[num];
				if (this.folderTree.ContainsKey(syncItemId))
				{
					if (!this.RemoveFolderAndChildren(syncItemId, folderIdMapping, foldersSeen, numberOfFoldersInTree))
					{
						return false;
					}
				}
				else
				{
					num++;
					MailboxSyncItemId mailboxSyncItemId = syncItemId as MailboxSyncItemId;
					AirSyncDiagnostics.TraceError<string>(ExTraceGlobals.AlgorithmTracer, this, "FolderTree.RemoveFolderAndChildren could not find child folder: {0} to remove.", (mailboxSyncItemId == null) ? "<folder is not a MailboxSyncItemId>" : mailboxSyncItemId.ToString());
				}
			}
			if (folderIdMapping.Contains(folderId))
			{
				folderIdMapping.Delete(new ISyncItemId[]
				{
					folderId
				});
			}
			this.RemoveFolder(folderId);
			return true;
		}

		// Token: 0x040005C1 RID: 1473
		private static PropertyDefinition[] fetchProperties = new PropertyDefinition[]
		{
			MessageItemSchema.FolderId,
			StoreObjectSchema.ParentItemId,
			FolderSchema.IsHidden,
			FolderSchema.ExtendedFolderFlags
		};

		// Token: 0x040005C2 RID: 1474
		private static ushort typeId;

		// Token: 0x040005C3 RID: 1475
		private Dictionary<ISyncItemId, FolderTree.FolderInfo> folderTree;

		// Token: 0x040005C4 RID: 1476
		private bool isDirty;

		// Token: 0x020000A4 RID: 164
		private enum FetchPropertiesEnum
		{
			// Token: 0x040005C6 RID: 1478
			Id,
			// Token: 0x040005C7 RID: 1479
			ParentId,
			// Token: 0x040005C8 RID: 1480
			IsHidden,
			// Token: 0x040005C9 RID: 1481
			ExtendedFolderFlags
		}

		// Token: 0x020000A5 RID: 165
		private sealed class FolderInfo : ICustomSerializableBuilder, ICustomSerializable
		{
			// Token: 0x0600091F RID: 2335 RVA: 0x00036352 File Offset: 0x00034552
			public FolderInfo()
			{
				this.Children = new List<ISyncItemId>(0);
			}

			// Token: 0x17000369 RID: 873
			// (get) Token: 0x06000920 RID: 2336 RVA: 0x00036366 File Offset: 0x00034566
			// (set) Token: 0x06000921 RID: 2337 RVA: 0x0003636E File Offset: 0x0003456E
			public ISyncItemId ParentId { get; set; }

			// Token: 0x1700036A RID: 874
			// (get) Token: 0x06000922 RID: 2338 RVA: 0x00036377 File Offset: 0x00034577
			// (set) Token: 0x06000923 RID: 2339 RVA: 0x0003637F File Offset: 0x0003457F
			public List<ISyncItemId> Children { get; private set; }

			// Token: 0x1700036B RID: 875
			// (get) Token: 0x06000924 RID: 2340 RVA: 0x00036388 File Offset: 0x00034588
			public bool Hidden
			{
				get
				{
					return this.hidden || this.HiddenDueToParent;
				}
			}

			// Token: 0x1700036C RID: 876
			// (get) Token: 0x06000925 RID: 2341 RVA: 0x0003639A File Offset: 0x0003459A
			// (set) Token: 0x06000926 RID: 2342 RVA: 0x000363A2 File Offset: 0x000345A2
			public bool HiddenDueToParent { get; set; }

			// Token: 0x1700036D RID: 877
			// (get) Token: 0x06000927 RID: 2343 RVA: 0x000363AB File Offset: 0x000345AB
			// (set) Token: 0x06000928 RID: 2344 RVA: 0x000363B3 File Offset: 0x000345B3
			public string Owner { get; set; }

			// Token: 0x1700036E RID: 878
			// (get) Token: 0x06000929 RID: 2345 RVA: 0x000363BC File Offset: 0x000345BC
			// (set) Token: 0x0600092A RID: 2346 RVA: 0x000363C4 File Offset: 0x000345C4
			public SyncPermissions Permissions { get; set; }

			// Token: 0x1700036F RID: 879
			// (get) Token: 0x0600092B RID: 2347 RVA: 0x000363CD File Offset: 0x000345CD
			public bool IsSharedFolder
			{
				get
				{
					return !string.IsNullOrEmpty(this.Owner);
				}
			}

			// Token: 0x17000370 RID: 880
			// (get) Token: 0x0600092C RID: 2348 RVA: 0x000363DD File Offset: 0x000345DD
			// (set) Token: 0x0600092D RID: 2349 RVA: 0x000363E4 File Offset: 0x000345E4
			public ushort TypeId
			{
				get
				{
					return FolderTree.FolderInfo.typeId;
				}
				set
				{
					FolderTree.FolderInfo.typeId = value;
				}
			}

			// Token: 0x0600092E RID: 2350 RVA: 0x000363EC File Offset: 0x000345EC
			public void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool)
			{
				DerivedData<ISyncItemId> derivedData = new DerivedData<ISyncItemId>();
				derivedData.DeserializeData(reader, componentDataPool);
				this.ParentId = derivedData.Data;
				GenericListData<DerivedData<ISyncItemId>, ISyncItemId> genericListData = new GenericListData<DerivedData<ISyncItemId>, ISyncItemId>();
				genericListData.DeserializeData(reader, componentDataPool);
				this.Children = genericListData.Data;
				BooleanData booleanDataInstance = componentDataPool.GetBooleanDataInstance();
				booleanDataInstance.DeserializeData(reader, componentDataPool);
				this.hidden = booleanDataInstance.Data;
				booleanDataInstance.DeserializeData(reader, componentDataPool);
				this.HiddenDueToParent = booleanDataInstance.Data;
				Int32Data int32DataInstance = componentDataPool.GetInt32DataInstance();
				int32DataInstance.DeserializeData(reader, componentDataPool);
				this.Permissions = (SyncPermissions)int32DataInstance.Data;
				StringData stringDataInstance = componentDataPool.GetStringDataInstance();
				stringDataInstance.DeserializeData(reader, componentDataPool);
				this.Owner = stringDataInstance.Data;
			}

			// Token: 0x0600092F RID: 2351 RVA: 0x00036498 File Offset: 0x00034698
			public void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool)
			{
				DerivedData<ISyncItemId> derivedData = new DerivedData<ISyncItemId>(this.ParentId);
				derivedData.SerializeData(writer, componentDataPool);
				GenericListData<DerivedData<ISyncItemId>, ISyncItemId> genericListData = new GenericListData<DerivedData<ISyncItemId>, ISyncItemId>(this.Children);
				genericListData.SerializeData(writer, componentDataPool);
				componentDataPool.GetBooleanDataInstance().Bind(this.hidden).SerializeData(writer, componentDataPool);
				componentDataPool.GetBooleanDataInstance().Bind(this.HiddenDueToParent).SerializeData(writer, componentDataPool);
				componentDataPool.GetInt32DataInstance().Bind((int)this.Permissions).SerializeData(writer, componentDataPool);
				componentDataPool.GetStringDataInstance().Bind(this.Owner).SerializeData(writer, componentDataPool);
			}

			// Token: 0x06000930 RID: 2352 RVA: 0x0003652D File Offset: 0x0003472D
			public ICustomSerializable BuildObject()
			{
				return new FolderTree.FolderInfo();
			}

			// Token: 0x06000931 RID: 2353 RVA: 0x00036534 File Offset: 0x00034734
			public void AddChild(ISyncItemId childId, FolderTree fullFolderTree, HashSet<ISyncItemId> visibilityChangedList)
			{
				this.Children.Add(childId);
				fullFolderTree.SetHiddenDueToParent(childId, this.Hidden, visibilityChangedList);
			}

			// Token: 0x06000932 RID: 2354 RVA: 0x00036550 File Offset: 0x00034750
			public bool SetHidden(bool newValue, FolderTree fullFolderTree, HashSet<ISyncItemId> visibilityChangedList)
			{
				bool result = false;
				if (this.ParentId == null && this.HiddenDueToParent)
				{
					this.HiddenDueToParent = false;
					result = true;
				}
				if (this.hidden != newValue)
				{
					this.hidden = newValue;
					foreach (ISyncItemId syncItemId in this.Children)
					{
						if (visibilityChangedList != null)
						{
							visibilityChangedList.Add(syncItemId);
						}
						fullFolderTree.SetHiddenDueToParent(syncItemId, newValue || this.HiddenDueToParent, visibilityChangedList);
					}
					result = true;
				}
				return result;
			}

			// Token: 0x06000933 RID: 2355 RVA: 0x000365EC File Offset: 0x000347EC
			public bool SetHiddenDueToParent(bool newValue, FolderTree fullFolderTree, HashSet<ISyncItemId> visibilityChangedList)
			{
				if (this.HiddenDueToParent != newValue)
				{
					this.HiddenDueToParent = newValue;
					if (!this.hidden)
					{
						foreach (ISyncItemId syncItemId in this.Children)
						{
							if (visibilityChangedList != null)
							{
								visibilityChangedList.Add(syncItemId);
							}
							fullFolderTree.SetHiddenDueToParent(syncItemId, newValue, visibilityChangedList);
						}
					}
					return true;
				}
				return false;
			}

			// Token: 0x040005CA RID: 1482
			private static ushort typeId;

			// Token: 0x040005CB RID: 1483
			private bool hidden;
		}
	}
}

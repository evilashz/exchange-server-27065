using System;
using System.Collections.Generic;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.InfoWorker.Common.ELC;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000083 RID: 131
	internal class UserRetentionPolicyCache
	{
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x00024EED File Offset: 0x000230ED
		internal StoreTagData DefaultTag
		{
			get
			{
				return this.defaultTag;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x00024EF5 File Offset: 0x000230F5
		internal StoreTagData DefaultVmTag
		{
			get
			{
				return this.defaultVmTag;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x00024EFD File Offset: 0x000230FD
		internal Dictionary<Guid, StoreTagData> StoreTagDictionary
		{
			get
			{
				return this.storeTagDictionary;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x00024F05 File Offset: 0x00023105
		internal Dictionary<StoreObjectId, Guid> FolderTagDictionary
		{
			get
			{
				return this.folderTagDictionary;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x00024F0D File Offset: 0x0002310D
		internal Dictionary<StoreObjectId, Guid> FolderArchiveTagDictionary
		{
			get
			{
				return this.folderArchiveTagDictionary;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x00024F15 File Offset: 0x00023115
		internal Dictionary<StoreObjectId, DefaultFolderType> FolderTypeDictionary
		{
			get
			{
				return this.folderTypeDictionary;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x00024F1D File Offset: 0x0002311D
		internal bool UnderRetentionPolicy
		{
			get
			{
				return this.underRetentionPolicy;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x00024F25 File Offset: 0x00023125
		internal byte[] RootFolderId
		{
			get
			{
				return this.rootFolderId;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x00024F2D File Offset: 0x0002312D
		internal byte[] SentItemsId
		{
			get
			{
				return this.sentItemsId;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00024F35 File Offset: 0x00023135
		internal byte[] DeletedItemsId
		{
			get
			{
				return this.deletedItemsId;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x00024F3D File Offset: 0x0002313D
		internal Dictionary<string, ContentSetting> ItemClassToPolicyMapping
		{
			get
			{
				return this.itemClassToPolicyMapping;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x00024F45 File Offset: 0x00023145
		internal List<ElcPolicySettings> DefaultContentSettingList
		{
			get
			{
				return this.defaultContentSettingList;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x00024F4D File Offset: 0x0002314D
		// (set) Token: 0x060004DA RID: 1242 RVA: 0x00024F55 File Offset: 0x00023155
		internal bool HasPendingFaiEvent
		{
			get
			{
				return this.hasPendingFaiEvent;
			}
			set
			{
				if (this.readOnlyCache)
				{
					throw new ArgumentException("HasPendingFaiEvent. Pending events should never be set when UnderRetentionPolicy is false.");
				}
				this.hasPendingFaiEvent = value;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x00024F71 File Offset: 0x00023171
		// (set) Token: 0x060004DC RID: 1244 RVA: 0x00024F79 File Offset: 0x00023179
		internal long PendingFaiEventCounter
		{
			get
			{
				return this.pendingFaiEventCounter;
			}
			set
			{
				this.pendingFaiEventCounter = value;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x00024F82 File Offset: 0x00023182
		internal bool ReadOnlyCache
		{
			get
			{
				return this.readOnlyCache;
			}
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00024F8C File Offset: 0x0002318C
		internal static bool IsFolderTypeToSkip(DefaultFolderType folderType)
		{
			foreach (DefaultFolderType defaultFolderType in UserRetentionPolicyCache.typeFoldersToSkip)
			{
				if (folderType == defaultFolderType)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00024FBC File Offset: 0x000231BC
		internal static UserRetentionPolicyCache GetCacheThatIsOff()
		{
			return new UserRetentionPolicyCache
			{
				readOnlyCache = true
			};
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00024FD8 File Offset: 0x000231D8
		internal StoreTagData GetDefaultTag()
		{
			if (!this.haveCheckedForDefault)
			{
				foreach (StoreTagData storeTagData in this.StoreTagDictionary.Values)
				{
					if (storeTagData.Tag.Type == ElcFolderType.All)
					{
						this.defaultTag = storeTagData;
						break;
					}
				}
				this.haveCheckedForDefault = true;
			}
			return this.defaultTag;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00025058 File Offset: 0x00023258
		internal StoreTagData GetDefaultTagAndDefaultVmTag()
		{
			if (!this.checkedForDefaultForMessageClass)
			{
				foreach (StoreTagData storeTagData in this.StoreTagDictionary.Values)
				{
					if (storeTagData.Tag.Type == ElcFolderType.All && storeTagData.ContentSettings.Count != 0)
					{
						using (SortedDictionary<Guid, ContentSetting>.ValueCollection.Enumerator enumerator2 = storeTagData.ContentSettings.Values.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								ContentSetting contentSetting = enumerator2.Current;
								if (contentSetting.MessageClass.Equals(ElcMessageClass.AllMailboxContent, StringComparison.CurrentCultureIgnoreCase))
								{
									this.defaultTag = storeTagData;
									break;
								}
								if (contentSetting.MessageClass.Equals(ElcMessageClass.VoiceMail, StringComparison.CurrentCultureIgnoreCase))
								{
									this.defaultVmTag = storeTagData;
									break;
								}
							}
							continue;
						}
					}
					if (storeTagData.Tag.Type == ElcFolderType.All && storeTagData.ContentSettings.Count == 0)
					{
						this.defaultTag = storeTagData;
						break;
					}
				}
				this.checkedForDefaultForMessageClass = true;
			}
			return this.defaultTag;
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00025180 File Offset: 0x00023380
		internal void ResetFolderCaches()
		{
			if (this.folderArchiveTagDictionary != null)
			{
				this.folderArchiveTagDictionary.Clear();
			}
			if (this.folderTagDictionary != null)
			{
				this.folderTagDictionary.Clear();
			}
			if (this.folderTypeDictionary != null)
			{
				this.folderTypeDictionary.Clear();
			}
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x000251BC File Offset: 0x000233BC
		internal void PurgeTag(StoreObjectId id)
		{
			if (id == null)
			{
				return;
			}
			if (this.folderArchiveTagDictionary != null && this.folderArchiveTagDictionary.ContainsKey(id))
			{
				this.folderArchiveTagDictionary.Remove(id);
			}
			if (this.folderTagDictionary != null && this.folderTagDictionary.ContainsKey(id))
			{
				this.folderTagDictionary.Remove(id);
			}
			if (this.folderTypeDictionary != null && this.folderTypeDictionary.ContainsKey(id))
			{
				this.folderTypeDictionary.Remove(id);
			}
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00025238 File Offset: 0x00023438
		internal void ResetState()
		{
			this.storeTagDictionary = null;
			this.haveCheckedForDefault = false;
			this.checkedForDefaultForMessageClass = false;
			this.defaultTag = null;
			this.defaultVmTag = null;
			this.folderTagDictionary = null;
			this.folderArchiveTagDictionary = null;
			this.folderTypeDictionary = null;
			this.underRetentionPolicy = false;
			this.rootFolderId = null;
			this.sentItemsId = null;
			this.deletedItemsId = null;
			this.foldersToSkip = null;
			this.itemClassToPolicyMapping = null;
			this.defaultContentSettingList = null;
			this.pendingFaiEventCounter = -1L;
			this.hasPendingFaiEvent = false;
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x000252C0 File Offset: 0x000234C0
		internal void LoadStoreTagDataFromStore(MailboxSession mailboxSession)
		{
			this.ResetState();
			StoreId defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.Inbox);
			Exception ex = null;
			this.underRetentionPolicy = false;
			this.storeTagDictionary = null;
			this.folderTagDictionary = null;
			this.folderArchiveTagDictionary = null;
			this.itemClassToPolicyMapping = null;
			try
			{
				using (UserConfiguration folderConfiguration = mailboxSession.UserConfigurationManager.GetFolderConfiguration("MRM", UserConfigurationTypes.Stream | UserConfigurationTypes.XML | UserConfigurationTypes.Dictionary, defaultFolderId))
				{
					this.storeTagDictionary = MrmFaiFormatter.Deserialize(folderConfiguration, mailboxSession.MailboxOwner);
					this.haveCheckedForDefault = false;
					this.checkedForDefaultForMessageClass = false;
					this.defaultTag = null;
					this.defaultVmTag = null;
					StoreTagData storeTagData = new StoreTagData();
					storeTagData.Tag = new RetentionTag();
					storeTagData.Tag.Guid = PolicyTagHelper.SystemCleanupTagGuid;
					this.storeTagDictionary[PolicyTagHelper.SystemCleanupTagGuid] = storeTagData;
					this.defaultContentSettingList = new List<ElcPolicySettings>();
					foreach (StoreTagData storeTagData2 in this.storeTagDictionary.Values)
					{
						if (storeTagData2.Tag.Type == ElcFolderType.All)
						{
							foreach (ContentSetting elcContentSetting in storeTagData2.ContentSettings.Values)
							{
								ElcPolicySettings.ParseContentSettings(this.defaultContentSettingList, elcContentSetting);
							}
						}
					}
					this.folderTypeDictionary = new Dictionary<StoreObjectId, DefaultFolderType>();
					this.folderTagDictionary = new Dictionary<StoreObjectId, Guid>();
					this.folderArchiveTagDictionary = new Dictionary<StoreObjectId, Guid>();
					this.itemClassToPolicyMapping = new Dictionary<string, ContentSetting>();
					this.underRetentionPolicy = true;
					if (this.rootFolderId == null)
					{
						StoreObjectId defaultFolderId2 = mailboxSession.GetDefaultFolderId(DefaultFolderType.Root);
						if (defaultFolderId2 != null)
						{
							this.rootFolderId = defaultFolderId2.ProviderLevelItemId;
						}
					}
					if (this.sentItemsId == null)
					{
						StoreObjectId defaultFolderId3 = mailboxSession.GetDefaultFolderId(DefaultFolderType.SentItems);
						if (defaultFolderId3 != null)
						{
							this.sentItemsId = defaultFolderId3.ProviderLevelItemId;
						}
					}
					if (this.deletedItemsId == null)
					{
						StoreObjectId defaultFolderId4 = mailboxSession.GetDefaultFolderId(DefaultFolderType.DeletedItems);
						if (defaultFolderId4 != null)
						{
							this.deletedItemsId = defaultFolderId4.ProviderLevelItemId;
						}
					}
					this.CacheFolderIdsToSkip(mailboxSession);
				}
			}
			catch (ObjectNotFoundException ex2)
			{
				ex = ex2;
			}
			catch (CorruptDataException ex3)
			{
				ex = ex3;
			}
			catch (StorageTransientException ex4)
			{
				ex = ex4;
			}
			catch (StoragePermanentException ex5)
			{
				ex = ex5;
			}
			if (ex != null)
			{
				UserRetentionPolicyCache.Tracer.TraceDebug<UserRetentionPolicyCache, Exception>((long)this.GetHashCode(), "{0}: Config message with store settings is missing or corrupt. Recreate it. Exception: {1}", this, ex);
				this.underRetentionPolicy = false;
				this.storeTagDictionary = null;
				this.folderTagDictionary = null;
				this.folderArchiveTagDictionary = null;
				this.itemClassToPolicyMapping = null;
			}
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x000255C0 File Offset: 0x000237C0
		internal bool IsFolderIdToSkip(byte[] entryId)
		{
			if (entryId != null && this.foldersToSkip != null)
			{
				foreach (byte[] x in this.foldersToSkip)
				{
					if (ArrayComparer<byte>.Comparer.Equals(x, entryId))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0002562C File Offset: 0x0002382C
		private void CacheFolderIdsToSkip(MailboxSession mailboxSession)
		{
			List<byte[]> list = new List<byte[]>(UserRetentionPolicyCache.typeFoldersToSkip.Length);
			foreach (DefaultFolderType defaultFolderType in UserRetentionPolicyCache.typeFoldersToSkip)
			{
				StoreObjectId defaultFolderId = mailboxSession.GetDefaultFolderId(defaultFolderType);
				if (defaultFolderId != null)
				{
					list.Add(defaultFolderId.ProviderLevelItemId);
				}
			}
			this.foldersToSkip = list;
		}

		// Token: 0x040003AD RID: 941
		private static readonly DefaultFolderType[] typeFoldersToSkip = new DefaultFolderType[]
		{
			DefaultFolderType.RecoverableItemsRoot,
			DefaultFolderType.RecoverableItemsDeletions,
			DefaultFolderType.RecoverableItemsVersions,
			DefaultFolderType.RecoverableItemsPurges,
			DefaultFolderType.Drafts,
			DefaultFolderType.DeferredActionFolder,
			DefaultFolderType.FreeBusyData,
			DefaultFolderType.Calendar,
			DefaultFolderType.Tasks,
			DefaultFolderType.Contacts,
			DefaultFolderType.DeletedItems,
			DefaultFolderType.JunkEmail,
			DefaultFolderType.Outbox,
			DefaultFolderType.SentItems
		};

		// Token: 0x040003AE RID: 942
		private static readonly Trace Tracer = ExTraceGlobals.EventBasedAssistantTracer;

		// Token: 0x040003AF RID: 943
		private static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;

		// Token: 0x040003B0 RID: 944
		private Dictionary<Guid, StoreTagData> storeTagDictionary;

		// Token: 0x040003B1 RID: 945
		private bool haveCheckedForDefault;

		// Token: 0x040003B2 RID: 946
		private bool checkedForDefaultForMessageClass;

		// Token: 0x040003B3 RID: 947
		private StoreTagData defaultTag;

		// Token: 0x040003B4 RID: 948
		private StoreTagData defaultVmTag;

		// Token: 0x040003B5 RID: 949
		private Dictionary<StoreObjectId, Guid> folderTagDictionary;

		// Token: 0x040003B6 RID: 950
		private Dictionary<StoreObjectId, Guid> folderArchiveTagDictionary;

		// Token: 0x040003B7 RID: 951
		private Dictionary<StoreObjectId, DefaultFolderType> folderTypeDictionary;

		// Token: 0x040003B8 RID: 952
		private bool underRetentionPolicy;

		// Token: 0x040003B9 RID: 953
		private byte[] rootFolderId;

		// Token: 0x040003BA RID: 954
		private byte[] sentItemsId;

		// Token: 0x040003BB RID: 955
		private byte[] deletedItemsId;

		// Token: 0x040003BC RID: 956
		private List<byte[]> foldersToSkip;

		// Token: 0x040003BD RID: 957
		private Dictionary<string, ContentSetting> itemClassToPolicyMapping;

		// Token: 0x040003BE RID: 958
		private List<ElcPolicySettings> defaultContentSettingList;

		// Token: 0x040003BF RID: 959
		private bool hasPendingFaiEvent;

		// Token: 0x040003C0 RID: 960
		private long pendingFaiEventCounter = -1L;

		// Token: 0x040003C1 RID: 961
		private bool readOnlyCache;
	}
}

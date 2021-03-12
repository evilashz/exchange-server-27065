using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200086F RID: 2159
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MasterCategoryList : ICollection<Category>, IEnumerable<Category>, IEnumerable
	{
		// Token: 0x0600514C RID: 20812 RVA: 0x00152718 File Offset: 0x00150918
		public MasterCategoryList(MailboxSession session) : this()
		{
			Util.ThrowOnNullArgument(session, "session");
			this.session = session;
		}

		// Token: 0x0600514D RID: 20813 RVA: 0x00152732 File Offset: 0x00150932
		private MasterCategoryList(MemoryPropertyBag propertyBagToAssume)
		{
			this.propertyBag = propertyBagToAssume;
		}

		// Token: 0x0600514E RID: 20814 RVA: 0x00152767 File Offset: 0x00150967
		private MasterCategoryList() : this(new MemoryPropertyBag())
		{
			this.propertyBag.SetAllPropertiesLoaded();
		}

		// Token: 0x0600514F RID: 20815 RVA: 0x00152780 File Offset: 0x00150980
		private MasterCategoryList(MasterCategoryList copyFrom) : this(new MemoryPropertyBag(copyFrom.propertyBag))
		{
			foreach (Category category in copyFrom)
			{
				this.Add(category.Clone());
			}
		}

		// Token: 0x170016C0 RID: 5824
		// (get) Token: 0x06005150 RID: 20816 RVA: 0x001527E0 File Offset: 0x001509E0
		// (set) Token: 0x06005151 RID: 20817 RVA: 0x001527F2 File Offset: 0x001509F2
		public string DefaultCategoryName
		{
			get
			{
				return this.propertyBag.GetValueOrDefault<string>(MasterCategoryListSchema.DefaultCategory);
			}
			set
			{
				this.propertyBag.SetOrDeleteProperty(MasterCategoryListSchema.DefaultCategory, value);
			}
		}

		// Token: 0x170016C1 RID: 5825
		// (get) Token: 0x06005152 RID: 20818 RVA: 0x00152805 File Offset: 0x00150A05
		public bool LoadedWithProblems
		{
			get
			{
				return this.loadedWithProblems;
			}
		}

		// Token: 0x170016C2 RID: 5826
		// (get) Token: 0x06005153 RID: 20819 RVA: 0x0015280D File Offset: 0x00150A0D
		public int Count
		{
			get
			{
				return this.categories.Count;
			}
		}

		// Token: 0x170016C3 RID: 5827
		// (get) Token: 0x06005154 RID: 20820 RVA: 0x0015281A File Offset: 0x00150A1A
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170016C4 RID: 5828
		// (get) Token: 0x06005155 RID: 20821 RVA: 0x0015281D File Offset: 0x00150A1D
		internal bool IsLoaded
		{
			get
			{
				return this.configurationItemId != null;
			}
		}

		// Token: 0x170016C5 RID: 5829
		public Category this[string categoryName]
		{
			get
			{
				Category result;
				if (!this.categories.TryGetValue(categoryName, out result))
				{
					return null;
				}
				return result;
			}
		}

		// Token: 0x170016C6 RID: 5830
		internal Category this[Guid categoryGuid]
		{
			get
			{
				Category result;
				if (!this.categoriesByGuid.TryGetValue(categoryGuid, out result))
				{
					return null;
				}
				return result;
			}
		}

		// Token: 0x06005158 RID: 20824 RVA: 0x0015286C File Offset: 0x00150A6C
		public static IComparer<Category> CreateUsageBasedComparer(OutlookModule module)
		{
			EnumValidator.ThrowIfInvalid<OutlookModule>(module);
			return new MasterCategoryList.UsageBasedCategoryComparer(module);
		}

		// Token: 0x06005159 RID: 20825 RVA: 0x0015287A File Offset: 0x00150A7A
		public bool Contains(string categoryName)
		{
			if (categoryName == null)
			{
				throw new ArgumentNullException("categoryName");
			}
			return this.categories.ContainsKey(categoryName);
		}

		// Token: 0x0600515A RID: 20826 RVA: 0x00152896 File Offset: 0x00150A96
		public bool HasQuickFlagsMigrationStarted()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600515B RID: 20827 RVA: 0x001528A0 File Offset: 0x00150AA0
		public bool Remove(string categoryName)
		{
			if (categoryName == null)
			{
				throw new ArgumentNullException("categoryName");
			}
			Category category = this[categoryName];
			if (category != null)
			{
				Guid guid = category.Guid;
				category.Abandon();
				this.isListModified = true;
				return this.categories.Remove(categoryName) && this.categoriesByGuid.Remove(guid);
			}
			return false;
		}

		// Token: 0x0600515C RID: 20828 RVA: 0x001528F8 File Offset: 0x00150AF8
		public void Save()
		{
			this.Save(SaveMode.ResolveConflicts);
		}

		// Token: 0x0600515D RID: 20829 RVA: 0x00152904 File Offset: 0x00150B04
		public void Save(SaveMode saveMode)
		{
			EnumValidator.ThrowIfInvalid<SaveMode>(saveMode);
			if (!this.IsLoaded)
			{
				throw new InvalidOperationException("The Master Category List is not loaded and thus cannot be saved");
			}
			this.FlushCategoryUsageLog();
			if (!this.IsDirty())
			{
				return;
			}
			this.propertyBag[MasterCategoryListSchema.LastSavedTime] = ExDateTime.GetNow(ExTimeZone.UtcTimeZone);
			using (UserConfiguration folderConfiguration = this.session.UserConfigurationManager.GetFolderConfiguration("CategoryList", UserConfigurationTypes.XML, this.session.GetDefaultFolderId(DefaultFolderType.Calendar)))
			{
				MasterCategoryList masterCategoryList;
				using (Stream xmlStream = folderConfiguration.GetXmlStream())
				{
					masterCategoryList = ((!this.configurationItemId.Equals(folderConfiguration.VersionedId)) ? this.MergeCopies(saveMode, folderConfiguration, xmlStream) : this);
					xmlStream.Position = 0L;
					using (Stream stream = new MemoryStream((int)xmlStream.Length))
					{
						masterCategoryList.SaveToStream(saveMode, stream, xmlStream);
						if (stream.Length > 524288L)
						{
							throw new StoragePermanentException(ServerStrings.ExMclIsTooBig(stream.Length, 524288L));
						}
						stream.Position = 0L;
						xmlStream.Position = 0L;
						xmlStream.SetLength(0L);
						Util.StreamHandler.CopyStreamData(stream, xmlStream);
					}
				}
				folderConfiguration.Save();
				this.MovePersistentContent(masterCategoryList);
				this.originalMcl = this.Clone();
				this.configurationItemId = folderConfiguration.VersionedId;
				this.loadedWithProblems = false;
				this.propertyBag.ClearChangeInfo();
				this.isListModified = false;
			}
		}

		// Token: 0x0600515E RID: 20830 RVA: 0x00152A9C File Offset: 0x00150C9C
		public Category[] ToArray()
		{
			return Util.CollectionToArray<Category>(this.categories.Values);
		}

		// Token: 0x0600515F RID: 20831 RVA: 0x00152AB0 File Offset: 0x00150CB0
		public void Add(Category item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (this.FindMatch(item) == null)
			{
				item.AssignMasterCategoryList(this);
				this.categoriesByGuid.Add(item.Guid, item);
				this.categories.Add(item.Name, item);
				this.isListModified = true;
				return;
			}
			throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Category \"{0}\" is already in the list", new object[]
			{
				item.Name
			}), "item");
		}

		// Token: 0x06005160 RID: 20832 RVA: 0x00152B34 File Offset: 0x00150D34
		public void Clear()
		{
			foreach (Category category in this.categories.Values)
			{
				category.Abandon();
			}
			this.categories.Clear();
			this.categoriesByGuid.Clear();
			this.DefaultCategoryName = null;
			this.isListModified = true;
		}

		// Token: 0x06005161 RID: 20833 RVA: 0x00152BB0 File Offset: 0x00150DB0
		bool ICollection<Category>.Contains(Category item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			return this.Contains(item.Name);
		}

		// Token: 0x06005162 RID: 20834 RVA: 0x00152BCC File Offset: 0x00150DCC
		public void CopyTo(Category[] array, int arrayIndex)
		{
			this.categories.Values.CopyTo(array, arrayIndex);
		}

		// Token: 0x06005163 RID: 20835 RVA: 0x00152BE0 File Offset: 0x00150DE0
		bool ICollection<Category>.Remove(Category item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			return this.Remove(item.Name);
		}

		// Token: 0x06005164 RID: 20836 RVA: 0x00152BFC File Offset: 0x00150DFC
		public IEnumerator<Category> GetEnumerator()
		{
			return this.categories.Values.GetEnumerator();
		}

		// Token: 0x06005165 RID: 20837 RVA: 0x00152C13 File Offset: 0x00150E13
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.categories.Values.GetEnumerator();
		}

		// Token: 0x06005166 RID: 20838 RVA: 0x00152E28 File Offset: 0x00151028
		internal static IEnumerable<PropValue> ResolveProperties(MemoryPropertyBag client, MemoryPropertyBag server, MemoryPropertyBag original, AcrProfile profile)
		{
			ConflictResolutionResult resolutionResult = profile.ResolveConflicts(MasterCategoryList.GetPropValuesToResolve(client, server, original, profile));
			if (resolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
			{
				throw new Exception();
			}
			foreach (PropertyConflict conflict in resolutionResult.PropertyConflicts)
			{
				yield return new PropValue(InternalSchema.ToStorePropertyDefinition(conflict.PropertyDefinition), conflict.ResolvedValue);
			}
			yield break;
		}

		// Token: 0x06005167 RID: 20839 RVA: 0x00152E5C File Offset: 0x0015105C
		internal static void Delete(MailboxSession mailboxSession)
		{
			mailboxSession.UserConfigurationManager.DeleteFolderConfigurations(mailboxSession.GetDefaultFolderId(DefaultFolderType.Calendar), new string[]
			{
				"CategoryList"
			});
		}

		// Token: 0x06005168 RID: 20840 RVA: 0x00152E8C File Offset: 0x0015108C
		internal void CategoryWasUsed(StoreId itemId, string itemClass, string categoryName)
		{
			if (this.IsLoaded && !this.Contains(categoryName))
			{
				return;
			}
			if (this.categoryUsageLogSize >= 500)
			{
				ExTraceGlobals.StorageTracer.TraceDebug((long)this.GetHashCode(), "Category usage log size has reached its maximum of {0} entries. Cannot add this: category=\"{1}\", class=\"{2}\", id=\"{3}\"", new object[]
				{
					500,
					categoryName,
					itemClass,
					itemId
				});
				return;
			}
			StoreObjectId parentFolderId;
			if (itemId != null)
			{
				StoreObjectId storeObjectId = StoreId.GetStoreObjectId(itemId);
				parentFolderId = this.session.GetParentFolderId(storeObjectId);
			}
			else
			{
				parentFolderId = MasterCategoryList.invalidId;
			}
			MasterCategoryList.PerFolderCategoryUsageLog perFolderCategoryUsageLog;
			if (!this.categoryUsageLog.TryGetValue(parentFolderId, out perFolderCategoryUsageLog))
			{
				perFolderCategoryUsageLog = new MasterCategoryList.PerFolderCategoryUsageLog();
				this.categoryUsageLog.Add(parentFolderId, perFolderCategoryUsageLog);
			}
			MasterCategoryList.CategoryUsageRecord key = new MasterCategoryList.CategoryUsageRecord(categoryName, MasterCategoryList.GetModuleForObjectClass(itemClass));
			perFolderCategoryUsageLog.CategoryUsageRecords[key] = ExDateTime.GetNow(ExTimeZone.UtcTimeZone);
			this.categoryUsageLogSize++;
		}

		// Token: 0x06005169 RID: 20841 RVA: 0x00152F6C File Offset: 0x0015116C
		internal Category FindMatch(Category categoryToMatch)
		{
			return this[categoryToMatch.Guid] ?? this[categoryToMatch.Name];
		}

		// Token: 0x0600516A RID: 20842 RVA: 0x00152F8A File Offset: 0x0015118A
		internal void Load()
		{
			ExTraceGlobals.StorageTracer.TraceDebug((long)this.GetHashCode(), "MasterCategoryList::Load - NOT forcing reload.");
			this.Load(false);
		}

		// Token: 0x0600516B RID: 20843 RVA: 0x00152FAC File Offset: 0x001511AC
		internal void Load(bool forceReload)
		{
			if (this.IsLoaded && !forceReload)
			{
				return;
			}
			IReadableUserConfiguration readableUserConfiguration = null;
			this.loadedWithProblems = false;
			try
			{
				try
				{
					readableUserConfiguration = this.session.UserConfigurationManager.GetReadOnlyFolderConfiguration("CategoryList", UserConfigurationTypes.XML, this.session.GetDefaultFolderId(DefaultFolderType.Calendar));
					ExTraceGlobals.StorageTracer.TraceDebug((long)this.GetHashCode(), "MasterCategoryList::Load - newly loaded configurationItem");
					if (this.configurationItemId != null && this.configurationItemId.Equals(readableUserConfiguration.VersionedId))
					{
						ExTraceGlobals.StorageTracer.TraceDebug((long)this.GetHashCode(), "MasterCategoryList::Load - returning without reloading since this.configurationItemId.Equals(configurationItem.VersionedId.");
						return;
					}
				}
				catch (ObjectNotFoundException)
				{
					ExTraceGlobals.StorageTracer.TraceDebug((long)this.GetHashCode(), "MasterCategoryList::Load - ObjectNotFoundException: creating and saving a new MCL");
					try
					{
						readableUserConfiguration = this.CreateMclConfiguration();
					}
					catch (ObjectExistedException ex)
					{
						ExTraceGlobals.StorageTracer.TraceWarning<string, string>((long)this.GetHashCode(), "MclConfiguration already created. Error: {0}. Stack: {1}.", ex.Message, ex.StackTrace);
						readableUserConfiguration = this.session.UserConfigurationManager.GetFolderConfiguration("CategoryList", UserConfigurationTypes.XML, this.session.GetDefaultFolderId(DefaultFolderType.Calendar));
					}
				}
				using (Stream xmlStream = readableUserConfiguration.GetXmlStream())
				{
					this.Load(xmlStream);
					this.originalMcl = this.Clone();
					this.configurationItemId = readableUserConfiguration.VersionedId;
				}
				this.isListModified = false;
			}
			finally
			{
				if (readableUserConfiguration != null)
				{
					readableUserConfiguration.Dispose();
				}
			}
		}

		// Token: 0x0600516C RID: 20844 RVA: 0x00153154 File Offset: 0x00151354
		internal void SetProperties(IEnumerable<PropValue> propValues)
		{
			this.propertyBag.Clear();
			foreach (PropValue propValue in propValues)
			{
				((IDirectPropertyBag)this.propertyBag).SetValue(propValue.Property, propValue.Value);
			}
			this.propertyBag.SetAllPropertiesLoaded();
			this.propertyBag.ClearChangeInfo();
		}

		// Token: 0x0600516D RID: 20845 RVA: 0x001531D0 File Offset: 0x001513D0
		internal object TryGetProperty(PropertyDefinition propertyDefinition)
		{
			return this.propertyBag.TryGetProperty(propertyDefinition);
		}

		// Token: 0x0600516E RID: 20846 RVA: 0x001531E0 File Offset: 0x001513E0
		internal bool IsDirty()
		{
			if (this.isListModified || this.propertyBag.IsDirty)
			{
				return true;
			}
			foreach (Category category in this.categories.Values)
			{
				if (category.CategoryPropertyBag.IsDirty)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600516F RID: 20847 RVA: 0x0015325C File Offset: 0x0015145C
		private static Dictionary<PropertyDefinition, AcrPropertyProfile.ValuesToResolve> GetPropValuesToResolve(MemoryPropertyBag client, MemoryPropertyBag server, MemoryPropertyBag original, AcrProfile profile)
		{
			Dictionary<PropertyDefinition, AcrPropertyProfile.ValuesToResolve> dictionary = new Dictionary<PropertyDefinition, AcrPropertyProfile.ValuesToResolve>();
			HashSet<PropertyDefinition> propertiesNeededForResolution = profile.GetPropertiesNeededForResolution(Util.CompositeEnumerator<PropertyDefinition>(new IEnumerable<PropertyDefinition>[]
			{
				client.Keys,
				server.Keys
			}));
			foreach (PropertyDefinition propertyDefinition in propertiesNeededForResolution)
			{
				dictionary.Add(propertyDefinition, new AcrPropertyProfile.ValuesToResolve(client.TryGetProperty(propertyDefinition), server.TryGetProperty(propertyDefinition), (original != null) ? original.TryGetProperty(propertyDefinition) : null));
			}
			return dictionary;
		}

		// Token: 0x06005170 RID: 20848 RVA: 0x001532F8 File Offset: 0x001514F8
		private static OutlookModule GetModuleForObjectClass(string objectClass)
		{
			if (objectClass == null)
			{
				return OutlookModule.None;
			}
			if (ObjectClass.IsCalendarFolder(objectClass) || ObjectClass.IsCalendarItem(objectClass))
			{
				return OutlookModule.Calendar;
			}
			if (ObjectClass.IsContactsFolder(objectClass) || ObjectClass.IsContact(objectClass) || ObjectClass.IsDistributionList(objectClass))
			{
				return OutlookModule.Contacts;
			}
			if (ObjectClass.IsJournalFolder(objectClass) || ObjectClass.IsJournalItem(objectClass))
			{
				return OutlookModule.Journal;
			}
			if (ObjectClass.IsNotesFolder(objectClass) || ObjectClass.IsNotesItem(objectClass))
			{
				return OutlookModule.Notes;
			}
			if (ObjectClass.IsTaskFolder(objectClass) || ObjectClass.IsTask(objectClass))
			{
				return OutlookModule.Tasks;
			}
			if (ObjectClass.IsMessageFolder(objectClass) || ObjectClass.IsMessage(objectClass, false) || ObjectClass.IsMeetingMessage(objectClass) || ObjectClass.IsTaskRequest(objectClass) || ObjectClass.IsReport(objectClass))
			{
				return OutlookModule.Mail;
			}
			return OutlookModule.None;
		}

		// Token: 0x06005171 RID: 20849 RVA: 0x00153398 File Offset: 0x00151598
		private static MasterCategoryList Resolve(MasterCategoryList client, MasterCategoryList server, MasterCategoryList original)
		{
			MasterCategoryList masterCategoryList = new MasterCategoryList();
			masterCategoryList.SetProperties(MasterCategoryList.ResolveProperties(client.propertyBag, server.propertyBag, original.propertyBag, AcrProfile.MasterCategoryListProfile));
			HashSet<Category> hashSet = new HashSet<Category>(server.Count);
			Util.AddRange<Category, Category>(hashSet, server);
			foreach (Category category in client)
			{
				Category category2 = server.FindMatch(category);
				Category original2 = original.FindMatch(category);
				Category category3 = Category.Resolve(category, category2, original2);
				if (category3 != null && masterCategoryList.FindMatch(category3) == null)
				{
					masterCategoryList.Add(category3);
				}
				if (category2 != null)
				{
					hashSet.Remove(category2);
				}
			}
			foreach (Category category4 in hashSet)
			{
				Category original3 = original.FindMatch(category4);
				Category category5 = Category.Resolve(null, category4, original3);
				if (category5 != null && masterCategoryList.FindMatch(category5) == null)
				{
					masterCategoryList.Add(category5);
				}
			}
			return masterCategoryList;
		}

		// Token: 0x06005172 RID: 20850 RVA: 0x001534BC File Offset: 0x001516BC
		private MasterCategoryList Clone()
		{
			return new MasterCategoryList(this);
		}

		// Token: 0x06005173 RID: 20851 RVA: 0x001534C4 File Offset: 0x001516C4
		private IReadableUserConfiguration CreateMclConfiguration()
		{
			IReadableUserConfiguration result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				UserConfiguration userConfiguration = this.session.UserConfigurationManager.CreateFolderConfiguration("CategoryList", UserConfigurationTypes.XML, this.session.GetDefaultFolderId(DefaultFolderType.Calendar));
				disposeGuard.Add<UserConfiguration>(userConfiguration);
				userConfiguration.Save();
				disposeGuard.Success();
				result = userConfiguration;
			}
			return result;
		}

		// Token: 0x06005174 RID: 20852 RVA: 0x00153538 File Offset: 0x00151738
		private void Load(Stream xmlStream)
		{
			this.Clear();
			if (xmlStream.Length == 0L)
			{
				return;
			}
			using (Stream stream = new BoundedStream(xmlStream, false, 0L, 524288L))
			{
				using (XmlReader xmlReader = XmlReader.Create(stream))
				{
					MasterCategoryListSerializer masterCategoryListSerializer = new MasterCategoryListSerializer(xmlReader);
					try
					{
						masterCategoryListSerializer.Deserialize(this);
						this.loadedWithProblems = masterCategoryListSerializer.HasFaults;
					}
					catch (CorruptDataException)
					{
						this.loadedWithProblems = true;
						if (xmlStream.Length > 524288L)
						{
							ExTraceGlobals.StorageTracer.TraceWarning<long, long>((long)this.GetHashCode(), "The size of MCL XML ({0}) has exceeded the limit of {1}. The loaded data is truncated.", xmlStream.Length, 524288L);
						}
					}
				}
			}
		}

		// Token: 0x06005175 RID: 20853 RVA: 0x00153604 File Offset: 0x00151804
		private void MovePersistentContent(MasterCategoryList sourceMcl)
		{
			if (this == sourceMcl)
			{
				List<Category> list = new List<Category>(this.Count);
				foreach (Category category in this)
				{
					list.Add(category.Clone());
				}
				this.Clear();
				Util.AddRange<Category, Category>(this, list);
				return;
			}
			this.Clear();
			foreach (Category category2 in sourceMcl.categories.Values)
			{
				category2.Detach();
				this.Add(category2);
			}
			sourceMcl.categories.Clear();
			this.propertyBag = sourceMcl.propertyBag;
			sourceMcl.propertyBag = new MemoryPropertyBag();
		}

		// Token: 0x06005176 RID: 20854 RVA: 0x001536E8 File Offset: 0x001518E8
		private void FlushCategoryUsageLog()
		{
			foreach (KeyValuePair<StoreObjectId, MasterCategoryList.PerFolderCategoryUsageLog> keyValuePair in this.categoryUsageLog)
			{
				if (keyValuePair.Key != MasterCategoryList.invalidId && keyValuePair.Value.OutlookModule == null)
				{
					try
					{
						using (Folder folder = Folder.Bind(this.session, keyValuePair.Key))
						{
							keyValuePair.Value.OutlookModule = new OutlookModule?(MasterCategoryList.GetModuleForObjectClass(folder.ClassName));
						}
					}
					catch (ObjectNotFoundException)
					{
						keyValuePair.Value.OutlookModule = new OutlookModule?(OutlookModule.None);
					}
				}
			}
			foreach (KeyValuePair<StoreObjectId, MasterCategoryList.PerFolderCategoryUsageLog> keyValuePair2 in this.categoryUsageLog)
			{
				foreach (KeyValuePair<MasterCategoryList.CategoryUsageRecord, ExDateTime> keyValuePair3 in keyValuePair2.Value.CategoryUsageRecords)
				{
					Category category = this[keyValuePair3.Key.CategoryName];
					if (category != null)
					{
						category.UpdateLastTimeUsed(keyValuePair3.Value, (keyValuePair2.Value.OutlookModule != null && keyValuePair2.Value.OutlookModule != OutlookModule.None) ? keyValuePair2.Value.OutlookModule : new OutlookModule?(keyValuePair3.Key.ModuleForItem));
					}
				}
				keyValuePair2.Value.CategoryUsageRecords.Clear();
			}
		}

		// Token: 0x06005177 RID: 20855 RVA: 0x001538E4 File Offset: 0x00151AE4
		private MasterCategoryList MergeCopies(SaveMode saveMode, UserConfiguration mclConfigurationItem, Stream mclXmlStream)
		{
			MasterCategoryList masterCategoryList = new MasterCategoryList();
			switch (saveMode)
			{
			case SaveMode.ResolveConflicts:
				try
				{
					using (Stream stream = new BoundedStream(mclXmlStream, false, 0L, 524288L))
					{
						using (XmlReader xmlReader = XmlReader.Create(stream))
						{
							new MasterCategoryListSerializer(xmlReader).Deserialize(masterCategoryList);
						}
					}
				}
				catch (CorruptDataException ex)
				{
					Exception ex2 = new SaveConflictException(ServerStrings.ExMclCannotBeResolved, ex);
					ExTraceGlobals.StorageTracer.TraceDebug<SaveMode, string>((long)this.GetHashCode(), "Failed to load the Server copy of MCL for conflict resolution (SaveMode={0}): {1}", saveMode, ex.Message);
					throw ex2;
				}
				return MasterCategoryList.Resolve(this, masterCategoryList, this.originalMcl);
			case SaveMode.FailOnAnyConflict:
				throw new SaveConflictException(ServerStrings.ExSaveFailedBecauseOfConflicts(mclConfigurationItem.Id), null);
			case SaveMode.NoConflictResolution:
				return this;
			default:
				throw new ArgumentOutOfRangeException("saveMode");
			}
		}

		// Token: 0x06005178 RID: 20856 RVA: 0x001539D4 File Offset: 0x00151BD4
		private void SaveToStream(SaveMode saveMode, Stream destination, Stream serverCopy)
		{
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
			xmlWriterSettings.CloseOutput = false;
			if (saveMode != SaveMode.NoConflictResolution && serverCopy.Length != 0L)
			{
				try
				{
					using (Stream stream = new BoundedStream(serverCopy, false, 0L, 524288L))
					{
						using (StreamReader streamReader = new StreamReader(stream, true))
						{
							using (XmlReader xmlReader = XmlReader.Create(streamReader))
							{
								xmlWriterSettings.Encoding = (streamReader.CurrentEncoding ?? Encoding.UTF8);
								using (XmlWriter xmlTextWriter = Util.GetXmlTextWriter(destination, xmlWriterSettings))
								{
									new MasterCategoryListSerializer(xmlReader).SerializeUsingSource(this, xmlTextWriter);
									return;
								}
							}
						}
					}
				}
				catch (CorruptDataException inner)
				{
					if (saveMode == SaveMode.FailOnAnyConflict)
					{
						throw new SaveConflictException(ServerStrings.ExMclCannotBeResolved, inner);
					}
				}
			}
			destination.Position = 0L;
			destination.SetLength(0L);
			using (XmlWriter xmlWriter = XmlWriter.Create(destination, xmlWriterSettings))
			{
				MasterCategoryListSerializer.Serialize(this, xmlWriter);
			}
		}

		// Token: 0x04002C50 RID: 11344
		internal const string MclConfigurationName = "CategoryList";

		// Token: 0x04002C51 RID: 11345
		private const int MaxCategoryUsageLogSize = 500;

		// Token: 0x04002C52 RID: 11346
		private const long MaxMclXmlSize = 524288L;

		// Token: 0x04002C53 RID: 11347
		private static readonly StoreObjectId invalidId = StoreObjectId.FromProviderSpecificId(Array<byte>.Empty, StoreObjectType.Unknown);

		// Token: 0x04002C54 RID: 11348
		private readonly Dictionary<string, Category> categories = new Dictionary<string, Category>(Category.NameComparer);

		// Token: 0x04002C55 RID: 11349
		private readonly Dictionary<Guid, Category> categoriesByGuid = new Dictionary<Guid, Category>();

		// Token: 0x04002C56 RID: 11350
		private readonly Dictionary<StoreObjectId, MasterCategoryList.PerFolderCategoryUsageLog> categoryUsageLog = new Dictionary<StoreObjectId, MasterCategoryList.PerFolderCategoryUsageLog>();

		// Token: 0x04002C57 RID: 11351
		private readonly MailboxSession session;

		// Token: 0x04002C58 RID: 11352
		private VersionedId configurationItemId;

		// Token: 0x04002C59 RID: 11353
		private bool loadedWithProblems;

		// Token: 0x04002C5A RID: 11354
		private MasterCategoryList originalMcl;

		// Token: 0x04002C5B RID: 11355
		private MemoryPropertyBag propertyBag;

		// Token: 0x04002C5C RID: 11356
		private int categoryUsageLogSize;

		// Token: 0x04002C5D RID: 11357
		private bool isListModified;

		// Token: 0x02000870 RID: 2160
		private struct CategoryUsageRecord : IEquatable<MasterCategoryList.CategoryUsageRecord>
		{
			// Token: 0x0600517A RID: 20858 RVA: 0x00153B1E File Offset: 0x00151D1E
			public CategoryUsageRecord(string categoryName, OutlookModule moduleForItem)
			{
				this.CategoryName = categoryName;
				this.ModuleForItem = moduleForItem;
			}

			// Token: 0x0600517B RID: 20859 RVA: 0x00153B2E File Offset: 0x00151D2E
			public override bool Equals(object obj)
			{
				return obj != null && this.Equals((MasterCategoryList.CategoryUsageRecord)obj);
			}

			// Token: 0x0600517C RID: 20860 RVA: 0x00153B41 File Offset: 0x00151D41
			public override int GetHashCode()
			{
				return this.CategoryName.GetHashCode() ^ (int)this.ModuleForItem;
			}

			// Token: 0x0600517D RID: 20861 RVA: 0x00153B55 File Offset: 0x00151D55
			public bool Equals(MasterCategoryList.CategoryUsageRecord other)
			{
				return this.ModuleForItem == other.ModuleForItem && this.CategoryName.Equals(other.CategoryName);
			}

			// Token: 0x04002C5E RID: 11358
			public readonly string CategoryName;

			// Token: 0x04002C5F RID: 11359
			public readonly OutlookModule ModuleForItem;
		}

		// Token: 0x02000871 RID: 2161
		private sealed class UsageBasedCategoryComparer : IComparer<Category>
		{
			// Token: 0x0600517E RID: 20862 RVA: 0x00153B7A File Offset: 0x00151D7A
			public UsageBasedCategoryComparer(OutlookModule module)
			{
				this.module = module;
			}

			// Token: 0x0600517F RID: 20863 RVA: 0x00153B8C File Offset: 0x00151D8C
			public int Compare(Category x, Category y)
			{
				int num = -x.LastTimeUsed[this.module].CompareTo(y.LastTimeUsed[this.module]);
				if (num == 0)
				{
					num = Category.NameComparer.Compare(x.Name, y.Name);
				}
				return num;
			}

			// Token: 0x04002C60 RID: 11360
			private readonly OutlookModule module;
		}

		// Token: 0x02000872 RID: 2162
		private sealed class PerFolderCategoryUsageLog
		{
			// Token: 0x170016C7 RID: 5831
			// (get) Token: 0x06005181 RID: 20865 RVA: 0x00153BF3 File Offset: 0x00151DF3
			public Dictionary<MasterCategoryList.CategoryUsageRecord, ExDateTime> CategoryUsageRecords
			{
				get
				{
					return this.categoryUsageRecords;
				}
			}

			// Token: 0x170016C8 RID: 5832
			// (get) Token: 0x06005182 RID: 20866 RVA: 0x00153BFB File Offset: 0x00151DFB
			// (set) Token: 0x06005183 RID: 20867 RVA: 0x00153C03 File Offset: 0x00151E03
			public OutlookModule? OutlookModule
			{
				get
				{
					return this.outlookModule;
				}
				set
				{
					this.outlookModule = value;
				}
			}

			// Token: 0x04002C61 RID: 11361
			private Dictionary<MasterCategoryList.CategoryUsageRecord, ExDateTime> categoryUsageRecords = new Dictionary<MasterCategoryList.CategoryUsageRecord, ExDateTime>();

			// Token: 0x04002C62 RID: 11362
			private OutlookModule? outlookModule;
		}
	}
}

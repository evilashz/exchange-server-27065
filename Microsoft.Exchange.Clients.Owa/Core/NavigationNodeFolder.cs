using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Clients.Owa.Premium;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200016C RID: 364
	internal sealed class NavigationNodeFolder : NavigationNode, ICloneable
	{
		// Token: 0x06000CBA RID: 3258 RVA: 0x00056E0C File Offset: 0x0005500C
		private static List<KeyValuePair<ExtendedFolderFlags, NavigationNodeFlags>> CreateMappingFromFolderFlagToNodeFlag()
		{
			return new List<KeyValuePair<ExtendedFolderFlags, NavigationNodeFlags>>
			{
				new KeyValuePair<ExtendedFolderFlags, NavigationNodeFlags>(ExtendedFolderFlags.IsSharepointFolder, NavigationNodeFlags.SharepointFolder),
				new KeyValuePair<ExtendedFolderFlags, NavigationNodeFlags>(ExtendedFolderFlags.SharedIn, NavigationNodeFlags.SharedIn),
				new KeyValuePair<ExtendedFolderFlags, NavigationNodeFlags>(ExtendedFolderFlags.SharedOut, NavigationNodeFlags.SharedOut),
				new KeyValuePair<ExtendedFolderFlags, NavigationNodeFlags>(ExtendedFolderFlags.SharedViaExchange, NavigationNodeFlags.SharedOut),
				new KeyValuePair<ExtendedFolderFlags, NavigationNodeFlags>(ExtendedFolderFlags.PersonalShare, NavigationNodeFlags.PersonFolder),
				new KeyValuePair<ExtendedFolderFlags, NavigationNodeFlags>(ExtendedFolderFlags.ICalFolder, NavigationNodeFlags.IcalFolder)
			};
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x00056E98 File Offset: 0x00055098
		internal NavigationNodeFolder(MailboxSession session, bool isMyMailbox, object[] folderPropertyValues, Dictionary<PropertyDefinition, int> folderPropertyMap, string subject, Guid groupClassId, NavigationNodeGroupSection navigationNodeGroupSection, string groupName) : base(NavigationNodeType.NormalFolder, subject, navigationNodeGroupSection)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			Utilities.CheckAndThrowForRequiredProperty(folderPropertyMap, new PropertyDefinition[]
			{
				FolderSchema.Id,
				StoreObjectSchema.RecordKey,
				FolderSchema.ExtendedFolderFlags
			});
			StoreObjectId objectId = ((VersionedId)folderPropertyValues[folderPropertyMap[FolderSchema.Id]]).ObjectId;
			ExtendedFolderFlags flags = (ExtendedFolderFlags)0;
			object obj = folderPropertyValues[folderPropertyMap[FolderSchema.ExtendedFolderFlags]];
			if (!(obj is PropertyError))
			{
				flags = (ExtendedFolderFlags)obj;
			}
			this.Initialize(session, isMyMailbox, objectId, folderPropertyValues[folderPropertyMap[StoreObjectSchema.RecordKey]], flags, groupClassId, groupName);
			base.ClearDirty();
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x00056F54 File Offset: 0x00055154
		internal NavigationNodeFolder(Folder folder, bool isMyFolder, string subject, NavigationNodeType navigationNodeType, Guid groupClassId, NavigationNodeGroupSection navigationNodeGroupSection, string groupName) : base(navigationNodeType, subject, navigationNodeGroupSection)
		{
			if (navigationNodeType == NavigationNodeType.Header || navigationNodeType == NavigationNodeType.Undefined)
			{
				throw new ArgumentException("The type should not be header for folders.");
			}
			MailboxSession mailboxSession = folder.Session as MailboxSession;
			if (mailboxSession == null)
			{
				throw new ArgumentException("The folder doesn't belong to a mailbox session.");
			}
			this.Initialize(mailboxSession, isMyFolder, folder.Id.ObjectId, folder.TryGetProperty(StoreObjectSchema.RecordKey), folder.GetValueOrDefault<ExtendedFolderFlags>(FolderSchema.ExtendedFolderFlags), groupClassId, groupName);
			if (navigationNodeType == NavigationNodeType.SmartFolder)
			{
				object obj = folder.TryGetProperty(FolderSchema.OutlookSearchFolderClsId);
				if (obj is Guid)
				{
					this.AssociatedSearchFolderId = (Guid)obj;
				}
			}
			base.ClearDirty();
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x00057048 File Offset: 0x00055248
		internal NavigationNodeFolder(UserContext userContext, MailboxSession session, bool includeArchive, object[] values, Dictionary<PropertyDefinition, int> propertyMap) : base(NavigationNodeFolder.navigationNodeFolderProperties, values, propertyMap)
		{
			string originalLegacyDN = this.GetLegacyDNFromStoreEntryId();
			if (base.IsFlagSet(NavigationNodeFlags.IsDefaultStore) || StringComparer.OrdinalIgnoreCase.Equals(session.MailboxOwner.LegacyDn, originalLegacyDN))
			{
				this.calculatedLegacyDN = originalLegacyDN;
				if (base.IsFlagSet(NavigationNodeFlags.TodoFolder))
				{
					this.calculatedFolderId = Utilities.TryGetDefaultFolderId(session, DefaultFolderType.ToDoSearch);
					return;
				}
				if (base.IsFlagSet(NavigationNodeFlags.RootFolder))
				{
					this.calculatedFolderId = Utilities.TryGetDefaultFolderId(session, DefaultFolderType.Root);
					return;
				}
			}
			else if (base.NavigationNodeType == NavigationNodeType.NormalFolder || base.NavigationNodeType == NavigationNodeType.SmartFolder)
			{
				this.isMailboxLegacyDNValid = false;
				if (userContext != null && userContext.ArchiveAccessed && includeArchive)
				{
					userContext.TryLoopArchiveMailboxes(delegate(MailboxSession archiveSession)
					{
						if (StringComparer.OrdinalIgnoreCase.Equals(archiveSession.MailboxOwnerLegacyDN, originalLegacyDN))
						{
							this.isMailboxLegacyDNValid = true;
							this.calculatedLegacyDN = originalLegacyDN;
						}
					});
				}
			}
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x0005713A File Offset: 0x0005533A
		private NavigationNodeFolder(string subject, NavigationNodeGroupSection navigationNodeGroupSection) : base(NavigationNodeType.SharedFolder, subject, navigationNodeGroupSection)
		{
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x00057158 File Offset: 0x00055358
		internal static NavigationNodeFolder CreateGSNode(ExchangePrincipal exchangePrincipal, Guid groupClassId, string groupName, string subject, NavigationNodeGroupSection navigationNodeGroupSection)
		{
			NavigationNodeFolder navigationNodeFolder = new NavigationNodeFolder(subject, navigationNodeGroupSection);
			navigationNodeFolder.GSCalendarSharerAddressBookEntryId = AddressBookEntryId.MakeAddressBookEntryID(exchangePrincipal);
			UserContext userContext = OwaContext.Current.UserContext;
			navigationNodeFolder.GSCalendarShareeStoreEntryId = StoreEntryId.ToProviderStoreEntryId(userContext.ExchangePrincipal);
			try
			{
				using (OwaStoreObjectIdSessionHandle owaStoreObjectIdSessionHandle = new OwaStoreObjectIdSessionHandle(exchangePrincipal, userContext))
				{
					try
					{
						using (Folder folder = Folder.Bind(owaStoreObjectIdSessionHandle.Session as MailboxSession, DefaultFolderType.Calendar, new PropertyDefinition[]
						{
							StoreObjectSchema.EffectiveRights
						}))
						{
							if (CalendarUtilities.UserHasRightToLoad(folder))
							{
								navigationNodeFolder.NavigationNodeEntryId = folder.StoreObjectId.ProviderLevelItemId;
								navigationNodeFolder.NavigationNodeStoreEntryId = StoreEntryId.ToProviderStoreEntryId(exchangePrincipal);
							}
						}
					}
					catch (ObjectNotFoundException)
					{
					}
				}
			}
			catch (OwaSharedFromOlderVersionException)
			{
			}
			navigationNodeFolder.ClearDirty();
			return navigationNodeFolder;
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x00057248 File Offset: 0x00055448
		private NavigationNodeFolder(MemoryPropertyBag propertyBag) : base(propertyBag)
		{
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x00057264 File Offset: 0x00055464
		private void Initialize(MailboxSession session, bool isMyMailbox, StoreObjectId folderId, object recordKey, ExtendedFolderFlags flags, Guid groupClassId, string groupName)
		{
			StoreObjectId storeObjectId = Utilities.TryGetDefaultFolderId(session, DefaultFolderType.Root);
			if (storeObjectId != null && storeObjectId.Equals(folderId))
			{
				throw new NotSupportedException("We don't support adding root folder to favorites.");
			}
			if (!session.IsRemote)
			{
				this.NavigationNodeStoreEntryId = StoreEntryId.ToProviderStoreEntryId(session.MailboxOwner);
			}
			this.NavigationNodeEntryId = folderId.ProviderLevelItemId;
			if (recordKey is byte[])
			{
				this.NavigationNodeRecordKey = (byte[])recordKey;
			}
			StoreObjectId storeObjectId2 = Utilities.TryGetDefaultFolderId(session, DefaultFolderType.ToDoSearch);
			if (storeObjectId2 != null && storeObjectId2.Equals(folderId))
			{
				base.NavigationNodeFlags |= NavigationNodeFlags.TodoFolder;
			}
			foreach (KeyValuePair<ExtendedFolderFlags, NavigationNodeFlags> keyValuePair in NavigationNodeFolder.mappingFromFolderFlagToNodeFlag)
			{
				if (Utilities.IsFlagSet((int)flags, (int)keyValuePair.Key))
				{
					base.NavigationNodeFlags |= keyValuePair.Value;
				}
			}
			if (isMyMailbox)
			{
				base.NavigationNodeFlags |= NavigationNodeFlags.IsDefaultStore;
			}
			if (!base.IsFavorites)
			{
				this.NavigationNodeParentGroupClassId = groupClassId;
				this.NavigationNodeGroupName = groupName;
			}
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x00057384 File Offset: 0x00055584
		private StoreObjectType GetExpectedStoreObjectType()
		{
			switch (base.NavigationNodeGroupSection)
			{
			case NavigationNodeGroupSection.First:
				if (this.IsFilteredView)
				{
					return StoreObjectType.SearchFolder;
				}
				if (!this.AssociatedSearchFolderId.Equals(Guid.Empty))
				{
					return StoreObjectType.OutlookSearchFolder;
				}
				break;
			case NavigationNodeGroupSection.Calendar:
				return StoreObjectType.CalendarFolder;
			case NavigationNodeGroupSection.Contacts:
				return StoreObjectType.ContactsFolder;
			case NavigationNodeGroupSection.Tasks:
				if (base.IsFlagSet(NavigationNodeFlags.TodoFolder))
				{
					return StoreObjectType.SearchFolder;
				}
				return StoreObjectType.TasksFolder;
			case NavigationNodeGroupSection.Notes:
				return StoreObjectType.NotesFolder;
			case NavigationNodeGroupSection.Journal:
				return StoreObjectType.JournalFolder;
			}
			return StoreObjectType.Folder;
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x000573FC File Offset: 0x000555FC
		internal void FixLegacyDNRelatedFlag(MailboxSession mailboxSession)
		{
			bool flag = string.Equals(mailboxSession.MailboxOwner.LegacyDn, this.GetLegacyDNFromStoreEntryId(), StringComparison.OrdinalIgnoreCase);
			if (base.IsFlagSet(NavigationNodeFlags.IsDefaultStore))
			{
				if (!flag)
				{
					this.NavigationNodeStoreEntryId = StoreEntryId.ToProviderStoreEntryId(mailboxSession.MailboxOwner);
					return;
				}
			}
			else if (flag)
			{
				base.NavigationNodeFlags |= NavigationNodeFlags.IsDefaultStore;
			}
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x00057458 File Offset: 0x00055658
		private string GetLegacyDNFromStoreEntryId()
		{
			string text = null;
			byte[] valueOrDefault = this.propertyBag.GetValueOrDefault<byte[]>(NavigationNodeSchema.StoreEntryId);
			if (valueOrDefault != null)
			{
				text = StoreEntryId.TryParseStoreEntryIdMailboxDN(valueOrDefault);
				if (text == null)
				{
					text = StoreEntryId.TryParseMailboxLegacyDN(valueOrDefault);
				}
				if (string.IsNullOrEmpty(text) || !Utilities.IsValidLegacyDN(text))
				{
					text = string.Empty;
				}
			}
			return text ?? string.Empty;
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000CC5 RID: 3269 RVA: 0x000574AC File Offset: 0x000556AC
		internal string MailboxLegacyDN
		{
			get
			{
				if (this.calculatedLegacyDN != null)
				{
					return this.calculatedLegacyDN;
				}
				if (this.legacyDN == null)
				{
					if (this.IsGSCalendar)
					{
						Eidt eidt;
						string text;
						if (AddressBookEntryId.IsAddressBookEntryId(this.GSCalendarSharerAddressBookEntryId, out eidt, out text))
						{
							this.legacyDN = text;
						}
					}
					else
					{
						this.legacyDN = this.GetLegacyDNFromStoreEntryId();
					}
				}
				return this.legacyDN;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x00057504 File Offset: 0x00055704
		// (set) Token: 0x06000CC7 RID: 3271 RVA: 0x00057516 File Offset: 0x00055716
		private byte[] GSCalendarSharerAddressBookEntryId
		{
			get
			{
				return this.propertyBag.GetValueOrDefault<byte[]>(NavigationNodeSchema.AddressBookEntryId);
			}
			set
			{
				this.propertyBag.SetOrDeleteProperty(NavigationNodeSchema.AddressBookEntryId, value);
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x00057529 File Offset: 0x00055729
		// (set) Token: 0x06000CC9 RID: 3273 RVA: 0x0005753B File Offset: 0x0005573B
		private byte[] GSCalendarShareeStoreEntryId
		{
			get
			{
				return this.propertyBag.GetValueOrDefault<byte[]>(NavigationNodeSchema.AddressBookStoreEntryId);
			}
			set
			{
				this.propertyBag.SetOrDeleteProperty(NavigationNodeSchema.AddressBookStoreEntryId, value);
			}
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x0005754E File Offset: 0x0005574E
		internal bool IsFolderInSpecificMailboxSession(MailboxSession mailboxSession)
		{
			return string.Equals(mailboxSession.MailboxOwnerLegacyDN, this.MailboxLegacyDN, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000CCB RID: 3275 RVA: 0x00057564 File Offset: 0x00055764
		internal StoreObjectId FolderId
		{
			get
			{
				if (this.calculatedFolderId != null)
				{
					return this.calculatedFolderId;
				}
				if (!this.IsGSCalendar && this.folderId == null)
				{
					byte[] valueOrDefault = this.propertyBag.GetValueOrDefault<byte[]>(NavigationNodeSchema.NodeEntryId);
					if (valueOrDefault != null)
					{
						try
						{
							this.folderId = StoreObjectId.FromProviderSpecificId(valueOrDefault, this.GetExpectedStoreObjectType());
						}
						catch (CorruptDataException ex)
						{
							string message = string.Format(CultureInfo.InvariantCulture, "NavigationNodeFolder.FolderId property accessor exception {0} for folder entry id {1}", new object[]
							{
								ex,
								Convert.ToBase64String(valueOrDefault)
							});
							ExTraceGlobals.CoreCallTracer.TraceDebug(0L, message);
						}
					}
				}
				return this.folderId;
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000CCC RID: 3276 RVA: 0x00057604 File Offset: 0x00055804
		// (set) Token: 0x06000CCD RID: 3277 RVA: 0x00057616 File Offset: 0x00055816
		private byte[] NavigationNodeEntryId
		{
			get
			{
				return this.propertyBag.GetValueOrDefault<byte[]>(NavigationNodeSchema.NodeEntryId);
			}
			set
			{
				this.propertyBag.SetOrDeleteProperty(NavigationNodeSchema.NodeEntryId, value);
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000CCE RID: 3278 RVA: 0x00057629 File Offset: 0x00055829
		// (set) Token: 0x06000CCF RID: 3279 RVA: 0x0005763B File Offset: 0x0005583B
		private byte[] NavigationNodeRecordKey
		{
			get
			{
				return this.propertyBag.GetValueOrDefault<byte[]>(NavigationNodeSchema.NodeRecordKey);
			}
			set
			{
				this.propertyBag.SetOrDeleteProperty(NavigationNodeSchema.NodeRecordKey, value);
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000CD0 RID: 3280 RVA: 0x0005764E File Offset: 0x0005584E
		// (set) Token: 0x06000CD1 RID: 3281 RVA: 0x00057660 File Offset: 0x00055860
		private byte[] NavigationNodeStoreEntryId
		{
			get
			{
				return this.propertyBag.GetValueOrDefault<byte[]>(NavigationNodeSchema.StoreEntryId);
			}
			set
			{
				this.propertyBag.SetOrDeleteProperty(NavigationNodeSchema.StoreEntryId, value);
			}
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x00057674 File Offset: 0x00055874
		public void SetFilterParameter(StoreObjectId folderId, int flags, string[] categories, string from, string to)
		{
			if (folderId == null)
			{
				throw new ArgumentNullException("folderId");
			}
			if (base.NavigationNodeType != NavigationNodeType.SmartFolder)
			{
				throw new InvalidOperationException("Only smart folder can be set filter parameters");
			}
			this.propertyBag.SetOrDeleteProperty(ViewStateProperties.FilterSourceFolder, folderId.ToBase64String());
			this.propertyBag.SetOrDeleteProperty(ViewStateProperties.FilteredViewFlags, flags);
			this.propertyBag.SetOrDeleteProperty(ViewStateProperties.FilteredViewFrom, from);
			this.propertyBag.SetOrDeleteProperty(ViewStateProperties.FilteredViewTo, to);
			this.propertyBag.SetOrDeleteProperty(ItemSchema.Categories, categories);
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x00057704 File Offset: 0x00055904
		public void DeleteFilterParameter()
		{
			this.propertyBag.Delete(ViewStateProperties.FilterSourceFolder);
			this.propertyBag.Delete(ViewStateProperties.FilteredViewFlags);
			this.propertyBag.Delete(ViewStateProperties.FilteredViewFrom);
			this.propertyBag.Delete(ViewStateProperties.FilteredViewTo);
			this.propertyBag.Delete(ItemSchema.Categories);
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x00057761 File Offset: 0x00055961
		public bool IsFilteredView
		{
			get
			{
				return base.NavigationNodeType == NavigationNodeType.SmartFolder && this.FilterSourceFolder != null;
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x0005777C File Offset: 0x0005597C
		public StoreObjectId FilterSourceFolder
		{
			get
			{
				if (base.NavigationNodeType != NavigationNodeType.SmartFolder)
				{
					return null;
				}
				string valueOrDefault = this.propertyBag.GetValueOrDefault<string>(ViewStateProperties.FilterSourceFolder);
				if (string.IsNullOrEmpty(valueOrDefault))
				{
					return null;
				}
				StoreObjectId result;
				try
				{
					result = Utilities.CreateStoreObjectId(valueOrDefault);
				}
				catch (OwaInvalidIdFormatException)
				{
					result = null;
				}
				return result;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x000577D0 File Offset: 0x000559D0
		public int FilterFlag
		{
			get
			{
				if (!this.IsFilteredView)
				{
					return 0;
				}
				return this.propertyBag.GetValueOrDefault<int>(ViewStateProperties.FilteredViewFlags);
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x000577EC File Offset: 0x000559EC
		public string[] FitlerCategories
		{
			get
			{
				if (!this.IsFilteredView)
				{
					return null;
				}
				return this.propertyBag.GetValueOrDefault<string[]>(ItemSchema.Categories);
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x00057808 File Offset: 0x00055A08
		public string FilterFrom
		{
			get
			{
				if (!this.IsFilteredView)
				{
					return null;
				}
				return this.propertyBag.GetValueOrDefault<string>(ViewStateProperties.FilteredViewFrom);
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x00057824 File Offset: 0x00055A24
		public string FilterTo
		{
			get
			{
				if (!this.IsFilteredView)
				{
					return null;
				}
				return this.propertyBag.GetValueOrDefault<string>(ViewStateProperties.FilteredViewTo);
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000CDA RID: 3290 RVA: 0x00057840 File Offset: 0x00055A40
		// (set) Token: 0x06000CDB RID: 3291 RVA: 0x00057858 File Offset: 0x00055A58
		public string NavigationNodeGroupName
		{
			get
			{
				this.ThrowIfInFavorites();
				return this.propertyBag.GetValueOrDefault<string>(NavigationNodeSchema.GroupName);
			}
			set
			{
				this.ThrowIfInFavorites();
				this.propertyBag.SetOrDeleteProperty(NavigationNodeSchema.GroupName, value);
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000CDC RID: 3292 RVA: 0x00057871 File Offset: 0x00055A71
		// (set) Token: 0x06000CDD RID: 3293 RVA: 0x00057884 File Offset: 0x00055A84
		internal Guid NavigationNodeParentGroupClassId
		{
			get
			{
				this.ThrowIfInFavorites();
				return base.GuidGetter(NavigationNodeSchema.ParentGroupClassId);
			}
			set
			{
				this.ThrowIfInFavorites();
				base.GuidSetter(NavigationNodeSchema.ParentGroupClassId, value);
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000CDE RID: 3294 RVA: 0x00057898 File Offset: 0x00055A98
		// (set) Token: 0x06000CDF RID: 3295 RVA: 0x000578BF File Offset: 0x00055ABF
		public int NavigationNodeCalendarColor
		{
			get
			{
				if (base.NavigationNodeGroupSection != NavigationNodeGroupSection.Calendar)
				{
					throw new NotSupportedException("Only calendar item supports it.");
				}
				return this.propertyBag.GetValueOrDefault<int>(NavigationNodeSchema.CalendarColor, -1);
			}
			set
			{
				if (base.NavigationNodeGroupSection != NavigationNodeGroupSection.Calendar)
				{
					throw new NotSupportedException("Only calendar item supports it.");
				}
				this.propertyBag.SetOrDeleteProperty(NavigationNodeSchema.CalendarColor, value);
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x000578EB File Offset: 0x00055AEB
		// (set) Token: 0x06000CE1 RID: 3297 RVA: 0x000578FE File Offset: 0x00055AFE
		internal Guid AssociatedSearchFolderId
		{
			get
			{
				this.ThrowIfGSCalendar();
				return base.GuidGetter(FolderSchema.AssociatedSearchFolderId);
			}
			private set
			{
				this.ThrowIfGSCalendar();
				base.GuidSetter(FolderSchema.AssociatedSearchFolderId, value);
			}
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x00057914 File Offset: 0x00055B14
		public object Clone()
		{
			return new NavigationNodeFolder(this.propertyBag)
			{
				isMailboxLegacyDNValid = this.isMailboxLegacyDNValid,
				calculatedFolderId = this.calculatedFolderId,
				calculatedLegacyDN = this.calculatedLegacyDN
			};
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x00057954 File Offset: 0x00055B54
		public override int GetHashCode()
		{
			if (this.hashCode == null)
			{
				this.hashCode = new int?(this.MailboxLegacyDN.ToLowerInvariant().GetHashCode());
				if (base.IsFavorites)
				{
					if (this.AssociatedSearchFolderId.Equals(Guid.Empty))
					{
						if (this.FolderId != null)
						{
							this.hashCode ^= this.FolderId.GetHashCode();
						}
					}
					else
					{
						this.hashCode ^= this.AssociatedSearchFolderId.GetHashCode();
					}
				}
				else
				{
					if (this.FolderId != null)
					{
						this.hashCode ^= this.FolderId.GetHashCode();
					}
					this.hashCode ^= base.NavigationNodeType.GetHashCode();
				}
				this.hashCode ^= base.NavigationNodeGroupSection.GetHashCode();
			}
			return this.hashCode.Value;
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x00057B20 File Offset: 0x00055D20
		public override bool Equals(object obj)
		{
			if (!(obj is NavigationNodeFolder))
			{
				return false;
			}
			NavigationNodeFolder navigationNodeFolder = obj as NavigationNodeFolder;
			if (!this.IsValid || !navigationNodeFolder.IsValid)
			{
				return false;
			}
			if (base.NavigationNodeGroupSection != navigationNodeFolder.NavigationNodeGroupSection)
			{
				return false;
			}
			if (!string.Equals(this.MailboxLegacyDN, navigationNodeFolder.MailboxLegacyDN, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			if (base.IsFavorites)
			{
				Guid associatedSearchFolderId = this.AssociatedSearchFolderId;
				Guid associatedSearchFolderId2 = navigationNodeFolder.AssociatedSearchFolderId;
				if (associatedSearchFolderId.Equals(Guid.Empty) && associatedSearchFolderId2.Equals(Guid.Empty))
				{
					return this.FolderId.Equals(navigationNodeFolder.FolderId);
				}
				return !associatedSearchFolderId.Equals(Guid.Empty) && !associatedSearchFolderId2.Equals(Guid.Empty) && associatedSearchFolderId.Equals(associatedSearchFolderId2);
			}
			else
			{
				if (this.IsGSCalendar && navigationNodeFolder.IsGSCalendar)
				{
					return this.NavigationNodeParentGroupClassId.Equals(navigationNodeFolder.NavigationNodeParentGroupClassId);
				}
				return this.FolderId != null && navigationNodeFolder.FolderId != null && this.FolderId.Equals(navigationNodeFolder.FolderId) && base.NavigationNodeType == navigationNodeFolder.NavigationNodeType;
			}
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x00057C3C File Offset: 0x00055E3C
		protected override void UpdateMessage(MessageItem message)
		{
			base.UpdateMessage(message);
			if (this.IsGSCalendar)
			{
				message[NavigationNodeSchema.AddressBookEntryId] = this.GSCalendarSharerAddressBookEntryId;
				message.SetOrDeleteProperty(NavigationNodeSchema.AddressBookStoreEntryId, this.GSCalendarShareeStoreEntryId);
			}
			message.SetOrDeleteProperty(NavigationNodeSchema.NodeEntryId, this.NavigationNodeEntryId);
			message.SetOrDeleteProperty(NavigationNodeSchema.StoreEntryId, this.NavigationNodeStoreEntryId);
			message.SetOrDeleteProperty(NavigationNodeSchema.CalendarTypeFromOlderExchange, this.propertyBag.TryGetProperty(NavigationNodeSchema.CalendarTypeFromOlderExchange));
			message.SetOrDeleteProperty(NavigationNodeSchema.NodeRecordKey, this.NavigationNodeRecordKey);
			if (base.IsFavorites)
			{
				Guid associatedSearchFolderId = this.AssociatedSearchFolderId;
				message.SetOrDeleteProperty(FolderSchema.AssociatedSearchFolderId, associatedSearchFolderId.Equals(Guid.Empty) ? null : associatedSearchFolderId.ToByteArray());
				if (this.IsFilteredView)
				{
					message.SetOrDeleteProperty(ViewStateProperties.FilterSourceFolder, this.FilterSourceFolder.ToBase64String());
					message.SetOrDeleteProperty(ViewStateProperties.FilteredViewFlags, this.FilterFlag);
					message.SetOrDeleteProperty(ItemSchema.Categories, this.FitlerCategories);
					message.SetOrDeleteProperty(ViewStateProperties.FilteredViewFrom, this.FilterFrom);
					message.SetOrDeleteProperty(ViewStateProperties.FilteredViewTo, this.FilterTo);
				}
				else
				{
					message.Delete(ViewStateProperties.FilterSourceFolder);
					message.Delete(ViewStateProperties.FilteredViewFlags);
					message.Delete(ItemSchema.Categories);
					message.Delete(ViewStateProperties.FilteredViewFrom);
					message.Delete(ViewStateProperties.FilteredViewTo);
				}
			}
			message.SetOrDeleteProperty(NavigationNodeSchema.ParentGroupClassId, base.IsFavorites ? null : this.NavigationNodeParentGroupClassId.ToByteArray());
			message.SetOrDeleteProperty(NavigationNodeSchema.GroupName, base.IsFavorites ? null : this.NavigationNodeGroupName);
			if (base.NavigationNodeGroupSection == NavigationNodeGroupSection.Calendar)
			{
				message[NavigationNodeSchema.CalendarColor] = this.NavigationNodeCalendarColor;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x00057DFC File Offset: 0x00055FFC
		internal bool IsValid
		{
			get
			{
				if (!this.isMailboxLegacyDNValid || string.IsNullOrEmpty(this.MailboxLegacyDN))
				{
					return false;
				}
				if (base.IsFavorites)
				{
					return !this.AssociatedSearchFolderId.Equals(Guid.Empty) || this.FolderId != null;
				}
				if (this.IsGSCalendar)
				{
					string gscalendarShareeLegacyDN = this.GetGSCalendarShareeLegacyDN();
					return !string.IsNullOrEmpty(this.MailboxLegacyDN) && (string.IsNullOrEmpty(gscalendarShareeLegacyDN) || string.Equals(OwaContext.Current.UserContext.ExchangePrincipal.LegacyDn, gscalendarShareeLegacyDN, StringComparison.Ordinal));
				}
				return this.FolderId != null;
			}
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x00057E9C File Offset: 0x0005609C
		internal string GetFolderClass()
		{
			return NavigationNode.GetFolderClass(base.NavigationNodeGroupSection);
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x00057EA9 File Offset: 0x000560A9
		private void ThrowIfInFavorites()
		{
			if (base.IsFavorites)
			{
				throw new NotSupportedException("Favorite nodes don't support this property");
			}
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x00057EBE File Offset: 0x000560BE
		private void ThrowIfGSCalendar()
		{
			if (this.IsGSCalendar)
			{
				throw new NotSupportedException("GS calendar nodes don't support this property/method");
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x00057ED3 File Offset: 0x000560D3
		public bool IsGSCalendar
		{
			get
			{
				return null != this.GSCalendarSharerAddressBookEntryId;
			}
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x00057EE4 File Offset: 0x000560E4
		public void UpgradeToGSCalendar()
		{
			if (!this.IsPrimarySharedCalendar)
			{
				throw new InvalidOperationException("Only Primary shared calendar folder node can be upgraded to GS calendar.");
			}
			if (this.IsGSCalendar)
			{
				return;
			}
			UserContext userContext = OwaContext.Current.UserContext;
			ExchangePrincipal exchangePrincipal;
			if (userContext.DelegateSessionManager.TryGetExchangePrincipal(this.MailboxLegacyDN, out exchangePrincipal))
			{
				this.GSCalendarSharerAddressBookEntryId = AddressBookEntryId.MakeAddressBookEntryID(exchangePrincipal);
				base.Save(userContext.MailboxSession);
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x00057F45 File Offset: 0x00056145
		public bool IsPrimarySharedCalendar
		{
			get
			{
				return this.IsE14PrimarySharedCalendar || this.OlderExchangeSharedCalendarType == NavigationNodeFolder.OlderExchangeCalendarType.Primary;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000CED RID: 3309 RVA: 0x00057F5C File Offset: 0x0005615C
		public bool IsE14PrimarySharedCalendar
		{
			get
			{
				if (base.NavigationNodeType != NavigationNodeType.SharedFolder || base.NavigationNodeGroupSection != NavigationNodeGroupSection.Calendar)
				{
					return false;
				}
				if (this.isE14PrimarySharedCalendar == null)
				{
					this.isE14PrimarySharedCalendar = new bool?(false);
					UserContext userContext = OwaContext.Current.UserContext;
					ExchangePrincipal exchangePrincipal;
					if (userContext.DelegateSessionManager.TryGetExchangePrincipal(this.MailboxLegacyDN, out exchangePrincipal))
					{
						try
						{
							StoreObjectId storeObjectId = Utilities.TryGetDefaultFolderId(userContext, exchangePrincipal, DefaultFolderType.Calendar);
							if (storeObjectId != null)
							{
								this.isE14PrimarySharedCalendar = new bool?(storeObjectId.Equals(this.FolderId));
							}
						}
						catch (OwaSharedFromOlderVersionException)
						{
						}
					}
				}
				return this.isE14PrimarySharedCalendar.Value;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x00057FFC File Offset: 0x000561FC
		// (set) Token: 0x06000CEF RID: 3311 RVA: 0x0005802D File Offset: 0x0005622D
		public NavigationNodeFolder.OlderExchangeCalendarType OlderExchangeSharedCalendarType
		{
			get
			{
				object obj = this.propertyBag.TryGetProperty(NavigationNodeSchema.CalendarTypeFromOlderExchange);
				if (obj != null && obj is int)
				{
					return (NavigationNodeFolder.OlderExchangeCalendarType)obj;
				}
				return NavigationNodeFolder.OlderExchangeCalendarType.Unknown;
			}
			set
			{
				this.propertyBag.SetOrDeleteProperty(NavigationNodeSchema.CalendarTypeFromOlderExchange, (int)value);
			}
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x00058045 File Offset: 0x00056245
		private string GetGSCalendarShareeLegacyDN()
		{
			return StoreEntryId.TryParseMailboxLegacyDN(this.GSCalendarShareeStoreEntryId);
		}

		// Token: 0x040008FE RID: 2302
		private static readonly List<KeyValuePair<ExtendedFolderFlags, NavigationNodeFlags>> mappingFromFolderFlagToNodeFlag = NavigationNodeFolder.CreateMappingFromFolderFlagToNodeFlag();

		// Token: 0x040008FF RID: 2303
		private static PropertyDefinition[] navigationNodeFolderProperties = new PropertyDefinition[]
		{
			NavigationNodeSchema.NodeEntryId,
			NavigationNodeSchema.NodeRecordKey,
			NavigationNodeSchema.StoreEntryId,
			NavigationNodeSchema.ParentGroupClassId,
			NavigationNodeSchema.GroupName,
			NavigationNodeSchema.CalendarColor,
			NavigationNodeSchema.AddressBookEntryId,
			NavigationNodeSchema.AddressBookStoreEntryId,
			NavigationNodeSchema.CalendarTypeFromOlderExchange,
			FolderSchema.AssociatedSearchFolderId,
			ItemSchema.Categories,
			ViewStateProperties.FilteredViewFrom,
			ViewStateProperties.FilteredViewTo,
			ViewStateProperties.FilteredViewFlags,
			ViewStateProperties.FilterSourceFolder
		};

		// Token: 0x04000900 RID: 2304
		private string legacyDN;

		// Token: 0x04000901 RID: 2305
		private string calculatedLegacyDN;

		// Token: 0x04000902 RID: 2306
		private StoreObjectId folderId;

		// Token: 0x04000903 RID: 2307
		private StoreObjectId calculatedFolderId;

		// Token: 0x04000904 RID: 2308
		private int? hashCode = null;

		// Token: 0x04000905 RID: 2309
		private bool isMailboxLegacyDNValid = true;

		// Token: 0x04000906 RID: 2310
		private bool? isE14PrimarySharedCalendar;

		// Token: 0x0200016D RID: 365
		public enum OlderExchangeCalendarType
		{
			// Token: 0x04000908 RID: 2312
			Unknown,
			// Token: 0x04000909 RID: 2313
			Primary,
			// Token: 0x0400090A RID: 2314
			Secondary,
			// Token: 0x0400090B RID: 2315
			NotOlderVersion
		}
	}
}

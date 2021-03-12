using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200039D RID: 925
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CalendarGroupEntry : FolderTreeData, ICalendarGroupEntry, IMessageItem, IToDoItem, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x06002902 RID: 10498 RVA: 0x000A34C0 File Offset: 0x000A16C0
		public static CalendarGroupEntry BindFromCalendarFolder(MailboxSession session, StoreObjectId calendarFolderObjectId)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(calendarFolderObjectId, "calendarFolderObjectId");
			if (calendarFolderObjectId.ObjectType != StoreObjectType.CalendarFolder && calendarFolderObjectId.ObjectType != StoreObjectType.Folder)
			{
				throw new ArgumentException("StoreObject is not a calendar folder.", "calendarFolderObjectId");
			}
			VersionedId groupEntryIdFromFolderId = CalendarGroupEntry.GetGroupEntryIdFromFolderId(session, calendarFolderObjectId);
			if (groupEntryIdFromFolderId == null)
			{
				if (calendarFolderObjectId.Equals(session.GetDefaultFolderId(DefaultFolderType.Calendar)))
				{
					using (CalendarGroup calendarGroup = CalendarGroup.Bind(session, FolderTreeData.MyFoldersClassId))
					{
						ReadOnlyCollection<CalendarGroupEntryInfo> childCalendars = calendarGroup.GetChildCalendars();
						foreach (CalendarGroupEntryInfo calendarGroupEntryInfo in childCalendars)
						{
							LocalCalendarGroupEntryInfo localCalendarGroupEntryInfo = calendarGroupEntryInfo as LocalCalendarGroupEntryInfo;
							if (localCalendarGroupEntryInfo != null && calendarFolderObjectId.Equals(localCalendarGroupEntryInfo.CalendarId))
							{
								return CalendarGroupEntry.Bind(session, calendarGroupEntryInfo.Id, null);
							}
						}
					}
				}
				throw new ObjectNotFoundException(ServerStrings.ExItemNotFound);
			}
			return CalendarGroupEntry.Bind(session, groupEntryIdFromFolderId, null);
		}

		// Token: 0x06002903 RID: 10499 RVA: 0x000A35F0 File Offset: 0x000A17F0
		public static VersionedId GetGroupEntryIdFromFolderId(MailboxSession session, StoreObjectId calendarFolderObjectId)
		{
			return FolderTreeData.FindFirstRowMatchingFilter(session, CalendarGroup.CalendarInfoProperties, (IStorePropertyBag row) => CalendarGroup.IsFolderTreeData(row) && CalendarGroup.IsCalendarSection(row) && CalendarGroup.IsCalendarGroupEntryForCalendar(row, calendarFolderObjectId));
		}

		// Token: 0x06002904 RID: 10500 RVA: 0x000A3624 File Offset: 0x000A1824
		public static CalendarGroupEntry Bind(MailboxSession session, StoreId storeId, ICollection<PropertyDefinition> propsToReturn = null)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(storeId, "storeId");
			CalendarGroupEntry calendarGroupEntry = ItemBuilder.ItemBind<CalendarGroupEntry>(session, storeId, CalendarGroupEntrySchema.Instance, propsToReturn);
			calendarGroupEntry.MailboxSession = session;
			return calendarGroupEntry;
		}

		// Token: 0x06002905 RID: 10501 RVA: 0x000A3660 File Offset: 0x000A1860
		public static CalendarGroupEntry Create(MailboxSession session, string legacyDistinguishedName, CalendarGroup parentGroup)
		{
			Util.ThrowOnNullOrEmptyArgument(legacyDistinguishedName, "legacyDistinguishedName");
			CalendarGroupEntry calendarGroupEntry = CalendarGroupEntry.Create(session, parentGroup.GroupClassId, parentGroup.GroupName);
			calendarGroupEntry[FolderTreeDataSchema.Type] = FolderTreeDataType.SharedFolder;
			calendarGroupEntry[FolderTreeDataSchema.FolderTreeDataFlags] = 0;
			calendarGroupEntry.SharerAddressBookEntryId = AddressBookEntryId.MakeAddressBookEntryID(legacyDistinguishedName, false);
			calendarGroupEntry.UserAddressBookStoreEntryId = Microsoft.Exchange.Data.Storage.StoreEntryId.ToProviderStoreEntryId(session.MailboxOwner);
			return calendarGroupEntry;
		}

		// Token: 0x06002906 RID: 10502 RVA: 0x000A36CC File Offset: 0x000A18CC
		public static CalendarGroupEntry Create(MailboxSession session, CalendarFolder calendarFolder, CalendarGroup parentGroup)
		{
			Util.ThrowOnNullArgument(parentGroup, "parentGroup");
			Util.ThrowOnNullArgument(calendarFolder, "calendarFolder");
			CalendarGroupEntry calendarGroupEntry = CalendarGroupEntry.Create(session, calendarFolder.Id.ObjectId, parentGroup.GroupClassId, parentGroup.GroupName);
			FolderTreeDataFlags folderTreeDataFlags = CalendarGroupEntry.ReadSharingFlags(calendarFolder);
			bool flag = !session.MailboxGuid.Equals(((MailboxSession)calendarFolder.Session).MailboxGuid);
			if (flag)
			{
				calendarGroupEntry[FolderTreeDataSchema.Type] = FolderTreeDataType.SharedFolder;
				calendarGroupEntry.StoreEntryId = Microsoft.Exchange.Data.Storage.StoreEntryId.ToProviderStoreEntryId(((MailboxSession)calendarFolder.Session).MailboxOwner);
				calendarGroupEntry.CalendarRecordKey = (byte[])calendarFolder.TryGetProperty(StoreObjectSchema.RecordKey);
			}
			else
			{
				folderTreeDataFlags |= FolderTreeDataFlags.IsDefaultStore;
			}
			calendarGroupEntry[FolderTreeDataSchema.FolderTreeDataFlags] = folderTreeDataFlags;
			return calendarGroupEntry;
		}

		// Token: 0x06002907 RID: 10503 RVA: 0x000A3798 File Offset: 0x000A1998
		public static CalendarGroupEntry Create(MailboxSession session, StoreObjectId calendarFolderId, CalendarGroup parentGroup)
		{
			Util.ThrowOnNullArgument(parentGroup, "parentGroup");
			CalendarGroupEntry calendarGroupEntry = CalendarGroupEntry.Create(session, calendarFolderId, parentGroup.GroupClassId, parentGroup.GroupName);
			calendarGroupEntry.parentGroup = parentGroup;
			return calendarGroupEntry;
		}

		// Token: 0x06002908 RID: 10504 RVA: 0x000A37CC File Offset: 0x000A19CC
		internal static CalendarGroupEntry Create(MailboxSession session, StoreObjectId calendarFolderId, Guid parentGroupClassId, string parentGroupName)
		{
			Util.ThrowOnNullArgument(calendarFolderId, "calendarFolderId");
			if (calendarFolderId.ObjectType != StoreObjectType.CalendarFolder)
			{
				throw new NotSupportedException("A calendar group entry can only be associated with a storeobject of type calendar folder.");
			}
			CalendarGroupEntry calendarGroupEntry = CalendarGroupEntry.Create(session, parentGroupClassId, parentGroupName);
			calendarGroupEntry.CalendarId = calendarFolderId;
			calendarGroupEntry.StoreEntryId = Microsoft.Exchange.Data.Storage.StoreEntryId.ToProviderStoreEntryId(session.MailboxOwner);
			return calendarGroupEntry;
		}

		// Token: 0x06002909 RID: 10505 RVA: 0x000A381C File Offset: 0x000A1A1C
		internal static CalendarGroupEntry Create(MailboxSession session, Guid parentGroupClassId, string parentGroupName)
		{
			Util.ThrowOnNullArgument(session, "session");
			CalendarGroupEntry calendarGroupEntry = ItemBuilder.CreateNewItem<CalendarGroupEntry>(session, session.GetDefaultFolderId(DefaultFolderType.CommonViews), ItemCreateInfo.CalendarGroupEntryInfo, CreateMessageType.Associated);
			calendarGroupEntry.MailboxSession = session;
			calendarGroupEntry.ParentGroupClassId = parentGroupClassId;
			calendarGroupEntry.ParentGroupName = parentGroupName;
			return calendarGroupEntry;
		}

		// Token: 0x0600290A RID: 10506 RVA: 0x000A3860 File Offset: 0x000A1A60
		internal static CalendarGroupEntryInfo GetCalendarGroupEntryInfoFromRow(IStorePropertyBag row)
		{
			VersionedId versionedId = (VersionedId)row.TryGetProperty(ItemSchema.Id);
			byte[] valueOrDefault = row.GetValueOrDefault<byte[]>(CalendarGroupEntrySchema.NodeEntryId, null);
			byte[] valueOrDefault2 = row.GetValueOrDefault<byte[]>(FolderTreeDataSchema.ParentGroupClassId, null);
			string valueOrDefault3 = row.GetValueOrDefault<string>(ItemSchema.Subject, string.Empty);
			LegacyCalendarColor valueOrDefault4 = row.GetValueOrDefault<LegacyCalendarColor>(CalendarGroupEntrySchema.CalendarColor, LegacyCalendarColor.Auto);
			byte[] valueOrDefault5 = row.GetValueOrDefault<byte[]>(FolderTreeDataSchema.Ordinal, null);
			byte[] valueOrDefault6 = row.GetValueOrDefault<byte[]>(CalendarGroupEntrySchema.SharerAddressBookEntryId, null);
			byte[] valueOrDefault7 = row.GetValueOrDefault<byte[]>(CalendarGroupEntrySchema.StoreEntryId, null);
			ExDateTime valueOrDefault8 = row.GetValueOrDefault<ExDateTime>(StoreObjectSchema.LastModifiedTime, ExDateTime.MinValue);
			FolderTreeDataType valueOrDefault9 = row.GetValueOrDefault<FolderTreeDataType>(FolderTreeDataSchema.Type, FolderTreeDataType.NormalFolder);
			FolderTreeDataFlags valueOrDefault10 = row.GetValueOrDefault<FolderTreeDataFlags>(FolderTreeDataSchema.FolderTreeDataFlags, FolderTreeDataFlags.None);
			Guid safeGuidFromByteArray = FolderTreeData.GetSafeGuidFromByteArray(valueOrDefault2);
			if (safeGuidFromByteArray.Equals(Guid.Empty))
			{
				ExTraceGlobals.StorageTracer.TraceDebug<int>(0L, "Found CalendarGroupEntry with invalid parent group class id. ArrayLength: {0}", (valueOrDefault2 == null) ? -1 : valueOrDefault2.Length);
				return null;
			}
			if (valueOrDefault9 != FolderTreeDataType.SharedFolder)
			{
				if (IdConverter.IsFolderId(valueOrDefault))
				{
					StoreObjectId storeObjectId = StoreObjectId.FromProviderSpecificId(valueOrDefault);
					if ((valueOrDefault10 & FolderTreeDataFlags.IsDefaultStore) == FolderTreeDataFlags.IsDefaultStore)
					{
						return new LocalCalendarGroupEntryInfo(valueOrDefault3, versionedId, valueOrDefault4, storeObjectId, valueOrDefault5, safeGuidFromByteArray, (valueOrDefault10 & FolderTreeDataFlags.ICalFolder) == FolderTreeDataFlags.ICalFolder || (valueOrDefault10 & FolderTreeDataFlags.SharedIn) == FolderTreeDataFlags.SharedIn, valueOrDefault8);
					}
					if ((valueOrDefault10 & FolderTreeDataFlags.PublicFolder) == FolderTreeDataFlags.PublicFolder)
					{
						string calendarOwner = Microsoft.Exchange.Data.Storage.StoreEntryId.TryParseStoreEntryIdMailboxDN(valueOrDefault7);
						storeObjectId = StoreObjectId.FromLegacyFavoritePublicFolderId(storeObjectId);
						return new LinkedCalendarGroupEntryInfo(valueOrDefault3, versionedId, valueOrDefault4, storeObjectId, calendarOwner, safeGuidFromByteArray, valueOrDefault5, false, true, valueOrDefault8);
					}
					ExTraceGlobals.StorageTracer.TraceDebug<StoreObjectType, string, VersionedId>(0L, "Found CalendarGroupEntry of type {0} referencing a non-calendar folder. ObjectType: {0}. CalendarName: {1}. Id: {2}.", storeObjectId.ObjectType, valueOrDefault3, versionedId);
				}
				return null;
			}
			bool flag = true;
			Eidt eidt;
			string text;
			if (!AddressBookEntryId.IsAddressBookEntryId(valueOrDefault6, out eidt, out text))
			{
				ExTraceGlobals.StorageTracer.TraceDebug<string>(0L, "AddressBookEntryId is missing, not primary calendar {0}", valueOrDefault3);
				if (valueOrDefault7 == null)
				{
					ExTraceGlobals.StorageTracer.TraceDebug<string>(0L, "StoreEntryId is missing for calendar: {0} - invalid entry, skipping.", valueOrDefault3);
					return null;
				}
				text = Microsoft.Exchange.Data.Storage.StoreEntryId.TryParseStoreEntryIdMailboxDN(valueOrDefault7);
				flag = false;
			}
			if (text == null)
			{
				ExTraceGlobals.StorageTracer.TraceDebug<string>(0L, "Unable to determine owner of shared calendar: {0}. Skipping.", valueOrDefault3);
				return null;
			}
			StoreObjectId storeObjectId2 = IdConverter.IsFolderId(valueOrDefault) ? StoreObjectId.FromProviderSpecificId(valueOrDefault) : null;
			if (!flag && storeObjectId2 == null)
			{
				ExTraceGlobals.StorageTracer.TraceDebug<string>(0L, "Secondary shared calendar without a folder id encountered {0}. Skipping.", valueOrDefault3);
				return null;
			}
			return new LinkedCalendarGroupEntryInfo(valueOrDefault3, versionedId, valueOrDefault4, storeObjectId2, text, safeGuidFromByteArray, valueOrDefault5, flag, false, valueOrDefault8);
		}

		// Token: 0x0600290B RID: 10507 RVA: 0x000A3A8C File Offset: 0x000A1C8C
		public CalendarGroupEntry(ICoreItem coreItem) : base(coreItem)
		{
			if (base.IsNew)
			{
				this.InitializeNewCalendarGroupEntry();
				return;
			}
			this.Initialize();
		}

		// Token: 0x0600290C RID: 10508 RVA: 0x000A3AAA File Offset: 0x000A1CAA
		public CalendarGroupEntryInfo GetCalendarGroupEntryInfo()
		{
			return CalendarGroupEntry.GetCalendarGroupEntryInfoFromRow(this);
		}

		// Token: 0x0600290D RID: 10509 RVA: 0x000A3AB4 File Offset: 0x000A1CB4
		private static FolderTreeDataFlags ReadSharingFlags(CalendarFolder calendarFolder)
		{
			ExtendedFolderFlags valueOrDefault = calendarFolder.GetValueOrDefault<ExtendedFolderFlags>(FolderSchema.ExtendedFolderFlags);
			FolderTreeDataFlags folderTreeDataFlags = FolderTreeDataFlags.None;
			foreach (ExtendedFolderFlags extendedFolderFlags in CalendarGroupEntry.mapExtendedFolderToSharingFlag.Keys)
			{
				if ((valueOrDefault & extendedFolderFlags) == extendedFolderFlags)
				{
					folderTreeDataFlags |= CalendarGroupEntry.mapExtendedFolderToSharingFlag[extendedFolderFlags];
				}
			}
			return folderTreeDataFlags;
		}

		// Token: 0x0600290E RID: 10510 RVA: 0x000A3B28 File Offset: 0x000A1D28
		private void InitializeNewCalendarGroupEntry()
		{
			this[StoreObjectSchema.ItemClass] = "IPM.Microsoft.WunderBar.Link";
			this[FolderTreeDataSchema.GroupSection] = FolderTreeDataSection.Calendar;
			this[FolderTreeDataSchema.ClassId] = CalendarGroup.CalendarSectionClassId.ToByteArray();
			this[FolderTreeDataSchema.Type] = FolderTreeDataType.NormalFolder;
			this[FolderTreeDataSchema.FolderTreeDataFlags] = FolderTreeDataFlags.IsDefaultStore;
			this[CalendarGroupEntrySchema.CalendarColor] = LegacyCalendarColor.Auto;
		}

		// Token: 0x0600290F RID: 10511 RVA: 0x000A3BA5 File Offset: 0x000A1DA5
		private void Initialize()
		{
			this.SetCalendarId(base.GetValueOrDefault<byte[]>(CalendarGroupEntrySchema.NodeEntryId));
			this.parentGroupClassId = FolderTreeData.GetSafeGuidFromByteArray(base.GetValueOrDefault<byte[]>(FolderTreeDataSchema.ParentGroupClassId));
		}

		// Token: 0x06002910 RID: 10512 RVA: 0x000A3BCE File Offset: 0x000A1DCE
		private void SetCalendarId(byte[] entryId)
		{
			if (IdConverter.IsFolderId(entryId))
			{
				this.calendarObjectId = StoreObjectId.FromProviderSpecificIdOrNull(entryId);
			}
		}

		// Token: 0x17000D76 RID: 3446
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
				if (propertyDefinition == CalendarGroupEntrySchema.NodeEntryId)
				{
					this.SetCalendarId(value as byte[]);
					return;
				}
				if (propertyDefinition == FolderTreeDataSchema.ParentGroupClassId)
				{
					this.parentGroupClassId = FolderTreeData.GetSafeGuidFromByteArray(value as byte[]);
				}
			}
		}

		// Token: 0x17000D77 RID: 3447
		// (get) Token: 0x06002913 RID: 10515 RVA: 0x000A3C46 File Offset: 0x000A1E46
		public override Schema Schema
		{
			get
			{
				this.CheckDisposed("Schema::get");
				return CalendarGroupEntrySchema.Instance;
			}
		}

		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x06002914 RID: 10516 RVA: 0x000A3C58 File Offset: 0x000A1E58
		// (set) Token: 0x06002915 RID: 10517 RVA: 0x000A3C70 File Offset: 0x000A1E70
		public byte[] StoreEntryId
		{
			get
			{
				this.CheckDisposed("StoreEntryId::get");
				return base.GetValueOrDefault<byte[]>(CalendarGroupEntrySchema.StoreEntryId);
			}
			set
			{
				this.CheckDisposed("StoreEntryId::set");
				this[CalendarGroupEntrySchema.StoreEntryId] = value;
			}
		}

		// Token: 0x17000D79 RID: 3449
		// (get) Token: 0x06002916 RID: 10518 RVA: 0x000A3C89 File Offset: 0x000A1E89
		// (set) Token: 0x06002917 RID: 10519 RVA: 0x000A3CA1 File Offset: 0x000A1EA1
		public string CalendarName
		{
			get
			{
				this.CheckDisposed("CalendarName::get");
				return base.GetValueOrDefault<string>(CalendarGroupEntrySchema.CalendarName);
			}
			set
			{
				this.CheckDisposed("CalendarName::set");
				this[CalendarGroupEntrySchema.CalendarName] = value;
			}
		}

		// Token: 0x17000D7A RID: 3450
		// (get) Token: 0x06002918 RID: 10520 RVA: 0x000A3CBA File Offset: 0x000A1EBA
		// (set) Token: 0x06002919 RID: 10521 RVA: 0x000A3CD2 File Offset: 0x000A1ED2
		public string ParentGroupName
		{
			get
			{
				this.CheckDisposed("ParentGroupName::get");
				return base.GetValueOrDefault<string>(CalendarGroupEntrySchema.ParentGroupName);
			}
			private set
			{
				this.CheckDisposed("ParentGroupName::set");
				this[CalendarGroupEntrySchema.ParentGroupName] = value;
			}
		}

		// Token: 0x17000D7B RID: 3451
		// (get) Token: 0x0600291A RID: 10522 RVA: 0x000A3CEB File Offset: 0x000A1EEB
		// (set) Token: 0x0600291B RID: 10523 RVA: 0x000A3CFE File Offset: 0x000A1EFE
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

		// Token: 0x17000D7C RID: 3452
		// (get) Token: 0x0600291C RID: 10524 RVA: 0x000A3D1D File Offset: 0x000A1F1D
		// (set) Token: 0x0600291D RID: 10525 RVA: 0x000A3D36 File Offset: 0x000A1F36
		public LegacyCalendarColor LegacyCalendarColor
		{
			get
			{
				this.CheckDisposed("LegacyCalendarColor::get");
				return base.GetValueOrDefault<LegacyCalendarColor>(CalendarGroupEntrySchema.CalendarColor, LegacyCalendarColor.Auto);
			}
			set
			{
				this.CheckDisposed("LegacyCalendarColor::set");
				EnumValidator.ThrowIfInvalid<LegacyCalendarColor>(value, "LegacyCalendarColor");
				this[CalendarGroupEntrySchema.CalendarColor] = value;
			}
		}

		// Token: 0x17000D7D RID: 3453
		// (get) Token: 0x0600291E RID: 10526 RVA: 0x000A3D5F File Offset: 0x000A1F5F
		// (set) Token: 0x0600291F RID: 10527 RVA: 0x000A3D77 File Offset: 0x000A1F77
		public CalendarColor CalendarColor
		{
			get
			{
				this.CheckDisposed("CalendarColor::get");
				return LegacyCalendarColorConverter.FromLegacyCalendarColor(this.LegacyCalendarColor);
			}
			set
			{
				this.CheckDisposed("CalendarColor::set");
				EnumValidator.ThrowIfInvalid<CalendarColor>(value, "CalendarColor");
				this.LegacyCalendarColor = LegacyCalendarColorConverter.ToLegacyCalendarColor(value);
			}
		}

		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x06002920 RID: 10528 RVA: 0x000A3D9B File Offset: 0x000A1F9B
		// (set) Token: 0x06002921 RID: 10529 RVA: 0x000A3DAE File Offset: 0x000A1FAE
		public StoreObjectId CalendarId
		{
			get
			{
				this.CheckDisposed("CalendarId::get");
				return this.calendarObjectId;
			}
			set
			{
				this.CheckDisposed("CalendarId::set");
				this[CalendarGroupEntrySchema.NodeEntryId] = value.ProviderLevelItemId;
			}
		}

		// Token: 0x17000D7F RID: 3455
		// (get) Token: 0x06002922 RID: 10530 RVA: 0x000A3DCC File Offset: 0x000A1FCC
		public VersionedId CalendarGroupEntryId
		{
			get
			{
				this.CheckDisposed("CalendarGroupEntryId::get");
				return base.GetValueOrDefault<VersionedId>(ItemSchema.Id, null);
			}
		}

		// Token: 0x17000D80 RID: 3456
		// (get) Token: 0x06002923 RID: 10531 RVA: 0x000A3DE5 File Offset: 0x000A1FE5
		// (set) Token: 0x06002924 RID: 10532 RVA: 0x000A3DFD File Offset: 0x000A1FFD
		public byte[] CalendarRecordKey
		{
			get
			{
				this.CheckDisposed("CalendarRecordKey::get");
				return base.GetValueOrDefault<byte[]>(CalendarGroupEntrySchema.NodeRecordKey);
			}
			set
			{
				this.CheckDisposed("CalendarRecordKey::set");
				this[CalendarGroupEntrySchema.NodeRecordKey] = value;
			}
		}

		// Token: 0x17000D81 RID: 3457
		// (get) Token: 0x06002925 RID: 10533 RVA: 0x000A3E16 File Offset: 0x000A2016
		public bool IsLocalMailboxCalendar
		{
			get
			{
				return this.CalendarId != null && base.FolderTreeDataType == FolderTreeDataType.NormalFolder && (base.FolderTreeDataFlags & FolderTreeDataFlags.IsDefaultStore) == FolderTreeDataFlags.IsDefaultStore;
			}
		}

		// Token: 0x17000D82 RID: 3458
		// (get) Token: 0x06002926 RID: 10534 RVA: 0x000A3E3D File Offset: 0x000A203D
		// (set) Token: 0x06002927 RID: 10535 RVA: 0x000A3E55 File Offset: 0x000A2055
		public byte[] SharerAddressBookEntryId
		{
			get
			{
				this.CheckDisposed("SharerAddressBookEntryId::get");
				return base.GetValueOrDefault<byte[]>(CalendarGroupEntrySchema.SharerAddressBookEntryId);
			}
			set
			{
				this.CheckDisposed("SharerAddressBookEntryId::set");
				this[CalendarGroupEntrySchema.SharerAddressBookEntryId] = value;
			}
		}

		// Token: 0x17000D83 RID: 3459
		// (get) Token: 0x06002928 RID: 10536 RVA: 0x000A3E6E File Offset: 0x000A206E
		// (set) Token: 0x06002929 RID: 10537 RVA: 0x000A3E86 File Offset: 0x000A2086
		public byte[] UserAddressBookStoreEntryId
		{
			get
			{
				this.CheckDisposed("UserAddressBookStoreEntryId::get");
				return base.GetValueOrDefault<byte[]>(CalendarGroupEntrySchema.UserAddressBookStoreEntryId);
			}
			set
			{
				this.CheckDisposed("UserAddressBookStoreEntryId::set");
				this[CalendarGroupEntrySchema.UserAddressBookStoreEntryId] = value;
			}
		}

		// Token: 0x0600292A RID: 10538 RVA: 0x000A3EC0 File Offset: 0x000A20C0
		protected override void OnBeforeSave()
		{
			base.OnBeforeSave();
			if (base.IsNew)
			{
				if (Guid.Empty.Equals(this.ParentGroupClassId))
				{
					throw new NotSupportedException("A new calendar group entry needs to have its ParentGroupClassId set.");
				}
				byte[] nodeBefore = null;
				if (this.parentGroup != null)
				{
					ReadOnlyCollection<CalendarGroupEntryInfo> childCalendars = this.parentGroup.GetChildCalendars();
					if (childCalendars.Count > 0)
					{
						nodeBefore = childCalendars[childCalendars.Count - 1].Ordinal;
					}
				}
				else
				{
					bool flag;
					nodeBefore = FolderTreeData.GetOrdinalValueOfFirstMatchingNode(base.MailboxSession, CalendarGroupEntry.FindLastCalendarOrdinalSortOrder, (IStorePropertyBag row) => CalendarGroup.IsFolderTreeData(row) && CalendarGroup.IsCalendarSection(row) && CalendarGroup.IsCalendarInGroup(row, this.ParentGroupClassId), CalendarGroup.CalendarInfoProperties, out flag);
				}
				base.SetNodeOrdinalInternal(nodeBefore, null);
			}
		}

		// Token: 0x040017DF RID: 6111
		private static readonly SortBy[] FindLastCalendarOrdinalSortOrder = new SortBy[]
		{
			new SortBy(StoreObjectSchema.ItemClass, SortOrder.Ascending),
			new SortBy(FolderTreeDataSchema.GroupSection, SortOrder.Ascending),
			new SortBy(FolderTreeDataSchema.ParentGroupClassId, SortOrder.Descending),
			new SortBy(FolderTreeDataSchema.Ordinal, SortOrder.Descending)
		};

		// Token: 0x040017E0 RID: 6112
		private static readonly Dictionary<ExtendedFolderFlags, FolderTreeDataFlags> mapExtendedFolderToSharingFlag = new Dictionary<ExtendedFolderFlags, FolderTreeDataFlags>
		{
			{
				ExtendedFolderFlags.IsSharepointFolder,
				FolderTreeDataFlags.SharepointFolder
			},
			{
				ExtendedFolderFlags.SharedIn,
				FolderTreeDataFlags.SharedIn
			},
			{
				ExtendedFolderFlags.SharedOut,
				FolderTreeDataFlags.SharedOut
			},
			{
				ExtendedFolderFlags.SharedViaExchange,
				FolderTreeDataFlags.SharedOut
			},
			{
				ExtendedFolderFlags.PersonalShare,
				FolderTreeDataFlags.PersonFolder
			},
			{
				ExtendedFolderFlags.ICalFolder,
				FolderTreeDataFlags.ICalFolder
			}
		};

		// Token: 0x040017E1 RID: 6113
		private StoreObjectId calendarObjectId;

		// Token: 0x040017E2 RID: 6114
		private Guid parentGroupClassId;

		// Token: 0x040017E3 RID: 6115
		private CalendarGroup parentGroup;
	}
}

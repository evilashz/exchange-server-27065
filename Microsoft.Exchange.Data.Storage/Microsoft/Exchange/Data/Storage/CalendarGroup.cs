using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200039B RID: 923
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CalendarGroup : FolderTreeData, ICalendarGroup, IFolderTreeData, IMessageItem, IToDoItem, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x060028C8 RID: 10440 RVA: 0x000A24CC File Offset: 0x000A06CC
		internal static bool IsFolderTreeData(IStorePropertyBag row)
		{
			string valueOrDefault = row.GetValueOrDefault<string>(StoreObjectSchema.ItemClass, string.Empty);
			return ObjectClass.IsFolderTreeData(valueOrDefault);
		}

		// Token: 0x060028C9 RID: 10441 RVA: 0x000A24F0 File Offset: 0x000A06F0
		internal static bool IsCalendarSection(IStorePropertyBag row)
		{
			FolderTreeDataSection valueOrDefault = row.GetValueOrDefault<FolderTreeDataSection>(FolderTreeDataSchema.GroupSection, FolderTreeDataSection.None);
			return valueOrDefault == FolderTreeDataSection.Calendar;
		}

		// Token: 0x060028CA RID: 10442 RVA: 0x000A2510 File Offset: 0x000A0710
		internal static bool IsCalendarGroupEntry(IStorePropertyBag row)
		{
			FolderTreeDataType valueOrDefault = row.GetValueOrDefault<FolderTreeDataType>(FolderTreeDataSchema.Type, FolderTreeDataType.Undefined);
			return valueOrDefault == FolderTreeDataType.NormalFolder || valueOrDefault == FolderTreeDataType.SharedFolder;
		}

		// Token: 0x060028CB RID: 10443 RVA: 0x000A2534 File Offset: 0x000A0734
		internal static bool IsCalendarGroup(IStorePropertyBag row)
		{
			FolderTreeDataType valueOrDefault = row.GetValueOrDefault<FolderTreeDataType>(FolderTreeDataSchema.Type, FolderTreeDataType.Undefined);
			return valueOrDefault == FolderTreeDataType.Header;
		}

		// Token: 0x060028CC RID: 10444 RVA: 0x000A2554 File Offset: 0x000A0754
		internal static bool IsCalendarGroup(IStorePropertyBag row, Guid groupId)
		{
			byte[] valueOrDefault = row.GetValueOrDefault<byte[]>(CalendarGroupSchema.GroupClassId, null);
			return CalendarGroup.IsCalendarGroup(row) && Util.CompareByteArray(groupId.ToByteArray(), valueOrDefault);
		}

		// Token: 0x060028CD RID: 10445 RVA: 0x000A2588 File Offset: 0x000A0788
		internal static bool IsCalendarGroupEntryForCalendar(IStorePropertyBag row, StoreObjectId calendarId)
		{
			byte[] valueOrDefault = row.GetValueOrDefault<byte[]>(CalendarGroupEntrySchema.NodeEntryId, null);
			return CalendarGroup.IsCalendarGroupEntry(row) && Util.CompareByteArray(calendarId.ProviderLevelItemId, valueOrDefault);
		}

		// Token: 0x060028CE RID: 10446 RVA: 0x000A25B8 File Offset: 0x000A07B8
		internal static bool IsCalendarInGroup(IStorePropertyBag row, Guid groupClassId)
		{
			byte[] valueOrDefault = row.GetValueOrDefault<byte[]>(FolderTreeDataSchema.ParentGroupClassId, null);
			return CalendarGroup.IsCalendarGroupEntry(row) && Util.CompareByteArray(groupClassId.ToByteArray(), valueOrDefault);
		}

		// Token: 0x060028CF RID: 10447 RVA: 0x000A25EC File Offset: 0x000A07EC
		public static CalendarGroup Bind(MailboxSession session, CalendarGroupType defaultGroupType)
		{
			EnumValidator.ThrowIfInvalid<CalendarGroupType>(defaultGroupType, new CalendarGroupType[]
			{
				CalendarGroupType.MyCalendars,
				CalendarGroupType.OtherCalendars,
				CalendarGroupType.PeoplesCalendars
			});
			return CalendarGroup.Bind(session, CalendarGroup.GetGroupGuidFromType(defaultGroupType));
		}

		// Token: 0x060028D0 RID: 10448 RVA: 0x000A261C File Offset: 0x000A081C
		public static CalendarGroup Bind(MailboxSession session, Guid groupClassId)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(groupClassId, "groupClassId");
			if (groupClassId.Equals(Guid.Empty))
			{
				throw new ArgumentException("Invalid GroupClassId", "groupClassId");
			}
			CalendarGroup calendarGroup = null;
			bool flag = true;
			bool flag2 = false;
			CalendarGroup result;
			using (Folder folder = Folder.Bind(session, DefaultFolderType.CommonViews))
			{
				using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.Associated, null, CalendarGroup.CalendarGroupViewSortOrder, CalendarGroup.CalendarInfoProperties))
				{
					queryResult.SeekToCondition(SeekReference.OriginBeginning, CalendarGroup.CalendarSectionFilter);
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
							if (!CalendarGroup.IsCalendarSection(storePropertyBag))
							{
								flag = false;
								break;
							}
							if (CalendarGroup.IsFolderTreeData(storePropertyBag))
							{
								if (!flag2 && CalendarGroup.IsCalendarGroup(storePropertyBag, groupClassId))
								{
									flag2 = true;
									VersionedId storeId = (VersionedId)storePropertyBag.TryGetProperty(ItemSchema.Id);
									calendarGroup = CalendarGroup.Bind(session, storeId, null);
								}
								if (flag2)
								{
									calendarGroup.LoadChildNodesCollection(propertyBags, i);
									break;
								}
							}
						}
					}
					if (flag2)
					{
						result = calendarGroup;
					}
					else if (FolderTreeData.MyFoldersClassId.Equals(groupClassId))
					{
						result = CalendarGroup.CreateMyCalendarsGroup(session);
					}
					else
					{
						if (!FolderTreeData.OtherFoldersClassId.Equals(groupClassId))
						{
							throw new ObjectNotFoundException(ServerStrings.ExItemNotFound);
						}
						result = CalendarGroup.InternalCreateGroup(session, CalendarGroupType.OtherCalendars);
					}
				}
			}
			return result;
		}

		// Token: 0x060028D1 RID: 10449 RVA: 0x000A279C File Offset: 0x000A099C
		public static CalendarGroup Bind(MailboxSession session, StoreId storeId, ICollection<PropertyDefinition> propsToReturn = null)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(storeId, "storeId");
			CalendarGroup calendarGroup = ItemBuilder.ItemBind<CalendarGroup>(session, storeId, CalendarGroupSchema.Instance, propsToReturn);
			calendarGroup.MailboxSession = session;
			return calendarGroup;
		}

		// Token: 0x060028D2 RID: 10450 RVA: 0x000A27D8 File Offset: 0x000A09D8
		public static CalendarGroup Create(MailboxSession session)
		{
			Util.ThrowOnNullArgument(session, "session");
			CalendarGroup calendarGroup = ItemBuilder.CreateNewItem<CalendarGroup>(session, session.GetDefaultFolderId(DefaultFolderType.CommonViews), ItemCreateInfo.CalendarGroupInfo, CreateMessageType.Associated);
			calendarGroup.MailboxSession = session;
			return calendarGroup;
		}

		// Token: 0x060028D3 RID: 10451 RVA: 0x000A2810 File Offset: 0x000A0A10
		public static CalendarGroupInfoList GetCalendarGroupsView(MailboxSession session)
		{
			bool flag = false;
			bool flag2 = true;
			Dictionary<Guid, CalendarGroupInfo> guidToGroupMapping = new Dictionary<Guid, CalendarGroupInfo>();
			Dictionary<StoreObjectId, LocalCalendarGroupEntryInfo> dictionary = new Dictionary<StoreObjectId, LocalCalendarGroupEntryInfo>();
			List<FolderTreeDataInfo> duplicateNodes = new List<FolderTreeDataInfo>();
			Dictionary<CalendarGroupType, CalendarGroupInfo> defaultGroups = new Dictionary<CalendarGroupType, CalendarGroupInfo>();
			CalendarGroupInfoList calendarGroupInfoList = new CalendarGroupInfoList(duplicateNodes, defaultGroups, dictionary);
			using (Folder folder = Folder.Bind(session, DefaultFolderType.CommonViews))
			{
				using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.Associated, null, CalendarGroup.CalendarGroupViewSortOrder, CalendarGroup.CalendarInfoProperties))
				{
					queryResult.SeekToCondition(SeekReference.OriginBeginning, CalendarGroup.CalendarSectionFilter);
					while (flag2)
					{
						IStorePropertyBag[] propertyBags = queryResult.GetPropertyBags(10000);
						if (propertyBags.Length == 0)
						{
							break;
						}
						foreach (IStorePropertyBag storePropertyBag in propertyBags)
						{
							if (!CalendarGroup.IsCalendarSection(storePropertyBag))
							{
								flag2 = false;
								break;
							}
							if (CalendarGroup.IsFolderTreeData(storePropertyBag))
							{
								if (CalendarGroup.IsCalendarGroup(storePropertyBag))
								{
									if (flag)
									{
										ExTraceGlobals.StorageTracer.TraceDebug<VersionedId, string>(0L, "Unexpected processing calendar group out of order. ItemId: {0}, Subject: {1}", (VersionedId)storePropertyBag.TryGetProperty(ItemSchema.Id), storePropertyBag.GetValueOrDefault<string>(ItemSchema.Subject, string.Empty));
									}
									else
									{
										CalendarGroup.AddGroupToList(storePropertyBag, guidToGroupMapping, calendarGroupInfoList);
									}
								}
								else if (CalendarGroup.IsCalendarGroupEntry(storePropertyBag))
								{
									flag = true;
									CalendarGroup.AddCalendarToList(storePropertyBag, guidToGroupMapping, dictionary, calendarGroupInfoList);
								}
							}
						}
					}
				}
			}
			return calendarGroupInfoList;
		}

		// Token: 0x060028D4 RID: 10452 RVA: 0x000A2970 File Offset: 0x000A0B70
		private static void AddCalendarToList(IStorePropertyBag row, Dictionary<Guid, CalendarGroupInfo> guidToGroupMapping, Dictionary<StoreObjectId, LocalCalendarGroupEntryInfo> calendarIdToGroupEntryMapping, CalendarGroupInfoList calendarGroups)
		{
			CalendarGroupEntryInfo calendarGroupEntryInfoFromRow = CalendarGroupEntry.GetCalendarGroupEntryInfoFromRow(row);
			if (calendarGroupEntryInfoFromRow == null)
			{
				return;
			}
			CalendarGroupInfo calendarGroupInfo;
			if (!guidToGroupMapping.TryGetValue(calendarGroupEntryInfoFromRow.ParentGroupClassId, out calendarGroupInfo))
			{
				ExTraceGlobals.StorageTracer.TraceDebug<string, Guid, VersionedId>(0L, "Found a calendar group entry with an invalid parent id. CalendarName: {0}, ParentId: {1}, ItemId: {2}", calendarGroupEntryInfoFromRow.CalendarName, calendarGroupEntryInfoFromRow.ParentGroupClassId, calendarGroupEntryInfoFromRow.Id);
				return;
			}
			LocalCalendarGroupEntryInfo localCalendarGroupEntryInfo = calendarGroupEntryInfoFromRow as LocalCalendarGroupEntryInfo;
			if (calendarGroupEntryInfoFromRow != null && calendarGroupEntryInfoFromRow.CalendarId != null && calendarGroupEntryInfoFromRow.CalendarId.IsPublicFolderType())
			{
				foreach (CalendarGroupEntryInfo calendarGroupEntryInfo in calendarGroupInfo.Calendars)
				{
					if (calendarGroupEntryInfo.CalendarId != null && calendarGroupEntryInfo.CalendarId.Equals(calendarGroupEntryInfoFromRow.CalendarId))
					{
						return;
					}
				}
			}
			if (localCalendarGroupEntryInfo != null)
			{
				LocalCalendarGroupEntryInfo localCalendarGroupEntryInfo2;
				if (calendarIdToGroupEntryMapping.TryGetValue(localCalendarGroupEntryInfo.CalendarId, out localCalendarGroupEntryInfo2))
				{
					if (localCalendarGroupEntryInfo2.LastModifiedTime.CompareTo(localCalendarGroupEntryInfo.LastModifiedTime) > 0)
					{
						calendarGroups.DuplicateNodes.Add(localCalendarGroupEntryInfo);
						return;
					}
					calendarGroups.DuplicateNodes.Add(localCalendarGroupEntryInfo2);
					guidToGroupMapping[localCalendarGroupEntryInfo2.ParentGroupClassId].Calendars.Remove(localCalendarGroupEntryInfo2);
					calendarIdToGroupEntryMapping[localCalendarGroupEntryInfo.CalendarId] = localCalendarGroupEntryInfo;
				}
				else
				{
					calendarIdToGroupEntryMapping.Add(localCalendarGroupEntryInfo.CalendarId, localCalendarGroupEntryInfo);
				}
			}
			calendarGroupInfo.Calendars.Add(calendarGroupEntryInfoFromRow);
		}

		// Token: 0x060028D5 RID: 10453 RVA: 0x000A2AC0 File Offset: 0x000A0CC0
		private static void AddGroupToList(IStorePropertyBag row, Dictionary<Guid, CalendarGroupInfo> guidToGroupMapping, CalendarGroupInfoList calendarGroups)
		{
			CalendarGroupInfo calendarGroupInfoFromRow = CalendarGroup.GetCalendarGroupInfoFromRow(row);
			if (calendarGroupInfoFromRow == null)
			{
				return;
			}
			CalendarGroupInfo calendarGroupInfo;
			if (guidToGroupMapping.TryGetValue(calendarGroupInfoFromRow.GroupClassId, out calendarGroupInfo))
			{
				if (calendarGroupInfo.LastModifiedTime.CompareTo(calendarGroupInfoFromRow.LastModifiedTime) > 0)
				{
					calendarGroups.DuplicateNodes.Add(calendarGroupInfoFromRow);
					return;
				}
				guidToGroupMapping[calendarGroupInfoFromRow.GroupClassId] = calendarGroupInfoFromRow;
				calendarGroups.DuplicateNodes.Add(calendarGroupInfo);
				calendarGroups.Remove(calendarGroupInfo);
				if (calendarGroups.DefaultGroups.ContainsKey(calendarGroupInfoFromRow.GroupType))
				{
					calendarGroups.DefaultGroups[calendarGroupInfoFromRow.GroupType] = calendarGroupInfoFromRow;
				}
			}
			else
			{
				guidToGroupMapping.Add(calendarGroupInfoFromRow.GroupClassId, calendarGroupInfoFromRow);
				if (calendarGroupInfoFromRow.GroupType != CalendarGroupType.Normal)
				{
					calendarGroups.DefaultGroups.Add(calendarGroupInfoFromRow.GroupType, calendarGroupInfoFromRow);
				}
			}
			calendarGroups.Add(calendarGroupInfoFromRow);
		}

		// Token: 0x060028D6 RID: 10454 RVA: 0x000A2B84 File Offset: 0x000A0D84
		private static CalendarGroup CreateMyCalendarsGroup(MailboxSession session)
		{
			CalendarGroup calendarGroup = null;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				calendarGroup = CalendarGroup.InternalCreateGroup(session, CalendarGroupType.MyCalendars);
				disposeGuard.Add<CalendarGroup>(calendarGroup);
				using (CalendarGroupEntry calendarGroupEntry = CalendarGroupEntry.Create(session, session.GetDefaultFolderId(DefaultFolderType.Calendar), FolderTreeData.MyFoldersClassId, calendarGroup.GroupName))
				{
					calendarGroupEntry.CalendarName = ClientStrings.Calendar.ToString(session.InternalCulture);
					ConflictResolutionResult conflictResolutionResult = calendarGroupEntry.Save(SaveMode.NoConflictResolution);
					if (conflictResolutionResult.SaveStatus != SaveResult.Success)
					{
						ExTraceGlobals.StorageTracer.TraceWarning<SmtpAddress>(0L, "Unable to associate default calendar with the MyCalendars group for user: {0}. Attempting to delete default calendars group.", session.MailboxOwner.MailboxInfo.PrimarySmtpAddress);
						AggregateOperationResult aggregateOperationResult = session.Delete(DeleteItemFlags.HardDelete, new StoreId[]
						{
							calendarGroup.Id
						});
						if (aggregateOperationResult.OperationResult != OperationResult.Succeeded)
						{
							ExTraceGlobals.StorageTracer.TraceWarning<SmtpAddress>(0L, "Unable to delete default calendar group after failing to add the default calendar to it. User: {0}", session.MailboxOwner.MailboxInfo.PrimarySmtpAddress);
						}
						throw new DefaultCalendarNodeCreationException();
					}
				}
				disposeGuard.Success();
			}
			return calendarGroup;
		}

		// Token: 0x060028D7 RID: 10455 RVA: 0x000A2CA0 File Offset: 0x000A0EA0
		private static CalendarGroup InternalCreateGroup(MailboxSession session, CalendarGroupType groupType)
		{
			CalendarGroup calendarGroup;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				calendarGroup = CalendarGroup.CreateDefaultGroup(groupType, session);
				disposeGuard.Add<CalendarGroup>(calendarGroup);
				ConflictResolutionResult conflictResolutionResult = calendarGroup.Save(SaveMode.NoConflictResolution);
				if (conflictResolutionResult.SaveStatus != SaveResult.Success)
				{
					ExTraceGlobals.StorageTracer.TraceWarning<CalendarGroupType, SmtpAddress>(0L, "Unable to create group of type {0} for user: {1}", groupType, session.MailboxOwner.MailboxInfo.PrimarySmtpAddress);
					throw new DefaultCalendarGroupCreationException(groupType.ToString());
				}
				calendarGroup.Load();
				disposeGuard.Success();
			}
			return calendarGroup;
		}

		// Token: 0x060028D8 RID: 10456 RVA: 0x000A2D38 File Offset: 0x000A0F38
		private static CalendarGroup CreateDefaultGroup(CalendarGroupType groupType, MailboxSession session)
		{
			CalendarGroup calendarGroup = CalendarGroup.Create(session);
			Guid groupGuidFromType = CalendarGroup.GetGroupGuidFromType(groupType);
			string groupName = string.Empty;
			switch (groupType)
			{
			case CalendarGroupType.MyCalendars:
				groupName = ClientStrings.MyCalendars.ToString(session.InternalPreferedCulture);
				break;
			case CalendarGroupType.OtherCalendars:
				groupName = ClientStrings.OtherCalendars.ToString(session.InternalPreferedCulture);
				break;
			case CalendarGroupType.PeoplesCalendars:
				groupName = ClientStrings.PeoplesCalendars.ToString(session.InternalPreferedCulture);
				break;
			}
			calendarGroup.GroupClassId = groupGuidFromType;
			calendarGroup.GroupName = groupName;
			return calendarGroup;
		}

		// Token: 0x060028D9 RID: 10457 RVA: 0x000A2DC4 File Offset: 0x000A0FC4
		private static Guid GetGroupGuidFromType(CalendarGroupType groupType)
		{
			switch (groupType)
			{
			case CalendarGroupType.MyCalendars:
				return FolderTreeData.MyFoldersClassId;
			case CalendarGroupType.OtherCalendars:
				return FolderTreeData.OtherFoldersClassId;
			case CalendarGroupType.PeoplesCalendars:
				return FolderTreeData.PeoplesFoldersClassId;
			default:
				return Guid.Empty;
			}
		}

		// Token: 0x060028DA RID: 10458 RVA: 0x000A2DFE File Offset: 0x000A0FFE
		private static CalendarGroupType GetGroupTypeFromGuid(Guid groupClassId)
		{
			if (groupClassId.Equals(FolderTreeData.MyFoldersClassId))
			{
				return CalendarGroupType.MyCalendars;
			}
			if (groupClassId.Equals(FolderTreeData.OtherFoldersClassId))
			{
				return CalendarGroupType.OtherCalendars;
			}
			if (groupClassId.Equals(FolderTreeData.PeoplesFoldersClassId))
			{
				return CalendarGroupType.PeoplesCalendars;
			}
			return CalendarGroupType.Normal;
		}

		// Token: 0x060028DB RID: 10459 RVA: 0x000A2E34 File Offset: 0x000A1034
		private static CalendarGroupInfo GetCalendarGroupInfoFromRow(IStorePropertyBag row)
		{
			VersionedId id = (VersionedId)row.TryGetProperty(ItemSchema.Id);
			byte[] valueOrDefault = row.GetValueOrDefault<byte[]>(CalendarGroupSchema.GroupClassId, null);
			string valueOrDefault2 = row.GetValueOrDefault<string>(ItemSchema.Subject, string.Empty);
			byte[] valueOrDefault3 = row.GetValueOrDefault<byte[]>(FolderTreeDataSchema.Ordinal, null);
			ExDateTime valueOrDefault4 = row.GetValueOrDefault<ExDateTime>(StoreObjectSchema.LastModifiedTime, ExDateTime.MinValue);
			Guid safeGuidFromByteArray = FolderTreeData.GetSafeGuidFromByteArray(valueOrDefault);
			if (safeGuidFromByteArray.Equals(Guid.Empty))
			{
				ExTraceGlobals.StorageTracer.TraceDebug<int>(0L, "Found calendar group with invalid group class id. ArrayLength: {0}", (valueOrDefault == null) ? -1 : valueOrDefault.Length);
				return null;
			}
			return new CalendarGroupInfo(valueOrDefault2, id, safeGuidFromByteArray, CalendarGroup.GetGroupTypeFromGuid(safeGuidFromByteArray), valueOrDefault3, valueOrDefault4);
		}

		// Token: 0x060028DC RID: 10460 RVA: 0x000A2ED4 File Offset: 0x000A10D4
		internal CalendarGroup(ICoreItem coreItem) : base(coreItem)
		{
			if (base.IsNew)
			{
				this.InitializeNewGroup();
				return;
			}
			this.groupClassId = FolderTreeData.GetSafeGuidFromByteArray(base.GetValueOrDefault<byte[]>(CalendarGroupSchema.GroupClassId));
		}

		// Token: 0x17000D66 RID: 3430
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
				if (propertyDefinition == CalendarGroupSchema.GroupClassId)
				{
					byte[] guid = value as byte[];
					this.groupClassId = FolderTreeData.GetSafeGuidFromByteArray(guid);
				}
			}
		}

		// Token: 0x17000D67 RID: 3431
		// (get) Token: 0x060028DF RID: 10463 RVA: 0x000A2F5F File Offset: 0x000A115F
		public override Schema Schema
		{
			get
			{
				this.CheckDisposed("Schema::get");
				return CalendarGroupSchema.Instance;
			}
		}

		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x060028E0 RID: 10464 RVA: 0x000A2F71 File Offset: 0x000A1171
		// (set) Token: 0x060028E1 RID: 10465 RVA: 0x000A2F84 File Offset: 0x000A1184
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
				this[CalendarGroupSchema.GroupClassId] = value.ToByteArray();
			}
		}

		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x060028E2 RID: 10466 RVA: 0x000A2FA3 File Offset: 0x000A11A3
		// (set) Token: 0x060028E3 RID: 10467 RVA: 0x000A2FBB File Offset: 0x000A11BB
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

		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x060028E4 RID: 10468 RVA: 0x000A2FD4 File Offset: 0x000A11D4
		public CalendarGroupType GroupType
		{
			get
			{
				this.CheckDisposed("GroupType::get");
				return CalendarGroup.GetGroupTypeFromGuid(this.GroupClassId);
			}
		}

		// Token: 0x060028E5 RID: 10469 RVA: 0x000A2FEC File Offset: 0x000A11EC
		public ReadOnlyCollection<CalendarGroupEntryInfo> GetChildCalendars()
		{
			this.CheckDisposed("GetChildCalendars");
			this.LoadChildNodesCollection();
			return new ReadOnlyCollection<CalendarGroupEntryInfo>(this.children);
		}

		// Token: 0x060028E6 RID: 10470 RVA: 0x000A300A File Offset: 0x000A120A
		public CalendarGroupInfo GetCalendarGroupInfo()
		{
			return CalendarGroup.GetCalendarGroupInfoFromRow(this);
		}

		// Token: 0x060028E7 RID: 10471 RVA: 0x000A3050 File Offset: 0x000A1250
		public CalendarGroupEntryInfo FindSharedGSCalendaryEntry(string sharerLegacyDN)
		{
			this.CheckDisposed("FindSharedGSCalendaryEntry");
			Util.ThrowOnNullArgument("sharerLegacyDN", sharerLegacyDN);
			return this.children.FirstOrDefault(delegate(CalendarGroupEntryInfo folder)
			{
				LinkedCalendarGroupEntryInfo linkedCalendarGroupEntryInfo = folder as LinkedCalendarGroupEntryInfo;
				return linkedCalendarGroupEntryInfo != null && linkedCalendarGroupEntryInfo.IsGeneralScheduleCalendar && string.Equals(linkedCalendarGroupEntryInfo.CalendarOwner, sharerLegacyDN, StringComparison.OrdinalIgnoreCase);
			});
		}

		// Token: 0x060028E8 RID: 10472 RVA: 0x000A30C4 File Offset: 0x000A12C4
		public CalendarGroupEntryInfo FindSharedCalendaryEntry(StoreObjectId folderId)
		{
			this.CheckDisposed("FindSharedCalendaryEntry");
			Util.ThrowOnNullArgument(folderId, "folderId");
			return this.children.FirstOrDefault((CalendarGroupEntryInfo folder) => folder.CalendarId != null && folder.CalendarId.Equals(folderId));
		}

		// Token: 0x060028E9 RID: 10473 RVA: 0x000A312C File Offset: 0x000A132C
		protected override void OnBeforeSave()
		{
			base.OnBeforeSave();
			if (base.IsNew)
			{
				bool flag;
				byte[] nodeBefore = FolderTreeData.GetOrdinalValueOfFirstMatchingNode(base.MailboxSession, CalendarGroup.FindLastGroupOrdinalSortOrder, (IStorePropertyBag row) => CalendarGroup.IsFolderTreeData(row) && CalendarGroup.IsCalendarSection(row) && CalendarGroup.IsCalendarGroup(row), CalendarGroup.CalendarInfoProperties, out flag);
				if (flag && !FolderTreeData.MyFoldersClassId.Equals(this.GroupClassId))
				{
					using (CalendarGroup calendarGroup = CalendarGroup.CreateMyCalendarsGroup(base.MailboxSession))
					{
						nodeBefore = calendarGroup.NodeOrdinal;
					}
				}
				base.SetNodeOrdinalInternal(nodeBefore, null);
			}
		}

		// Token: 0x060028EA RID: 10474 RVA: 0x000A31CC File Offset: 0x000A13CC
		private void InitializeNewGroup()
		{
			this[StoreObjectSchema.ItemClass] = "IPM.Microsoft.WunderBar.Link";
			this[FolderTreeDataSchema.Type] = FolderTreeDataType.Header;
			this[FolderTreeDataSchema.FolderTreeDataFlags] = 0;
			this[FolderTreeDataSchema.GroupSection] = FolderTreeDataSection.Calendar;
			this[CalendarGroupSchema.GroupClassId] = Guid.NewGuid().ToByteArray();
			this[FolderTreeDataSchema.ClassId] = CalendarGroup.CalendarSectionClassId.ToByteArray();
		}

		// Token: 0x060028EB RID: 10475 RVA: 0x000A324C File Offset: 0x000A144C
		private void LoadChildNodesCollection()
		{
			if (base.IsNew || this.hasLoadedCalendarsCollection)
			{
				return;
			}
			using (Folder folder = Folder.Bind(base.MailboxSession, DefaultFolderType.CommonViews))
			{
				using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.Associated, null, CalendarGroup.CalendarGroupViewSortOrder, CalendarGroup.CalendarInfoProperties))
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

		// Token: 0x060028EC RID: 10476 RVA: 0x000A32DC File Offset: 0x000A14DC
		private void LoadChildNodesCollection(IStorePropertyBag[] rows, int startIndex)
		{
			for (int i = startIndex; i < rows.Length; i++)
			{
				IStorePropertyBag storePropertyBag = rows[i];
				if (CalendarGroup.IsCalendarSection(storePropertyBag) && CalendarGroup.IsFolderTreeData(storePropertyBag) && CalendarGroup.IsCalendarGroupEntry(storePropertyBag))
				{
					byte[] valueOrDefault = storePropertyBag.GetValueOrDefault<byte[]>(FolderTreeDataSchema.ParentGroupClassId, null);
					if (valueOrDefault == null || valueOrDefault.Length != 16)
					{
						ExTraceGlobals.StorageTracer.TraceDebug<int>(0L, "Found CalendarGroupEntry with invalid parent group id. ArrayLength: {0}", (valueOrDefault == null) ? -1 : valueOrDefault.Length);
					}
					else
					{
						Guid g = new Guid(valueOrDefault);
						if (this.groupClassId.Equals(g))
						{
							CalendarGroupEntryInfo calendarGroupEntryInfoFromRow = CalendarGroupEntry.GetCalendarGroupEntryInfoFromRow(storePropertyBag);
							if (calendarGroupEntryInfoFromRow != null)
							{
								this.children.Add(calendarGroupEntryInfoFromRow);
							}
						}
					}
				}
			}
			this.hasLoadedCalendarsCollection = true;
		}

		// Token: 0x040017D6 RID: 6102
		internal static readonly Guid CalendarSectionClassId = new Guid("{00067802-0000-0000-c000-000000000046}");

		// Token: 0x040017D7 RID: 6103
		private static readonly QueryFilter CalendarSectionFilter = new ComparisonFilter(ComparisonOperator.Equal, FolderTreeDataSchema.GroupSection, FolderTreeDataSection.Calendar);

		// Token: 0x040017D8 RID: 6104
		private static readonly SortBy[] CalendarGroupViewSortOrder = new SortBy[]
		{
			new SortBy(FolderTreeDataSchema.GroupSection, SortOrder.Ascending),
			new SortBy(StoreObjectSchema.ItemClass, SortOrder.Ascending),
			new SortBy(FolderTreeDataSchema.ParentGroupClassId, SortOrder.Ascending),
			new SortBy(FolderTreeDataSchema.Ordinal, SortOrder.Ascending)
		};

		// Token: 0x040017D9 RID: 6105
		private static readonly SortBy[] FindLastGroupOrdinalSortOrder = new SortBy[]
		{
			new SortBy(StoreObjectSchema.ItemClass, SortOrder.Ascending),
			new SortBy(FolderTreeDataSchema.GroupSection, SortOrder.Ascending),
			new SortBy(FolderTreeDataSchema.Type, SortOrder.Descending),
			new SortBy(FolderTreeDataSchema.Ordinal, SortOrder.Descending)
		};

		// Token: 0x040017DA RID: 6106
		internal static readonly PropertyDefinition[] CalendarInfoProperties = new PropertyDefinition[]
		{
			ItemSchema.Id,
			StoreObjectSchema.LastModifiedTime,
			StoreObjectSchema.ItemClass,
			ItemSchema.Subject,
			FolderTreeDataSchema.GroupSection,
			CalendarGroupSchema.GroupClassId,
			FolderTreeDataSchema.Type,
			FolderTreeDataSchema.Ordinal,
			FolderTreeDataSchema.ParentGroupClassId,
			CalendarGroupEntrySchema.NodeEntryId,
			CalendarGroupEntrySchema.CalendarColor,
			CalendarGroupEntrySchema.SharerAddressBookEntryId,
			CalendarGroupEntrySchema.StoreEntryId,
			FolderTreeDataSchema.FolderTreeDataFlags
		};

		// Token: 0x040017DB RID: 6107
		private readonly List<CalendarGroupEntryInfo> children = new List<CalendarGroupEntryInfo>();

		// Token: 0x040017DC RID: 6108
		private Guid groupClassId;

		// Token: 0x040017DD RID: 6109
		private bool hasLoadedCalendarsCollection;
	}
}

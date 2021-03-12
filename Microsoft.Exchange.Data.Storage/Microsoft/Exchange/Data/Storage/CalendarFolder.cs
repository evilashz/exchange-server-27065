using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200038F RID: 911
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CalendarFolder : Folder, ICalendarFolder, IFolder, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x0600280E RID: 10254 RVA: 0x0009F4EF File Offset: 0x0009D6EF
		internal CalendarFolder(CoreFolder coreFolder) : base(coreFolder)
		{
		}

		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x0600280F RID: 10255 RVA: 0x0009F4F8 File Offset: 0x0009D6F8
		public override Schema Schema
		{
			get
			{
				this.CheckDisposed("Schema::get");
				return CalendarFolderSchema.Instance;
			}
		}

		// Token: 0x06002810 RID: 10256 RVA: 0x0009F50A File Offset: 0x0009D70A
		public static bool IsCrossOrgShareFolder(int extendedFolderFlags)
		{
			return Folder.ExtendedFlagsContains(new int?(extendedFolderFlags), ExtendedFolderFlags.ExchangeCrossOrgShareFolder);
		}

		// Token: 0x06002811 RID: 10257 RVA: 0x0009F51C File Offset: 0x0009D71C
		public static bool IsConsumerShareFolder(int extendedFolderFlags)
		{
			return Folder.ExtendedFlagsContains(new int?(extendedFolderFlags), ExtendedFolderFlags.ExchangeConsumerShareFolder);
		}

		// Token: 0x06002812 RID: 10258 RVA: 0x0009F52E File Offset: 0x0009D72E
		public static bool IsInternetCalendar(int extendedFolderFlags)
		{
			return Folder.ExtendedFlagsContains(new int?(extendedFolderFlags), ExtendedFolderFlags.WebCalFolder);
		}

		// Token: 0x06002813 RID: 10259 RVA: 0x0009F540 File Offset: 0x0009D740
		public static bool IsInWindow(ExDateTime startView, ExDateTime endView, ExDateTime startTime, ExDateTime endTime)
		{
			return (endTime >= startView && startTime < endView) || startTime == startView;
		}

		// Token: 0x17000D3E RID: 3390
		// (get) Token: 0x06002814 RID: 10260 RVA: 0x0009F55D File Offset: 0x0009D75D
		// (set) Token: 0x06002815 RID: 10261 RVA: 0x0009F57A File Offset: 0x0009D77A
		public bool IsHidden
		{
			get
			{
				this.CheckDisposed("IsHidden");
				return (bool)this[FolderSchema.IsHidden];
			}
			set
			{
				this.CheckDisposed("IsHidden");
				this[FolderSchema.IsHidden] = value;
			}
		}

		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x06002816 RID: 10262 RVA: 0x0009F598 File Offset: 0x0009D798
		public bool IsInternetCalendarFolder
		{
			get
			{
				this.CheckDisposed("IsInternetCalendarFolder::get");
				return base.ExtendedFlagsContains(ExtendedFolderFlags.WebCalFolder);
			}
		}

		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x06002817 RID: 10263 RVA: 0x0009F5B0 File Offset: 0x0009D7B0
		public bool IsExchangePublishedCalendar
		{
			get
			{
				return base.ExtendedFlagsContains(ExtendedFolderFlags.ExchangePublishedCalendar);
			}
		}

		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x06002818 RID: 10264 RVA: 0x0009F5BD File Offset: 0x0009D7BD
		// (set) Token: 0x06002819 RID: 10265 RVA: 0x0009F5CF File Offset: 0x0009D7CF
		public Guid ConsumerCalendarGuid
		{
			get
			{
				return base.GetValueOrDefault<Guid>(CalendarFolderSchema.ConsumerCalendarGuid, Guid.Empty);
			}
			set
			{
				this[CalendarFolderSchema.ConsumerCalendarGuid] = value;
			}
		}

		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x0600281A RID: 10266 RVA: 0x0009F5E2 File Offset: 0x0009D7E2
		// (set) Token: 0x0600281B RID: 10267 RVA: 0x0009F5F1 File Offset: 0x0009D7F1
		public long ConsumerCalendarOwnerId
		{
			get
			{
				return base.GetValueOrDefault<long>(CalendarFolderSchema.ConsumerCalendarOwnerId, 0L);
			}
			set
			{
				this[CalendarFolderSchema.ConsumerCalendarOwnerId] = value;
			}
		}

		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x0600281C RID: 10268 RVA: 0x0009F604 File Offset: 0x0009D804
		// (set) Token: 0x0600281D RID: 10269 RVA: 0x0009F616 File Offset: 0x0009D816
		public Guid ConsumerCalendarPrivateFreeBusyId
		{
			get
			{
				return base.GetValueOrDefault<Guid>(CalendarFolderSchema.ConsumerCalendarPrivateFreeBusyId, Guid.Empty);
			}
			set
			{
				this[CalendarFolderSchema.ConsumerCalendarPrivateFreeBusyId] = value;
			}
		}

		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x0600281E RID: 10270 RVA: 0x0009F629 File Offset: 0x0009D829
		// (set) Token: 0x0600281F RID: 10271 RVA: 0x0009F63B File Offset: 0x0009D83B
		public Guid ConsumerCalendarPrivateDetailId
		{
			get
			{
				return base.GetValueOrDefault<Guid>(CalendarFolderSchema.ConsumerCalendarPrivateDetailId, Guid.Empty);
			}
			set
			{
				this[CalendarFolderSchema.ConsumerCalendarPrivateDetailId] = value;
			}
		}

		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x06002820 RID: 10272 RVA: 0x0009F64E File Offset: 0x0009D84E
		// (set) Token: 0x06002821 RID: 10273 RVA: 0x0009F65C File Offset: 0x0009D85C
		public PublishVisibility ConsumerCalendarPublishVisibility
		{
			get
			{
				return base.GetValueOrDefault<PublishVisibility>(CalendarFolderSchema.ConsumerCalendarPublishVisibility, PublishVisibility.None);
			}
			set
			{
				this[CalendarFolderSchema.ConsumerCalendarPublishVisibility] = value;
			}
		}

		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x06002822 RID: 10274 RVA: 0x0009F66F File Offset: 0x0009D86F
		// (set) Token: 0x06002823 RID: 10275 RVA: 0x0009F681 File Offset: 0x0009D881
		public string ConsumerCalendarSharingInvitations
		{
			get
			{
				return base.GetValueOrDefault<string>(CalendarFolderSchema.ConsumerCalendarSharingInvitations, string.Empty);
			}
			set
			{
				this[CalendarFolderSchema.ConsumerCalendarSharingInvitations] = value;
			}
		}

		// Token: 0x17000D47 RID: 3399
		// (get) Token: 0x06002824 RID: 10276 RVA: 0x0009F68F File Offset: 0x0009D88F
		// (set) Token: 0x06002825 RID: 10277 RVA: 0x0009F69D File Offset: 0x0009D89D
		public SharingPermissionLevel ConsumerCalendarPermissionLevel
		{
			get
			{
				return base.GetValueOrDefault<SharingPermissionLevel>(CalendarFolderSchema.ConsumerCalendarPermissionLevel, SharingPermissionLevel.FreeBusy);
			}
			set
			{
				this[CalendarFolderSchema.ConsumerCalendarPermissionLevel] = value;
			}
		}

		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x06002826 RID: 10278 RVA: 0x0009F6B0 File Offset: 0x0009D8B0
		// (set) Token: 0x06002827 RID: 10279 RVA: 0x0009F6C2 File Offset: 0x0009D8C2
		public string ConsumerCalendarSynchronizationState
		{
			get
			{
				return base.GetValueOrDefault<string>(CalendarFolderSchema.ConsumerCalendarSynchronizationState, string.Empty);
			}
			set
			{
				this[CalendarFolderSchema.ConsumerCalendarSynchronizationState] = value;
			}
		}

		// Token: 0x06002828 RID: 10280 RVA: 0x0009F6D0 File Offset: 0x0009D8D0
		public new static CalendarFolder Bind(StoreSession session, StoreId folderId)
		{
			return CalendarFolder.Bind(session, folderId, null);
		}

		// Token: 0x06002829 RID: 10281 RVA: 0x0009F6DA File Offset: 0x0009D8DA
		public new static CalendarFolder Bind(StoreSession session, StoreId folderId, ICollection<PropertyDefinition> propsToReturn)
		{
			if (propsToReturn == null || propsToReturn.Count == 0)
			{
				propsToReturn = CalendarFolderSchema.Instance.AutoloadProperties;
			}
			return Folder.InternalBind<CalendarFolder>(session, folderId, propsToReturn);
		}

		// Token: 0x0600282A RID: 10282 RVA: 0x0009F6FB File Offset: 0x0009D8FB
		public static CalendarFolder Bind(MailboxSession session, DefaultFolderType defaultFolderType)
		{
			return CalendarFolder.Bind(session, defaultFolderType, null);
		}

		// Token: 0x0600282B RID: 10283 RVA: 0x0009F708 File Offset: 0x0009D908
		public static CalendarFolder Bind(MailboxSession session, DefaultFolderType defaultFolderType, ICollection<PropertyDefinition> propsToReturn)
		{
			EnumValidator.ThrowIfInvalid<DefaultFolderType>(defaultFolderType, new DefaultFolderType[]
			{
				DefaultFolderType.Calendar,
				DefaultFolderType.BirthdayCalendar
			});
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			StoreObjectId defaultFolderId = session.GetDefaultFolderId(defaultFolderType);
			if (defaultFolderId == null)
			{
				throw new ObjectNotFoundException(ServerStrings.ExDefaultFolderNotFound(defaultFolderType));
			}
			return CalendarFolder.Bind(session, defaultFolderId, propsToReturn);
		}

		// Token: 0x0600282C RID: 10284 RVA: 0x0009F75E File Offset: 0x0009D95E
		public static CalendarFolder Create(StoreSession session, StoreId parentFolderId)
		{
			return (CalendarFolder)Folder.Create(session, parentFolderId, StoreObjectType.CalendarFolder);
		}

		// Token: 0x0600282D RID: 10285 RVA: 0x0009F76D File Offset: 0x0009D96D
		public new static CalendarFolder Create(StoreSession session, StoreId parentFolderId, StoreObjectType folderType)
		{
			EnumValidator.ThrowIfInvalid<StoreObjectType>(folderType, StoreObjectType.CalendarFolder);
			return (CalendarFolder)Folder.Create(session, parentFolderId, folderType);
		}

		// Token: 0x0600282E RID: 10286 RVA: 0x0009F783 File Offset: 0x0009D983
		public new static CalendarFolder Create(StoreSession session, StoreId parentFolderId, StoreObjectType folderType, string displayName, CreateMode createMode)
		{
			EnumValidator.ThrowIfInvalid<CreateMode>(createMode, "createMode");
			EnumValidator.ThrowIfInvalid<StoreObjectType>(folderType, StoreObjectType.CalendarFolder);
			return (CalendarFolder)Folder.Create(session, parentFolderId, StoreObjectType.CalendarFolder, displayName, createMode);
		}

		// Token: 0x0600282F RID: 10287 RVA: 0x0009F7A8 File Offset: 0x0009D9A8
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<CalendarFolder>(this);
		}

		// Token: 0x06002830 RID: 10288 RVA: 0x0009F7B0 File Offset: 0x0009D9B0
		public object[][] GetCalendarView(ExDateTime startTime, ExDateTime endTime, params PropertyDefinition[] dataColumns)
		{
			CalendarViewLatencyInformation calendarViewLatencyInformation = new CalendarViewLatencyInformation();
			return this.GetCalendarView(startTime, endTime, calendarViewLatencyInformation, dataColumns);
		}

		// Token: 0x06002831 RID: 10289 RVA: 0x0009F7CD File Offset: 0x0009D9CD
		public object[][] GetCalendarView(ExDateTime startTime, ExDateTime endTime, CalendarViewLatencyInformation calendarViewLatencyInformation, params PropertyDefinition[] dataColumns)
		{
			return this.InternalGetCalendarView(startTime, endTime, true, RecurrenceExpansionOption.IncludeRegularOccurrences, calendarViewLatencyInformation, dataColumns);
		}

		// Token: 0x06002832 RID: 10290 RVA: 0x0009F7DC File Offset: 0x0009D9DC
		public object[][] GetSyncView(ExDateTime startTime, ExDateTime endTime, CalendarViewBatchingStrategy batchingStrategy, PropertyDefinition[] dataColumns, bool includeNprMasters)
		{
			if (startTime > endTime)
			{
				throw new ArgumentOutOfRangeException("startTime", startTime, string.Format("startTime ({0}) should be less than or equal to endTime ({1}).", startTime, endTime));
			}
			Util.ThrowOnNullArgument(batchingStrategy, "batchingStrategy");
			Util.ThrowOnNullOrEmptyArgument(dataColumns, "dataColumns");
			ExTraceGlobals.StorageTracer.TraceDebug<int, ExDateTime, ExDateTime>((long)this.GetHashCode(), "CalendarFolder::GetSyncView. HashCode = {0}, startTime={1}, endTime={2}", this.GetHashCode(), startTime, endTime);
			object[][] array;
			if (batchingStrategy.ShouldQuerySingleInstanceMeetings)
			{
				List<object[]> singleCalendarItems = this.GetSingleCalendarItems(startTime, endTime, dataColumns, includeNprMasters, batchingStrategy);
				if (batchingStrategy.ReachedBatchSizeLimit)
				{
					array = singleCalendarItems.ToArray();
				}
				else
				{
					List<object[]> recurringCalendarItems = this.GetRecurringCalendarItems(startTime, endTime, dataColumns, RecurrenceExpansionOption.IncludeAll, batchingStrategy);
					array = new object[singleCalendarItems.Count + recurringCalendarItems.Count][];
					singleCalendarItems.CopyTo(array);
					recurringCalendarItems.CopyTo(array, singleCalendarItems.Count);
				}
			}
			else
			{
				List<object[]> recurringCalendarItems = this.GetRecurringCalendarItems(startTime, endTime, dataColumns, RecurrenceExpansionOption.IncludeAll, batchingStrategy);
				array = recurringCalendarItems.ToArray();
			}
			return array;
		}

		// Token: 0x06002833 RID: 10291 RVA: 0x0009F8C0 File Offset: 0x0009DAC0
		public AdjacencyOrConflictInfo[] GetAdjacentOrConflictingItems(CalendarItemBase calendarItemBase)
		{
			return this.GetAdjacentOrConflictingItems(calendarItemBase, null);
		}

		// Token: 0x06002834 RID: 10292 RVA: 0x0009F8E0 File Offset: 0x0009DAE0
		public AdjacencyOrConflictInfo[] GetAdjacentOrConflictingItems(CalendarItemBase calendarItemBase, ExDateTime? expansionLimit)
		{
			this.CheckDisposed("GetAdjacenctOrConflictingItems");
			PropertyDefinition[] dataColumns = new PropertyDefinition[]
			{
				InternalSchema.ItemId,
				InternalSchema.GlobalObjectId,
				InternalSchema.Subject,
				InternalSchema.Location,
				InternalSchema.FreeBusyStatus,
				ItemSchema.Sensitivity,
				CalendarItemInstanceSchema.StartTime,
				CalendarItemInstanceSchema.EndTime,
				CalendarItemBaseSchema.IsAllDayEvent,
				InternalSchema.AppointmentRecurrenceBlob,
				InternalSchema.TimeZoneBlob,
				InternalSchema.TimeZone,
				InternalSchema.TimeZoneDefinitionRecurring,
				InternalSchema.Codepage
			};
			PropertyDefinition[] dataColumns2 = new PropertyDefinition[]
			{
				InternalSchema.ItemId,
				InternalSchema.GlobalObjectId,
				InternalSchema.Subject,
				InternalSchema.Location,
				InternalSchema.FreeBusyStatus,
				ItemSchema.Sensitivity,
				InternalSchema.StartTime,
				InternalSchema.EndTime,
				CalendarItemBaseSchema.IsAllDayEvent
			};
			List<Pair<ExDateTime, ExDateTime>> appointmentTimes = this.GetAppointmentTimes(calendarItemBase, expansionLimit);
			if (appointmentTimes.Count == 0)
			{
				return Array<AdjacencyOrConflictInfo>.Empty;
			}
			Dictionary<VersionedId, AdjacencyOrConflictInfo> dictionary = new Dictionary<VersionedId, AdjacencyOrConflictInfo>();
			ExDateTime first = appointmentTimes[0].First;
			ExDateTime viewEndDate = this.GetViewEndDate(appointmentTimes, expansionLimit);
			List<object[]> singleCalendarItems = this.GetSingleCalendarItems(first, viewEndDate, dataColumns2, false, null);
			this.ProcessQueryResults(calendarItemBase, appointmentTimes, singleCalendarItems, true, dictionary);
			List<object[]> recurringCalendarItems = this.GetRecurringCalendarItems(first, viewEndDate, dataColumns, RecurrenceExpansionOption.IncludeRegularOccurrences, null);
			this.ProcessQueryResults(calendarItemBase, appointmentTimes, recurringCalendarItems, false, dictionary);
			AdjacencyOrConflictInfo[] array = Util.CollectionToArray<AdjacencyOrConflictInfo>(dictionary.Values);
			CalendarFolder.AdjacencyOrConflictInfoStartTimeComparer comparer = new CalendarFolder.AdjacencyOrConflictInfoStartTimeComparer();
			Array.Sort<AdjacencyOrConflictInfo>(array, comparer);
			return array;
		}

		// Token: 0x06002835 RID: 10293 RVA: 0x0009FA70 File Offset: 0x0009DC70
		private ExDateTime GetViewEndDate(List<Pair<ExDateTime, ExDateTime>> appointmentTimes, ExDateTime? expansionLimit)
		{
			ExDateTime exDateTime = expansionLimit ?? appointmentTimes[0].First.IncrementMonths(6).AddDays(1.0);
			ExDateTime exDateTime2 = appointmentTimes[appointmentTimes.Count - 1].Second.AddSeconds(1.0);
			return (exDateTime2 > exDateTime) ? exDateTime : exDateTime2;
		}

		// Token: 0x06002836 RID: 10294 RVA: 0x0009FAF0 File Offset: 0x0009DCF0
		private void ProcessQueryResults(CalendarItemBase calendarItemBase, List<Pair<ExDateTime, ExDateTime>> appointmentTimes, List<object[]> results, bool useStartTimeAsOriginalStartTime, Dictionary<VersionedId, AdjacencyOrConflictInfo> cachedResults)
		{
			foreach (object[] array in results)
			{
				if (!CalendarFolder.IsRowMatchingCalendarItem(calendarItemBase, array))
				{
					foreach (Pair<ExDateTime, ExDateTime> startEndTime in appointmentTimes)
					{
						AdjacencyOrConflictInfo adjacencyOrConflictInfo = CalendarFolder.GetAdjacencyOrConflictInfo(array, startEndTime, useStartTimeAsOriginalStartTime, cachedResults);
						if (adjacencyOrConflictInfo != null)
						{
							VersionedId key = (VersionedId)array[0];
							cachedResults[key] = adjacencyOrConflictInfo;
						}
					}
				}
			}
		}

		// Token: 0x06002837 RID: 10295 RVA: 0x0009FB9C File Offset: 0x0009DD9C
		public new CalendarFolderPermissionSet GetPermissionSet()
		{
			this.CheckDisposed("GetPermissionSet");
			return (CalendarFolderPermissionSet)base.GetPermissionSet();
		}

		// Token: 0x06002838 RID: 10296 RVA: 0x0009FBB4 File Offset: 0x0009DDB4
		public PropertyBag GetCalendarItemProperties(byte[] globalObjectIdBytes, ICollection<PropertyDefinition> additionalProperties = null)
		{
			IEnumerable<VersionedId> enumerable = null;
			return this.GetCalendarItemProperties(globalObjectIdBytes, false, out enumerable, additionalProperties);
		}

		// Token: 0x06002839 RID: 10297 RVA: 0x0009FBD0 File Offset: 0x0009DDD0
		public PropertyBag GetCalendarItemProperties(byte[] globalObjectIdBytes, bool shouldDetectDuplicateIds, out IEnumerable<VersionedId> detectedDuplicatesIds, ICollection<PropertyDefinition> additionalProperties = null)
		{
			new GlobalObjectId(globalObjectIdBytes);
			CalendarCorrelationMatch calendarCorrelationMatch = this.GetCalendarCorrelationMatch(globalObjectIdBytes, shouldDetectDuplicateIds, additionalProperties, out detectedDuplicatesIds);
			if (calendarCorrelationMatch != null)
			{
				return calendarCorrelationMatch.Properties;
			}
			return null;
		}

		// Token: 0x0600283A RID: 10298 RVA: 0x0009FBFC File Offset: 0x0009DDFC
		public VersionedId GetCalendarItemId(byte[] globalObjectIdBytes)
		{
			IEnumerable<VersionedId> enumerable = null;
			return this.GetCalendarItemId(globalObjectIdBytes, false, out enumerable);
		}

		// Token: 0x0600283B RID: 10299 RVA: 0x0009FC18 File Offset: 0x0009DE18
		public VersionedId GetCalendarItemId(byte[] globalObjectIdBytes, bool shouldDetectDuplicateIds, out IEnumerable<VersionedId> detectedDuplicatesIds)
		{
			GlobalObjectId globalObjectId = new GlobalObjectId(globalObjectIdBytes);
			CalendarCorrelationMatch calendarCorrelationMatch = this.GetCalendarCorrelationMatch(globalObjectIdBytes, shouldDetectDuplicateIds, null, out detectedDuplicatesIds);
			if (calendarCorrelationMatch == null)
			{
				return null;
			}
			return calendarCorrelationMatch.GetCorrelatedId(globalObjectId);
		}

		// Token: 0x0600283C RID: 10300 RVA: 0x0009FC78 File Offset: 0x0009DE78
		public CalendarItem ImportICAL(Stream inputStream, string charset, InboundConversionOptions options)
		{
			CalendarItem calendarItem = CalendarItem.Create(base.Session, base.StoreObjectId);
			LocalizedString errorMessage = LocalizedString.Empty;
			InboundAddressCache addressCache = new InboundAddressCache(options, new ConversionLimitsTracker(options.Limits), MimeMessageLevel.TopLevelMessage);
			bool succeeded = false;
			ConvertUtils.CallCts(ExTraceGlobals.ICalTracer, "CalendarFolder::ImportICAL", ServerStrings.ConversionCorruptContent, delegate
			{
				succeeded = CalendarDocument.ICalToItem(inputStream, calendarItem, addressCache, false, charset, out errorMessage);
			});
			if (!succeeded)
			{
				throw new ConversionFailedException(ConversionFailureReason.CorruptContent, errorMessage, null);
			}
			addressCache.Resolve();
			addressCache.CopyDataToItem(calendarItem.CoreItem);
			calendarItem.PropertyBag.ExTimeZone = base.Session.ExTimeZone;
			return calendarItem;
		}

		// Token: 0x0600283D RID: 10301 RVA: 0x0009FD54 File Offset: 0x0009DF54
		internal object[][] InternalGetCalendarView(ExDateTime startTime, ExDateTime endTime, bool busyStatusOnly, bool filterPrivateItems, bool sortByTime, RecurrenceExpansionOption expansionOptions, params PropertyDefinition[] dataColumns)
		{
			CalendarViewLatencyInformation calendarViewLatencyInformation = new CalendarViewLatencyInformation();
			return this.InternalGetCalendarView(startTime, endTime, busyStatusOnly, filterPrivateItems, sortByTime, expansionOptions, calendarViewLatencyInformation, dataColumns);
		}

		// Token: 0x0600283E RID: 10302 RVA: 0x0009FD7C File Offset: 0x0009DF7C
		internal object[][] InternalGetCalendarView(ExDateTime startTime, ExDateTime endTime, bool sortByTime, RecurrenceExpansionOption expansionOptions, CalendarViewLatencyInformation calendarViewLatencyInformation, PropertyDefinition[] dataColumns)
		{
			this.CheckDisposed("GetCalendarView");
			ExTraceGlobals.RecurrenceTracer.Information<int, ExDateTime, ExDateTime>((long)this.GetHashCode(), "CalendarFolder::GetCalendarView. HashCode = {0}, startTime={1}, endTime={2}", this.GetHashCode(), startTime, endTime);
			this.Sync();
			if (startTime < ExDateTime.MaxValue.IncrementYears(-2) && endTime > startTime.IncrementYears(2))
			{
				throw new ArgumentOutOfRangeException(ServerStrings.ExInvalidDateTimeRange((DateTime)startTime, (DateTime)endTime));
			}
			MailboxSession mailboxSession = base.Session as MailboxSession;
			bool filterPrivateItems = mailboxSession != null && mailboxSession.FilterPrivateItems;
			return this.InternalGetCalendarView(startTime, endTime, false, filterPrivateItems, sortByTime, expansionOptions, calendarViewLatencyInformation, dataColumns);
		}

		// Token: 0x0600283F RID: 10303 RVA: 0x0009FE30 File Offset: 0x0009E030
		internal object[][] InternalGetCalendarView(ExDateTime startTime, ExDateTime endTime, bool busyStatusOnly, bool filterPrivateItems, bool sortByTime, RecurrenceExpansionOption expansionOptions, CalendarViewLatencyInformation calendarViewLatencyInformation, params PropertyDefinition[] dataColumns)
		{
			int num = Array.IndexOf<PropertyDefinition>(dataColumns, InternalSchema.FreeBusyStatus);
			int num2 = Array.IndexOf<PropertyDefinition>(dataColumns, InternalSchema.Subject);
			if (busyStatusOnly && (num < 0 || num2 < 0))
			{
				throw new ArgumentException("busyStatusOnly can not be TRUE if FreeBusyStatus and Subject are not requested data column");
			}
			if (startTime > endTime)
			{
				throw new ArgumentException(ServerStrings.ExInvalidDateTimeRange((DateTime)startTime, (DateTime)endTime));
			}
			if (startTime.Year > 4500)
			{
				return Array<object[]>.Empty;
			}
			Stopwatch stopwatch = Stopwatch.StartNew();
			object[][] result;
			try
			{
				this.ReconfigureLocationProperties(dataColumns);
				ColumnAdjuster columnAdjuster = new ColumnAdjuster(InternalSchema.StartTime);
				ColumnAdjuster columnAdjuster2 = new ColumnAdjuster(InternalSchema.MapiSensitivity);
				PropertyDefinition[] dataColumns2 = ColumnAdjuster.Adjust(dataColumns, new ColumnAdjuster[]
				{
					columnAdjuster,
					columnAdjuster2
				});
				int num3 = base.GetValueOrDefault<int>(FolderSchema.CalendarFolderVersion, -1);
				ExTraceGlobals.MeetingMessageTracer.TraceDebug<int>(0L, "CalendarFolderVersion:{0}", num3);
				if (num3 > 0)
				{
					if (this.IsNewCalendarViewEnabled())
					{
						ExTraceGlobals.MeetingMessageTracer.TraceDebug(0L, "CalendarView flight is enabled");
					}
					else
					{
						ExTraceGlobals.MeetingMessageTracer.TraceDebug(0L, "CalendarView flight is disabled. Using legacy view although the calendar folder has been upgraded.");
						num3 = -1;
					}
				}
				object[][] array;
				if (num3 == -1)
				{
					ExTraceGlobals.MeetingMessageTracer.TraceDebug(0L, "CalendarFolder using the legacy view");
					calendarViewLatencyInformation.IsNewView = false;
					array = this.GetLegacyCalendarView(startTime, endTime, dataColumns2, expansionOptions, calendarViewLatencyInformation);
				}
				else
				{
					ExTraceGlobals.MeetingMessageTracer.TraceDebug(0L, "CalendarFolder using the View start time");
					calendarViewLatencyInformation.IsNewView = true;
					array = this.GetCalendarViewFromViewProperties(startTime, endTime, dataColumns2, expansionOptions, calendarViewLatencyInformation, null);
				}
				if (sortByTime)
				{
					Array.Sort(array, new CalendarFolder.SortByTimeColumnComparer(columnAdjuster.Index));
				}
				if (busyStatusOnly || filterPrivateItems)
				{
					this.ModifySubjectLocationAndPrivateItems(busyStatusOnly, filterPrivateItems, array, dataColumns, num, num2, columnAdjuster2.Index);
				}
				CalendarFolder.LimitColumns(array, dataColumns.Length);
				result = array;
			}
			finally
			{
				stopwatch.Stop();
				calendarViewLatencyInformation.ViewTime = stopwatch.ElapsedMilliseconds;
			}
			return result;
		}

		// Token: 0x06002840 RID: 10304 RVA: 0x000A001C File Offset: 0x0009E21C
		internal override PermissionSet CreatePermissionSet(PermissionTable permissionTable)
		{
			return new CalendarFolderPermissionSet(permissionTable);
		}

		// Token: 0x06002841 RID: 10305 RVA: 0x000A0024 File Offset: 0x0009E224
		protected override void InternalSavePermissions(PermissionTable permissionsTable)
		{
			this.SaveFreebusyDataPermissions(permissionsTable);
			base.InternalSavePermissions(permissionsTable);
		}

		// Token: 0x06002842 RID: 10306 RVA: 0x000A0034 File Offset: 0x0009E234
		private List<object[]> GetInstancesFromMasterRow(ExDateTime startTime, ExDateTime endTime, object[] masterRow, QueryResultPropertyBag masterPropertyBag, PropertyDefinition[] queriedColumns, NativeStorePropertyDefinition[] nativeColumns, RecurrenceExpansionOption expansionOption, RecurringItemLatencyInformation latencyInformation, CalendarViewBatchingStrategy batchingStrategy)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			VersionedId versionedId = masterPropertyBag.TryGetProperty(InternalSchema.ItemId) as VersionedId;
			int valueOrDefault = masterPropertyBag.GetValueOrDefault<int>(InternalSchema.Codepage, CalendarItem.DefaultCodePage);
			bool flag = Array.IndexOf<PropertyDefinition>(queriedColumns, InternalSchema.IsSeriesCancelled) >= 0;
			bool flag2 = false;
			if (flag)
			{
				flag2 = CalendarFolder.GetIsSeriesCancelled(masterPropertyBag);
			}
			PropertyDefinition[] array = new PropertyDefinition[nativeColumns.Length + 1];
			array[0] = InternalSchema.ItemId;
			nativeColumns.CopyTo(array, 1);
			QueryResultPropertyBag queryResultPropertyBag = new QueryResultPropertyBag(base.Session, array);
			Exception ex = null;
			try
			{
				InternalRecurrence internalRecurrence = InternalRecurrence.FromMasterPropertyBag(masterPropertyBag, base.Session, versionedId, valueOrDefault, latencyInformation);
				if (internalRecurrence == null)
				{
					return null;
				}
				RecurrenceManager recurrenceManager = new RecurrenceManager(base.Session, internalRecurrence);
				object[] array2 = new object[masterRow.Length + 1];
				array2[0] = versionedId;
				masterRow.CopyTo(array2, 1);
				long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
				List<object[]> list = recurrenceManager.Expand(array2, startTime, endTime, valueOrDefault, array, expansionOption);
				latencyInformation.BlobExpansionTime = stopwatch.ElapsedMilliseconds - elapsedMilliseconds;
				if (list.Count == 0)
				{
					return null;
				}
				List<object[]> list2 = new List<object[]>();
				long elapsedMilliseconds2 = stopwatch.ElapsedMilliseconds;
				foreach (object[] array3 in list)
				{
					if (list2.Count == StorageLimits.Instance.CalendarExpansionInstanceLimit)
					{
						break;
					}
					queryResultPropertyBag.SetQueryResultRow(array3);
					object[] array4 = new object[queriedColumns.Length];
					for (int i = 0; i < queriedColumns.Length; i++)
					{
						if (queriedColumns[i] == InternalSchema.ItemId)
						{
							array4[i] = array3[Array.IndexOf<PropertyDefinition>(array, InternalSchema.ItemId)];
						}
						else if (queriedColumns[i] == InternalSchema.IsSeriesCancelled)
						{
							array4[i] = flag2;
						}
						else
						{
							array4[i] = queryResultPropertyBag.TryGetProperty(queriedColumns[i]);
						}
					}
					batchingStrategy.AddNewRow(list2, array4);
				}
				latencyInformation.AddRowsForInstancesTime = stopwatch.ElapsedMilliseconds - elapsedMilliseconds2;
				return list2;
			}
			catch (RecurrenceException ex2)
			{
				ex = ex2;
			}
			catch (OccurrenceNotFoundException ex3)
			{
				ex = ex3;
			}
			catch (CorruptDataException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				ExTraceGlobals.StorageTracer.TraceDebug<int, VersionedId, Exception>((long)this.GetHashCode(), "CalendarFolder::GetRecurringCalendarItems. HashCode = {0}. Failed to expand recurring calendar item={1} due to exception {2}", this.GetHashCode(), versionedId, ex);
			}
			return null;
		}

		// Token: 0x06002843 RID: 10307 RVA: 0x000A02D4 File Offset: 0x0009E4D4
		private object[] GetSingleMeeting(ExDateTime startTime, ExDateTime endTime, QueryResultPropertyBag rowPropertyBag, PropertyDefinition[] dataColumns, bool isPublicFolder)
		{
			object obj = rowPropertyBag.TryGetProperty(InternalSchema.StartTime);
			object obj2 = rowPropertyBag.TryGetProperty(InternalSchema.EndTime);
			if (!PropertyError.IsPropertyError(obj) && !PropertyError.IsPropertyError(obj2))
			{
				VersionedId valueOrDefault = rowPropertyBag.GetValueOrDefault<VersionedId>(InternalSchema.ItemId);
				ExDateTime startTime2 = (ExDateTime)obj;
				ExDateTime endTime2 = (ExDateTime)obj2;
				if (CalendarFolder.IsInWindow(startTime, endTime, startTime2, endTime2) && (valueOrDefault.ObjectId.ObjectType == StoreObjectType.CalendarItem || ((valueOrDefault.ObjectId.ObjectType == StoreObjectType.MeetingRequest || valueOrDefault.ObjectId.ObjectType == StoreObjectType.MeetingCancellation) && isPublicFolder)))
				{
					object[] array = new object[dataColumns.Length];
					for (int i = 0; i < dataColumns.Length; i++)
					{
						array[i] = rowPropertyBag.TryGetProperty(dataColumns[i]);
					}
					return array;
				}
			}
			return null;
		}

		// Token: 0x06002844 RID: 10308 RVA: 0x000A039C File Offset: 0x0009E59C
		private object[][] GetCalendarViewFromViewProperties(ExDateTime startTime, ExDateTime endTime, PropertyDefinition[] dataColumns, RecurrenceExpansionOption expansionOption, CalendarViewLatencyInformation calendarViewLatencyInformation, CalendarViewBatchingStrategy batchingStrategy = null)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			if (batchingStrategy == null)
			{
				batchingStrategy = CalendarViewBatchingStrategy.CreateNoneBatchingInstance();
			}
			object[][] result;
			try
			{
				List<object[]> list = new List<object[]>();
				bool isPublicFolder = base.Session is PublicFolderSession;
				ExDateTime exDateTime = startTime.ToUtc();
				endTime.ToUtc();
				PropertyDefinition[] array = new PropertyDefinition[]
				{
					InternalSchema.EndTime,
					InternalSchema.StartTime,
					CalendarFolder.MeetingsSortKey,
					InternalSchema.AppointmentRecurrenceBlob,
					InternalSchema.ItemId,
					InternalSchema.TimeZoneBlob,
					InternalSchema.TimeZone,
					InternalSchema.TimeZoneDefinitionRecurring,
					InternalSchema.CleanGlobalObjectId,
					InternalSchema.Codepage,
					InternalSchema.ViewEndTime,
					InternalSchema.ViewStartTime
				};
				PropertyDefinition[] array2 = new PropertyDefinition[dataColumns.Length + array.Length];
				dataColumns.CopyTo(array2, 0);
				array.CopyTo(array2, dataColumns.Length);
				NativeStorePropertyDefinition[] array3 = StorePropertyDefinition.GetNativePropertyDefinitions<PropertyDefinition>(PropertyDependencyType.NeedForRead, array2).ToArray<NativeStorePropertyDefinition>();
				QueryResultPropertyBag queryResultPropertyBag = new QueryResultPropertyBag(base.Session, array3);
				long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
				using (QueryResult queryResult = base.ItemQueryInternal(ItemQueryType.None, null, CalendarFolder.MeetingsSortRule, QueryExclusionType.None, array3))
				{
					long elapsedMilliseconds2 = stopwatch.ElapsedMilliseconds;
					calendarViewLatencyInformation.SingleItemQueryTime = elapsedMilliseconds2 - elapsedMilliseconds;
					queryResult.SeekToCondition(SeekReference.OriginBeginning, new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, InternalSchema.ViewEndTime, exDateTime));
					calendarViewLatencyInformation.SingleQuerySeekToTime = stopwatch.ElapsedMilliseconds - elapsedMilliseconds2;
					bool flag = true;
					while (flag)
					{
						elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
						object[][] rows = queryResult.GetRows(StorageLimits.Instance.CalendarSingleInstanceLimit, out flag);
						calendarViewLatencyInformation.SingleItemGetRowsTime += stopwatch.ElapsedMilliseconds - elapsedMilliseconds;
						calendarViewLatencyInformation.SingleItemQueryCount += rows.Length;
						foreach (object[] array5 in rows)
						{
							queryResultPropertyBag.SetQueryResultRow(array5);
							if (!(queryResultPropertyBag.TryGetProperty(InternalSchema.AppointmentRecurrenceBlob) is byte[]))
							{
								object[] singleMeeting = this.GetSingleMeeting(startTime, endTime, queryResultPropertyBag, dataColumns, isPublicFolder);
								if (singleMeeting != null)
								{
									list.Add(singleMeeting);
								}
							}
							else
							{
								RecurringItemLatencyInformation recurringItemLatencyInformation = new RecurringItemLatencyInformation();
								List<object[]> instancesFromMasterRow = this.GetInstancesFromMasterRow(startTime, endTime, array5, queryResultPropertyBag, dataColumns, array3, expansionOption, recurringItemLatencyInformation, batchingStrategy);
								if (instancesFromMasterRow != null)
								{
									list.AddRange(instancesFromMasterRow);
								}
								else
								{
									calendarViewLatencyInformation.RecurringItemsNoInstancesInWindow += 1L;
								}
								CalendarFolder.UpdateRecurringExpansionLatencies(calendarViewLatencyInformation, recurringItemLatencyInformation);
								if (list.Count > StorageLimits.Instance.CalendarExpansionInstanceLimit)
								{
									break;
								}
								if (list.Count >= StorageLimits.Instance.CalendarSingleInstanceLimit)
								{
									flag = false;
									break;
								}
							}
						}
					}
				}
				result = list.ToArray();
			}
			finally
			{
				stopwatch.Stop();
				calendarViewLatencyInformation.SingleItemTotalTime = stopwatch.ElapsedMilliseconds;
			}
			return result;
		}

		// Token: 0x06002845 RID: 10309 RVA: 0x000A067C File Offset: 0x0009E87C
		private object[][] GetLegacyCalendarView(ExDateTime startTime, ExDateTime endTime, PropertyDefinition[] dataColumns, RecurrenceExpansionOption expansionOption, CalendarViewLatencyInformation calendarViewLatencyInformation)
		{
			List<object[]> singleCalendarItems = this.GetSingleCalendarItems(startTime, endTime, dataColumns, calendarViewLatencyInformation, false, null);
			List<object[]> recurringCalendarItems = this.GetRecurringCalendarItems(startTime, endTime, dataColumns, expansionOption, calendarViewLatencyInformation, null);
			object[][] array = new object[singleCalendarItems.Count + recurringCalendarItems.Count][];
			singleCalendarItems.CopyTo(array, 0);
			recurringCalendarItems.CopyTo(array, singleCalendarItems.Count);
			return array;
		}

		// Token: 0x06002846 RID: 10310 RVA: 0x000A06E8 File Offset: 0x0009E8E8
		private CalendarCorrelationMatch GetCalendarCorrelationMatch(byte[] globalObjectIdBytes, bool shouldDetectDuplicateIds, ICollection<PropertyDefinition> properties, out IEnumerable<VersionedId> detectedDuplicatesIds)
		{
			this.CheckDisposed("GetCalendarItemId");
			if (globalObjectIdBytes == null)
			{
				throw new ArgumentNullException("globalObjectIdBytes");
			}
			GlobalObjectId globalObjectId = new GlobalObjectId(globalObjectIdBytes);
			List<CalendarCorrelationMatch> list = CalendarCorrelationMatch.FindMatches(this, globalObjectId, properties);
			if (list.Count == 0)
			{
				detectedDuplicatesIds = (shouldDetectDuplicateIds ? Array<VersionedId>.Empty : null);
				return null;
			}
			list.Sort();
			int index = list.Count - 1;
			CalendarCorrelationMatch result = list[index];
			if (shouldDetectDuplicateIds)
			{
				list.RemoveAt(index);
				detectedDuplicatesIds = from match in list
				select match.GetCorrelatedId(globalObjectId);
			}
			else
			{
				detectedDuplicatesIds = null;
			}
			return result;
		}

		// Token: 0x06002847 RID: 10311 RVA: 0x000A0790 File Offset: 0x0009E990
		private void ReconfigureLocationProperties(PropertyDefinition[] dataColumns)
		{
			int num = Array.IndexOf<PropertyDefinition>(dataColumns, InternalSchema.LocationSource);
			if (num < 0)
			{
				return;
			}
			for (int i = 0; i < dataColumns.Length; i++)
			{
				int num2 = Array.IndexOf<PropertyDefinition>(CalendarItemProperties.EnhancedLocationPropertyDefinitions, dataColumns[i]);
				if (num2 >= 0)
				{
					if (dataColumns[i] == InternalSchema.LocationDisplayName)
					{
						dataColumns[i] = InternalSchema.Location;
					}
					else
					{
						dataColumns[i] = new NullLocationProperty(CalendarItemProperties.EnhancedLocationPropertyDefinitions[num2].Name);
					}
				}
			}
		}

		// Token: 0x06002848 RID: 10312 RVA: 0x000A07F8 File Offset: 0x0009E9F8
		private static void LimitColumns(object[][] results, int columnLength)
		{
			for (int i = 0; i < results.Length; i++)
			{
				if (results[i].Length > columnLength)
				{
					object[] array = new object[columnLength];
					Array.Copy(results[i], array, columnLength);
					results[i] = array;
				}
			}
		}

		// Token: 0x06002849 RID: 10313 RVA: 0x000A0854 File Offset: 0x0009EA54
		private static bool IsNonPrivateProperty(PropertyDefinition propertyDefinition)
		{
			if (propertyDefinition is NativeStorePropertyDefinition)
			{
				if (CalendarFolder.NonPrivateNativeProperties.Any((NativeStorePropertyDefinition p) => p.Equals(propertyDefinition)))
				{
					return true;
				}
			}
			else if (CalendarFolder.NonPrivateProperties.Any((PropertyDefinition p) => p.Equals(propertyDefinition)))
			{
				return true;
			}
			return false;
		}

		// Token: 0x0600284A RID: 10314 RVA: 0x000A08C0 File Offset: 0x0009EAC0
		private static AdjacencyOrConflictInfo GetAdjacencyOrConflictInfo(object[] row, Pair<ExDateTime, ExDateTime> startEndTime, bool useStartTimeAsOriginalStartTime, Dictionary<VersionedId, AdjacencyOrConflictInfo> cachedResults)
		{
			ExDateTime exDateTime = (ExDateTime)row[6];
			ExDateTime endTime = (ExDateTime)row[7];
			AdjacencyOrConflictType adjacencyOrConflictType;
			if (!CalendarFolder.IsAdjacencyOrConflictItem(exDateTime, endTime, startEndTime, out adjacencyOrConflictType))
			{
				return null;
			}
			VersionedId versionedId = (VersionedId)row[0];
			AdjacencyOrConflictInfo adjacencyOrConflictInfo = null;
			if (!cachedResults.TryGetValue(versionedId, out adjacencyOrConflictInfo))
			{
				ExDateTime originalStartTime = exDateTime;
				if (!useStartTimeAsOriginalStartTime)
				{
					originalStartTime = ExDateTime.MinValue;
				}
				OccurrenceInfo occurrenceInfo = new OccurrenceInfo(versionedId, exDateTime.Date, originalStartTime, exDateTime, endTime);
				byte[] globalObjectId = row[1] as byte[];
				string subject = row[2] as string;
				string location = row[3] as string;
				bool isAllDayEvent = (bool)row[8];
				BusyType busyType = BusyType.Free;
				if (row[4] is int)
				{
					busyType = (BusyType)row[4];
					if (!EnumValidator.IsValidValue<BusyType>(busyType))
					{
						busyType = BusyType.Free;
					}
				}
				Sensitivity sensitivity = Sensitivity.Normal;
				if (row[5] is int)
				{
					sensitivity = (Sensitivity)row[5];
					if (!EnumValidator.IsValidValue<Sensitivity>(sensitivity))
					{
						sensitivity = Sensitivity.Normal;
					}
				}
				return new AdjacencyOrConflictInfo(occurrenceInfo, subject, location, busyType, adjacencyOrConflictType, globalObjectId, sensitivity, isAllDayEvent);
			}
			if ((adjacencyOrConflictType & adjacencyOrConflictInfo.AdjacencyOrConflictType) != adjacencyOrConflictType)
			{
				return new AdjacencyOrConflictInfo(adjacencyOrConflictInfo.OccurrenceInfo, adjacencyOrConflictInfo.Subject, adjacencyOrConflictInfo.Location, adjacencyOrConflictInfo.FreeBusyStatus, adjacencyOrConflictInfo.AdjacencyOrConflictType | adjacencyOrConflictType, adjacencyOrConflictInfo.GlobalObjectId, adjacencyOrConflictInfo.Sensitivity, adjacencyOrConflictInfo.IsAllDayEvent);
			}
			return null;
		}

		// Token: 0x0600284B RID: 10315 RVA: 0x000A09F8 File Offset: 0x0009EBF8
		private static bool IsRowMatchingCalendarItem(CalendarItemBase calendarItem, object[] row)
		{
			byte[] array = (calendarItem.GlobalObjectId == null) ? null : calendarItem.GlobalObjectId.Bytes;
			bool flag;
			if (array == null)
			{
				VersionedId versionedId = (VersionedId)row[0];
				StoreObjectId storeObjectId = (calendarItem.Id != null) ? calendarItem.Id.ObjectId : null;
				if (storeObjectId != null)
				{
					storeObjectId = StoreObjectId.FromProviderSpecificId(storeObjectId.ProviderLevelItemId, StoreObjectType.CalendarItem);
				}
				flag = object.Equals(versionedId.ObjectId, storeObjectId);
				if (flag)
				{
					ExTraceGlobals.MeetingMessageTracer.TraceDebug<StoreObjectId, StoreObjectId>(0L, "CalendarFolder.IsConflictDataFromSameCalendarItem: Based on VersionId, conflicting data (id={0}) is from the processing item (id={1})", versionedId.ObjectId, storeObjectId);
				}
			}
			else
			{
				byte[] array2 = row[1] as byte[];
				flag = (array2 != null && GlobalObjectId.CompareCleanGlobalObjectIds(array2, array));
				if (flag)
				{
					ExTraceGlobals.MeetingMessageTracer.TraceDebug<byte[], byte[]>(0L, "CalendarFolder.IsConflictDataFromSameCalendarItem: Based on GOID, conflicting data (GOID={0}) is from the processing item (GOID={1})", array2, array);
				}
			}
			return flag;
		}

		// Token: 0x0600284C RID: 10316 RVA: 0x000A0AB0 File Offset: 0x0009ECB0
		private static bool IsAdjacencyOrConflictItem(ExDateTime startTime, ExDateTime endTime, Pair<ExDateTime, ExDateTime> startEndTime, out AdjacencyOrConflictType type)
		{
			type = AdjacencyOrConflictType.Conflicts;
			if (startTime == startEndTime.Second)
			{
				type = AdjacencyOrConflictType.Follows;
			}
			else if (endTime == startEndTime.First)
			{
				type = AdjacencyOrConflictType.Precedes;
			}
			else
			{
				if (!(startTime <= startEndTime.Second) || !(endTime >= startEndTime.First))
				{
					return false;
				}
				type = AdjacencyOrConflictType.Conflicts;
			}
			return true;
		}

		// Token: 0x0600284D RID: 10317 RVA: 0x000A0B0C File Offset: 0x0009ED0C
		private void ModifySubjectLocationAndPrivateItems(bool busyStatusOnly, bool filterPrivateItems, object[][] queryResults, PropertyDefinition[] dataColumns, int busyStatusIndex, int subjectIndex, int sensitivityIndex)
		{
			foreach (object[] array in queryResults)
			{
				if (busyStatusOnly)
				{
					BusyType busyType;
					if (array[busyStatusIndex] is PropertyError)
					{
						ExTraceGlobals.StorageTracer.TraceDebug<Guid>((long)this.GetHashCode(), "Missing FreeBusyStatus property. MailboxGuid:{0}", base.Session.MailboxGuid);
						busyType = BusyType.Tentative;
					}
					else
					{
						busyType = (BusyType)array[busyStatusIndex];
					}
					array[subjectIndex] = CalendarUtil.GetSubjectFromFreeBusyStatus(busyType, base.Session.InternalPreferedCulture);
					int num = Array.IndexOf<PropertyDefinition>(dataColumns, InternalSchema.Location);
					if (num > -1)
					{
						array[num] = new PropertyError(dataColumns[num], PropertyErrorCode.NotFound);
					}
				}
				if (filterPrivateItems && object.Equals(array[sensitivityIndex], 2))
				{
					for (int j = 0; j < dataColumns.Length; j++)
					{
						if (j == subjectIndex)
						{
							if (!busyStatusOnly)
							{
								array[subjectIndex] = ClientStrings.PrivateAppointmentSubject.ToString(base.Session.InternalPreferedCulture);
							}
						}
						else if (!CalendarFolder.IsNonPrivateProperty(dataColumns[j]))
						{
							array[j] = new PropertyError(dataColumns[j], PropertyErrorCode.NotFound);
						}
					}
				}
			}
		}

		// Token: 0x0600284E RID: 10318 RVA: 0x000A0C10 File Offset: 0x0009EE10
		private void SaveFreebusyDataPermissions(PermissionTable calendarPermissionsTable)
		{
			if (base.InternalObjectId == null)
			{
				return;
			}
			MailboxSession mailboxSession = base.Session as MailboxSession;
			if (mailboxSession == null)
			{
				return;
			}
			if (!base.StoreObjectId.Equals(mailboxSession.GetDefaultFolderId(DefaultFolderType.Calendar)))
			{
				return;
			}
			if (mailboxSession.GetDefaultFolderId(DefaultFolderType.FreeBusyData) == null)
			{
				Exception ex = null;
				try
				{
					mailboxSession.CreateDefaultFolder(DefaultFolderType.FreeBusyData);
					ExTraceGlobals.StorageTracer.TraceDebug<IExchangePrincipal>((long)this.GetHashCode(), "{0}: New FreeBusyData folder created.", mailboxSession.MailboxOwner);
				}
				catch (AccessDeniedException ex2)
				{
					ex = ex2;
				}
				catch (TransientException ex3)
				{
					ex = ex3;
				}
				if (ex != null)
				{
					ExTraceGlobals.StorageTracer.TraceError<IExchangePrincipal, string>((long)this.GetHashCode(), "{0}: No FreeBusyData folder, attempt to create one failed! {1}", mailboxSession.MailboxOwner, ex.Message);
					return;
				}
			}
			List<PermissionSecurityPrincipal> list = new List<PermissionSecurityPrincipal>();
			foreach (Permission permission in calendarPermissionsTable)
			{
				PermissionSecurityPrincipal principal = permission.Principal;
				if (principal.Type == PermissionSecurityPrincipal.SecurityPrincipalType.ADRecipientPrincipal && principal.ADRecipient.IsValidSecurityPrincipal)
				{
					CalendarFolderPermission calendarFolderPermission = (CalendarFolderPermission)permission;
					if (calendarFolderPermission.PermissionLevel != PermissionLevel.Custom && calendarFolderPermission.FreeBusyAccess == FreeBusyAccess.Details)
					{
						list.Add(principal);
					}
				}
			}
			using (Folder folder = Folder.Bind(mailboxSession, DefaultFolderType.FreeBusyData))
			{
				PermissionSet permissionSet = folder.GetPermissionSet();
				foreach (PermissionSecurityPrincipal permissionSecurityPrincipal in list)
				{
					Permission permission2 = permissionSet.GetEntry(permissionSecurityPrincipal);
					if (permission2 == null)
					{
						ExTraceGlobals.StorageTracer.TraceDebug<IExchangePrincipal, PermissionSecurityPrincipal>((long)this.GetHashCode(), "{0}: Adding a new entry for {1} on FreeBusyData folder.", mailboxSession.MailboxOwner, permissionSecurityPrincipal);
						permission2 = permissionSet.AddEntry(permissionSecurityPrincipal, PermissionLevel.Editor);
					}
					else
					{
						ExTraceGlobals.StorageTracer.TraceDebug<IExchangePrincipal, PermissionSecurityPrincipal>((long)this.GetHashCode(), "{0}: Modifying the entry for {1} on FreeBusyData folder.", mailboxSession.MailboxOwner, permissionSecurityPrincipal);
						permission2.PermissionLevel = PermissionLevel.Editor;
					}
				}
				if (calendarPermissionsTable.DefaultPermission.IsDirty)
				{
					Permission defaultPermission = permissionSet.DefaultPermission;
					if (calendarPermissionsTable.DefaultPermission.MemberRights == MemberRights.None || (calendarPermissionsTable.DefaultPermission.MemberRights & MemberRights.Visible) == MemberRights.None)
					{
						permissionSet.DefaultPermission.PermissionLevel = PermissionLevel.None;
					}
					else
					{
						permissionSet.DefaultPermission.PermissionLevel = PermissionLevel.Editor;
					}
				}
				folder.Save();
			}
		}

		// Token: 0x0600284F RID: 10319 RVA: 0x000A0E70 File Offset: 0x0009F070
		internal List<object[]> GetSingleCalendarItems(ExDateTime startView, ExDateTime endView, PropertyDefinition[] dataColumns, bool includeNprMasters, CalendarViewBatchingStrategy batchingStrategy = null)
		{
			CalendarViewLatencyInformation calendarViewLatencyInformation = new CalendarViewLatencyInformation();
			return this.GetSingleCalendarItems(startView, endView, dataColumns, calendarViewLatencyInformation, includeNprMasters, batchingStrategy);
		}

		// Token: 0x06002850 RID: 10320 RVA: 0x000A0E94 File Offset: 0x0009F094
		internal List<object[]> GetSingleCalendarItems(ExDateTime startView, ExDateTime endView, PropertyDefinition[] dataColumns, CalendarViewLatencyInformation calendarViewLatencyInformation, bool includeNprMasters, CalendarViewBatchingStrategy batchingStrategy = null)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			List<object[]> result;
			try
			{
				if (batchingStrategy == null)
				{
					batchingStrategy = CalendarViewBatchingStrategy.CreateNoneBatchingInstance();
				}
				bool flag = base.Session is PublicFolderSession;
				int num = 5;
				int num2 = dataColumns.Length;
				int num3 = num2 + 1;
				int num4 = num3 + 1;
				int num5 = num4 + 1;
				int num6 = num5 + 1;
				int num7 = num6 + 1;
				int num8 = num7 + 1;
				if (includeNprMasters)
				{
					num += 2;
				}
				PropertyDefinition[] array;
				if (batchingStrategy.ShouldBatch)
				{
					num++;
					array = new PropertyDefinition[dataColumns.Length + num];
					array[array.Length - 1] = InternalSchema.InstanceKey;
					batchingStrategy.SetColumnIndices(array.Length - 1, num4);
				}
				else
				{
					array = new PropertyDefinition[dataColumns.Length + num];
				}
				dataColumns.CopyTo(array, 0);
				array[num2] = InternalSchema.EndTime;
				array[num3] = InternalSchema.StartTime;
				array[num4] = CalendarFolder.SingleInstanceMeetingsSortKey;
				array[num5] = InternalSchema.AppointmentRecurrenceBlob;
				array[num6] = InternalSchema.ItemId;
				if (includeNprMasters)
				{
					array[num7] = InternalSchema.ClipStartTime;
					array[num8] = InternalSchema.ClipEndTime;
				}
				long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
				using (QueryResult queryResult = base.ItemQueryInternal(ItemQueryType.None, null, CalendarFolder.SingleInstanceMeetingsSortRule, QueryExclusionType.None, array))
				{
					long elapsedMilliseconds2 = stopwatch.ElapsedMilliseconds;
					calendarViewLatencyInformation.SingleItemQueryTime = elapsedMilliseconds2 - elapsedMilliseconds;
					List<object[]> list = new List<object[]>();
					bool flag2 = true;
					bool flag3 = false;
					bool isFirstFetch = true;
					bool flag4 = false;
					long num9 = 0L;
					object[][] array2;
					while (!flag4 && batchingStrategy.TryGetNextBatch(queryResult, StorageLimits.Instance.CalendarSingleInstanceLimit, flag2 ? 50 : 100, false, isFirstFetch, out array2, out num9))
					{
						calendarViewLatencyInformation.SingleItemGetRowsTime += num9;
						calendarViewLatencyInformation.SingleItemQueryCount += list.Count;
						isFirstFetch = false;
						int num10 = 0;
						foreach (object[] array4 in array2)
						{
							if (!(array4[num5] is byte[]))
							{
								if (flag2 && !(array4[num4] is PropertyError))
								{
									flag3 = true;
									flag2 = false;
									isFirstFetch = true;
									break;
								}
								VersionedId versionedId = (VersionedId)array4[num6];
								StoreObjectType objectType = versionedId.ObjectId.ObjectType;
								bool flag5;
								int num11;
								int num12;
								switch (objectType)
								{
								case StoreObjectType.MeetingRequest:
								case StoreObjectType.MeetingCancellation:
									flag5 = flag;
									num11 = num3;
									num12 = num2;
									break;
								case StoreObjectType.MeetingResponse:
								case StoreObjectType.ConflictMessage:
									goto IL_202;
								case StoreObjectType.CalendarItem:
									flag5 = true;
									num11 = num3;
									num12 = num2;
									break;
								default:
									if (objectType != StoreObjectType.CalendarItemSeries)
									{
										goto IL_202;
									}
									flag5 = includeNprMasters;
									num11 = num7;
									num12 = num8;
									break;
								}
								IL_20B:
								if (!flag5 || !(array4[num11] is ExDateTime) || !(array4[num12] is ExDateTime))
								{
									goto IL_29C;
								}
								ExDateTime startTime = (ExDateTime)array4[num11];
								ExDateTime endTime = (ExDateTime)array4[num12];
								if (!CalendarFolder.IsInWindow(startView, endView, startTime, endTime))
								{
									num10++;
									goto IL_29C;
								}
								if (batchingStrategy.ShouldAddNewRow(array4, false))
								{
									object[] array5 = new object[dataColumns.Length];
									for (int j = 0; j < dataColumns.Length; j++)
									{
										array5[j] = array4[j];
									}
									batchingStrategy.AddNewRow(list, array5);
									num10 = 0;
									goto IL_29C;
								}
								flag4 = true;
								break;
								IL_202:
								flag5 = false;
								num11 = -1;
								num12 = -1;
								goto IL_20B;
							}
							IL_29C:;
						}
						if (!flag4)
						{
							if (flag3)
							{
								flag3 = false;
								long elapsedMilliseconds3 = stopwatch.ElapsedMilliseconds;
								queryResult.SeekToCondition(SeekReference.OriginBeginning, new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, InternalSchema.MapiEndTime, startView));
								calendarViewLatencyInformation.SingleQuerySeekToTime = stopwatch.ElapsedMilliseconds - elapsedMilliseconds3;
							}
							else if (!flag2 && num10 > 3)
							{
								queryResult.SeekToCondition(SeekReference.OriginCurrent, new ComparisonFilter(ComparisonOperator.LessThanOrEqual, InternalSchema.MapiStartTime, endView));
							}
						}
					}
					result = list;
				}
			}
			finally
			{
				stopwatch.Stop();
				calendarViewLatencyInformation.SingleItemTotalTime = stopwatch.ElapsedMilliseconds;
			}
			return result;
		}

		// Token: 0x06002851 RID: 10321 RVA: 0x000A123C File Offset: 0x0009F43C
		private List<Pair<ExDateTime, ExDateTime>> GetAppointmentTimes(CalendarItemBase calendarItemBase)
		{
			return this.GetAppointmentTimes(calendarItemBase, null);
		}

		// Token: 0x06002852 RID: 10322 RVA: 0x000A125C File Offset: 0x0009F45C
		private List<Pair<ExDateTime, ExDateTime>> GetAppointmentTimes(CalendarItemBase calendarItemBase, ExDateTime? expansionLimit)
		{
			List<Pair<ExDateTime, ExDateTime>> list = new List<Pair<ExDateTime, ExDateTime>>();
			CalendarItem calendarItem = calendarItemBase as CalendarItem;
			try
			{
				if (calendarItem != null && calendarItem.Recurrence != null)
				{
					InternalRecurrence internalRecurrence = (InternalRecurrence)calendarItem.Recurrence;
					ExDateTime endView = expansionLimit ?? internalRecurrence.Range.StartDate.AddMonths(6);
					using (IEnumerator<OccurrenceInfo> enumerator = internalRecurrence.GetOccurrenceInfoList(ExDateTime.MinValue, endView).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							OccurrenceInfo occurrenceInfo = enumerator.Current;
							list.Add(new Pair<ExDateTime, ExDateTime>(occurrenceInfo.StartTime, occurrenceInfo.EndTime));
						}
						goto IL_F9;
					}
				}
				ExDateTime exDateTime = calendarItemBase.StartTime;
				ExDateTime exDateTime2 = calendarItemBase.EndTime;
				ExDateTime exDateTime3 = ExDateTime.MaxValue.AddSeconds(-1.0);
				if (exDateTime2 > exDateTime3)
				{
					exDateTime2 = exDateTime3;
				}
				if (exDateTime > exDateTime2)
				{
					exDateTime = exDateTime2;
				}
				list.Add(new Pair<ExDateTime, ExDateTime>(exDateTime, exDateTime2));
				IL_F9:;
			}
			catch (CorruptDataException)
			{
			}
			return list;
		}

		// Token: 0x06002853 RID: 10323 RVA: 0x000A1384 File Offset: 0x0009F584
		private List<object[]> GetRecurringCalendarItems(ExDateTime startTime, ExDateTime endTime, PropertyDefinition[] dataColumns, RecurrenceExpansionOption options, CalendarViewBatchingStrategy batchingStrategy = null)
		{
			CalendarViewLatencyInformation calendarViewLatencyInformation = new CalendarViewLatencyInformation();
			return this.GetRecurringCalendarItems(startTime, endTime, dataColumns, options, calendarViewLatencyInformation, batchingStrategy);
		}

		// Token: 0x06002854 RID: 10324 RVA: 0x000A13A8 File Offset: 0x0009F5A8
		private List<object[]> GetRecurringCalendarItems(ExDateTime startTime, ExDateTime endTime, PropertyDefinition[] dataColumns, RecurrenceExpansionOption options, CalendarViewLatencyInformation calendarViewLatencyInformation, CalendarViewBatchingStrategy batchingStrategy = null)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			List<object[]> result;
			try
			{
				if (batchingStrategy == null)
				{
					batchingStrategy = CalendarViewBatchingStrategy.CreateNoneBatchingInstance();
				}
				IList<ColumnAdjuster> list = new List<ColumnAdjuster>(9);
				list.Add(new ColumnAdjuster(InternalSchema.AppointmentRecurrenceBlob));
				list.Add(new ColumnAdjuster(InternalSchema.ItemId));
				list.Add(new ColumnAdjuster(InternalSchema.TimeZoneBlob));
				list.Add(new ColumnAdjuster(InternalSchema.TimeZone));
				list.Add(new ColumnAdjuster(InternalSchema.TimeZoneDefinitionRecurring));
				list.Add(new ColumnAdjuster(InternalSchema.CleanGlobalObjectId));
				list.Add(new ColumnAdjuster(InternalSchema.Codepage));
				PropertyDefinition[] propertyDefinitions;
				if (batchingStrategy.ShouldBatch)
				{
					ColumnAdjuster item = new ColumnAdjuster(InternalSchema.InstanceKey);
					list.Add(item);
					ColumnAdjuster item2 = new ColumnAdjuster(CalendarFolder.RecurringMeetingsSortKey);
					list.Add(item2);
					propertyDefinitions = ColumnAdjuster.Adjust(dataColumns, list);
				}
				else
				{
					propertyDefinitions = ColumnAdjuster.Adjust(dataColumns, list);
				}
				NativeStorePropertyDefinition[] array = StorePropertyDefinition.GetNativePropertyDefinitions<PropertyDefinition>(PropertyDependencyType.NeedForRead, propertyDefinitions).ToArray<NativeStorePropertyDefinition>();
				if (batchingStrategy.ShouldBatch)
				{
					int num = 0;
					int? num2 = null;
					int? num3 = null;
					do
					{
						PropertyDefinition propertyDefinition = array[num];
						if (propertyDefinition == InternalSchema.InstanceKey)
						{
							num2 = new int?(num);
						}
						else if (propertyDefinition == CalendarFolder.RecurringMeetingsSortKey)
						{
							num3 = new int?(num);
						}
						num++;
					}
					while (num2 == null || num3 == null);
					batchingStrategy.SetColumnIndices(num2.Value, num3.Value);
				}
				List<object[]> list2 = new List<object[]>(2 * (int)(endTime - startTime).TotalDays);
				PropertyDefinition[] array2 = new PropertyDefinition[array.Length + 1];
				array2[0] = InternalSchema.ItemId;
				array.CopyTo(array2, 1);
				QueryResultPropertyBag queryResultPropertyBag = new QueryResultPropertyBag(base.Session, array);
				long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
				using (QueryResult queryResult = base.ItemQueryInternal(ItemQueryType.None, CalendarFolder.RecurringCalendarItemFilter, batchingStrategy.ShouldBatch ? CalendarFolder.RecurringMeetingsSortRule : null, QueryExclusionType.None, array))
				{
					long elapsedMilliseconds2 = stopwatch.ElapsedMilliseconds;
					calendarViewLatencyInformation.RecurringItemQueryTime = elapsedMilliseconds2 - elapsedMilliseconds;
					bool isFirstFetch = true;
					bool flag = false;
					long num4 = 0L;
					object[][] array3;
					while (!flag && batchingStrategy.TryGetNextBatch(queryResult, StorageLimits.Instance.CalendarExpansionInstanceLimit, 2147483647, true, isFirstFetch, out array3, out num4))
					{
						isFirstFetch = false;
						calendarViewLatencyInformation.RecurringItemGetRowsTime += num4;
						calendarViewLatencyInformation.RecurringItemQueryCount += (long)array3.Length;
						foreach (object[] array5 in array3)
						{
							if (!batchingStrategy.ShouldAddNewRow(array5, true))
							{
								flag = true;
								break;
							}
							queryResultPropertyBag.SetQueryResultRow(array5);
							RecurringItemLatencyInformation recurringItemLatencyInformation = new RecurringItemLatencyInformation();
							List<object[]> instancesFromMasterRow = this.GetInstancesFromMasterRow(startTime, endTime, array5, queryResultPropertyBag, dataColumns, array, options, recurringItemLatencyInformation, batchingStrategy);
							if (instancesFromMasterRow != null)
							{
								list2.AddRange(instancesFromMasterRow);
							}
							else
							{
								calendarViewLatencyInformation.RecurringItemsNoInstancesInWindow += 1L;
							}
							CalendarFolder.UpdateRecurringExpansionLatencies(calendarViewLatencyInformation, recurringItemLatencyInformation);
						}
					}
				}
				result = list2;
			}
			finally
			{
				stopwatch.Stop();
				calendarViewLatencyInformation.RecurringItemTotalTime = stopwatch.ElapsedMilliseconds;
			}
			return result;
		}

		// Token: 0x06002855 RID: 10325 RVA: 0x000A16C4 File Offset: 0x0009F8C4
		private static void UpdateRecurringExpansionLatencies(CalendarViewLatencyInformation calendarViewLatency, RecurringItemLatencyInformation recurringItemLatency)
		{
			if (calendarViewLatency.MaxRecurringItemLatencyInformation != null)
			{
				if (calendarViewLatency.MaxRecurringItemLatencyInformation.BlobExpansionTime + calendarViewLatency.MaxRecurringItemLatencyInformation.BlobParseTime + calendarViewLatency.MaxRecurringItemLatencyInformation.AddRowsForInstancesTime < recurringItemLatency.BlobExpansionTime + recurringItemLatency.BlobParseTime + recurringItemLatency.AddRowsForInstancesTime)
				{
					calendarViewLatency.MaxRecurringItemLatencyInformation = recurringItemLatency;
					return;
				}
			}
			else
			{
				calendarViewLatency.MaxRecurringItemLatencyInformation = recurringItemLatency;
			}
		}

		// Token: 0x06002856 RID: 10326 RVA: 0x000A1724 File Offset: 0x0009F924
		private static bool GetIsSeriesCancelled(QueryResultPropertyBag masterPropertyBag)
		{
			AppointmentStateFlags valueOrDefault = masterPropertyBag.GetValueOrDefault<AppointmentStateFlags>(InternalSchema.AppointmentState);
			return CalendarItemBase.IsAppointmentStateCancelled(valueOrDefault);
		}

		// Token: 0x06002857 RID: 10327 RVA: 0x000A1744 File Offset: 0x0009F944
		private void Sync()
		{
			if (!base.IsExchangeCrossOrgShareFolder && !base.IsExchangeConsumerShareFolder)
			{
				ExTraceGlobals.StorageTracer.TraceDebug<string>((long)this.GetHashCode(), "CalendarFolder::Sync. Sync for folder {0} ignored since it is not an external or consumer shared folder.", base.DisplayName);
				return;
			}
			MailboxSession mailboxSession = base.Session as MailboxSession;
			if (mailboxSession == null)
			{
				ExTraceGlobals.StorageTracer.TraceDebug<string>((long)this.GetHashCode(), "CalendarFolder::Sync. Sync for folder {0} called with a non-mailbox session. Ignoring.", base.DisplayName);
				return;
			}
			if (!RefreshSharingFolderClient.CanSyncNow(base.LastAttemptedSyncTime))
			{
				return;
			}
			if (SyncAssistantInvoker.MailboxServerSupportsSync(mailboxSession))
			{
				SyncAssistantInvoker.SyncFolder(mailboxSession, StoreId.GetStoreObjectId(base.Id));
				return;
			}
			RefreshSharingFolderClient.Sync(this);
		}

		// Token: 0x06002858 RID: 10328 RVA: 0x000A17D8 File Offset: 0x0009F9D8
		private bool IsNewCalendarViewEnabled()
		{
			MailboxSession mailboxSession = base.Session as MailboxSession;
			if (mailboxSession == null)
			{
				return false;
			}
			VariantConfigurationSnapshot configuration = mailboxSession.MailboxOwner.GetConfiguration();
			return configuration.DataStorage.CalendarView.Enabled;
		}

		// Token: 0x040017A8 RID: 6056
		private const int MaxYearOutlookCanHandle = 4500;

		// Token: 0x040017A9 RID: 6057
		private const int MaxViewTimePeriod = 2;

		// Token: 0x040017AA RID: 6058
		private const int MaxOccurrenceExpansionPeriod = 6;

		// Token: 0x040017AB RID: 6059
		public static readonly PropertyDefinition SingleInstanceMeetingsSortKey = InternalSchema.MapiEndTime;

		// Token: 0x040017AC RID: 6060
		public static readonly PropertyDefinition MeetingsSortKey = InternalSchema.ViewEndTime;

		// Token: 0x040017AD RID: 6061
		public static readonly PropertyDefinition RecurringMeetingsSortKey = StoreObjectSchema.CreationTime;

		// Token: 0x040017AE RID: 6062
		public static readonly SortBy[] SingleInstanceMeetingsSortRule = new SortBy[]
		{
			new SortBy(CalendarFolder.SingleInstanceMeetingsSortKey, SortOrder.Ascending)
		};

		// Token: 0x040017AF RID: 6063
		public static readonly SortBy[] RecurringMeetingsSortRule = new SortBy[]
		{
			new SortBy(CalendarFolder.RecurringMeetingsSortKey, SortOrder.Ascending)
		};

		// Token: 0x040017B0 RID: 6064
		public static readonly SortBy[] MeetingsSortRule = new SortBy[]
		{
			new SortBy(CalendarFolder.MeetingsSortKey, SortOrder.Ascending)
		};

		// Token: 0x040017B1 RID: 6065
		internal static QueryFilter RecurringCalendarItemFilter = new ComparisonFilter(ComparisonOperator.Equal, InternalSchema.AppointmentRecurring, true);

		// Token: 0x040017B2 RID: 6066
		private static readonly PropertyDefinition[] NonPrivateProperties = new PropertyDefinition[]
		{
			CalendarItemInstanceSchema.StartTime,
			CalendarItemInstanceSchema.EndTime,
			CalendarItemBaseSchema.IsAllDayEvent,
			CalendarItemBaseSchema.FreeBusyStatus,
			ItemSchema.Sensitivity,
			ItemSchema.Id,
			StoreObjectSchema.ParentItemId,
			StoreObjectSchema.ItemClass,
			CalendarItemBaseSchema.CalendarItemType,
			CalendarItemBaseSchema.AppointmentRecurrenceBlob,
			CalendarItemBaseSchema.TimeZone,
			CalendarItemBaseSchema.TimeZoneBlob,
			CalendarItemBaseSchema.TimeZoneDefinitionRecurring,
			CalendarItemBaseSchema.GlobalObjectId
		};

		// Token: 0x040017B3 RID: 6067
		private static readonly ICollection<NativeStorePropertyDefinition> NonPrivateNativeProperties = StorePropertyDefinition.GetNativePropertyDefinitions<PropertyDefinition>(PropertyDependencyType.NeedForRead, CalendarFolder.NonPrivateProperties);

		// Token: 0x02000390 RID: 912
		private class SortByTimeColumnComparer : IComparer
		{
			// Token: 0x0600285A RID: 10330 RVA: 0x000A193D File Offset: 0x0009FB3D
			internal SortByTimeColumnComparer(int index)
			{
				this.index = index;
			}

			// Token: 0x0600285B RID: 10331 RVA: 0x000A194C File Offset: 0x0009FB4C
			public int Compare(object a, object b)
			{
				ExDateTime t = (ExDateTime)((object[])a)[this.index];
				ExDateTime t2 = (ExDateTime)((object[])b)[this.index];
				if (t < t2)
				{
					return -1;
				}
				if (t > t2)
				{
					return 1;
				}
				return 0;
			}

			// Token: 0x040017B4 RID: 6068
			private int index;
		}

		// Token: 0x02000391 RID: 913
		private class AdjacencyOrConflictInfoStartTimeComparer : IComparer<AdjacencyOrConflictInfo>
		{
			// Token: 0x0600285C RID: 10332 RVA: 0x000A1998 File Offset: 0x0009FB98
			public int Compare(AdjacencyOrConflictInfo x, AdjacencyOrConflictInfo y)
			{
				return x.OccurrenceInfo.StartTime.CompareTo(y.OccurrenceInfo.StartTime);
			}
		}
	}
}

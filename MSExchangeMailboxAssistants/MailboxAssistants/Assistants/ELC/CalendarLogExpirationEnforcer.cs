using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.CalendarDiagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.ELC;
using Microsoft.Exchange.MailboxAssistants.Assistants.ELC.Logging;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x020000A8 RID: 168
	internal class CalendarLogExpirationEnforcer : SysCleanupEnforcerBase
	{
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x00031247 File Offset: 0x0002F447
		// (set) Token: 0x0600066F RID: 1647 RVA: 0x0003124E File Offset: 0x0002F44E
		internal static PropertyDefinition OverwriteAgePropertyDefinition
		{
			get
			{
				return CalendarLogExpirationEnforcer.agePropertyDefinition;
			}
			set
			{
				CalendarLogExpirationEnforcer.PropertyColumns.Replace(CalendarLogExpirationEnforcer.agePropertyDefinition, value);
				CalendarLogExpirationEnforcer.agePropertyDefinition = value;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x00031268 File Offset: 0x0002F468
		internal static EnhancedTimeSpan DefaultAgeLimit
		{
			get
			{
				if (CalendarLogExpirationEnforcer.defaultAgeLimitForCalendarItems == null)
				{
					object obj = Globals.ReadRegKey(ElcGlobals.ParameterRegistryKeyPath, "DefaultAgeLimitForCalendarItems");
					int num;
					if (obj != null && int.TryParse(obj.ToString(), out num))
					{
						CalendarLogExpirationEnforcer.defaultAgeLimitForCalendarItems = new EnhancedTimeSpan?(EnhancedTimeSpan.FromDays((double)num));
					}
					else
					{
						CalendarLogExpirationEnforcer.defaultAgeLimitForCalendarItems = new EnhancedTimeSpan?(EnhancedTimeSpan.FromDays(31.0));
					}
				}
				return CalendarLogExpirationEnforcer.defaultAgeLimitForCalendarItems.Value;
			}
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x000312D8 File Offset: 0x0002F4D8
		internal CalendarLogExpirationEnforcer(MailboxDataForTags mailboxDataForTags, SysCleanupSubAssistant sysCleanupSubAssistant) : base(mailboxDataForTags, sysCleanupSubAssistant)
		{
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x000312F0 File Offset: 0x0002F4F0
		protected override void CollectItemsToExpire()
		{
			using (Folder folder = Folder.Bind(base.MailboxDataForTags.MailboxSession, DefaultFolderType.CalendarLogging))
			{
				this.ProcessFolderContents(folder, ItemQueryType.None);
				this.ProcessFolderContents(folder, ItemQueryType.Associated);
			}
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0003133C File Offset: 0x0002F53C
		private void ProcessFolderContents(Folder folder, ItemQueryType itemQueryType)
		{
			int num = base.FolderItemTypeCount(folder, itemQueryType);
			if (num <= 0)
			{
				CalendarLogExpirationEnforcer.Tracer.TraceDebug<CalendarLogExpirationEnforcer, string, ItemQueryType>((long)this.GetHashCode(), "{0}:{1} Folder is Empty of type {2}", this, folder.Id.ObjectId.ToHexEntryId(), itemQueryType);
				return;
			}
			using (QueryResult queryResult = folder.ItemQuery(itemQueryType, null, new SortBy[]
			{
				new SortBy(CalendarLogExpirationEnforcer.agePropertyDefinition, SortOrder.Ascending)
			}, CalendarLogExpirationEnforcer.PropertyColumns.PropertyDefinitions))
			{
				queryResult.SeekToOffset(SeekReference.OriginBeginning, 0);
				bool flag = false;
				while (!flag)
				{
					object[][] rows = queryResult.GetRows(100);
					if (rows.Length <= 0)
					{
						break;
					}
					foreach (object[] rawProperties in rows)
					{
						PropertyArrayProxy propertyArrayProxy = new PropertyArrayProxy(CalendarLogExpirationEnforcer.PropertyColumns, rawProperties);
						if (!ElcMailboxHelper.Exists(propertyArrayProxy[CalendarLogExpirationEnforcer.agePropertyDefinition]))
						{
							CalendarLogExpirationEnforcer.Tracer.TraceDebug<CalendarLogExpirationEnforcer>((long)this.GetHashCode(), "{0}: Last Modified date is missing. Skipping items from here on.", this);
							flag = true;
							break;
						}
						if (!this.EnlistItem(propertyArrayProxy))
						{
							flag = true;
							break;
						}
					}
					base.SysCleanupSubAssistant.ThrottleStoreCallAndCheckForShutdown(base.MailboxDataForTags.MailboxSession.MailboxOwner);
				}
			}
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00031474 File Offset: 0x0002F674
		private bool EnlistItem(PropertyArrayProxy itemProperties)
		{
			VersionedId versionedId = itemProperties[ItemSchema.Id] as VersionedId;
			if (versionedId == null)
			{
				CalendarLogExpirationEnforcer.Tracer.TraceError<CalendarLogExpirationEnforcer>((long)this.GetHashCode(), "{0}: We could not get id of this item. Skipping it.", this);
				return true;
			}
			if (!CalendarLoggingHelper.IsCalendarItem(versionedId.ObjectId))
			{
				return true;
			}
			if (!this.IsTimeToDie(itemProperties, this.AgeLimit))
			{
				CalendarLogExpirationEnforcer.Tracer.TraceDebug<CalendarLogExpirationEnforcer, VersionedId, EnhancedTimeSpan>((long)this.GetHashCode(), "{0}: Item {1} newer than minAgeLimitForFolder {2}.", this, versionedId, this.AgeLimit);
				return false;
			}
			base.TagExpirationExecutor.AddToDoomedHardDeleteList(new ItemData(versionedId, (int)itemProperties[ItemSchema.Size]), true);
			return true;
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00031510 File Offset: 0x0002F710
		private bool IsTimeToDie(PropertyArrayProxy itemProperties, EnhancedTimeSpan ageLimit)
		{
			if (ElcGlobals.ExpireDumpsterRightNow)
			{
				return true;
			}
			object obj = itemProperties[StoreObjectSchema.LastModifiedTime];
			TimeSpan t = base.MailboxDataForTags.UtcNow - (DateTime)((ExDateTime)obj).ToUtc();
			return t >= ageLimit;
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x00031560 File Offset: 0x0002F760
		private EnhancedTimeSpan AgeLimit
		{
			get
			{
				if (this.ageLimit == null)
				{
					if (base.MailboxDataForTags.MailboxSession.COWSettings != null)
					{
						if (!base.MailboxDataForTags.MailboxSession.COWSettings.IsCalendarLoggingEnabled())
						{
							this.ageLimit = new EnhancedTimeSpan?(EnhancedTimeSpan.FromDays(0.0));
							CalendarLogExpirationEnforcer.Tracer.TraceDebug<CalendarLogExpirationEnforcer, EnhancedTimeSpan>((long)this.GetHashCode(), "{0}: Calendar logging is not enabled. We'll set retention period to {1} for calendar items in the root.", this, this.ageLimit.Value);
							return this.ageLimit.Value;
						}
						CalendarLogExpirationEnforcer.Tracer.TraceDebug<CalendarLogExpirationEnforcer>((long)this.GetHashCode(), "{0}: Calendar logging is enabled.", this);
					}
					else
					{
						CalendarLogExpirationEnforcer.Tracer.TraceDebug<CalendarLogExpirationEnforcer>((long)this.GetHashCode(), "{0}: Unable to get CalendarLoggingEnabled setting.", this);
					}
					this.ageLimit = new EnhancedTimeSpan?(CalendarLogExpirationEnforcer.DefaultAgeLimit);
					CalendarLogExpirationEnforcer.Tracer.TraceDebug<CalendarLogExpirationEnforcer, EnhancedTimeSpan>((long)this.GetHashCode(), "{0}: We'll use the default cal period: {1}", this, this.ageLimit.Value);
				}
				return this.ageLimit.Value;
			}
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0003165A File Offset: 0x0002F85A
		protected override void StartPerfCounterCollect()
		{
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0003165C File Offset: 0x0002F85C
		protected override void StopPerfCounterCollect(long timeElapsed)
		{
			ELCPerfmon.TotalExpiredDumpsterItems.IncrementBy(0L);
			StatisticsLogEntry statisticsLogEntry = base.MailboxDataForTags.StatisticsLogEntry;
			statisticsLogEntry.NumberOfItemsDeletedByCalendarLogExpirationEnforcer = statisticsLogEntry.NumberOfItemsDeletedByCalendarLogExpirationEnforcer;
			base.MailboxDataForTags.StatisticsLogEntry.CalendarLogExpirationEnforcerProcessingTime = timeElapsed;
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00031692 File Offset: 0x0002F892
		internal static void ResetRegistrySettings()
		{
			CalendarLogExpirationEnforcer.defaultAgeLimitForCalendarItems = null;
		}

		// Token: 0x040004B5 RID: 1205
		internal const string DefaultAgeLimitForCalendarItemsRegKey = "DefaultAgeLimitForCalendarItems";

		// Token: 0x040004B6 RID: 1206
		internal const int DefaultAgeLimitForCalendarItemsDefaultRegValue = 31;

		// Token: 0x040004B7 RID: 1207
		private const int ItemsExpired = 0;

		// Token: 0x040004B8 RID: 1208
		private static readonly Trace Tracer = ExTraceGlobals.CalendarLogExpirationEnforcerTracer;

		// Token: 0x040004B9 RID: 1209
		private static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;

		// Token: 0x040004BA RID: 1210
		private static EnhancedTimeSpan? defaultAgeLimitForCalendarItems = null;

		// Token: 0x040004BB RID: 1211
		private static readonly PropertyDefinitionArray PropertyColumns = new PropertyDefinitionArray(new PropertyDefinition[]
		{
			ItemSchema.Id,
			StoreObjectSchema.LastModifiedTime,
			ItemSchema.Size
		});

		// Token: 0x040004BC RID: 1212
		private static PropertyDefinition agePropertyDefinition = StoreObjectSchema.LastModifiedTime;

		// Token: 0x040004BD RID: 1213
		private EnhancedTimeSpan? ageLimit = null;
	}
}

using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LazyIndexing
{
	// Token: 0x0200002B RID: 43
	public class LogicalIndexVersionHistory
	{
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00012BD6 File Offset: 0x00010DD6
		internal static ComponentVersion CurrentVersion
		{
			get
			{
				return new ComponentVersion(LogicalIndexVersionHistory.historyOfChanges.Length);
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00012BE4 File Offset: 0x00010DE4
		internal static void Initialize()
		{
			if (LogicalIndexVersionHistory.logicalIndexVersionHistoryDataSlot == -1)
			{
				LogicalIndexVersionHistory.logicalIndexVersionHistoryDataSlot = StoreDatabase.AllocateComponentDataSlot();
			}
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00012BF8 File Offset: 0x00010DF8
		internal static void MountEventHandler(StoreDatabase database)
		{
			database.ComponentData[LogicalIndexVersionHistory.logicalIndexVersionHistoryDataSlot] = new LogicalIndexVersionHistory();
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00012C0F File Offset: 0x00010E0F
		internal static void DismountEventHandler(StoreDatabase database)
		{
			database.ComponentData[LogicalIndexVersionHistory.logicalIndexVersionHistoryDataSlot] = null;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00012C24 File Offset: 0x00010E24
		internal static bool IsAffected(Context context, LogicalIndex logicalIndex)
		{
			int num = logicalIndex.LogicalIndexVersion.CompareTo(LogicalIndexVersionHistory.CurrentVersion);
			if (num > 0)
			{
				return true;
			}
			if (num == 0)
			{
				return false;
			}
			LogicalIndexVersionHistory logicalIndexVersionHistory = context.Database.ComponentData[LogicalIndexVersionHistory.logicalIndexVersionHistoryDataSlot] as LogicalIndexVersionHistory;
			if (logicalIndexVersionHistory.aggregatedHistoryOfChanges == null)
			{
				logicalIndexVersionHistory.aggregatedHistoryOfChanges = LogicalIndexVersionHistory.AggregateChangeEntries(context.Database, LogicalIndexVersionHistory.historyOfChanges);
			}
			LogicalIndexVersionHistory.ChangeEntry<Column> changeEntry = logicalIndexVersionHistory.aggregatedHistoryOfChanges[logicalIndex.LogicalIndexVersion.Value];
			if (changeEntry.IsGlobalChange)
			{
				return true;
			}
			for (int i = 0; i < logicalIndex.Columns.Count; i++)
			{
				Column column = logicalIndex.Columns[i];
				if (changeEntry.Changes.Contains(column))
				{
					return true;
				}
				if (changeEntry.Predicate != null && changeEntry.Predicate(context, logicalIndex, column))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00012D50 File Offset: 0x00010F50
		internal static LogicalIndexVersionHistory.ChangeEntry<Column>[] AggregateChangeEntries(StoreDatabase database, LogicalIndexVersionHistory.ChangeEntry<StorePropTag>[] changes)
		{
			LogicalIndexVersionHistory.ChangeEntry<Column>[] array = new LogicalIndexVersionHistory.ChangeEntry<Column>[LogicalIndexVersionHistory.historyOfChanges.Length];
			LogicalIndexVersionHistory.ChangeEntry<StorePropTag> changeEntry = LogicalIndexVersionHistory.ChangeEntry<StorePropTag>.Empty;
			for (int i = LogicalIndexVersionHistory.historyOfChanges.Length - 1; i >= 0; i--)
			{
				changeEntry = changeEntry.AggregateWith(changes[i]);
				if (changeEntry.IsGlobalChange)
				{
					array[i] = LogicalIndexVersionHistory.ChangeEntry<Column>.Global;
				}
				else
				{
					Func<Context, LogicalIndex, StorePropTag, bool> predicate = changeEntry.Predicate;
					array[i] = new LogicalIndexVersionHistory.ChangeEntry<Column>(false, (from propertyTag in changeEntry.Changes
					select PropertySchema.MapToColumn(database, ObjectType.Message, propertyTag)).ToArray<Column>(), (predicate == null) ? null : new Func<Context, LogicalIndex, Column, bool>(delegate(Context context, LogicalIndex li, Column column)
					{
						ExtendedPropertyColumn extendedPropertyColumn = column as ExtendedPropertyColumn;
						return !(extendedPropertyColumn == null) && predicate(context, li, extendedPropertyColumn.StorePropTag);
					}));
				}
			}
			return array;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00012E18 File Offset: 0x00011018
		internal static LogicalIndexVersionHistory.ChangeEntry<StorePropTag>[] SetIndexVersionHistory(StoreDatabase database, LogicalIndexVersionHistory.ChangeEntry<StorePropTag>[] newHistory)
		{
			LogicalIndexVersionHistory.ChangeEntry<StorePropTag>[] result = LogicalIndexVersionHistory.historyOfChanges;
			LogicalIndexVersionHistory.historyOfChanges = newHistory;
			LogicalIndexVersionHistory logicalIndexVersionHistory = database.ComponentData[LogicalIndexVersionHistory.logicalIndexVersionHistoryDataSlot] as LogicalIndexVersionHistory;
			logicalIndexVersionHistory.aggregatedHistoryOfChanges = LogicalIndexVersionHistory.AggregateChangeEntries(database, LogicalIndexVersionHistory.historyOfChanges);
			return result;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00012F30 File Offset: 0x00011130
		// Note: this type is marked as 'beforefieldinit'.
		static LogicalIndexVersionHistory()
		{
			LogicalIndexVersionHistory.ChangeEntry<StorePropTag>[] array = new LogicalIndexVersionHistory.ChangeEntry<StorePropTag>[27];
			array[0] = LogicalIndexVersionHistory.ChangeEntry<StorePropTag>.Global;
			array[1] = LogicalIndexVersionHistory.ChangeEntry<StorePropTag>.PropertiesChange(new StorePropTag[]
			{
				PropTag.Message.InstanceKey
			});
			array[2] = LogicalIndexVersionHistory.ChangeEntry<StorePropTag>.Global;
			array[3] = LogicalIndexVersionHistory.ChangeEntry<StorePropTag>.PropertiesChange(new StorePropTag[]
			{
				PropTag.Message.DeferredDeliveryTime,
				PropTag.Message.Importance
			});
			array[4] = LogicalIndexVersionHistory.ChangeEntry<StorePropTag>.PropertiesChange(new StorePropTag[]
			{
				PropTag.Message.SentRepresentingEntryId,
				PropTag.Message.SentRepresentingSearchKey,
				PropTag.Message.SentRepresentingAddressType,
				PropTag.Message.SentRepresentingEmailAddress,
				PropTag.Message.SentRepresentingName,
				PropTag.Message.SentRepresentingSimpleDisplayName,
				PropTag.Message.SentRepresentingFlags,
				PropTag.Message.SentRepresentingOrgAddressType,
				PropTag.Message.SentRepresentingOrgEmailAddr,
				PropTag.Message.SentRepresentingSID,
				PropTag.Message.SentRepresentingGuid,
				PropTag.Message.SenderEntryId,
				PropTag.Message.SenderSearchKey,
				PropTag.Message.SenderAddressType,
				PropTag.Message.SenderEmailAddress,
				PropTag.Message.SenderName,
				PropTag.Message.SenderSimpleDisplayName,
				PropTag.Message.SenderFlags,
				PropTag.Message.SenderOrgAddressType,
				PropTag.Message.SenderOrgEmailAddr,
				PropTag.Message.SenderSID,
				PropTag.Message.SenderGuid
			});
			array[5] = LogicalIndexVersionHistory.ChangeEntry<StorePropTag>.PropertiesChange(new StorePropTag[]
			{
				PropTag.Message.ConversationTopic,
				PropTag.Message.ConversationCategories,
				PropTag.Message.ConversationCategoriesMailboxWide,
				PropTag.Message.ConversationHasAttach,
				PropTag.Message.ConversationHasAttachMailboxWide
			});
			array[6] = LogicalIndexVersionHistory.ChangeEntry<StorePropTag>.PropertiesChange(new StorePropTag[]
			{
				PropTag.Message.Subject,
				PropTag.Message.SubjectPrefix,
				PropTag.Message.NormalizedSubject
			});
			array[7] = LogicalIndexVersionHistory.ChangeEntry<StorePropTag>.PropertiesChange(new StorePropTag[]
			{
				PropTag.Message.PreviewUnread
			});
			array[8] = LogicalIndexVersionHistory.ChangeEntry<StorePropTag>.PropertiesChange(new StorePropTag[]
			{
				PropTag.Message.GroupingActions,
				PropTag.Message.ConversationGroupingActions
			});
			array[9] = LogicalIndexVersionHistory.ChangeEntry<StorePropTag>.PropertiesChange(new StorePropTag[]
			{
				PropTag.Message.PersonPhotoContactEntryIdMailboxWide,
				PropTag.Message.PersonIsDistributionListMailboxWide,
				PropTag.Message.PersonGALLinkIDMailboxWide,
				PropTag.Message.PersonMvEmailAddressMailboxWide,
				PropTag.Message.PersonMvEmailDisplayNameMailboxWide,
				PropTag.Message.PersonMvEmailRoutingTypeMailboxWide,
				PropTag.Message.PersonImAddressMailboxWide
			});
			array[10] = LogicalIndexVersionHistory.ChangeEntry<StorePropTag>.PropertiesChange(new StorePropTag[]
			{
				PropTag.Message.PersonCompanyNameMailboxWide,
				PropTag.Message.PersonDisplayNameMailboxWide,
				PropTag.Message.PersonGivenNameMailboxWide,
				PropTag.Message.PersonSurnameMailboxWide,
				PropTag.Message.PersonRelevanceScoreMailboxWide,
				PropTag.Message.PersonHomeCityMailboxWide,
				PropTag.Message.PersonFileAsMailboxWide,
				PropTag.Message.PersonWorkCityMailboxWide,
				PropTag.Message.PersonDisplayNameFirstLastMailboxWide,
				PropTag.Message.PersonDisplayNameLastFirstMailboxWide
			});
			array[11] = LogicalIndexVersionHistory.ChangeEntry<StorePropTag>.PropertiesChange(new StorePropTag[]
			{
				PropTag.Message.InstanceKey
			});
			array[12] = LogicalIndexVersionHistory.ChangeEntry<StorePropTag>.PropertiesChange(new StorePropTag[]
			{
				PropTag.Message.ParentDisplay
			});
			array[13] = LogicalIndexVersionHistory.ChangeEntry<StorePropTag>.ConditionalChange(delegate(Context context, LogicalIndex logicalIndex, StorePropTag unused)
			{
				if (logicalIndex.IndexType != LogicalIndexType.CategoryHeaders)
				{
					return false;
				}
				if (!logicalIndex.MaintainPerUserData)
				{
					return false;
				}
				Column physicalColumnForLogicalColumn = logicalIndex.GetPhysicalColumnForLogicalColumn(PropertySchema.MapToColumn(context.Database, ObjectType.Message, PropTag.Message.CnsetIn));
				return physicalColumnForLogicalColumn == null;
			});
			array[14] = LogicalIndexVersionHistory.ChangeEntry<StorePropTag>.ConditionalChange(delegate(Context context, LogicalIndex logicalIndex, StorePropTag unused)
			{
				if (logicalIndex.IndexType != LogicalIndexType.CategoryHeaders)
				{
					return false;
				}
				Column physicalColumnForLogicalColumn = logicalIndex.GetPhysicalColumnForLogicalColumn(PropertySchema.MapToColumn(context.Database, ObjectType.Message, PropTag.Message.UnreadCountInt64));
				return physicalColumnForLogicalColumn == null;
			});
			array[15] = LogicalIndexVersionHistory.ChangeEntry<StorePropTag>.PropertiesChange(new StorePropTag[]
			{
				PropTag.Message.LocalCommitTime
			});
			array[16] = LogicalIndexVersionHistory.ChangeEntry<StorePropTag>.PropertiesChange(new StorePropTag[]
			{
				PropTag.Message.SentRepresentingSearchKey,
				PropTag.Message.SenderSearchKey,
				PropTag.Message.OriginalSentRepresentingSearchKey,
				PropTag.Message.OriginalSenderSearchKey,
				PropTag.Message.ReceivedRepresentingSearchKey,
				PropTag.Message.ReceivedBySearchKey,
				PropTag.Message.ReadReceiptSearchKey,
				PropTag.Message.ReportSearchKey,
				PropTag.Message.OriginatorSearchKey,
				PropTag.Message.OriginalAuthorSearchKey,
				PropTag.Message.ReportDestinationSearchKey
			});
			array[17] = LogicalIndexVersionHistory.ChangeEntry<StorePropTag>.PropertiesChange(new StorePropTag[]
			{
				PropTag.Message.BodyUnicode,
				PropTag.Message.RtfCompressed,
				PropTag.Message.BodyHtml
			});
			array[18] = LogicalIndexVersionHistory.ChangeEntry<StorePropTag>.PropertiesChange(new StorePropTag[]
			{
				PropTag.Message.ConversationInferredImportanceInternal,
				PropTag.Message.ConversationInferredImportanceOverride,
				PropTag.Message.ConversationInferredUnimportanceInternal,
				PropTag.Message.ConversationInferredImportanceInternalMailboxWide,
				PropTag.Message.ConversationInferredImportanceOverrideMailboxWide,
				PropTag.Message.ConversationInferredUnimportanceInternalMailboxWide
			});
			array[19] = LogicalIndexVersionHistory.ChangeEntry<StorePropTag>.ConditionalChange(delegate(Context context, LogicalIndex logicalIndex, StorePropTag unused)
			{
				if (!logicalIndex.MaintainPerUserData)
				{
					return false;
				}
				Column physicalColumnForLogicalColumn = logicalIndex.GetPhysicalColumnForLogicalColumn(PropertySchema.MapToColumn(context.Database, ObjectType.Message, PropTag.Message.Read));
				if (physicalColumnForLogicalColumn == null)
				{
					physicalColumnForLogicalColumn = logicalIndex.GetPhysicalColumnForLogicalColumn(PropertySchema.MapToColumn(context.Database, ObjectType.Message, PropTag.Message.MessageFlags));
				}
				return physicalColumnForLogicalColumn != null;
			});
			array[20] = LogicalIndexVersionHistory.ChangeEntry<StorePropTag>.PropertiesChange(new StorePropTag[]
			{
				PropTag.Message.DeliveryOrRenewTime,
				PropTag.Message.ConversationMessageDeliveryOrRenewTimeMailboxWide
			});
			array[21] = LogicalIndexVersionHistory.ChangeEntry<StorePropTag>.PropertiesChange(new StorePropTag[]
			{
				PropTag.Message.ConversationFamilyId
			});
			array[22] = LogicalIndexVersionHistory.ChangeEntry<StorePropTag>.PropertiesChange(new StorePropTag[]
			{
				PropTag.Message.FamilyId
			});
			array[23] = LogicalIndexVersionHistory.ChangeEntry<StorePropTag>.PropertiesChange(new StorePropTag[]
			{
				PropTag.Message.RichContent
			});
			array[24] = LogicalIndexVersionHistory.ChangeEntry<StorePropTag>.PropertiesChange(new StorePropTag[]
			{
				PropTag.Message.ConversationMessageRichContentMailboxWide
			});
			array[25] = LogicalIndexVersionHistory.ChangeEntry<StorePropTag>.PropertiesChange(new StorePropTag[]
			{
				PropTag.Message.ConversationMessageDeliveryOrRenewTime
			});
			array[26] = LogicalIndexVersionHistory.ChangeEntry<StorePropTag>.PropertiesChange(new StorePropTag[]
			{
				PropTag.Message.GroupingActions,
				PropTag.Message.PredictedActionsSummary,
				PropTag.Message.PredictedActionsThresholds,
				PropTag.Message.ConversationGroupingActions,
				PropTag.Message.ConversationGroupingActionsMailboxWide,
				PropTag.Message.ConversationPredictedActionsSummary,
				PropTag.Message.ConversationPredictedActionsSummaryMailboxWide
			});
			LogicalIndexVersionHistory.historyOfChanges = array;
			LogicalIndexVersionHistory.logicalIndexVersionHistoryDataSlot = -1;
		}

		// Token: 0x0400013E RID: 318
		private static LogicalIndexVersionHistory.ChangeEntry<StorePropTag>[] historyOfChanges;

		// Token: 0x0400013F RID: 319
		private static int logicalIndexVersionHistoryDataSlot;

		// Token: 0x04000140 RID: 320
		private LogicalIndexVersionHistory.ChangeEntry<Column>[] aggregatedHistoryOfChanges;

		// Token: 0x0200002C RID: 44
		internal class ChangeEntry<TKey>
		{
			// Token: 0x170000B0 RID: 176
			// (get) Token: 0x06000234 RID: 564 RVA: 0x000137BE File Offset: 0x000119BE
			internal static LogicalIndexVersionHistory.ChangeEntry<TKey> Empty
			{
				get
				{
					return LogicalIndexVersionHistory.ChangeEntry<TKey>.empty;
				}
			}

			// Token: 0x170000B1 RID: 177
			// (get) Token: 0x06000235 RID: 565 RVA: 0x000137C5 File Offset: 0x000119C5
			internal static LogicalIndexVersionHistory.ChangeEntry<TKey> Global
			{
				get
				{
					return LogicalIndexVersionHistory.ChangeEntry<TKey>.global;
				}
			}

			// Token: 0x06000236 RID: 566 RVA: 0x000137CC File Offset: 0x000119CC
			internal ChangeEntry(bool globalChange, TKey[] changes, Func<Context, LogicalIndex, TKey, bool> predicate)
			{
				this.globalChange = globalChange;
				this.changes = changes;
				this.predicate = predicate;
			}

			// Token: 0x06000237 RID: 567 RVA: 0x000137E9 File Offset: 0x000119E9
			internal static LogicalIndexVersionHistory.ChangeEntry<TKey> PropertiesChange(params TKey[] changes)
			{
				return new LogicalIndexVersionHistory.ChangeEntry<TKey>(false, changes, null);
			}

			// Token: 0x06000238 RID: 568 RVA: 0x000137F3 File Offset: 0x000119F3
			internal static LogicalIndexVersionHistory.ChangeEntry<TKey> ConditionalChange(Func<Context, LogicalIndex, TKey, bool> predicate)
			{
				return new LogicalIndexVersionHistory.ChangeEntry<TKey>(false, Array<TKey>.Empty, predicate);
			}

			// Token: 0x170000B2 RID: 178
			// (get) Token: 0x06000239 RID: 569 RVA: 0x00013801 File Offset: 0x00011A01
			internal bool IsGlobalChange
			{
				get
				{
					return this.globalChange;
				}
			}

			// Token: 0x170000B3 RID: 179
			// (get) Token: 0x0600023A RID: 570 RVA: 0x00013809 File Offset: 0x00011A09
			internal TKey[] Changes
			{
				get
				{
					return this.changes;
				}
			}

			// Token: 0x170000B4 RID: 180
			// (get) Token: 0x0600023B RID: 571 RVA: 0x00013811 File Offset: 0x00011A11
			internal Func<Context, LogicalIndex, TKey, bool> Predicate
			{
				get
				{
					return this.predicate;
				}
			}

			// Token: 0x0600023C RID: 572 RVA: 0x00013850 File Offset: 0x00011A50
			internal LogicalIndexVersionHistory.ChangeEntry<TKey> AggregateWith(LogicalIndexVersionHistory.ChangeEntry<TKey> otherChange)
			{
				if (this.globalChange || otherChange.globalChange)
				{
					return LogicalIndexVersionHistory.ChangeEntry<TKey>.Global;
				}
				List<TKey> list = new List<TKey>(this.changes);
				list.AddRange(otherChange.changes);
				Func<Context, LogicalIndex, TKey, bool> func = null;
				if (this.predicate != null && otherChange.predicate != null)
				{
					func = ((Context context, LogicalIndex li, TKey key) => this.predicate(context, li, key) || otherChange.predicate(context, li, key));
				}
				else if (this.predicate != null)
				{
					func = this.predicate;
				}
				else if (otherChange.predicate != null)
				{
					func = otherChange.predicate;
				}
				return new LogicalIndexVersionHistory.ChangeEntry<TKey>(false, list.ToArray(), func);
			}

			// Token: 0x04000144 RID: 324
			private static LogicalIndexVersionHistory.ChangeEntry<TKey> empty = new LogicalIndexVersionHistory.ChangeEntry<TKey>(false, Array<TKey>.Empty, null);

			// Token: 0x04000145 RID: 325
			private static LogicalIndexVersionHistory.ChangeEntry<TKey> global = new LogicalIndexVersionHistory.ChangeEntry<TKey>(true, Array<TKey>.Empty, null);

			// Token: 0x04000146 RID: 326
			private bool globalChange;

			// Token: 0x04000147 RID: 327
			private TKey[] changes;

			// Token: 0x04000148 RID: 328
			private Func<Context, LogicalIndex, TKey, bool> predicate;
		}
	}
}

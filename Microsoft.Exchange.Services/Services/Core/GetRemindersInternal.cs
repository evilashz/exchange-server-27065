using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000322 RID: 802
	internal class GetRemindersInternal
	{
		// Token: 0x060016A9 RID: 5801 RVA: 0x0007727C File Offset: 0x0007547C
		public GetRemindersInternal(MailboxSession mailboxSession, ExDateTime reminderWindowStart, ExDateTime reminderWindowEnd, int requestedMaxNumberOfReminders, ReminderTypes reminderQueryType, ReminderGroupType? reminderGroupType)
		{
			this.mailboxSession = mailboxSession;
			this.reminderWindowStart = reminderWindowStart;
			this.reminderWindowEnd = reminderWindowEnd;
			this.requestedMaxNumberOfReminders = requestedMaxNumberOfReminders;
			this.reminderQueryType = reminderQueryType;
			this.reminderRequestGroupType = reminderGroupType;
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x000772B4 File Offset: 0x000754B4
		public ReminderType[] Execute(ExDateTime executionTimeContext)
		{
			this.activeReminders = new LinkedList<ReminderType>();
			this.upcomingReminders = new LinkedList<ReminderType>();
			this.activeExpandedRecurrenceRemindersLists = new List<LinkedList<ReminderType>>();
			this.upcomingExpandedRecurrenceRemindersLists = new List<LinkedList<ReminderType>>();
			this.executionTimeContext = executionTimeContext;
			if (this.reminderWindowEnd.CompareTo(ExDateTime.MinValue.AddDays(1.0)) <= 0)
			{
				this.reminderWindowEnd = ExDateTime.MaxValue;
			}
			if (this.reminderWindowStart > this.reminderWindowEnd)
			{
				throw new ServiceInvalidOperationException((CoreResources.IDs)4006585486U);
			}
			if (this.reminderWindowStart < executionTimeContext)
			{
				ExDateTime exDateTime = executionTimeContext.AddDays(365.0);
				if (exDateTime < this.reminderWindowEnd)
				{
					ExTraceGlobals.GetRemindersCallTracer.TraceDebug<ExDateTime, ExDateTime>((long)this.GetHashCode(), "ReminderWindow begins before Now. Capping RemindersEnd from {0} to {1}", this.reminderWindowEnd, exDateTime);
					this.reminderWindowEnd = exDateTime;
				}
			}
			else
			{
				ExDateTime exDateTime2 = this.reminderWindowStart.AddDays(365.0);
				if (exDateTime2 < this.reminderWindowEnd)
				{
					ExTraceGlobals.GetRemindersCallTracer.TraceDebug<ExDateTime, ExDateTime>((long)this.GetHashCode(), "ReminderWindow begins after Now. Capping RemindersEnd from {0} to {1}", this.reminderWindowEnd, exDateTime2);
					this.reminderWindowEnd = exDateTime2;
				}
			}
			this.maxReturnedReminders = ((this.requestedMaxNumberOfReminders > 0 && this.requestedMaxNumberOfReminders < 200) ? this.requestedMaxNumberOfReminders : 200);
			this.upcomingRemindersSlots = (int)(0.25 * (double)this.maxReturnedReminders);
			this.activeRemindersSlots = this.maxReturnedReminders - this.upcomingRemindersSlots;
			QueryFilter filter = this.CreateFilter();
			this.QueryReminders(filter);
			return this.TriageReminders();
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x0007744C File Offset: 0x0007564C
		protected static LinkedList<ReminderType> MergeReminderLists(LinkedList<ReminderType> mergeTarget, LinkedList<ReminderType> sourceList, bool pruneFromStart, int maxListSize)
		{
			LinkedListNode<ReminderType> linkedListNode = mergeTarget.First;
			LinkedListNode<ReminderType> linkedListNode2 = sourceList.First;
			if (linkedListNode == null)
			{
				return sourceList;
			}
			if (linkedListNode2 == null)
			{
				return mergeTarget;
			}
			while (linkedListNode != null || linkedListNode2 != null)
			{
				if (linkedListNode2 == null)
				{
					return mergeTarget;
				}
				if (linkedListNode == null)
				{
					mergeTarget.AddLast(linkedListNode2.Value);
					linkedListNode2 = linkedListNode2.Next;
				}
				else if (linkedListNode2.Value.ReminderDateTime.CompareTo(linkedListNode.Value.ReminderDateTime) < 0)
				{
					mergeTarget.AddBefore(linkedListNode, linkedListNode2.Value);
					linkedListNode2 = linkedListNode2.Next;
				}
				else
				{
					linkedListNode = linkedListNode.Next;
				}
				if (mergeTarget.Count > maxListSize)
				{
					if (pruneFromStart)
					{
						mergeTarget.RemoveFirst();
					}
					else
					{
						if (linkedListNode == mergeTarget.Last)
						{
							linkedListNode = null;
						}
						mergeTarget.RemoveLast();
					}
				}
			}
			return mergeTarget;
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x00077504 File Offset: 0x00075704
		private QueryFilter CreateFilter()
		{
			QueryFilter queryFilter = new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ItemSchema.ReminderNextTime, this.reminderWindowStart);
			QueryFilter queryFilter2 = new BitMaskFilter(CalendarItemBaseSchema.AppointmentState, 1UL, false);
			QueryFilter queryFilter3;
			switch (this.reminderQueryType)
			{
			case ReminderTypes.All:
				queryFilter3 = queryFilter;
				break;
			case ReminderTypes.Current:
			{
				QueryFilter queryFilter4 = new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, CalendarItemInstanceSchema.EndTime, this.reminderWindowStart);
				queryFilter3 = new OrFilter(new QueryFilter[]
				{
					queryFilter,
					queryFilter4
				});
				queryFilter3 = new OrFilter(new QueryFilter[]
				{
					queryFilter3,
					queryFilter2
				});
				break;
			}
			case ReminderTypes.Old:
				queryFilter3 = new AndFilter(new QueryFilter[]
				{
					queryFilter,
					new NotFilter(queryFilter2)
				});
				break;
			default:
				throw new ServiceInvalidOperationException(CoreResources.IDs.RuleErrorInvalidValue);
			}
			return queryFilter3;
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x000775D8 File Offset: 0x000757D8
		private void QueryReminders(QueryFilter filter)
		{
			MailboxId mailboxId = new MailboxId(this.mailboxSession);
			using (SearchFolder searchFolder = SearchFolder.Bind(this.mailboxSession, DefaultFolderType.Reminders))
			{
				using (QueryResult queryResult = searchFolder.ItemQuery(ItemQueryType.None, null, GetRemindersInternal.OrderRemindersBy, GetRemindersInternal.QueryProperties))
				{
					queryResult.SeekToCondition(SeekReference.OriginBeginning, filter, SeekToConditionFlags.AllowExtendedFilters);
					bool flag = true;
					int num = 0;
					while (flag)
					{
						IStorePropertyBag[] propertyBags = queryResult.GetPropertyBags(5000);
						if (propertyBags == null || propertyBags.Length == 0)
						{
							break;
						}
						foreach (IStorePropertyBag reminder in propertyBags)
						{
							ReminderGroupType reminderGroup;
							if (this.ReminderMatchesRequest(reminder, out flag, out reminderGroup))
							{
								this.AddReminder(reminder, mailboxId, reminderGroup);
							}
							if (!flag)
							{
								return;
							}
							num++;
							if (num >= 5000)
							{
								ExTraceGlobals.GetRemindersCallTracer.TraceWarning<Guid>((long)this.GetHashCode(), "GetReminders ItemsProcessed limit reached. MailboxGuid:{0}", this.mailboxSession.MailboxGuid);
								return;
							}
						}
					}
				}
			}
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x000776EC File Offset: 0x000758EC
		private ReminderType[] TriageReminders()
		{
			if (this.activeExpandedRecurrenceRemindersLists.Count > 0)
			{
				this.MergeRemindersCollections();
			}
			ExTraceGlobals.GetRemindersCallTracer.TraceDebug<int, int, int>((long)this.GetHashCode(), "Number of active reminders before triage: {0}. Number of upcoming reminders before triage: {1}. MaxNumber returned: {2}", this.activeReminders.Count, this.upcomingReminders.Count, this.maxReturnedReminders);
			ReminderType[] array;
			if (this.activeReminders.Count + this.upcomingReminders.Count <= this.maxReturnedReminders)
			{
				array = new ReminderType[this.activeReminders.Count + this.upcomingReminders.Count];
				this.activeReminders.CopyTo(array, 0);
				this.upcomingReminders.CopyTo(array, this.activeReminders.Count);
				ExTraceGlobals.GetRemindersCallTracer.TraceDebug((long)this.GetHashCode(), "Total number of returned reminders lower than max. No triage necessary.");
			}
			else
			{
				array = new ReminderType[this.maxReturnedReminders];
				int num;
				if (this.activeReminders.Count > this.activeRemindersSlots && this.upcomingReminders.Count > this.upcomingRemindersSlots)
				{
					num = this.maxReturnedReminders - this.upcomingRemindersSlots;
				}
				else if (this.activeReminders.Count > this.activeRemindersSlots)
				{
					num = this.maxReturnedReminders - this.upcomingReminders.Count;
				}
				else
				{
					num = this.activeReminders.Count;
				}
				ExTraceGlobals.GetRemindersCallTracer.TraceDebug<int>((long)this.GetHashCode(), "Triage is necessary. Index in returned array where upcoming reminders start: {0}. ", num);
				LinkedListNode<ReminderType> linkedListNode = this.activeReminders.Last;
				for (int i = num - 1; i >= 0; i--)
				{
					array[i] = linkedListNode.Value;
					linkedListNode = linkedListNode.Previous;
				}
				linkedListNode = this.upcomingReminders.First;
				for (int j = num; j < array.Length; j++)
				{
					array[j] = linkedListNode.Value;
					linkedListNode = linkedListNode.Next;
				}
			}
			return array;
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x000778A4 File Offset: 0x00075AA4
		private void MergeRemindersCollections()
		{
			int num = 0;
			for (int i = 1; i < this.activeExpandedRecurrenceRemindersLists.Count; i *= 2)
			{
				int num2 = 0;
				while (num2 + i < this.activeExpandedRecurrenceRemindersLists.Count)
				{
					this.activeExpandedRecurrenceRemindersLists[num2] = GetRemindersInternal.MergeReminderLists(this.activeExpandedRecurrenceRemindersLists[num2], this.activeExpandedRecurrenceRemindersLists[num2 + i], true, this.maxReturnedReminders);
					this.upcomingExpandedRecurrenceRemindersLists[num2] = GetRemindersInternal.MergeReminderLists(this.upcomingExpandedRecurrenceRemindersLists[num2], this.upcomingExpandedRecurrenceRemindersLists[num2 + i], false, this.maxReturnedReminders);
					num++;
					num2 += 2 * i;
				}
			}
			ExTraceGlobals.GetRemindersCallTracer.TraceDebug((long)this.GetHashCode(), "Finished {0} merge operations - merging lists for {1} recurring reminders. Total number of active recurring items: {2}. Total number of upcoming recurring items: {3}", new object[]
			{
				num,
				this.activeExpandedRecurrenceRemindersLists.Count,
				(this.activeExpandedRecurrenceRemindersLists.Count > 0) ? this.activeExpandedRecurrenceRemindersLists[0].Count : -1,
				(this.upcomingExpandedRecurrenceRemindersLists.Count > 0) ? this.upcomingExpandedRecurrenceRemindersLists[0].Count : -1
			});
			this.activeReminders = GetRemindersInternal.MergeReminderLists(this.activeReminders, this.activeExpandedRecurrenceRemindersLists[0], true, this.maxReturnedReminders);
			this.upcomingReminders = GetRemindersInternal.MergeReminderLists(this.upcomingReminders, this.upcomingExpandedRecurrenceRemindersLists[0], false, this.maxReturnedReminders);
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x00077A28 File Offset: 0x00075C28
		private bool ReminderMatchesRequest(IStorePropertyBag reminder, out bool fetchMoreRows, out ReminderGroupType reminderGroup)
		{
			ExDateTime valueOrDefault = reminder.GetValueOrDefault<ExDateTime>(ItemSchema.ReminderNextTime, ExDateTime.MinValue);
			ExDateTime valueOrDefault2 = reminder.GetValueOrDefault<ExDateTime>(CalendarItemInstanceSchema.EndTime, ExDateTime.MinValue);
			bool flag = !reminder.GetValueOrDefault<bool>(CalendarItemBaseSchema.IsMeeting, false);
			fetchMoreRows = true;
			reminderGroup = ReminderGroupType.Calendar;
			if (valueOrDefault == ExDateTime.MinValue)
			{
				ExTraceGlobals.GetRemindersCallTracer.TraceDebug((long)this.GetHashCode(), "Could not retrieve ReminderTime from item. ");
				return false;
			}
			string valueOrDefault3 = reminder.GetValueOrDefault<string>(StoreObjectSchema.ItemClass, string.Empty);
			if (ObjectClass.IsCalendarItemCalendarItemOccurrenceOrRecurrenceException(valueOrDefault3))
			{
				reminderGroup = ReminderGroupType.Calendar;
			}
			else
			{
				if (!ObjectClass.IsTask(valueOrDefault3))
				{
					return false;
				}
				reminderGroup = ReminderGroupType.Task;
			}
			if (this.reminderRequestGroupType != null && this.reminderRequestGroupType != reminderGroup)
			{
				return false;
			}
			switch (this.reminderQueryType)
			{
			case ReminderTypes.All:
				if (valueOrDefault > this.reminderWindowEnd)
				{
					fetchMoreRows = false;
					return false;
				}
				return true;
			case ReminderTypes.Current:
				if (valueOrDefault > this.reminderWindowEnd)
				{
					fetchMoreRows = false;
					return false;
				}
				return flag || valueOrDefault >= this.reminderWindowStart || valueOrDefault2 >= this.reminderWindowStart;
			case ReminderTypes.Old:
				return !flag && valueOrDefault < this.reminderWindowEnd && valueOrDefault2 < this.reminderWindowEnd;
			default:
				throw new ServiceInvalidOperationException(CoreResources.IDs.RuleErrorInvalidValue);
			}
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x00077B8C File Offset: 0x00075D8C
		private void AddReminder(IStorePropertyBag reminder, MailboxId mailboxId, ReminderGroupType reminderGroup)
		{
			VersionedId versionedId = (VersionedId)reminder.TryGetProperty(ItemSchema.Id);
			if (reminderGroup != ReminderGroupType.Calendar && reminderGroup != ReminderGroupType.Task)
			{
				ExTraceGlobals.GetRemindersCallTracer.TraceDebug<ReminderGroupType>((long)this.GetHashCode(), "Unexpectedly encountered not supported reminder group: {0}", reminderGroup);
				return;
			}
			ExDateTime valueOrDefault = reminder.GetValueOrDefault<ExDateTime>(ItemSchema.ReminderNextTime, ExDateTime.MinValue);
			bool valueOrDefault2 = reminder.GetValueOrDefault<bool>(ItemSchema.ReminderIsSet, false);
			if (reminderGroup == ReminderGroupType.Calendar)
			{
				CalendarItemType valueOrDefault3 = reminder.GetValueOrDefault<CalendarItemType>(CalendarItemBaseSchema.CalendarItemType, CalendarItemType.Single);
				if (valueOrDefault3 == CalendarItemType.RecurringMaster)
				{
					this.AddRecurringReminder(reminder, versionedId, valueOrDefault, mailboxId, valueOrDefault2);
					return;
				}
			}
			if (valueOrDefault2)
			{
				this.AddSingleReminder(reminder, valueOrDefault, versionedId, mailboxId, reminderGroup);
				return;
			}
			ExTraceGlobals.GetRemindersCallTracer.TraceDebug<VersionedId>((long)this.GetHashCode(), "Unexpectedly encountered non-recurring item in reminders table without ReminderIsSet. ItemId: {0}", versionedId);
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x00077C30 File Offset: 0x00075E30
		private void AddSingleReminder(IStorePropertyBag reminder, ExDateTime reminderTime, VersionedId itemId, MailboxId mailboxId, ReminderGroupType reminderGroup)
		{
			ReminderType reminderType = new ReminderType();
			if (this.executionTimeContext.CompareTo(reminderTime) >= 0)
			{
				this.activeReminders.AddLast(reminderType);
				if (this.activeReminders.Count > this.maxReturnedReminders)
				{
					this.activeReminders.RemoveFirst();
				}
			}
			else
			{
				if (this.upcomingReminders.Count > this.maxReturnedReminders)
				{
					return;
				}
				this.upcomingReminders.AddLast(reminderType);
			}
			string uid;
			ICalendar.UidProperty.TryGetUidFromPropertyBag(reminder, out uid);
			reminderType.ItemId = IdConverter.GetItemIdFromStoreId(itemId, mailboxId);
			reminderType.UID = uid;
			reminderType.Subject = reminder.GetValueOrDefault<string>(ItemSchema.Subject, string.Empty);
			reminderType.Location = reminder.GetValueOrDefault<string>(CalendarItemBaseSchema.Location, string.Empty);
			reminderType.ReminderDateTime = reminderTime;
			reminderType.StartDateTime = reminder.GetValueOrDefault<ExDateTime>(CalendarItemInstanceSchema.StartTime, ExDateTime.MinValue);
			reminderType.EndDateTime = reminder.GetValueOrDefault<ExDateTime>(CalendarItemInstanceSchema.EndTime, ExDateTime.MinValue);
			reminderType.ReminderGroup = reminderGroup;
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x00077D28 File Offset: 0x00075F28
		private void AddRecurringReminder(IStorePropertyBag recurringMasterItem, VersionedId masterItemId, ExDateTime masterReminderNextTime, MailboxId mailboxId, bool isReminderSet)
		{
			ExDateTime exDateTime = (masterReminderNextTime > this.reminderWindowStart) ? masterReminderNextTime : this.reminderWindowStart;
			if (exDateTime > this.reminderWindowEnd)
			{
				ExTraceGlobals.GetRemindersCallTracer.TraceDebug((long)this.GetHashCode(), "occurrenceInfoListStartTime is after remindersWindowEnd. No Occurrences expanded. ItemId: {0}, occurrenceInfoListStartTime: {1}, remindersWindowStart: {2}, remindersWindowEnd: {3}", new object[]
				{
					masterItemId,
					exDateTime,
					this.reminderWindowStart,
					this.reminderWindowEnd
				});
				return;
			}
			LinkedList<ReminderType> linkedList = new LinkedList<ReminderType>();
			LinkedList<ReminderType> linkedList2 = new LinkedList<ReminderType>();
			ItemId itemIdFromStoreId = IdConverter.GetItemIdFromStoreId(masterItemId, mailboxId);
			ExDateTime t = this.reminderWindowEnd;
			try
			{
				ExTraceGlobals.GetRemindersCallTracer.TraceDebug<StoreObjectId>((long)this.GetHashCode(), "Processing Recurring Master Calendar Item. ItemId: {0}", masterItemId.ObjectId);
				InternalRecurrence internalRecurrence = InternalRecurrence.FromMasterPropertyBag(recurringMasterItem, this.mailboxSession, masterItemId);
				if (internalRecurrence == null)
				{
					ExTraceGlobals.GetRemindersCallTracer.TraceError<VersionedId>((long)this.GetHashCode(), "There was a problem instantiating an InternalRecurrence from the recurring master. ItemId: {0}", masterItemId);
				}
				else
				{
					if (!isReminderSet)
					{
						if (!internalRecurrence.HasModifiedOccurrences())
						{
							return;
						}
						ExTraceGlobals.GetRemindersCallTracer.TraceDebug<StoreObjectId>((long)this.GetHashCode(), "Processing list of exceptions for recurring calendar item without ReminderIsSet. ItemId: {0}", masterItemId.ObjectId);
						IList<OccurrenceInfo> modifiedOccurrences = internalRecurrence.GetModifiedOccurrences();
						for (int i = modifiedOccurrences.Count - 1; i >= 0; i--)
						{
							ExceptionInfo exceptionInfo = (ExceptionInfo)modifiedOccurrences[i];
							if (!(exceptionInfo.StartTime > this.reminderWindowEnd))
							{
								if (t <= exDateTime || linkedList.Count > 0)
								{
									break;
								}
								if ((exceptionInfo.ModificationType & ModificationType.Reminder) == ModificationType.Reminder && exceptionInfo.PropertyBag.GetValueOrDefault<bool>(ItemSchema.ReminderIsSet, false))
								{
									OccurrenceInfo firstOccurrenceAfterDate = internalRecurrence.GetFirstOccurrenceAfterDate(exceptionInfo.StartTime);
									if (firstOccurrenceAfterDate != null)
									{
										ExDateTime nominalReminderTimeForOccurrence = Reminder.GetNominalReminderTimeForOccurrence(recurringMasterItem, firstOccurrenceAfterDate);
										if (nominalReminderTimeForOccurrence.CompareTo(masterReminderNextTime) <= 0)
										{
											ExTraceGlobals.GetRemindersCallTracer.TraceDebug<ExDateTime, ExDateTime, StoreObjectId>((long)this.GetHashCode(), "Skipping exception with reminder set, as the nominal reminder next time forthe next occurrence ({0}) is smaller than the ReminderNextTime on the master ({1}). ItemId: {2}", nominalReminderTimeForOccurrence, masterReminderNextTime, masterItemId.ObjectId);
											goto IL_21A;
										}
										if (this.executionTimeContext.CompareTo(nominalReminderTimeForOccurrence) > 0)
										{
											ExTraceGlobals.GetRemindersCallTracer.TraceDebug<ExDateTime, ExDateTime, StoreObjectId>((long)this.GetHashCode(), "Skipping exception with reminder set, as the nominal reminder next time forthe next occurrence ({0}) is before the current time. MasterReminderNextTime: {1}. ItemId: {2}", nominalReminderTimeForOccurrence, masterReminderNextTime, masterItemId.ObjectId);
											goto IL_21A;
										}
									}
									t = this.AddReminderServiceObject(recurringMasterItem, internalRecurrence, mailboxId, itemIdFromStoreId, exceptionInfo, linkedList, linkedList2, masterReminderNextTime);
								}
							}
							IL_21A:;
						}
					}
					else
					{
						ExTraceGlobals.GetRemindersCallTracer.TraceDebug<StoreObjectId>((long)this.GetHashCode(), "Expanding occurrence list for recurring calendar item with ReminderIsSet. ItemId: {0}", masterItemId.ObjectId);
						OccurrenceInfo occurrenceInfo = internalRecurrence.GetLastOccurrenceBeforeDate(this.reminderWindowEnd);
						OccurrenceInfo firstOccurrence = internalRecurrence.GetFirstOccurrence();
						while (occurrenceInfo != null && linkedList.Count == 0 && t > exDateTime)
						{
							t = this.AddReminderServiceObject(recurringMasterItem, internalRecurrence, mailboxId, itemIdFromStoreId, occurrenceInfo, linkedList, linkedList2, masterReminderNextTime);
							if (occurrenceInfo.OccurrenceDateId == firstOccurrence.OccurrenceDateId)
							{
								break;
							}
							occurrenceInfo = internalRecurrence.GetPreviousOccurrence(occurrenceInfo);
						}
					}
					ExTraceGlobals.GetRemindersCallTracer.TraceDebug((long)this.GetHashCode(), "Expanding recurring meeting resulted in {0} active and {1} upcoming reminders. Total lists count: {2} ItemId: {3}", new object[]
					{
						linkedList.Count,
						linkedList2.Count,
						this.activeExpandedRecurrenceRemindersLists.Count,
						masterItemId
					});
					this.activeExpandedRecurrenceRemindersLists.Add(linkedList);
					this.upcomingExpandedRecurrenceRemindersLists.Add(linkedList2);
				}
			}
			catch (StoragePermanentException ex)
			{
				ExTraceGlobals.GetRemindersCallTracer.TraceError<string>((long)this.GetHashCode(), "Unable to retrieve calendar occurence due to StoragePermanentException. Message: {0}", ex.Message);
			}
			catch (StorageTransientException ex2)
			{
				ExTraceGlobals.GetRemindersCallTracer.TraceError<string>((long)this.GetHashCode(), "Unable to retrieve calendar occurence due to StorageTransientException. Message: {0}", ex2.Message);
			}
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x000780C8 File Offset: 0x000762C8
		private ExDateTime AddReminderServiceObject(IStorePropertyBag recurringMasterItem, Recurrence masterRecurrence, MailboxId mailboxId, ItemId masterItemEwsId, OccurrenceInfo occurrence, LinkedList<ReminderType> activeExpandedRecurrenceReminders, LinkedList<ReminderType> upcomingExpandedRecurrenceReminders, ExDateTime masterReminderTime)
		{
			if (!masterRecurrence.IsValidOccurrenceId(occurrence.OccurrenceDateId))
			{
				ExTraceGlobals.GetRemindersCallTracer.TraceError<ItemId, string, string>((long)this.GetHashCode(), "OccurrenceId returned for Master is invalid. MasterId: {0}, OccurrenceId: {1} OccurrenceStartTime: {2}", masterItemEwsId, (occurrence.VersionedId == null) ? "is null" : occurrence.VersionedId.ToBase64String(), occurrence.StartTime.ToString());
				return this.reminderWindowEnd;
			}
			if (masterRecurrence.IsOccurrenceDeleted(occurrence.OccurrenceDateId))
			{
				return this.reminderWindowEnd;
			}
			string uid;
			ICalendar.UidProperty.TryGetUidFromPropertyBag(recurringMasterItem, out uid);
			ReminderType reminderType = new ReminderType();
			reminderType.ItemId = IdConverter.GetItemIdFromStoreId(occurrence.VersionedId, mailboxId);
			reminderType.UID = uid;
			reminderType.RecurringMasterItemId = masterItemEwsId;
			reminderType.StartDateTime = occurrence.StartTime;
			reminderType.EndDateTime = occurrence.EndTime;
			reminderType.Subject = recurringMasterItem.GetValueOrDefault<string>(ItemSchema.Subject, string.Empty);
			reminderType.Location = recurringMasterItem.GetValueOrDefault<string>(CalendarItemBaseSchema.Location, string.Empty);
			ExDateTime nominalReminderTimeForOccurrence = Reminder.GetNominalReminderTimeForOccurrence(recurringMasterItem, occurrence);
			ExceptionInfo exceptionInfo = occurrence as ExceptionInfo;
			if (exceptionInfo != null)
			{
				if ((exceptionInfo.ModificationType & ModificationType.Reminder) == ModificationType.Reminder && !exceptionInfo.PropertyBag.GetValueOrDefault<bool>(ItemSchema.ReminderIsSet, false))
				{
					return this.reminderWindowEnd;
				}
				if ((exceptionInfo.ModificationType & ModificationType.Subject) == ModificationType.Subject)
				{
					reminderType.Subject = exceptionInfo.PropertyBag.GetValueOrDefault<string>(ItemSchema.Subject, string.Empty);
				}
				if ((exceptionInfo.ModificationType & ModificationType.Location) == ModificationType.Location)
				{
					reminderType.Location = exceptionInfo.PropertyBag.GetValueOrDefault<string>(CalendarItemBaseSchema.Location, string.Empty);
				}
			}
			ExDateTime exDateTime;
			if (nominalReminderTimeForOccurrence.CompareTo(masterReminderTime) <= 0)
			{
				exDateTime = masterReminderTime;
			}
			else
			{
				exDateTime = nominalReminderTimeForOccurrence;
			}
			reminderType.ReminderDateTime = exDateTime;
			if (this.executionTimeContext.CompareTo(exDateTime) >= 0)
			{
				activeExpandedRecurrenceReminders.AddFirst(reminderType);
			}
			else
			{
				upcomingExpandedRecurrenceReminders.AddFirst(reminderType);
				if (upcomingExpandedRecurrenceReminders.Count > this.maxReturnedReminders)
				{
					upcomingExpandedRecurrenceReminders.RemoveLast();
				}
			}
			return exDateTime;
		}

		// Token: 0x04000F3A RID: 3898
		private const int MaxReturnedReminders = 200;

		// Token: 0x04000F3B RID: 3899
		private const int MaxEndTimeFromTodayInDays = 365;

		// Token: 0x04000F3C RID: 3900
		private const int MaxItemsProcessed = 5000;

		// Token: 0x04000F3D RID: 3901
		private const double UpcomingVSActiveRemindersRatio = 0.25;

		// Token: 0x04000F3E RID: 3902
		private static readonly PropertyDefinition[] ReminderItemProperties = new PropertyDefinition[]
		{
			ItemSchema.Id,
			StoreObjectSchema.ItemClass,
			ItemSchema.Subject,
			ItemSchema.Codepage,
			ItemSchema.ReminderIsSet,
			ItemSchema.ReminderDueBy,
			ItemSchema.ReminderNextTime,
			ItemSchema.ReminderMinutesBeforeStart,
			CalendarItemInstanceSchema.StartTime,
			CalendarItemInstanceSchema.EndTime,
			CalendarItemBaseSchema.Location,
			CalendarItemBaseSchema.IsMeeting,
			CalendarItemBaseSchema.CalendarItemType,
			CalendarItemBaseSchema.CleanGlobalObjectId
		};

		// Token: 0x04000F3F RID: 3903
		private static readonly ICollection<PropertyDefinition> QueryProperties = GetRemindersInternal.ReminderItemProperties.Union(InternalRecurrence.RequiredRecurrenceProperties);

		// Token: 0x04000F40 RID: 3904
		private static readonly SortBy[] OrderRemindersBy = new SortBy[]
		{
			new SortBy(ItemSchema.ReminderNextTime, SortOrder.Ascending)
		};

		// Token: 0x04000F41 RID: 3905
		private static readonly ExDateTime jsonSerializerMinDateTimeValue = new ExDateTime(ExTimeZone.UtcTimeZone, new DateTime(1970, 1, 1));

		// Token: 0x04000F42 RID: 3906
		private readonly MailboxSession mailboxSession;

		// Token: 0x04000F43 RID: 3907
		private readonly ExDateTime reminderWindowStart;

		// Token: 0x04000F44 RID: 3908
		private readonly ReminderTypes reminderQueryType;

		// Token: 0x04000F45 RID: 3909
		private readonly ReminderGroupType? reminderRequestGroupType;

		// Token: 0x04000F46 RID: 3910
		private readonly int requestedMaxNumberOfReminders;

		// Token: 0x04000F47 RID: 3911
		private ExDateTime reminderWindowEnd;

		// Token: 0x04000F48 RID: 3912
		private LinkedList<ReminderType> activeReminders;

		// Token: 0x04000F49 RID: 3913
		private LinkedList<ReminderType> upcomingReminders;

		// Token: 0x04000F4A RID: 3914
		private List<LinkedList<ReminderType>> activeExpandedRecurrenceRemindersLists;

		// Token: 0x04000F4B RID: 3915
		private List<LinkedList<ReminderType>> upcomingExpandedRecurrenceRemindersLists;

		// Token: 0x04000F4C RID: 3916
		private ExDateTime executionTimeContext;

		// Token: 0x04000F4D RID: 3917
		private int maxReturnedReminders;

		// Token: 0x04000F4E RID: 3918
		private int upcomingRemindersSlots;

		// Token: 0x04000F4F RID: 3919
		private int activeRemindersSlots;
	}
}

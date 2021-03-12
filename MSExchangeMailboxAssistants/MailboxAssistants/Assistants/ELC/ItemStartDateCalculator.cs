using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common;
using Microsoft.Exchange.InfoWorker.Common.ELC;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000046 RID: 70
	internal class ItemStartDateCalculator
	{
		// Token: 0x0600028D RID: 653 RVA: 0x0000EFFA File Offset: 0x0000D1FA
		internal ItemStartDateCalculator(PropertyIndexHolder propertyIndexHolder, string folderDisplayName, DefaultFolderType folderType, MailboxSession mailboxSession, Trace tracer)
		{
			this.propertyIndexHolder = propertyIndexHolder;
			this.folderDisplayName = folderDisplayName;
			this.folderType = folderType;
			this.mailboxSession = mailboxSession;
			this.tracer = tracer;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000F027 File Offset: 0x0000D227
		internal DateTime GetStartDateForTag(VersionedId itemId, string itemClass, object[] itemProperties, DateTime? existingStartDate, bool itemMoved)
		{
			if (this.folderType != DefaultFolderType.DeletedItems)
			{
				return this.GetStartDate(itemId, itemClass, itemProperties);
			}
			if (itemMoved || existingStartDate == null)
			{
				return (DateTime)ExDateTime.Now;
			}
			return existingStartDate.Value;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000F05C File Offset: 0x0000D25C
		internal DateTime GetStartDate(VersionedId itemId, string itemClass, object[] itemProperties)
		{
			DateTime d = DateTime.MinValue;
			if (this.folderType != DefaultFolderType.DeletedItems && ObjectClass.IsCalendarItem(itemClass))
			{
				this.tracer.TraceDebug<object, VersionedId>((long)this.GetHashCode(), "{0}: Calculating start date of calendar item {1} based on special criteria.", TraceContext.Get(), itemId);
				d = this.GetStartDateOfCalendarItem(itemId, itemProperties);
			}
			else if (this.folderType != DefaultFolderType.DeletedItems && ObjectClass.IsTask(itemClass))
			{
				this.tracer.TraceDebug<object, VersionedId>((long)this.GetHashCode(), "{0}: Calculating start date of task item {1} based on special criteria.", TraceContext.Get(), itemId);
				d = this.GetStartDateOfTaskItem(itemId, itemProperties);
			}
			else
			{
				this.tracer.TraceDebug<object, VersionedId>((long)this.GetHashCode(), "{0}: Calculating start date of item {1} based on delivery date", TraceContext.Get(), itemId);
				d = this.GetStartDateFromCreationDate(itemProperties);
			}
			if (!(d == DateTime.MinValue))
			{
				return d.ToUniversalTime();
			}
			return DateTime.MinValue;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000F120 File Offset: 0x0000D320
		private DateTime GetStartDateOfCalendarItem(VersionedId id, object[] itemProperties)
		{
			DateTime? calStartDateFromView = this.GetCalStartDateFromView(itemProperties);
			if (calStartDateFromView != null)
			{
				return calStartDateFromView.Value;
			}
			return this.OpenCalItemToGetStartDate(id);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000F150 File Offset: 0x0000D350
		private DateTime? GetCalStartDateFromView(object[] itemProperties)
		{
			if (this.folderType == DefaultFolderType.Calendar)
			{
				if (ElcMailboxHelper.Exists(itemProperties[this.propertyIndexHolder.CalendarTypeIndex]) && ElcMailboxHelper.Exists(itemProperties[this.propertyIndexHolder.EndDateIndex]) && (CalendarItemType)itemProperties[this.propertyIndexHolder.CalendarTypeIndex] == CalendarItemType.Single)
				{
					this.tracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: Using end date property in view to calculate start date of item in Folder: {1}.", TraceContext.Get(), this.folderDisplayName);
					return new DateTime?((DateTime)((ExDateTime)itemProperties[this.propertyIndexHolder.EndDateIndex]));
				}
				if (!ElcMailboxHelper.Exists(itemProperties[this.propertyIndexHolder.CalendarTypeIndex]))
				{
					this.tracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: Calendar item in Folder: {1} is corrupt.", TraceContext.Get(), this.folderDisplayName);
					return new DateTime?(ItemStartDateCalculator.startDateForCorruptItems);
				}
			}
			return null;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000F230 File Offset: 0x0000D430
		private DateTime OpenCalItemToGetStartDate(VersionedId id)
		{
			Exception ex = null;
			try
			{
				this.tracer.TraceDebug<object, VersionedId, string>((long)this.GetHashCode(), "{0}: Going to open item {1} to calculate its start date in Folder: {2}.", TraceContext.Get(), id, this.folderDisplayName);
				using (Item item = Item.Bind(this.mailboxSession, id, ItemBindOption.LoadRequiredPropertiesOnly))
				{
					if (!(item is CalendarItem))
					{
						this.tracer.TraceDebug((long)this.GetHashCode(), "{0}: Item '{1}' in folder '{2}' is supposed to be Calendar item, but is of type '{3}'. Corrupt item.", new object[]
						{
							TraceContext.Get(),
							id,
							this.folderDisplayName,
							item.GetType()
						});
						return ItemStartDateCalculator.startDateForCorruptItems;
					}
					CalendarItem calendarItem = (CalendarItem)item;
					if (calendarItem.Recurrence == null)
					{
						return (DateTime)calendarItem.EndTime;
					}
					if (calendarItem.Recurrence.Range is NoEndRecurrenceRange)
					{
						return DateTime.MinValue;
					}
					if (calendarItem.Recurrence.Range is NumberedRecurrenceRange || calendarItem.Recurrence.Range is EndDateRecurrenceRange)
					{
						OccurrenceInfo lastOccurrence = calendarItem.Recurrence.GetLastOccurrence();
						return (DateTime)lastOccurrence.EndTime;
					}
				}
			}
			catch (CorruptDataException ex2)
			{
				this.tracer.TraceError((long)this.GetHashCode(), "{0}: CorruptDataException thrown for calendar Item '{1}' in folder '{2}' - Corrupt item. Exception: {3}", new object[]
				{
					TraceContext.Get(),
					id,
					this.folderDisplayName,
					ex2
				});
				return ItemStartDateCalculator.startDateForCorruptItems;
			}
			catch (RecurrenceException ex3)
			{
				this.tracer.TraceError((long)this.GetHashCode(), "{0}: RecurrenceException thrown for Item '{1}' in folder '{2}' - Corrupt item. Exception: {3}", new object[]
				{
					TraceContext.Get(),
					id,
					this.folderDisplayName,
					ex3
				});
				return ItemStartDateCalculator.startDateForCorruptItems;
			}
			catch (ObjectNotFoundException ex4)
			{
				ex = ex4;
			}
			catch (VirusDetectedException ex5)
			{
				ex = ex5;
			}
			catch (VirusScanInProgressException ex6)
			{
				ex = ex6;
			}
			if (ex != null)
			{
				this.tracer.TraceDebug((long)this.GetHashCode(), "{0}: Exception: {1} thrown for calendar Item '{2}' in folder '{3}' - skipping item.", new object[]
				{
					TraceContext.Get(),
					ex,
					id,
					this.folderDisplayName
				});
				return DateTime.MinValue;
			}
			return DateTime.MinValue;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000F4F0 File Offset: 0x0000D6F0
		private DateTime GetStartDateOfTaskItem(VersionedId id, object[] itemProperties)
		{
			DateTime? taskStartDateFromView = this.GetTaskStartDateFromView(itemProperties);
			if (taskStartDateFromView != null)
			{
				return taskStartDateFromView.Value;
			}
			return this.OpenTaskItemToGetStartDate(id, itemProperties);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000F520 File Offset: 0x0000D720
		private DateTime? GetTaskStartDateFromView(object[] itemProperties)
		{
			if (this.folderType == DefaultFolderType.Tasks)
			{
				if (!(itemProperties[this.propertyIndexHolder.IsTaskRecurringIndex] is bool))
				{
					this.tracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: Task item in Folder: {1} is corrupt.", TraceContext.Get(), this.folderDisplayName);
					return new DateTime?(ItemStartDateCalculator.startDateForCorruptItems);
				}
				if (!(bool)itemProperties[this.propertyIndexHolder.IsTaskRecurringIndex])
				{
					return new DateTime?(this.GetStartDateFromCreationDate(itemProperties));
				}
			}
			return null;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000F5A4 File Offset: 0x0000D7A4
		private DateTime OpenTaskItemToGetStartDate(VersionedId id, object[] itemProperties)
		{
			Exception ex = null;
			try
			{
				this.tracer.TraceDebug<object, VersionedId, string>((long)this.GetHashCode(), "{0}: Going to open item {1} to calculate its start date in Folder: {2}.", TraceContext.Get(), id, this.folderDisplayName);
				using (Item item = Item.Bind(this.mailboxSession, id, ItemBindOption.LoadRequiredPropertiesOnly))
				{
					if (!(item is Task))
					{
						this.tracer.TraceDebug((long)this.GetHashCode(), "{0}: Item '{1}' in folder '{2}' is supposed to be Task item but is of type '{3}'. Corrupt item.", new object[]
						{
							TraceContext.Get(),
							id,
							this.folderDisplayName,
							item.GetType()
						});
						return ItemStartDateCalculator.startDateForCorruptItems;
					}
					Task task = (Task)item;
					if (task.Recurrence == null)
					{
						return this.GetStartDateFromCreationDate(itemProperties);
					}
					if (task.Recurrence.Range is NoEndRecurrenceRange)
					{
						return DateTime.MinValue;
					}
					if (task.Recurrence.Pattern is RegeneratingPattern)
					{
						this.tracer.TraceDebug<object, VersionedId, string>((long)this.GetHashCode(), "{0}: Task item '{1}' in folder '{2}' will be skipped because it is a Regenerating task.", TraceContext.Get(), id, this.folderDisplayName);
						return DateTime.MinValue;
					}
					if (task.Recurrence.Range is NumberedRecurrenceRange || task.Recurrence.Range is EndDateRecurrenceRange)
					{
						OccurrenceInfo lastOccurrence = task.Recurrence.GetLastOccurrence();
						return (DateTime)lastOccurrence.EndTime;
					}
				}
			}
			catch (CorruptDataException)
			{
				this.tracer.TraceError<object, VersionedId, string>((long)this.GetHashCode(), "{0}: CorruptDataException thrown for task Item '{1}' in folder '{2}' - Corrupt item.", TraceContext.Get(), id, this.folderDisplayName);
				return ItemStartDateCalculator.startDateForCorruptItems;
			}
			catch (RecurrenceException ex2)
			{
				this.tracer.TraceError((long)this.GetHashCode(), "{0}: RecurrenceException thrown for Item '{1}' in folder '{2}' - Corrupt item. Exception: {3}", new object[]
				{
					TraceContext.Get(),
					id,
					this.folderDisplayName,
					ex2
				});
				return ItemStartDateCalculator.startDateForCorruptItems;
			}
			catch (ObjectNotFoundException ex3)
			{
				ex = ex3;
			}
			catch (VirusDetectedException ex4)
			{
				ex = ex4;
			}
			catch (VirusScanInProgressException ex5)
			{
				ex = ex5;
			}
			if (ex != null)
			{
				this.tracer.TraceDebug((long)this.GetHashCode(), "{0}: Exception: {1} thrown for task Item '{2}' in folder '{3}' - skipping item.", new object[]
				{
					TraceContext.Get(),
					ex,
					id,
					this.folderDisplayName
				});
				return DateTime.MinValue;
			}
			return DateTime.MinValue;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000F884 File Offset: 0x0000DA84
		private DateTime GetStartDateFromCreationDate(object[] itemProperties)
		{
			DateTime result = DateTime.MinValue;
			if (ElcMailboxHelper.Exists(itemProperties[this.propertyIndexHolder.ReceivedTimeIndex]))
			{
				result = (DateTime)((ExDateTime)itemProperties[this.propertyIndexHolder.ReceivedTimeIndex]);
			}
			else if (ElcMailboxHelper.Exists(itemProperties[this.propertyIndexHolder.CreationTimeIndex]))
			{
				result = (DateTime)((ExDateTime)itemProperties[this.propertyIndexHolder.CreationTimeIndex]);
			}
			return result;
		}

		// Token: 0x04000231 RID: 561
		private static DateTime startDateForCorruptItems = DateTime.MinValue;

		// Token: 0x04000232 RID: 562
		private PropertyIndexHolder propertyIndexHolder;

		// Token: 0x04000233 RID: 563
		private string folderDisplayName;

		// Token: 0x04000234 RID: 564
		private DefaultFolderType folderType;

		// Token: 0x04000235 RID: 565
		private MailboxSession mailboxSession;

		// Token: 0x04000236 RID: 566
		private Trace tracer;
	}
}

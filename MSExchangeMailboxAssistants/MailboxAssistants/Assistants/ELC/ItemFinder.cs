using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x0200003A RID: 58
	internal class ItemFinder
	{
		// Token: 0x06000204 RID: 516 RVA: 0x0000D731 File Offset: 0x0000B931
		internal ItemFinder(MailboxSession mailboxSession, string folderDisplayName, DateTime utcNow)
		{
			this.mailboxSession = mailboxSession;
			this.folderDisplayName = folderDisplayName;
			this.utcNow = utcNow;
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000205 RID: 517 RVA: 0x0000D74E File Offset: 0x0000B94E
		// (set) Token: 0x06000206 RID: 518 RVA: 0x0000D756 File Offset: 0x0000B956
		internal bool NeedAutoCopyProps
		{
			get
			{
				return this.needAutoCopyProps;
			}
			set
			{
				this.needAutoCopyProps = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0000D75F File Offset: 0x0000B95F
		// (set) Token: 0x06000208 RID: 520 RVA: 0x0000D767 File Offset: 0x0000B967
		internal bool NeedExpiryTime
		{
			get
			{
				return this.needExpiryTime;
			}
			set
			{
				this.needExpiryTime = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0000D770 File Offset: 0x0000B970
		// (set) Token: 0x0600020A RID: 522 RVA: 0x0000D778 File Offset: 0x0000B978
		internal bool NeedMigrationExpiryTime
		{
			get
			{
				return this.needMigrationExpiryTime;
			}
			set
			{
				this.needMigrationExpiryTime = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000D781 File Offset: 0x0000B981
		// (set) Token: 0x0600020C RID: 524 RVA: 0x0000D789 File Offset: 0x0000B989
		internal bool NeedCalendarProps
		{
			get
			{
				return this.needCalendarProps;
			}
			set
			{
				this.needCalendarProps = value;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000D792 File Offset: 0x0000B992
		// (set) Token: 0x0600020E RID: 526 RVA: 0x0000D79A File Offset: 0x0000B99A
		internal bool NeedTaskProps
		{
			get
			{
				return this.needTaskProps;
			}
			set
			{
				this.needTaskProps = value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000D7A3 File Offset: 0x0000B9A3
		// (set) Token: 0x06000210 RID: 528 RVA: 0x0000D7AB File Offset: 0x0000B9AB
		internal bool NeedMoveDate
		{
			get
			{
				return this.needMoveDate;
			}
			set
			{
				this.needMoveDate = value;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000211 RID: 529 RVA: 0x0000D7B4 File Offset: 0x0000B9B4
		// (set) Token: 0x06000212 RID: 530 RVA: 0x0000D7BC File Offset: 0x0000B9BC
		internal bool NeedMessageSubject
		{
			get
			{
				return this.needMessageSubject;
			}
			set
			{
				this.needMessageSubject = value;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000D7C5 File Offset: 0x0000B9C5
		// (set) Token: 0x06000214 RID: 532 RVA: 0x0000D7CD File Offset: 0x0000B9CD
		internal bool NeedMessageInternetId
		{
			get
			{
				return this.needMessageInternetId;
			}
			set
			{
				this.needMessageInternetId = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000D7D6 File Offset: 0x0000B9D6
		// (set) Token: 0x06000216 RID: 534 RVA: 0x0000D7DE File Offset: 0x0000B9DE
		internal bool NeedMessageSender
		{
			get
			{
				return this.needMessageSender;
			}
			set
			{
				this.needMessageSender = value;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000217 RID: 535 RVA: 0x0000D7E7 File Offset: 0x0000B9E7
		// (set) Token: 0x06000218 RID: 536 RVA: 0x0000D7EF File Offset: 0x0000B9EF
		internal bool NeedRetentionTagProps
		{
			get
			{
				return this.needRetentionTagProps;
			}
			set
			{
				this.needRetentionTagProps = value;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000219 RID: 537 RVA: 0x0000D7F8 File Offset: 0x0000B9F8
		// (set) Token: 0x0600021A RID: 538 RVA: 0x0000D800 File Offset: 0x0000BA00
		internal int SmallestAgeLimit
		{
			get
			{
				return this.smallestAgeLimit;
			}
			set
			{
				this.smallestAgeLimit = value;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600021B RID: 539 RVA: 0x0000D809 File Offset: 0x0000BA09
		internal PropertyDefinition[] DataColumns
		{
			get
			{
				return this.dataColumns;
			}
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000D814 File Offset: 0x0000BA14
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = string.Concat(new object[]
				{
					"Item finder for mailbox ",
					this.mailboxSession.MailboxOwner,
					" for folder ",
					this.folderDisplayName
				});
			}
			return this.toString;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000D869 File Offset: 0x0000BA69
		internal void SetAuditLogFlags(bool setMessageSubject)
		{
			this.needMessageSender = true;
			this.needMessageInternetId = true;
			if (setMessageSubject)
			{
				this.needMessageSubject = true;
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000D884 File Offset: 0x0000BA84
		internal IEnumerator<List<object[]>> GetResultView(Folder folder)
		{
			List<PropertyDefinition> list = new List<PropertyDefinition>();
			list.Add(ItemSchema.Id);
			list.Add(StoreObjectSchema.ItemClass);
			list.Add(ItemSchema.ReceivedTime);
			list.Add(StoreObjectSchema.CreationTime);
			list.Add(ItemSchema.Size);
			list.Add(StoreObjectSchema.LastModifiedTime);
			if (this.needExpiryTime)
			{
				list.Add(MessageItemSchema.ExpiryTime);
			}
			if (this.needMigrationExpiryTime)
			{
				list.Add(ItemSchema.EHAMigrationExpiryDate);
			}
			if (this.needMoveDate)
			{
				list.Add(ItemSchema.ElcMoveDate);
			}
			if (this.needCalendarProps)
			{
				list.Add(CalendarItemInstanceSchema.EndTime);
				list.Add(CalendarItemBaseSchema.CalendarItemType);
			}
			if (this.needTaskProps)
			{
				list.Add(TaskSchema.IsTaskRecurring);
			}
			if (this.needMessageSubject)
			{
				list.Add(ItemSchema.Subject);
			}
			if (this.needMessageSender)
			{
				list.Add(MessageItemSchema.SenderEmailAddress);
				list.Add(ItemSchema.SentRepresentingEmailAddress);
			}
			if (this.needMessageInternetId)
			{
				list.Add(ItemSchema.InternetMessageId);
			}
			if (this.needAutoCopyProps)
			{
				list.Add(ItemSchema.ElcAutoCopyTag);
			}
			if (this.needRetentionTagProps)
			{
				list.Add(StoreObjectSchema.PolicyTag);
				list.Add(StoreObjectSchema.ExplicitPolicyTag);
				list.Add(StoreObjectSchema.RetentionPeriod);
				list.Add(ItemSchema.StartDateEtc);
				list.Add(ItemSchema.RetentionDate);
				list.Add(StoreObjectSchema.RetentionFlags);
				list.Add(StoreObjectSchema.ArchiveTag);
				list.Add(StoreObjectSchema.ExplicitArchiveTag);
				list.Add(StoreObjectSchema.ArchivePeriod);
				list.Add(ItemSchema.ArchiveDate);
				list.Add(StoreObjectSchema.ParentItemId);
			}
			this.dataColumns = list.ToArray();
			SortBy[] sortColumns;
			if (this.needAutoCopyProps)
			{
				sortColumns = ItemFinder.sortAutocopyTag;
			}
			else if (this.needRetentionTagProps)
			{
				sortColumns = null;
			}
			else
			{
				sortColumns = ItemFinder.sortReceivedTime;
			}
			IEnumerator<List<object[]>> result = null;
			Exception ex = null;
			try
			{
				QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, sortColumns, this.dataColumns);
				if (this.needAutoCopyProps || this.needRetentionTagProps)
				{
					queryResult.SeekToOffset(SeekReference.OriginBeginning, 0);
				}
				else
				{
					ComparisonFilter seekFilter = new ComparisonFilter(ComparisonOperator.LessThanOrEqual, ItemSchema.ReceivedTime, this.utcNow.AddDays((double)(-(double)this.smallestAgeLimit)));
					queryResult.SeekToCondition(SeekReference.OriginBeginning, seekFilter);
				}
				result = new ItemFinder.ItemEnumerator(queryResult, this);
			}
			catch (ObjectNotFoundException ex2)
			{
				ex = ex2;
			}
			catch (InvalidFolderLanguageIdException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				ItemFinder.Tracer.TraceDebug<ItemFinder, Exception>((long)this.GetHashCode(), "{0}: Failed to get items from the folder because the folder was not found or was inaccessible. Exception: '{1}'", this, ex);
			}
			return result;
		}

		// Token: 0x040001AA RID: 426
		private static readonly SortBy[] sortReceivedTime = new SortBy[]
		{
			new SortBy(ItemSchema.ReceivedTime, SortOrder.Descending)
		};

		// Token: 0x040001AB RID: 427
		private static readonly SortBy[] sortAutocopyTag = new SortBy[]
		{
			new SortBy(ItemSchema.ElcAutoCopyTag, SortOrder.Ascending)
		};

		// Token: 0x040001AC RID: 428
		private static readonly Trace Tracer = ExTraceGlobals.CommonEnforcerOperationsTracer;

		// Token: 0x040001AD RID: 429
		private bool needExpiryTime;

		// Token: 0x040001AE RID: 430
		private bool needMigrationExpiryTime;

		// Token: 0x040001AF RID: 431
		private bool needCalendarProps;

		// Token: 0x040001B0 RID: 432
		private bool needTaskProps;

		// Token: 0x040001B1 RID: 433
		private bool needMoveDate;

		// Token: 0x040001B2 RID: 434
		private bool needMessageSubject;

		// Token: 0x040001B3 RID: 435
		private bool needMessageInternetId;

		// Token: 0x040001B4 RID: 436
		private bool needMessageSender;

		// Token: 0x040001B5 RID: 437
		private bool needAutoCopyProps;

		// Token: 0x040001B6 RID: 438
		private bool needRetentionTagProps;

		// Token: 0x040001B7 RID: 439
		private int smallestAgeLimit;

		// Token: 0x040001B8 RID: 440
		private PropertyDefinition[] dataColumns;

		// Token: 0x040001B9 RID: 441
		private string toString;

		// Token: 0x040001BA RID: 442
		private MailboxSession mailboxSession;

		// Token: 0x040001BB RID: 443
		private string folderDisplayName;

		// Token: 0x040001BC RID: 444
		private DateTime utcNow;

		// Token: 0x0200003C RID: 60
		private class ItemEnumerator : QueryResultsEnumerator
		{
			// Token: 0x06000229 RID: 553 RVA: 0x0000DC60 File Offset: 0x0000BE60
			internal ItemEnumerator(QueryResult queryResult, ItemFinder itemFinder, int batchSize) : base(queryResult, batchSize)
			{
				this.itemFinder = itemFinder;
				this.autocopyIndex = Array.IndexOf<PropertyDefinition>(this.itemFinder.DataColumns, ItemSchema.ElcAutoCopyTag);
			}

			// Token: 0x0600022A RID: 554 RVA: 0x0000DC8C File Offset: 0x0000BE8C
			internal ItemEnumerator(QueryResult queryResult, ItemFinder itemFinder) : this(queryResult, itemFinder, 2000)
			{
			}

			// Token: 0x0600022B RID: 555 RVA: 0x0000DC9C File Offset: 0x0000BE9C
			protected override bool ProcessResults(object[][] partialResults)
			{
				if (this.itemFinder.NeedAutoCopyProps)
				{
					object[] array = partialResults[partialResults.GetUpperBound(0)];
					object obj = array[this.autocopyIndex];
					if (obj != null && !(obj is PropertyError))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x0600022C RID: 556 RVA: 0x0000DCD7 File Offset: 0x0000BED7
			protected override void HandleException(Exception exception)
			{
				ItemFinder.Tracer.TraceDebug<ItemFinder, Exception>((long)this.itemFinder.GetHashCode(), "{0}: Failed to get items from the folder because the folder was not found or was inaccessible. Exception: '{1}'", this.itemFinder, exception);
			}

			// Token: 0x040001C1 RID: 449
			private readonly ItemFinder itemFinder;

			// Token: 0x040001C2 RID: 450
			private readonly int autocopyIndex;
		}
	}
}

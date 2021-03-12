using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000846 RID: 2118
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ReminderMessage : MessageItem
	{
		// Token: 0x06004EFE RID: 20222 RVA: 0x0014B2C2 File Offset: 0x001494C2
		internal ReminderMessage(ICoreItem coreItem) : base(coreItem, false)
		{
		}

		// Token: 0x06004EFF RID: 20223 RVA: 0x0014B2CC File Offset: 0x001494CC
		public new static ReminderMessage Bind(StoreSession session, StoreId storeId)
		{
			return ReminderMessage.Bind(session, storeId, null);
		}

		// Token: 0x06004F00 RID: 20224 RVA: 0x0014B2D6 File Offset: 0x001494D6
		public new static ReminderMessage Bind(StoreSession session, StoreId storeId, ICollection<PropertyDefinition> propsToReturn)
		{
			return ItemBuilder.ItemBind<ReminderMessage>(session, storeId, ReminderMessageSchema.Instance, propsToReturn);
		}

		// Token: 0x06004F01 RID: 20225 RVA: 0x0014B2E8 File Offset: 0x001494E8
		public static ReminderMessage CreateReminderMessage(StoreSession session, StoreId destFolderId, string itemClass)
		{
			ReminderMessage reminderMessage = null;
			bool flag = false;
			ReminderMessage result;
			try
			{
				reminderMessage = ItemBuilder.CreateNewItem<ReminderMessage>(session, destFolderId, ItemCreateInfo.ReminderMessageInfo);
				reminderMessage[InternalSchema.ItemClass] = itemClass;
				flag = true;
				result = reminderMessage;
			}
			finally
			{
				if (!flag && reminderMessage != null)
				{
					reminderMessage.Dispose();
					reminderMessage = null;
				}
			}
			return result;
		}

		// Token: 0x17001659 RID: 5721
		// (get) Token: 0x06004F02 RID: 20226 RVA: 0x0014B338 File Offset: 0x00149538
		public override Schema Schema
		{
			get
			{
				this.CheckDisposed("Schema::get");
				return ReminderMessageSchema.Instance;
			}
		}

		// Token: 0x1700165A RID: 5722
		// (get) Token: 0x06004F03 RID: 20227 RVA: 0x0014B34A File Offset: 0x0014954A
		// (set) Token: 0x06004F04 RID: 20228 RVA: 0x0014B362 File Offset: 0x00149562
		public string ReminderText
		{
			get
			{
				this.CheckDisposed("ReminderText::get");
				return base.GetValueOrDefault<string>(ReminderMessageSchema.ReminderText);
			}
			set
			{
				this.CheckDisposed("ReminderText::set");
				this[ReminderMessageSchema.ReminderText] = value;
			}
		}

		// Token: 0x1700165B RID: 5723
		// (get) Token: 0x06004F05 RID: 20229 RVA: 0x0014B37B File Offset: 0x0014957B
		// (set) Token: 0x06004F06 RID: 20230 RVA: 0x0014B393 File Offset: 0x00149593
		public string Location
		{
			get
			{
				this.CheckDisposed("Location::get");
				return base.GetValueOrDefault<string>(ReminderMessageSchema.Location);
			}
			set
			{
				this.CheckDisposed("Location::set");
				this[ReminderMessageSchema.Location] = value;
			}
		}

		// Token: 0x1700165C RID: 5724
		// (get) Token: 0x06004F07 RID: 20231 RVA: 0x0014B3AC File Offset: 0x001495AC
		// (set) Token: 0x06004F08 RID: 20232 RVA: 0x0014B3C4 File Offset: 0x001495C4
		public ExDateTime ReminderStartTime
		{
			get
			{
				this.CheckDisposed("ReminderStartTime::get");
				return base.GetValueOrDefault<ExDateTime>(ReminderMessageSchema.ReminderStartTime);
			}
			set
			{
				this.CheckDisposed("ReminderStartTime::set");
				this[ReminderMessageSchema.ReminderStartTime] = value;
			}
		}

		// Token: 0x1700165D RID: 5725
		// (get) Token: 0x06004F09 RID: 20233 RVA: 0x0014B3E2 File Offset: 0x001495E2
		// (set) Token: 0x06004F0A RID: 20234 RVA: 0x0014B3FA File Offset: 0x001495FA
		public ExDateTime ReminderEndTime
		{
			get
			{
				this.CheckDisposed("ReminderEndTime::get");
				return base.GetValueOrDefault<ExDateTime>(ReminderMessageSchema.ReminderEndTime);
			}
			set
			{
				this.CheckDisposed("ReminderEndTime::set");
				this[ReminderMessageSchema.ReminderEndTime] = value;
			}
		}

		// Token: 0x1700165E RID: 5726
		// (get) Token: 0x06004F0B RID: 20235 RVA: 0x0014B418 File Offset: 0x00149618
		// (set) Token: 0x06004F0C RID: 20236 RVA: 0x0014B435 File Offset: 0x00149635
		public Guid ReminderId
		{
			get
			{
				this.CheckDisposed("ReminderId::get");
				return base.GetValueOrDefault<Guid>(ReminderMessageSchema.ReminderId, Guid.Empty);
			}
			set
			{
				this.CheckDisposed("ReminderId::set");
				this[ReminderMessageSchema.ReminderId] = value;
			}
		}

		// Token: 0x1700165F RID: 5727
		// (get) Token: 0x06004F0D RID: 20237 RVA: 0x0014B454 File Offset: 0x00149654
		// (set) Token: 0x06004F0E RID: 20238 RVA: 0x0014B488 File Offset: 0x00149688
		public GlobalObjectId ReminderItemGlobalObjectId
		{
			get
			{
				this.CheckDisposed("ReminderItemGlobalObjectId::get");
				byte[] valueOrDefault = base.GetValueOrDefault<byte[]>(ReminderMessageSchema.ReminderItemGlobalObjectId, null);
				return (valueOrDefault != null) ? new GlobalObjectId(valueOrDefault) : null;
			}
			set
			{
				this.CheckDisposed("ReminderItemGlobalObjectId::set");
				byte[] value2 = (value != null) ? value.Bytes : null;
				this[ReminderMessageSchema.ReminderItemGlobalObjectId] = value2;
			}
		}

		// Token: 0x17001660 RID: 5728
		// (get) Token: 0x06004F0F RID: 20239 RVA: 0x0014B4BC File Offset: 0x001496BC
		// (set) Token: 0x06004F10 RID: 20240 RVA: 0x0014B4F0 File Offset: 0x001496F0
		public GlobalObjectId ReminderOccurrenceGlobalObjectId
		{
			get
			{
				this.CheckDisposed("ReminderOccurrenceGlobalObjectId::get");
				byte[] valueOrDefault = base.GetValueOrDefault<byte[]>(ReminderMessageSchema.ReminderOccurrenceGlobalObjectId, null);
				return (valueOrDefault != null) ? new GlobalObjectId(valueOrDefault) : null;
			}
			set
			{
				this.CheckDisposed("ReminderOccurrenceGlobalObjectId::set");
				byte[] value2 = (value != null) ? value.Bytes : null;
				this[ReminderMessageSchema.ReminderOccurrenceGlobalObjectId] = value2;
			}
		}

		// Token: 0x06004F11 RID: 20241 RVA: 0x0014B524 File Offset: 0x00149724
		public CalendarItemBase GetCachedCorrelatedOccurrence()
		{
			if (!(base.Session is MailboxSession))
			{
				return null;
			}
			if (this.cachedCorrelatedOccurrence != null)
			{
				return this.cachedCorrelatedOccurrence;
			}
			GlobalObjectId globalObjectId = this.ReminderOccurrenceGlobalObjectId;
			if (globalObjectId == null)
			{
				globalObjectId = this.ReminderItemGlobalObjectId;
			}
			this.cachedCorrelatedOccurrence = this.GetCorrelatedItem(globalObjectId);
			return this.cachedCorrelatedOccurrence;
		}

		// Token: 0x06004F12 RID: 20242 RVA: 0x0014B573 File Offset: 0x00149773
		public CalendarItemBase GetCachedCorrelatedItem()
		{
			if (!(base.Session is MailboxSession))
			{
				return null;
			}
			if (this.cachedCorrelatedItem != null)
			{
				return this.cachedCorrelatedItem;
			}
			this.cachedCorrelatedItem = this.GetCorrelatedItem(this.ReminderItemGlobalObjectId);
			return this.cachedCorrelatedItem;
		}

		// Token: 0x06004F13 RID: 20243 RVA: 0x0014B5AC File Offset: 0x001497AC
		public CalendarItemBase GetCorrelatedItem(GlobalObjectId globalObjectId)
		{
			MailboxSession mailboxSession = (MailboxSession)base.Session;
			StoreObjectId defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.Calendar);
			if (defaultFolderId == null)
			{
				ExTraceGlobals.MeetingMessageTracer.Information((long)this.GetHashCode(), "ReminderMessage::GetCorrelatedItem. Calendar folder is not found. We treat it as correlated calendar item has been deleted.");
				return null;
			}
			CalendarItemBase calendarItemBase = null;
			bool flag = false;
			try
			{
				using (CalendarFolder calendarFolder = CalendarFolder.Bind(mailboxSession, defaultFolderId))
				{
					int i = 0;
					while (i < 2)
					{
						ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId, int>((long)this.GetHashCode(), "Storage.MeetingMessage.GetCorrelatedItemInternal: GOID={0}. Retrying fetch count={1}", globalObjectId, i);
						this.correlatedItemId = calendarFolder.GetCalendarItemId(globalObjectId.Bytes);
						if (this.correlatedItemId != null)
						{
							try
							{
								calendarItemBase = CalendarItemBase.Bind(mailboxSession, this.correlatedItemId.ObjectId);
							}
							catch (OccurrenceNotFoundException arg)
							{
								ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId, OccurrenceNotFoundException>((long)this.GetHashCode(), "Storage.MeetingMessage.GetCorrelatedItemInternal: GOID={0}. Occurrence not found. Exception: {1}", globalObjectId, arg);
								this.correlatedItemId = null;
								break;
							}
							catch (ObjectNotFoundException arg2)
							{
								ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId, ObjectNotFoundException>((long)this.GetHashCode(), "Storage.MeetingMessage.GetCorrelatedItemInternal: GOID={0}. Calendar item id was found but calendar item got deleted. Exception: {1}", globalObjectId, arg2);
								this.correlatedItemId = null;
								goto IL_106;
							}
							catch (WrongObjectTypeException innerException)
							{
								throw new CorruptDataException(ServerStrings.ExNonCalendarItemReturned("Unknown"), innerException);
							}
							catch (ArgumentException innerException2)
							{
								throw new CorruptDataException(ServerStrings.ExNonCalendarItemReturned("Unknown"), innerException2);
							}
							goto IL_FF;
							IL_106:
							i++;
							continue;
							IL_FF:
							flag = true;
							return calendarItemBase;
						}
						break;
					}
				}
			}
			finally
			{
				if (!flag)
				{
					Util.DisposeIfPresent(calendarItemBase);
				}
			}
			return null;
		}

		// Token: 0x06004F14 RID: 20244 RVA: 0x0014B738 File Offset: 0x00149938
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.cachedCorrelatedItem != null)
			{
				this.cachedCorrelatedItem.Dispose();
				this.cachedCorrelatedItem = null;
			}
			if (disposing && this.cachedCorrelatedOccurrence != null)
			{
				this.cachedCorrelatedOccurrence.Dispose();
				this.cachedCorrelatedOccurrence = null;
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x04002AF3 RID: 10995
		private VersionedId correlatedItemId;

		// Token: 0x04002AF4 RID: 10996
		private CalendarItemBase cachedCorrelatedItem;

		// Token: 0x04002AF5 RID: 10997
		private CalendarItemBase cachedCorrelatedOccurrence;
	}
}

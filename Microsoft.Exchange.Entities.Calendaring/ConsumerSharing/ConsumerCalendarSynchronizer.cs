using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common;

namespace Microsoft.Exchange.Entities.Calendaring.ConsumerSharing
{
	// Token: 0x02000018 RID: 24
	internal class ConsumerCalendarSynchronizer
	{
		// Token: 0x06000085 RID: 133 RVA: 0x00002FC6 File Offset: 0x000011C6
		public ConsumerCalendarSynchronizer(IMailboxSession mailboxSession, IXSOFactory xsoFactory, ITracer tracer)
		{
			this.MailboxSession = mailboxSession;
			this.XsoFactory = xsoFactory;
			this.Tracer = tracer;
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000086 RID: 134 RVA: 0x00002FE4 File Offset: 0x000011E4
		// (remove) Token: 0x06000087 RID: 135 RVA: 0x0000301C File Offset: 0x0000121C
		public event EventHandler<string> LogError;

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003051 File Offset: 0x00001251
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00003059 File Offset: 0x00001259
		public IMailboxSession MailboxSession { get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00003062 File Offset: 0x00001262
		// (set) Token: 0x0600008B RID: 139 RVA: 0x0000306A File Offset: 0x0000126A
		public IXSOFactory XsoFactory { get; private set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00003073 File Offset: 0x00001273
		// (set) Token: 0x0600008D RID: 141 RVA: 0x0000307B File Offset: 0x0000127B
		public ITracer Tracer { get; private set; }

		// Token: 0x0600008E RID: 142 RVA: 0x00003084 File Offset: 0x00001284
		public SyncResult Synchronize(StoreObjectId folderId, Deadline deadline)
		{
			this.Tracer.TraceDebug<object, StoreObjectId, string>((long)this.GetHashCode(), "{0}: ConsumerCalendarSharingEngine.Synchronize will try to sync folder {1} for mailbox {2}.", TraceContext.Get(), folderId, this.MailboxSession.DisplayAddress);
			ConsumerCalendarSubscription consumerCalendarSubscription = this.TryGetSubscription(folderId);
			if (consumerCalendarSubscription == null)
			{
				return SyncResult.SubscriptionLost;
			}
			using (ICalendarItem calendarItem = this.XsoFactory.CreateCalendarItem(this.MailboxSession, folderId))
			{
				calendarItem.StartTime = ExDateTime.Now;
				calendarItem.EndTime = calendarItem.StartTime.AddSeconds(1.0);
				calendarItem.Subject = string.Format("Consumer Calendar Sync: OwnerId={0}, CalendarGuid={1}", consumerCalendarSubscription.ConsumerCalendarOwnerId, consumerCalendarSubscription.ConsumerCalendarGuid);
				calendarItem.Save(SaveMode.NoConflictResolutionForceSave);
			}
			return SyncResult.Completed;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000314C File Offset: 0x0000134C
		public virtual ConsumerCalendarSubscription TryGetSubscription(StoreObjectId folderId)
		{
			ConsumerCalendarSubscription result;
			using (ICalendarFolder calendarFolder = this.BindToConsumerCalendarFolder(folderId))
			{
				long consumerCalendarOwnerId = calendarFolder.ConsumerCalendarOwnerId;
				if (consumerCalendarOwnerId == 0L)
				{
					this.OnLogError("ConsumerCalendarOwnerId is set to zero.");
					result = null;
				}
				else
				{
					Guid consumerCalendarGuid = calendarFolder.ConsumerCalendarGuid;
					if (consumerCalendarGuid == Guid.Empty)
					{
						this.OnLogError("ConsumerCalendarGuid is empty.");
						result = null;
					}
					else
					{
						result = new ConsumerCalendarSubscription(consumerCalendarOwnerId, consumerCalendarGuid);
					}
				}
			}
			return result;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000031C4 File Offset: 0x000013C4
		protected ICalendarFolder BindToConsumerCalendarFolder(StoreObjectId folderId)
		{
			ICalendarFolder calendarFolder = this.XsoFactory.BindToCalendarFolder(this.MailboxSession, folderId);
			calendarFolder.Load(CalendarFolderSchema.ConsumerCalendarProperties);
			return calendarFolder;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000031F0 File Offset: 0x000013F0
		protected void OnLogError(string error)
		{
			EventHandler<string> logError = this.LogError;
			if (logError != null)
			{
				logError(this, error);
			}
		}
	}
}

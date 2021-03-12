using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200070A RID: 1802
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class LastEventAdvisor : AdvisorBase
	{
		// Token: 0x06004753 RID: 18259 RVA: 0x0012F868 File Offset: 0x0012DA68
		private LastEventAdvisor(Guid mailboxGuid, bool isPublicFolderDatabase, EventCondition condition, EventWatermark watermark) : base(mailboxGuid, isPublicFolderDatabase, condition, watermark)
		{
		}

		// Token: 0x06004754 RID: 18260 RVA: 0x0012F875 File Offset: 0x0012DA75
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<LastEventAdvisor>(this);
		}

		// Token: 0x06004755 RID: 18261 RVA: 0x0012F8AC File Offset: 0x0012DAAC
		public static LastEventAdvisor Create(StoreSession session, EventCondition condition)
		{
			return EventSink.InternalCreateEventSink<LastEventAdvisor>(session, null, () => new LastEventAdvisor(session.MailboxGuid, session is PublicFolderSession, condition, null));
		}

		// Token: 0x06004756 RID: 18262 RVA: 0x0012F91C File Offset: 0x0012DB1C
		public static LastEventAdvisor Create(StoreSession session, EventCondition condition, EventWatermark watermark)
		{
			if (watermark == null)
			{
				throw new ArgumentNullException("watermark");
			}
			return EventSink.InternalCreateEventSink<LastEventAdvisor>(session, watermark, () => new LastEventAdvisor(session.MailboxGuid, session is PublicFolderSession, condition, watermark));
		}

		// Token: 0x06004757 RID: 18263 RVA: 0x0012F974 File Offset: 0x0012DB74
		public Event GetLastEvent()
		{
			this.CheckDisposed(null);
			base.CheckException();
			Event result;
			lock (base.ThisLock)
			{
				Event @event = this.lastEvent;
				this.lastEvent = null;
				result = @event;
			}
			return result;
		}

		// Token: 0x06004758 RID: 18264 RVA: 0x0012F9CC File Offset: 0x0012DBCC
		protected override void InternalRecoveryConsumeRelevantEvent(MapiEvent mapiEvent)
		{
			this.lastEvent = new Event(base.MdbGuid, mapiEvent);
		}

		// Token: 0x06004759 RID: 18265 RVA: 0x0012F9E0 File Offset: 0x0012DBE0
		protected override bool TryGetCurrentWatermark(bool isRecoveryWatermark, out EventWatermark watermark)
		{
			if (this.lastEvent != null)
			{
				watermark = new EventWatermark(base.MdbGuid, this.lastRelevantEventWatermark, false);
				return true;
			}
			watermark = null;
			return false;
		}

		// Token: 0x0600475A RID: 18266 RVA: 0x0012FA04 File Offset: 0x0012DC04
		protected override void AdvisorInternalConsumeRelevantEvent(MapiEvent mapiEvent)
		{
			this.lastEvent = new Event(base.MdbGuid, mapiEvent);
			this.lastRelevantEventWatermark = mapiEvent.Watermark.EventCounter;
		}

		// Token: 0x0600475B RID: 18267 RVA: 0x0012FA29 File Offset: 0x0012DC29
		protected override bool ShouldIgnoreRecoveryEventsAfterConsume()
		{
			return true;
		}

		// Token: 0x04002703 RID: 9987
		private Event lastEvent;

		// Token: 0x04002704 RID: 9988
		private long lastRelevantEventWatermark;
	}
}

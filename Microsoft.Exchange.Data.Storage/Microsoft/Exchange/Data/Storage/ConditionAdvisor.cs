using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020006FB RID: 1787
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ConditionAdvisor : AdvisorBase
	{
		// Token: 0x060046D6 RID: 18134 RVA: 0x0012D7BA File Offset: 0x0012B9BA
		private ConditionAdvisor(Guid mailboxGuid, bool isPublicFolderDatabase, EventCondition condition, EventWatermark watermark) : base(mailboxGuid, isPublicFolderDatabase, condition, watermark)
		{
		}

		// Token: 0x060046D7 RID: 18135 RVA: 0x0012D7C7 File Offset: 0x0012B9C7
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ConditionAdvisor>(this);
		}

		// Token: 0x060046D8 RID: 18136 RVA: 0x0012D800 File Offset: 0x0012BA00
		public static ConditionAdvisor Create(StoreSession session, EventCondition condition)
		{
			return EventSink.InternalCreateEventSink<ConditionAdvisor>(session, null, () => new ConditionAdvisor(session.MailboxGuid, session is PublicFolderSession, condition, null));
		}

		// Token: 0x060046D9 RID: 18137 RVA: 0x0012D870 File Offset: 0x0012BA70
		public static ConditionAdvisor Create(StoreSession session, EventCondition condition, EventWatermark watermark)
		{
			if (watermark == null)
			{
				throw new ArgumentNullException("watermark");
			}
			return EventSink.InternalCreateEventSink<ConditionAdvisor>(session, watermark, () => new ConditionAdvisor(session.MailboxGuid, session is PublicFolderSession, condition, watermark));
		}

		// Token: 0x17001498 RID: 5272
		// (get) Token: 0x060046DA RID: 18138 RVA: 0x0012D8C8 File Offset: 0x0012BAC8
		public bool IsConditionTrue
		{
			get
			{
				this.CheckDisposed(null);
				base.CheckException();
				return this.isConditionTrue;
			}
		}

		// Token: 0x060046DB RID: 18139 RVA: 0x0012D8E0 File Offset: 0x0012BAE0
		public void ResetCondition()
		{
			this.CheckDisposed(null);
			base.CheckException();
			lock (base.ThisLock)
			{
				this.isConditionTrue = false;
				base.IgnoreRecoveryEvents();
			}
		}

		// Token: 0x060046DC RID: 18140 RVA: 0x0012D934 File Offset: 0x0012BB34
		protected override void InternalRecoveryConsumeRelevantEvent(MapiEvent mapiEvent)
		{
			this.isConditionTrue = true;
		}

		// Token: 0x060046DD RID: 18141 RVA: 0x0012D93D File Offset: 0x0012BB3D
		protected override bool TryGetCurrentWatermark(bool isRecoveryWatermark, out EventWatermark watermark)
		{
			if (this.isConditionTrue)
			{
				watermark = new EventWatermark(base.MdbGuid, this.lastRelevantEventWatermark, false);
				return true;
			}
			watermark = null;
			return false;
		}

		// Token: 0x060046DE RID: 18142 RVA: 0x0012D961 File Offset: 0x0012BB61
		protected override void AdvisorInternalConsumeRelevantEvent(MapiEvent mapiEvent)
		{
			this.isConditionTrue = true;
			this.lastRelevantEventWatermark = mapiEvent.Watermark.EventCounter;
		}

		// Token: 0x060046DF RID: 18143 RVA: 0x0012D97B File Offset: 0x0012BB7B
		protected override bool ShouldIgnoreRecoveryEventsAfterConsume()
		{
			return true;
		}

		// Token: 0x040026B4 RID: 9908
		private bool isConditionTrue;

		// Token: 0x040026B5 RID: 9909
		private long lastRelevantEventWatermark;
	}
}

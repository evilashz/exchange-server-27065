using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020006F9 RID: 1785
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class AdvisorBase : EventSink, IRecoveryEventSink
	{
		// Token: 0x060046C1 RID: 18113 RVA: 0x0012D495 File Offset: 0x0012B695
		protected AdvisorBase(Guid mailboxGuid, bool isPublicFolderDatabase, EventCondition condition, EventWatermark watermark) : base(mailboxGuid, isPublicFolderDatabase, condition)
		{
			if (watermark != null)
			{
				this.firstMissedEventWatermark = watermark;
				this.needsRecovery = true;
				this.useRecoveryValues = true;
			}
		}

		// Token: 0x060046C2 RID: 18114 RVA: 0x0012D4C8 File Offset: 0x0012B6C8
		protected sealed override void InternalConsume(MapiEvent mapiEvent)
		{
			bool flag = false;
			lock (this.thisLock)
			{
				this.AdvisorInternalConsumeRelevantEvent(mapiEvent);
				this.lastKnownWatermark = mapiEvent.Watermark.EventCounter;
				if (this.needsRecovery)
				{
					this.lastMissedEventWatermark = mapiEvent.Watermark.EventCounter;
					this.needsRecovery = false;
					flag = true;
				}
				if (this.ShouldIgnoreRecoveryEventsAfterConsume())
				{
					this.IgnoreRecoveryEvents();
				}
			}
			if (flag)
			{
				base.RequestRecovery();
			}
		}

		// Token: 0x060046C3 RID: 18115 RVA: 0x0012D558 File Offset: 0x0012B758
		internal sealed override IRecoveryEventSink StartRecovery()
		{
			this.CheckDisposed(null);
			this.InternalStartRecovery();
			return this;
		}

		// Token: 0x060046C4 RID: 18116 RVA: 0x0012D568 File Offset: 0x0012B768
		internal sealed override void SetLastKnownWatermark(long mapiWatermark, bool mayInitiateRecovery)
		{
			bool flag = false;
			lock (this.thisLock)
			{
				this.lastKnownWatermark = mapiWatermark;
				if (this.needsRecovery && mayInitiateRecovery)
				{
					this.lastMissedEventWatermark = mapiWatermark;
					this.needsRecovery = false;
					flag = true;
				}
			}
			if (flag)
			{
				base.RequestRecovery();
			}
		}

		// Token: 0x060046C5 RID: 18117 RVA: 0x0012D5D0 File Offset: 0x0012B7D0
		internal sealed override void SetFirstEventToConsumeOnSink(long firstEventToConsumeWatermark)
		{
			lock (this.thisLock)
			{
				base.FirstEventToConsumeWatermark = firstEventToConsumeWatermark;
			}
		}

		// Token: 0x060046C6 RID: 18118 RVA: 0x0012D614 File Offset: 0x0012B814
		internal sealed override EventWatermark GetCurrentEventWatermark()
		{
			EventWatermark result;
			lock (this.thisLock)
			{
				EventWatermark firstMissedEventWatermark;
				if (this.useRecoveryValues)
				{
					if (!this.TryGetCurrentWatermark(true, out firstMissedEventWatermark))
					{
						firstMissedEventWatermark = this.firstMissedEventWatermark;
					}
				}
				else if (!this.TryGetCurrentWatermark(false, out firstMissedEventWatermark))
				{
					if (base.FirstEventToConsumeWatermark > this.lastKnownWatermark)
					{
						return new EventWatermark(base.MdbGuid, base.FirstEventToConsumeWatermark, false);
					}
					return new EventWatermark(base.MdbGuid, this.lastKnownWatermark, true);
				}
				result = firstMissedEventWatermark;
			}
			return result;
		}

		// Token: 0x060046C7 RID: 18119 RVA: 0x0012D6B0 File Offset: 0x0012B8B0
		bool IRecoveryEventSink.RecoveryConsume(MapiEvent mapiEvent)
		{
			this.CheckDisposed(null);
			base.CheckForFinalEvents(mapiEvent);
			bool flag = base.IsEventRelevant(mapiEvent);
			lock (this.thisLock)
			{
				if (flag && !this.ignoreRecoveryEvents)
				{
					this.firstMissedEventWatermark = new EventWatermark(base.MdbGuid, mapiEvent.Watermark.EventCounter, true);
					this.InternalRecoveryConsumeRelevantEvent(mapiEvent);
				}
			}
			return true;
		}

		// Token: 0x060046C8 RID: 18120 RVA: 0x0012D730 File Offset: 0x0012B930
		void IRecoveryEventSink.EndRecovery()
		{
			lock (this.thisLock)
			{
				this.InternalEndRecovery();
				this.useRecoveryValues = false;
			}
		}

		// Token: 0x17001494 RID: 5268
		// (get) Token: 0x060046C9 RID: 18121 RVA: 0x0012D778 File Offset: 0x0012B978
		EventWatermark IRecoveryEventSink.FirstMissedEventWatermark
		{
			get
			{
				this.CheckDisposed(null);
				return this.firstMissedEventWatermark;
			}
		}

		// Token: 0x17001495 RID: 5269
		// (get) Token: 0x060046CA RID: 18122 RVA: 0x0012D787 File Offset: 0x0012B987
		long IRecoveryEventSink.LastMissedEventWatermark
		{
			get
			{
				this.CheckDisposed(null);
				return this.lastMissedEventWatermark;
			}
		}

		// Token: 0x060046CB RID: 18123 RVA: 0x0012D796 File Offset: 0x0012B996
		protected virtual void InternalStartRecovery()
		{
		}

		// Token: 0x060046CC RID: 18124 RVA: 0x0012D798 File Offset: 0x0012B998
		protected virtual void InternalEndRecovery()
		{
		}

		// Token: 0x060046CD RID: 18125
		protected abstract void InternalRecoveryConsumeRelevantEvent(MapiEvent mapiEvent);

		// Token: 0x060046CE RID: 18126
		protected abstract bool TryGetCurrentWatermark(bool isRecoveryWatermark, out EventWatermark watermark);

		// Token: 0x060046CF RID: 18127
		protected abstract void AdvisorInternalConsumeRelevantEvent(MapiEvent mapiEvent);

		// Token: 0x060046D0 RID: 18128
		protected abstract bool ShouldIgnoreRecoveryEventsAfterConsume();

		// Token: 0x060046D1 RID: 18129 RVA: 0x0012D79A File Offset: 0x0012B99A
		protected void IgnoreRecoveryEvents()
		{
			this.ignoreRecoveryEvents = true;
			this.useRecoveryValues = false;
		}

		// Token: 0x17001496 RID: 5270
		// (get) Token: 0x060046D2 RID: 18130 RVA: 0x0012D7AA File Offset: 0x0012B9AA
		protected bool UseRecoveryValues
		{
			get
			{
				return this.useRecoveryValues;
			}
		}

		// Token: 0x17001497 RID: 5271
		// (get) Token: 0x060046D3 RID: 18131 RVA: 0x0012D7B2 File Offset: 0x0012B9B2
		protected object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x040026AE RID: 9902
		private readonly object thisLock = new object();

		// Token: 0x040026AF RID: 9903
		private bool needsRecovery;

		// Token: 0x040026B0 RID: 9904
		private bool useRecoveryValues;

		// Token: 0x040026B1 RID: 9905
		private bool ignoreRecoveryEvents;

		// Token: 0x040026B2 RID: 9906
		private long lastMissedEventWatermark;

		// Token: 0x040026B3 RID: 9907
		private long lastKnownWatermark;
	}
}

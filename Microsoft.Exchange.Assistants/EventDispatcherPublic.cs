using System;
using Microsoft.Exchange.Diagnostics.Components.Assistants;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x0200003F RID: 63
	internal class EventDispatcherPublic : EventDispatcher
	{
		// Token: 0x0600027B RID: 635 RVA: 0x0000E446 File Offset: 0x0000C646
		public EventDispatcherPublic(AssistantCollectionEntry assistant, EventControllerPublic controller, ThrottleGovernor governor) : base(assistant, new MailboxGovernor(controller.Governor, new Throttle("EventDispatcherPublic", controller.Throttle.OpenThrottleValue, controller.Throttle)), controller)
		{
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000E476 File Offset: 0x0000C676
		public override string MailboxDisplayName
		{
			get
			{
				return "<public folder database>";
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000E47D File Offset: 0x0000C67D
		public override string ToString()
		{
			return "Dispatcher for database " + base.Controller.DatabaseInfo.DisplayName;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000E499 File Offset: 0x0000C699
		public void ProcessPolledEvent(MapiEvent mapiEvent)
		{
			ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPublic, long>((long)this.GetHashCode(), "{0}: ProcessPolledEvent {1}", this, mapiEvent.EventCounter);
			base.EnqueueIfInteresting(new EmergencyKit(mapiEvent));
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000E4C5 File Offset: 0x0000C6C5
		public long GetWatermark(long highestEventPolled)
		{
			return Math.Min(highestEventPolled, this.FindLowestQueuedEventCounter() - 1L);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000E4FC File Offset: 0x0000C6FC
		protected override AIException DangerousProcessItem(EmergencyKit kit, InterestingEvent interestingEvent)
		{
			AIException result = null;
			try
			{
				base.CatchMeIfYouCan(delegate
				{
					this.HandleEvent(kit, interestingEvent, null, null);
				}, (base.Assistant != null) ? base.Assistant.Name : "<null>");
			}
			catch (AIException ex)
			{
				ExTraceGlobals.EventDispatcherTracer.TraceError<EventDispatcherPublic, AIException>((long)this.GetHashCode(), "{0}: Exception from HandleEvent: {1}", this, ex);
				result = ex;
			}
			return result;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000E58C File Offset: 0x0000C78C
		private long FindLowestQueuedEventCounter()
		{
			long num = long.MaxValue;
			lock (base.Locker)
			{
				foreach (InterestingEvent interestingEvent in base.ActiveQueue)
				{
					num = Math.Min(num, interestingEvent.MapiEvent.EventCounter);
				}
				foreach (InterestingEvent interestingEvent2 in base.PendingQueue)
				{
					num = Math.Min(num, interestingEvent2.MapiEvent.EventCounter);
				}
			}
			ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPublic, long>((long)this.GetHashCode(), "{0}: LowestQueuedEvent is {1}", this, num);
			return num;
		}
	}
}

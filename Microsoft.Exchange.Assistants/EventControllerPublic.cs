using System;
using Microsoft.Exchange.Diagnostics.Components.Assistants;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x0200003B RID: 59
	internal sealed class EventControllerPublic : EventController
	{
		// Token: 0x0600021E RID: 542 RVA: 0x0000C084 File Offset: 0x0000A284
		public EventControllerPublic(DatabaseInfo databaseInfo, EventBasedAssistantCollection assistants, PoisonEventControl poisonControl, PerformanceCountersPerDatabaseInstance databaseCounters, ThrottleGovernor serverGovernor) : base(databaseInfo, assistants, poisonControl, databaseCounters, serverGovernor, (MapiEventTypeFlags)0)
		{
			this.dispatcher = new EventDispatcherPublic(assistants.GetAssistantForPublicFolder(), this, base.Governor);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000C0AC File Offset: 0x0000A2AC
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.dispatcher.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000C0C3 File Offset: 0x0000A2C3
		protected override void WaitUntilStoppedInternal()
		{
			this.dispatcher.WaitForShutdown();
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000C0D0 File Offset: 0x0000A2D0
		protected override void ProcessPolledEvent(MapiEvent mapiEvent)
		{
			this.dispatcher.ProcessPolledEvent(mapiEvent);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000C0E0 File Offset: 0x0000A2E0
		protected override void UpdateWatermarksForAssistant(Guid assistantId)
		{
			long watermark = this.dispatcher.GetWatermark(base.HighestEventPolled);
			if (watermark != base.DatabaseBookmark[assistantId])
			{
				ExTraceGlobals.EventControllerTracer.TraceDebug<EventControllerPublic, long>((long)this.GetHashCode(), "{0}: Saving database watermark at {1}", this, watermark);
				base.EventAccess.SaveWatermarks(assistantId, new Watermark[]
				{
					Watermark.GetDatabaseWatermark(watermark)
				});
				base.DatabaseBookmark[assistantId] = watermark;
			}
		}

		// Token: 0x04000199 RID: 409
		private EventDispatcherPublic dispatcher;
	}
}

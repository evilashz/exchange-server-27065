using System;
using System.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000035 RID: 53
	internal sealed class InterestingEvent
	{
		// Token: 0x060001BE RID: 446 RVA: 0x000098C8 File Offset: 0x00007AC8
		public static InterestingEvent Create(MapiEvent mapiEvent)
		{
			return new InterestingEvent(mapiEvent)
			{
				WasProcessed = true
			};
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000098E4 File Offset: 0x00007AE4
		public static InterestingEvent CreateUnprocessed(MapiEvent mapiEvent)
		{
			return new InterestingEvent(mapiEvent);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x000098EC File Offset: 0x00007AEC
		private InterestingEvent(MapiEvent mapiEvent)
		{
			this.mapiEvent = mapiEvent;
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00009906 File Offset: 0x00007B06
		public MapiEvent MapiEvent
		{
			get
			{
				return this.mapiEvent;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x0000990E File Offset: 0x00007B0E
		public Stopwatch EnqueuedStopwatch
		{
			get
			{
				return this.enqueuedStopwatch;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00009916 File Offset: 0x00007B16
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x0000991E File Offset: 0x00007B1E
		public bool WasProcessed { get; private set; }

		// Token: 0x04000169 RID: 361
		private MapiEvent mapiEvent;

		// Token: 0x0400016A RID: 362
		private Stopwatch enqueuedStopwatch = Stopwatch.StartNew();
	}
}

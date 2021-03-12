using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Worker.Health;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x0200020C RID: 524
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class RoundtripCompleteEventArgs : EventArgs
	{
		// Token: 0x060011C1 RID: 4545 RVA: 0x00039FCB File Offset: 0x000381CB
		public RoundtripCompleteEventArgs(TimeSpan roundtripTime, bool roundtripSuccessful)
		{
			this.roundtripTime = roundtripTime;
			this.roundtripSuccessful = roundtripSuccessful;
			this.throttlingInfo = null;
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x00039FE8 File Offset: 0x000381E8
		public RoundtripCompleteEventArgs(TimeSpan roundtripTime, ThrottlingInfo throttlingInfo, bool roundtripSuccessful) : this(roundtripTime, roundtripSuccessful)
		{
			this.throttlingInfo = throttlingInfo;
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x060011C3 RID: 4547 RVA: 0x00039FF9 File Offset: 0x000381F9
		public TimeSpan RoundtripTime
		{
			get
			{
				return this.roundtripTime;
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x060011C4 RID: 4548 RVA: 0x0003A001 File Offset: 0x00038201
		internal bool RoundtripSuccessful
		{
			get
			{
				return this.roundtripSuccessful;
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x060011C5 RID: 4549 RVA: 0x0003A009 File Offset: 0x00038209
		internal ThrottlingInfo ThrottlingInfo
		{
			get
			{
				return this.throttlingInfo;
			}
		}

		// Token: 0x040009A9 RID: 2473
		private TimeSpan roundtripTime;

		// Token: 0x040009AA RID: 2474
		private bool roundtripSuccessful;

		// Token: 0x040009AB RID: 2475
		private ThrottlingInfo throttlingInfo;
	}
}

using System;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x0200008D RID: 141
	internal struct TransitionLogEntry
	{
		// Token: 0x060003C3 RID: 963 RVA: 0x0000C511 File Offset: 0x0000A711
		internal TransitionLogEntry(long sequence, uint currentState, uint signal)
		{
			this.sequence = sequence;
			this.currentState = currentState;
			this.signal = signal;
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x0000C528 File Offset: 0x0000A728
		internal long Sequence
		{
			get
			{
				return this.sequence;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x0000C530 File Offset: 0x0000A730
		internal uint CurrentState
		{
			get
			{
				return this.currentState;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x0000C538 File Offset: 0x0000A738
		internal uint Signal
		{
			get
			{
				return this.signal;
			}
		}

		// Token: 0x0400019F RID: 415
		private readonly long sequence;

		// Token: 0x040001A0 RID: 416
		private readonly uint currentState;

		// Token: 0x040001A1 RID: 417
		private readonly uint signal;
	}
}

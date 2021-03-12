using System;

namespace Microsoft.Exchange.TextMatching
{
	// Token: 0x02000047 RID: 71
	internal abstract class StateNode
	{
		// Token: 0x060001EF RID: 495 RVA: 0x00008E91 File Offset: 0x00007091
		public StateNode(int state, bool finalState)
		{
			this.state = state;
			this.finalState = finalState;
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00008EAE File Offset: 0x000070AE
		public int State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00008EB6 File Offset: 0x000070B6
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x00008EBE File Offset: 0x000070BE
		public bool IsFinal
		{
			get
			{
				return this.finalState;
			}
			set
			{
				this.finalState = value;
			}
		}

		// Token: 0x040000DF RID: 223
		private int state = -1;

		// Token: 0x040000E0 RID: 224
		private bool finalState;
	}
}

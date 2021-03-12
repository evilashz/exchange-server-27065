using System;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x020002C1 RID: 705
	internal sealed class EventAreaVisual : CalendarVisual
	{
		// Token: 0x06001B83 RID: 7043 RVA: 0x0009DC0A File Offset: 0x0009BE0A
		public EventAreaVisual(int dataIndex) : base(dataIndex)
		{
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06001B84 RID: 7044 RVA: 0x0009DC13 File Offset: 0x0009BE13
		// (set) Token: 0x06001B85 RID: 7045 RVA: 0x0009DC1B File Offset: 0x0009BE1B
		public bool LeftBreak
		{
			get
			{
				return this.leftBreak;
			}
			set
			{
				this.leftBreak = value;
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06001B86 RID: 7046 RVA: 0x0009DC24 File Offset: 0x0009BE24
		// (set) Token: 0x06001B87 RID: 7047 RVA: 0x0009DC2C File Offset: 0x0009BE2C
		public bool RightBreak
		{
			get
			{
				return this.rightBreak;
			}
			set
			{
				this.rightBreak = value;
			}
		}

		// Token: 0x06001B88 RID: 7048 RVA: 0x0009DC35 File Offset: 0x0009BE35
		public void SetInnerBreak(int position)
		{
			this.innerBreaks |= 1UL << position;
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06001B89 RID: 7049 RVA: 0x0009DC4B File Offset: 0x0009BE4B
		public ulong InnerBreaks
		{
			get
			{
				return this.innerBreaks;
			}
		}

		// Token: 0x040013F9 RID: 5113
		private bool leftBreak;

		// Token: 0x040013FA RID: 5114
		private bool rightBreak;

		// Token: 0x040013FB RID: 5115
		private ulong innerBreaks;
	}
}

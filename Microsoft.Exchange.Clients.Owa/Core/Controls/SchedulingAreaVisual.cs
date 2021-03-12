using System;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x020002D0 RID: 720
	public sealed class SchedulingAreaVisual : CalendarVisual
	{
		// Token: 0x06001BFB RID: 7163 RVA: 0x000A0E71 File Offset: 0x0009F071
		public SchedulingAreaVisual(int dataIndex) : base(dataIndex)
		{
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06001BFC RID: 7164 RVA: 0x000A0E7A File Offset: 0x0009F07A
		// (set) Token: 0x06001BFD RID: 7165 RVA: 0x000A0E82 File Offset: 0x0009F082
		public int Column
		{
			get
			{
				return this.column;
			}
			set
			{
				this.column = value;
			}
		}

		// Token: 0x040014AC RID: 5292
		private int column;
	}
}

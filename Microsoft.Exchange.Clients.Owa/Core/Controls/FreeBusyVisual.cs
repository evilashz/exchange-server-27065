using System;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x020002C7 RID: 711
	internal sealed class FreeBusyVisual : CalendarVisual
	{
		// Token: 0x06001BB3 RID: 7091 RVA: 0x0009E53B File Offset: 0x0009C73B
		public FreeBusyVisual(int dataIndex) : base(dataIndex)
		{
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06001BB4 RID: 7092 RVA: 0x0009E544 File Offset: 0x0009C744
		// (set) Token: 0x06001BB5 RID: 7093 RVA: 0x0009E54C File Offset: 0x0009C74C
		public BusyTypeWrapper FreeBusyIndex
		{
			get
			{
				return this.index;
			}
			set
			{
				this.index = value;
			}
		}

		// Token: 0x0400140A RID: 5130
		private BusyTypeWrapper index;
	}
}

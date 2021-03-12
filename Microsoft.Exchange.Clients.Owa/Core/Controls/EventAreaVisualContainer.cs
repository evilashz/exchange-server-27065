using System;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x020002C2 RID: 706
	internal class EventAreaVisualContainer : CalendarVisualContainer
	{
		// Token: 0x06001B8A RID: 7050 RVA: 0x0009DC53 File Offset: 0x0009BE53
		public EventAreaVisualContainer(DailyViewBase parentView, DateRange dateRange) : base(dateRange)
		{
			if (parentView == null)
			{
				throw new ArgumentNullException("parentView");
			}
			this.mapper = new EventAreaVisualMapper(parentView, this);
		}

		// Token: 0x06001B8B RID: 7051 RVA: 0x0009DC77 File Offset: 0x0009BE77
		public override void MapVisuals()
		{
			this.mapper.MapVisuals();
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06001B8C RID: 7052 RVA: 0x0009DC84 File Offset: 0x0009BE84
		public EventAreaVisualMapper Mapper
		{
			get
			{
				return this.mapper;
			}
		}

		// Token: 0x040013FC RID: 5116
		private EventAreaVisualMapper mapper;
	}
}

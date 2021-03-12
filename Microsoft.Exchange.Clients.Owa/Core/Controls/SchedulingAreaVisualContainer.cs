using System;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x020002D1 RID: 721
	internal class SchedulingAreaVisualContainer : CalendarVisualContainer
	{
		// Token: 0x06001BFE RID: 7166 RVA: 0x000A0E8B File Offset: 0x0009F08B
		public SchedulingAreaVisualContainer(DailyViewBase parentView, DateRange dateRange) : base(dateRange)
		{
			if (parentView == null)
			{
				throw new ArgumentNullException("parentView");
			}
			this.mapper = new SchedulingAreaVisualMapper(parentView, this);
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x000A0EAF File Offset: 0x0009F0AF
		public override void MapVisuals()
		{
			this.mapper.MapVisuals();
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06001C00 RID: 7168 RVA: 0x000A0EBC File Offset: 0x0009F0BC
		public SchedulingAreaVisualMapper Mapper
		{
			get
			{
				return this.mapper;
			}
		}

		// Token: 0x040014AD RID: 5293
		private SchedulingAreaVisualMapper mapper;
	}
}

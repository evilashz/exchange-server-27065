using System;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x020002C8 RID: 712
	internal class FreeBusyVisualContainer : CalendarVisualContainer
	{
		// Token: 0x06001BB6 RID: 7094 RVA: 0x0009E555 File Offset: 0x0009C755
		public FreeBusyVisualContainer(DailyViewBase parentView, DateRange dateRange) : base(dateRange)
		{
			if (parentView == null)
			{
				throw new ArgumentNullException("parentView");
			}
			this.mapper = new FreeBusyVisualMapper(parentView, this);
		}

		// Token: 0x06001BB7 RID: 7095 RVA: 0x0009E579 File Offset: 0x0009C779
		public override void MapVisuals()
		{
			this.mapper.MapVisuals();
		}

		// Token: 0x0400140B RID: 5131
		private FreeBusyVisualMapper mapper;
	}
}

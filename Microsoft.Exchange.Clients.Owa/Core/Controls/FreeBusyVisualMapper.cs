using System;
using System.Collections.Generic;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x020002C9 RID: 713
	internal sealed class FreeBusyVisualMapper : IComparer<CalendarVisual>
	{
		// Token: 0x06001BB8 RID: 7096 RVA: 0x0009E586 File Offset: 0x0009C786
		public FreeBusyVisualMapper(DailyViewBase parentView, FreeBusyVisualContainer dayFreeBusy)
		{
			if (dayFreeBusy == null)
			{
				throw new ArgumentNullException("dayFreeBusy");
			}
			this.dayFreeBusy = dayFreeBusy;
			this.parentView = parentView;
			this.dataSource = parentView.DataSource;
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x0009E5B6 File Offset: 0x0009C7B6
		public void MapVisuals()
		{
			if (this.dayFreeBusy.Count == 0)
			{
				return;
			}
			this.MapVisualsY();
			this.MapVisualsZ();
		}

		// Token: 0x06001BBA RID: 7098 RVA: 0x0009E5D4 File Offset: 0x0009C7D4
		private void MapVisualsY()
		{
			long ticks = ((DateTime)this.dayFreeBusy.DateRange.Start).Ticks;
			long num = ((DateTime)this.dayFreeBusy.DateRange.End).Ticks - ticks;
			for (int i = 0; i < this.dayFreeBusy.Count; i++)
			{
				FreeBusyVisual freeBusyVisual = (FreeBusyVisual)this.dayFreeBusy[i];
				freeBusyVisual.FreeBusyIndex = this.dataSource.GetWrappedBusyType(freeBusyVisual.DataIndex);
				if (freeBusyVisual.FreeBusyIndex == BusyTypeWrapper.Unknown)
				{
					freeBusyVisual.FreeBusyIndex = BusyTypeWrapper.Free;
				}
				ExDateTime startTime = this.dataSource.GetStartTime(freeBusyVisual.DataIndex);
				ExDateTime endTime = this.dataSource.GetEndTime(freeBusyVisual.DataIndex);
				long num2 = ((DateTime)startTime).Ticks - ticks;
				long num3 = ((DateTime)endTime).Ticks - ticks;
				int num4 = (this.parentView.TimeStripMode == TimeStripMode.FifteenMinutes) ? 96 : 48;
				double num5 = (double)num2 * (double)num4 / (double)num;
				freeBusyVisual.Rect.Y = Math.Floor(num5);
				if (freeBusyVisual.Rect.Y < 0.0)
				{
					freeBusyVisual.Rect.Y = 0.0;
					num5 = 0.0;
				}
				double a = (double)num3 * (double)num4 / (double)num;
				freeBusyVisual.Rect.Height = Math.Ceiling(a) - num5;
				if (freeBusyVisual.Rect.Y + freeBusyVisual.Rect.Height > (double)num4)
				{
					freeBusyVisual.Rect.Height = (double)num4 - freeBusyVisual.Rect.Y;
				}
			}
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x0009E784 File Offset: 0x0009C984
		private void MapVisualsZ()
		{
			this.dayFreeBusy.SortVisuals(this);
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x0009E794 File Offset: 0x0009C994
		public int Compare(CalendarVisual x, CalendarVisual y)
		{
			FreeBusyVisual freeBusyVisual = (FreeBusyVisual)x;
			FreeBusyVisual freeBusyVisual2 = (FreeBusyVisual)y;
			return freeBusyVisual.FreeBusyIndex - freeBusyVisual2.FreeBusyIndex;
		}

		// Token: 0x0400140C RID: 5132
		private FreeBusyVisualContainer dayFreeBusy;

		// Token: 0x0400140D RID: 5133
		private DailyViewBase parentView;

		// Token: 0x0400140E RID: 5134
		private ICalendarDataSource dataSource;
	}
}

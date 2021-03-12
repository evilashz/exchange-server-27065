using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x020002C3 RID: 707
	internal class EventAreaVisualMapper
	{
		// Token: 0x06001B8D RID: 7053 RVA: 0x0009DC8C File Offset: 0x0009BE8C
		public EventAreaVisualMapper(CalendarViewBase parentView, IComparer<CalendarVisual> comparer, CalendarVisualContainer visualContainer)
		{
			if (parentView == null)
			{
				throw new ArgumentNullException("parentView");
			}
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			if (visualContainer == null)
			{
				throw new ArgumentNullException("visualContainer");
			}
			this.visualContainer = visualContainer;
			this.parentView = parentView;
			this.dataSource = parentView.DataSource;
			this.comparer = comparer;
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x0009DCEA File Offset: 0x0009BEEA
		public EventAreaVisualMapper(CalendarViewBase parentView, CalendarVisualContainer visualContainer) : this(parentView, new EventAreaVisualMapper.EventAreaVisualComparer(parentView.DataSource), visualContainer)
		{
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06001B8F RID: 7055 RVA: 0x0009DCFF File Offset: 0x0009BEFF
		protected CalendarVisualContainer VisualContainer
		{
			get
			{
				return this.visualContainer;
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06001B90 RID: 7056 RVA: 0x0009DD07 File Offset: 0x0009BF07
		protected ICalendarDataSource DataSource
		{
			get
			{
				return this.dataSource;
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06001B91 RID: 7057 RVA: 0x0009DD0F File Offset: 0x0009BF0F
		protected CalendarViewBase ParentView
		{
			get
			{
				return this.parentView;
			}
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x0009DD17 File Offset: 0x0009BF17
		public void MapVisuals()
		{
			if (this.visualContainer.Count == 0)
			{
				return;
			}
			this.MapVisualsX();
			this.MapVisualsY();
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x0009DD34 File Offset: 0x0009BF34
		protected virtual void MapVisualsX()
		{
			for (int i = 0; i < this.visualContainer.Count; i++)
			{
				EventAreaVisual eventAreaVisual = (EventAreaVisual)this.visualContainer[i];
				eventAreaVisual.Rect.Width = 0.0;
				ExDateTime startTime = this.dataSource.GetStartTime(eventAreaVisual.DataIndex);
				ExDateTime endTime = this.dataSource.GetEndTime(eventAreaVisual.DataIndex);
				DateRange[] dateRanges = this.parentView.DateRanges;
				for (int j = 0; j < this.parentView.DayCount; j++)
				{
					if (dateRanges[j].Intersects(startTime, endTime))
					{
						eventAreaVisual.Rect.Width += 1.0;
						if (eventAreaVisual.Rect.Width == 1.0)
						{
							if (startTime < dateRanges[j].Start.Date)
							{
								eventAreaVisual.LeftBreak = true;
							}
							eventAreaVisual.Rect.X = (double)j;
						}
						else if (eventAreaVisual.Rect.Width > 1.0 && dateRanges[j - 1].Start.Date.IncrementDays(1) != dateRanges[j].Start.Date)
						{
							eventAreaVisual.SetInnerBreak((int)((double)j - eventAreaVisual.Rect.X));
						}
					}
				}
				if (eventAreaVisual.Rect.Width != 0.0)
				{
					int num = (int)(eventAreaVisual.Rect.X + eventAreaVisual.Rect.Width - 1.0);
					if (dateRanges[num].End.Date < endTime)
					{
						eventAreaVisual.RightBreak = true;
					}
				}
			}
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x0009DF0C File Offset: 0x0009C10C
		private void MapVisualsY()
		{
			if (this.visualContainer.Count == 1)
			{
				EventAreaVisual eventAreaVisual = (EventAreaVisual)this.visualContainer[0];
				eventAreaVisual.Rect.Y = 0.0;
			}
			this.visualContainer.SortVisuals(this.comparer);
			this.matrix = new EventAreaVisualMapper.EventAreaMatrix(this.parentView.DayCount);
			for (int i = 0; i < this.visualContainer.Count; i++)
			{
				EventAreaVisual eventAreaVisual2 = (EventAreaVisual)this.visualContainer[i];
				int j;
				for (j = 0; j < this.matrix.RowCount; j++)
				{
					if (this.matrix.FitsInRow(j, eventAreaVisual2.Rect))
					{
						this.matrix.AddToRow(j, eventAreaVisual2.Rect);
						eventAreaVisual2.Rect.Y = (double)j;
						break;
					}
				}
				if (j == this.matrix.RowCount)
				{
					if (this.RowCount >= this.parentView.MaxEventAreaRows)
					{
						this.parentView.RemoveItemFromView(eventAreaVisual2.DataIndex);
					}
					else
					{
						this.matrix.AddRow();
						this.matrix.AddToRow(this.matrix.RowCount - 1, eventAreaVisual2.Rect);
						eventAreaVisual2.Rect.Y = (double)(this.matrix.RowCount - 1);
					}
				}
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06001B95 RID: 7061 RVA: 0x0009E065 File Offset: 0x0009C265
		public int RowCount
		{
			get
			{
				if (this.matrix != null)
				{
					return this.matrix.RowCount;
				}
				return 0;
			}
		}

		// Token: 0x040013FD RID: 5117
		private CalendarVisualContainer visualContainer;

		// Token: 0x040013FE RID: 5118
		private CalendarViewBase parentView;

		// Token: 0x040013FF RID: 5119
		private ICalendarDataSource dataSource;

		// Token: 0x04001400 RID: 5120
		private EventAreaVisualMapper.EventAreaMatrix matrix;

		// Token: 0x04001401 RID: 5121
		private IComparer<CalendarVisual> comparer;

		// Token: 0x020002C4 RID: 708
		private sealed class EventAreaMatrix
		{
			// Token: 0x06001B96 RID: 7062 RVA: 0x0009E07C File Offset: 0x0009C27C
			public EventAreaMatrix(int columnCount)
			{
				this.rows = new ArrayList(2);
				this.columnCount = columnCount;
				this.AddRow();
			}

			// Token: 0x06001B97 RID: 7063 RVA: 0x0009E09D File Offset: 0x0009C29D
			public void AddRow()
			{
				this.rows.Add(new bool[this.columnCount]);
			}

			// Token: 0x1700074C RID: 1868
			// (get) Token: 0x06001B98 RID: 7064 RVA: 0x0009E0B6 File Offset: 0x0009C2B6
			public int RowCount
			{
				get
				{
					return this.rows.Count;
				}
			}

			// Token: 0x06001B99 RID: 7065 RVA: 0x0009E0C4 File Offset: 0x0009C2C4
			public bool FitsInRow(int rowIndex, Rect rect)
			{
				if (rect == null)
				{
					throw new ArgumentNullException("rect");
				}
				int val = (int)rect.X;
				int val2 = (int)(rect.X + rect.Width);
				bool[] array = (bool[])this.rows[rowIndex];
				for (int i = Math.Max(0, val); i < Math.Min(val2, array.Length); i++)
				{
					if (array[i])
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06001B9A RID: 7066 RVA: 0x0009E12C File Offset: 0x0009C32C
			public void AddToRow(int rowIndex, Rect rect)
			{
				if (rect == null)
				{
					throw new ArgumentNullException("rect");
				}
				int val = (int)rect.X;
				int val2 = (int)(rect.X + rect.Width);
				bool[] array = (bool[])this.rows[rowIndex];
				for (int i = Math.Max(0, val); i < Math.Min(val2, array.Length); i++)
				{
					array[i] = true;
				}
			}

			// Token: 0x04001402 RID: 5122
			private ArrayList rows;

			// Token: 0x04001403 RID: 5123
			private int columnCount;
		}

		// Token: 0x020002C5 RID: 709
		private sealed class EventAreaVisualComparer : IComparer<CalendarVisual>
		{
			// Token: 0x06001B9B RID: 7067 RVA: 0x0009E18E File Offset: 0x0009C38E
			public EventAreaVisualComparer(ICalendarDataSource dataSource)
			{
				if (dataSource == null)
				{
					throw new ArgumentNullException("dataSource");
				}
				this.dataSource = dataSource;
			}

			// Token: 0x06001B9C RID: 7068 RVA: 0x0009E1AC File Offset: 0x0009C3AC
			public int Compare(CalendarVisual visual1, CalendarVisual visual2)
			{
				if (visual1.Rect.X == visual2.Rect.X)
				{
					string subject = this.dataSource.GetSubject(visual1.DataIndex);
					string subject2 = this.dataSource.GetSubject(visual2.DataIndex);
					return string.Compare(subject, subject2, StringComparison.CurrentCulture);
				}
				if (visual1.Rect.X > visual2.Rect.X)
				{
					return 1;
				}
				return -1;
			}

			// Token: 0x04001404 RID: 5124
			private ICalendarDataSource dataSource;
		}
	}
}

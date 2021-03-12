using System;
using System.Collections.Generic;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x020002D2 RID: 722
	internal sealed class SchedulingAreaVisualMapper
	{
		// Token: 0x06001C01 RID: 7169 RVA: 0x000A0EC4 File Offset: 0x0009F0C4
		public SchedulingAreaVisualMapper(DailyViewBase parentView, SchedulingAreaVisualContainer viewDay)
		{
			if (viewDay == null)
			{
				throw new ArgumentNullException("viewDay");
			}
			this.viewDay = viewDay;
			this.parentView = parentView;
			this.dataSource = parentView.DataSource;
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x000A0EF4 File Offset: 0x0009F0F4
		public void MapVisuals()
		{
			if (this.viewDay.Count == 0)
			{
				return;
			}
			this.MapVisualsY();
			this.MapVisualsX();
		}

		// Token: 0x06001C03 RID: 7171 RVA: 0x000A0F10 File Offset: 0x0009F110
		private void MapVisualsY()
		{
			long ticks = this.viewDay.DateRange.VisualStart.Value.Ticks;
			long num = this.viewDay.DateRange.VisualEnd.Value.Ticks - ticks;
			int num2 = (int)(this.viewDay.DateRange.VisualEnd.Value - this.viewDay.DateRange.VisualStart.Value).TotalHours;
			for (int i = 0; i < this.viewDay.Count; i++)
			{
				SchedulingAreaVisual schedulingAreaVisual = (SchedulingAreaVisual)this.viewDay[i];
				ExDateTime startTime = this.dataSource.GetStartTime(schedulingAreaVisual.DataIndex);
				ExDateTime endTime = this.dataSource.GetEndTime(schedulingAreaVisual.DataIndex);
				long num3 = ((DateTime)startTime).Ticks - ticks;
				long num4 = ((DateTime)endTime).Ticks - ticks;
				int num5 = (this.parentView.TimeStripMode == TimeStripMode.FifteenMinutes) ? (num2 * 4) : (num2 * 2);
				double num6 = (double)num3 * (double)num5 / (double)num;
				schedulingAreaVisual.Rect.Y = num6;
				double num7 = (double)num4 * (double)num5 / (double)num;
				schedulingAreaVisual.Rect.Height = num7 - num6;
				schedulingAreaVisual.AdjustedRect.Y = schedulingAreaVisual.Rect.Y;
				schedulingAreaVisual.AdjustedRect.Height = schedulingAreaVisual.Rect.Height;
				if (schedulingAreaVisual.Rect.Y < 0.0 && schedulingAreaVisual.Rect.Y + schedulingAreaVisual.Rect.Height < 1.0)
				{
					schedulingAreaVisual.AdjustedRect.Height = 1.0 - schedulingAreaVisual.Rect.Y;
				}
				double num8 = 23.5 * (double)((this.parentView.TimeStripMode == TimeStripMode.FifteenMinutes) ? 4 : 2);
				if (schedulingAreaVisual.Rect.Y > num8)
				{
					double num9 = schedulingAreaVisual.Rect.Y - num8;
					schedulingAreaVisual.AdjustedRect.Y = num8;
					schedulingAreaVisual.AdjustedRect.Height += num9;
				}
				if (schedulingAreaVisual.Rect.Height < 1.0)
				{
					schedulingAreaVisual.AdjustedRect.Height = 1.0;
				}
			}
		}

		// Token: 0x06001C04 RID: 7172 RVA: 0x000A1194 File Offset: 0x0009F394
		private void MapVisualsX()
		{
			if (this.viewDay.Count == 1)
			{
				this.viewDay[0].Rect.X = 0.0;
				this.viewDay[0].Rect.Width = 1.0;
				return;
			}
			if (this.viewDay.Count != 2)
			{
				this.viewDay.SortVisuals(new SchedulingAreaVisualMapper.SchedulingAreaVisualComparer(this.dataSource));
				for (int i = 0; i < this.viewDay.Count; i++)
				{
					Rect rect = new Rect(this.viewDay[i].AdjustedRect);
					int num = 1;
					while (i + num < this.viewDay.Count && rect.IntersectsY(this.viewDay[i + num].AdjustedRect))
					{
						rect.Add(this.viewDay[i + num].AdjustedRect);
						num++;
					}
					if (num == 1)
					{
						this.viewDay[i].Rect.X = 0.0;
						this.viewDay[i].Rect.Width = 1.0;
					}
					else
					{
						int num2 = 1;
						List<SchedulingAreaVisual>[] array = new List<SchedulingAreaVisual>[num];
						SchedulingAreaVisual schedulingAreaVisual = (SchedulingAreaVisual)this.viewDay[i];
						schedulingAreaVisual.Column = 0;
						for (int j = 1; j < num; j++)
						{
							ulong num3 = 0UL;
							for (int k = 0; k < j; k++)
							{
								if (this.viewDay[i + k].AdjustedRect.IntersectsY(this.viewDay[i + j].AdjustedRect))
								{
									schedulingAreaVisual = (SchedulingAreaVisual)this.viewDay[i + k];
									num3 |= 1UL << schedulingAreaVisual.Column;
								}
							}
							int num4 = 0;
							while ((num3 & 1UL << num4) != 0UL)
							{
								num4++;
							}
							schedulingAreaVisual = (SchedulingAreaVisual)this.viewDay[i + j];
							if (num4 >= this.parentView.MaxConflictingItems)
							{
								this.parentView.RemoveItemFromView(schedulingAreaVisual.DataIndex);
							}
							else
							{
								schedulingAreaVisual.Column = num4;
								if (array[schedulingAreaVisual.Column] == null)
								{
									array[schedulingAreaVisual.Column] = new List<SchedulingAreaVisual>();
								}
								array[schedulingAreaVisual.Column].Add(schedulingAreaVisual);
								if (num4 + 1 > num2)
								{
									num2 = num4 + 1;
								}
							}
						}
						if (num2 > this.conflictCount)
						{
							this.conflictCount = num2;
						}
						double num5 = 1.0 / (double)num2;
						for (int l = 0; l < num; l++)
						{
							schedulingAreaVisual = (SchedulingAreaVisual)this.viewDay[i + l];
							schedulingAreaVisual.Rect.X = (double)schedulingAreaVisual.Column * num5;
							int num6 = 1;
							for (int m = schedulingAreaVisual.Column + 1; m < num2; m++)
							{
								List<SchedulingAreaVisual> list = array[m];
								if (list != null)
								{
									bool flag = false;
									for (int n = 0; n < list.Count; n++)
									{
										SchedulingAreaVisual schedulingAreaVisual2 = list[n];
										if (schedulingAreaVisual.AdjustedRect.IntersectsY(schedulingAreaVisual2.AdjustedRect))
										{
											flag = true;
											break;
										}
									}
									if (flag)
									{
										break;
									}
								}
								num6++;
							}
							schedulingAreaVisual.Rect.Width = num5 * (double)num6;
						}
						i += num - 1;
					}
				}
				return;
			}
			this.viewDay.SortVisuals(new SchedulingAreaVisualMapper.SchedulingAreaVisualComparer(this.dataSource));
			if (this.viewDay[0].AdjustedRect.IntersectsY(this.viewDay[1].AdjustedRect))
			{
				this.viewDay[0].Rect.X = 0.0;
				this.viewDay[0].Rect.Width = 0.5;
				this.viewDay[1].Rect.X = 0.5;
				this.viewDay[1].Rect.Width = 0.5;
				return;
			}
			this.viewDay[0].Rect.X = 0.0;
			this.viewDay[0].Rect.Width = 1.0;
			this.viewDay[1].Rect.X = 0.0;
			this.viewDay[1].Rect.Width = 1.0;
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06001C05 RID: 7173 RVA: 0x000A1636 File Offset: 0x0009F836
		public int ConflictCount
		{
			get
			{
				return this.conflictCount;
			}
		}

		// Token: 0x040014AE RID: 5294
		private SchedulingAreaVisualContainer viewDay;

		// Token: 0x040014AF RID: 5295
		private DailyViewBase parentView;

		// Token: 0x040014B0 RID: 5296
		private ICalendarDataSource dataSource;

		// Token: 0x040014B1 RID: 5297
		private int conflictCount;

		// Token: 0x020002D3 RID: 723
		public sealed class SchedulingAreaVisualComparer : IComparer<CalendarVisual>
		{
			// Token: 0x06001C06 RID: 7174 RVA: 0x000A163E File Offset: 0x0009F83E
			public SchedulingAreaVisualComparer(ICalendarDataSource dataSource)
			{
				if (dataSource == null)
				{
					throw new ArgumentNullException("dataSource");
				}
				this.dataSource = dataSource;
			}

			// Token: 0x06001C07 RID: 7175 RVA: 0x000A165C File Offset: 0x0009F85C
			public int Compare(CalendarVisual visual1, CalendarVisual visual2)
			{
				if (visual1.Rect.Y == visual2.Rect.Y)
				{
					if (visual1.Rect.Height == visual2.Rect.Height)
					{
						string subject = this.dataSource.GetSubject(visual1.DataIndex);
						string subject2 = this.dataSource.GetSubject(visual2.DataIndex);
						return string.Compare(subject, subject2, StringComparison.CurrentCulture);
					}
					if (visual1.Rect.Height > visual2.Rect.Height)
					{
						return -1;
					}
					return 1;
				}
				else
				{
					if (visual1.Rect.Y < visual2.Rect.Y)
					{
						return -1;
					}
					return 1;
				}
			}

			// Token: 0x040014B2 RID: 5298
			private ICalendarDataSource dataSource;
		}
	}
}

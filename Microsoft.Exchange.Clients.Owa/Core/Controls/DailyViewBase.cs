using System;
using System.Collections;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x02000030 RID: 48
	internal abstract class DailyViewBase : CalendarViewBase
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000138 RID: 312
		public abstract int MaxConflictingItems { get; }

		// Token: 0x06000139 RID: 313 RVA: 0x00009D47 File Offset: 0x00007F47
		protected virtual TimeStripMode GetTimeStripMode()
		{
			return DailyViewBase.GetPersistedTimeStripMode(base.SessionContext);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00009D54 File Offset: 0x00007F54
		protected DailyViewBase(ISessionContext sessionContext, CalendarAdapterBase calendarAdapter) : base(sessionContext, calendarAdapter)
		{
			ExTraceGlobals.CalendarCallTracer.TraceDebug(0L, "DailyViewBase.DailyViewBase");
			this.timeStripMode = this.GetTimeStripMode();
			if (calendarAdapter.DataSource != null)
			{
				if (base.DateRanges != null)
				{
					Array.Sort(base.DateRanges, base.DateRanges[0]);
				}
				ExTraceGlobals.CalendarTracer.TraceDebug(0L, "Creating and mapping visuals in the view");
				this.CreateVisuals();
				this.MapVisuals();
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00009DD0 File Offset: 0x00007FD0
		private void CreateVisuals()
		{
			this.viewDays = new SchedulingAreaVisualContainer[base.DayCount];
			this.eventArea = new EventAreaVisualContainer(this, null);
			this.rolledUpFreeBusy = new FreeBusyVisualContainer[base.DayCount];
			this.rowFreeBusy = new BusyTypeWrapper[base.DayCount][];
			for (int i = 0; i < base.DayCount; i++)
			{
				this.viewDays[i] = new SchedulingAreaVisualContainer(this, base.DateRanges[i]);
				this.rolledUpFreeBusy[i] = new FreeBusyVisualContainer(this, base.DateRanges[i]);
			}
			for (int i = 0; i < base.DataSource.Count; i++)
			{
				if (i > this.MaxItemsPerView)
				{
					base.RemoveItemFromView(i);
				}
				else
				{
					ExDateTime startTime = base.DataSource.GetStartTime(i);
					ExDateTime endTime = base.DataSource.GetEndTime(i);
					if ((endTime - startTime).Days > 0)
					{
						this.eventArea.AddVisual(new EventAreaVisual(i));
						for (int j = 0; j < base.DateRanges.Length; j++)
						{
							if (base.DateRanges[j].Intersects(startTime, endTime))
							{
								this.rolledUpFreeBusy[j].AddVisual(new FreeBusyVisual(i));
							}
						}
					}
					else
					{
						int k = 0;
						while (k < base.DateRanges.Length)
						{
							if (base.DateRanges[k].Intersects(startTime, endTime))
							{
								this.viewDays[k].AddVisual(new SchedulingAreaVisual(i));
								if (startTime >= base.DateRanges[k].Start && endTime > base.DateRanges[k].End && k < base.DateRanges.Length - 1 && endTime > base.DateRanges[k + 1].Start)
								{
									this.viewDays[k + 1].AddVisual(new SchedulingAreaVisual(i));
									break;
								}
								break;
							}
							else
							{
								k++;
							}
						}
					}
				}
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00009FBC File Offset: 0x000081BC
		private void MapVisuals()
		{
			for (int i = 0; i < this.viewDays.Length; i++)
			{
				this.viewDays[i].MapVisuals();
			}
			this.eventArea.MapVisuals();
			if (base.DataSource != null)
			{
				this.transformedRolledUpFreeBusy = new ArrayList[base.DayCount];
				for (int j = 0; j < base.DayCount; j++)
				{
					this.rolledUpFreeBusy[j].MapVisuals();
					this.TransformRolledUpFreeBusyVisuals(j);
				}
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000A034 File Offset: 0x00008234
		private void TransformRolledUpFreeBusyVisuals(int iDay)
		{
			ArrayList arrayList = null;
			this.rowFreeBusy[iDay] = new BusyTypeWrapper[24 * ((this.timeStripMode == TimeStripMode.FifteenMinutes) ? 4 : 2)];
			BusyTypeWrapper[] array = this.rowFreeBusy[iDay];
			FreeBusyVisualContainer freeBusyVisualContainer = this.rolledUpFreeBusy[iDay];
			for (int i = 0; i < freeBusyVisualContainer.Count; i++)
			{
				FreeBusyVisual freeBusyVisual = (FreeBusyVisual)freeBusyVisualContainer[i];
				if (!base.IsItemRemoved(freeBusyVisual.DataIndex))
				{
					int num = 0;
					while ((double)num < freeBusyVisual.Rect.Height)
					{
						if (array[(int)freeBusyVisual.Rect.Y + num] < freeBusyVisual.FreeBusyIndex)
						{
							array[(int)freeBusyVisual.Rect.Y + num] = freeBusyVisual.FreeBusyIndex;
						}
						num++;
					}
				}
			}
			if (base.DataSource != null)
			{
				int num2 = 0;
				int num3 = 1;
				BusyTypeWrapper busyTypeWrapper = array[num2];
				for (int j = 1; j < array.Length; j++)
				{
					if (array[j] == busyTypeWrapper)
					{
						num3++;
					}
					else
					{
						if (arrayList == null)
						{
							arrayList = new ArrayList();
						}
						arrayList.Add(new FreeBusyVisual(0)
						{
							FreeBusyIndex = busyTypeWrapper,
							Rect = 
							{
								Y = (double)num2,
								Height = (double)num3
							}
						});
						num2 = j;
						num3 = 1;
						busyTypeWrapper = array[j];
					}
				}
				if (arrayList == null)
				{
					arrayList = new ArrayList();
				}
				arrayList.Add(new FreeBusyVisual(0)
				{
					FreeBusyIndex = busyTypeWrapper,
					Rect = 
					{
						Y = (double)num2,
						Height = (double)num3
					}
				});
				this.transformedRolledUpFreeBusy[iDay] = arrayList;
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000A1B8 File Offset: 0x000083B8
		public static TimeStripMode GetPersistedTimeStripMode(ISessionContext sessionContext)
		{
			if (sessionContext == null)
			{
				throw new ArgumentNullException("sessionContext");
			}
			int hourIncrement = sessionContext.HourIncrement;
			if (hourIncrement != 15)
			{
				return TimeStripMode.ThirtyMinutes;
			}
			return TimeStripMode.FifteenMinutes;
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600013F RID: 319 RVA: 0x0000A1E4 File Offset: 0x000083E4
		public override SanitizedHtmlString FolderDateDescription
		{
			get
			{
				if (this.dateDescription == null)
				{
					DateRange[] dateRanges = base.CalendarAdapter.DateRanges;
					ExDateTime start = dateRanges[0].Start;
					ExDateTime start2 = dateRanges[dateRanges.Length - 1].Start;
					string text = start.ToString("y", base.SessionContext.UserCulture);
					if (dateRanges.Length > 1 && (start.Month != start2.Month || start.Year != start2.Year))
					{
						text = text + " - " + start2.ToString("y", base.SessionContext.UserCulture);
					}
					this.dateDescription = SanitizedHtmlString.GetSanitizedStringWithoutEncoding(text);
				}
				return this.dateDescription;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000140 RID: 320 RVA: 0x0000A291 File Offset: 0x00008491
		public int EventAreaRowCount
		{
			get
			{
				if (this.eventArea != null)
				{
					return this.eventArea.Mapper.RowCount;
				}
				return 0;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000141 RID: 321 RVA: 0x0000A2B0 File Offset: 0x000084B0
		public int VisualCount
		{
			get
			{
				int num = this.eventArea.Count;
				for (int i = 0; i < this.viewDays.Length; i++)
				{
					num += this.viewDays[i].Count;
				}
				return num;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000142 RID: 322 RVA: 0x0000A2ED File Offset: 0x000084ED
		public TimeStripMode TimeStripMode
		{
			get
			{
				return this.timeStripMode;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000143 RID: 323 RVA: 0x0000A2F5 File Offset: 0x000084F5
		public SchedulingAreaVisualContainer[] ViewDays
		{
			get
			{
				return this.viewDays;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000144 RID: 324 RVA: 0x0000A2FD File Offset: 0x000084FD
		protected ArrayList[] TransformedRolledUpFreeBusy
		{
			get
			{
				return this.transformedRolledUpFreeBusy;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000145 RID: 325 RVA: 0x0000A305 File Offset: 0x00008505
		public BusyTypeWrapper[][] RowFreeBusy
		{
			get
			{
				return this.rowFreeBusy;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000146 RID: 326 RVA: 0x0000A30D File Offset: 0x0000850D
		public EventAreaVisualContainer EventArea
		{
			get
			{
				return this.eventArea;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000147 RID: 327 RVA: 0x0000A315 File Offset: 0x00008515
		public bool IsLimitHit
		{
			get
			{
				return base.RemovedItemCount > 0;
			}
		}

		// Token: 0x040000C7 RID: 199
		private SchedulingAreaVisualContainer[] viewDays;

		// Token: 0x040000C8 RID: 200
		private FreeBusyVisualContainer[] rolledUpFreeBusy;

		// Token: 0x040000C9 RID: 201
		private ArrayList[] transformedRolledUpFreeBusy;

		// Token: 0x040000CA RID: 202
		private EventAreaVisualContainer eventArea;

		// Token: 0x040000CB RID: 203
		private TimeStripMode timeStripMode = TimeStripMode.ThirtyMinutes;

		// Token: 0x040000CC RID: 204
		private BusyTypeWrapper[][] rowFreeBusy;

		// Token: 0x040000CD RID: 205
		private SanitizedHtmlString dateDescription;
	}
}

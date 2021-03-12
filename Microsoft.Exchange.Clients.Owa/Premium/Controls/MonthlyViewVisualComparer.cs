using System;
using System.Collections.Generic;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003C1 RID: 961
	internal sealed class MonthlyViewVisualComparer : IComparer<CalendarVisual>
	{
		// Token: 0x060023F1 RID: 9201 RVA: 0x000CF6F9 File Offset: 0x000CD8F9
		public MonthlyViewVisualComparer(ICalendarDataSource dataSource)
		{
			if (dataSource == null)
			{
				throw new ArgumentNullException("dataSource");
			}
			this.dataSource = dataSource;
		}

		// Token: 0x060023F2 RID: 9202 RVA: 0x000CF718 File Offset: 0x000CD918
		public int Compare(CalendarVisual visual1, CalendarVisual visual2)
		{
			if (object.ReferenceEquals(visual1, visual2))
			{
				return 0;
			}
			if (visual1.Rect.X == visual2.Rect.X)
			{
				TimeSpan t = this.Duration(visual1);
				TimeSpan t2 = this.Duration(visual2);
				ExDateTime exDateTime = this.StartTime(visual1);
				ExDateTime exDateTime2 = this.StartTime(visual2);
				int num = this.FreeBusyStatus(visual1);
				int num2 = this.FreeBusyStatus(visual2);
				if (t.Days >= 1 || t2.Days >= 1)
				{
					if (t != t2)
					{
						if (!(t > t2))
						{
							return 1;
						}
						return -1;
					}
					else if (num != num2)
					{
						if (num <= num2)
						{
							return 1;
						}
						return -1;
					}
					else if (exDateTime != exDateTime2)
					{
						return ExDateTime.Compare(exDateTime, exDateTime2);
					}
				}
				else
				{
					if (exDateTime != exDateTime2)
					{
						return ExDateTime.Compare(exDateTime, exDateTime2);
					}
					if (num != num2)
					{
						if (num <= num2)
						{
							return 1;
						}
						return -1;
					}
					else if (t != t2)
					{
						if (!(t > t2))
						{
							return 1;
						}
						return -1;
					}
				}
				string subject = this.dataSource.GetSubject(visual1.DataIndex);
				string subject2 = this.dataSource.GetSubject(visual2.DataIndex);
				return string.Compare(subject, subject2, StringComparison.CurrentCulture);
			}
			if (visual1.Rect.X <= visual2.Rect.X)
			{
				return -1;
			}
			return 1;
		}

		// Token: 0x060023F3 RID: 9203 RVA: 0x000CF848 File Offset: 0x000CDA48
		private ExDateTime StartTime(CalendarVisual visual)
		{
			return this.dataSource.GetStartTime(visual.DataIndex);
		}

		// Token: 0x060023F4 RID: 9204 RVA: 0x000CF85B File Offset: 0x000CDA5B
		private TimeSpan Duration(CalendarVisual visual)
		{
			return this.dataSource.GetEndTime(visual.DataIndex) - this.StartTime(visual);
		}

		// Token: 0x060023F5 RID: 9205 RVA: 0x000CF87A File Offset: 0x000CDA7A
		private int FreeBusyStatus(CalendarVisual visual)
		{
			return (int)this.dataSource.GetWrappedBusyType(visual.DataIndex);
		}

		// Token: 0x040018F2 RID: 6386
		private ICalendarDataSource dataSource;
	}
}

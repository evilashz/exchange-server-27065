using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x020002B3 RID: 691
	internal abstract class CalendarVisualContainer
	{
		// Token: 0x06001B0B RID: 6923 RVA: 0x0009B649 File Offset: 0x00099849
		public CalendarVisualContainer(DateRange dateRange)
		{
			this.dateRange = dateRange;
		}

		// Token: 0x06001B0C RID: 6924 RVA: 0x0009B658 File Offset: 0x00099858
		public void AddVisual(CalendarVisual visual)
		{
			if (visual == null)
			{
				throw new ArgumentNullException("visual");
			}
			if (this.visuals == null)
			{
				this.visuals = new List<CalendarVisual>(4);
			}
			this.visuals.Add(visual);
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06001B0D RID: 6925 RVA: 0x0009B688 File Offset: 0x00099888
		public DateRange DateRange
		{
			get
			{
				return this.dateRange;
			}
		}

		// Token: 0x1700071C RID: 1820
		public CalendarVisual this[int index]
		{
			get
			{
				return this.visuals[index];
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06001B0F RID: 6927 RVA: 0x0009B69E File Offset: 0x0009989E
		public int Count
		{
			get
			{
				if (this.visuals == null)
				{
					return 0;
				}
				return this.visuals.Count;
			}
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x0009B6B5 File Offset: 0x000998B5
		public void SortVisuals(IComparer<CalendarVisual> comparer)
		{
			this.visuals.Sort(comparer);
		}

		// Token: 0x06001B11 RID: 6929
		public abstract void MapVisuals();

		// Token: 0x04001325 RID: 4901
		private DateRange dateRange;

		// Token: 0x04001326 RID: 4902
		private List<CalendarVisual> visuals;
	}
}

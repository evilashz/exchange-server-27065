using System;
using Microsoft.Exchange.Clients.Owa.Core.Controls;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003C2 RID: 962
	internal class MonthlyViewVisualContainer : CalendarVisualContainer
	{
		// Token: 0x060023F6 RID: 9206 RVA: 0x000CF88D File Offset: 0x000CDA8D
		public MonthlyViewVisualContainer(CalendarViewBase parentView) : base(null)
		{
			if (parentView == null)
			{
				throw new ArgumentNullException("parentView");
			}
			this.mapper = new MonthlyViewVisualMapper(parentView, new MonthlyViewVisualComparer(parentView.DataSource), this);
		}

		// Token: 0x060023F7 RID: 9207 RVA: 0x000CF8BC File Offset: 0x000CDABC
		public override void MapVisuals()
		{
			this.mapper.MapVisuals();
		}

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x060023F8 RID: 9208 RVA: 0x000CF8C9 File Offset: 0x000CDAC9
		public MonthlyViewVisualMapper Mapper
		{
			get
			{
				return this.mapper;
			}
		}

		// Token: 0x040018F3 RID: 6387
		private MonthlyViewVisualMapper mapper;
	}
}

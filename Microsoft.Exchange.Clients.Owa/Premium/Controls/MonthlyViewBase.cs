using System;
using System.Globalization;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003BE RID: 958
	internal class MonthlyViewBase : CalendarViewBase
	{
		// Token: 0x060023DB RID: 9179 RVA: 0x000CED20 File Offset: 0x000CCF20
		public MonthlyViewBase(ISessionContext sessionContext, CalendarAdapterBase calendarAdapter) : base(sessionContext, calendarAdapter)
		{
			if (calendarAdapter != null && calendarAdapter.DateRanges != null && calendarAdapter.DataSource != null)
			{
				this.CreateVisuals();
				this.MapVisuals();
				this.monthName = base.DateRanges[7].Start.ToString("y", sessionContext.UserCulture);
			}
		}

		// Token: 0x060023DC RID: 9180 RVA: 0x000CED7C File Offset: 0x000CCF7C
		private void CreateVisuals()
		{
			this.visualContainer = new MonthlyViewVisualContainer(this);
			for (int i = 0; i < base.DataSource.Count; i++)
			{
				if (i > this.MaxItemsPerView)
				{
					base.RemoveItemFromView(i);
				}
				else
				{
					this.visualContainer.AddVisual(new EventAreaVisual(i));
				}
			}
		}

		// Token: 0x060023DD RID: 9181 RVA: 0x000CEDCE File Offset: 0x000CCFCE
		private void MapVisuals()
		{
			this.visualContainer.MapVisuals();
		}

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x060023DE RID: 9182 RVA: 0x000CEDDB File Offset: 0x000CCFDB
		public override int MaxEventAreaRows
		{
			get
			{
				return 100;
			}
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x060023DF RID: 9183 RVA: 0x000CEDDF File Offset: 0x000CCFDF
		public override int MaxItemsPerView
		{
			get
			{
				return 1000;
			}
		}

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x060023E0 RID: 9184 RVA: 0x000CEDE6 File Offset: 0x000CCFE6
		public override SanitizedHtmlString FolderDateDescription
		{
			get
			{
				return SanitizedHtmlString.GetSanitizedStringWithoutEncoding(this.monthName);
			}
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x060023E1 RID: 9185 RVA: 0x000CEDF3 File Offset: 0x000CCFF3
		public MonthlyViewVisualContainer VisualContainer
		{
			get
			{
				return this.visualContainer;
			}
		}

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x060023E2 RID: 9186 RVA: 0x000CEDFC File Offset: 0x000CCFFC
		public string PreviousMonthName
		{
			get
			{
				ExDateTime start = base.DateRanges[0].Start;
				if (start.Day == 1)
				{
					return string.Empty;
				}
				int num = start.Month - 1;
				return CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[num];
			}
		}

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x060023E3 RID: 9187 RVA: 0x000CEE44 File Offset: 0x000CD044
		public string CurrentMonthName
		{
			get
			{
				int num = base.DateRanges[7].Start.Month - 1;
				return CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[num];
			}
		}

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x060023E4 RID: 9188 RVA: 0x000CEE7C File Offset: 0x000CD07C
		public string NextMonthName
		{
			get
			{
				ExDateTime start = base.DateRanges[base.DateRanges.Length - 1].Start;
				if (start.Day < 7)
				{
					int num = start.Month - 1;
					return CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[num];
				}
				return string.Empty;
			}
		}

		// Token: 0x040018EA RID: 6378
		public const int RowHeight = 20;

		// Token: 0x040018EB RID: 6379
		public const int DaysOfAWeek = 7;

		// Token: 0x040018EC RID: 6380
		private MonthlyViewVisualContainer visualContainer;

		// Token: 0x040018ED RID: 6381
		protected string monthName;
	}
}

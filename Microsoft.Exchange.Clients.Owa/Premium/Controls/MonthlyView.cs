using System;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003BF RID: 959
	internal sealed class MonthlyView : MonthlyViewBase, ICalendarViewControl
	{
		// Token: 0x060023E5 RID: 9189 RVA: 0x000CEECB File Offset: 0x000CD0CB
		public MonthlyView(ISessionContext sessionContext, CalendarAdapterBase calendarAdapter) : base(sessionContext, calendarAdapter)
		{
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x060023E6 RID: 9190 RVA: 0x000CEED5 File Offset: 0x000CD0D5
		// (set) Token: 0x060023E7 RID: 9191 RVA: 0x000CEEDD File Offset: 0x000CD0DD
		public OwaStoreObjectId SelectedItemId
		{
			get
			{
				return this.selectedItemId;
			}
			set
			{
				if (value == null)
				{
					throw new InvalidOperationException("SelectedItemId cannot be null");
				}
				this.selectedItemId = value;
			}
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x060023E8 RID: 9192 RVA: 0x000CEEF4 File Offset: 0x000CD0F4
		public int Count
		{
			get
			{
				return base.VisualContainer.Count;
			}
		}

		// Token: 0x060023E9 RID: 9193 RVA: 0x000CEF04 File Offset: 0x000CD104
		public void RenderView(TextWriter writer, bool visible)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write("<div id=\"divMV\"");
			if (!visible)
			{
				writer.Write(" style=\"display:none;\"");
			}
			writer.Write(">");
			writer.Write("<div id=\"divMVH\">");
			CalendarUtilities.RenderPreviousNextButtons(base.SessionContext, writer);
			writer.Write("<div id=\"divPrevMonth\"></div><div id=\"divCurrMonth\"></div><div id=\"divNextMonth\"></div>");
			writer.Write("</div>");
			for (int i = 0; i < 6; i++)
			{
				writer.Write("<div class=\"weekSelector\" id=\"divWeekSelector");
				writer.Write(i.ToString(CultureInfo.InvariantCulture));
				writer.Write("\"><div id=\"divWeekNumber\"></div></div>");
			}
			writer.Write("<div id=\"divMVContainer");
			writer.Write(Utilities.SanitizeHtmlEncode(base.CalendarAdapter.IdentityString));
			writer.Write("\" class=\"calContainer\">");
			writer.Write("<div id=\"divMVTab\"></div>");
			writer.Write("<div id=\"divMVBody\">");
			ThemeFileId themeFileId = base.SessionContext.IsRtl ? ThemeFileId.MonthlyViewExpandOverflowRtl : ThemeFileId.MonthlyViewExpandOverflow;
			for (int j = 0; j < 6; j++)
			{
				writer.Write("<div class=\"monthlyViewWeek\" id=\"divWeek");
				writer.Write(j.ToString(CultureInfo.InvariantCulture));
				writer.Write("\">");
				for (int k = 0; k < 7; k++)
				{
					writer.Write("<div class=\"monthlyViewCell\" id=\"divDay");
					writer.Write(k.ToString(CultureInfo.InvariantCulture));
					writer.Write("\">");
					writer.Write("<div id=\"divDayHeader\"><div id=\"divOI\">");
					base.SessionContext.RenderThemeImage(writer, themeFileId);
					writer.Write("</div></div>");
					writer.Write("<div id=\"divMonthlyViewDateName\"></div></div>");
				}
				writer.Write("</div>");
			}
			writer.Write("<div id=\"divWeekHeader\" class=\"abs\">");
			for (int l = 0; l < 7; l++)
			{
				writer.Write("<div class=\"weekdayHeader\" id=\"divDay");
				writer.Write(l.ToString(CultureInfo.InvariantCulture));
				writer.Write("\"></div>");
			}
			writer.Write("</div>");
			writer.Write("</div></div></div>");
		}

		// Token: 0x040018EE RID: 6382
		private OwaStoreObjectId selectedItemId;
	}
}

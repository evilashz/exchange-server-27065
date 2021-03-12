using System;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000344 RID: 836
	internal sealed class DailyView : DailyViewBase, ICalendarViewControl
	{
		// Token: 0x06001F99 RID: 8089 RVA: 0x000B60B0 File Offset: 0x000B42B0
		public DailyView(ISessionContext sessionContext, CalendarAdapterBase calendarAdapter) : base(sessionContext, calendarAdapter)
		{
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x06001F9A RID: 8090 RVA: 0x000B60BA File Offset: 0x000B42BA
		public override int MaxEventAreaRows
		{
			get
			{
				return this.MaxItemsPerView;
			}
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06001F9B RID: 8091 RVA: 0x000B60C2 File Offset: 0x000B42C2
		public override int MaxItemsPerView
		{
			get
			{
				return 300;
			}
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06001F9C RID: 8092 RVA: 0x000B60C9 File Offset: 0x000B42C9
		public override int MaxConflictingItems
		{
			get
			{
				return 25;
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06001F9D RID: 8093 RVA: 0x000B60CD File Offset: 0x000B42CD
		// (set) Token: 0x06001F9E RID: 8094 RVA: 0x000B60D5 File Offset: 0x000B42D5
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

		// Token: 0x06001F9F RID: 8095 RVA: 0x000B60EC File Offset: 0x000B42EC
		public void RenderSchedulingArea(TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			output.Write("<div id=\"divDVContainer");
			output.Write(Utilities.SanitizeHtmlEncode(base.CalendarAdapter.IdentityString));
			output.Write("\" class=\"calContainer\">");
			output.Write("<div id=\"divDVBody\">");
			for (int i = 0; i < base.DateRanges.Length; i++)
			{
				output.Write("<div class=\"abs\" id=\"divDay");
				output.Write(i.ToString(CultureInfo.InvariantCulture));
				output.Write("\">");
				output.Write("<div id=\"divSchedulingAreaBack\"></div>");
				output.Write("<div id=\"divSchedulingAreaVisualContainer\"><div id=\"divToolTip\"></div></div>");
				output.Write("</div>");
			}
			output.Write("</div></div>");
		}

		// Token: 0x06001FA0 RID: 8096 RVA: 0x000B61A8 File Offset: 0x000B43A8
		public void RenderHeadersAndEventArea(TextWriter output, bool visible)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			output.Write("<div id=\"divTop\"");
			if (!visible)
			{
				output.Write(" style=\"display:none;\"");
			}
			output.Write(">");
			output.Write("<div id=\"divGAP\">");
			CalendarUtilities.RenderPreviousNextButtons(base.SessionContext, output);
			output.Write("</div>");
			output.Write("<div id=\"divEAContainer");
			output.Write(Utilities.SanitizeHtmlEncode(base.CalendarAdapter.IdentityString));
			output.Write("\" class=\"calContainer\">");
			output.Write("<div id=\"divDVTab\"></div>");
			output.Write("<div id=\"divDVEventBack\">");
			for (int i = 0; i < base.DateRanges.Length; i++)
			{
				output.Write("<div class=\"abs\" id=\"divDay");
				output.Write(i.ToString(CultureInfo.InvariantCulture));
				output.Write("\"><div id=\"divDVEventAreaGradient\"></div></div>");
			}
			output.Write("</div>");
			output.Write("<div id=\"divDVHeader\">");
			if (DateTimeUtilities.GetDaysFormat(base.SessionContext.DateFormat) == null)
			{
			}
			for (int j = 0; j < base.DateRanges.Length; j++)
			{
				DateTimeUtilities.IsToday(base.DateRanges[j].Start);
				output.Write("<div id=\"divDay");
				output.Write(j.ToString(CultureInfo.InvariantCulture));
				output.Write("\" class=\"calHD\">");
				output.Write("<span id=\"spanDayName\"></span><span id=\"spanWeekDayName\"></span>");
				output.Write("</div>");
			}
			output.Write("</div>");
			output.Write("</div>");
			base.SessionContext.RenderThemeImage(output, ThemeFileId.Clear1x1, "calScr", new object[]
			{
				"id=\"imgSACT\", style=\"display:none\""
			});
			base.SessionContext.RenderThemeImage(output, ThemeFileId.Clear1x1, "calScr", new object[]
			{
				"id=\"imgSACB\", style=\"display:none\""
			});
			output.Write("</div>");
			output.Write("<div id=\"divDVEventContainer\"><div id=\"divDVEventBody");
			output.Write(Utilities.SanitizeHtmlEncode(base.CalendarAdapter.IdentityString));
			output.Write("\" class=\"calContainer\"></div></div>");
		}

		// Token: 0x06001FA1 RID: 8097 RVA: 0x000B63B0 File Offset: 0x000B45B0
		public void RenderTimeStrip(TextWriter output)
		{
			output.Write("<div id=\"divTSW\"></div><div id=\"divTSWB\"></div>");
			int value;
			if (base.TimeStripMode == TimeStripMode.FifteenMinutes)
			{
				value = 96;
			}
			else
			{
				value = 48;
			}
			DateTime dateTime = new DateTime(2000, 1, 1, 0, 0, 0);
			string text = DateTimeUtilities.GetHoursFormat(base.SessionContext.TimeFormat);
			if (text == null)
			{
				text = "%h";
			}
			for (int i = 0; i < 24; i++)
			{
				string s = "00";
				if (text[1] == 'h')
				{
					if (i < 12)
					{
						s = Culture.AMDesignator;
					}
					else
					{
						s = Culture.PMDesignator;
					}
				}
				output.Write("<div class=\"timeStripLine\" style=\"height:");
				output.Write(value);
				output.Write("px\"><div class=\"timeStripLeft\">");
				output.Write(dateTime.ToString(text, CultureInfo.InvariantCulture));
				output.Write("</div><div class=\"timeStripRight\">");
				output.Write(Utilities.SanitizeHtmlEncode(s));
				output.Write("</div></div>");
				dateTime = dateTime.AddHours(1.0);
			}
		}

		// Token: 0x06001FA2 RID: 8098 RVA: 0x000B64A4 File Offset: 0x000B46A4
		internal static void RenderSecondaryNavigation(TextWriter output, CalendarFolder folder, UserContext userContext)
		{
			output.Write("<div id=\"divCalPicker\">");
			RenderingUtilities.RenderSecondaryNavigationDatePicker(folder, output, "divErrDP", "dp", userContext);
			new MonthPicker(userContext, "divMp").Render(output);
			output.Write("</div>");
			NavigationHost.RenderNavigationTreeControl(output, userContext, NavigationModule.Calendar);
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06001FA3 RID: 8099 RVA: 0x000B64F2 File Offset: 0x000B46F2
		public int EventAreaPixelHeight
		{
			get
			{
				return 2 + 26 * base.EventAreaRowCount + 12 + 2;
			}
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06001FA4 RID: 8100 RVA: 0x000B6504 File Offset: 0x000B4704
		public int Count
		{
			get
			{
				return base.VisualCount;
			}
		}

		// Token: 0x040016FE RID: 5886
		public const int RowHeight = 24;

		// Token: 0x040016FF RID: 5887
		private OwaStoreObjectId selectedItemId;
	}
}

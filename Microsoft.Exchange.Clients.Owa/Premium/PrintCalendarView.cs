using System;
using System.Collections.Generic;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000461 RID: 1121
	public class PrintCalendarView : OwaPage, IRegistryOnlyForm
	{
		// Token: 0x060029FE RID: 10750 RVA: 0x000EB600 File Offset: 0x000E9800
		protected override void OnLoad(EventArgs e)
		{
			string queryStringParameter = Utilities.GetQueryStringParameter(base.Request, "vt", false);
			int num = 1;
			if (!string.IsNullOrEmpty(queryStringParameter) && (!int.TryParse(queryStringParameter, out num) || num < 1 || num >= 7))
			{
				throw new OwaInvalidRequestException("View type error");
			}
			this.viewType = (CalendarViewType)num;
			string queryStringParameter2 = Utilities.GetQueryStringParameter(base.Request, "wo", false);
			if (queryStringParameter2 == "1" || this.viewType == CalendarViewType.WorkWeek)
			{
				this.workingDayOnly = true;
			}
			this.days = Utilities.GetQueryStringParameterDateTimeArray(base.Request, "d", base.SessionContext.TimeZone, false, 7);
			if (this.days == null || this.days.Length == 0)
			{
				this.days = new ExDateTime[]
				{
					ExDateTime.Now.Date
				};
			}
			string queryStringParameter3 = Utilities.GetQueryStringParameter(base.Request, "st", false);
			string queryStringParameter4 = Utilities.GetQueryStringParameter(base.Request, "et", false);
			if (!string.IsNullOrEmpty(queryStringParameter3))
			{
				int.TryParse(queryStringParameter3, out this.startTime);
			}
			if (!string.IsNullOrEmpty(queryStringParameter4))
			{
				int.TryParse(queryStringParameter4, out this.endTime);
			}
			if (this.startTime >= this.endTime || this.startTime < 0 || this.endTime > 24)
			{
				throw new OwaInvalidRequestException("start time and end time must be in range and start time must early than end time");
			}
			if (this.workingDayOnly)
			{
				if (this.viewType == CalendarViewType.Weekly)
				{
					this.viewType = CalendarViewType.WorkWeek;
				}
				else if (this.viewType == CalendarViewType.WeeklyAgenda)
				{
					this.viewType = CalendarViewType.WorkWeeklyAgenda;
				}
			}
			if (this.viewType == CalendarViewType.Min)
			{
				string queryStringParameter5 = Utilities.GetQueryStringParameter(base.Request, "rn", false);
				if (queryStringParameter5 == "1")
				{
					this.renderNotes = true;
				}
			}
			string queryStringParameter6 = Utilities.GetQueryStringParameter(base.Request, "el", false);
			if (!string.IsNullOrEmpty(queryStringParameter6) && queryStringParameter6 == "1")
			{
				this.printEventList = true;
			}
			string queryStringParameter7 = Utilities.GetQueryStringParameter(base.Request, "id", false);
			if (base.SessionContext is AnonymousSessionContext)
			{
				PublishedCalendarAdapter publishedCalendarAdapter = new PublishedCalendarAdapter((AnonymousSessionContext)base.SessionContext);
				publishedCalendarAdapter.LoadData(CalendarUtilities.QueryProperties, this.days, this.startTime, this.endTime, this.viewType);
				this.calendarAdapter = publishedCalendarAdapter;
			}
			else
			{
				OwaStoreObjectId owaStoreObjectId;
				if (queryStringParameter7 != null)
				{
					owaStoreObjectId = OwaStoreObjectId.CreateFromString(queryStringParameter7);
				}
				else
				{
					ExTraceGlobals.CalendarTracer.TraceDebug(0L, "folder Id is null, using default folder");
					owaStoreObjectId = base.UserContext.CalendarFolderOwaId;
				}
				if (owaStoreObjectId == null)
				{
					throw new OwaInvalidRequestException("Invalid folder id");
				}
				CalendarAdapter calendarAdapter = new CalendarAdapter(base.UserContext, owaStoreObjectId);
				calendarAdapter.LoadData(this.printEventList ? CalendarUtilities.PrintQueryProperties : CalendarUtilities.QueryProperties, this.days, false, this.startTime, this.endTime, ref this.viewType, out this.viewWidth, out this.readingPanePosition);
				this.calendarAdapter = calendarAdapter;
			}
			if (!this.calendarAdapter.UserCanReadItem)
			{
				return;
			}
			base.OnLoad(e);
		}

		// Token: 0x060029FF RID: 10751 RVA: 0x000EB8F0 File Offset: 0x000E9AF0
		protected void RenderCssLink()
		{
			base.SanitizingResponse.Write("<link type=\"text/css\" rel=\"stylesheet\" href=\"");
			base.SessionContext.RenderThemeFileUrl(base.SanitizingResponse, ThemeFileId.PrintCalendarCss);
			base.SanitizingResponse.Write("\">");
		}

		// Token: 0x06002A00 RID: 10752 RVA: 0x000EB925 File Offset: 0x000E9B25
		protected void RenderTitle()
		{
			if (this.calendarAdapter.CalendarTitle != null)
			{
				base.SanitizingResponse.Write(this.calendarAdapter.CalendarTitle);
			}
		}

		// Token: 0x06002A01 RID: 10753 RVA: 0x000EB94C File Offset: 0x000E9B4C
		protected void RenderPrintView()
		{
			if (this.calendarAdapter.UserCanReadItem)
			{
				IPrintCalendarViewControl view;
				if (this.viewType == CalendarViewType.Monthly)
				{
					view = new PrintMonthlyView(base.SessionContext, this.calendarAdapter, this.workingDayOnly);
				}
				else if (this.viewType == CalendarViewType.WeeklyAgenda || this.viewType == CalendarViewType.WorkWeeklyAgenda)
				{
					string queryStringParameter = Utilities.GetQueryStringParameter(base.Request, "al", false);
					bool isHorizontalLayout = queryStringParameter == "1";
					view = new PrintWeeklyAgendaView(base.SessionContext, this.calendarAdapter, this.viewType, isHorizontalLayout);
				}
				else
				{
					view = new PrintDailyView(base.SessionContext, this.calendarAdapter, this.startTime, this.endTime, this.renderNotes);
				}
				this.InternalRenderPrintView(view, 0);
			}
		}

		// Token: 0x06002A02 RID: 10754 RVA: 0x000EBA04 File Offset: 0x000E9C04
		private void InternalRenderPrintView(IPrintCalendarViewControl view, int pageNumber)
		{
			base.SanitizingResponse.Write("<table class=\"titleTable");
			if (base.SessionContext.BrowserType == BrowserType.Chrome || base.SessionContext.BrowserType == BrowserType.Safari)
			{
				base.SanitizingResponse.Write(" chromeAndSafariTable");
			}
			base.SanitizingResponse.Write("\"");
			if (pageNumber != 0)
			{
				base.SanitizingResponse.Write(" style=\"display: none\"");
			}
			base.SanitizingResponse.Write(" id=\"page");
			base.SanitizingResponse.Write(pageNumber);
			base.SanitizingResponse.Write("\">");
			base.SanitizingResponse.Write("<tr class=\"printTitle\"><td class=\"titleTd\">");
			this.RenderPageHeader(view);
			base.SanitizingResponse.Write("</td></tr><tr><td>");
			this.RenderViewBody(view);
			base.SanitizingResponse.Write("</td></tr></table>");
		}

		// Token: 0x06002A03 RID: 10755 RVA: 0x000EBADC File Offset: 0x000E9CDC
		private void RenderPageHeader(IPrintCalendarViewControl view)
		{
			if (view != null)
			{
				base.SanitizingResponse.Write("<table class=\"innerTable titleTable\"><tr><td class=\"titleTd\">");
				base.SanitizingResponse.Write("<div class=\"dateDescription\">");
				base.SanitizingResponse.Write(view.DateDescription);
				base.SanitizingResponse.Write("</div><div class=\"calendarName\">");
				base.SanitizingResponse.Write(view.CalendarName);
				base.SanitizingResponse.Write("</div></td>");
				List<ExDateTime> list = new List<ExDateTime>();
				ExDateTime date = this.days[0].Date;
				ExDateTime[] effectiveDates = view.GetEffectiveDates();
				if (this.viewType != CalendarViewType.Min)
				{
					list.Add(effectiveDates[0].AddMonths(1));
				}
				list.Add(date);
				if (this.viewType == CalendarViewType.Monthly)
				{
					list.Add(effectiveDates[0].AddMonths(-1));
				}
				base.SanitizingResponse.Write("<td class=\"titleTd dpTd");
				base.SanitizingResponse.Write(list.Count);
				base.SanitizingResponse.Write("\"><div id=\"divDatePicker\"");
				if (base.SessionContext.BrowserType == BrowserType.Chrome)
				{
					base.SanitizingResponse.Write(" class=\"chromeDatePicker\"");
				}
				base.SanitizingResponse.Write(">");
				if (effectiveDates.Length > 0)
				{
					foreach (ExDateTime exDateTime in list)
					{
						base.SanitizingResponse.Write("<div class=\"datePickerContainer\">");
						DatePicker datePicker;
						if (exDateTime == date)
						{
							datePicker = new DatePicker("divDatePicker", effectiveDates);
						}
						else
						{
							datePicker = new DatePicker(exDateTime);
						}
						datePicker.Render(base.SanitizingResponse);
						base.SanitizingResponse.Write("</div>");
					}
				}
				base.SanitizingResponse.Write("</div></td></tr></table>");
			}
		}

		// Token: 0x06002A04 RID: 10756 RVA: 0x000EBCAC File Offset: 0x000E9EAC
		private void RenderViewBody(IPrintCalendarViewControl view)
		{
			if (view != null)
			{
				view.RenderView(base.SanitizingResponse);
			}
		}

		// Token: 0x06002A05 RID: 10757 RVA: 0x000EBCC0 File Offset: 0x000E9EC0
		protected void RenderEventList()
		{
			if (this.printEventList && this.calendarAdapter.UserCanReadItem)
			{
				PrintEventList printEventList = new PrintEventList(base.SessionContext, this.calendarAdapter, this.viewType, this.workingDayOnly);
				printEventList.RenderView(base.SanitizingResponse);
			}
		}

		// Token: 0x06002A06 RID: 10758 RVA: 0x000EBD0C File Offset: 0x000E9F0C
		protected override void OnUnload(EventArgs e)
		{
			if (this.calendarAdapter != null)
			{
				this.calendarAdapter.Dispose();
				this.calendarAdapter = null;
			}
		}

		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x06002A07 RID: 10759 RVA: 0x000EBD28 File Offset: 0x000E9F28
		protected override bool UseStrictMode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x06002A08 RID: 10760 RVA: 0x000EBD2B File Offset: 0x000E9F2B
		protected bool PrintEventList
		{
			get
			{
				return this.printEventList;
			}
		}

		// Token: 0x04001C62 RID: 7266
		private const string FolderIdQueryParameter = "id";

		// Token: 0x04001C63 RID: 7267
		private const string StartTimeQueryParameter = "st";

		// Token: 0x04001C64 RID: 7268
		private const string EndTimeQueryParameter = "et";

		// Token: 0x04001C65 RID: 7269
		private const string ViewTypeQueryParameter = "vt";

		// Token: 0x04001C66 RID: 7270
		private const string DayQueryParameter = "d";

		// Token: 0x04001C67 RID: 7271
		private const string FontSizeQueryParameter = "fs";

		// Token: 0x04001C68 RID: 7272
		private const string WorkingDayOnlyParameter = "wo";

		// Token: 0x04001C69 RID: 7273
		private const string RenderNotesParameter = "rn";

		// Token: 0x04001C6A RID: 7274
		private const string AgendaLayoutParameter = "al";

		// Token: 0x04001C6B RID: 7275
		private const string PrintEventListParameter = "el";

		// Token: 0x04001C6C RID: 7276
		private int startTime;

		// Token: 0x04001C6D RID: 7277
		private int endTime = 24;

		// Token: 0x04001C6E RID: 7278
		private CalendarAdapterBase calendarAdapter;

		// Token: 0x04001C6F RID: 7279
		private int viewWidth = 450;

		// Token: 0x04001C70 RID: 7280
		private CalendarViewType viewType;

		// Token: 0x04001C71 RID: 7281
		private ReadingPanePosition readingPanePosition = ReadingPanePosition.Right;

		// Token: 0x04001C72 RID: 7282
		private ExDateTime[] days;

		// Token: 0x04001C73 RID: 7283
		private bool workingDayOnly;

		// Token: 0x04001C74 RID: 7284
		private bool renderNotes;

		// Token: 0x04001C75 RID: 7285
		private bool printEventList;
	}
}
